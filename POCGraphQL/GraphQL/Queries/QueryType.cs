using POCGraphQL.DbContexts;
using POCGraphQL.GraphQL.Platforms;
using POCGraphQL.Models;

namespace POCGraphQL.GraphQL.Queries;

public class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Field(x => x.GetPlatform(default))
            .Type<ListType<NonNullType<PlatformType>>>()
            .UseSorting()
            .UseFiltering();
    }
}
