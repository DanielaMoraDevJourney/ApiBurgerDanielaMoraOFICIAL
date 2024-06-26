﻿using Microsoft.EntityFrameworkCore;
using ApiBurgerDanielaMora.Data;
using ApiBurgerDanielaMora.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace ApiBurgerDanielaMora.Controllers;

public static class BurgerEndpoints
{
    public static void MapBurgerEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Burger").WithTags(nameof(Burger));

        group.MapGet("/", async (ApiBurgerDanielaMoraContext db) =>
        {
            return await db.Burger.ToListAsync();
        })
        .WithName("GetAllBurgers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Burger>, NotFound>> (int burgerid, ApiBurgerDanielaMoraContext db) =>
        {
            return await db.Burger.AsNoTracking()
                .FirstOrDefaultAsync(model => model.BurgerId == burgerid)
                is Burger model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBurgerById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int burgerid, Burger burger, ApiBurgerDanielaMoraContext db) =>
        {
            var affected = await db.Burger
                .Where(model => model.BurgerId == burgerid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.BurgerId, burger.BurgerId)
                    .SetProperty(m => m.Name, burger.Name)
                    .SetProperty(m => m.WithCheese, burger.WithCheese)
                    .SetProperty(m => m.Precio, burger.Precio)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBurger")
        .WithOpenApi();

        group.MapPost("/", async (Burger burger, ApiBurgerDanielaMoraContext db) =>
        {
            db.Burger.Add(burger);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Burger/{burger.BurgerId}",burger);
        })
        .WithName("CreateBurger")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int burgerid, ApiBurgerDanielaMoraContext db) =>
        {
            var affected = await db.Burger
                .Where(model => model.BurgerId == burgerid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBurger")
        .WithOpenApi();
    }
}
