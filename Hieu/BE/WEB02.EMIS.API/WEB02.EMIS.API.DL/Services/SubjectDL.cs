﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB02.EMIS.API.Common.Entity;
using WEB02.EMIS.API.DL.Interfaces;

namespace WEB02.EMIS.API.DL.Services
{
    public class SubjectDL : BaseDL<Subject>, ISubjectDL
    {
        public SubjectDL(IConfiguration configuration) : base(configuration)
        {
        }
    }
}