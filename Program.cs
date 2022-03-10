using CustomerMinimals.Context;
using CustomerMinimals.Models;
using CustomerMinimals.Models.Responses;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configure Services

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure

app.UseSwagger();
app.UseSwaggerUI();

// GET ALL
app.MapGet("customer/", [ProducesResponseType(200, Type = typeof(List<Customer>))] ([FromServices] AppDbContext context) =>
{
    List<Customer> customers = context.Customers.ToList();

    return Results.Ok(customers);
});

// GET DETAILS
app.MapGet("customer/{customerId}", 
[ProducesResponseType(200, Type = typeof(Customer))] 
[ProducesResponseType(404, Type = typeof(NotFoundObject))] 
([FromServices] AppDbContext context, Guid customerId) =>
{
    Customer customer = context.Customers.FirstOrDefault(x => x.Id == customerId);

    if (customer is null)
        return Results.NotFound(new NotFoundObject { Message = "Customer not found." });

    return Results.Ok(customer);
});

// CREATE
app.MapPut("customer/", [ProducesResponseType(201, Type = typeof(Customer))] ([FromServices] AppDbContext context, [FromBody] Customer customer) =>
{
    context.Customers.Add(customer);
    context.SaveChanges();

    return Results.Created($"customer/{customer.Id}", customer);
});

// UPDATE
app.MapPost("customer/", [ProducesResponseType(200, Type = typeof(Customer))] ([FromServices] AppDbContext context, [FromBody] Customer customer) =>
{
    Customer currentCustomer = context.Customers.FirstOrDefault(x => x.Id == customer.Id);

    if (currentCustomer is null)
        return Results.NotFound();

    context.Entry(currentCustomer).CurrentValues.SetValues(customer);
    context.SaveChanges();

    return Results.Redirect($"customer/{customer.Id}");
});

// DELETE
app.MapDelete("customer/{customerId}", [ProducesResponseType(200, Type = typeof(Customer))] ([FromServices] AppDbContext context, Guid customerId) => {
    Customer customer = context.Customers.FirstOrDefault(x => x.Id == customerId);

    if (customer is null)
        return Results.NotFound();
    
    context.Customers.Remove(customer);
    context.SaveChanges();

    return Results.Ok();
});


app.Run();
