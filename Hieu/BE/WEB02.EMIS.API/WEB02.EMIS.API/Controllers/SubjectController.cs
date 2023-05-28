using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;

namespace WEB02.EMIS.API.Controllers
{
    public class SubjectController : BaseController<Subject>
    {
        public SubjectController(ISubjectBL subjectBL) : base(subjectBL)
        {
        }
    }
}
