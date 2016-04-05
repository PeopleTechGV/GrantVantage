using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public enum QuestionType
    {
        QT_ASK = 1,
        QT_RATE = 2,
        QT_SELF = 3
    }

    public enum ProfileRecordMode
    {
        New = 1,
        Old = 2
    }
    public enum QueryOpeation
    {
        Eq = 1,
        DNEq = 11,
        Con = 2,
        DNCon = 22,
        BeWith = 3,
        DNBeWith = 33,
        EndWith = 4,
        DNEndWith = 44,
        IsLt = 5,
        IsGt = 6,
        ConDa = 7,
        DNConDa = 77,
        ONAfter = 8,
        ONBefore = 9,
        LSTDays = 10,
        LSTMonths = 12,
        EQToOrAbove = 13

    }


    public enum MasterData
    {
        LanguageList,
        MenuList,
        DegreeType,
        SkillType,
        OfferTemplates
    }

    public enum ATSPrivilage
    {
        Application = 1,
        Division = 4,
        Interview = 5,
        JobLocation = 6,
        Offer = 7,
        PositionType = 8,
        Question = 9,
        Resume = 11,
        SecurityRole = 13,
        User = 14,
        Vacancy = 15,
        VacancyQuestion = 16,
        VacancyTemplate = 17,
        Other = 18,
        PortalContent = 19,
        DivisionPositionType = 20,
        UserDivisionPermission = 21,
        DegreeType = 24,
        SkillType = 25,
        VacancyStatus = 26,
        RndType = 27,
        VacQueType = 28,
        ApplicationBasedStatus = 30,
        LanguageBlocks = 32,
        DocumentType = 34,
        Function = 35,  //Indeed
        ExperienceLevel = 36, //Indeed
        Industry = 37, //Indeed
        ATSSecurity = 38,
        OfferLetters = 39,
        OfferTemplates = 40
        //Candidate = 2,
        //Client = 3,
        //Rating = 10,
        //ReviewPanel = 12,
        //QueCat = 29,
        //AnsOpt = 31,
        //UserPositionTypePermission = 22,
        //UserLocationPermission = 23,
    }

    public enum PageMode
    {
        Create,
        Update,
        View,
        Other

    }

    public enum ATSPermissionType
    {
        Create = 101,
        View = 102,
        Modify = 103,
        Delete = 104,
        Other = 105,
        ChangeVacancyStatus = 106,
        XMLFeed = 107,
        CreateInterview = 108,
        HireCandidate = 109
    }

    public enum ATSScope
    {
        A = 201,
        S = 202,
        C = 203,
        O = 204,
        Other = 205
    }
    public enum ATSSecurityRole
    {
        SystemAdministrator = 301,
        Candidate = 307,
        Other = 308
    }

    public enum YesNo
    {
        No = 0,
        Yes = 1
    }

    //start CRNOV14

    public class enummodel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
    public enum VacancyStatusDrp
    {
        Draft = 0,
        Open = 1,
        Close = 2,
        Archive = 3
    }


    //end CRNOV14


    public enum AppBasedStatusDrp
    {
        Declined = 0
    }

    public enum VacancyState
    {
        Active = 1,
        Inactive = 2
    }


    public enum DrpDownFields
    {
        JobType

    }
    public enum VacancyStatusCategory
    {
        Closed,
        Open
    }

    public enum PublicCodelink
    {
        Candidate = 102,
        Manager = 105
    }

    public enum UploadFileType
    {
        LocalDevice,
        MSDropBox,
        GoogleDrive
    }
    public enum LanguageBlock
    {
        Setp2Block,//Application process step 2
        Step3_4Block, //Application process step 3/4    
        NoteToCandidate //Application process step 3/4
    }

    public enum RndAttrNo
    {
        ApplicationRound = 1,
        InternalEvalRound = 2,
        InterviewRound = 3,
        OfferRound = 4,
        CandidateSurvey = 5
    }
    public enum VacancyOfferStatus
    {
        Draft = 1,
        Offer_Made = 2,
        Candidate_Declined = 3,
        Candidate_Countered = 4,
        Company_Countered = 5,
        Company_Declined = 6,
        Candidate_Accepted = 7,
        Company_Confirmed = 8,
        Retracted = 9,
        Candidate_Retracted = 10,

    }
    public enum VacancyOfferType
    {
        Draft_Offer = 1,
        Initial_Offer = 2,
        Revised_Offer = 3,
        Candidate_Counter = 4,
        New_Offer = 5
    }
    public enum PlaceMentType
    {
        Contract = 1,
        Permanent = 2,
        Temp = 3
    }
    public enum SalaryType
    {
        Salary = 1,
        Hourly = 2,
        PieceType = 3
    }
    public enum EmailTemplateCateogory
    {
        Offers = 1,
        New_Vacancy_Applications = 2,
        Notify_Assigned_Manager = 3

    }
    public enum EmailTemplateIdentifier
    {
        MakeOffer_NoteToCandidate,
        MakeOffer_ReviseOffer,
        MakeOffer_DeclineOffer,
        MakeOffer_AcceptOffer,
        MakeOffer_CounterOffer,
        MakeOffer_RetractOffer,
        Apply_Vacancy,
        Notify_Assigned_Manager,
        Candidate_Survey,
        Candidate_Survey_Completed
    }

    public enum RecentlyViewedCategory
    {
        Application,
        ApplicationBasedStatus,
        Division,
        JobLocation,
        OfferLetter,
        PositionType,
        ReviewRound,
        StatusReason,
        DegreeType,
        EmailTemplate,
        LanguageBlock,
        OfferTemplate,
        Question,
        SkillType,
        User,
        VacancyTemplate,
        FeaturedJob,
        MyVacancy,
        MyCandidate
    }

    public enum InterviewStatus
    {
        BeginInterview = 1,
        FinishLater = 2,
        InterViewComplete = 3,
        Terminate = 4,
        TerminateAndDecline = 5,
        InterviewReopened = 6

    }

    public enum DocCategoryType
    {
        Resume = 1,
        CoverLetter = 2,
        Others = 3
    }

    public enum DummyInterviewRound
    {
        ApplicationRound = 1,
        InternalEvalRound = 2,
        CandidateSurvey = 5
    }
}