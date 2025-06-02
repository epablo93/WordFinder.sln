using WebApplication1.Core;
using WebApplication1.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<IEnumerable<string>>, MatrixInputValidator>();
builder.Services.AddScoped<MatrixInputValidator>();
builder.Services.AddScoped<WordStreamValidator>();
builder.Services.AddScoped<WordFinderService>(sp =>
    new WordFinderService(
        sp.GetRequiredService<MatrixInputValidator>(),
        sp.GetRequiredService<WordStreamValidator>()
    ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
