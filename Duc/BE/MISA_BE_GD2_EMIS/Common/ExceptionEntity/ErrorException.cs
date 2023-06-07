using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIS.Common.ExceptionEntity
{
    public class ErrorException:Exception
    {
        string devMsg;
        IDictionary errors;

        public ErrorException(string devmsg = null, List<string> errors = null)
        {
            this.devMsg = devmsg;
            this.errors = new Dictionary<string, List<string>>
            {
                { "Error", errors }
            };
        }
        public override string Message => this.devMsg;
        public override IDictionary Data => this.errors;
    }
}
