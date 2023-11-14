using HotChocolate.Execution;

namespace POCGraphQL.GraphQL.Mutations;

public class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field(x => x.AddPlatformAsync(default!, default!))
            .Error<PlatformNameTakenError>();
    }
}

public class PlatformNameTakenException : GraphQLException
{
    public string Name { get; set; }
    public PlatformNameTakenException(string name) 
        :base($"Name {name} is already taken.")
    {
        Name = name;
    }
}

public class PlatformNameTakenError
{
    private PlatformNameTakenError(PlatformNameTakenException ex)
    {
        Message = ex.Message;
    }

    public string Message { get; }
}