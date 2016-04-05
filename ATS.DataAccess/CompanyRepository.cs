using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.DataAccess;
using System.Data.Common;
using BEClient = ATS.BusinessEntity;
using System.Data;

namespace ATS.DataAccess
{
   public class CompanyRepository:Repository<BEClient.Company>
    {
       public CompanyRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

       public Guid AddCompany(BEClient.Company objCompany)
       {
           try
           {
               Guid reREsult = Guid.Empty;
               DbCommand command = Database.GetStoredProcCommand("InsertCompany");

               command.CommandTimeout = 100;

               Database.AddInParameter(command, "@ClientId", DbType.Guid, objCompany.ClientId);
               Database.AddInParameter(command, "@SuffixTitle", DbType.String, objCompany.SuffixTitle);
               Database.AddInParameter(command, "@CompanyName", DbType.String, objCompany.CompanyName);
               Database.AddInParameter(command, "@PhoneNo", DbType.String, objCompany.PhoneNo); 
               Database.AddInParameter(command, "@Address", DbType.String, objCompany.Address);
               Database.AddInParameter(command, "@WebSite", DbType.String, objCompany.WebSite);
               Database.AddInParameter(command, "@IsActive", DbType.Boolean, true);
               Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objCompany.IsDelete);
               Database.AddOutParameter(command, "CompanyId", DbType.Guid, 32);
               var result = base.Add(command, "CompanyId");
               if (result != null)
               {
                   Guid.TryParse(result.ToString(), out reREsult);
               }
               return reREsult;
           }

           catch
           {
               throw;
           }
       }



       public bool UpdateCompany(BEClient.Company objCompany)
       {
           try
           {
               bool reREsult = false;
               DbCommand command = Database.GetStoredProcCommand("UpdateCompany");

               command.CommandTimeout = 100;

               Database.AddInParameter(command, "@CompanyId", DbType.Guid, objCompany.CompanyId);
               Database.AddInParameter(command, "@ClientId", DbType.Guid, objCompany.ClientId);

               Database.AddInParameter(command, "@SuffixTitle", DbType.String, objCompany.SuffixTitle);

               Database.AddInParameter(command, "@CompanyName", DbType.String, objCompany.CompanyName);

               Database.AddInParameter(command, "@PhoneNo", DbType.String, objCompany.PhoneNo);

               Database.AddInParameter(command, "@Address", DbType.String, objCompany.Address);
               Database.AddInParameter(command, "@WebSite", DbType.String, objCompany.WebSite);
               Database.AddInParameter(command, "@IsActive", DbType.Boolean, objCompany.IsActive); 
               Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objCompany.IsDelete);



               var result = base.Save(command);

               if (result != null)
               {
                   bool.TryParse(result.ToString(), out reREsult);
               }

               return reREsult;
           }

           catch
           {
               throw;
           }
       }

       public List<BEClient.Company> GetAllCompanyByClient(Guid ClientId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetAllCompany");
               Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
               return base.Find(command, new GetAllCompanyFactory());
           }
           catch (Exception)
           {
               throw;
           }
       }


       public BEClient.Company GetCompanyById(Guid precordId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetCompanyById");
               Database.AddInParameter(command, "@CompanyId", DbType.Guid, precordId);
               return base.FindOne(command, new GetCompanyByIdFactory());
           }
           catch (Exception)
           {
               throw;
           }
       }

       internal class GetCompanyByIdFactory : IDomainObjectFactory<BEClient.Company>
       {
           public BEClient.Company Construct(IDataReader reader)
           {
               BEClient.Company objCompany = new BEClient.Company();
               objCompany.CompanyId = HelperMethods.GetGuid(reader, "CompanyId");
               objCompany.ClientId = HelperMethods.GetGuid(reader, "ClientId");
               objCompany.CompanyName = HelperMethods.GetString(reader, "CompanyName");
               objCompany.Address = HelperMethods.GetString(reader, "Address");
               objCompany.WebSite = HelperMethods.GetString(reader, "WebSite");
               objCompany.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
               objCompany.PhoneNo = HelperMethods.GetString(reader, "PhoneNo");
               objCompany.SuffixTitle = HelperMethods.GetString(reader, "SuffixTitle");
               return objCompany;
           }
       }

       internal class GetAllCompanyFactory : IDomainObjectFactory<BEClient.Company>
       {
           public BEClient.Company Construct(IDataReader reader)
           {
               BEClient.Company objCompany = new BEClient.Company();
               objCompany.CompanyId = HelperMethods.GetGuid(reader, "CompanyId");
               objCompany.ClientId = HelperMethods.GetGuid(reader, "ClientId");
               objCompany.CompanyName = HelperMethods.GetString(reader, "CompanyName");
               objCompany.Address = HelperMethods.GetString(reader, "Address");
               objCompany.WebSite = HelperMethods.GetString(reader, "WebSite");
               objCompany.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
               objCompany.PhoneNo = HelperMethods.GetString(reader, "PhoneNo");
               objCompany.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
               objCompany.SuffixTitle = HelperMethods.GetString(reader, "SuffixTitle");
               return objCompany;
           }
       }



    }
}

