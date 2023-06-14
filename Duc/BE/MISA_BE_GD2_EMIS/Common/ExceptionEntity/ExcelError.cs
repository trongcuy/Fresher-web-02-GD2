using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.ExceptionEntity
{
    public class ExcelError
    {
        public int RowIndex { get; set; }
        public List<string> Errors { get; set; }
    }
}
