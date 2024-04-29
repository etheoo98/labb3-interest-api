using InterestApi.Data;
using InterestApi.Models.DatabaseModels;
using InterestApi.Models.DTOs.RequestDTOs;
using InterestApi.Models.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//
// GET
//

//
// Retrieve All People
//
app.MapGet("/people", async (ApplicationDbContext context) =>
{
    var people = await context.People.Include(p => p.PersonInterests)
        .ThenInclude(pi => pi.Interest).ThenInclude(i => i.InterestLinks).ToListAsync();

    // Map database model to DTO
    var personResponses = people.Select(person => new PersonResponseDto
    {
        Id = person.Id,
        FirstName = person.FirstName,
        LastName = person.LastName,
        PhoneNumber = person.PhoneNumber,
        InterestDtos = person.PersonInterests.Select(pi => new InterestResponseDto
        {
            Id = pi.Interest.Id,
            Name = pi.Interest.Name,
            Description = pi.Interest.Description,
            LinkResponseDtos = pi.Interest.InterestLinks.Where(il => il.PersonId == person.Id)
                .Select(il => new InterestLinkResponseDto
            {
                Id = il.Id,
                InterestId = pi.Interest.Id,
                InterestName = pi.Interest.Name,
                Link = il.Link
            }).ToList()
        }).ToList()
    }).ToList();

    return Results.Ok(personResponses);
});

//
// Retrieve a Person with specific ID
//
app.MapGet("/people/{id:int}", async (int id, ApplicationDbContext context) =>
{
    var person = await context.People.FirstOrDefaultAsync(e => e.Id == id);

    if (person == null) return Results.NotFound($"Unable to find any Person with ID '{id}'.");
    
    // Map database model to DTO
    var personResponse = new PersonResponseDto
    {
        Id = person.Id,
        FirstName = person.FirstName,
        LastName = person.LastName,
        PhoneNumber = person.PhoneNumber
    };

    return Results.Ok(personResponse);
});

//
// Retrieve a Person's Interests
//
app.MapGet("/people/{id:int}/interests", async (int id, ApplicationDbContext context) =>
{
    var interests = await context.Interests.Where(i => i.PersonInterests.Any(pi => pi.PersonId == id)).ToListAsync();
    
    if (interests.Count == 0) return Results.NotFound($"Unable To Find Any Interests For Person With ID '{id}'");
    
    // Map database model to DTO
    var interestsResponse = interests.Select(interest => new InterestResponseDto
        {
            Id = interest.Id,
            Name = interest.Name,
            Description = interest.Description
        }).ToList();
    
    return Results.Ok(interestsResponse);
});

//
// Retrieve a Person's Links
//
app.MapGet("/people/{id:int}/interests/links", async (int id, ApplicationDbContext context) =>
{
    var links = await context.InterestLinks.Where(i => i.PersonId == id)
        .Include(i => i.Interest).ToListAsync();

    if (links.Count == 0) return Results.NotFound($"Unable To Find Any Links For Person With ID '{id}'");
    
    // Map database model to DTO
    var linksResponse = links.Select(il => new InterestLinkResponseDto
    {
        Id = il.Id,
        InterestId = il.Interest.Id,
        InterestName = il.Interest.Name,
        Link = il.Link,
    }).ToList();
    
    return Results.Ok(linksResponse);
});

//
// Retrieve All Interests
//
app.MapGet("/interests", async (ApplicationDbContext context) =>
{
    var interests = await context.Interests.ToListAsync();
    
    if (interests.Count == 0) return Results.NotFound("No Interests Found.");
    
    // Map database model to DTO
    var interestsResponse = interests.Select(interest => new InterestResponseDto
    {
        Id = interest.Id,
        Name = interest.Name,
        Description = interest.Description
    }).ToList();

    return Results.Ok(interestsResponse);
});

//
// Retrieve an Interest with a specific ID
//
app.MapGet("/interests/{id:int}", async (int id, ApplicationDbContext context) =>
{
    var interest = await context.Interests.FirstOrDefaultAsync(e => e.Id == id);

    if (interest == null) return Results.NotFound($"Unable To Find Any Interest With ID '{id}'");

    // Map database model to DTO
    var personResponse = new InterestResponseDto
    {
        Id = interest.Id,
        Name = interest.Name,
        Description = interest.Description
    };

    return Results.Ok(personResponse);
});

//
// POST
//

//
// Add Person To Database
//
app.MapPost("/people", async (AddPersonDto dto, ApplicationDbContext context) =>
{
    // Map DTO to database model
    var person = new Person
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        PhoneNumber = dto.PhoneNumber
    };
    
    await context.People.AddAsync(person);
    await context.SaveChangesAsync();
    
    return Results.Created($"/people/{person.Id}", person);
});

//
// Add Interest To Database
//
app.MapPost("/interests", async (AddInterestDto dto, ApplicationDbContext context) =>
{
    // Map DTO to database model
    var interest = new Interest
    {
        Name = dto.Name,
        Description = dto.Description
    };
    
    await context.Interests.AddAsync(interest);
    await context.SaveChangesAsync();
    
    return Results.Created($"/interests/{interest.Id}",  new NoContentResult());
});

//
// Add an Interest to a Person
//
app.MapPost("/people/interests/", async (AddPersonInterestDto dto, ApplicationDbContext context) =>
{
    // Check if specified person and interest exists
    var personExists = await context.People.AnyAsync(p => p.Id == dto.PersonId);
    var interestExists = await context.Interests.AnyAsync(i => i.Id == dto.InterestId);
    
    if (!personExists || !interestExists) return Results.NotFound("Person or Interest not found.");
    
    // Check if person has the interest already
    var personInterest = new PersonInterest { PersonId = dto.PersonId, InterestId = dto.InterestId };
    var isDuplicate = context.PersonInterests.Any(pi => pi.InterestId == dto.InterestId && pi.PersonId == dto.PersonId);
    
    if (isDuplicate) return Results.BadRequest("Person already has this interest.");
    
    await context.PersonInterests.AddAsync(personInterest);
    await context.SaveChangesAsync();
    
    return Results.Created($"/people/{dto.PersonId}/interests/{dto.InterestId}",  new NoContentResult());
});

//
// Add Link to Interest
//
app.MapPost("/people/interests/links", async (AddInterestLinkDto dto, ApplicationDbContext context) =>
{
    // Check if specified person and interest exists
    var personExists = await context.People.AnyAsync(p => p.Id == dto.PersonId);
    var interestExists = await context.Interests.AnyAsync(i => i.Id == dto.InterestId);
    
    if (!personExists || !interestExists) return Results.NotFound("Person or Interest not found.");
    
    // Check if person has the interest they're trying to add a link for
    var hasInterest = context.PersonInterests.Any(pi => pi.PersonId == dto.PersonId && pi.InterestId == dto.InterestId);
    
    if (!hasInterest) return Results.BadRequest("Person does not have the specified interest.");
    
    // Check if duplicate link
    var isDuplicate = context.InterestLinks.Any(il => il.Link == dto.Link);
    
    if (isDuplicate) return Results.BadRequest("Link already exists.");

    // Map DTO to database model
    var link = new InterestLink
    {
        Link = dto.Link,
        InterestId = dto.InterestId,
        PersonId = dto.PersonId
    };
    
    // Add new InterestLink
    await context.InterestLinks.AddAsync(link);
    await context.SaveChangesAsync();

    return Results.Created($"/people/{dto.PersonId}/interests/{dto.InterestId}/links/{link.Id}", new NoContentResult());
});

app.Run();