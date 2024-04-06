using Microsoft.EntityFrameworkCore;
using StudentsManagement.Application.Interfaces.Services;
using StudentsManagement.Application.Services;
using StudentsManagement.Infrastructure.DataContext;
using StudentsManagement.Infrastructure.Interfaces.Services;
using StudentsManagement.Infrastructure.Interfaces.Services.Base;
using StudentsManagement.Infrastructure.Services;
using StudentsManagement.Infrastructure.Services.Base;
using StudentsManagement.Web.Core.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterMapsterConfiguration();

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
builder.Services.AddScoped<IStudentExamService, StudentExamService>();

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