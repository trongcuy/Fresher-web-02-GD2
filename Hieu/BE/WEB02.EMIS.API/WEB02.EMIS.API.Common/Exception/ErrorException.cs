using System.Collections;

namespace MISA.Web02.CeGov.Common
{
    /// <summary>
    /// Định nghĩa lại exception
    /// VMHieu 25/05/2023
    /// </summary>
    public class ErrorException : Exception
    {
        #region Property

        string devMsg;
        IDictionary errors;

        #endregion

        #region Method
        public ErrorException(string devmsg = null, List<string> listErrors = null)
        {
            this.devMsg = devmsg;
            errors = new Dictionary<string, List<string>>
            {
                { "Error", listErrors }
            };
        } 

        #endregion

        public override string Message => this.devMsg;
        public override IDictionary Data => this.errors;

    }
}
