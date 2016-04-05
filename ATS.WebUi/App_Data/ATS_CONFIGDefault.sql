USE [ATS_CONFIG]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 06/05/2014 15:02:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Language](
	[LanguageId] [uniqueidentifier] NOT NULL,
	[LanguageName] [varchar](30) NOT NULL,
	[LanguageCulture] [varchar](10) NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IsDelete] ON [dbo].[Language] 
(
	[IsDelete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [VersionNo] ON [dbo].[Language] 
(
	[VersionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lable]    Script Date: 06/05/2014 15:02:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lable](
	[LableId] [uniqueidentifier] NOT NULL,
	[LableName] [varchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Lable] PRIMARY KEY CLUSTERED 
(
	[LableId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LableName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LableName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IsDelete] ON [dbo].[Lable] 
(
	[IsDelete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [VersionNo] ON [dbo].[Lable] 
(
	[VersionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetRoutineDefination]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[CreateDB]    Script Date: 02/19/2014 7:15:10 PM ******/

-- =============================================
-- Author:		Viral Shah
-- Create date: 19-02-2014
-- Description:	Create default database
-- =============================================
CREATE PROCEDURE [dbo].[GetRoutineDefination]
	@RoutineName nvarchar(max)
AS

BEGIN
SELECT  ROUTINE_DEFINITION,
		ROUTINE_NAME 
    FROM INFORMATION_SCHEMA.ROUTINES 
    WHERE ROUTINE_TYPE='PROCEDURE' AND ROUTINE_NAME = @RoutineName
END
GO
/****** Object:  Table [dbo].[Client]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Client](
	[ClientId] [uniqueidentifier] NOT NULL,
	[ClientName] [nvarchar](50) NOT NULL,
	[ContactPerson] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[SubDomain] [nvarchar](50) NOT NULL,
	[ConnectionString] [varchar](256) NULL,
	[DatabaseName] [nvarchar](50) NULL,
	[CurrencySymbol] [nvarchar](10) NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IsDelete] ON [dbo].[Client] 
(
	[IsDelete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [VersionNo] ON [dbo].[Client] 
(
	[VersionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CheckDuplicateRecord]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 31-03-2014
-- Description:	Check duplicate record exists or not
-- =============================================
CREATE PROCEDURE [dbo].[CheckDuplicateRecord] 
	@FieldValue nvarchar(max),
	@TableName varchar(50),
	@Result bit output
AS
declare @Query nvarchar(max) = ''
BEGIN
 set @Query = N'
 if exists (select 1 from ' + @TableName + ' where ' + @FieldValue + ')
 begin
     select @Result1 = 1
 end
 else
 begin
	 select @Result1 = 0
 end
 '

EXEC sp_executesql @Query,
				   N'@Result1 bit output',
                   @Result1 = @Result OUTPUT
END
GO
/****** Object:  StoredProcedure [dbo].[CheckDBNameExists]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 08-03-2014
-- Description:	Check databse name exists or not in Client Table
-- =============================================
CREATE PROCEDURE [dbo].[CheckDBNameExists]
	@DatabaseName varchar(50)
AS
BEGIN
IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @DatabaseName 
OR name = @DatabaseName)))
begin
	select 1
end
else
begin
  select 0
end	
	--select count(ClientId) from Client where DatabaseName = @DatabaseName
END
GO
/****** Object:  Table [dbo].[ClientSubscription]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientSubscription](
	[ClientSubscriptionId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[SubscriptionId] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[CurrentUserCount] [int] NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_ClientSubscription] PRIMARY KEY CLUSTERED 
(
	[ClientSubscriptionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscription]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscription](
	[SubscriptionId] [uniqueidentifier] NOT NULL,
	[UserLimit] [nvarchar](50) NOT NULL,
	[Price] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Subscription] PRIMARY KEY CLUSTERED 
(
	[SubscriptionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SolrUserDetail]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrUserDetail]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
[ProfileId] uniqueidentifier,
[ClientId] uniqueidentifier,
UserId uniqueidentifier

,FirstName nvarchar(MAX),
MiddleName nvarchar(MAX),
LastName nvarchar(MAX),
Affix nvarchar(MAX),
Address nvarchar(MAX),
Nationality nvarchar(MAX),
City nvarchar(MAX),
State nvarchar(MAX),
Zip nvarchar(MAX),
CountryCode nvarchar(MAX),
BusinessPhoneNo nvarchar(MAX),
HomePhone nvarchar(MAX),
Fax nvarchar(MAX),
Pager nvarchar(MAX),
WorkEmail nvarchar(MAX),
MobilePhone nvarchar(MAX)
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrSpeakingEventHistory]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrSpeakingEventHistory]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[SEHTitle] nvarchar(MAX),
	  [SEHEventName] nvarchar(MAX),
	  [SEHEventType] nvarchar(MAX),
	  [SEHLocation] nvarchar(MAX),
	  [SEHLink] nvarchar(MAX)
	  
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrSkills]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrSkills]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[SKSkillAndQualification] nvarchar(MAX),
	  [SKDescription] nvarchar(MAX),
	  [SKSkillType] nvarchar(MAX)
	  
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrReference]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrReference]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[RFReferenceName] nvarchar(MAX),
	  [RFReferenceEmail] nvarchar(MAX),
	  [RFReferencePhone] nvarchar(MAX)
	  
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrPublicationHistory]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrPublicationHistory]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[PHTitle] nvarchar(MAX),
	  [PHType] nvarchar(MAX),
	  [PHName] nvarchar(MAX)
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrProfile]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrProfile]

AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
[ProfileId] uniqueidentifier
)

--DBQUERY--




select * from @tbl  


END
GO
/****** Object:  StoredProcedure [dbo].[SolrObjective]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrObjective]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[OBObjectiveDetails] nvarchar(MAX)
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrLicenceAndCertifications]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrLicenceAndCertifications]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[LCName] nvarchar(MAX),
	  [LCIssuingAuthority] nvarchar(MAX),
      [LCDescription] nvarchar(MAX)
      
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrExecutiveSummary]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrExecutiveSummary]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[ESExecutiveSummaryDetails] nvarchar(MAX)
      
      
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrEmploymentHistory]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrEmploymentHistory]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[EMHCompanyName] nvarchar(MAX)
      ,[EMHMayWeContact] nvarchar(MAX)
      ,[EMHSupervisorName] nvarchar(MAX)
      ,[EMHAddress] nvarchar(MAX)
      ,[EMHCity] nvarchar(MAX)
      ,[EMHZip] nvarchar(MAX)
      ,[EMHExperience] nvarchar(MAX)
      ,[EMHDutiesAndResponsibilities] nvarchar(MAX)
      ,EMHStartingPay nvarchar(MAX)
      ,EMHEndingPay nvarchar(MAX)
      ,EMHJobCategory nvarchar(MAX)
      ,EMHMostRecentPosition nvarchar(MAX)
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrEducationHistory]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrEducationHistory]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[EHInstitutionName] nvarchar(MAX)
      ,[EHMeasureSystem] nvarchar(MAX)
      ,[EHDegreeType] nvarchar(MAX)
      ,[EHMajorSubject] nvarchar(MAX)
      ,[EHCity] nvarchar(MAX)
      ,[EHState] nvarchar(MAX)
      ,[EHCountry] nvarchar(MAX)
      ,[EHMeasureValue] nvarchar(MAX)
     
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrAvailability]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrAvailability]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   
	  [ProfileId] uniqueidentifier
	  ,[AVWillingToWorkOverTime] nvarchar(MAX)
      ,[AVRelativesWorkingInCompany] nvarchar(MAX)
      ,[AVRelativesIfYes] nvarchar(MAX)
      ,[AVSubmittedApplicationBefore] nvarchar(MAX)
      ,[AVEligibleToWorkInUS] nvarchar(MAX)
      ,[AVEmploymentPreference] nvarchar(MAX)
      ,[AVTargetIncome] nvarchar(MAX)
      ,[AVDateAvailability] datetime
)

--DBQUERY--


select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrAssociations]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrAssociations]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	  [ProfileId] uniqueidentifier
	  ,[ASName] nvarchar(MAX)
      ,[ASAssociationType] nvarchar(MAX)
      ,[ASLink] nvarchar(MAX)
      ,[ASRole] nvarchar(MAX)
      
      
)

--DBQUERY--

select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  StoredProcedure [dbo].[SolrAchievement]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[SolrAchievement]
@ProfileId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [ProfileId] uniqueidentifier
	  ,[ACDescription] nvarchar(MAX)
      ,[ACIssuingAuthority] nvarchar(MAX)
      
      
)

--DBQUERY--




select * from @tbl  where ProfileId =@ProfileId


END
GO
/****** Object:  Table [dbo].[LanguageLable]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LanguageLable](
	[LanguageLableId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[LableId] [uniqueidentifier] NOT NULL,
	[LableLocal] [nvarchar](150) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_LanguageLable] PRIMARY KEY CLUSTERED 
(
	[LanguageLableId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Fk_LanguageId] ON [dbo].[LanguageLable] 
(
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IsDelete] ON [dbo].[LanguageLable] 
(
	[IsDelete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [LableCode] ON [dbo].[LanguageLable] 
(
	[LableId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [VersionNo] ON [dbo].[LanguageLable] 
(
	[VersionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[UpdateSubscriptionById]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 14-02-2014
-- Description:	Update subscription by subscriptionId
-- =============================================
CREATE PROCEDURE [dbo].[UpdateSubscriptionById]
	@SubscriptionId uniqueidentifier,
	@UserLimit nvarchar(50),
	@Price int,
	@UpdatedBy uniqueidentifier = null,
	@UpdatedOn	datetime = null,
	@IsDelete bit = 0,
	@FieldValue nvarchar(max),
	@IsError int OUTPUT
AS
Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'Subscription'
BEGIN
  Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output
	
	IF @Result = 0
	begin
		update Subscription
		set UserLimit = @UserLimit,Price = @Price,UpdatedBy = @UpdatedBy,UpdatedOn = @UpdatedOn,IsDelete = @IsDelete
		where SubscriptionId = @SubscriptionId
	end
	else
	begin
		Set @IsError = 101 -- Subscription already exists
	end
END
GO
/****** Object:  StoredProcedure [dbo].[InsertClientDetail]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 18-02-2014
-- Description:	Add client in client table and client subscription table
-- =============================================
CREATE PROCEDURE [dbo].[InsertClientDetail] 
	@ClientId uniqueidentifier output,
	@ClientName nvarchar(50),
	@ContactPerson nvarchar(50),
	@ContactNo nvarchar(50),
	@SubDomain nvarchar(50),
	@ConnectionString varchar(256),
	@DatabaseName nvarchar(50),
	@CreatedBy uniqueidentifier = null,
	@CreatedOn datetime = null,
	@SubscriptionId uniqueidentifier,
	@StartDate datetime,
	@EndDate datetime,
	@IsDelete bit,
	@FieldValue nvarchar(max),
	@IsError int OUTPUT,
	@CurrencySymbol nvarchar(10)
AS
Declare @CurrentUserCount int = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'Client'
Set @IsError = 0
BEGIN
Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
	set @ClientId = NEWID();
	insert into Client 
		(ClientId,ClientName,ContactPerson,ContactNo,SubDomain,ConnectionString,DatabaseName,IsDelete,VersionNo,CreatedBy,CreatedOn,CurrencySymbol)
	values(@ClientId,@ClientName,@ContactPerson,@ContactNo,@SubDomain,@ConnectionString,@DatabaseName,@IsDelete,			default,@CreatedBy,@CreatedOn,@CurrencySymbol)

	insert into ClientSubscription
		(ClientSubscriptionId,ClientId,SubscriptionId,StartDate,EndDate,CurrentUserCount,IsDelete,CreatedBy,				CreatedOn)
	values(NEWID(),@ClientId,@SubscriptionId,@StartDate,@EndDate,@CurrentUserCount,@IsDelete,@CreatedBy,
			@CreatedOn)
END
else
	begin
		Set @IsError = 101 -- Client already exists
	end
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUserName]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah	
-- Create date: 26-02-2014
-- Description:	Get user detail by username
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByUserName]
	--@UserName nvarchar(100) = null,
	@IsDelete bit
AS
BEGIN
	select UserId,UserName,FirstName,LastName
	from [User]
	where /*UserName = @UserName and */ IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetSubscriptionById]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 14-02-2014
-- Description:	Get Subscription by subscriptionId
-- =============================================
CREATE PROCEDURE [dbo].[GetSubscriptionById]
	@SubscriptionId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select SubscriptionId,UserLimit,Price
	from Subscription 
	where SubscriptionId = @SubscriptionId and IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[InsertLanguage]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 08-02-2014
-- Description:	Insert Language in language Table
-- =============================================
CREATE PROCEDURE [dbo].[InsertLanguage]
	@LanguageId uniqueidentifier OUT,
	@LanguageName varchar(30),
	@Culture varchar(10),
	@CreatedBy uniqueidentifier = null,
	@CreatedOn datetime = null,
	@FieldValue nvarchar(max),
	@IsError int OUTPUT
AS
declare @IsDelete bit = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'Language'
Set @IsError = 0
BEGIN

Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
	SET @LanguageId = newid();

	insert into Language
	(LanguageId,LanguageName,LanguageCulture,IsDelete,VersionNo,CreatedBy,CreatedOn)
	values(@LanguageId,@LanguageName,@Culture,@IsDelete,default,@CreatedBy,@CreatedOn)
END
ELSE
BEGIN
  set @IsError = 101 -- Language already exists
END
END
GO
/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah	
-- Create date: 26-02-2014
-- Description:	Get user detail by username
-- =============================================
CREATE PROCEDURE [dbo].[ValidateUser]
	@UserName nvarchar(100),
	@Password nvarchar(100),
	@IsDelete bit
AS
BEGIN
	select UserId,UserName,FirstName,LastName
	from [User]
	where UserName = @UserName and IsDelete = @IsDelete and [Password] = @Password
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateClientDetail]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 19-02-2014
-- Description:	Update Client Detail in Client and Client Subscription table. 
-- =============================================
CREATE PROCEDURE [dbo].[UpdateClientDetail]
	@ClientId uniqueidentifier,
	@ClientName nvarchar(50),
	@ContactPerson nvarchar(50),
	@ContactNo nvarchar(50),
	@SubDomain nvarchar(50),
	@ConnectionString varchar(256),
	@DatabaseName nvarchar(50),
	@UpdatedBy uniqueidentifier = null,
	@UpdatedOn	datetime = null,
	@SubscriptionId uniqueidentifier,
	@StartDate datetime,
	@EndDate datetime,
	@IsDelete bit,
	@FieldValue nvarchar(max),
	@IsError int OUTPUT,
	@CurrencySymbol nvarchar(10)
AS
Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'Client'
BEGIN

Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output
IF @Result = 0
	begin
Update Client
set ClientName = @ClientName,ContactPerson = @ContactPerson,ContactNo = @ContactNo,SubDomain = @SubDomain,ConnectionString = @ConnectionString,
DatabaseName = @DatabaseName,IsDelete = @IsDelete,UpdatedBy = @UpdatedBy,UpdatedOn = @UpdatedOn,CurrencySymbol = @CurrencySymbol
where ClientId = @ClientId

Update ClientSubscription
set SubscriptionId = @SubscriptionId,StartDate = @StartDate,EndDate = @EndDate,IsDelete = @IsDelete,UpdatedBy = @UpdatedBy,UpdatedOn = @UpdatedOn
where ClientId = @ClientId
END
else
begin
Set @IsError = 101 -- Client already exists
end

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateLanguageById]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 08-02-2014
-- Description:	Update language in language table
-- =============================================
CREATE PROCEDURE [dbo].[UpdateLanguageById]
	@LanguageId uniqueidentifier,
	@LanguageName varchar(30),
	@Culture varchar(10),
	@UpdatedBy uniqueidentifier = null,
	@UpdatedOn datetime = null,
	@IsDelete bit = 0,
	@FieldValue nvarchar(max),
	@IsError int OUTPUT
AS
Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'Language'
BEGIN

Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
	update Language
	set LanguageName = @LanguageName,LanguageCulture = @Culture,UpdatedBy = @UpdatedBy,UpdatedOn = @UpdatedOn,		IsDelete = @IsDelete
	where LanguageId = @LanguageId
END
else
	begin
		Set @IsError = 101 -- Language already exists
	end
END
GO
/****** Object:  Table [dbo].[ClientLanguage]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientLanguage](
	[ClientLanguageId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[IsDefault] [bit] NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_ClientLanguage] PRIMARY KEY CLUSTERED 
(
	[ClientLanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Fk_ClientId] ON [dbo].[ClientLanguage] 
(
	[ClientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [Fk_LanguageId] ON [dbo].[ClientLanguage] 
(
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IsDelete] ON [dbo].[ClientLanguage] 
(
	[IsDelete] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [VersionNo] ON [dbo].[ClientLanguage] 
(
	[VersionNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[ATSSolrSearch]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[ATSSolrSearch]
@VacancyId uniqueidentifier = null
AS
BEGIN
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [VacancyId] Uniqueidentifier primary key
	  ,[JobTitle] nvarchar(100)
      ,[ClientId] Uniqueidentifier
      ,[PostedOn] datetime
      ,[PositionID] nvarchar(100)
      ,[VacancystatusText] nvarchar(100)
      ,[JobTypeText] nvarchar(100)
      ,[EmploymentTypeText] nvarchar(100)
      ,[VacancyStateText] nvarchar(100)
      ,[DivisionText] nvarchar(100)
      ,[PositionTypeText] nvarchar(100)
	   ,[Location] nvarchar(100)
      ,[TotalPositions] int
      ,[ShowOnWeb] bit
      ,[FeaturedOnWeb] bit
      ,[JobDescription] nvarchar(MAX)
      ,[ShowOnWebJobDescription] bit
      ,[DutiesAndResponsibilities] nvarchar(MAX)
      ,[ShowOnWebDuties] bit
      ,[SkillsAndQualification] nvarchar(MAX)
      ,[ShowOnWebSkills] bit
      ,[Benefits] nvarchar(MAX)
      ,[ShowOnWebBenefits] bit
      ,[SalaryMin] decimal
      ,[SalaryMax] decimal
      ,[ShowOnWebSal] bit
	  ,[languageid] Uniqueidentifier
	  ,[CreatedBy] Uniqueidentifier
)
--DBQUERY--
 


if @VacancyId IS NULL
BEGIN
select * from @tbl  
END
ELSE
BEGIN
select * from @tbl  where vacancyId =@VacancyId 
END

END
GO
/****** Object:  StoredProcedure [dbo].[ATSSolrDelta]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[ATSSolrDelta]-- '01-01-2014'
@UpdatedOn DateTime=null
AS
BEGIN
declare @tbl TABLE 
(      
	   [VacancyId] Uniqueidentifier primary key
	  
)

--DBQUERY--

 


select [VacancyId] from @tbl  

END
GO
/****** Object:  StoredProcedure [dbo].[ATSSolrDelete]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Alter date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE .[dbo].[ATSSolrDelete] --'01-01-2014'
@UpdatedOn DateTime =null
AS
BEGIN
declare @tbl TABLE 
(      
	   [VacancyId] Uniqueidentifier primary key
	 
)


--DBQUERY--


select [VacancyId] from @tbl 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEducationHistoryByProfileIdAndUserId]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteEducationHistoryByProfileIdAndUserId]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier
As
Begin

Delete from dbo.EducationHistory
Where ProfileId = @ProfileId and UserId = @UserId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllProjectByProfileAndUser]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllProjectByProfileAndUser]

@ProfileId uniqueidentifier,
@UserId uniqueidentifier
AS
BEGIN

Delete from dbo.Project 
WHERE [ProfileId] = @ProfileId and [UserId] = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllProjectByProfileAndContact]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllProjectByProfileAndContact]

@ProfileId uniqueidentifier,
@UserId uniqueidentifier



AS
BEGIN

Delete from dbo.Project 
WHERE [ProfileId] = @ProfileId and [UserId] = @UserId

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllClient]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 15-02-2014
-- Description:	Get all Client
-- =============================================
CREATE PROCEDURE [dbo].[GetAllClient]
	@IsDelete bit 
AS
BEGIN
	select Client.ClientId,Client.ClientName,CS.StartDate,cs.EndDate,Subscription.UserLimit,Client.CurrencySymbol
	from Client
	inner join ClientSubscription CS on CS.ClientId = Client.ClientId and CS.IsDelete = @IsDelete and Client.IsDelete = @IsDelete
	inner join Subscription on Subscription.SubscriptionId = CS.SubscriptionId
END
GO
/****** Object:  StoredProcedure [dbo].[CreateDB]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[CreateDB]    Script Date: 02/19/2014 7:15:10 PM ******/

-- =============================================
-- Author:		Viral Shah
-- Create date: 19-02-2014
-- Description:	Create default database
-- =============================================
CREATE PROCEDURE [dbo].[CreateDB]
	@ClientId uniqueIdentifier
AS
declare @IsDelete bit = 0
BEGIN
	Declare @DatabaseName nvarchar(100)
Declare @CreateDatabase nvarchar(max)

Select @DatabaseName = DatabaseName From Client Where ClientId = @ClientId and IsDelete = @IsDelete

SET @CreateDatabase = '
USE [master]
CREATE DATABASE ['+@DatabaseName+'] '

EXEC sp_executesql @CreateDatabase


END
GO
/****** Object:  StoredProcedure [dbo].[GetAllLanguage]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 03-02-2014
-- Description:	Get All language from Language table 
-- =============================================
CREATE PROCEDURE [dbo].[GetAllLanguage]
	@IsDelete bit
AS
BEGIN
	select LanguageId,LanguageName,LanguageCulture 
	from [Language] 
	where  IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSubscription]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 14-02-2014
-- Description:	Get all subscription 
-- =============================================
CREATE PROCEDURE [dbo].[GetAllSubscription]
	@IsDelete bit
AS
BEGIN
	select SubscriptionId,UserLimit,Price 
	from Subscription
	where IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetClientDetailById]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 19-02-2014
-- Description:	Get Client Detail from client,ClientSubscription tables for edit client detail
-- =============================================
CREATE PROCEDURE [dbo].[GetClientDetailById] 
	@ClientId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
select Client.ClientId,Client.ClientName,Client.ContactPerson,Client.ContactNo,Client.SubDomain,
Client.ConnectionString,Client.DatabaseName,CS.SubscriptionId,CS.StartDate,CS.EndDate,Client.CurrencySymbol
from Client
inner join ClientSubscription CS on  CS.ClientId = Client.ClientId 
					and CS.IsDelete = @IsDelete 
					and Client.IsDelete = @IsDelete
					and Client.ClientId = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[GetLanguageById]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 08-02-2014
-- Description:	Get Language by Language Id
-- =============================================
CREATE PROCEDURE [dbo].[GetLanguageById]
	@LanguageId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select LanguageId,LanguageName,LanguageCulture
	from Language 
	where LanguageId = @LanguageId and IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[InsertSubscription]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 14-02-2014
-- Description:	Insert subscription in subscription table
-- =============================================
CREATE PROCEDURE [dbo].[InsertSubscription]
	@SubscriptionId uniqueidentifier OUT,
	@UserLimit nvarchar(50),
	@Price int,
	@CreatedBy uniqueidentifier = null,
	@CreatedOn	datetime = null,
	@FieldValue nvarchar(max),
	@IsError int OUTPUT
AS

declare @IsDelete bit = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'Subscription'
Set @IsError = 0
BEGIN
	
    
    Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output
	
	IF @Result = 0
	begin
	SET @SubscriptionId = newid();
		insert into Subscription
			(SubscriptionId,UserLimit,Price,IsDelete,VersionNo,CreatedBy,CreatedOn)
		values(@SubscriptionId,@UserLimit,@Price,@IsDelete,default,@CreatedBy,@CreatedOn)
	end
	else
	begin
		Set @IsError = 101 -- Subscription already exists
	end
End
GO
/****** Object:  StoredProcedure [dbo].[InsertLanguageLabel]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 08-02-2014
-- Description:	Insert local language in languagelable table
-- =============================================
CREATE PROCEDURE [dbo].[InsertLanguageLabel]
	@LanguageLabelId uniqueidentifier out,
	@LanguageId uniqueidentifier,
	@LabelName varchar(50),
	@LabelLocal nvarchar(100),
	@CreatedBy uniqueidentifier = null,
	@CreatedOn datetime = null
AS
declare @LabelId uniqueidentifier
declare @IsDelete bit = 0
BEGIN
	SET @LanguageLabelId = newid();
	select @LabelId = LableId from Lable where LableName = @LabelName

	insert into LanguageLable
	(LanguageLableId,LanguageId,LableId,LableLocal,IsDelete,VersionNo,CreatedBy,CreatedOn)
	values (@LanguageLabelId,@LanguageId,@LabelId,@LabelLocal,@IsDelete,default,@CreatedBy,@CreatedOn)

END
GO
/****** Object:  StoredProcedure [dbo].[GetLanguageByClientId]    Script Date: 06/05/2014 15:02:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get Active language by client id 
-- =============================================
CREATE PROCEDURE [dbo].[GetLanguageByClientId]
	@ClientId uniqueidentifier=null,
	@IsDelete bit
AS
BEGIN
	select 
      _Language.LanguageId
      ,_ClientLanguage.IsDefault
	  ,LanguageName
	  ,LanguageCulture
	from ClientLanguage _ClientLanguage 
	inner join [Language] _Language on 
	_Language.LanguageId = _ClientLanguage.LanguageId 
	where _ClientLanguage.ClientId = @ClientId and _ClientLanguage.IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetLableByLanguageId]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload by languageid
-- =============================================
Create PROCEDURE [dbo].[GetLableByLanguageId]
	@LanguageId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select Lable.LableName,LL.LableLocal,Language.LanguageCulture
	from LanguageLable LL 
	inner join Lable on LL.LableId = Lable.LableId 
			and LL.IsDelete = @IsDelete 
			and Lable.IsDelete = @IsDelete
	inner join Language on Language.LanguageId = LL.LanguageId 
			and Language.IsDelete = @IsDelete
			and LL.LanguageId = @LanguageId

END
GO
/****** Object:  StoredProcedure [dbo].[GetLabelByLanguage]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get Active language by client id 
-- =============================================
CREATE PROCEDURE [dbo].[GetLabelByLanguage]
	@LanguageId uniqueidentifier,
	@LabelName nvarchar(256)
AS
BEGIN
	select _LabelLanguage.LableLocal from [Lable] _Label 
  inner join LanguageLable _LabelLanguage     on _LabelLanguage.LableId = _Label.LableId
  AND _LabelLanguage.LanguageId = @LanguageId    
  where _Label.LableName=@LabelName 
END
GO
/****** Object:  StoredProcedure [dbo].[GetClientByName]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*	ERROR CODES	
   
  
  
  -101	- NotAuthenticated
  
  
  */
  
CREATE Procedure [dbo].[GetClientByName]

  (         
	
	@ClientName nvarchar(100),
	@IsDelete bit
   )
   AS      
   declare @IsDefault bit = 1
   /****** Script for SelectTopNRows command from SSMS  ******/
SELECT [Client].[ClientId]
      ,[ClientName]
      ,[ContactPerson]
      ,[ContactNo]
      ,[SubDomain]
      ,[ConnectionString]
      ,[DatabaseName]
      ,[Client].[IsDelete]
      ,[Client].[VersionNo]
      ,[Client].[CreatedBy]
      ,[Client].[CreatedOn]
      ,[Client].[UpdatedBy]
      ,[Client].[UpdatedOn]
	  ,CL.LanguageId
	  ,Client.CurrencySymbol
  FROM [Client]
  inner join ClientLanguage CL on CL.ClientId = Client.ClientId
			and [Client].IsDelete = @IsDelete
   AND [Client].ClientName = @ClientName and CL.IsDefault = @IsDefault
GO
/****** Object:  StoredProcedure [dbo].[GetClientById]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*	ERROR CODES	
   
  
  
  -101	- NotAuthenticated
  
  
  */
  
CREATE Procedure [dbo].[GetClientById]-- '4021C3D8-FB86-4D7D-9B98-B97934429C0B','samadmin@admin.com','YyK/wC0RtLfS73LgsysXuA==','TCS',0

  (         
	
	@ClientId uniqueidentifier=null,
	@IsDelete bit
   )
   AS      
   declare @IsDefault bit = 1
   /****** Script for SelectTopNRows command from SSMS  ******/
SELECT [Client].[ClientId]
      ,[ClientName]
      ,[ContactPerson]
      ,[ContactNo]
      ,[SubDomain]
      ,[ConnectionString]
      ,[DatabaseName]
      ,[Client].[IsDelete]
      ,[Client].[VersionNo]
      ,[Client].[CreatedBy]
      ,[Client].[CreatedOn]
      ,[Client].[UpdatedBy]
      ,[Client].[UpdatedOn]
	  ,CL.LanguageId
  FROM [Client]
  inner join ClientLanguage CL on CL.ClientId = Client.ClientId
			and [Client].IsDelete = @IsDelete AND [Client].ClientId=@ClientId and CL.IsDefault = @IsDefault
GO
/****** Object:  StoredProcedure [dbo].[GetAllLanguageLable]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 03-02-2014
-- Description:	Get all Language Lable to generate XML file
-- =============================================
CREATE PROCEDURE [dbo].[GetAllLanguageLable]
	@IsDelete bit
AS

BEGIN
	select LL.LanguageLableId,LL.LanguageId,LL.LableId,Lable.LableName,LL.LableLocal
	from LanguageLable LL
	inner join Lable on Lable.LableId = LL.LableId and LL.IsDelete = @IsDelete and Lable.IsDelete = @IsDelete
	order by LanguageId
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllLableByLanguageId]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload by languageid
-- =============================================
CREATE PROCEDURE [dbo].[GetAllLableByLanguageId]
	@LanguageId uniqueidentifier,
	@IsDelete bit
AS
declare @LanguageName varchar(10) = 'english' 
BEGIN
declare @temptable table(id int identity(1,1),lable nvarchar(max))

	select Lable.LableName,LL.LableLocal,LL.LanguageId,LL.LableId  
	from LanguageLable LL 
	inner join Lable on LL.LableId = Lable.LableId 
			and LL.IsDelete = @IsDelete 
			and Lable.IsDelete = @IsDelete
	inner join Language on Language.LanguageId = LL.LanguageId 
			and Language.IsDelete = @IsDelete
			and (LOWER(Language.LanguageName) = @LanguageName 
					OR
                 LL.LanguageId = @LanguageId) 

END
GO
/****** Object:  StoredProcedure [dbo].[GetAllLable]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 06-02-2014
-- Description:	Get all lable for export lable to client to upload
-- =============================================
CREATE PROCEDURE [dbo].[GetAllLable]
	@IsDelete bit
AS
declare @LanguageName varchar(10) = 'english' 
BEGIN
	select Lable.LableName,LL.LableLocal
	from LanguageLable LL 
	inner join Lable on LL.LableId = Lable.LableId 
			and LL.IsDelete = @IsDelete 
			and Lable.IsDelete = @IsDelete
	inner join Language on Language.LanguageId = LL.LanguageId 
			and Language.IsDelete = @IsDelete
			and LOWER(Language.LanguageName) = @LanguageName
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllClientDetail]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 19-02-2014
-- Description:	Delete all data of client
-- =============================================
CREATE PROCEDURE [dbo].[DeleteAllClientDetail]
	@ClientId uniqueidentifier
AS
BEGIN
	delete from ClientLanguage where ClientId = @ClientId
	delete from ClientSubscription where ClientId = @ClientId
	delete from Client where ClientId = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateClientLanguage]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 19-02-2014
-- Description: Update Client Language.InActive languages by ClientId
-- =============================================
CREATE PROCEDURE [dbo].[UpdateClientLanguage]
	@ClientId uniqueidentifier,
	@UpdatedBy uniqueidentifier = null,
	@UpdatedOn	datetime = null,
	@IsDelete bit
AS
declare @LanguageName varchar(10) = 'english' 
declare @DefaultLanguageId uniqueidentifier
BEGIN
select @DefaultLanguageId = LanguageId 
from Language 
where LOWER(Language.LanguageName) = @LanguageName 

	update ClientLanguage 
	set IsDelete = @IsDelete,UpdatedBy = @UpdatedBy,UpdatedOn = @UpdatedOn
	where ClientId = @ClientId and LanguageId != @DefaultLanguageId
END
GO
/****** Object:  StoredProcedure [dbo].[InsertClientLanguage]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 18-02-2014
-- Description:	Insert Client Language into ClientLanguage table
-- =============================================
CREATE PROCEDURE [dbo].[InsertClientLanguage] 
	@ClientLanguageId uniqueidentifier out,
	@ClientId uniqueidentifier,
	@LanguageId uniqueidentifier,
	@CreatedBy uniqueidentifier = null,
	@CreatedOn datetime = null,
	@IsDelete bit
AS
declare @LanguageName varchar(10) = 'english' 
declare @DefaultLanguageId uniqueidentifier
declare @IsNotDefault bit = 0
declare @IsDefault bit = 1
BEGIN
set @ClientLanguageId = NEWID()
select @DefaultLanguageId = LanguageId 
from Language 
where LOWER(Language.LanguageName) = @LanguageName 

If not Exists(select 1 from ClientLanguage where ClientId = @ClientId and LanguageId = @DefaultLanguageId)
begin
	insert into ClientLanguage
(ClientLanguageId,ClientId,LanguageId,IsDefault,IsDelete,VersionNo,CreatedBy,CreatedOn)
values(@ClientLanguageId,@ClientId,@LanguageId,@IsDefault,@IsDelete,default,@CreatedBy,@CreatedOn)

end
else
begin
if @LanguageId != @DefaultLanguageId
begin
	insert into ClientLanguage
(ClientLanguageId,ClientId,LanguageId,IsDefault,IsDelete,VersionNo,CreatedBy,CreatedOn)
values(@ClientLanguageId,@ClientId,@LanguageId,@IsNotDefault,@IsDelete,default,@CreatedBy,@CreatedOn)
end
end 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateLanguageLabelByLabelName]    Script Date: 06/05/2014 15:02:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 08-02-2014
-- Description:	Update language label by label name in languagelabel table
-- =============================================
CREATE PROCEDURE [dbo].[UpdateLanguageLabelByLabelName]--'44d8617e-376e-42b5-99c7-fcc251202070','City','City',NULL,NULL
	@LanguageId uniqueidentifier,
	@LabelName varchar(50),
	@LabelLocal nvarchar(100),
	@UpdatedBy uniqueidentifier = null,
	@UpdatedOn datetime = null,
	@IsDelete bit = 0
AS
declare @LabelId uniqueidentifier
declare @LanguageLabelId uniqueidentifier
BEGIN
	select @LabelId = LableId from Lable where LableName = @LabelName
	
	select @LanguageLabelId = LanguageLableId 
	from LanguageLable 
	where LableId = @LabelId and LanguageId = @LanguageId

	if @LanguageLabelId IS NULL
	BEGIN
	exec [InsertLanguageLabel] NULL,@LanguageId,@LabelName,@LabelLocal,@UpdatedBy,@UpdatedOn
	END
	ELSE
	BEGIN
	update LanguageLable
	set LableLocal = @LabelLocal,IsDelete = @IsDelete,UpdatedBy = @UpdatedBy,UpdatedOn = @UpdatedOn
	where LableId = @LabelId and LanguageId = @LanguageId
	END


END
GO
/****** Object:  Default [DF_ClientSubscription_CurrentUserCount]    Script Date: 06/05/2014 15:02:43 ******/
ALTER TABLE [dbo].[ClientSubscription] ADD  CONSTRAINT [DF_ClientSubscription_CurrentUserCount]  DEFAULT ((0)) FOR [CurrentUserCount]
GO
/****** Object:  ForeignKey [FK_LanguageLable_Lable]    Script Date: 06/05/2014 15:02:43 ******/
ALTER TABLE [dbo].[LanguageLable]  WITH CHECK ADD  CONSTRAINT [FK_LanguageLable_Lable] FOREIGN KEY([LableId])
REFERENCES [dbo].[Lable] ([LableId])
GO
ALTER TABLE [dbo].[LanguageLable] CHECK CONSTRAINT [FK_LanguageLable_Lable]
GO
/****** Object:  ForeignKey [FK_LanguageLable_Language]    Script Date: 06/05/2014 15:02:43 ******/
ALTER TABLE [dbo].[LanguageLable]  WITH CHECK ADD  CONSTRAINT [FK_LanguageLable_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO
ALTER TABLE [dbo].[LanguageLable] CHECK CONSTRAINT [FK_LanguageLable_Language]
GO
/****** Object:  ForeignKey [FK_ClientLanguage_Client]    Script Date: 06/05/2014 15:02:43 ******/
ALTER TABLE [dbo].[ClientLanguage]  WITH CHECK ADD  CONSTRAINT [FK_ClientLanguage_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
ALTER TABLE [dbo].[ClientLanguage] CHECK CONSTRAINT [FK_ClientLanguage_Client]
GO
/****** Object:  ForeignKey [FK_ClientLanguage_Language]    Script Date: 06/05/2014 15:02:43 ******/
ALTER TABLE [dbo].[ClientLanguage]  WITH CHECK ADD  CONSTRAINT [FK_ClientLanguage_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([LanguageId])
GO
ALTER TABLE [dbo].[ClientLanguage] CHECK CONSTRAINT [FK_ClientLanguage_Language]
GO
