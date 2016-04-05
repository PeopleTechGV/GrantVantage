using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;


namespace ATS.DataAccess
{
   public class LanguagesRepository:Repository<BEClient.Languages>
    {
       public LanguagesRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

       public Guid AddLanguages(BEClient.Languages languages)
       {
           bool LocalTrasaction = false;
           try
           {
               if (base.Transaction == null)
               {
                   base.BeginTransaction();
                   LocalTrasaction = true;
               }
               Guid reREsult = Guid.Empty;
               DbCommand command = Database.GetStoredProcCommand("InsertLanguages");

               command.CommandTimeout = 100;

               Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, languages.ProfileId);

               Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

               Database.AddInParameter(command, "@LanguageCode", DbType.String, languages.LanguageCode);

               Database.AddInParameter(command, "@Read", DbType.Boolean, languages.Read);

               Database.AddInParameter(command, "@Write", DbType.Boolean, languages.Write);

               Database.AddInParameter(command, "@Speak", DbType.Boolean, languages.Speak);

               Database.AddInParameter(command, "@Comments", DbType.String, languages.Comments);

               Database.AddInParameter(command, "@IsDelete", DbType.Boolean, languages.IsDelete);

               Database.AddOutParameter(command, "LanguagesId", DbType.Guid, 16); ;

               var result = base.Add(command, "LanguagesId");

               if (result != null)
               {
                   Guid.TryParse(result.ToString(), out reREsult);
               }
               if (LocalTrasaction)
                   base.CommitTransaction();

               return reREsult;
           }

           catch
           {
               if (LocalTrasaction)
                   this.RollbackTransaction();
               throw;
           }
       }


       public bool UpdateLanguages(BEClient.Languages languages)
       {
           bool LocalTrasaction = false;
           try
           {
               if (base.Transaction == null)
               {
                   base.BeginTransaction();
                   LocalTrasaction = true;
               }
               bool reREsult = false;
               DbCommand command = Database.GetStoredProcCommand("UpdateLanguages");

               command.CommandTimeout = 100;

               Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, languages.ProfileId);

               Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

               Database.AddInParameter(command, "@LanguageCode", DbType.String, languages.LanguageCode);

               Database.AddInParameter(command, "@Read", DbType.Boolean, languages.Read);

               Database.AddInParameter(command, "@Write", DbType.Boolean, languages.Write);

               Database.AddInParameter(command, "@Speak", DbType.Boolean, languages.Speak);

               Database.AddInParameter(command, "@Comments", DbType.String, languages.Comments);

               Database.AddInParameter(command, "@IsDelete", DbType.Boolean, languages.IsDelete);

               Database.AddInParameter(command, "@LanguagesId", DbType.Guid, languages.LanguagesId); 

               var result = base.Save(command);

               if (result != null)
               {
                   bool.TryParse(result.ToString(), out reREsult);
               }
               if (LocalTrasaction)
                   base.CommitTransaction();

               return reREsult;
           }

           catch
           {
               if (LocalTrasaction)
                   this.RollbackTransaction();
               throw;
           }
       }


       public List<BEClient.Languages> GetLanguagesByProfileId(Guid ProfileId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetLanguagesByProfile");
               Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);



               return base.Find(command, new GetLanguagesByProfileIdFactory());
           }
           catch
           {
               throw;
           }
       }


       public bool DeleteLanguages(Guid recordid)
       {

           bool LocalTrasaction = false;

           try
           {
               if (base.Transaction == null)
               {
                   base.BeginTransaction();
                   LocalTrasaction = true;
               }
               bool reREsult = false;
               //parameterArray
               DbCommand command = Database.GetStoredProcCommand("DeleteLanguages");

               command.CommandTimeout = 100;

               Database.AddInParameter(command, "@LanguagesId", DbType.Guid, recordid);

               

               var result = base.Remove(command);
               if (result != null)
               {
                   bool.TryParse(Convert.ToString(result), out reREsult);

               }
               if (LocalTrasaction)
                   base.CommitTransaction();

               return reREsult;
           }

           catch (Exception)
           {
               if (LocalTrasaction)
                   this.RollbackTransaction();
               throw;
           }

       }

       public bool DeleteAllLanguages(Guid ProfileId)
       {

           bool LocalTrasaction = false;

           try
           {
               if (base.Transaction == null)
               {
                   base.BeginTransaction();
                   LocalTrasaction = true;
               }
               bool reREsult = false;
               //parameterArray
               DbCommand command = Database.GetStoredProcCommand("DeleteAllLanguages");

               command.CommandTimeout = 100;

               Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);
               Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);



               var result = base.Remove(command);
               if (result != null)
               {
                   bool.TryParse(Convert.ToString(result), out reREsult);

               }
               if (LocalTrasaction)
                   base.CommitTransaction();

               return reREsult;
           }

           catch (Exception)
           {
               if (LocalTrasaction)
                   this.RollbackTransaction();
               throw;
           }

       }

       internal class GetLanguagesByProfileIdFactory : IDomainObjectFactory<BEClient.Languages>
       {
           public BEClient.Languages Construct(IDataReader reader)
           {
               BEClient.Languages languages = new BEClient.Languages();
               languages.LanguagesId = HelperMethods.GetGuid(reader, "LanguagesId");
               languages.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
               languages.LanguageCode = HelperMethods.GetString(reader, "LanguageCode");
               languages.Read = HelperMethods.GetBoolean(reader, "Read");
               languages.Write = HelperMethods.GetBoolean(reader, "Write");
               languages.Speak = HelperMethods.GetBoolean(reader, "Speak");

               languages.UserId = HelperMethods.GetGuid(reader, "UserId");
               

               languages.Comments = HelperMethods.GetString(reader, "Comments");



               return languages;
           }
       }
    }
}
