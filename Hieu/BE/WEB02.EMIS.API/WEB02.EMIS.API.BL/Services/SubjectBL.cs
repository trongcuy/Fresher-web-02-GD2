using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.BL.Interfaces;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.BL.Services
{
    public class SubjectBL : BaseBL<Subject>, ISubjectBL
    {
        public SubjectBL(ISubjectDL subjectDL) : base(subjectDL)
        {
        }
    }
}
