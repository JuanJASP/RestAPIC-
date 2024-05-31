using Microsoft.Extensions.Options;
using Trabalhinho.MusicasRotas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "origins",
	policy =>
		{
			policy.WithOrigins("http://localhost:3000")
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
	
app.UseCors("origins");

app.UseHttpsRedirection();

//Mexendo nos endpoints

app.AddMusicas();

app.Run();

