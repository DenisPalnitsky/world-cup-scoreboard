using WorldCupScoreboard.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var scoreboard = ScoreboardFactory.Create();

app.MapGet("/scoreboard", () =>
{
    return scoreboard.ListInProgress();
})
.WithName("GetScoreboard")
.WithOpenApi();



app.MapPut("/scoreboard/matches/{id}", (string id, Score score) =>
{
    var match = scoreboard.ListInProgress().Single(m => m.Id == id);
    match.SetScore(score.Home, score.Away);
});

app.Run();