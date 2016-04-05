using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using BEMaster = ATS.BusinessEntity.Master;


namespace ATS.DataAccess.Master
{
    public class LanguageLableRepository : Repository<BEMaster.LanguageLableList>
    {
        public List<BEMaster.LanguageLableList> GetAllLanguageLable()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllLanguageLable");
                return base.Find(command, new GetAllLanguageLableFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEMaster.LanguageLableList> GetAllLable()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllLable");
                return base.Find(command, new GetAllLableFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public String GetLabelByLanguage(String LabelName, Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetLabelByLanguage");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                Database.AddInParameter(command, "@LabelName", DbType.String, LabelName);
                BEMaster.LanguageLableList label = base.FindOne(command, new GetLabelByLanguageFactory(), false);
                if(label != null)
                return label.LableLocal;
                else
                return string.Empty;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEMaster.LanguageLableList> GetAllLableByLanguageId(Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllLableByLanguageId");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                return base.Find(command, new GetAllLableByLanguageIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class LabelListRepository : Repository<BEMaster.LabelList>
    {
        public List<BEMaster.LabelList> GetLableByLanguageId(Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetLableByLanguageId");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                return base.Find(command, new GetLableByLanguageIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    internal class GetLabelByLanguageFactory : IDomainObjectFactory<BEMaster.LanguageLableList>
    {
        public BEMaster.LanguageLableList Construct(IDataReader reader)
        {
            BEMaster.LanguageLableList objLanguageLableList = new BEMaster.LanguageLableList();
            objLanguageLableList.LableLocal = HelperMethods.GetString(reader, "LableLocal");
            return objLanguageLableList;
        }
    }

    internal class GetAllLanguageLableFactory : IDomainObjectFactory<BEMaster.LanguageLableList>
    {
        public BEMaster.LanguageLableList Construct(IDataReader reader)
        {
            BEMaster.LanguageLableList objLanguageLableList = new BEMaster.LanguageLableList();

            objLanguageLableList.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
            objLanguageLableList.LableLocal = HelperMethods.GetString(reader, "LableLocal");
            objLanguageLableList.LableName = HelperMethods.GetString(reader, "LableName");
            objLanguageLableList.LableId = HelperMethods.GetGuid(reader, "LableId");
            objLanguageLableList.LanguageLableId = HelperMethods.GetGuid(reader, "LanguageLableId");

            return objLanguageLableList;
        }
    }

    internal class GetAllLableFactory : IDomainObjectFactory<BEMaster.LanguageLableList>
    {
        public BEMaster.LanguageLableList Construct(IDataReader reader)
        {
            BEMaster.LanguageLableList objLable = new BEMaster.LanguageLableList();

            objLable.LableName = HelperMethods.GetString(reader, "LableName");
            objLable.LableLocal = HelperMethods.GetString(reader, "LableLocal");

            return objLable;
        }
    }


    internal class GetAllLableByLanguageIdFactory : IDomainObjectFactory<BEMaster.LanguageLableList>
    {
        public BEMaster.LanguageLableList Construct(IDataReader reader)
        {
            BEMaster.LanguageLableList objLanguageLableList = new BEMaster.LanguageLableList();

            objLanguageLableList.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
            objLanguageLableList.LableId = HelperMethods.GetGuid(reader, "LableId");
            objLanguageLableList.LableName = HelperMethods.GetString(reader, "LableName");
            objLanguageLableList.LableLocal = HelperMethods.GetString(reader, "LableLocal");

            return objLanguageLableList;
        }
    }

    internal class GetLableByLanguageIdFactory : IDomainObjectFactory<BEMaster.LabelList>
    {
        public BEMaster.LabelList Construct(IDataReader reader)
        {
            BEMaster.LabelList objLabelList = new BEMaster.LabelList();

            objLabelList.LanguageCulture = HelperMethods.GetString(reader, "LanguageCulture");
            objLabelList.LableName = HelperMethods.GetString(reader, "LableName");
            objLabelList.LableLocal = HelperMethods.GetString(reader, "LableLocal");

            return objLabelList;
        }
    }
}
