using Microsoft.Net.Http.Headers;
using SpeakersData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SpeakersRepository>();
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapGet("/speakers", (SpeakersRepository speakersRepository, ILogger<Program> logger) =>
{
    logger.LogInformation("Receiving request");
    var speakers = speakersRepository.AllSpeakers();
    logger.LogInformation("Sending response with {count}",speakers.Count());
    return Results.Ok(speakers);
})
.WithName("GetSpeakers");

app.MapGet("/speakers/{id}",(SpeakersRepository speakersRepository, ILogger <Program >logger, string id) =>
{
    logger.LogInformation("Receiving request");
    var speaker = speakersRepository.AllSpeakers()?.SingleOrDefault(s => s.Id == id);
    logger.LogInformation("Sending response with {speaker}", speaker);
    return speaker is null ? Results.NoContent() : Results.Ok(speaker);
})
.WithName("GetSpeakerById");



app.Run();


