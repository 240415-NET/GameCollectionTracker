using GameCollectionTracker.Data;
using GameCollectionTracker.Services;

var builder = WebApplication.CreateBuilder(args);

var myTerribleCORSPolicy = "_terribleCORSPolicy";

builder.Services.AddCors(options => {
    options.AddPolicy(name: myTerribleCORSPolicy,
                    policy =>
                    {
                        policy.AllowAnyOrigin();
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                    });
    });
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEFUserStorageRepo, EFUserStorageRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerStorageEFRepo, PlayerStorageEFRepo>();
builder.Services.AddScoped<IGameStorageEFRepo, GameStorageEFRepo>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddDbContext<GameContext>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myTerribleCORSPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
