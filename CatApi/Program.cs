using CatApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<TheCatApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["TheCatApi:BaseUrl"]!);
    client.DefaultRequestHeaders.Add("x-api-key", builder.Configuration["TheCatApi:ApiKey"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();

app.Run();
