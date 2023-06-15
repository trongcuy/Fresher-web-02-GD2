using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.Controllers
{
    public class QuestionController : BaseController<Question>
    {
        public QuestionController(IQuestionBL questionBL) : base(questionBL)
        {
        }
    }
}
