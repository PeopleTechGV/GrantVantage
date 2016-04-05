using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prompt.Shared.Utility.Library;

namespace Resources.Entities
{
    
    public class ResourceEntry
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Culture { get; set; }
        public string Type { get; set; }


        public ResourceEntry()
        {
            Type = "string";
        }
    }

    public class XMLResources
    {
        public const string rootNode_resource = "res";
        public const string childNode_resource = "re";
        public const string childAttr_culture = "cu";
        public const string childAttr_name = "nm";
        public const string childAttr_value = "val";
        public static string directory_Name = Methods.GetAppSettingValue("DirectoryName");
        public static string file_Name = Methods.GetAppSettingValue("FileName");

    }
}
