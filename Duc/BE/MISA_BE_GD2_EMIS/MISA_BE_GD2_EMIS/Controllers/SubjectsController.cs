using EMIS.BL.BaseBL;
using EMIS.Common.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA_BE_GD2_EMIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : BaseController<Subject>
    {
        public SubjectsController(IBaseBL<Subject> baseBL) : base(baseBL)
        {
        }
    }
}
