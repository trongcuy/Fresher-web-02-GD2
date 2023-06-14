
using Microsoft.Extensions.FileProviders;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.BL.Services;
using WEB02.EMIS.API.DL.Interfaces;
using WEB02.EMIS.API.DL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IAnswerDL, AnswerDL>();
builder.Services.AddScoped<IAnswerBL, AnswerBL>();

builder.Services.AddScoped<IExerciseDL, ExerciseDL>();
builder.Services.AddScoped<IExerciseBL, ExerciseBL>();

builder.Services.AddScoped<IExerciseTopicDL, ExerciseTopicDL>();
builder.Services.AddScoped<IExerciseTopicBL, ExerciseTopicBL>();

builder.Services.AddScoped<IGradeDL, GradeDL>();
builder.Services.AddScoped<IGradeBL, GradeBL>();

builder.Services.AddScoped<ISubjectDL, SubjectDL>();
builder.Services.AddScoped<ISubjectBL, SubjectBL>();

builder.Services.AddScoped<ITopicDL, TopicDL>();
builder.Services.AddScoped<ITopicBL, TopicBL>();

builder.Services.AddScoped<IQuestionDL, QuestionDL>();
builder.Services.AddScoped<IQuestionBL, QuestionBL>();

builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/Uploads"
});

app.Run();
