using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BLClient = ATS.BusinessLogic;

namespace ATS.BusinessLogic
{
    public class ProjectAction:ActionBase
    {
        #region private data member
        private DAClient.ProjectRepository _ProjectRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public ProjectAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ProjectRepository = new DAClient.ProjectRepository(base.ConnectionString);
            _ProjectRepository.CurrentUser = base.CurrentUser;
            _ProjectRepository.CurrentClient = base.CurrentClient;

        }
        #endregion


   
    }

}
