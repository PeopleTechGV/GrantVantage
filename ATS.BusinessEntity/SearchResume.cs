using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class RootObject
    {
        public List<Data> data { get; set; }
    }
    public class Data
    {
        public string header { get; set; }
        public string display { get; set; }
        public string icon { get; set; }
        public List<field> Fields { get; set; }
    }
    
    public class field
    {
        public string fieldName { get; set; }
        public string type { get; set; }
        public string display { get; set; }
        public string[] operators { get; set; }
        public Input input { get; set; }
    }
    public class FieldsValue
    {
        public string display { get; set; }
        public string[] operators { get; set; }
        public Input input { get; set; }
    }
    public class Input
    {
        public string name { get; set; }
        public string validation { get; set; }
    }

}
