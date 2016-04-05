using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.WebUi.Models
{
    public class JsonModels
    {
        public bool IsError { get; set; }
        public bool IsDefaultMessage { get; set; }
        public bool SessionTimeOut { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}