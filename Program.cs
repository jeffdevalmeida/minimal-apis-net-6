using CustomerMinimals.Context;
using CustomerMinimals.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

// GET ALL
app.MapGet("customer/", ([FromServices] AppDbContext context) =>
{
    List<Customer> customers = context.Customers.ToList();

    return Results.Ok(customers);
});

// GET DETAILS
app.MapGet("customer/{customerId}", ([FromServices] AppDbContext context, Guid customerId) =>
{
    Customer customer = context.Customers.FirstOrDefault(x => x.Id == customerId);

    if (customer is null)
        return Results.NotFound(new { message = "Customer not found." });

    return Results.Ok(customer);
});

// CREATE
app.MapPut("customer/", ([FromServices] AppDbContext context, [FromBody] Customer customer) =>
{
    context.Customers.Add(customer);
    context.SaveChanges();

    return Results.Created($"customer/{customer.Id}", customer);
});

// UPDATE
app.MapPost("customer/", ([FromServices] AppDbContext context, [FromBody] Customer customer) =>
{
    Customer currentCustomer = context.Customers.FirstOrDefault(x => x.Id == customer.Id);

    if (currentCustomer is null)
        return Results.NotFound();

    context.Entry(currentCustomer).CurrentValues.SetValues(customer);
    context.SaveChanges();

    return Results.Redirect($"customer/{customer.Id}");
});

// DELETE
app.MapDelete("customer/{customerId}", ([FromServices] AppDbContext context, Guid customerId) => {
    Customer customer = context.Customers.FirstOrDefault(x => x.Id == customerId);

    if (customer is null)
        return Results.NotFound();
    
    context.Customers.Remove(customer);
    context.SaveChanges();

    return Results.Ok();
});


app.Run();
