using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.Controllers
{
    public class GradeController : BaseController<Grade>
    {
        public GradeController(IGradeBL gradeBL) : base(gradeBL)
        {
        }
    }
}
