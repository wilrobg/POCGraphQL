using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;
using POCGraphQL.DbContexts;
using POCGraphQL.GraphQL.Mutations;
using POCGraphQL.GraphQL.Platforms;
using POCGraphQL.GraphQL.Queries;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    // Usado para utilizar Code First Approach
    .AddPooledDbContextFactory<AppDbContext>(options =>
    {
        options.UseInMemoryDatabase("DB");
    })

    // Usado por que necesito mockear data automaticamente   #1
    .AddDbContext<AppDbContext>(options =>
    {
        options.UseInMemoryDatabase("DB");
    })
    ;

builder.Services
    .AddGraphQLServer()
    //Realizar el DI en este punto es para buscar el Annotation-base approach.

    // Si escojemos un pooled, debe considerarse que el performace lo debemos manejar totalmente de nuestro lado
    //.RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
    
    // Se utiliza para hacer DI por Annotation Approach. Dependiendo nuestra necesidad. Deberiamos definir cual seria el scope.
    //Trabaja de forma concurrente para evitar el paralelismo
    .RegisterDbContext<AppDbContext>(DbContextKind.Synchronized)
    .AddQueryType<QueryType>()
    //Required to add schema defintion and mutation conversion
    .AddMutationConventions()
    .AddMutationType<MutationType>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddFiltering()
    .AddSorting()
    //Used for Annotation-based Approach
    //.AddProjections()
    .AddErrorFilter<CustomErrorFilter>()
    .InitializeOnStartup();

var app = builder.Build();

//#1 Mock Data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.FillMockData();
}

app.MapGraphQL();

app.UseGraphQLVoyager("/ui/voyager", new VoyagerOptions 
{
    GraphQLEndPoint = "/graphql"
});

app.Run();

public class CustomErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is PlatformNameTakenException platformNameTakenException)
        {
            return error.WithMessage($"Name '{platformNameTakenException.Name}' is already taken.");
        }
        return error;
    }
}