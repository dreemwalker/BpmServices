using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BpmContactsApp.Models.HttpServices
{
    public class ResponseStatus
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Exception { get; set; }
        public object PasswordChangeUrl { get; set; }
        public object RedirectUrl { get; set; }
    }
}
