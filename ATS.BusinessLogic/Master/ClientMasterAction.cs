using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using BEClient = ATS.BusinessEntity;
using System.IO;
using System.Xml.Linq;
using DAMaster = ATS.DataAccess.Master;
using BEMaster = ATS.BusinessEntity.Master;
using BE = ATS.BusinessEntity;

namespace ATS.BusinessLogic.Master
{
    public class ClientMasterAction : ActionBase
    {
        private Guid _MyClientId { get; set; }
        private DAMaster.ClientMasterRepository objClientMasterRepository;

        public ClientMasterAction()
        {
            base.SetMasterDatabsaeConnectionString();
            objClientMasterRepository = new DAMaster.ClientMasterRepository();
        }

        public ClientMasterAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            objClientMasterRepository = new DAMaster.ClientMasterRepository(base.ConnectionString);
        }

        public List<BEMaster.ClientMaster> GetAllClient()
        {
            try
            {
                return objClientMasterRepository.GetAllClient();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEMaster.ClientMaster GetClientDetailById(Guid clientId)
        {
            try
            {
                return objClientMasterRepository.GetClientDetailById(clientId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid Add(BEMaster.ClientMaster objClientMaster, string[] languageIds)
        {
            Guid clientId = Guid.Empty;
            try
            {
                objClientMasterRepository.BeginTransaction();
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BE.Common.ClientTbl.ClientName, objClientMaster.ClientName, BE.Common.CommonTblVal.IsDelete, 0);
                clientId = objClientMasterRepository.InsertClientDetail(objClientMaster,fieldValue);
                if (clientId == null)
                {
                    objClientMasterRepository.RollbackTransaction();
                    return Guid.Empty;
                }
                for (int i = 0; i < languageIds.Count(); i++)
                {

                    Guid clientLanguageId = objClientMasterRepository.InsertClientLanguage(clientId, new Guid(languageIds[i]));
                    if (clientLanguageId.Equals(Guid.Empty))
                    {
                        objClientMasterRepository.RollbackTransaction();
                        return Guid.Empty;
                    }

                }
                objClientMasterRepository.CommitTransaction();
            }
            catch
            {
                objClientMasterRepository.RollbackTransaction();
                throw;
            }
            bool CreateDBResult = false;
            try
            {
                base.SetMasterDatabsaeConnectionString();
                objClientMasterRepository = new DAMaster.ClientMasterRepository();
                CreateDBResult = objClientMasterRepository.CreateDB(clientId);
                if (!CreateDBResult)
                {
                    return Guid.Empty;
                }
            }
            catch (Exception)
            {
                CreateDBResult = objClientMasterRepository.DeleteAllClientDetail(clientId);
                if (CreateDBResult)
                {
                    clientId = Guid.Empty;
                }
                throw;
            }

            return clientId;

        }

        public Boolean AddTables(string dbName)
        {
            try
            {
                Boolean result = false;

                try
                {
                    objClientMasterRepository = new DAMaster.ClientMasterRepository(base.ConnectionString);
                    FileInfo file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BEClient.Common.SQLFile.DefaultFolder, BEClient.Common.SQLFile.DefaultDBScript));
                    if (file.Exists)
                    {
                        string strscript = file.OpenText().ReadToEnd();
                        string strupdatescript = strscript.Replace(BEClient.Common.SQLFile.DefaultDBName, dbName);
                        result = objClientMasterRepository.AddTables(strupdatescript);

                        if (result == true)
                        {
                            result = false;
                            try
                            {
                                FileInfo datafile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BEClient.Common.SQLFile.DefaultFolder, BEClient.Common.SQLFile.DefaultDataScript));
                                if (datafile.Exists)
                                {
                                    string strdatascript = datafile.OpenText().ReadToEnd();
                                    string strdataupdatescript = strdatascript.Replace(BEClient.Common.SQLFile.DefaultDBName, dbName);
                                    strdataupdatescript = strdataupdatescript.Replace(BEClient.Common.SQLFile.ClientId, Convert.ToString(_MyClientId));
                                    result = objClientMasterRepository.AddTables(strdataupdatescript);
                                    if (result)
                                    {
                                        DAMaster.StoredProcedureRepository _StoredProcedureRepository = new DAMaster.StoredProcedureRepository();

                                        _StoredProcedureRepository.BeginTransaction();
                                        try
                                        {

                                        #region Create Query from fixed format
                                        //Create respective queries
                                            String SolrDeleteQuery = String.Format(BEClient.Common.RoutineName.SOLR_DELETE_FORMAT, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrDeltaQuery = String.Format(BEClient.Common.RoutineName.SOLR_DELTA_FORMAT, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrSearchQuery = String.Format(BEClient.Common.RoutineName.SOLR_SEARCH_FORMAT, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);

                                            String SolrAchievementQuery = String.Format(BEClient.Common.RoutineName.SOLR_ACHIEVEMENT, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrAssociationsQuery = String.Format(BEClient.Common.RoutineName.SOLR_ASSOCIATIONS, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrAvailabilityQuery = String.Format(BEClient.Common.RoutineName.SOLR_AVAILABILITY, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);

                                            String SolrEduHistoryQuery = String.Format(BEClient.Common.RoutineName.SOLR_EDUHISTORY, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrEmpHistoryQuery = String.Format(BEClient.Common.RoutineName.SOLR_EMPHISTORY, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrExecutiveSummaryQuery = String.Format(BEClient.Common.RoutineName.SOLR_EXECUTIVESUMMARY, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrLicenceAndCertificationQuery = String.Format(BEClient.Common.RoutineName.SOLR_LICENCEANDCERTIFICATION, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrObjectiveQuery = String.Format(BEClient.Common.RoutineName.SOLR_OBJECTIVE, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrProfileQuery = String.Format(BEClient.Common.RoutineName.SOLR_PROFILE, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrProfileDeleteQuery = String.Format(BEClient.Common.RoutineName.SOLR_PROFILE_DELETE, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrProfileDeltaQuery = String.Format(BEClient.Common.RoutineName.SOLR_PROFILE_DELTA, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);

                                            String SolrPublicationHistoryQuery = String.Format(BEClient.Common.RoutineName.SOLR_PUBLICATIONHISTORY, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrReferenceQuery = String.Format(BEClient.Common.RoutineName.SOLR_REFERENCE, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrSkillsQuery = String.Format(BEClient.Common.RoutineName.SOLR_SKILLS, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrSpeakingEventHistoryQuery = String.Format(BEClient.Common.RoutineName.SOLR_SPEAKINGEVENTHISTORY, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);
                                            String SolrUserDetailQuery = String.Format(BEClient.Common.RoutineName.SOLR_USERDETAIL, BEClient.Common.RoutineName.SOLR_FIXED_TEXT, dbName);

                                        #endregion

                                        #region Append in SOLR Delete
                                            BEMaster.StoredProcedure SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_DELETE);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrDeleteQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Delta
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_DELTA);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrDeltaQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Search
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_SEARCH);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrSearchQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }

                                        #endregion

                                        #region Append in SOLR Achievement
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_ACHIEVEMENT);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrAchievementQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Associations
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_ASSOCIATIONS);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrAssociationsQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Availability
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_AVAILABILITY);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrAvailabilityQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Education History
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_EDUHISTORY);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrEduHistoryQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Employment History
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_EMPHISTORY);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrEmpHistoryQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Executive Summary
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_EXECUTIVESUMMARY);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrExecutiveSummaryQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Licence And Certification
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_LICENCEANDCERTIFICATION);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrLicenceAndCertificationQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Objective
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_OBJECTIVE);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrObjectiveQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Profile
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_PROFILE);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrProfileQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SolrProfileDelete
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_PROFILE_DELETE);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrProfileDeleteQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SolrProfileDelta
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_PROFILE_DELTA);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrProfileDeltaQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Publication History
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_PUBLICATIONHISTORY);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrPublicationHistoryQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Reference
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_REFERENCE);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrReferenceQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Skills
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_SKILLS);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrSkillsQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR Speaking Event History
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_SPEAKINGEVENTHISTORY);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrSpeakingEventHistoryQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        #region Append in SOLR User Detail
                                        SolrSp = null;
                                        SolrSp = _StoredProcedureRepository.GetRoutineDefination(BEClient.Common.RoutineName.SOLR_CON_USERDETAIL);
                                        if (SolrSp != null)
                                        {
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace(BEClient.Common.RoutineName.SOLR_FIXED_TEXT, SolrUserDetailQuery);
                                            SolrSp.ROUTINE_DEFINITION = SolrSp.ROUTINE_DEFINITION.Replace("CREATE", "ALTER");
                                            _StoredProcedureRepository.UpdateRoutineDefination(SolrSp.ROUTINE_DEFINITION);
                                        }
                                        #endregion

                                        _StoredProcedureRepository.CommitTransaction();
                                        }
                                        catch (Exception)
                                        {
                                            _StoredProcedureRepository.RollbackTransaction();
                                            throw;
                                        }
                                        
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }

                        }
                    }
                }
                catch (Exception)
                {
                    objClientMasterRepository.DeleteAllClientDetail(_MyClientId);
                    FileInfo datafile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BEClient.Common.SQLFile.DefaultFolder, BEClient.Common.SQLFile.DefaultDropDBScript));
                    if (datafile.Exists)
                    {
                        string strdatascript = datafile.OpenText().ReadToEnd();
                        string strdataupdatescript = strdatascript.Replace(BEClient.Common.SQLFile.DefaultDBName, dbName);
                        objClientMasterRepository = new DAMaster.ClientMasterRepository();
                        result = objClientMasterRepository.AddTables(strdataupdatescript);
                    }
                    throw;
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckDBNameExists(string dbName)
        {
            try
            {
                return objClientMasterRepository.CheckDBNameExists(dbName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save(BEMaster.ClientMaster objClientMaster, string[] languageIds)
        {
            try
            {
                objClientMasterRepository.BeginTransaction();
                bool updateClientResult = false;
                
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BE.Common.ClientTbl.ClientName, objClientMaster.ClientName, BE.Common.ClientTbl.ClientId, objClientMaster.ClientId, BE.Common.CommonTblVal.IsDelete, 0);

                updateClientResult = objClientMasterRepository.UpdateClientDetail(objClientMaster, fieldValue);
                if (!updateClientResult)
                {
                    objClientMasterRepository.RollbackTransaction();
                    return false;
                }

                bool updateClientLanguageResult = objClientMasterRepository.UpdateClientLanguage(objClientMaster.ClientId);
                if (!updateClientLanguageResult)
                {
                    objClientMasterRepository.RollbackTransaction();
                    return false;
                }

                for (int i = 0; i < languageIds.Count(); i++)
                {

                    Guid clientLanguageId = objClientMasterRepository.InsertClientLanguage(objClientMaster.ClientId, new Guid(languageIds[i]));
                    if (clientLanguageId.Equals(Guid.Empty))
                    {
                        objClientMasterRepository.RollbackTransaction();
                        return false;
                    }

                }
                objClientMasterRepository.CommitTransaction();
                return updateClientResult;
            }
            catch
            {
                objClientMasterRepository.RollbackTransaction();
                throw;
            }
        }
    }
}
