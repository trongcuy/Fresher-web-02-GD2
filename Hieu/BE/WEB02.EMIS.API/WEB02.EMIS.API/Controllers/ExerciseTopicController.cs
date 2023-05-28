using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.Controllers
{
    public class ExerciseTopicController : BaseController<ExerciseTopic>
    {
        public ExerciseTopicController(IExerciseTopicBL exerciseTopicBL) : base(exerciseTopicBL)
        {
        }
    }
}
