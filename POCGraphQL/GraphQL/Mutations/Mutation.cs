using Microsoft.EntityFrameworkCore;
using POCGraphQL.DbContexts;
using POCGraphQL.GraphQL.Platforms;
using POCGraphQL.Models;

namespace POCGraphQL.GraphQL.Mutations;

public class Mutation
{
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, AppDbContext context)
    {
        var existsPlaform = await context.Platforms.AnyAsync(x => x.Name == input.Name);
        if (existsPlaform) throw new PlatformNameTakenException(input.Name);

        Platform platform = new()
        {
            Name = input.Name
        };

        context.Platforms.Add(platform);
        await context.SaveChangesAsync();

        return new AddPlatformPayload(platform);
    }
}
