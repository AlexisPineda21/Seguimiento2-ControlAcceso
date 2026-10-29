using AccessControl.Api.Responses;
using AccessControl.Application;
using AccessControl.Persistence;
using AccessControl.Persistence.Seeds;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        List<ApiError> errors = context.ModelState.Where(entry => entry.Value is not null && entry.Value.Errors.Count > 0)
                                                                  .SelectMany(entry => entry.Value!.Errors.Select(error =>
                                                                      new ApiError($"Validation.{entry.Key}", error.ErrorMessage, "validation")))
                                                                  .ToList();

                        return new BadRequestObjectResult(ApiResponse<object>.Fail(errors, "La solicitud tiene datos inválidos."));
                    };
                });

builder.Services.AddOpenApi();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await DataBaseSeeder.SeedAsync(app.Services);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
