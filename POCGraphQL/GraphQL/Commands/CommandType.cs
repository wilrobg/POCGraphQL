using Microsoft.EntityFrameworkCore;
using POCGraphQL.DbContexts;
using POCGraphQL.Models;

namespace POCGraphQL.GraphQL.Platforms;

//Code First approach 
public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command");

        descriptor
            .Field(x => x.Platform)
            .UseDbContext<AppDbContext>()
            .ResolveWith<Resolvers>(x => x.GetPlatform(default, default))
            .Description("This is the platform to wich the command belongs");
    }

    private class Resolvers 
    {
        public Platform GetPlatform([Parent] Command command, AppDbContext context)
        {
            return context.Platforms.FirstOrDefault(x => x.Id == command.PlatformId);
        }
    }
}
