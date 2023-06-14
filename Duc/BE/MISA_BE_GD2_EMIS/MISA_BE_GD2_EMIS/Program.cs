using Aspose.Cells.Charts;
using EMIS.BL.BaseBL;
using EMIS.BL.ExcelBL;
using EMIS.BL.ExerciseBL;
using EMIS.BL.QuestionBL;
using EMIS.BL.TopicBL;
using EMIS.DL.BaseDL;
using EMIS.DL.ExcelDL;
using EMIS.DL.ExerciseDL;
using EMIS.DL.QuestionDL;
using EMIS.DL.TopicDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependence injection
builder.Services.AddScoped<IExerciseBL, ExerciseBL>();
builder.Services.AddScoped<IExerciseDL, ExerciseDL>();
builder.Services.AddScoped<IQuestionBL, QuestionBL>();
builder.Services.AddScoped<IQuestionDL, QuestionDL>();
builder.Services.AddScoped<ITopicBL, TopicBL>();
builder.Services.AddScoped<ITopicDL, TopicDL>();
builder.Services.AddScoped<IExcelBL, ExcelBL>();
builder.Services.AddScoped<IExcelDL, ExcelDL>();
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));

//add cache memory
builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//bo chan  CORS
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
