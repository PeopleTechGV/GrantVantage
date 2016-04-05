using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DA = ATS.DataAccess.Master;
using BEMaster = ATS.BusinessEntity.Master;

namespace ATS.BusinessLogic.Master
{
    public class StoredProcedureAction: ActionBase
    {
       private DA.StoredProcedureRepository objStoredProcedureRepository;

       public StoredProcedureAction()
        {
            base.SetMasterDatabsaeConnectionString();
            objStoredProcedureRepository = new DA.StoredProcedureRepository();
        }
       public BEMaster.StoredProcedure GetRoutineDefination(String RoutineName)
       {
           try
           {
               return objStoredProcedureRepository.GetRoutineDefination(RoutineName);
           }
           catch (Exception)
           {
               throw;
           }
       }
       public bool UpdateRoutineDefination(String RoutineDefination)
       {
           try
           {
               return objStoredProcedureRepository.UpdateRoutineDefination(RoutineDefination);
           }
           catch (Exception)
           {
               throw;
           }
       }
    }
}
