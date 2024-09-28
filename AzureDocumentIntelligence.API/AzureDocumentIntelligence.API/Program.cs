using AzureDocumentIntelligence.API.Extractors;
using AzureDocumentIntelligence.API.Interfaces;
using AzureDocumentIntelligence.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IAzureDocumentIntelligenceService, AzureDocumentIntelligenceService>();
builder.Services.AddTransient<IFieldExtractor, SingleFieldsExtractor>();
builder.Services.AddTransient<IFieldExtractor, EducationExtractor>();
builder.Services.AddTransient<IFieldExtractor, WorkExperienceExtractor>();
builder.Services.AddTransient<IFieldExtractor, SkillsExtractor>();
builder.Services.AddTransient<IFieldExtractor, CertificationsExtractor>();
builder.Services.AddTransient<IFieldExtractor, ProjectsExtractor>();
builder.Services.AddTransient<IFieldExtractor, LanguagesExtractor>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
