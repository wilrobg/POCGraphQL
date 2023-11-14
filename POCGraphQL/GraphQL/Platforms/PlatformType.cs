using HotChocolate.Execution;
using POCGraphQL.DbContexts;
using POCGraphQL.GraphQL.Mutations;
using POCGraphQL.Models;

namespace POCGraphQL.GraphQL.Platforms;

//Code First approach 
public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor
            .Description("Represents any software or service that has a command line interface.");

        descriptor
            .Field(x => x.LicenseKey)
            .Ignore();

        descriptor
            .Field(x => x.Commands)
            .UseDbContext<AppDbContext>()
            .ResolveWith<Resolvers>((r) => r.GetCommands(default, default))
            .Description("This is the list of available commands for the platform.");
    }

    private class Resolvers 
    {
        public IQueryable<Command> GetCommands([Parent] Platform platform, AppDbContext context)
        {
            return context.Commands.Where(x => x.PlatformId == platform.Id);
        }
    }
}
