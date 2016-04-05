using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BEMaster = ATS.BusinessEntity.Master;
using BE = ATS.BusinessEntity;
using BLMaster = ATS.BusinessLogic.Master;

namespace ATS.BusinessLogic.Master
{
    public class GenerateLanguageXML : ActionBase
    {
        private LanguageLableAction objLanguageLableAction; 
        
        public GenerateLanguageXML()
        {
            base.SetMasterDatabsaeConnectionString();
            objLanguageLableAction = new LanguageLableAction();
        }
        public XmlDocument GeneratelanguageXML()
        {
            try
            {
                XmlDocument XD = new XmlDocument();
                XmlNode Root = XD.AppendChild(XD.CreateElement(BE.Common.XMLResources.rootNode_resource));

                 BLMaster.LanguageAction objLanguage = new BLMaster.LanguageAction();
                List<BEMaster.Language> objLanguageList = objLanguage.GetAllLanguage();
                List<BEMaster.LanguageLableList> objLanguageLblLst = objLanguageLableAction.GetAllLanguageLable();

                foreach (BEMaster.Language objLang in objLanguageList)
                {
                    foreach (var v in objLanguageLblLst)
                    {
                        if (v.LanguageId == objLang.LanguageId)
                        {
                            XmlNode childNode_resources = Root.AppendChild(XD.CreateElement(BE.Common.XMLResources.childNode_resource));
                            XmlAttribute childAtt_Culture = childNode_resources.Attributes.Append(XD.CreateAttribute(BE.Common.XMLResources.childAttr_culture));
                            XmlAttribute childAtt_Name = childNode_resources.Attributes.Append(XD.CreateAttribute(BE.Common.XMLResources.childAttr_name));
                            XmlAttribute childAtt_Value = childNode_resources.Attributes.Append(XD.CreateAttribute(BE.Common.XMLResources.childAttr_value));

                            //Add value to node attributes
                            childAtt_Culture.InnerText = objLang.LanguageCulture;
                            childAtt_Name.InnerText = v.LableName;
                            childAtt_Value.InnerText = v.LableLocal;

                        }
                    }
                }
                return XD;
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}
