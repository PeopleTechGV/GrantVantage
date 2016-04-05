using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class OfferLetterRepository : Repository<BEClient.OfferLetters>
    {
        public OfferLetterRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public List<BEClient.OfferLetters> GetAllOfferLetters(string UsersDivisionList)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllOfferLetter");
                if (!string.IsNullOrEmpty(UsersDivisionList))
                    Database.AddInParameter(command, "@UsersDivisionList", DbType.String, UsersDivisionList);
                return base.Find(command, new GetAllOfferLettersFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public Guid AddOfferLetter(BEClient.OfferLetters OfferLetter, string fieldValue)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertOfferLetter");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@OfferLetterName", DbType.String, OfferLetter.OfferLetterName);
                Database.AddInParameter(command, "@Description", DbType.String, OfferLetter.Description);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, OfferLetter.IsDelete);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, OfferLetter.PositionTypeId);
                Database.AddOutParameter(command, "OfferLetterId", DbType.Guid, 16);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);
                string[] outParams = new string[] { "OfferLetterId", "@IsError" };
                var result = base.Add(command, outParams);
                String errorCode = result[outParams[1]].ToString();
                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Award Letter already exists.");
                    }
                    Guid.TryParse(result[outParams[0]].ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }



        public bool UpdateOfferLetter(BEClient.OfferLetters OfferLetter, string fieldValue)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateOfferLetter");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "OfferLetterId", DbType.Guid, OfferLetter.OfferLetterId);
                Database.AddInParameter(command, "@OfferLetterName", DbType.String, OfferLetter.OfferLetterName);
                Database.AddInParameter(command, "@Description", DbType.String, OfferLetter.Description);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, OfferLetter.IsDelete);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, OfferLetter.PositionTypeId);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Award Letter exists.");
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

        public BEClient.OfferLetters GetOfferLetterById(Guid @OfferLetterId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetOfferLetterById");
                Database.AddInParameter(command, "@OfferLetterId", DbType.Guid, @OfferLetterId);
                return base.FindOne(command, new GetOfferLetterByIdFactory());
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
                DbCommand command = Database.GetStoredProcCommand("FillOfferLettersByIds");
                Database.AddInParameter(command, "@OfferLetterList", DbType.String, OfferLetterList);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new FillOfferLettes(), false);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteOfferLetter(string OfferletterId)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteOfferLetter");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@OfferLetterId", DbType.String, OfferletterId);
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


        public List<BEClient.OfferLetters> GetOfferLettersByPositionTypeId(Guid PositionTypeIdId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetOfferLettersByPositionTypeId");
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, PositionTypeIdId);
                return base.Find(command, new GetOfferLettersByPositionTypeIdFactory(), true);
            }
            catch
            {
                throw;
            }
        }
        internal class GetAllOfferLettersFactory : IDomainObjectFactory<BEClient.OfferLetters>
        {
            public BEClient.OfferLetters Construct(IDataReader reader)
            {
                BEClient.OfferLetters objOfferLetter = new BEClient.OfferLetters();
                objOfferLetter.OfferLetterId = HelperMethods.GetGuid(reader, "OfferLetterId");
                objOfferLetter.OfferLetterName = HelperMethods.GetString(reader, "OfferLetterName");
                objOfferLetter.Description = HelperMethods.GetString(reader, "Description");
                string DivisionList = HelperMethods.GetString(reader, "DivisionList");
                if (!string.IsNullOrEmpty(DivisionList))
                    objOfferLetter.DivisionList = DivisionList.Split(',').Select(x => Guid.Parse(x)).ToList();

                return objOfferLetter;
            }
        }
        internal class GetOfferLettersByPositionTypeIdFactory : IDomainObjectFactory<BEClient.OfferLetters>
        {
            public BEClient.OfferLetters Construct(IDataReader reader)
            {
                BEClient.OfferLetters objOfferLetter = new BEClient.OfferLetters();
                objOfferLetter.OfferLetterId = HelperMethods.GetGuid(reader, "OfferLetterId");
                objOfferLetter.OfferLetterName = HelperMethods.GetString(reader, "OfferLetterName");
                return objOfferLetter;
            }
        }
        internal class GetOfferLetterByIdFactory : IDomainObjectFactory<BEClient.OfferLetters>
        {
            public BEClient.OfferLetters Construct(IDataReader reader)
            {
                BEClient.OfferLetters objOfferLetter = new BEClient.OfferLetters();
                objOfferLetter.OfferLetterId = HelperMethods.GetGuid(reader, "OfferLetterId");
                objOfferLetter.OfferLetterName = HelperMethods.GetString(reader, "OfferLetterName");
                objOfferLetter.Description = HelperMethods.GetString(reader, "Description");
                objOfferLetter.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");

                return objOfferLetter;
            }
        }

        internal class FillOfferLettes : IDomainObjectFactory<BEClient.OfferLetters>
        {
            public BEClient.OfferLetters Construct(IDataReader reader)
            {
                BEClient.OfferLetters objOfferLetter = new BEClient.OfferLetters();
                objOfferLetter.OfferLetterId = HelperMethods.GetGuid(reader, "OfferLetterId");
                objOfferLetter.OfferLetterName = HelperMethods.GetString(reader, "OfferLetterName");
                return objOfferLetter;
            }
        }
    }
}
