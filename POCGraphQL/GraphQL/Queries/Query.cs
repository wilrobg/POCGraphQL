using POCGraphQL.DbContexts;
using POCGraphQL.Models;

namespace POCGraphQL.GraphQL.Queries;

//Annotation-base approach 
public class Query
{

    //[UseProjection] Used for Annotation-based Approach
    public IQueryable<Platform> GetPlatform(AppDbContext context) =>
        context.Platforms;

    //[UseProjection] Used for Annotation-based Approach
    public IQueryable<Command> GetCommands(AppDbContext context) =>
        context.Commands;
}
