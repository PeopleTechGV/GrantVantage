using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class OfferLetterAction : ActionBase
    {
        #region private data member
        private DAClient.OfferLetterRepository _OfferLetterRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public OfferLetterAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPOfferLetters>(ClientId));

            _MyClientId = ClientId;
            _OfferLetterRepository = new DAClient.OfferLetterRepository(base.ConnectionString);
            _OfferLetterRepository.CurrentUser = base.CurrentUser;
            _OfferLetterRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.OfferLetters> GetAllOfferLetters()
        {
            try
            {
                string usersDivisionList = base.GetAllDivisionsByCurrentUser;
                List<BEClient.OfferLetters> objOfferLetterList = _OfferLetterRepository.GetAllOfferLetters(usersDivisionList);
                foreach (BEClient.OfferLetters objOfferLetter in objOfferLetterList)
                {
                    base.SetRoleByDivisionList(objOfferLetter, objOfferLetter.DivisionList);
                }
                return objOfferLetterList;
                //return _OfferLetterRepository.GetAllOfferLetters();
            }
            catch
            {
                throw;
            }
        }
        public BEClient.OfferLetters GetOfferLetterById(Guid OfferLetterId)
        {
            try
            {
                return _OfferLetterRepository.GetOfferLetterById(OfferLetterId);
            }
            catch
            {
                throw;
            }

        }
        public Guid InsertOfferletter(BEClient.OfferLetters OfferLetter)
        {
            try
            {
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.OfferLetterName, OfferLetter.OfferLetterName, BEClient.Common.CommonTblVal.IsDelete, 0);
                return _OfferLetterRepository.AddOfferLetter(OfferLetter, fieldValue);
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateOfferletter(BEClient.OfferLetters OfferLetter)
        {
            try
            {
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.OfferLetterName, OfferLetter.OfferLetterName, BEClient.Common.OfferLetterTbl.OfferletterId, OfferLetter.OfferLetterId, BEClient.Common.CommonTblVal.IsDelete, 0);

                return _OfferLetterRepository.UpdateOfferLetter(OfferLetter, fieldValue);
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteOfferLeter(string OfferLetterId)
        {
            try
            {
                return _OfferLetterRepository.DeleteOfferLetter(OfferLetterId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.OfferLetters> FillOfferLettersByIds(string OfferLetterList, Guid LanguageId)
        {
            try
            {
                return _OfferLetterRepository.FillOfferLettersByIds(OfferLetterList, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.OfferLetters> GetOfferLettersByPositiontypeId(Guid PositionTypeId)
        {
            try
            {
                return _OfferLetterRepository.GetOfferLettersByPositionTypeId(PositionTypeId);
            }
            catch
            {
                throw;
            }
        }
    }
}
