using Microsoft.EntityFrameworkCore;
using StudentsManagementApi.Application.Services;
using StudentsManagementApi.Core.Interfaces.Application;
using StudentsManagementApi.Core.Interfaces.Infrastructure;
using StudentsManagementApi.Infrastructure.Data;
using StudentsManagementApi.Infrastructure.Repositories;
using StudentsManagementApi.Infrastructure.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add infrastructure Services
builder.Services.AddDbContext<StudentsApiDbContext>(opt => opt.UseInMemoryDatabase("Database"), ServiceLifetime.Singleton);
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();

// Add Application Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IExamService, ExamService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
