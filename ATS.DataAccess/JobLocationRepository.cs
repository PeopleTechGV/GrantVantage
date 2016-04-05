using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;
namespace ATS.DataAccess
{
    public class JobLocationRepository : Repository<BEClient.JobLocation>
    {
        public JobLocationRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddJobLocation(BEClient.JobLocation jobLocation, string fieldValue)
        {
            //bool LocalTrasaction = false;
            try
            {
                //if (base.Transaction == null)
                //{
                //    base.BeginTransaction();
                //    LocalTrasaction = true;
                //}
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertJobLocation");

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, jobLocation.DivisionId);

                Database.AddInParameter(command, "@LocationManagerId", DbType.Guid, jobLocation.LocationManagerId);

                Database.AddInParameter(command, "@OnBoardingManagerId", DbType.Guid, jobLocation.OnBoardingManagerId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, jobLocation.ClientId);

                Database.AddInParameter(command, "@DefaultValue", DbType.String, jobLocation.DefaultValue);
                Database.AddInParameter(command, "@City", DbType.String, jobLocation.City);
                Database.AddInParameter(command, "@State", DbType.String, jobLocation.State);
                Database.AddInParameter(command, "@Country", DbType.String, jobLocation.Country);
                Database.AddInParameter(command, "@ZipCode", DbType.String, jobLocation.ZipCode);


                Database.AddOutParameter(command, "@JobLocationId", DbType.Guid, 16);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                string[] outParams = new string[] { "JobLocationId", "@IsError" };

                var result = base.Add(command, outParams);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Job Location already exists.");
                    }
                    Guid.TryParse(result[outParams[0]].ToString(), out reREsult);
                }

                //if (LocalTrasaction)
                //    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                //if (LocalTrasaction)
                //    this.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateJobLocation(BEClient.JobLocation jobLocation, string fieldValue)
        {
            try
            {
                Boolean reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateJobLocation");
                
                Database.AddInParameter(command, "@JobLocationId", DbType.Guid, jobLocation.JobLocationId);
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, jobLocation.DivisionId);
                Database.AddInParameter(command, "@LocationManagerId", DbType.Guid, jobLocation.LocationManagerId);
                Database.AddInParameter(command, "@OnBoardingManagerId", DbType.Guid, jobLocation.OnBoardingManagerId);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, jobLocation.ClientId);
                Database.AddInParameter(command, "@DefaultValue", DbType.String, jobLocation.DefaultValue);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, jobLocation.IsDelete);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, jobLocation.IsActive);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@City", DbType.String, jobLocation.City);
                Database.AddInParameter(command, "@State", DbType.String, jobLocation.State);
                Database.AddInParameter(command, "@Country", DbType.String, jobLocation.Country);
                Database.AddInParameter(command, "@ZipCode", DbType.String, jobLocation.ZipCode);
                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Job Location already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }

                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string recordid)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                
                Boolean reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteJobLocation");

                Database.AddInParameter(command, "@JobLocationId", DbType.String, recordid);

                var result = base.Save(command, false);

                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }

                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }
        public List<BEClient.JobLocation> GetAllJobLocationByLanguageId(string divisions, Guid languageId)
        {

            try
            {
                List<BEClient.JobLocation> reREsult = null;
                DbCommand command = Database.GetStoredProcCommand("GetAllJobLocationByLanguageId");
                if (!string.IsNullOrEmpty(divisions))
                {
                    Database.AddInParameter(command, "@Users", DbType.String, divisions);
                }
                
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                reREsult = base.Find(command, new GetAllJobLocationGetFactory());

                return reREsult;
            }
            catch
            {
                throw;
            }
        }
        public BEClient.JobLocation GetRecordByRecordId(Guid recordId)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                BEClient.JobLocation reREsult = null;
                DbCommand command = Database.GetStoredProcCommand("GetJobLocationById");

                Database.AddInParameter(command, "@JobLocationId", DbType.Guid, recordId);

                reREsult = base.FindOne(command, new GetJobLocationGetFactory());

                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }


        public List<BEClient.JobLocation> GetJobLocationByDivision(Guid UserId, Guid DivisionId, Guid LanguageId)
        {
            try
            {
                List<BEClient.JobLocation> reREsult = null;
                DbCommand command = Database.GetStoredProcCommand("GetJobLocationByDivision");

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                reREsult = base.Find(command, new GetJobLocationGetFactory());

                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.JobLocation> GetAllJobLocation(Guid LanguageId)
        {
            try
            {
                List<BEClient.JobLocation> reREsult = null;
                DbCommand command = Database.GetStoredProcCommand("GetAllJobLocation");

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                reREsult = base.Find(command, new GetAllJobLocationFactory());

                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.JobLocation> FillJobLocationByApplicationId(Guid ApplicationId, Guid LanguageId)
        {
            try
            {
                List<BEClient.JobLocation> reResult = null;
                DbCommand command = Database.GetStoredProcCommand("FillJobLocationByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                reResult = base.Find(command, new FillJobLocationFactory());
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.JobLocation> GetAllJobLocationSByDivisionId(Guid DivisionId, Guid LanguageId)
        {
            try
            {
                List<BEClient.JobLocation> reResult = null;
                DbCommand command = Database.GetStoredProcCommand("GetAllJobLocationSByDivisionId");
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                reResult = base.Find(command, new GetAllJobLocationSByDivisionIdFactory(), false);
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        //for Auto Complete
        public BEClient.JobLocation GetClientLocationsBySearch(Guid _ClientId, Guid LanguageId, Guid DivisionId, string SearchValue)
        {
            BEClient.JobLocation clientJoblocations;
            try
            {
                clientJoblocations = new BEClient.JobLocation();
                DbCommand command = Database.GetStoredProcCommand("GetJobLocationBySearch");
                if (!string.IsNullOrEmpty(SearchValue))
                {

                    Database.AddInParameter(command, "@SearchValue", DbType.String, SearchValue);
                }
                //if (!string.IsNullOrEmpty(_searchInfo.SortBy))
                //{
                //    Database.AddInParameter(command, "@SortyBy", DbType.String, _searchInfo.SortBy);


                //}
                //if (!string.IsNullOrEmpty(_searchInfo.SortDirection))
                //{
                //    Database.AddInParameter(command, "@SortDirection", DbType.String, _searchInfo.SortDirection);


                //}
                //if (_searchInfo.PageSize > 0)
                //{
                //    Database.AddInParameter(command, "@PageSize", DbType.String, _searchInfo.PageSize);


                //}
                //if (_searchInfo.PageIndex > 0)
                //{
                //    Database.AddInParameter(command, "@PageIndex", DbType.String, _searchInfo.PageIndex);


                //}

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, _ClientId);

                Database.AddInParameter(command, "@LanguageID", DbType.Guid, LanguageId);

                clientJoblocations = base.FindOne(command, new GetJobLocationGetFactory());



                return clientJoblocations;
            }
            catch (Exception)
            {
                throw;
            }
        }


        internal class GetAllJobLocationBasedOnUserAndDivisionFactory : IDomainObjectFactory<BEClient.JobLocation>
        {
            public BEClient.JobLocation Construct(IDataReader reader)
            {
                BEClient.JobLocation objEntityJobLocation = new BEClient.JobLocation();

                objEntityJobLocation.JobLocationId = HelperMethods.GetGuid(reader, "JobLocationId");

                objEntityJobLocation.LocalName = HelperMethods.GetString(reader, "LocalName");
                objEntityJobLocation.Records = new List<BEClient.JobLocation>();
                objEntityJobLocation.Records.Add(objEntityJobLocation);
                return objEntityJobLocation;
            }
        }


        internal class GetAllJobLocationGetFactory : IDomainObjectFactory<BEClient.JobLocation>
        {
            public BEClient.JobLocation Construct(IDataReader reader)
            {
                BEClient.JobLocation objEntityJobLocation = new BEClient.JobLocation();

                objEntityJobLocation.JobLocationId = HelperMethods.GetGuid(reader, "JobLocationId");
                objEntityJobLocation.DefaultValue = HelperMethods.GetString(reader, "DefaultValue");
                objEntityJobLocation.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objEntityJobLocation.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objEntityJobLocation.JobLocationId = HelperMethods.GetGuid(reader, "JobLocationId");
                objEntityJobLocation.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objEntityJobLocation.LocalName = HelperMethods.GetString(reader, "LocalName");
                objEntityJobLocation.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                string strDivList = HelperMethods.GetString(reader, "DivisionList");
                if (!string.IsNullOrEmpty(strDivList))
                    objEntityJobLocation.DivisionList = strDivList.Split(',').Select(x => Guid.Parse(x)).ToList();

                objEntityJobLocation.Records = new List<BEClient.JobLocation>();
                objEntityJobLocation.Records.Add(objEntityJobLocation);
                return objEntityJobLocation;
            }
        }

        internal class GetJobLocationGetFactory : IDomainObjectFactory<BEClient.JobLocation>
        {
            public BEClient.JobLocation Construct(IDataReader reader)
            {
                BEClient.JobLocation objEntityJobLocation = new BEClient.JobLocation();
                objEntityJobLocation.Records = new List<BEClient.JobLocation>();
                objEntityJobLocation.JobLocationId = HelperMethods.GetGuid(reader, "JobLocationId");
                objEntityJobLocation.DefaultValue = HelperMethods.GetString(reader, "DefaultValue");
                objEntityJobLocation.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objEntityJobLocation.LocalName = HelperMethods.GetString(reader, "LocalName");
                objEntityJobLocation.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objEntityJobLocation.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                objEntityJobLocation.LocationManagerId = HelperMethods.GetGuid(reader, "LocationManagerId");
                objEntityJobLocation.OnBoardingManagerId = HelperMethods.GetGuid(reader, "OnBoardingManagerId");
                objEntityJobLocation.City = HelperMethods.GetString(reader, "City");
                objEntityJobLocation.State = HelperMethods.GetString(reader, "State");
                objEntityJobLocation.Country = HelperMethods.GetString(reader, "Country");
                objEntityJobLocation.ZipCode = HelperMethods.GetString(reader, "ZipCode");

                objEntityJobLocation.Records.Add(objEntityJobLocation);

                return objEntityJobLocation;
            }
        }

        internal class GetAllJobLocationFactory : IDomainObjectFactory<BEClient.JobLocation>
        {
            public BEClient.JobLocation Construct(IDataReader reader)
            {
                BEClient.JobLocation objEntityJobLocation = new BEClient.JobLocation();
                objEntityJobLocation.Records = new List<BEClient.JobLocation>();
                objEntityJobLocation.JobLocationId = HelperMethods.GetGuid(reader, "JobLocationId");
                objEntityJobLocation.DefaultValue = HelperMethods.GetString(reader, "DefaultValue");
                objEntityJobLocation.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objEntityJobLocation.LocalName = HelperMethods.GetString(reader, "LocalName");
                objEntityJobLocation.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objEntityJobLocation.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                objEntityJobLocation.Records.Add(objEntityJobLocation);

                return objEntityJobLocation;
            }
        }

        internal class FillJobLocationFactory : IDomainObjectFactory<BEClient.JobLocation>
        {
            public BEClient.JobLocation Construct(IDataReader reader)
            {
                BEClient.JobLocation objEntityJobLocation = new BEClient.JobLocation();
                objEntityJobLocation.Records = new List<BEClient.JobLocation>();
                objEntityJobLocation.JobLocationId = HelperMethods.GetGuid(reader, "JobLocationId");
                objEntityJobLocation.LocalName = HelperMethods.GetString(reader, "JobLocationName");
                objEntityJobLocation.Records.Add(objEntityJobLocation);
                return objEntityJobLocation;
            }
        }

        internal class GetAllJobLocationSByDivisionIdFactory : IDomainObjectFactory<BEClient.JobLocation>
        {
            public BEClient.JobLocation Construct(IDataReader reader)
            {
                BEClient.JobLocation objEntityJobLocation = new BEClient.JobLocation();
                objEntityJobLocation.Records = new List<BEClient.JobLocation>();
                objEntityJobLocation.JobLocationId = HelperMethods.GetGuid(reader, "JobLocationId");
                objEntityJobLocation.LocalName = HelperMethods.GetString(reader, "LocalName");
                objEntityJobLocation.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objEntityJobLocation.LocationManagerName = HelperMethods.GetString(reader, "LocationManagerName");
                objEntityJobLocation.OnBoardingManagerName = HelperMethods.GetString(reader, "OnBoardingManagerName");
                objEntityJobLocation.LocationDivisionId = HelperMethods.GetGuid(reader, "LocationDivisionId");
                objEntityJobLocation.Records.Add(objEntityJobLocation);
                return objEntityJobLocation;
            }
        }


    }
}
