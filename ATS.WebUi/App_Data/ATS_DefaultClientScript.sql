USE [ATS_Client]
GO
/****** Object:  StoredProcedure [dbo].[DeleteMasters]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[DeleteMasters] 
@Id uniqueidentifier,
@FieldName nvarchar(max),
@TableName nvarchar(max)






AS
BEGIN
SET NOCOUNT ON;
	Declare @SQL nvarchar(max)
	
	Set @SQL = 'Update dbo.'+ @TableName + 'Set IsDelete  =  1 Where' + @FieldName+ '='''+Convert(nvarchar(max),@Id)+''''
	
	Exec(@SQL) 
	
End
GO
/****** Object:  Table [dbo].[Division]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Division](
	[DivisionId] [uniqueidentifier] NOT NULL,
	[ParentDivisionId] [uniqueidentifier] NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DefaultName] [nvarchar](256) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Division] PRIMARY KEY CLUSTERED 
(
	[DivisionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntityLanguage_back]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityLanguage_back](
	[EntityLanguageId] [uniqueidentifier] NOT NULL,
	[EntityName] [nvarchar](100) NOT NULL,
	[RecordId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[LocalName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_EntityLanguage] PRIMARY KEY CLUSTERED 
(
	[EntityName] ASC,
	[RecordId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntityLanguage]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityLanguage](
	[EntityLanguageId] [uniqueidentifier] NOT NULL,
	[EntityName] [nvarchar](100) NOT NULL,
	[RecordId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[LocalName] [nvarchar](256) NOT NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_EntityLanguage_1] PRIMARY KEY CLUSTERED 
(
	[EntityName] ASC,
	[RecordId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DrpStringMapping]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DrpStringMapping](
	[DrpStringMappingId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[FormName] [varchar](100) NOT NULL,
	[DrpName] [varchar](100) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[Text] [nvarchar](256) NOT NULL,
	[SortOrder] [smallint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_DrpStringMapping] PRIMARY KEY CLUSTERED 
(
	[DrpStringMappingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[GetAlldivisions]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAlldivisions] --'0A007CAD-B148-429C-8C46-4758324954E8'
@ClientId uniqueidentifier,

@IsDelete bit

AS
BEGIN
SELECT _Division.[DivisionId]
      ,_Division.[ClientId]
      ,_Division.[LanguageId]
      ,_Division.[ParentDivisionId]
	  ,child.DivisionName as 'ParentDivisionName'
      ,_Division.[DivisionName]
      ,_Division.[Description]
      ,_Division.[CreatedBy]
      
      ,_Division.[IsDelete]
  FROM [dbo].[DivisionLanguage]  _Division
  left join [dbo].[DivisionLanguage] child on _Division.parentdivisionid = child.DivisionId
  AND _Division.ClientId = child.ClientId
  WHERE _Division.[ClientId] = @ClientId  and _Division.IsDelete = 'False' 
END
GO
/****** Object:  StoredProcedure [dbo].[ATS_ClientSolrSearch]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ATS_ClientSolrSearch] --'64EF5995-D7CE-46C7-B396-85411F1663FC' 
@VacancyId uniqueidentifier = null
AS
BEGIN
DECLARE @SQLQuery nvarchar(MAX)
SET @SQLQuery = N'SELECT v.VacancyId
	  ,v.[JobTitle]
      ,v.[ClientId]
      ,v.[PostedOn]
      ,v.[PositionID]
      ,_ENTL1.LocalName
      ,s2.[text] as JobTypeText
      ,s1.[text] as EmploymentTypeText
      ,s3.[text] as VacancyStateText
      ,e.[LocalName] as DivisionText
      ,e1.[LocalName] as PositionTypeText
	    ,e2.LocalName as  Location
      ,[TotalPositions]
      ,[ShowOnWeb]
      ,[FeaturedOnWeb]
      ,[JobDescription]
      ,[ShowOnWebJobDescription]
      ,[DutiesAndResponsibilities]
      ,[ShowOnWebDuties]
      ,[SkillsAndQualification]
      ,[ShowOnWebSkills]
      ,[Benefits]
      ,[ShowOnWebBenefits]
      ,[SalaryMin]
      ,[SalaryMax]
      ,[ShowOnWebSal]
	  ,v.languageid
	  ,v.CreatedBy

  FROM [Vacancy] v 
  left join [EntityLanguage] _ENTL1 on v.VacancyStatusId = _ENTL1.RecordId AND _ENTL1.LanguageId = v.languageid
  left join drpstringmapping s1 on v.[JobType] = s1.value and s1.drpname = ''JobType'' and s1.languageid = v.languageid
  left join drpstringmapping s2 on v.[EmploymentType] = s2.value and s2.drpname = ''EmploymentType'' and s2.languageid =v.languageid
  left join drpstringmapping s3 on v.[VacancyState] = s3.value and s3.drpname = ''VacancyState'' and s3.languageid = v.languageid
  left join entitylanguage e on v.[DivisionId] = e.[RecordId] and e.[LanguageId] = v.languageid
  left join entitylanguage e1 on v.[PositionTypeId] = e1.[RecordId] and e1.[LanguageId] = v.languageid
  inner join EntityLanguage e2 on v.Location = e2.RecordId  and e2.LanguageId = v.languageid
 
 where 
 v.IsDelete = 0 '

	
if @VacancyId IS NULL
BEGIN
SET @SQLQuery += ' AND v.ShowOnWeb = ''1'''
print @SQLQuery 
execute(@SQLQuery)
END
ELSE
BEGIN
SET @SQLQuery += ' AND v.VacancyId=''' + CAST(@VacancyId as nvarchar(40)) + ''''+ 'AND v.ShowOnWeb = ''1'''
print @SQLQuery 
execute(@SQLQuery)

END

END
GO
/****** Object:  Table [dbo].[ATSResume]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ATSResume](
	[ATSResumeId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[IsCoverLetter] [bit] NULL,
	[Details] [nvarchar](max) NULL,
	[UploadedFileName] [nvarchar](250) NULL,
	[NewFileName] [nvarchar](250) NULL,
	[Path] [nvarchar](250) NULL,
	[TitleName] [nvarchar](100) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NULL,
 CONSTRAINT [PK_ATSResume] PRIMARY KEY CLUSTERED 
(
	[ATSResumeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetATSResumeByContactAndProfileId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetATSResumeByContactAndProfileId]
	   @UserId uniqueidentifier,
	   @ProfileId uniqueidentifier
		AS

BEGIN
Select 

		
		[ATSResumeId]
      ,[ProfileId]
      ,[UserId]
      ,[Details]
      ,[UploadedFileName]
      ,[NewFileName]
      ,[Path]
      ,[TitleName]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[VersionNo]
  FROM [ATS].[dbo].[ATSResume]
      
 WHERE [ProfileId] = @ProfileId AND [UserId] = @UserId and [ATSResume].IsDelete = 0

 END
GO
/****** Object:  Table [dbo].[ARProcess]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ARProcess](
	[ARProcessId] [uniqueidentifier] NOT NULL,
	[VacancyId] [uniqueidentifier] NOT NULL,
	[RoundType] [nvarchar](256) NOT NULL,
	[Weight] [int] NOT NULL,
	[Reviewer] [nvarchar](256) NOT NULL,
	[ReviewerCount] [int] NOT NULL,
	[RCAutomatically] [bit] NOT NULL,
	[Score] [int] NOT NULL,
	[CandidateBatch] [nvarchar](50) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_ARProcess] PRIMARY KEY CLUSTERED 
(
	[ARProcessId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DegreeType]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DegreeType](
	[DegreeTypeId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DefaultName] [nvarchar](256) NOT NULL,
	[Priority] [int] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NULL,
 CONSTRAINT [PK_DegreeType] PRIMARY KEY CLUSTERED 
(
	[DegreeTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[CompanyName] [nvarchar](100) NOT NULL,
	[PhoneNo] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[WebSite] [nvarchar](100) NOT NULL,
	[SuffixTitle] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientLocation]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientLocation](
	[ClientLocationId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DivisionId] [uniqueidentifier] NOT NULL,
	[FullLocation] [nvarchar](100) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_ClientLocation] PRIMARY KEY CLUSTERED 
(
	[ClientLocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CheckDuplicateRecord]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 11-04-2014
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
/****** Object:  StoredProcedure [dbo].[PrimaryKeyExists]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[PrimaryKeyExists] --'EmploymentHistory','EmployementHistoryId','BD6BA847-8ADD-4E36-8FB1-077185FD41DE'

(
@TableName nvarchar(100),
@FieldName nvarchar(100),
@FieldValue uniqueidentifier
)
AS
BEGIN
Declare @Sql as nvarchar(256) 
 Set @Sql = 'Select 1 from  '+ @TableName +' where '+ @FieldName +' = '''+CONVERT(varchar(40),@FieldValue) +''''

 print @Sql
 EXECUTE sp_executesql @Sql
 
END
GO
/****** Object:  Table [dbo].[PositionType]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionType](
	[PositionTypeId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DefaultName] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_PositionType] PRIMARY KEY CLUSTERED 
(
	[PositionTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PortalContent]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortalContent](
	[PortalContentId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NULL,
	[Logo] [nvarchar](max) NULL,
	[RightText] [nvarchar](256) NULL,
	[BorderStyle] [nvarchar](max) NULL,
	[BgStyle] [nvarchar](max) NULL,
	[HeadTitle] [nvarchar](max) NULL,
	[HeadBody] [nvarchar](max) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_PortalContent] PRIMARY KEY CLUSTERED 
(
	[PortalContentId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertDivisionLanguage]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDivisionLanguage]
@DivisionLanguageId uniqueidentifier OUT,

@DivisionId uniqueidentifier,

@LanguageId uniqueidentifier,
@DivisionName nvarchar(100),
@Description nvarchar(max),
@IsDelete bit,
@CreatedOn datetime,
@CreatedBy uniqueidentifier


AS
BEGIN
SET @DivisionLanguageId = newid()
INSERT INTO [DivisionLanguage]
           ([DivisionLanguageId]
        
           ,[DivisionId]
           ,[LanguageId]
           ,[DivisionName]
           ,[Description]
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy])
           
     VALUES
           (@DivisionLanguageId
   
           ,@DivisionId
           
           ,@LanguageId
           ,@DivisionName
           ,@Description
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy)
           
End
GO
/****** Object:  StoredProcedure [dbo].[RecordCount]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RecordCount] --'Vacancy','VacancyId','33795F80-57D9-43C6-A466-03E816180694','17120BF4-7DC1-4B14-AAEB-E74AF12E6C64'  
  @condition as nvarchar(100),
  @tablename as nvarchar(100),
  @countonfield nvarchar(100),
  @languageId as uniqueidentifier,
  @UserId uniqueidentifier,
  @Divisionid uniqueidentifier


 
AS
BEGIN
set @UserId = '17120BF4-7DC1-4B14-AAEB-E74AF12E6C64'  
set @languageId = '33795F80-57D9-43C6-A466-03E816180694'
set @Divisionid ='b0da11c9-9d4d-4ec8-8326-009087c44066'
set @tablename = 'Vacancy'
declare @Sql nvarchar(250)
set @countonfield = 'VacancyId'
set @condition = N'Where CreatedBy = '''+ CAST(@UserId as varchar(50)) + ''' '
set @Sql = 'select COUNT(*) as count from '+@tablename +' ' + @condition + '='+ CAST(@UserId as varchar(50)) +'and IsDelete = 0 and LanguageId = '''+ CAST(@languageId as nvarchar(50)) + ''' and DivisionId = '''+CAST(@DivisionId as nvarchar(50)) +'''' 
print(@Sql)
exec (@Sql)
END
GO
/****** Object:  StoredProcedure [dbo].[QuickUpdateContact]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[QuickUpdateContact] 
	-- Add the parameters for the stored procedure here
	@UserId uniqueidentifier,
	@FieldValues nvarchar(max),
	@UpdatedBy uniqueidentifier,
	@UpdatedOn datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @SQL nvarchar(max)
	
	Set @SQL = 'Update [dbo].[Users] Set '+ @FieldValues + ',[UpdatedOn] = getutcdate() where [UserId] = '''+Convert(nvarchar(max),@UserId)+''''
	Exec(@SQL) 
	
END
GO
/****** Object:  Table [dbo].[JobLocation]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobLocation](
	[JobLocationId] [uniqueidentifier] NOT NULL,
	[DivisionId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DefaultValue] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_JobLocation] PRIMARY KEY CLUSTERED 
(
	[JobLocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StringMapping]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StringMapping](
	[StringMappingId] [uniqueidentifier] NOT NULL,
	[KeyName] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_StringMapping] PRIMARY KEY CLUSTERED 
(
	[StringMappingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  UserDefinedFunction [dbo].[SplitComaToTable]    Script Date: 07/11/2014 17:23:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitComaToTable](   
@input AS NVARCHAR(MAX)
)
RETURNS  @Result TABLE(Value NVARCHAR(50))
AS
BEGIN
DECLARE @str AS NVARCHAR(50)
DECLARE @ind AS Int
 IF(@input is not null)
      BEGIN
            SET @ind = CharIndex(',',@input)
            WHILE @ind > 0
            BEGIN
                  SET @str = SUBSTRING(@input,1,@ind-1)
                  SET @input = SUBSTRING(@input,@ind+1,LEN(@input)-@ind)
                  INSERT INTO @Result values (cast(@str as NVARCHAR(50)))
                  SET @ind = CharIndex(',',@input)
            END
            SET @str = @input
            INSERT INTO @Result values (cast(@str as NVARCHAR(50)))
      END
	  
RETURN
END
GO
/****** Object:  Table [dbo].[SkillType]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillType](
	[SkillTypeId] [uniqueidentifier] NOT NULL,
	[DefaultName] [nvarchar](256) NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_SkillType] PRIMARY KEY CLUSTERED 
(
	[SkillTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchResume]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchResume](
	[SearchResumeId] [uniqueidentifier] NOT NULL,
	[ParentDivisionId] [uniqueidentifier] NULL,
	[DefaultName] [nvarchar](256) NOT NULL,
	[Value] [nvarchar](256) NULL,
	[Icon] [nvarchar](100) NULL,
	[Ordinal] [int] NOT NULL,
	[Type] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_SearchResume] PRIMARY KEY CLUSTERED 
(
	[SearchResumeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tmpJsonTable]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tmpJsonTable](
	[json] [varchar](1347) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[UpdateDivisionLanguage]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[UpdateDivisionLanguage]

@DivisionId uniqueidentifier,
@LanguageId uniqueidentifier,
@DivisionName nvarchar(100),
@Description nvarchar(max),

@UpdatedOn datetime,
@UpdatedBy uniqueidentifier

AS
BEGIN

UPDATE [dbo].[DivisionLanguage]
   SET
       [DivisionId] = @DivisionId
      ,[LanguageId] = @LanguageId
      ,[DivisionName] = @DivisionName
      ,[Description] = @Description
     
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE [DivisionId] = @DivisionId and [LanguageId] = @LanguageId
END
GO
/****** Object:  StoredProcedure [dbo].[GetVacanciesById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVacanciesById]
@VacancyId uniqueidentifier,
@ClientId uniqueidentifier

AS
BEGiN
SELECT [VacancyId]
      ,[ClientId]
      ,[PositionTypeId]
      ,[JobTitle]
      ,[PositionID]
      ,[VacancyStatus]
      ,[JobType]
      ,[EmploymentType]
      ,[DivisionId]
      ,[Location]
      ,[StartDate]
      ,[EndDate]
      ,[TotalPositions]
      ,[RemainingPositions]
      ,[ShowOnWeb]
      ,[FeaturedOnWeb]
      ,[PositionRequestedBy]
      ,[PositionOwner]
      ,[JobDescription]
           ,[ShowOnWebJobDescription]
           ,[DutiesAndResponsibilities]
           ,[ShowOnWebDuties]
           ,[SkillsAndQualification]
           ,[ShowOnWebSkills]
           ,[Benefits]
           ,[ShowOnWebBenefits]
      ,[SalaryMin]
      ,[SalaryMax]
      ,[ShowOnWebSal]
      ,[HourlyMin]
      ,[HourlyMax]
      ,[ShowonWebHour]
      ,[Commission]
      ,[ShowOnWebCommission]
      ,[BonusPotential]
      ,[ShowOnWebBonus]
      ,[IsDelete]
      ,[VacancyState]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[VersionNo]
  FROM [ATS].[dbo].[Vacancy]
  Where [VacancyId] = @VacancyId AND [ClientId] = @ClientId And Vacancy.IsDelete = 0
  END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVacancyByField]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVacancyByField] 
@FieldName nvarchar(max),
@VacancyId uniqueidentifier,
@FieldValue nvarchar(max),
@UpdatedOn datetime=null,
@CreatedOn datetime = null,
@UpdatedBy uniqueidentifier = null,
@CreatedBy uniqueidentifier = null

AS
BEGIN
SET NOCOUNT ON;
	Declare @SQL nvarchar(max)
	
	Set @SQL = 'Update [dbo].[Vacancy] Set '+ @FieldName + ' = ' + @FieldValue+ ',[UpdatedOn] = getutcdate(),[UpdatedBy]= '''+Convert(nvarchar(max),@UpdatedBy)+''' where [VacancyId] = '''+Convert(nvarchar(max),@VacancyId)+''''
	
	Exec(@SQL) 
	
End
GO
/****** Object:  StoredProcedure [dbo].[UpdateVacancyStatusByVacancy]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[UpdateVacancyStatusByVacancy] 
@FieldName nvarchar(max),
@VacancyId uniqueidentifier,
@FieldValue uniqueidentifier,
@UpdatedOn datetime=null,
@CreatedOn datetime = null,
@UpdatedBy uniqueidentifier = null,
@CreatedBy uniqueidentifier = null

AS
BEGIN
SET NOCOUNT ON;
	Declare @SQL nvarchar(max)
	
	Set @SQL = 'Update [dbo].[Vacancy] Set '+ @FieldName + ' = ''' +Convert(nvarchar(max),@FieldValue)+ ''',[UpdatedOn] = getutcdate(),[UpdatedBy]= '''+Convert(nvarchar(max),@UpdatedBy)+''' where [VacancyId] = '''+Convert(nvarchar(max),@VacancyId)+''''
	
	Exec(@SQL) 
	
End
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DivisionId] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[IsDelete] [bit] NOT NULL,
	[LanguageId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDivisionPermission]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDivisionPermission](
	[UserDivisionPermissionId] [uniqueidentifier] NOT NULL,
	[DivisionId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDelete] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_UserDivisionPermission] PRIMARY KEY CLUSTERED 
(
	[UserDivisionPermissionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[UserDetailId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[HomeEmail] [nvarchar](100) NULL,
	[Affix] [nvarchar](50) NULL,
	[FirstName] [nvarchar](100) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](100) NULL,
	[GivenName] [nvarchar](100) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](100) NULL,
	[Region] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[Zip] [nvarchar](50) NULL,
	[BusinessPhoneNo] [nvarchar](50) NULL,
	[HomePhone] [nvarchar](50) NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Pager] [nvarchar](50) NULL,
	[WebSite] [nvarchar](100) NULL,
	[WorkEmail] [nvarchar](100) NULL,
	[PostOfficeBox] [nvarchar](50) NULL,
	[Misdemeanor] [bit] NULL,
	[MisdemeanorExplain] [nvarchar](100) NULL,
	[MilitaryService] [bit] NULL,
	[MilitaryTypeDischarge] [nvarchar](max) NULL,
	[EmergencyContact1] [nvarchar](50) NULL,
	[EmergencyContact2] [nvarchar](50) NULL,
	[EmergencyContact1Phone] [nvarchar](50) NULL,
	[EmergencyContact2Phone] [nvarchar](50) NULL,
	[Isdelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VacancyStatus]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VacancyStatus](
	[VacancyStatusId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[VacancyStatusText] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](50) NULL,
	[Ordinal] [int] NULL,
	[IsDelete] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_VacancyStatus] PRIMARY KEY CLUSTERED 
(
	[VacancyStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vacancy]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vacancy](
	[VacancyId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[PositionTypeId] [uniqueidentifier] NOT NULL,
	[DivisionId] [uniqueidentifier] NOT NULL,
	[JobTitle] [nvarchar](100) NULL,
	[PostedOn] [datetime] NULL,
	[PositionID] [nvarchar](100) NULL,
	[PublicCode] [bigint] NULL,
	[VacancyStatus] [int] NULL,
	[VacancyStatusId] [uniqueidentifier] NULL,
	[JobType] [int] NULL,
	[EmploymentType] [int] NULL,
	[Location] [uniqueidentifier] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[TotalPositions] [int] NULL,
	[RemainingPositions] [int] NULL,
	[ShowOnWeb] [bit] NOT NULL,
	[FeaturedOnWeb] [bit] NOT NULL,
	[PositionRequestedBy] [uniqueidentifier] NOT NULL,
	[PositionOwner] [uniqueidentifier] NOT NULL,
	[JobDescription] [nvarchar](max) NULL,
	[ShowOnWebJobDescription] [bit] NOT NULL,
	[DutiesAndresponsibilities] [nvarchar](max) NULL,
	[ShowOnWebDuties] [bit] NOT NULL,
	[SkillsAndQualification] [nvarchar](max) NULL,
	[ShowOnWebSkills] [bit] NOT NULL,
	[Benefits] [nvarchar](max) NULL,
	[ShowOnWebBenefits] [bit] NOT NULL,
	[SalaryMin] [decimal](10, 2) NULL,
	[SalaryMax] [decimal](10, 2) NULL,
	[ShowOnWebSal] [bit] NOT NULL,
	[HourlyMin] [decimal](10, 2) NULL,
	[HourlyMax] [decimal](10, 2) NULL,
	[ShowonWebHour] [bit] NOT NULL,
	[Commission] [nvarchar](max) NULL,
	[ShowOnWebCommission] [bit] NOT NULL,
	[BonusPotential] [nvarchar](max) NULL,
	[ShowOnWebBonus] [bit] NOT NULL,
	[VacancyState] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Vacancy_1] PRIMARY KEY CLUSTERED 
(
	[VacancyId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 27-03-2014
-- Description:	Update User Details
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser]
	@UserId uniqueidentifier,
	@UserName nvarchar(100),
	
	@DivisionId uniqueidentifier,
	@IsActive bit,
	@UpdatedBy uniqueidentifier,
	@UpdatedOn datetime,
	@IsDelete bit
AS
BEGIN
	Update Users set Username = @UserName,DivisionId = @DivisionId,IsActive = @IsActive,IsDelete = @IsDelete,
	UpdatedBy = @UpdatedBy,UpdatedOn = @UpdatedOn 
	where UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateJobLocation]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateJobLocation]

@JobLocationId uniqueidentifier OUT,
@DivisionId uniqueidentifier,
@ClientId uniqueidentifier,
@DefaultValue nvarchar(256),
@UpdatedBy uniqueidentifier,
@UpdatedOn Datetime,
@IsDelete bit,
@IsActive bit,
@FieldValue nvarchar(max),
@IsError int OUTPUT


AS
Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'JobLocation'
BEGIN
BEGIN TRY

  Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output
  
  IF @Result = 0
begin
	  -- Do some stuff
Update	[JobLocation]
	set
		    [DivisionId] = @DivisionId
           ,[ClientId] = @ClientId
           ,[DefaultValue]=@DefaultValue
           ,[UpdatedBy]=@UpdatedBy
		   ,[UpdatedOn]=@UpdatedOn
		   ,[IsDelete] = @IsDelete
		   ,[IsActive] = @IsActive

		   where [JobLocationId]=@JobLocationId
END   
else
begin
		Set @IsError = 101 -- Job Location already exists
end      
 END TRY
 BEGIN CATCH
  
  
  -- Raise an error with the details of the exception
  DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
  SELECT @ErrMsg = ERROR_MESSAGE(),
    @ErrSeverity = ERROR_SEVERITY()

  RAISERROR(@ErrMsg, @ErrSeverity, 1)
 END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateSkillType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSkillType]
@SkillTypeId uniqueidentifier,
@ClientId uniqueidentifier,
@DefaultName nvarchar(256),
@IsDelete bit,
@UpdatedOn datetime,
@UpdatedBy uniqueidentifier,

@FieldValue nvarchar(max),
@IsError int OUTPUT

AS
Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'SkillType'
BEGIN

Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
UPDATE [dbo].[SkillType]
   SET  
      [ClientId] = @ClientId
      ,[DefaultName] = @DefaultName
      ,[IsDelete] = @IsDelete
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE  [SkillTypeId] = @SkillTypeId  
End
else
begin
		Set @IsError = 101 -- Skill Type already exists
end
  END
GO
/****** Object:  StoredProcedure [dbo].[ValidateUserName]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateUserName]
	-- Add the parameters for the stored procedure here
	@UserName nvarchar(256), 
	@ClientId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT _User.[UserId],_User.[ClientId] from [dbo].[Users] as _User
	Where _User.[UserName] = @UserName and [ClientId] = @ClientId and _User.[IsActive] = 1
END
GO
/****** Object:  StoredProcedure [dbo].[ValidateUserByClient]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*	ERROR CODES	
   
  
  
  -101	- NotAuthenticated
  
  
  */
  
CREATE Procedure [dbo].[ValidateUserByClient]-- '4021C3D8-FB86-4D7D-9B98-B97934429C0B','samadmin@admin.com','YyK/wC0RtLfS73LgsysXuA==','TCS',0

  (         
	@UserName nvarchar(256)=null,         
	@Password nvarchar(256)=null,
	@ClientId uniqueidentifier=null,
	@IsDelete bit=null
	
   )
   AS      
   declare @IsError int
	SET @IsError = 0
	IF EXISTS (Select UserId from [Users] as _User
	where UserName = @UserName and [Password] = @Password and _User.ClientId= @ClientId  and _User.IsDelete =@IsDelete )
		BEGIN	
			print 'LoginSuccess';
			Select  UserId , @IsError as IsError from [Users] as _User
					
					where UserName = @UserName and [Password] = @Password and _User.ClientId= @ClientId  and _User.IsDelete =@IsDelete and _User.IsActive = 1
			return
		END 
	ELSE 
		BEGIN
			print 'Login un Success';
			SET @IsError = -101
		return 
	END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePositionTypeByid]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePositionTypeByid]
@PositionTypeId uniqueidentifier,
@ClientId uniqueidentifier,
@DefaultName nvarchar(100),
@UpdatedBy uniqueidentifier = null,
@UpdatedOn datetime = null,
@IsActive bit,
@FieldValue nvarchar(max),
@IsError int OUTPUT

AS
Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'PositionType'
BEGIN

Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
UPDATE [PositionType]
   SET 
      [ClientId] = @ClientId
      ,[DefaultName] = @DefaultName      
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedOn] = @UpdatedOn
      ,[IsActive] = @IsActive
  WHERE [PositionTypeId] = @PositionTypeId AND [ClientId] = @ClientId
end
else
begin
		Set @IsError = 101 -- Position Type already exists
end
  END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePortalContent]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[UpdatePortalContent]

			@PortalContentId	uniqueidentifier 
           ,@LanguageId			uniqueidentifier
           ,@ClientId			uniqueidentifier
           ,@Logo				nvarchar(max)
           ,@RightText			nvarchar(256)
           ,@BorderStyle		nvarchar(max)
           ,@BgStyle			nvarchar(max)
           ,@HeadTitle			nvarchar(max)
           ,@HeadBody			nvarchar(max)
           ,@CreatedBy			uniqueidentifier
           ,@CreatedOn			datetime
           ,@IsDelete			bit
AS
BEGIN


Update [dbo].[PortalContent]
set 
           [Logo]			 =@Logo			
           ,[RightText]		 =@RightText		
           ,[BorderStyle]	 =@BorderStyle	
           ,[BgStyle]		 =@BgStyle		
           ,[HeadTitle]		 =@HeadTitle		
           ,[HeadBody]		 =@HeadBody		
           ,[CreatedBy]		 =@CreatedBy		
           ,[CreatedOn]		 =@CreatedOn		
           ,[IsDelete]		 = @IsDelete
		   where 
		   [PortalContentId]=@PortalContentId AND 
           [LanguageId]	 =@LanguageId	AND
		   [ClientId]		 =@ClientId		
     END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDivision]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[UpdateDivision]
@ClientId uniqueidentifier,
@DivisionId uniqueidentifier,
@ParentDivisionId uniqueidentifier,
@DefaultValue nvarchar(256),
@UpdatedBy uniqueidentifier,
@UpdatedOn datetime,
@FieldValue nvarchar(max),
@IsError int OUTPUT,
@IsActive bit 

AS
Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'Division'
BEGIN

  Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output
  
IF @Result = 0
begin
	UPDATE [dbo].[Division]
	SET [DivisionId] = @DivisionId
      ,[ParentDivisionId] = @ParentDivisionId
      ,[ClientId] = @ClientId
      ,[DefaultName] = @DefaultValue
      ,IsActive = @IsActive
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
	WHERE [DivisionId] = @DivisionId AND [ClientId] = @ClientId 
end
else
begin
		Set @IsError = 101 -- Division already exists
end
End
GO
/****** Object:  StoredProcedure [dbo].[UpdateDegreeType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateDegreeType]
@DegreeTypeId uniqueidentifier,
@ClientId uniqueidentifier,
@DefaultName nvarchar(256),
@Priority int,
@IsDelete bit,
@UpdatedOn datetime,
@UpdatedBy uniqueidentifier,

@FieldValue nvarchar(max),
@IsError int OUTPUT

AS
Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'DegreeType'
BEGIN

Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
UPDATE [dbo].[DegreeType]
   SET  
      [ClientId] = @ClientId
      ,[DefaultName] = @DefaultName
      ,[Priority] = @Priority
      ,[IsDelete] = @IsDelete
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE  [DegreeTypeId] = @DegreeTypeId  
End
else
begin
		Set @IsError = 101 -- Degree Type already exists
end
  END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEntityLanguage]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEntityLanguage]

@RecordId uniqueidentifier,
@LanguageId uniqueidentifier,
@EntityName nvarchar(100),
@LocalName nvarchar(256)

AS
BEGIN



Update [EntityLanguage] set

           [LocalName]=@LocalName
		   where 
		   [LanguageId]=@LanguageId AND
           [RecordId]=@RecordId AND
           [EntityName]=@EntityName
      END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCompany]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCompany]


@CompanyId uniqueidentifier OUT
,@ClientId uniqueidentifier
,@CompanyName nvarchar(100)
,@PhoneNo nvarchar(50)
,@Address nvarchar(max)
,@WebSite nvarchar(100)
,@IsActive bit
,@SuffixTitle nvarchar(100)
,@IsDelete bit
,@UpdatedOn datetime
,@UpdatedBy uniqueidentifier

AS
BEGIN
UPDATE [dbo].[Company]
   SET [CompanyId] = @CompanyId
      ,[ClientId] = @ClientId
      ,[CompanyName] = @CompanyName
      ,[PhoneNo] = @PhoneNo
      ,[Address] = @Address
      ,[WebSite] = @WebSite
      ,[IsActive] = @IsActive
      ,[IsDelete] = @IsDelete
     ,[SuffixTitle] = @SuffixTitle
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE [CompanyId] = @CompanyId And [ClientId] = @ClientId
END
GO
/****** Object:  Table [dbo].[SystemMenu]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemMenu](
	[SystemMenuId] [uniqueidentifier] NOT NULL,
	[ParentSystemMenuId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[SystemMenueName] [nvarchar](256) NULL,
	[SEODiaplayName] [nvarchar](256) NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[MenuTitle] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NULL,
	[MetaKeyWords] [nvarchar](max) NULL,
	[PageURL] [nvarchar](100) NULL,
	[Icon] [nvarchar](256) NULL,
	[Ordinal] [int] NULL,
	[IsDelete] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_SystemMenu] PRIMARY KEY CLUSTERED 
(
	[SystemMenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SignupUser]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SignupUser]-- NULL,'vaishali.khopkar@promptsoftech.com','111',NULL,'DF9C2730-08DE-467E-BC8D-3D065FE1F855',NULL
	-- Add the parameters for the stored procedure here
	@UserId uniqueidentifier OUTPUT,
	@LanguageId uniqueidentifier,
	@Username nvarchar(256),
	@Password nvarchar(256),
	@DivisionId uniqueidentifier =NULL,
	@ClientId nvarchar(256),
	@IsError int OUTPUT,
	@Active bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	Declare @IsActive bit
	Declare @IsDelete bit
	
	Select @UserId = UserId ,@IsDelete =IsDelete,@IsActive =IsActive  from [dbo].[Users] 	
	Where UserName = @Username and [ClientId] = @ClientId	
	Set @IsError = 0
	IF (@UserId  Is Not Null and (@IsActive = 1 AND @IsDelete = 1))
		Begin
			Set @IsError = 102 -- USer Deleted By Addmin
			return
			
		End
	Else IF (@UserId  Is Not Null and @IsActive = 0 AND @IsDelete = 0)
		Begin
			Set @IsError = 105 -- USer is registed but not active
			return
			
		End
	Else IF (@UserId  Is Not Null and @IsActive = 1 or @IsDelete = 0)
		Begin
			Set @IsError = 104 -- User already Exist
			return
		End
	Else 
		
			Begin
				Set @IsError = 0
				Set @UserId= newid()
				
				Insert Into [dbo].[Users] 
					([UserId],[UserName],[Password],[ClientId],[DivisionId],[IsDelete],[IsActive],[CreatedOn],[LanguageId] )
				Values (
					@UserId,@UserName,@Password,@ClientId,@DivisionId,0,@Active,getutcdate(),@LanguageId)
			End			
	--print  @Username
	--print  @Password
	--print  @DivisionId
	--print  @ClientId
	--print @IsError 
END
GO
/****** Object:  Table [dbo].[Search]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Search](
	[SearchId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Location] [nvarchar](256) NULL,
	[EmploymentType] [nvarchar](256) NULL,
	[Position] [nvarchar](256) NULL,
	[JobType] [nvarchar](100) NULL,
	[SalMinRange] [decimal](10, 2) NULL,
	[SalMaxRange] [decimal](10, 2) NULL,
	[KeyWords] [nvarchar](256) NULL,
	[DateMinRange] [datetime] NULL,
	[DateMaxRange] [datetime] NULL,
	[LanguageId] [uniqueidentifier] NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Search] PRIMARY KEY CLUSTERED 
(
	[SearchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaveResumeSearch]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaveResumeSearch](
	[SaveResumeSearchId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[JsonQuery] [nvarchar](max) NULL,
	[SearchQueryName] [nvarchar](256) NULL,
	[IsDefault] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_SaveResumeSearch] PRIMARY KEY CLUSTERED 
(
	[SaveResumeSearchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertSkillType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertSkillType]
@SkillTypeId uniqueidentifier OUT,
@ClientId uniqueidentifier,
@DefaultName nvarchar(256),
@IsDelete bit,
@CreatedOn datetime,
@CreatedBy uniqueidentifier,
@FieldValue nvarchar(max),
@IsError int OUTPUT

AS

Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'SkillType'
BEGIN
Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output


IF @Result = 0
begin
SET @SkillTypeId = NEWID();
INSERT INTO [dbo].[SkillType]
           ([SkillTypeId]
           ,[ClientId]
           ,[DefaultName]
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy]
)
     VALUES
           (@SkillTypeId
                      ,@ClientId
           ,@DefaultName
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy
          )
END

else
begin
		Set @IsError = 101 -- Skill Type already exists
end
      END
GO
/****** Object:  StoredProcedure [dbo].[InsertPositionType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPositionType]
@PositionTypeId uniqueidentifier OUT,
@ClientId uniqueidentifier,
@DefaultName nvarchar(256),
@IsDelete bit,
@CreatedBy uniqueidentifier = null,
@CreatedOn datetime = null,
@IsActive bit,
@FieldValue nvarchar(max),
@IsError int OUTPUT

AS
declare @Result bit = 0
declare @TableName varchar(50) = 'PositionType'
Set @IsError = 0
BEGIN
Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
SET @PositionTypeId = newid();

INSERT INTO [PositionType]
           ([PositionTypeId]
           ,[ClientId]
           ,[DefaultName]
           ,[IsActive] 
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
     (@PositionTypeId
           ,@ClientId
           ,@DefaultName
           ,@IsActive
           ,@IsDelete
           ,@CreatedBy
           ,@CreatedOn
          )
end
else
begin
		Set @IsError = 101 -- Position Type already exists
end
      END
GO
/****** Object:  StoredProcedure [dbo].[InsertPortalContent]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[InsertPortalContent]

			@PortalContentId	uniqueidentifier OUT
           ,@LanguageId			uniqueidentifier
           ,@ClientId			uniqueidentifier
           ,@Logo				nvarchar(max)
           ,@RightText			nvarchar(256)
           ,@BorderStyle		nvarchar(max)
           ,@BgStyle			nvarchar(max)
           ,@HeadTitle			nvarchar(max)
           ,@HeadBody			nvarchar(max)
           ,@CreatedBy			uniqueidentifier
           ,@CreatedOn			datetime
           ,@IsDelete			bit
AS
BEGIN
SET @PortalContentId = newid();

INSERT INTO [dbo].[PortalContent]
           ([PortalContentId]
           ,[LanguageId]
           ,[ClientId]
           ,[Logo]
           ,[RightText]
           ,[BorderStyle]
           ,[BgStyle]
           ,[HeadTitle]
           ,[HeadBody]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[IsDelete])
     VALUES
           (@PortalContentId
			,@LanguageId		
			,@ClientId		
			,@Logo			
			,@RightText		
			,@BorderStyle	
			,@BgStyle		
			,@HeadTitle		
			,@HeadBody		
			,@CreatedBy		
			,@CreatedOn		
			,@IsDelete		
			)
			end
GO
/****** Object:  StoredProcedure [dbo].[InsertJobLocation]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertJobLocation]

@JobLocationId uniqueidentifier OUT,
@DivisionId uniqueidentifier,
@ClientId uniqueidentifier,
@DefaultValue nvarchar(256),
@CreatedBy uniqueidentifier,
@CreatedOn Datetime,
@FieldValue nvarchar(max),
@IsError int OUTPUT

AS
declare @Result bit = 0
declare @TableName varchar(50) = 'JobLocation'
Set @IsError = 0
BEGIN
BEGIN TRY

Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
	SET @JobLocationId = newid();  
	  -- Do some stuff
	  INSERT INTO [JobLocation]
         (
		    [JobLocationId]
           ,[DivisionId]
           ,[ClientId]
           ,[DefaultValue]
           ,[CreatedBy]
		   ,[CreatedOn]
		   ,[IsDelete]
		   ,[IsActive]
           
         )
	     VALUES
		 (
			@JobLocationId
			,@DivisionId 
			,@ClientId 
			,@DefaultValue      
			,@CreatedBy
			,@CreatedOn
			,0
			,1
		)
End
else
begin
		Set @IsError = 101 -- Job Location already exists
end
  

 END TRY
 BEGIN CATCH
  
  
  -- Raise an error with the details of the exception
  DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int
  SELECT @ErrMsg = ERROR_MESSAGE(),
    @ErrSeverity = ERROR_SEVERITY()

  RAISERROR(@ErrMsg, @ErrSeverity, 1)
 END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*	ERROR CODES	
   
  
  
  -101	- NotAuthenticated
  
  
  */
  
CREATE Procedure [dbo].[GetUserById]-- '4021C3D8-FB86-4D7D-9B98-B97934429C0B','samadmin@admin.com','YyK/wC0RtLfS73LgsysXuA==','TCS',0

  (         
	
	@UserId uniqueidentifier=null,
	@IsDelete bit
   )
   AS      
   SELECT [UserId]
      ,[Password] --Added by viral shah
      ,[Username]
      ,[ClientId]
	  ,[DivisionId]
	  ,[IsActive]
      ,[CreatedBy]
  FROM [Users]  where IsDelete = @IsDelete AND UserId=@UserId
GO
/****** Object:  StoredProcedure [dbo].[InsertATSResume]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertATSResume]
	   @ATSResumeId uniqueidentifier OUT
      ,@ProfileId uniqueidentifier
      ,@UserId uniqueidentifier
      ,@Details nvarchar(MAX)= null
      ,@UploadedFileName nvarchar(250)
      ,@NewFileName nvarchar(250)
      ,@Path nvarchar(250)
      ,@TitleName nvarchar(100) = null
      ,@IsDelete bit
      ,@CreatedBy uniqueidentifier
      ,@CreatedOn datetime
	  ,@IsCoverLetter bit		
		   
		   AS
		   BEGIN

		   SET @ATSResumeId = NEWID();

		   INSERT INTO [dbo].[ATSResume]
    (
	[ATSResumeId]
	,[ProfileId]
	,[UserId] 
   ,[Details] 
   ,[UploadedFileName]
   ,[NewFileName]
   ,[Path]
   ,[TitleName]
   ,[IsDelete]
   ,[CreatedBy] 
   ,[CreatedOn]
   ,[IsCoverLetter]) 
   values
   (
   @ATSResumeId 
    ,@ProfileId
    ,@UserId
   , @Details
   , @UploadedFileName
   , @NewFileName 
   , @Path 
   , @TitleName 
   ,@IsDelete
   , @CreatedBy
   , @CreatedOn
   ,@IsCoverLetter
   )

 

 END
GO
/****** Object:  StoredProcedure [dbo].[InsertDivision]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDivision] 
@DivisionId uniqueidentifier OUT,
@ParentDivisionId uniqueidentifier = null,
@ClientId uniqueidentifier,
@DefaultName nvarchar(256),
@IsDelete bit,
@CreatedOn datetime,
@CreatedBy uniqueidentifier,
@FieldValue nvarchar(max),
@IsError int OUTPUT,
@IsActive bit

AS
declare @Result bit = 0
declare @TableName varchar(50) = 'Division'
Set @IsError = 0
BEGIN

Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output

IF @Result = 0
begin
SET @DivisionId = newid();
INSERT INTO [dbo].[Division]
           ([DivisionId]
           ,[ParentDivisionId]
           ,[DefaultName]
           ,[ClientId]
           ,[IsActive] 
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy]
           )
     VALUES
           (@DivisionId
           ,@ParentDivisionId
            ,@DefaultName
           ,@ClientId
           ,@IsActive
          ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy
           )
end
else
begin
		Set @IsError = 101 -- Division already exists
end
END
GO
/****** Object:  StoredProcedure [dbo].[InsertDegreeType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDegreeType]
@DegreeTypeId uniqueidentifier OUT,
@ClientId uniqueidentifier,
@DefaultName nvarchar(256),
@Priority int,
@IsDelete bit,

@CreatedOn datetime,
@CreatedBy uniqueidentifier,
@FieldValue nvarchar(max),
@IsError int OUTPUT

AS

Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'DegreeType'
BEGIN
Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output


IF @Result = 0
begin
SET @DegreeTypeId = NEWID();
INSERT INTO [dbo].[DegreeType]
           ([DegreeTypeId]
           ,[ClientId]
           ,[DefaultName]
           ,[Priority]
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy]
)
     VALUES
           (@DegreeTypeId
                      ,@ClientId
           ,@DefaultName
           ,@Priority
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy
          )
END

else
begin
		Set @IsError = 101 -- Degree Type already exists
end
      END
GO
/****** Object:  StoredProcedure [dbo].[InsertCompany]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCompany]
@CompanyId uniqueidentifier OUT
,@ClientId uniqueidentifier
,@CompanyName nvarchar(100)
,@SuffixTitle nvarchar(100)
,@PhoneNo nvarchar(50)
,@Address nvarchar(max)
,@WebSite nvarchar(100)
,@IsActive bit
,@IsDelete bit
,@CreatedOn datetime
,@CreatedBy uniqueidentifier
           
AS
BEGIN
SET @CompanyId =  NEWID();
INSERT INTO [dbo].[Company]
           ([CompanyId]
           ,[ClientId]
           ,[CompanyName]
           ,[PhoneNo]
           ,[Address]
           ,[WebSite]
           ,[IsActive]
           ,[SuffixTitle]
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy]
           )
     VALUES
           (@CompanyId
           ,@ClientId
           ,@CompanyName
           ,@PhoneNo
           ,@Address
           ,@WebSite
           ,@IsActive 
           ,@SuffixTitle
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy)
           End
GO
/****** Object:  StoredProcedure [dbo].[InsertEntityLanguage]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertEntityLanguage]
@EntityLanguageId uniqueidentifier OUT,
@RecordId uniqueidentifier,
@LanguageId uniqueidentifier,
@EntityName nvarchar(100),
@LocalName nvarchar(256),
@IsDelete bit

AS
BEGIN

SET @EntityLanguageId = newid();

INSERT INTO [EntityLanguage]
           (
		   [EntityLanguageId]
           ,[LanguageId]
           ,[RecordId]
           ,[EntityName]
           ,[LocalName]
           ,[IsDelete]         
           
           )
     VALUES
     (@EntityLanguageId
           ,@LanguageId
           ,@RecordId
           ,@EntityName
           ,@LocalName
           ,@IsDelete
          )
           
           
      END
GO
/****** Object:  StoredProcedure [dbo].[GetSkillTypeById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSkillTypeById]
@SkillTypeId uniqueidentifier,
@IsDelete bit

AS
BEGIN
SELECT [SkillTypeId]
      ,[ClientId]
      ,[DefaultName]
      ,[IsDelete]
      
  FROM [dbo].[SkillType]
  
  where [SkillTypeId] = @SkillTypeId and IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserFromUserName]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetUserFromUserName]
@UserName nvarchar(100),
@IsDelete bit

AS
BEGIN

Select UserId,IsActive from Users where UserName = @UserName ANd IsDelete = @IsDelete 
--IF (IsActive = 0 AND @IsDelete = 0)
--	Begin
--		Set @IsError = 105 -- USer is registed but not active
--		return
--	End 
--Else
--	Begin
--		Set @IsError = 0
--	End
 END
GO
/****** Object:  StoredProcedure [dbo].[GetPositionTypeByid]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPositionTypeByid]
@PositionTypeId uniqueidentifier,
@ClientId uniqueidentifier,
@IsDelete bit
AS
BEGIN

SELECT [PositionTypeId]
      ,[ClientId]
      ,[DefaultName]
	  ,CreatedBy
	  ,IsActive
  FROM [dbo].[PositionType]
  WHERE [PositionTypeId] = @PositionTypeId AND [ClientId] = @ClientId and IsDelete = @IsDelete 

  END
GO
/****** Object:  StoredProcedure [dbo].[GetPortalContentById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetPortalContentById]

			@PortalContentId	uniqueidentifier 
           ,@LanguageId			uniqueidentifier
		        ,@ClientId			uniqueidentifier
           
AS
BEGIN


select
            [Logo]			
           ,[RightText]			
           ,[BorderStyle]	
           ,[BgStyle]		
           ,[HeadTitle]			
           ,[HeadBody]		
           ,[CreatedBy]			
           ,[CreatedOn]			
           ,[IsDelete]		
		   from 
		   [dbo].[PortalContent]
		   where 
		   [PortalContentId]=@PortalContentId AND 
           [LanguageId]	 =@LanguageId	AND
		   [ClientId]		 =@ClientId	 and IsDelete = 0	
     END
GO
/****** Object:  StoredProcedure [dbo].[GetCompanyById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetCompanyById]
@CompanyId uniqueidentifier,
@IsDelete bit
AS
Begin


SELECT [CompanyId]
      ,[ClientId]
      ,[CompanyName]
      ,[PhoneNo]
      ,[Address]
      ,[WebSite]
      ,[IsActive] 
      ,[SuffixTitle]
      ,[IsDelete]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[UpdatedOn]
      ,[UpdatedBy]
      ,[VersionNo]
  FROM [dbo].[Company]
  Where [CompanyId] = @CompanyId and IsDelete = @IsDelete 
END
GO
/****** Object:  StoredProcedure [dbo].[GetLanguageDivisionById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLanguageDivisionById]
@DivisionId uniqueidentifier,
@LanguageId uniqueidentifier,
@ClientId uniqueidentifier,
@IsDelete bit

AS
BEGIN

select _Division.DivisionId as 'DivisionId'
       ,_Division.ClientId as 'ClientId'
       ,_Division.ParentDivisionId as 'ParentDivisionId'
       ,_DivisionLanguage.DivisionlanguageId   
       ,child.DivisionName as 'ParentDivisionName'
       ,_DivisionLanguage.DivisionName as 'DivisionName'
       ,_DivisionLanguage.Description as 'Description'
       ,_DivisionLanguage.LanguageId as 'LanguageId' 
       ,_DivisionLanguage.DivisionId
     
       from Division as _Division 
       left join [dbo].[DivisionLanguage] child on _Division.parentdivisionid = child.DivisionId
       join DivisionLanguage as _DivisionLanguage on
       _Division.Divisionid = _DivisionLanguage.DivisionId  where _Division.ClientId = @ClientId and _DivisionLanguage.languageid = @LanguageId and _Division.DivisionId = @DivisionId


END
GO
/****** Object:  StoredProcedure [dbo].[GetJobLocationBySearch]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetJobLocationBySearch] --'DF9C2730-08DE-467E-BC8D-3D065FE1F855','33795F80-57D9-43C6-A466-03E816180694','La%',0,'33e70182-2f34-4f5c-b7d5-16738c036106'
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@SearchValue nvarchar(max) = null,
@IsDelete bit,
@DivisionId uniqueidentifier

AS
BEGIN


Select 
           _JobLocation.JobLocationId,
		   _JobLocation.DivisionId,
		   _JobLocation.DefaultValue,
		   _JobLocation.ClientId,
		   _JobLocation.CreatedBy,
		   _JobLocation.IsActive,
		   _EntityLanguage.LocalName
		 
		   from 
		   JobLocation _JobLocation 
		   
               		   
		   inner join EntityLanguage _EntityLanguage
		   		   on _JobLocation.JobLocationId = _EntityLanguage.RecordId
							AND _EntityLanguage.EntityName = 'JobLocation'
							AND _EntityLanguage.LanguageId = @LanguageId
							AND _EntityLanguage.LocalName Like @SearchValue	
							AND  _JobLocation.DivisionId = @DivisionId			
							And _EntityLanguage.IsDelete = @IsDelete 
		    where  _JobLocation.IsDelete = @IsDelete
		 
		  
		  

		

		   
           
      
 print(@SearchValue)
  END
GO
/****** Object:  StoredProcedure [dbo].[GetJobLocationById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetJobLocationById]

@JobLocationId uniqueidentifier,
@IsDelete bit
AS
BEGIN

Select 
           _JobLocation.JobLocationId,
		   _JobLocation.DivisionId,
		   _JobLocation.DefaultValue,
		   _JobLocation.IsActive,
		   
		   _JobLocation.CreatedBy
		   
		   from 
		   JobLocation _JobLocation 
		   
		   where _JobLocation.JobLocationId = @JobLocationId and _JobLocation.IsDelete = @IsDelete
		   
           
      

  END
GO
/****** Object:  StoredProcedure [dbo].[GetEntityLanguageByEntityAndRecordId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEntityLanguageByEntityAndRecordId]

@RecordId uniqueidentifier,
@EntityName nvarchar(100)


AS
BEGIN


Select 
           
		   [LanguageId],
           [LocalName]
		   from 
		   [EntityLanguage] 
		   where 
           [RecordId]=@RecordId AND
           [EntityName]=@EntityName And IsDelete = 0
      END
GO
/****** Object:  StoredProcedure [dbo].[GetEntityLanguageByEntityAndLanguageId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEntityLanguageByEntityAndLanguageId]

@LanguageId uniqueidentifier,
@EntityName nvarchar(100)


AS
BEGIN


Select 
           
		   RecordId,
           [LocalName]
		   from 
		   [EntityLanguage] 
		   where 
           [LanguageId]=@LanguageId AND
           [EntityName]=@EntityName and IsDelete = 0
      END
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[ProfileId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ProfileName] [nvarchar](100) NULL,
	[Objectives] [nvarchar](max) NULL,
	[Hobbies] [nvarchar](250) NULL,
	[Category] [nvarchar](100) NULL,
	[SubCategory] [nvarchar](100) NULL,
	[CurrentSalary] [decimal](18, 0) NULL,
	[ExpectedSalary] [decimal](18, 0) NULL,
	[IsDefault] [bit] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
	[LanguageId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[ProfileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCoverLetter]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCoverLetter]
@AtsResumeId uniqueidentifier
As
Begin
DELETE FROM [dbo].[ATSResume]
      WHERE [ATSResumeId] = @AtsResumeId And IsCoverLetter = 1
      End
GO
/****** Object:  StoredProcedure [dbo].[DeleteEntityLanguage]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteEntityLanguage]  
@RecordId as nvarchar(max)

As  
Begin  
Update EntityLanguage Set IsDelete = 1 where Cast(RecordId as nvarchar(40)) in (@RecordId) 
End
GO
/****** Object:  Table [dbo].[BlockCandidate]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockCandidate](
	[BlockCandidateId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_BlockCandidate] PRIMARY KEY CLUSTERED 
(
	[BlockCandidateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[ATSResumeId] [uniqueidentifier] NOT NULL,
	[ATSCoverLetterId] [uniqueidentifier] NULL,
	[VacancyId] [uniqueidentifier] NOT NULL,
	[VacancyStatus] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ATSPrivilege]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ATSPrivilege](
	[ATSPrivilegeId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_ATSPrivilege] PRIMARY KEY CLUSTERED 
(
	[ATSPrivilegeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ATSSecurityRole]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ATSSecurityRole](
	[ATSSecurityRoleId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DefaultName] [nvarchar](256) NULL,
	[SequenceNo] [int] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_ATSSecurityRole] PRIMARY KEY CLUSTERED 
(
	[ATSSecurityRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetATSResumeByUserAndProfileId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetATSResumeByUserAndProfileId]
	   @UserId uniqueidentifier,
	   @ProfileId uniqueidentifier,
	   @IsDelete bit
		AS
		Declare @IsCoverLetter bit = 0
BEGIN
Select 

		[ATSResumeId]
      ,[ProfileId]
      ,[UserId]
      ,[Details]
      ,[UploadedFileName]
      ,[NewFileName]
      ,[Path]
      ,[TitleName]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[VersionNo]
  FROM [dbo].[ATSResume]
      
 WHERE [ProfileId] = @ProfileId AND [UserId] = @UserId and IsCoverLetter = @IsCoverLetter
		And IsDelete = @IsDelete 

 END
GO
/****** Object:  StoredProcedure [dbo].[GetATSResumeByProfileId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetATSResumeByProfileId]
	   
	   @ProfileId uniqueidentifier,
	   @IsDelete bit
		AS

BEGIN
Select 

		
		[ATSResumeId]
      ,[ProfileId]
      ,[UserId]
      ,[Details]
      ,[UploadedFileName]
      ,[NewFileName]
      ,[Path]
      ,[TitleName]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[VersionNo]
  FROM [dbo].[ATSResume]
      
 WHERE [ProfileId] = @ProfileId and IsDelete = @IsDelete 

 END
GO
/****** Object:  StoredProcedure [dbo].[GetATSResumeById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetATSResumeById]
@ATSResumeId uniqueidentifier,
@IsDelete bit
As
Begin
SELECT [ATSResumeId]
      ,[ProfileId]
      ,[UserId]
      ,[IsCoverLetter]
      ,[Details]
      ,[UploadedFileName]
      ,[NewFileName]
      ,[Path]
      ,[TitleName]
      ,[IsDelete]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[VersionNo]
  FROM [ATSResume]

Where [ATSResumeId] = @ATSResumeId and IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetDivisionById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDivisionById]
@DivisionId uniqueidentifier,
@IsDelete bit

AS
BEGIN

select _Division.DivisionId as 'DivisionId'
       ,_Division.ParentDivisionId as 'ParentDivisionId'
       ,_Division.DefaultName as 'DefaultName'
	   ,_Division.CreatedBy
	   ,_Division.IsActive
       from Division as _Division 
	   where
       _Division.Divisionid = @DivisionId AND IsDelete = @IsDelete


END
GO
/****** Object:  StoredProcedure [dbo].[GetDivisionByClientAndId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDivisionByClientAndId]
@DivisionId uniqueidentifier,
@ClientId uniqueidentifier

AS
BEGIN

SELECT [DivisionId]
      ,[ParentDivisionId]
      ,[ClientId]
      ,[IsDelete]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[UpdatedOn]
      ,[UpdatedBy]
      ,[VersionNo]
  FROM [dbo].[Division]
  WHERE [DivisionId] = @DivisionId AND [ClientId] = @ClientId and IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetDegreeTypeById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDegreeTypeById]
@DegreeTypeId uniqueidentifier,
@IsDelete bit

AS
BEGIN
SELECT [DegreeTypeId]
      ,[ClientId]
      ,[DefaultName]
      ,[Priority]
      ,[IsDelete]
      
  FROM [dbo].[DegreeType]
  
  where [DegreeTypeId] = @DegreeTypeId and IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPositionTypeByUsersAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllPositionTypeByUsersAndLanguage] 
@Users nvarchar(MAX)=null,
@LanguageId uniqueidentifier,
@IsDelete bit
AS
BEGIN
if @Users IS NULL
begin
	Select 
           _PositionType.PositionTypeId,
		   _PositionType.DefaultName,
		   _PositionType.ClientId,
		   _PositionType.CreatedBy,
		   _PositionType.IsActive,
		   _EntityLanguage.LocalName
		   from 
		   PositionType _PositionType 
		   
		   inner join EntityLanguage _EntityLanguage
		   		   on _PositionType.PositionTypeId = _EntityLanguage.RecordId
		   AND _EntityLanguage.EntityName = 'PositionType'
		   AND _EntityLanguage.LanguageId = @LanguageId and _PositionType .IsDelete = @IsDelete 
		   and _EntityLanguage.IsDelete =@IsDelete 
		    order by _PositionType.CreatedOn desc 
  END
  ELSE
  BEGIN
  Select 
           _PositionType.PositionTypeId,
		   
		   _PositionType.DefaultName,
		   _PositionType.ClientId,
		   _PositionType.CreatedBy,
		   _PositionType.IsActive,
		   _EntityLanguage.LocalName
		   from 
		   PositionType _PositionType 
		   
		   inner join EntityLanguage _EntityLanguage
		   		   on _PositionType.PositionTypeId = _EntityLanguage.RecordId
		   AND _EntityLanguage.EntityName = 'PositionType'
		   AND _EntityLanguage.LanguageId = @LanguageId and _PositionType .IsDelete = @IsDelete 
		   and _EntityLanguage.IsDelete =@IsDelete And
			_PositionType.CreatedBy in (SELECT value FROM [dbo].[SplitComaToTable] (@Users))
			order by _PositionType.CreatedOn desc
  END
  END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPositionTypeByClient]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllPositionTypeByClient] --'DF9C2730-08DE-467E-BC8D-3D065FE1F855','33795F80-57D9-43C6-A466-03E816180694',0

@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit
AS
declare @IsActive bit = 1
BEGIN

Select 
           _PositionType.PositionTypeId,
		   _PositionType.DefaultName,
		   _PositionType.ClientId,
		   _PositionType.CreatedBy,
		   _EntityLanguage.LocalName
		  
		   from 
		   PositionType _PositionType 
			inner join EntityLanguage _EntityLanguage  on _PositionType.PositionTypeId = _EntityLanguage.RecordId
									AND _EntityLanguage.EntityName = 'PositionType'
									AND _EntityLanguage.LanguageId = @LanguageId 
									And _EntityLanguage.IsDelete = @IsDelete 
           and IsActive = @IsActive and _PositionType.IsDelete = @IsDelete
           --Where 
      
      

  END
GO
/****** Object:  StoredProcedure [dbo].[GetAllJobLocationByLanguageId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllJobLocationByLanguageId] --'33795F80-57D9-43C6-A466-03E816180694',0
@Users nvarchar(MAX)=null,
@LanguageId uniqueidentifier,
@IsDelete bit
AS
declare @IsActive bit=1
BEGIN
if @Users IS Null
BEGIN
Select 
           _JobLocation.JobLocationId,
		   _JobLocation.DivisionId,
		   _JobLocation.DefaultValue,
		   _JobLocation.ClientId,
		   _JobLocation.CreatedBy,
		   _JobLocation.IsActive,
		   _EntityLanguage.LocalName,
		   _EntityLanguage1.LocalName as 'DivisionName'
		   from 
		   JobLocation _JobLocation 
		   
		   inner join EntityLanguage _EntityLanguage on _JobLocation.JobLocationId = _EntityLanguage.RecordId
							AND _EntityLanguage.EntityName = 'JobLocation' AND _EntityLanguage.LanguageId = @LanguageId 
							And _EntityLanguage.IsDelete = @IsDelete 
	--Added By Love Gandhi
		   inner join Division _Div on _Div.DivisionId = _JobLocation.DivisionId and _Div.IsActive = @IsActive and _Div.IsDelete = @IsDelete 
	--END
												 
		   inner join EntityLanguage _EntityLanguage1 on _JobLocation.DivisionId= _EntityLanguage1.RecordId
							AND _EntityLanguage1.EntityName = 'Division' AND _EntityLanguage1.LanguageId = @LanguageId AND _JobLocation.IsDelete = 0
							And _EntityLanguage1.IsDelete = @IsDelete 
			order by _JobLocation.CreatedOn desc
		   
           
      

  END
  Else
  begin 
  Select 
           _JobLocation.JobLocationId,
		   _JobLocation.DivisionId,
		   _JobLocation.DefaultValue,
		   _JobLocation.ClientId,
		   _JobLocation.CreatedBy,
		   _EntityLanguage.LocalName,
		   _EntityLanguage1.LocalName as 'DivisionName'
		   from 
		   JobLocation _JobLocation 
		   
		   inner join EntityLanguage _EntityLanguage on _JobLocation.JobLocationId = _EntityLanguage.RecordId
									AND _EntityLanguage.EntityName = 'JobLocation' AND _EntityLanguage.LanguageId = @LanguageId 
									And _EntityLanguage.IsDelete = @IsDelete 
		   --Added By Love Gandhi
		   inner join Division _Div on _Div.DivisionId = _JobLocation.DivisionId and _Div.IsActive = @IsActive and _Div.IsDelete = @IsDelete 
			--END

		   inner join EntityLanguage _EntityLanguage1 on _JobLocation.DivisionId= _EntityLanguage1.RecordId
								AND _EntityLanguage1.EntityName = 'Division'AND _EntityLanguage1.LanguageId = @LanguageId 
								And _EntityLanguage1.IsDelete = @IsDelete 								
          where  _JobLocation.IsDelete = 0 AND _JobLocation.DivisionId in (SELECT value FROM [dbo].[SplitComaToTable] (@Users))
          order by _JobLocation.CreatedOn desc
          
		   
      END
      END
GO
/****** Object:  StoredProcedure [dbo].[GetAllDropDownValue]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 05-02-2014
-- Description:	Get all dropdown value,text by passing dropdown name
-- =============================================
CREATE PROCEDURE [dbo].[GetAllDropDownValue]
@LanguageId uniqueidentifier,
@DrpName varchar(100),
@IsDelete bit
AS
BEGIN
	select Value,Text from DrpStringMapping
where LanguageId = @LanguageId and DrpName = @DrpName and IsDelete = @IsDelete
order by SortOrder
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllActiveDivisionbyClientAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllActiveDivisionbyClientAndLanguage] --'df9c2730-08de-467e-bc8d-3d065fe1f855','310613d3-1cd4-4a34-bcf4-36a8697ee242', false
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit

AS
declare @IsActive bit = 1
BEGIN

select _Division.DivisionId as 'DivisionId'
       ,_Division.ClientId as 'ClientId'
       ,_Division.CreatedBy
       ,_Division.UpdatedBy
       ,_Division.ParentDivisionId as 'ParentDivisionId'
       ,_Entitylanguage.EntityLanguageId   
       ,child.LocalName as 'ParentDivisionName'
       ,_Entitylanguage.LocalName as 'DivisionName'
       ,_Entitylanguage.LanguageId as 'LanguageId' 
       ,_Entitylanguage.RecordId
       from Division as _Division 
       left join [dbo].[EntityLanguage] child on _Division.parentdivisionid = child.RecordId AND child.LanguageId = @LanguageId
        join [EntityLanguage] as _Entitylanguage on
       _Division.Divisionid = _Entitylanguage.RecordId  
       where _Division.ClientId = @ClientId and _Entitylanguage.languageid = @LanguageId and _Division.IsDelete = @IsDelete and IsActive = @IsActive
       and _Entitylanguage.IsDelete =@IsDelete 
   END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSkillTypeBylanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllSkillTypeBylanguage]
@LanguageId uniqueidentifier,
@IsDelete bit

AS
Begin

	Select _SkillType.SkillTypeId,
		   _SkillType.DefaultName,
		   _SkillType.ClientId,
		   _SkillType.CreatedBy,
		   _EntityLanguage.LocalName
		   from 
		   SkillType _SkillType 
		   
		   inner join EntityLanguage _EntityLanguage
		   		   on  _SkillType.SkillTypeId = _EntityLanguage.RecordId
		   AND _EntityLanguage.EntityName = 'SkillType'
		   AND _EntityLanguage.LanguageId = @LanguageId and _SkillType.IsDelete = @IsDelete
		   And _EntityLanguage.IsDelete = @IsDelete 
		   order by _SkillType.CreatedOn desc
		   END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSearchResumeModuleAndFields]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 01-05-2014
-- Description:	Get Search Resume Fields
-- =============================================
CREATE PROCEDURE [dbo].[GetAllSearchResumeModuleAndFields]
	@LanguageId uniqueidentifier
AS
BEGIN
	select SR.Value as 'ModuleKey',EL.LocalName as 'ModuleName',SR.Icon,
	       SR1.Value as 'FieldKey', EL1.LocalName as 'FieldName',SR1.[Type]
from SearchResume SR
inner join SearchResume SR1 on SR.SearchResumeId = SR1.ParentDivisionId and SR1.IsDelete = 0
inner join EntityLanguage EL on SR.SearchResumeId = EL.RecordId 
								and SR.ParentDivisionId is null 
								and SR.IsActive = 1 
								and SR.IsDelete = 0
								and EL.LanguageId = @LanguageId
								and EL.IsDelete = 0
inner join EntityLanguage EL1 on EL1.RecordId = SR1.SearchResumeId 
								 and SR1.IsActive = 1 
								 and SR1.IsDelete = 0
								 and EL1.LanguageId = @LanguageId
								 and EL1.IsDelete = 0
Where sr.IsDelete = 0								 
order by SR.Ordinal ,EL1.LocalName
END
GO
/****** Object:  Table [dbo].[DivisionPositionType]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DivisionPositionType](
	[DivisionPositionTypeId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[DivisionId] [uniqueidentifier] NOT NULL,
	[PositionTypeId] [uniqueidentifier] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_DivisionPositionType] PRIMARY KEY CLUSTERED 
(
	[DivisionPositionTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetallDivisionbyClientAndUsers]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetallDivisionbyClientAndUsers] --'df9c2730-08de-467e-bc8d-3d065fe1f855','310613d3-1cd4-4a34-bcf4-36a8697ee242', false

@LanguageId uniqueidentifier,
@IsDelete bit,
@Users nvarchar(MAX)=null

AS
declare @IsActive bit = 1
BEGIN
if @Users IS NULL 
begin
select _Division.DivisionId as 'DivisionId'
       ,_Division.ClientId as 'ClientId'
       ,_Division.CreatedBy
       ,_Division.UpdatedBy
       ,_Division.ParentDivisionId as 'ParentDivisionId'
       ,_Entitylanguage.EntityLanguageId   
       ,child.LocalName as 'ParentDivisionName'
       ,_Entitylanguage.LocalName as 'DivisionName'
       ,_Entitylanguage.LanguageId as 'LanguageId' 
       ,_Entitylanguage.RecordId
       ,_Division.IsActive
       from Division as _Division 
       left join [dbo].[EntityLanguage] child on _Division.parentdivisionid = child.RecordId AND child.LanguageId = @LanguageId and child.IsDelete=@IsDelete
        join [EntityLanguage] as _Entitylanguage on
       _Division.Divisionid = _Entitylanguage.RecordId  
       where _Entitylanguage.languageid = @LanguageId and _Division.IsDelete = @IsDelete and _Entitylanguage.IsDelete = @IsDelete  
       order by _Division.CreatedOn  desc
	   END
	   ELSE
	   BEGIN
	   select _Division.DivisionId as 'DivisionId'
       ,_Division.ClientId as 'ClientId'
       ,_Division.CreatedBy
       ,_Division.UpdatedBy
       ,_Division.ParentDivisionId as 'ParentDivisionId'
       ,_Entitylanguage.EntityLanguageId   
       ,child.LocalName as 'ParentDivisionName'
       ,_Entitylanguage.LocalName as 'DivisionName'
       ,_Entitylanguage.LanguageId as 'LanguageId' 
       ,_Entitylanguage.RecordId
     ,_Division.IsActive
       from Division as _Division 
       left join [dbo].[EntityLanguage] child on _Division.parentdivisionid = child.RecordId AND child.LanguageId = @LanguageId
        join [EntityLanguage] as _Entitylanguage on
       _Division.Divisionid = _Entitylanguage.RecordId  
       where _Entitylanguage.languageid = @LanguageId and _Division.IsDelete = @IsDelete And _Entitylanguage.IsDelete = @IsDelete 
       AND  _Division.DivisionId in (SELECT value FROM [dbo].[SplitComaToTable] (@Users))
       order by _Division.CreatedOn  desc
	   END
   END
GO
/****** Object:  StoredProcedure [dbo].[GetallDivisionbyClientAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetallDivisionbyClientAndLanguage] --'df9c2730-08de-467e-bc8d-3d065fe1f855','310613d3-1cd4-4a34-bcf4-36a8697ee242', false
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit

AS

BEGIN

select _Division.DivisionId as 'DivisionId'
       ,_Division.ClientId as 'ClientId'
       ,_Division.CreatedBy
       ,_Division.UpdatedBy
       ,_Division.ParentDivisionId as 'ParentDivisionId'
       ,_Entitylanguage.EntityLanguageId   
       ,child.LocalName as 'ParentDivisionName'
       ,_Entitylanguage.LocalName as 'DivisionName'
       ,_Entitylanguage.LanguageId as 'LanguageId' 
       ,_Entitylanguage.RecordId
       from Division as _Division 
       left join [dbo].[EntityLanguage] child on _Division.parentdivisionid = child.RecordId AND child.LanguageId = @LanguageId and _Division.IsDelete=@IsDelete
        join [EntityLanguage] as _Entitylanguage on
       _Division.Divisionid = _Entitylanguage.RecordId  
       where _Division.ClientId = @ClientId and _Entitylanguage.languageid = @LanguageId and _Division.IsDelete = @IsDelete
       and _Entitylanguage.IsDelete =@IsDelete 
       
   END
GO
/****** Object:  StoredProcedure [dbo].[GetAllDegreeTypeBylanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllDegreeTypeBylanguage]
@LanguageId uniqueidentifier,
@IsDelete bit

AS
Begin

	Select _DegreeType.DegreeTypeId,
		   _DegreeType.DefaultName,
		   _DegreeType.ClientId,
		   _DegreeType.CreatedBy,
		   _DegreeType.Priority,
		   _EntityLanguage.LocalName
		   from 
		   DegreeType _DegreeType 
		   
		   inner join EntityLanguage _EntityLanguage
		   		   on  _DegreeType.DegreeTypeId = _EntityLanguage.RecordId
		   AND _EntityLanguage.EntityName = 'DegreeType'
		   AND _EntityLanguage.LanguageId = @LanguageId and _DegreeType.IsDelete = @IsDelete
		   And _EntityLanguage.IsDelete =@IsDelete 
		   Order By CreatedOn desc
		   END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCoverLetter]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 12-03-2014
-- Description:	Get all CoverLetter 
-- =============================================
CREATE PROCEDURE [dbo].[GetAllCoverLetter]
	@UserId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select ATSResumeId,AR.ProfileId,AR.UploadedFileName
	from ATSResume AR 
	where AR.UserId = @UserId
							 and AR.IsDelete = @IsDelete
							 and AR.IsCoverLetter = 1
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCompany]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCompany]
@ClientId uniqueidentifier,
@IsDelete bit
AS
BEGIN

SELECT [CompanyId]
      ,[CompanyName]
      ,[PhoneNo]
      ,[ClientId]
      ,[Address]
      ,[WebSite]
      ,[IsActive] 
      ,[IsDelete]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[UpdatedOn]
      ,[UpdatedBy]
      ,[VersionNo]
  FROM [dbo].[Company]
  where ClientId = @ClientId And IsDelete = @IsDelete 
  order by CreatedOn desc
  End
GO
/****** Object:  Table [dbo].[EmploymentHistory]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmploymentHistory](
	[EmployementHistoryId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CompanyName] [nvarchar](100) NULL,
	[MayWeContact] [bit] NULL,
	[SupervisorName] [nvarchar](100) NULL,
	[Telephone] [nvarchar](100) NULL,
	[Address] [nvarchar](256) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[Zip] [nvarchar](100) NULL,
	[StartMonth] [int] NULL,
	[EndMonth] [int] NULL,
	[StartYear] [int] NULL,
	[EndYear] [int] NULL,
	[Experience] [int] NULL,
	[JobCategory] [nvarchar](100) NULL,
	[StartingPay] [decimal](10, 2) NULL,
	[StartigPosition] [nvarchar](100) NULL,
	[MostRecentPosition] [nvarchar](100) NULL,
	[EndingPay] [decimal](10, 2) NULL,
	[ReasonForLeaving] [nvarchar](max) NULL,
	[DutiesAndResponsibilities] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_EmploymentHistory] PRIMARY KEY CLUSTERED 
(
	[EmployementHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EducationHistory]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EducationHistory](
	[EducationHistoryId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[InstitutionName] [nvarchar](100) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[DegreeDate] [datetime] NULL,
	[MeasureSystem] [nvarchar](50) NULL,
	[MeasureValue] [nvarchar](50) NULL,
	[DegreeType] [uniqueidentifier] NULL,
	[MajorSubject] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[Country] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_EducationHistory] PRIMARY KEY CLUSTERED 
(
	[EducationHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[DelteBlockCandidate]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DelteBlockCandidate]
@UserId uniqueidentifier
AS
BEGIN

DELETE FROM [dbo].[BlockCandidate]
      WHERE [UserId] = @UserId
      
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteVacancy]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteVacancy]
@ClientId uniqueidentifier,
@VacancyId uniqueidentifier,
@UpdatedBy uniqueidentifier,
@UpdatedOn datetime
AS
BEGIN

UPDATE [dbo].[Vacancy]
SET [IsDelete] = 1
WHERE [ClientId] = @ClientId AND [VacancyId] = @VacancyId

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSaveResumeSearch]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSaveResumeSearch]
	@SaveResumeSearchId uniqueidentifier,
	@UpdatedBy uniqueidentifier,
	@UpdatedOn datetime
AS
declare @IsDelete bit = 1
declare @IsDefault bit = 0
BEGIN
      
   update SaveResumeSearch set IsDelete = @IsDelete,IsDefault = @IsDefault where SaveResumeSearchId = @SaveResumeSearchId
   
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteDegreeTypeEducationHistory]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteDegreeTypeEducationHistory]  
@DegreeTypeId as nvarchar(max)  
  
As  
Begin  
Update dbo.EducationHistory  Set IsDelete = 1 Where   Cast(DegreeType as nvarchar(40))  in(@DegreeTypeId)  
End
GO
/****** Object:  StoredProcedure [dbo].[GetAllCandidateApplicationsByUser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetAllCandidateApplicationsByUser]
@UserId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit

AS
BEGIN

select 
    _APP.ApplicationId,
    _APP.CreatedOn as 'AppliedOn',
    _VANC.VacancyId,
    _VANC.JobTitle,
    _VANC.Location,
    _VANC.VacancyStatus,
    _ATSR.UploadedFileName as ResumeName,
	_ATSR.ProfileId,
	_ATSR.ATSResumeId,
	_APP.ATSCoverLetterId as CoverLetterId,
	_ENTL.LocalName as 'LocationText',
	_ENTL1.LocalName as 'VacancyStatusText',
	_ATSR1.UploadedFileName as CoverLetterName,
	_PRO.ProfileName as ProfileName
    from Application _App 
    inner join Vacancy _VANC on _APP.VacancyId = _VANC.VacancyId and _VANC.IsDelete =@IsDelete 
	inner join  ATSResume _ATSR on _APP.ATSResumeId = _ATSR.ATSResumeId And _ATSR.IsDelete =@IsDelete 
	left join  ATSResume _ATSR1 on _APP.ATSCoverLetterId = _ATSR1.ATSResumeId And _ATSR1.IsDelete =@IsDelete 
	inner join [Profile] _PRO on _PRO.ProfileId = _ATSR.ProfileId  And _PRO.IsDelete =@IsDelete
	inner join [EntityLanguage] _ENTL on _VANC.Location = _ENTL.RecordId AND _ENTL.LanguageId = @LanguageId and _ENTL.IsDelete =@IsDelete 
	inner join [EntityLanguage] _ENTL1 on _VANC.VacancyStatusId = _ENTL1.RecordId AND _ENTL1.LanguageId = @LanguageId and _ENTL1.IsDelete =@IsDelete 
    where _APP.CreatedBy = @UserId and _App.IsDelete = @IsDelete 
    order by _App.CreatedOn desc
    END
GO
/****** Object:  StoredProcedure [dbo].[GetAllCandidateApplications]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetAllCandidateApplications]
@UserId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit

AS
BEGIN

select 
    _APP.ApplicationId,
    _VANC.VacancyId,
    _VANC.JobTitle,
    _VANC.Location,
    _VANC.VacancyStatus,
     _DRP.[Text] as 'VacancyStatusText',
    _ATSR.UploadedFileName as ResumeName,
	_ATSR.ProfileId,
	_ATSR1.UploadedFileName as CoverLetterName,
	_PRO.ProfileName as ProfileName
    from Application _App 
    inner join Vacancy _VANC on _APP.VacancyId = _VANC.VacancyId And _VANC.IsDelete = @IsDelete 
    inner join  [DrpStringMapping] _DRP on _VANC.[VacancyStatus] = _DRP.Value 
					AND _DRP.FormName = 'Vacancy' AND _DRP.DrpName = 'VacancyStatus' 
					AND _DRP.LanguageId = @LanguageId And _DRP.IsDelete = @IsDelete 
	inner join  ATSResume _ATSR on _APP.ATSResumeId = _ATSR.ATSResumeId And _ATSR.IsDelete =@IsDelete
	left join  ATSResume _ATSR1 on _APP.ATSCoverLetterId = _ATSR1.ATSResumeId And _ATSR1.IsDelete =@IsDelete 
	inner join [Profile] _PRO on _PRO.ProfileId = _ATSR.ProfileId And _PRO.IsDelete = @IsDelete 
    where _APP.CreatedBy = @UserId and _APP.IsDelete = @IsDelete
    END
GO
/****** Object:  StoredProcedure [dbo].[GetAllBlockCandidatesByUser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetAllBlockCandidatesByUser]
@CreatedById uniqueidentifier,
@IsDelete bit

AS
BEGIN

Select UserId  from BlockCandidate where CreatedBy = @CreatedById And IsDelete = 0

End
GO
/****** Object:  StoredProcedure [dbo].[GetAllATSSecurityRoleByClientAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 20-02-2014
-- Description:	Get all security role by client and language
-- =============================================
CREATE PROCEDURE [dbo].[GetAllATSSecurityRoleByClientAndLanguage] 
	@IsDelete bit,
	@ClientId uniqueidentifier,
	@LanguageId uniqueidentifier,
	@SequenceNo int 
AS
BEGIN
	select ATSSecurityRole.ATSSecurityRoleId,EL.LocalName,SequenceNo
	from ATSSecurityRole 
	inner join EntityLanguage EL on EL.RecordId = ATSSecurityRole.ATSSecurityRoleId 
					and EL.IsDelete =@IsDelete 
					and ATSSecurityRole.ClientId = @ClientId
					and EL.LanguageId = @LanguageId
					and ATSSecurityRole.IsDelete = @IsDelete  
					and LOWER(DefaultName) != 'candidate'
					--and SequenceNo >= @SequenceNo
	order by SequenceNo
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllATSPrevilegeByClientAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 20-02-2014
-- Description:	Get all user ATS Previlege by client and language from ATSPrivilege table
-- =============================================
CREATE PROCEDURE [dbo].[GetAllATSPrevilegeByClientAndLanguage]
	@IsDelete bit,
	@ClientId uniqueidentifier,
	@LanguageId uniqueidentifier
AS
BEGIN
	select AP.ATSPrivilegeId,EL.LocalName
	from ATSPrivilege AP
	inner join EntityLanguage EL on EL.RecordId = AP.ATSPrivilegeId
					and EL.IsDelete =@IsDelete 
					and AP.ClientId = @ClientId
					and EL.LanguageId = @LanguageId
					and AP.IsDelete = @IsDelete 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllApplyVacancy]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 15-03-2014
-- Description:	Get All Vacancy from Application Table by userId
-- =============================================
CREATE PROCEDURE [dbo].[GetAllApplyVacancy] 
	@UserId uniqueidentifier,
	@LanguageId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select VacancyId
	from [Application]
	inner join ATSResume AR on AR.ATSResumeId = [Application].ATSResumeId 
							and AR.UserId = @UserId
							and LanguageId = @LanguageId 
							and AR.IsDelete = @IsDelete 
							and [Application].IsDelete = @IsDelete
							and AR.IsCoverLetter = 0
END
GO
/****** Object:  Table [dbo].[ExecutiveSummary]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExecutiveSummary](
	[ExecutiveSummaryId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[ExecutiveSummaryDetails] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NULL,
 CONSTRAINT [PK_ExecutiveSummary] PRIMARY KEY CLUSTERED 
(
	[ExecutiveSummaryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetAllDivisionPositionType]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllDivisionPositionType] --'f041f20f-ba1e-4ec7-8613-6e02bac4d0f4,5c543808-7867-4c22-a503-c2acddd250c9,dc9000f6-c664-462f-890d-435c9f9bd99b','DF9C2730-08DE-467E-BC8D-3D065FE1F855','33795f80-57d9-43c6-a466-03e816180694',0
@Divisions nvarchar(max)= null,
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit
AS
declare @IsActive bit=1
BEGIN
if(@Divisions IS NULL)
BEGIN
select _DP.DivisionPositionTypeId,
       _DP.ClientId,
       _DP.DivisionId,
       _DP.PositionTypeId,
       _EL.LocalName as DivisionName,
       _EL1.LocalName as PositionTypeName
       From DivisionPositiontype _DP 
       
       --Added By Love
       inner Join Division _DIV on _DP.DivisionId = _DIV.DivisionId AND _DP.ClientId = @ClientId and _Div.IsActive = @IsActive  and	_Div.IsDelete  = @IsDelete
      -- End       
       inner Join EntityLanguage _EL on _DP.DivisionId = _El.RecordId And _EL.LanguageId = @LanguageId And _EL.IsDelete = @IsDelete 
        inner Join PositionType _PT on _DP.PositionTypeId = _PT.PositionTypeId AND _DP.ClientId = @ClientId And _PT.IsDelete =@IsDelete
       Inner Join EntityLanguage _EL1 on _DP.PositiontypeId = _EL1.RecordId And _EL1.LanguageId = @LanguageId And _EL1.IsDelete =@IsDelete 
       where _DP.IsDelete = @IsDelete 
       order by _DP.CreatedOn  desc
END

else
BEGIN
select _DP.DivisionPositionTypeId,
       _DP.ClientId,
       _DP.DivisionId,
       _DP.PositionTypeId,
       _EL.LocalName as DivisionName,
       _EL1.LocalName as PositionTypeName
       From DivisionPositiontype _DP 
       
       --Added By Love
       
       inner Join Division _DIV 
       on _DP.DivisionId = _DIV.DivisionId AND _DP.ClientId = @ClientId and _Div.IsActive = @IsActive  and	_Div.IsDelete  = @IsDelete
       
       --End
       Inner Join EntityLanguage _EL on _DP.DivisionId = _El.RecordId And _EL.LanguageId = @LanguageId And _EL.IsDelete = @IsDelete 
       inner Join PositionType _PT  on _DP.PositionTypeId = _PT.PositionTypeId AND _DP.ClientId = @ClientId And _PT.IsDelete =@IsDelete 
       Inner Join EntityLanguage _EL1 on _DP.PositiontypeId = _EL1.RecordId And _EL1.LanguageId = @LanguageId And _EL1.IsDelete =@IsDelete 
       where _DP.IsDelete = @IsDelete AND _DP.DivisionId in (SELECT value FROM [dbo].[SplitComaToTable] (@Divisions))
       order by _DP.CreatedOn  desc
end
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllProfileAndCoverLetter]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 12-03-2014
-- Description:	Get all Profile And Cover letter
-- =============================================
CREATE PROCEDURE [dbo].[GetAllProfileAndCoverLetter]
	@UserId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select ATSResumeId,AR.ProfileId,IsCoverLetter,UploadedFileName
	from ATSResume AS AR
	inner join [Profile] on [Profile].ProfileId = AR.ProfileId 
	                         and [Profile].UserId = @UserId 
							 and [Profile].IsDelete = @IsDelete
							 and AR.IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 12-03-2014
-- Description:	Get all Profile 
-- =============================================
CREATE PROCEDURE [dbo].[GetAllProfile]
	@UserId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select ATSResumeId,AR.ProfileId,[Profile].ProfileName
	from [Profile] 
	inner join ATSResume AS AR on [Profile].ProfileId = AR.ProfileId 
	                         and [Profile].UserId = @UserId 
							 and [Profile].IsDelete = @IsDelete
							 and AR.IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSaveResumeSearch]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllSaveResumeSearch]
@UserId uniqueidentifier,
@IsDelete bit
AS
BEGIN
SELECT [SaveResumeSearchId]
      ,[UserId]
      ,[JsonQuery]
      ,[SearchQueryName]
      ,[IsDefault]
      ,[IsDelete]
            
      
      
      
  FROM [dbo].[SaveResumeSearch] where  UserId = @UserId and IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetApplicationByAtsResumeId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetApplicationByAtsResumeId]
@AtsResumeId uniqueidentifier,
@IsDelete bit
As
Begin

SELECT [ApplicationId]
     
  FROM [dbo].[Application]

  Where [ATSCoverLetterId] = @AtsResumeId  Or [ATSResumeId] = @AtsResumeId  and [Application].IsDelete = 0  
  End
GO
/****** Object:  StoredProcedure [dbo].[GetApplicationByApplicationId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetApplicationByApplicationId]  
@ApplicationId uniqueidentifier,  
@IsDelete bit  
As  
Begin  
  
select   
       [ATSResumeId]  
      ,[ATSCoverLetterId]  
      ,[VacancyId]  
      ,[LanguageId]  
      ,[CreatedBy]  
      FROM [dbo].[Application] _App where _App.ApplicationId= @ApplicationId and _App.IsDelete = @IsDelete  
      order by _App.CreatedOn desc
END
GO
/****** Object:  StoredProcedure [dbo].[GetApplicantDetailsByVacancy]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetApplicantDetailsByVacancy] --'C75C67A4-7A23-44DE-8D61-3F82A5CFC899',0
@VacancyId uniqueidentifier,
@IsDelete bit
As
Begin

select 

	_UserDetails.FirstName + ' ' + _UserDetails.LastName as ApplicantName,
	_App.ApplicationId,
	_App.VacancyId,
	_Vac.JobTitle As JobTitle,	
	_App.CreatedOn As AppliedOn,
	_App.ATSCoverLetterId as CoverLetterId,
	_App.ATSResumeId as ResumeId,
	--_ATSR.ATSResumeId,
	_ATSR.UploadedFileName as ResumeName,
	_ATSR.NewFileName as NewFileName,
	_ATSR.ProfileId,
	_ATSR1.UploadedFileName as CoverLetterName,
	_PRO.ProfileName as ProfileName
	from UserDetails _UserDetails
	inner join application _App on _UserDetails.UserId = _App.CreatedBy and _App.IsDelete = @IsDelete
	left join  ATSResume _ATSR on _APP.ATSResumeId = _ATSR.ATSResumeId and _ATSR.IsDelete = @IsDelete
	left join  ATSResume _ATSR1 on _APP.ATSCoverLetterId = _ATSR1.ATSResumeId and _ATSR.IsDelete = @IsDelete
	left join [Profile] _PRO on _PRO.ProfileId = _ATSR.ProfileId and  _PRO.IsDelete = @IsDelete
	inner join Vacancy _Vac on _Vac.VacancyId = _App.VacancyId and _Vac.IsDelete = @IsDelete
where _App.VacancyId = @VacancyId and _UserDetails.Isdelete  = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllVacancyStatusByCategoryAndlanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllVacancyStatusByCategoryAndlanguage]
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit,
@Category nvarchar(100)= null

AS
Begin
if(@Category!=null OR @Category != '')
begin
	Select _VAS.VacancyStatusId,
		   _VAS.Category,
		   _VAS.ClientId,
		   _VAS.VacancyStatusText,
		   
		   _EntityLanguage.LocalName as 'VacancyStatusTextLocal'
		   from 
		   VacancyStatus _VAS 
		   
		   inner join EntityLanguage _EntityLanguage
		   		   on  _VAS.VacancyStatusId = _EntityLanguage.RecordId 
		   AND _EntityLanguage.EntityName = 'VacancyStatus'
		   AND _EntityLanguage.LanguageId = @LanguageId and _VAS.IsDelete = @IsDelete And _EntityLanguage.IsDelete =@IsDelete 
		   AND _VAS.Category = @Category 
		   AND _VAS.ClientId = @ClientId
		   
		   END
		   else
		   begin
		   	Select _VAS.VacancyStatusId,
		   _VAS.Category,
		   _VAS.ClientId,
		   _VAS.VacancyStatusText,
		   
		   _EntityLanguage.LocalName as 'VacancyStatusTextLocal'
		   from 
		   VacancyStatus _VAS 
		   
		   inner join EntityLanguage _EntityLanguage
		   		   on  _VAS.VacancyStatusId = _EntityLanguage.RecordId 
		   AND _EntityLanguage.EntityName = 'VacancyStatus'
		   AND _EntityLanguage.LanguageId = @LanguageId and _VAS.IsDelete = @IsDelete And _EntityLanguage.IsDelete =@IsDelete 
		  
		   AND _VAS.ClientId = @ClientId
		   order by  _VAS.Ordinal
		   end
End
GO
/****** Object:  StoredProcedure [dbo].[GetAllVacancyStatus]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllVacancyStatus]
@ClientId uniqueidentifier
,@LanguageId uniqueidentifier
,@IsDelete bit
AS
BEGIN
SELECT  _VacancyStaus.VacancyStatusId as 'VacancyStatusId'
       ,_VacancyStaus.ClientId as 'ClientId'
       ,_VacancyStaus.CreatedBy
       ,_VacancyStaus.Category as 'Category'
       ,_VacancyStaus.VacancyStatusText as 'VacancyStatusText'
       ,_Entitylanguage.EntityLanguageId   
       ,_Entitylanguage.LocalName as 'VacancyStatusTextLocal'
       ,_Entitylanguage.LanguageId as 'LanguageId' 
       ,_Entitylanguage.RecordId
       from VacancyStatus as _VacancyStaus 
       join [EntityLanguage] as _Entitylanguage on
       _VacancyStaus.VacancyStatusId = _Entitylanguage.RecordId  And _Entitylanguage.IsDelete =@IsDelete 
       where _VacancyStaus.ClientId = @ClientId and _Entitylanguage.languageid = @LanguageId and _VacancyStaus.IsDelete = 0
       Order by _VacancyStaus.CreatedOn desc 
   END
GO
/****** Object:  StoredProcedure [dbo].[GetAllVacancyApplyByUser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 19-03-2014
-- Description:	Get All Vacancy that applaid by user 
-- =============================================
CREATE PROCEDURE [dbo].[GetAllVacancyApplyByUser]
@UserId uniqueidentifier,
@LanguageId uniqueidentifier,
@VacancyState int,
@IsDelete bit
AS
Declare @VacancyStatus uniqueidentifier
select @VacancyStatus = StringMappingId from StringMapping where Lower(KeyName) = 'draft'
BEGIN
select distinct [Application].VacancyId
from [Application]
inner join ATSResume AR on AR.ATSResumeId = [Application].ATSResumeId and AR.UserId = @UserId and [Application].LanguageId = @LanguageId and AR.IsDelete = @IsDelete 
inner join StringMapping SM on SM.StringMappingId = [Application].VacancyStatus and [Application].VacancyStatus = @VacancyStatus
inner join Vacancy on Vacancy.VacancyId = [Application].VacancyId and Vacancy.IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllVacanciesByUsersAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllVacanciesByUsersAndLanguage]-- '17120BF4-7DC1-4B14-AAEB-E74AF12E6C64,17120BF4-7DC1-4B14-AAEB-E74AF12E6C64','310613D3-1CD4-4A34-BCF4-36A8697EE242','0'
@Users nvarchar(MAX)=null,
@LanguageId uniqueidentifier,
@IsDelete bit

AS
BEGiN
if @Users IS NULL 
begin
SELECT _VAN.[VacancyId]
      ,_VAN.[ClientId]
      ,_VAN.[PostedOn]
      ,_VAN.[LanguageId]
      ,_VAN.[PositionTypeId]
      ,_VAN.[JobTitle]
      ,_VAN.[PositionID]
      --,_VAN.[VacancyStatus]
      ,_VAN.[JobType]
      ,_VAN.[EmploymentType]
      ,_VAN.[DivisionId]
      ,_VAN.[Location]
      ,_VAN.[StartDate]
      ,_VAN.[EndDate]
      ,_VAN.[TotalPositions]
      ,_VAN.[RemainingPositions]
      ,_VAN.[ShowOnWeb]
      ,_VAN.[FeaturedOnWeb]
      ,_VAN.[PositionRequestedBy]
      ,_VAN.[PositionOwner]
      ,_VAN.[JobDescription]
      ,_VAN.[SkillsAndQualification]
      ,_VAN.[DutiesAndResponsibilities]
      ,_VAN.[Benefits]
      ,_VAN.[SalaryMin]
      ,_VAN.[SalaryMax]
      ,_VAN.[HourlyMin]
      ,_VAN.[HourlyMax]
      ,_VAN.[Commission]
      ,_VAN.[BonusPotential]
      ,_VAN.[IsDelete]
	  ,_VAN.[CreatedBy]
	  ,_ENTL1.LocalName as 'VacancyStatusText'
	  ,_DRP1.[Text] as 'JobTypeText'
	  ,_DRP2.[Text] as 'EmploymentTypeText'
	  ,_ENTL.LocalName as 'LocationText'
  FROM [dbo].[Vacancy] _VAN
  
  inner join  [DrpStringMapping] _DRP1 on _VAN.[JobType] = _DRP1.Value AND _DRP1.FormName = 'Vacancy' AND _DRP1.DrpName = 'JobType' 
																	   AND _DRP1.LanguageId = @LanguageId And _DRP1.IsDelete = @IsDelete
  inner join  [DrpStringMapping] _DRP2 on _VAN.[EmploymentType] = _DRP2.Value AND _DRP2.FormName = 'Vacancy' AND _DRP2.DrpName = 'EmploymentType' 
																			  AND _DRP2.LanguageId = @LanguageId And _DRP2.IsDelete = @IsDelete
  inner join [EntityLanguage] _ENTL on _VAN.Location = _ENTL.RecordId AND _ENTL.LanguageId = @LanguageId And _ENTL.IsDelete =@IsDelete 
  inner join [EntityLanguage] _ENTL1 on _VAN.VacancyStatusId = _ENTL1.RecordId AND _ENTL1.LanguageId = @LanguageId And _ENTL1.IsDelete =@IsDelete 
  Where  _VAN.[LanguageId] = @LanguageId and _VAN.[IsDelete] = @IsDelete  order by  _VAN.CreatedOn DESC 
  END
  else
  BEGIN
SELECT _VAN.[VacancyId]
      ,_VAN.[ClientId]
      ,_VAN.[PostedOn]
      ,_VAN.[LanguageId]
      ,_VAN.[PositionTypeId]
      ,_VAN.[JobTitle]
      ,_VAN.[PositionID]
      --,_VAN.[VacancyStatus]
      ,_VAN.[JobType]
      ,_VAN.[EmploymentType]
      ,_VAN.[DivisionId]
      ,_VAN.[Location]
      ,_VAN.[StartDate]
      ,_VAN.[EndDate]
      ,_VAN.[TotalPositions]
      ,_VAN.[RemainingPositions]
      ,_VAN.[ShowOnWeb]
      ,_VAN.[FeaturedOnWeb]
      ,_VAN.[PositionRequestedBy]
      ,_VAN.[PositionOwner]
      ,_VAN.[JobDescription]
      ,_VAN.[SkillsAndQualification]
      ,_VAN.[DutiesAndResponsibilities]
      ,_VAN.[Benefits]
      ,_VAN.[SalaryMin]
      ,_VAN.[SalaryMax]
      ,_VAN.[HourlyMin]
      ,_VAN.[HourlyMax]
      ,_VAN.[Commission]
      ,_VAN.[BonusPotential]
      ,_VAN.[IsDelete]
	  ,_VAN.[CreatedBy]
	  ,_ENTL1.LocalName as 'VacancyStatusText'
	  ,_DRP1.[Text] as 'JobTypeText'
	  ,_DRP2.[Text] as 'EmploymentTypeText'
	  ,_ENTL.LocalName as 'LocationText'
  FROM [dbo].[Vacancy] _VAN
  
  inner join  [DrpStringMapping] _DRP1 on _VAN.[JobType] = _DRP1.Value AND _DRP1.FormName = 'Vacancy' AND _DRP1.DrpName = 'JobType' 
																	   AND _DRP1.LanguageId = @LanguageId And _DRP1.IsDelete = @IsDelete 
  inner join  [DrpStringMapping] _DRP2 on _VAN.[EmploymentType] = _DRP2.Value AND _DRP2.FormName = 'Vacancy' AND _DRP2.DrpName = 'EmploymentType' 
																			  AND _DRP2.LanguageId = @LanguageId And _DRP2.IsDelete = @IsDelete 
  inner join [EntityLanguage] _ENTL on _VAN.Location = _ENTL.RecordId AND _ENTL.LanguageId = @LanguageId And _ENTL.IsDelete =@IsDelete 
   inner join [EntityLanguage] _ENTL1 on _VAN.VacancyStatusId = _ENTL1.RecordId AND _ENTL1.LanguageId = @LanguageId And _ENTL1.IsDelete =@IsDelete 
  Where  _VAN.[LanguageId] = @LanguageId and _VAN.[IsDelete] = @IsDelete AND _VAN.DivisionId in (SELECT value FROM [dbo].[SplitComaToTable] (@Users)) order by _VAN.CreatedOn DESC
  END
  END
GO
/****** Object:  StoredProcedure [dbo].[GetAllVacanciesByClientAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllVacanciesByClientAndLanguage]
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit

AS
BEGiN
SELECT [VacancyId]
      ,[ClientId]
      ,[PostedOn]
      ,[LanguageId]
      ,[PositionTypeId]
      ,[JobTitle]
      ,[PositionID]
      ,[VacancyStatusId]
      ,[JobType]
      ,[EmploymentType]
      ,[DivisionId]
      ,[Location]
      ,[StartDate]
      ,[EndDate]
      ,[TotalPositions]
      ,[RemainingPositions]
      ,[ShowOnWeb]
      ,[FeaturedOnWeb]
      ,[PositionRequestedBy]
      ,[PositionOwner]
      ,[JobDescription]
      ,[SkillsAndQualification]
      ,[DutiesAndResponsibilities]
      ,[Benefits]
      ,[SalaryMin]
      ,[SalaryMax]
      ,[HourlyMin]
      ,[HourlyMax]
      ,[Commission]
      ,[BonusPotential]
      ,[IsDelete]
  FROM [dbo].[Vacancy]
  Where [ClientId] = @ClientId and [LanguageId] = @LanguageId and [IsDelete] = @IsDelete
  END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsersByDivision]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsersByDivision] --'33795F80-57D9-43C6-A466-03E816180694' ,0,null 
@LanguageId uniqueidentifier,
@IsDelete bit,
@UsersDivision nvarchar(MAX)=null
AS
BEGIN
if @UsersDivision IS NULL 
begin
	select Users.UserId,UD.FirstName,UD.LastName,Users.Username,EL.LocalName as DivisionName,Division.DivisionId
	from Users 
	inner join UserDetails UD on Users.UserId = UD.UserId and UD.Isdelete = @IsDelete
	inner join Division on Users.DivisionId = Division.DivisionId and Division.IsDelete = @IsDelete
	inner join EntityLanguage EL on EL.RecordId = Division.DivisionId and EL.LanguageId = @LanguageId and EL.IsDelete =@IsDelete  and Users.IsDelete = @IsDelete
end
else
begin
    select Users.UserId,UD.FirstName,UD.LastName,Users.Username,EL.LocalName as DivisionName,Division.DivisionId
	from Users 
	inner join UserDetails UD on Users.UserId = UD.UserId and UD.Isdelete = @IsDelete
	inner join Division on Users.DivisionId = Division.DivisionId and Division.IsDelete = @IsDelete
	inner join EntityLanguage EL on EL.RecordId = Division.DivisionId 
									And El.IsDelete =@IsDelete 
									and EL.LanguageId = @LanguageId 									
									and Users.IsDelete = @IsDelete
									and Division.DivisionId in (SELECT value FROM [dbo].[SplitComaToTable] (@UsersDivision))
end
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetAllUsers] --'DF9C2730-08DE-467E-BC8D-3D065FE1F855','33795F80-57D9-43C6-A466-03E816180694' 
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier
As
Begin
select
 _Users.UserId,
 _Users.DivisionId,
 _UserDetails.FirstName,
 _UserDetails.LastName,
 _UserDetails.HomeEmail,
 _EntityLanguage.LocalName as 'DivisionName'
 from [Users] _Users
		   inner Join UserDetails _UserDetails on _Users.UserId = _UserDetails.UserId And _Users.IsActive = 1 and _UserDetails.Isdelete = 0
	       inner join EntityLanguage _EntityLanguage on _Users.DivisionId= _EntityLanguage.RecordId
			AND _EntityLanguage.EntityName = 'Division'AND _EntityLanguage.LanguageId = @LanguageId 
			And _EntityLanguage.IsDelete =0
   where _Users.ClientId = @ClientId and _Users.Isdelete = 0
   order by _Users.CreatedOn desc  	 
 End
GO
/****** Object:  StoredProcedure [dbo].[GetAllUserDivisionPermissionBy]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUserDivisionPermissionBy]-- null,'DF9C2730-08DE-467E-BC8D-3D065FE1F855','33795F80-57D9-43C6-A466-03E816180694',0
@Users nvarchar(MAX)=null,
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit
AS
declare @IsActive bit = 1
BEGIN
if @Users IS Null
BEGIN
select 
       _UDP.UserDivisionPermissionId
       ,_UDP.DivisionId 
       ,_UDP.UserId
       ,_UD.FirstName + ' ' + LastName as 'UserFirstName'
       ,_UD.LastName
       ,_EL.LocalName as 'DivisionName'
       from Division
       inner join UserDivisionPermission _UDP on Division.DivisionId = _UDP.DivisionId 
												 and Division.IsActive = @IsActive 
												 and Division.IsDelete  = @IsDelete 
												 AND _UDP.Isdelete = @IsDelete
       inner join Users on Users.IsActive = @IsActive and Users.UserId = _UDP.UserId
												and Users.IsDelete = @IsDelete
       inner join UserDetails _UD on Users.UserId = _UD.UserId 
										and _UDP.ClientId = @ClientId 
										AND _UD.Isdelete = 0
       inner join EntityLanguage _EL on Division.DivisionId = _EL.RecordId 
										and _EL.LanguageId = @LanguageId and _EL.IsDelete =@IsDelete 
										order by Division.CreatedOn  desc
										
       END
       else     
       Begin
       select 
       _UDP.UserDivisionPermissionId
       ,_UDP.DivisionId
       ,_UDP.UserId
       ,_UD.FirstName + ' ' + LastName as 'UserFirstName'
       ,_UD.LastName
       ,_EL.LocalName as 'DivisionName'
       from Division
       inner join UserDivisionPermission _UDP on Division.DivisionId = _UDP.DivisionId 
												 and Division.IsActive = @IsActive 
												 and Division.IsDelete  = @IsDelete 
												 AND _UDP.Isdelete = @IsDelete
       inner join Users on Users.IsActive = @IsActive and Users.UserId = _UDP.UserId 
							and Users.IsDelete = @IsDelete
       inner join UserDetails _UD on Users.UserId = _UD.UserId 
										and _UDP.ClientId = @ClientId 
										AND _UD.Isdelete = 0
       inner join EntityLanguage _EL on Division.DivisionId = _EL.RecordId 
										and _EL.LanguageId = @LanguageId and _EL.IsDelete =@IsDelete 
       where _UDP.DivisionId in (SELECT value FROM [dbo].[SplitComaToTable] (@Users))
       order by Division.CreatedOn  desc
       end
       END
GO
/****** Object:  StoredProcedure [dbo].[GetDefaultParamByLanguageAndUser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 25-02-2014
-- Description:	Get Default Search parameter by language and User from Search table
-- =============================================
CREATE PROCEDURE [dbo].[GetDefaultParamByLanguageAndUser]
	@LanguageId uniqueidentifier,
	@UserId uniqueidentifier,
	@ClientId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select Location,EmploymentType,Position,JobType,SalMinRange,SalMaxRange,KeyWords,DateMinRange,DateMaxRange
	from Search
	where ClientId = @ClientId and UserId = @UserId and LanguageId = @LanguageId and IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetCoverLetterAndResumeByProfileAndUser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetCoverLetterAndResumeByProfileAndUser] --'14F9EEDD-5FAE-4A80-B229-32F86D93998D','0585CDCF-2E13-4D48-86AB-3F1E8DD01DA8'
@ProfileId uniqueidentifier, 
@UserId uniqueidentifier 
AS
BEGIN

select 
	 
	_App.ATSCoverLetterId as CoverLetterId,
	_App.ATSResumeId ,
	_ATSR.UploadedFileName as ResumeName,
	_ATSR.ProfileId,
	_ATSR1.UploadedFileName as CoverLetterName,
	_PRO.ProfileName as ProfileName
    from Application _APP 
    left join  ATSResume _ATSR on _APP.ATSResumeId = _ATSR.ATSResumeId  and _ATSR.IsDelete = 0
	left join  ATSResume _ATSR1 on _APP.ATSCoverLetterId = _ATSR1.ATSResumeId and _ATSR1.IsDelete = 0
	left join [Profile] _PRO on _PRO.ProfileId = @ProfileId and _PRO.IsDelete = 0
	where   _ATSR.UserId = @UserId and _APP.IsDelete = 0
	END
GO
/****** Object:  StoredProcedure [dbo].[ATS_ClientSolrDelta]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ATS_ClientSolrDelta] --'01-01-2014'
@UpdatedOn DateTime
AS
BEGIN

SELECT VacancyId from Vacancy where UpdatedOn + GETDATE() - GETUTCDATE() > @UpdatedOn  AND 
      (ShowOnWeb=1 OR UpdatedOn IS NULL) AND IsDelete=0


END
GO
/****** Object:  StoredProcedure [dbo].[ATS_ClientSolrDelete]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[ATS_ClientSolrDelete] --'01-01-2014'
@UpdatedOn DateTime
AS
BEGIN

SELECT VacancyId from Vacancy where UpdatedOn + GETDATE() - GETUTCDATE() > @UpdatedOn  AND 
      (ShowOnWeb=1 AND IsDelete=1)


END
GO
/****** Object:  Table [dbo].[Associations]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Associations](
	[AssociationsId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[Name] [nvarchar](100) NULL,
	[AssociationType] [nvarchar](100) NULL,
	[Link] [nvarchar](100) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Role] [nvarchar](100) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Associations] PRIMARY KEY CLUSTERED 
(
	[AssociationsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdHocPrivilege]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdHocPrivilege](
	[AdHocPrivilegeId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ATSPrivilegeId] [uniqueidentifier] NOT NULL,
	[PermissionType] [nvarchar](100) NOT NULL,
	[Scope] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_AdHocPrivilege] PRIMARY KEY CLUSTERED 
(
	[AdHocPrivilegeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Achievement]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievement](
	[AchievementId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[IssuingAuthority] [nvarchar](256) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Achievement] PRIMARY KEY CLUSTERED 
(
	[AchievementId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Availability]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Availability](
	[AvailibilityId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[DateAvailability] [datetime] NULL,
	[TargetIncome] [decimal](10, 2) NULL,
	[HrsMon] [nvarchar](100) NULL,
	[HrsTue] [nvarchar](100) NULL,
	[HrsWed] [nvarchar](100) NULL,
	[HrsThu] [nvarchar](100) NULL,
	[HrsFri] [nvarchar](100) NULL,
	[HrsSat] [nvarchar](100) NULL,
	[HrsSun] [nvarchar](100) NULL,
	[EmploymentPreference] [int] NULL,
	[WillingToWorkOverTime] [bit] NULL,
	[RelativesWorkingInCompany] [bit] NULL,
	[RelativesIfYes] [nvarchar](100) NULL,
	[SubmittedApplicationBefore] [bit] NULL,
	[AppicationSubmision] [nvarchar](256) NULL,
	[HearAboutThePosition] [nvarchar](100) NULL,
	[ReferredBy] [nvarchar](100) NULL,
	[HowOld] [bit] NULL,
	[EligibleToWorkInUS] [bit] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NULL,
 CONSTRAINT [PK_Availability] PRIMARY KEY CLUSTERED 
(
	[AvailibilityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmploymentHistory]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteEmploymentHistory]
@EmployementHistoryId uniqueidentifier

As
Begin

Delete from dbo.EmploymentHistory
Where EmployementHistoryId = @EmployementHistoryId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteEducationHistory]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteEducationHistory]
@EducationHistoryId uniqueidentifier

As
Begin

Delete from dbo.EducationHistory
Where EducationHistoryId = @EducationHistoryId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteDivisionPositionTypeByDivisionId]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 18-06-2014
-- Description:	Delete Selected Position Type By DivisionId
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDivisionPositionTypeByDivisionId] 
@DivisionId uniqueidentifier
AS
BEGIN
	Delete from DivisionPositionType where DivisionId = @DivisionId 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteDivisionByUserAndClient]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 27-03-2014
-- Description:	Delete exsisting division from UserDivisionPermission table
-- =============================================
CREATE PROCEDURE [dbo].[DeleteDivisionByUserAndClient]
	@ClientId uniqueidentifier,
	@UserId uniqueidentifier
AS
BEGIN
	delete from UserDivisionPermission where UserId = @UserId and ClientId = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllEmploymentHistory]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllEmploymentHistory]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM EmploymentHistory 
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllEducationHistory]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllEducationHistory]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM EducationHistory 
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  Table [dbo].[Objective]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Objective](
	[ObjectiveId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[ObjectiveDetails] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdateOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Objectives] PRIMARY KEY CLUSTERED 
(
	[ObjectiveId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenceAndCertifications]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenceAndCertifications](
	[LicenceAndCertificationsId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[IssuingAuthority] [nvarchar](256) NULL,
	[Description] [nvarchar](max) NULL,
	[ValidFrom] [datetime] NULL,
	[ValidTo] [datetime] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_LicenceAndCertifications] PRIMARY KEY CLUSTERED 
(
	[LicenceAndCertificationsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguagesId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[LanguageCode] [nvarchar](50) NULL,
	[Read] [bit] NOT NULL,
	[Write] [bit] NOT NULL,
	[Speak] [bit] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[LanguagesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetEmploymentHistoryByProfileId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEmploymentHistoryByProfileId]
@ProfileId uniqueidentifier,
@IsDelete bit

AS
BEGIN

SELECT [EmployementHistoryId]
      ,[ProfileId]
      ,[UserId]
      ,[CompanyName]
      ,[MayWeContact]
      ,[SupervisorName]
      ,[Telephone]
      ,[Address]
      ,[City]
      ,[State]
      ,[Zip]
      ,[StartMonth]
           ,[EndMonth]
           ,[StartYear]
           ,[EndYear]
           ,[Experience]
           ,[JobCategory]
      ,[StartigPosition]
      ,[MostRecentPosition]
      ,[StartingPay]
      ,[EndingPay]
      ,[ReasonForLeaving]
      ,[DutiesAndResponsibilities]
      
	  
     
  FROM [dbo].[EmploymentHistory]
  WHERE [ProfileId] = @ProfileId and IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetEducationHistoryByProfileId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEducationHistoryByProfileId]
@ProfileId uniqueidentifier,
@IsDelete bit


AS
BEGIN

SELECT [EducationHistoryId]
      ,[ProfileId]
      ,[UserId]
      ,[InstitutionName]
      ,[StartDate]
      ,[EndDate]
      ,[DegreeType]
      ,[MajorSubject]
      ,[City]
      ,[State]
      ,[Country]
      ,[Description]
	  ,[DegreeDate]
	  ,[MeasureSystem]
	  ,[MeasureValue]
      
	  
	   FROM [dbo].[EducationHistory]

	   WHERE [ProfileId] = @ProfileId and IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetdocumentsByUser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetdocumentsByUser] --'295E4FCB-D23D-4EE0-BE4F-AAE5DD999971'
@UserId uniqueidentifier,
@IsDelete bit
AS
BEGin

SELECT DISTINCT  AR.[ATSResumeId] 
      ,AR.[ProfileId]
      ,AR.[UserId]
      ,AR.[IsCoverLetter]
      ,AR.[Details]
      ,AR.[UploadedFileName]
      ,AR.[NewFileName]
      ,AR.[Path]
      ,AR.[TitleName]
      ,AR.[IsDelete]
      ,AR.[CreatedBy]
      ,AR.[CreatedOn]
      ,AR.[UpdatedOn]
      ,PR.[IsDefault]
      ,CASE 
         WHEN AP.ApplicationId is null And AP1.ApplicationId is null THEN CAST(0 as bit)
         ELSE CAST(1 as bit)
      END as IsApplied
      
           
  FROM [dbo].[ATSResume] AR
  left Join Profile PR on PR.ProfileId = AR.ProfileId  
  left join Application AP on AP.ATSResumeId = AR.ATSResumeId
  left join Application AP1 on AP1.ATSCoverLetterId = AR.ATSResumeId
  
where AR.[UserId] = @UserId AND AR.[IsDelete]  = 0

END
GO
/****** Object:  StoredProcedure [dbo].[GetDivisionPositionTypeById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDivisionPositionTypeById] --'DC7674D6-D49B-413E-8F23-28B506CEB695','DF9C2730-08DE-467E-BC8D-3D065FE1F855','33795F80-57D9-43C6-A466-03E816180694',0
@DivisionPositionTypeId uniqueidentifier,
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit
AS
BEGIN

select _DP.DivisionPositionTypeId,
       _DP.ClientId,
       _DP.DivisionId,
       _DP.PositionTypeId,
       _EL.LocalName as DivisionName,
       _EL1.LocalName as PositionTypeName
       From DivisionPositiontype _DP 
       inner Join Division _DIV on _DP.DivisionId = _DIV.DivisionId AND _DP.ClientId = @ClientId  AND _DP.DivisionPositionTypeId = @DivisionPositionTypeId And _DP.IsDelete =@IsDelete 
       Inner Join EntityLanguage _EL on _DP.DivisionId = _El.RecordId And _EL.LanguageId = @LanguageId And _EL.IsDelete = @IsDelete 
        inner Join PositionType _PT  on _DP.PositionTypeId = _PT.PositionTypeId AND _DP.ClientId = @ClientId And _PT.IsDelete = @IsDelete   
       Inner Join EntityLanguage _EL1 on _DP.PositiontypeId = _EL1.RecordId And _EL1.LanguageId = @LanguageId And _EL1.IsDelete = @IsDelete 
       
       END
GO
/****** Object:  StoredProcedure [dbo].[GetDivisionByUserAndClient]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 27-03-2014
-- Description:	Get all Division from UserDivisionPermission table by UserId and ClientId
-- =============================================
CREATE PROCEDURE [dbo].[GetDivisionByUserAndClient]
	@UserId uniqueidentifier,
	@ClientId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
  select DivisionId
  from UserDivisionPermission UDP
  where UDP.ClientId = @ClientId and UDP.UserId = @UserId and UDP.IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetJobLocationByDivision]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 01-04-2014
-- Description:	Get Job lOcation from JobLocation table by User Id and Division Id
-- =============================================
CREATE PROCEDURE [dbo].[GetJobLocationByDivision]
	@UserId uniqueidentifier,
	@DivisionId uniqueidentifier,
	@LanguageId uniqueidentifier,
	@IsDelete bit
AS
declare @IsActive bit = 1
BEGIN
	select distinct JL.JobLocationId,JL.DefaultValue,EL.LocalName,JL.IsActive 
	from DivisionPositionType DIVPOS
	inner join JobLocation JL on JL.DivisionId = DIVPOS.DivisionId and JL.IsDelete = @IsDelete and DIVPOS.IsDelete = @IsDelete and JL.IsActive = @IsActive
		inner join EntityLanguage EL on EL.RecordId = JL.JobLocationId and EL.LanguageId = @LanguageId And EL.IsDelete = @IsDelete 
					and DIVPOS.DivisionId = @DivisionId and JL.IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetJsonEmploymentHistory]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--[dbo].[GetJsonEmploymentHistory] '58CA38AC-DA88-42E3-B189-5E70C374421B'
CREATE proc [dbo].[GetJsonEmploymentHistory]
(
@ProfileId uniqueidentifier
)
AS
Begin
select '[' + STUFF((
        select 
            ',{"ProfileId":' + cast(ProfileId as varchar(max))
            + ',"CompanyName":"' + CompanyName + '"'
            + ',"City":"' + City + '"'
            + ',"DutiesAndResponsibilities":"' + DutiesAndResponsibilities + '"'
            
            +'}'

        from EmploymentHistory where ProfileId = @ProfileId
        for xml path(''), type
    ).value('.', 'varchar(max)'), 1, 1, '') + ']' as EmployeeHistory 
END
GO
/****** Object:  StoredProcedure [dbo].[GetLastUpdatedProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetLastUpdatedProfile]
@UserId uniqueidentifier,
@IsDelete bit
AS
Begin

Select [ProfileId] from [dbo].[Profile] where [UserId] = @UserId And IsDefault = 1 and IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetProfilesByUser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProfilesByUser]
@UserId uniqueidentifier,
@Isdelete bit

AS
BEGIN


Select [ProfileId]
      ,[LanguageId]
      ,[UserId]
      ,[ProfileName]
	  ,[VersionNo]
	  ,[Objectives]
      ,[Hobbies]
      ,[Category]
      ,[SubCategory]
      ,[CurrentSalary]
      ,[ExpectedSalary]
      ,[IsDefault] 
     
  FROM [dbo].[Profile]
  WHERE [UserId] = @UserId and IsDelete = @Isdelete

END
GO
/****** Object:  StoredProcedure [dbo].[GetProfileByProfileId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProfileByProfileId]
@ProfileId uniqueidentifier,
@IsDelete bit
AS
BEGIN




Select [ProfileId]
      ,[UserId]
      ,[ProfileName]
	  ,[VersionNo]
	  ,[Objectives]
      ,[Hobbies]
      ,[Category]
      ,[SubCategory]
      ,[CurrentSalary]
      ,[ExpectedSalary]
      ,[IsDefault]
     
  FROM [dbo].[Profile]
  WHERE [ProfileId] = @ProfileId and IsDelete = 0
  end
GO
/****** Object:  StoredProcedure [dbo].[GetPositionTypeByDivision]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPositionTypeByDivision]
	@UserId uniqueidentifier,
	@DivisionId uniqueidentifier,
	@LanguageId uniqueidentifier,
	@IsDelete bit
AS
declare @IsActive bit = 1
BEGIN
	select distinct DIVPOS.PositionTypeId,POS.DefaultName,EL.LocalName,POS.IsActive 
	from DivisionPositionType DIVPOS
			inner join PositionType POS on POS.PositionTypeId = DIVPOS.PositionTypeId and POS.IsDelete = @IsDelete and DIVPOS.IsDelete = @IsDelete and POS.IsActive = @IsActive
			inner join EntityLanguage EL on EL.RecordId = DIVPOS.PositionTypeId and EL.LanguageId = @LanguageId  And EL.IsDelete =@IsDelete 
					and DivisionId = @DivisionId  
 END
GO
/****** Object:  StoredProcedure [dbo].[GetSearchQuery]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSearchQuery]
	@SearchQueryId uniqueidentifier
AS
BEGIN
	select SaveResumeSearchId,IsDefault,JsonQuery,SearchQueryName 
	from SaveResumeSearch where SaveResumeSearchId = @SearchQueryId
	And SaveResumeSearch.IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetSaveSearchQueryByUserId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSaveSearchQueryByUserId]
	@UserId uniqueidentifier
AS
declare @IsDefault bit = 1
BEGIN
   select SaveResumeSearchId,JsonQuery,SearchQueryName,IsDefault 
   from SaveResumeSearch 
   where UserId = @UserId And IsDefault = @IsDefault And IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserDivisionPermissionById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserDivisionPermissionById]

@UserDivisionPermissionId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit
AS
BEGIN
select 
       _UDP.UserDivisionPermissionId as 'UserDivisionPermissionId'
       ,_UDP.DivisionId as 'DivisionId'
       ,_UDP.UserId as 'UserId'
       ,_UDp.ClientId as 'ClientId'
       ,_UD.FirstName
       ,_EL.LocalName
       from UserDivisionPermission _UDP 
       inner join UserDetails _UD on _UDP.UserId = _UD.UserId And _UDP.IsDelete = @IsDelete 
       inner join entitylanguage _EL on _UDP.DivisionId = _EL.RecordId and _EL.LanguageId = @LanguageId And _EL.IsDelete =@IsDelete 
       where _UDP.UserDivisionPermissionId = @UserDivisionPermissionId AND _UDP.Isdelete = @IsDelete 
       
       
       END
GO
/****** Object:  StoredProcedure [dbo].[GetUserDivisionPermission]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserDivisionPermission]--'0A007CAD-B148-429C-8C46-4758324954E8',0
@UserId uniqueidentifier,
@IsDelete bit
AS
BEGIN
SELECT _Division.[DivisionId]
      ,_Division.[ClientId]
      ,_Division.[ParentDivisionId]
     
  FROM [dbo].[UserDivisionPermission]  _UserDivisionPermission
	inner join Division _Division On _UserDivisionPermission.DivisionId = _Division.DivisionId And _Division.IsDelete = @IsDelete 
  WHERE _UserDivisionPermission.IsDelete = @IsDelete  AND _UserDivisionPermission.UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserDetailsByUserId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserDetailsByUserId] --'17120BF4-7DC1-4B14-AAEB-E74AF12E6C64','DF9C2730-08DE-467E-BC8D-3D065FE1F855'
	@UserId uniqueidentifier,
	@IsDelete bit 
	AS

BEGIN 

select 
[UserId],
[HomeEmail],
[FirstName],
[MiddleName],
[LastName],
[Affix],
[Fax],
[WebSite],
[PostOfficeBox],
[Address],
[City],
[State],
[Zip],
[BusinessPhoneNo],
[HomePhone],
[MobilePhone],
[Pager],
[WorkEmail],
[Misdemeanor]
,[MisdemeanorExplain]
 ,[MilitaryService]
 ,[MilitaryTypeDischarge]
 ,[EmergencyContact1]
 ,[EmergencyContact2]
 ,[EmergencyContact1Phone]
 ,[EmergencyContact2Phone]
 ,[IsDelete]   
      
from [dbo].[UserDetails] where 
[UserId] = @UserId  And Isdelete = @IsDelete 
 
 END
GO
/****** Object:  StoredProcedure [dbo].[GetSelectedPositionTypeByDivisionId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 18-06-2014
-- Description:	Get selected position type by division Id
-- =============================================
CREATE PROCEDURE [dbo].[GetSelectedPositionTypeByDivisionId] 
@DivisionId uniqueidentifier,
@IsDelete bit
AS
BEGIN
	select PositionTypeId
	from DivisionPositionType
where DivisionId = @DivisionId and IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[InsertEmploymentHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertEmploymentHistory]
@EmployementHistoryId uniqueidentifier OUT,
@ProfileId uniqueidentifier,
@UserId uniqueidentifier,
@CompanyName nvarchar(50) = null,
@MayWeContact bit ,
@SupervisorName nvarchar(50) = null,
@Telephone nvarchar(11) = null,
@Address nvarchar(200)= null,
@City nvarchar(50) = null,
@State nvarchar(50)= null,
@Zip nvarchar(50)= null,
@StartMonth int = null,
@EndMonth int  = null,
@StartYear int = null,
@EndYear int = null,
@Experience int = null,
@JobCategory nvarchar(100) = null,
@StartingPosition nvarchar(50) = null,
@MostRecentPosition nvarchar(50) = null,
@StartingPay decimal(18,0)=null,
@EndingPay decimal(18,0)= null ,
@ReasonForLeaving nvarchar(max) = null,
@DutiesAndResponsibilities nvarchar(max) = null,

@IsDelete bit,
@UpdatedOn datetime = null,
@CreatedOn datetime = null,
@UpdatedBy uniqueidentifier = null,
@CreatedBy uniqueidentifier = null

AS
Begin 
SET @EmployementHistoryId = newid();
INSERT INTO [dbo].[EmploymentHistory]
           ([EmployementHistoryId]
		   ,[ProfileId]
           ,[UserId]
           ,[CompanyName]
           ,[MayWeContact]
           ,[SupervisorName]
           ,[Telephone]
           ,[Address]
           ,[City]
           ,[State]
           ,[Zip]
           ,[StartMonth]
           ,[EndMonth]
           ,[StartYear]
           ,[EndYear]
           ,[Experience]
           ,[JobCategory]
           ,[StartigPosition]
           ,[MostRecentPosition]
           ,[StartingPay]
           ,[EndingPay]
           ,[ReasonForLeaving]
           ,[DutiesAndResponsibilities]
           
           ,[IsDelete]
           ,[UpdatedOn]
           ,[createdOn]
           ,[UpdatedBy]
           ,[CreatedBy])
     VALUES
	 (
	 @EmployementHistoryId ,
	 @ProfileId,
@UserId,
@CompanyName,
@MayWeContact,
@SupervisorName,
@Telephone,
@Address,
@City,
@State,
@Zip,
@StartMonth,
@EndMonth,
@StartYear,
@EndYear,
@Experience,
@JobCategory,
@StartingPosition,
@MostRecentPosition,
@StartingPay,
@EndingPay,
@ReasonForLeaving,
@DutiesAndResponsibilities,

@IsDelete,
@UpdatedOn,
@CreatedOn,
@UpdatedBy,
@CreatedBy)

End
GO
/****** Object:  StoredProcedure [dbo].[InsertEducationHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[InsertEducationHistory]
@EducationHistoryId uniqueidentifier OUT,
@ProfileId uniqueidentifier,
@UserId uniqueidentifier,
@InstitutionName nvarchar(100),
@StartDate datetime,
@EndDate datetime,
@DegreeType uniqueidentifier,
@MajorSubject nvarchar(100),
@City nvarchar(100),
@State nvarchar(100),
@Country nvarchar(50),
@Description nvarchar(50),
@IsDelete bit,
@DegreeDate datetime,
@MeasureSystem nvarchar(50),
@MeasureValue nvarchar(50),

@CreatedOn datetime = null,

@CreatedBy uniqueidentifier = null
AS
BEGIN

set @EducationHistoryId= newid();

INSERT INTO [dbo].[EducationHistory]
           ([EducationHistoryId]
		   ,[ProfileId]
           ,[UserId]
           ,[InstitutionName]
           ,[StartDate]
           ,[EndDate]
           ,[DegreeType]
           ,[MajorSubject]
           ,[City]
           ,[State]
           ,[Country]
           ,[Description]
           ,[IsDelete]
           ,[DegreeDate]
           ,[MeasureSystem]
           ,[MeasureValue]
           ,[CreatedOn]
           
           ,[CreatedBy])
Values
(@EducationHistoryId,
@ProfileId,
@UserId,
@InstitutionName,
@StartDate,
@EndDate,
@DegreeType,
@MajorSubject,
@City,
@State,
@Country,
@Description,
@IsDelete,
@DegreeDate,
           @MeasureSystem,
           @MeasureValue,

@CreatedOn,

@CreatedBy
)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertDivisionPositionType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDivisionPositionType]

@DivisionPositionTypeId uniqueidentifier OUT,
@ClientId uniqueidentifier,
@DivisionId uniqueidentifier,
@PositionTypeId uniqueidentifier,
@IsDelete bit,
@CreatedBy uniqueidentifier,
@CreatedOn datetime
AS
Begin


Set @DivisionPositionTypeId = newId();




INSERT INTO[dbo].[DivisionPositionType]
           ([DivisionPositionTypeId]
           ,[ClientId]
           ,[DivisionId]
           ,[PositionTypeId]
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy]
          )
     VALUES
           (@DivisionPositionTypeId 
           ,@ClientId
           ,@DivisionId
           ,@PositionTypeId
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy)
                                 
                      END
GO
/****** Object:  StoredProcedure [dbo].[InsertBlockCandidate]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertBlockCandidate]

@BlockCandidateId uniqueidentifier out,
@UserId uniqueidentifier,
@IsDelete bit,
@CreatedBy uniqueidentifier,
@CreatedOn datetime
          
          
           AS
           BEGIN
set @BlockCandidateId =NEWID()
INSERT INTO [dbo].[BlockCandidate]
           ([BlockCandidateId]
           ,[UserId]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
           (@BlockCandidateId
           ,@UserId
           ,@IsDelete
           ,@CreatedBy
           ,@CreatedOn
           )
end
GO
/****** Object:  StoredProcedure [dbo].[GetVacancyStatusById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVacancyStatusById]
@VacancyStatusId uniqueidentifier,
@IsDelete bit
AS
BEGIN
SELECT [VacancyStatusId]
      ,[UserId]
      ,[ClientId]
      ,[VacancyStatusText]
      ,[Category]            
      
      
      
  FROM [dbo].[VacancyStatus]
  where [VacancyStatusId] = @VacancyStatusId And IsDelete = @IsDelete 

END
GO
/****** Object:  StoredProcedure [dbo].[GetVacancyByPublicCodeId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVacancyByPublicCodeId] --'bdb81ebd-ef20-40fa-9c82-57d917157609','df9c2730-08de-467e-bc8d-3d065fe1f855','33795f80-57d9-43c6-a466-03e816180694',0
@PublicCode bigint,
@IsDelete bit

AS
BEGiN
SELECT _VAN.[VacancyId]
      ,_VAN.[ClientId]
      ,_VAN.[LanguageId]
      ,_VAN.[PositionTypeId]
      ,_VAN.[JobTitle]
      ,_VAN.[PositionID]
      ,_VAN.[VacancyStatusId]
      ,_VAN.[JobType]
      ,_VAN.[EmploymentType]
      ,_VAN.[DivisionId]
      ,_VAN.[Location]
      ,_VAN.[StartDate]
      ,_VAN.[EndDate]
      ,_VAN.[TotalPositions]
      ,_VAN.[RemainingPositions]
      ,_VAN.[ShowOnWeb]
      ,_VAN.[FeaturedOnWeb]
      ,_VAN.[PositionRequestedBy]
      ,_VAN.[PositionOwner]
      ,_VAN.[JobDescription]
      ,_VAN.[PostedOn]
      ,_VAN.[ShowOnWebJobDescription]
      ,_VAN.[DutiesAndResponsibilities]
      ,_VAN.[ShowOnWebDuties]
      ,_VAN.[SkillsAndQualification]
      ,_VAN.[ShowOnWebSkills]
      ,_VAN.[Benefits]
      ,_VAN.[ShowOnWebBenefits]
      ,_VAN.[SalaryMin]
      ,_VAN.[SalaryMax]
      ,_VAN.[ShowOnWebSal]
      ,_VAN.[HourlyMin]
      ,_VAN.[HourlyMax]
      ,_VAN.[ShowonWebHour]
      ,_VAN.[Commission]
      ,_VAN.[ShowOnWebCommission]
      ,_VAN.[BonusPotential]
      ,_VAN.[ShowOnWebBonus]
      ,_VAN.[IsDelete]
      ,_VAN.[VacancyState]
      ,_VAN.[CreatedBy]
      ,_VAN.[CreatedOn]
      ,_VAN.[UpdatedBy]
      ,_VAN.[UpdatedOn]
      ,_VAN.[VersionNo]
      ,_VAN.[PublicCode]
      ,_ENTL1.LocalName as 'VacancyStatusText'
	  ,_DRP1.[Text] as 'JobTypeText'
	  ,_DRP2.[Text] as 'EmploymentTypeText'
	  ,_EL.LocalName as 'PositionTypeName'
	  ,_EL1.LocalName as 'LocationText'
  FROM [dbo].[Vacancy] _VAN
         left join EntityLanguage _EL on _VAN.PositionTypeId = _EL.RecordId and _EL.LanguageId = _VAN.LanguageId and _EL.IsDelete = @IsDelete 
         left join EntityLanguage _EL1 on _EL1.RecordId = _VAN.Location and _EL.LanguageId = _VAN.LanguageId and _EL1.IsDelete = @IsDelete 
		 inner join  [DrpStringMapping] _DRP1 on _VAN.[JobType] = _DRP1.Value AND _DRP1.FormName = 'Vacancy' AND _DRP1.DrpName = 'JobType' 
																			AND _DRP1.LanguageId = _VAN.LanguageId And _DRP1.IsDelete =@IsDelete
		 inner join  [DrpStringMapping] _DRP2 on _VAN.[EmploymentType] = _DRP2.Value AND _DRP2.FormName = 'Vacancy' AND _DRP2.DrpName = 'EmploymentType' 
																					AND _DRP2.LanguageId = _VAN.LanguageId And _DRP2.IsDelete= @IsDelete
		inner join [EntityLanguage] _ENTL1 on _VAN.VacancyStatusId = _ENTL1.RecordId AND _ENTL1.LanguageId = _VAN.LanguageId
  Where _VAN.PublicCode = @PublicCode And _VAN.IsDelete =@IsDelete 
  END
GO
/****** Object:  StoredProcedure [dbo].[GetVacancyById]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVacancyById] --'bdb81ebd-ef20-40fa-9c82-57d917157609','df9c2730-08de-467e-bc8d-3d065fe1f855','33795f80-57d9-43c6-a466-03e816180694',0
@VacancyId uniqueidentifier,
@ClientId uniqueidentifier,
@LanguageId uniqueidentifier,
@IsDelete bit

AS
BEGiN
SELECT _VAN.[VacancyId]
      ,_VAN.[ClientId]
      ,_VAN.[LanguageId]
      ,_VAN.[PositionTypeId]
      ,_VAN.[JobTitle]
      ,_VAN.[PositionID]
      ,_VAN.[VacancyStatusId]
      ,_VAN.[JobType]
      ,_VAN.[EmploymentType]
      ,_VAN.[DivisionId]
      ,_VAN.[Location]
      ,_VAN.[StartDate]
      ,_VAN.[EndDate]
      ,_VAN.[TotalPositions]
      ,_VAN.[RemainingPositions]
      ,_VAN.[ShowOnWeb]
      ,_VAN.[FeaturedOnWeb]
      ,_VAN.[PositionRequestedBy]
      ,_VAN.[PositionOwner]
      ,_VAN.[JobDescription]
      ,_VAN.[PostedOn]
      ,_VAN.[ShowOnWebJobDescription]
      ,_VAN.[DutiesAndResponsibilities]
      ,_VAN.[ShowOnWebDuties]
      ,_VAN.[SkillsAndQualification]
      ,_VAN.[ShowOnWebSkills]
      ,_VAN.[Benefits]
      ,_VAN.[ShowOnWebBenefits]
      ,_VAN.[SalaryMin]
      ,_VAN.[SalaryMax]
      ,_VAN.[ShowOnWebSal]
      ,_VAN.[HourlyMin]
      ,_VAN.[HourlyMax]
      ,_VAN.[ShowonWebHour]
      ,_VAN.[Commission]
      ,_VAN.[ShowOnWebCommission]
      ,_VAN.[BonusPotential]
      ,_VAN.[ShowOnWebBonus]
      ,_VAN.[IsDelete]
      ,_VAN.[VacancyState]
      ,_VAN.[CreatedBy]
      ,_VAN.[CreatedOn]
      ,_VAN.[UpdatedBy]
      ,_VAN.[UpdatedOn]
      ,_VAN.[VersionNo]
      ,_VAN.[PublicCode]
      ,_ENTL1.LocalName as 'VacancyStatusText'
	  ,_DRP1.[Text] as 'JobTypeText'
	  ,_DRP2.[Text] as 'EmploymentTypeText'
	  ,_EL.LocalName as 'PositionTypeName'
	  ,_EL1.LocalName as 'LocationText'
  FROM [dbo].[Vacancy] _VAN
         left join EntityLanguage _EL on _VAN.PositionTypeId = _EL.RecordId and _EL.LanguageId = @LanguageId And _EL.IsDelete = @IsDelete 
         left join EntityLanguage _EL1 on _EL1.RecordId = _VAN.Location and _EL.LanguageId = @LanguageId And _EL1.IsDelete  =@IsDelete 
		 inner join  [DrpStringMapping] _DRP1 on _VAN.[JobType] = _DRP1.Value AND _DRP1.FormName = 'Vacancy' AND _DRP1.DrpName = 'JobType' 
																				AND _DRP1.LanguageId = @LanguageId And _DRP1.IsDelete =@IsDelete 
		 inner join  [DrpStringMapping] _DRP2 on _VAN.[EmploymentType] = _DRP2.Value AND _DRP2.FormName = 'Vacancy' AND _DRP2.DrpName = 'EmploymentType' 
																					AND _DRP2.LanguageId = @LanguageId And _DRP2.IsDelete =@IsDelete 
		 inner join [EntityLanguage] _ENTL1 on _VAN.VacancyStatusId = _ENTL1.RecordId AND _ENTL1.LanguageId = @LanguageId
  Where _VAN.[VacancyId] = @VacancyId AND _VAN.[ClientId] = @ClientId AND _VAN.[LanguageId] = @LanguageId And _VAN.IsDelete = @IsDelete 
  END
GO
/****** Object:  StoredProcedure [dbo].[InsertSearch]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertSearch]
@SearchId uniqueidentifier OUT,
@ClientId uniqueidentifier,
@UserId uniqueidentifier,
@Location  nvarchar(256),
@EmploymentType  nvarchar(256),
@Position nvarchar(256),
@JobType nvarchar(100),
@SalMinRange decimal(10,2),
@SalMaxRange decimal(10,2),
@Keywords nvarchar(256),
@DateMinRange datetime,
@DateMaxRange datetime,
@LanguageId uniqueidentifier,
@IsDelete bit,
@CreatedOn datetime,
@CreatedBy uniqueidentifier

AS
BEGIN


SET @SearchId = newid()
IF exists (select 1 from Search where UserId = @UserId and LanguageId = @LanguageId)
begin
  Delete from Search where UserId = @UserId and LanguageId = @LanguageId
end
INSERT INTO [dbo].[Search]
           ([SearchId]
           ,[ClientId]
           ,[UserId]
           ,[Location]
		   ,[EmploymentType]
		   ,[Position]
           ,[JobType]
           ,[SalMinRange]
           ,[SalMaxRange]
           ,[KeyWords]
		   ,[DateMinRange]
           ,[DateMaxRange]
           ,[LanguageId]
           ,[IsDelete]
		   ,[VersionNo]
           ,[CreatedOn]
           ,[CreatedBy])
           
     VALUES
           (@SearchId
           ,@ClientId
           ,@UserId
           ,@Location
           ,@EmploymentType
		   ,@Position
           ,@JobType
           ,@SalMinRange
		   ,@SalMaxRange
           ,@Keywords
           ,@DateMinRange
		   ,@DateMaxRange
           ,@LanguageId
           ,@IsDelete
		   ,default
           ,@CreatedOn
           ,@CreatedBy)
           
END
GO
/****** Object:  StoredProcedure [dbo].[InsertSaveResumeSearch]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertSaveResumeSearch]

@SaveResumeSearchId uniqueidentifier output,
  @UserId uniqueidentifier,
  @JsonQuery nvarchar(max),
  @SearchQueryName nvarchar(256),
           @IsDefault bit,
           @IsDelete bit,
           @CreatedOn datetime,
           @CreatedBy uniqueidentifier
        AS
        Begin
        
if @IsDefault = 1
begin
   update SaveResumeSearch
   set IsDefault = 0 
   where UserId = @UserId 
end

SET @SaveResumeSearchId = NEWID(); 
INSERT INTO [dbo].[SaveResumeSearch]
           ([SaveResumeSearchId]
           ,[UserId]
           ,[JsonQuery]
           ,[SearchQueryName]
           ,[IsDefault]
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy])
  
     VALUES
           (@SaveResumeSearchId
           ,@UserId
           ,@JsonQuery
           ,@SearchQueryName
           ,@IsDefault
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy         
           )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertProfile]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertProfile]
@ProfileId uniqueidentifier OUT
,@LanguageId uniqueidentifier
           ,@UserId uniqueidentifier
             ,@ClientId uniqueidentifier
           ,@ProfileName nvarchar(100)
		    ,@Objectives nvarchar(MAX)= null
			,@Hobbies nvarchar(250) = null
			,@Category nvarchar(100) = null
			,@SubCategory nvarchar(100) = null
			,@CurrentSalary decimal = null
			,@ExpectedSalary decimal = null
			,@IsDefault bit
			,@IsDelete bit
           ,@CreatedOn datetime = null
           ,@CreatedBy uniqueidentifier =null
           

AS 
BEGIN

Declare @IsDefaultCount as int 
select @IsDefaultCount =  COUNT(*) from Profile where IsDefault = 1 AND UserId = @UserId

if(@IsDefaultCount > 0)
begin 
    SET @IsDefault = 0 
    end
    else
    begin
    SET @IsDefault = 1
    end
  
SET @ProfileId = NEWID();

INSERT INTO [dbo].[Profile]
           ([ProfileId]
           ,[LanguageId]
           ,[ClientId]
           ,[UserId]
           ,[ProfileName]
		   ,[Objectives]
			,[Hobbies]
			,[Category]
			,[SubCategory]
			,[CurrentSalary] 
			,[ExpectedSalary]
			
			,[IsDefault] 
			,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy]
           )
     VALUES
           (@ProfileId
           ,@LanguageId
           ,@ClientId
           ,@UserId
           ,@ProfileName
		   ,@Objectives
		   ,@Hobbies
		   ,@Category
		   ,@SubCategory
		   ,@CurrentSalary 
		   ,@ExpectedSalary 
			,@IsDefault
		     ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy
           )


		   END
GO
/****** Object:  StoredProcedure [dbo].[GetAtsResumedetailsByVacancyAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetAtsResumedetailsByVacancyAndLanguage] --'9ee1f4eb-6fb4-4f1b-907c-d70473a56324','33795F80-57D9-43C6-A466-03E816180694',0
@VacancyId uniqueidentifier, 
@LanguageId uniqueidentifier,
@IsDelete bit,
@UserId uniqueidentifier 
AS
BEGIN

select 
	 _APP.VacancyId,
	 _APP.CreatedOn as AppliedOn,
	_App.ATSCoverLetterId as CoverLetterId,
	_App.ATSResumeId ,
	_ATSR.UploadedFileName as ResumeName,
	_ATSR.ProfileId,
	_ATSR1.UploadedFileName as CoverLetterName,
	_PRO.ProfileName as ProfileName
    from Application _APP 
    left join  ATSResume _ATSR on _APP.ATSResumeId = _ATSR.ATSResumeId And _ATSR.IsDelete = @IsDelete
	left join  ATSResume _ATSR1 on _APP.ATSCoverLetterId = _ATSR1.ATSResumeId And _ATSR1.IsDelete =@IsDelete
	left join [Profile] _PRO on _PRO.ProfileId = _ATSR.ProfileId And _PRO.IsDelete = @IsDelete 
	where  _APP.VacancyId = @VacancyId and _APP.LanguageId = @LanguageId and _ATSR.UserId = @UserId
		And _APP.IsDelete = @IsDelete 
	END
GO
/****** Object:  Table [dbo].[Reference]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reference](
	[ReferenceId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ReferenceName] [nvarchar](100) NULL,
	[Relationship] [nvarchar](100) NULL,
	[ReferencePhone] [nvarchar](100) NULL,
	[ReferenceEmail] [nvarchar](100) NULL,
	[IsDelete] [bit] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Reference] PRIMARY KEY CLUSTERED 
(
	[ReferenceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[RecordExistsForVacancyStatus]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordExistsForVacancyStatus] --'DE82A681-D1F0-43DA-AEB0-1DBC5C4AD817'
@VacancyStatusId uniqueidentifier,
@IsDelete bit
As
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [TableName] nvarchar(256)
	  ,[Count] int
)
Declare @Count as int 
Begin
Select @Count =  COUNT(*)  from [dbo].Vacancy where VacancyStatusId = @VacancyStatusId and IsDelete=0
insert into @tbl ([TableName],[Count]) values('Vacancy',@Count)
select * from @tbl

End
GO
/****** Object:  Table [dbo].[PublicationHistory]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PublicationHistory](
	[PublicationHistoryId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Type] [nvarchar](100) NULL,
	[Role] [nvarchar](100) NULL,
	[Name] [nvarchar](100) NULL,
	[PublicationDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[Comments] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_PublicationHistory] PRIMARY KEY CLUSTERED 
(
	[PublicationHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectId] [uniqueidentifier] NOT NULL,
	[EmploymentHistoryId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[ProjectName] [nvarchar](100) NOT NULL,
	[TeamSize] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[InsertVacancyStatus]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertVacancyStatus]
@VacancyStatusId uniqueidentifier OUT
           ,@UserId uniqueidentifier
           ,@ClientId uniqueidentifier
           ,@VacancyStatusText nvarchar(100)
           ,@Category nvarchar(50)
           ,@IsDelete bit
           ,@CreatedOn datetime
           ,@FieldValue nvarchar(100)
           ,@IsError int OUTPUT
           ,@CreatedBy uniqueidentifier
           
           AS 
           declare @Result bit = 0
           declare @TableName varchar(50) = 'VacancyStatus'
           Set @IsError = 0
           BEGIN
           Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output
           
           IF @Result = 0
           begin
           SET @VacancyStatusId = NEWID();

         INSERT INTO [dbo].[VacancyStatus]
           ([VacancyStatusId]
           ,[UserId]
           ,[ClientId]
           ,[VacancyStatusText]
           ,[Category]
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (@VacancyStatusId
           ,@UserId
           ,@ClientId
           ,@VacancyStatusText
           ,@Category
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy)
           
END
else
begin
		Set @IsError = 101 -- Vacancystatus already exists
end
END
GO
/****** Object:  StoredProcedure [dbo].[InsertVacancyByLanguage]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertVacancyByLanguage]    
@VacancyLanguageId uniqueidentifier OUT,    
@VacancyId uniqueidentifier,    
@ClientId uniqueidentifier,    
@LanguageId uniqueidentifier,    
@PositionTypeId uniqueidentifier,    
@JobTitle nvarchar(100),    
@PostedOn datetime,    
@PositionID nvarchar(100),    
@VacancyStatusId uniqueidentifier,    
@JobType int,    
@EmploymentType int,    
@DivisionId uniqueidentifier,    
@Location nvarchar(100),    
@StartDate datetime,    
@VacancyState int,    
@EndDate datetime,    
@TotalPositions int,    
@RemainingPositions int,    
@ShowOnWeb bit,    
@FeaturedOnWeb bit,    
@PositionRequestedBy uniqueidentifier,    
@PositionOwner uniqueidentifier,    
@JobDescription nvarchar(max)= null,    
@ShowOnWebJobDescription bit,    
@DutiesAndResponsibilities nvarchar(max)= null,    
@ShowOnWebDuties bit,    
@SkillsAndQualification nvarchar(max)= null,    
@ShowOnWebSkills bit,    
@Benefits nvarchar(max) = null,    
@ShowOnWebBenefits bit,    
@SalaryMin decimal(10,2),    
@SalaryMax decimal(10,2)= null,    
@ShowOnWebSal bit,    
@HourlyMin decimal(10,2)= null,    
@HourlyMax decimal(10,2)= null,    
@ShowonWebHour bit,    
--@Commission decimal(10,2)= null,    
@Commission nvarchar(max)= null,    
@ShowOnWebCommission bit,    
--@BonusPotential decimal(10,2)= null,    
@BonusPotential nvarchar(max)= null,    
@ShowOnWebBonus bit,    
@IsDelete bit= null,    
@CreatedBy uniqueidentifier= null,    
@CreatedOn datetime= null    
    
    
AS    
BEGIN    
SET @VacancyLanguageId = newid();    
INSERT INTO [dbo].[Vacancy]    
           ([VacancyId]    
           ,[ClientId]    
           ,[LanguageId]    
           ,[PositionTypeId]    
           ,[JobTitle]    
           ,[PostedOn]    
           ,[PositionID]    
           ,[VacancyStatusId]    
           ,[JobType]    
           ,[EmploymentType]    
           ,[DivisionId]    
           ,[Location]    
           ,[StartDate]    
           ,[EndDate]    
           ,[TotalPositions]    
           ,[RemainingPositions]    
           ,[ShowOnWeb]    
           ,[FeaturedOnWeb]    
           ,[PositionRequestedBy]    
           ,[PositionOwner]    
           ,[JobDescription]    
           ,[ShowOnWebJobDescription]    
           ,[DutiesAndResponsibilities]    
           ,[ShowOnWebDuties]    
           ,[SkillsAndQualification]    
           ,[ShowOnWebSkills]    
           ,[Benefits]    
           ,[ShowOnWebBenefits]    
           ,[SalaryMin]    
           ,[SalaryMax]    
           ,[ShowOnWebSal]    
           ,[HourlyMin]    
           ,[HourlyMax]    
           ,[ShowonWebHour]    
           ,[Commission]    
           ,[ShowOnWebCommission]    
           ,[BonusPotential]    
           ,[ShowOnWebBonus]    
           ,[IsDelete]    
           ,[VacancyState]    
           ,[CreatedBy]    
           ,[CreatedOn]  
           ,[PublicCode]  
           )    
     VALUES    
           (@VacancyId    
           ,@ClientId    
           ,@LanguageId    
           ,@PositionTypeId    
           ,@JobTitle    
           ,@PostedOn    
           ,@PositionID    
           ,@VacancyStatusId    
           ,@JobType    
           ,@EmploymentType    
           ,@DivisionId     
           ,@Location    
           ,@StartDate    
           ,@EndDate    
           ,@TotalPositions    
           ,@RemainingPositions    
           ,@ShowOnWeb    
           ,@FeaturedOnWeb    
           ,@PositionRequestedBy    
           ,@PositionOwner    
           ,@JobDescription    
           ,@ShowOnWebJobDescription    
           ,@DutiesAndResponsibilities    
           ,@ShowOnWebDuties    
           ,@SkillsAndQualification    
           ,@ShowOnWebSkills    
           ,@Benefits    
           ,@ShowOnWebBenefits    
           ,@SalaryMin    
           ,@SalaryMax    
           ,@ShowOnWebSal    
           ,@HourlyMin    
           ,@HourlyMax    
           ,@ShowonWebHour    
           ,@Commission    
           ,@ShowOnWebCommission    
           ,@BonusPotential    
           ,@ShowOnWebBonus    
           ,@IsDelete    
           ,@VacancyState    
           ,@CreatedBy    
           ,@CreatedOn    
           ,ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT))
          )    
    
    
    
END
GO
/****** Object:  StoredProcedure [dbo].[InsertVacancy]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertVacancy]  
@VacancyId uniqueidentifier OUT,  
@ClientId uniqueidentifier,  
@LanguageId uniqueidentifier,  
@PositionTypeId uniqueidentifier,  

@JobTitle nvarchar(100),  
@PostedOn datetime,  
@PositionID nvarchar(100),  
@VacancyStatusId uniqueidentifier,  
@JobType int,  
@EmploymentType int,  
@DivisionId uniqueidentifier,  
@Location uniqueidentifier,  
@StartDate datetime,  
@VacancyState int,  
@EndDate datetime,  
@TotalPositions int,  
@RemainingPositions int,  
@ShowOnWeb bit,  
@FeaturedOnWeb bit,  
@PositionRequestedBy uniqueidentifier,  
@PositionOwner uniqueidentifier,  
@JobDescription nvarchar(max)= null,  
@ShowOnWebJobDescription bit,  
@DutiesAndResponsibilities nvarchar(max)= null,  
@ShowOnWebDuties bit,  
@SkillsAndQualification nvarchar(max)= null,  
@ShowOnWebSkills bit,  
@Benefits nvarchar(max) = null,  
@ShowOnWebBenefits bit,  
@SalaryMin decimal(10,2),  
@SalaryMax decimal(10,2)= null,  
@ShowOnWebSal bit,  
@HourlyMin decimal(10,2)= null,  
@HourlyMax decimal(10,2)= null,  
@ShowonWebHour bit,  
--@Commission decimal(10,2)= null,  
@Commission nvarchar(max) = null,  
@ShowOnWebCommission bit,  
--@BonusPotential decimal(10,2)= null,  
@BonusPotential nvarchar(max) = null,  
@ShowOnWebBonus bit,  
@IsDelete bit= null,  
@CreatedBy uniqueidentifier= null,  
@CreatedOn datetime= null,  
@FieldValue nvarchar(max),  
@IsError int OUTPUT  
  
  
AS  
declare @Result bit = 0  
declare @TableName varchar(50) = 'Vacancy'  
Set @IsError = 0  
BEGIN  
  
Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output  
  
IF @Result = 0  
 begin  
SET @VacancyId = newid();  
INSERT INTO [dbo].[Vacancy]  
           ([VacancyId]  
           ,[ClientId]  
           ,[LanguageId]  
           ,[PositionTypeId]  
           
             
           ,[JobTitle]  
           ,[PostedOn]  
           ,[PositionID]  
           ,[VacancyStatusId]  
           ,[JobType]  
           ,[EmploymentType]  
           ,[DivisionId]  
           ,[Location]  
           ,[StartDate]  
           ,[EndDate]  
           ,[TotalPositions]  
           ,[RemainingPositions]  
           ,[ShowOnWeb]  
           ,[FeaturedOnWeb]  
           ,[PositionRequestedBy]  
           ,[PositionOwner]  
           ,[JobDescription]  
           ,[ShowOnWebJobDescription]  
           ,[DutiesAndResponsibilities]  
           ,[ShowOnWebDuties]  
           ,[SkillsAndQualification]  
           ,[ShowOnWebSkills]  
           ,[Benefits]  
           ,[ShowOnWebBenefits]  
           ,[SalaryMin]  
           ,[SalaryMax]  
           ,[ShowOnWebSal]  
           ,[HourlyMin]  
           ,[HourlyMax]  
           ,[ShowonWebHour]  
           ,[Commission]  
           ,[ShowOnWebCommission]  
           ,[BonusPotential]  
           ,[ShowOnWebBonus]  
           ,[IsDelete]  
           ,[VacancyState]  
           ,[CreatedBy]  
           ,[CreatedOn] 
           ,[PublicCode] 
           )  
     VALUES  
           (@VacancyId  
           ,@ClientId  
           ,@LanguageId  
           ,@PositionTypeId  
           
           ,@JobTitle  
           ,@PostedOn  
           ,@PositionID  
           ,@VacancyStatusId  
           ,@JobType  
           ,@EmploymentType  
           ,@DivisionId   
           ,@Location  
           ,@StartDate  
           ,@EndDate  
           ,@TotalPositions  
           ,@RemainingPositions  
           ,@ShowOnWeb  
           ,@FeaturedOnWeb  
           ,@PositionRequestedBy  
           ,@PositionOwner  
           ,@JobDescription  
           ,@ShowOnWebJobDescription  
           ,@DutiesAndResponsibilities  
           ,@ShowOnWebDuties  
           ,@SkillsAndQualification  
           ,@ShowOnWebSkills  
           ,@Benefits  
           ,@ShowOnWebBenefits  
           ,@SalaryMin  
           ,@SalaryMax  
           ,@ShowOnWebSal  
           ,@HourlyMin  
           ,@HourlyMax  
           ,@ShowonWebHour  
           ,@Commission  
           ,@ShowOnWebCommission  
           ,@BonusPotential  
           ,@ShowOnWebBonus  
           ,@IsDelete  
           ,@VacancyState  
           ,@CreatedBy  
           ,@CreatedOn  
           ,ABS(CAST(CAST(NEWID() AS VARBINARY) AS INT))
          )  
end   else  
 begin  
  Set @IsError = 101 -- Position Id already exists  
 end  
  
END
GO
/****** Object:  StoredProcedure [dbo].[SetSaveDefaultSearchQuery]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SetSaveDefaultSearchQuery]
	@SaveResumeSearchId uniqueidentifier,
	@UserId uniqueidentifier,
	@UpdatedBy uniqueidentifier,
	@UpdatedOn datetime
AS
declare @IsDefault bit = 1
BEGIN
   update SaveResumeSearch set IsDefault = 0 where UserId = @UserId
   
   update SaveResumeSearch set IsDefault = @IsDefault where SaveResumeSearchId = @SaveResumeSearchId
   
END
GO
/****** Object:  StoredProcedure [dbo].[SetDefaultProfile]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SetDefaultProfile]
@UserId uniqueidentifier,
@ProfileId uniqueidentifier,
@UpdatedBy uniqueidentifier,
@UpdatedOn datetime

As
Begin

Update Profile 
Set IsDefault = 0 where UserId = @UserId

Update Profile 
Set IsDefault = 1,
    UpdatedBy = @UpdatedBy,
    UpdatedOn = @UpdatedOn
where UserId = @UserId And ProfileId = @ProfileId

End
GO
/****** Object:  Table [dbo].[SecurityRolePrivilege]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityRolePrivilege](
	[SecurityRolePrivilegeId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[AtsSecurityRoleId] [uniqueidentifier] NOT NULL,
	[ATSPrivilegeId] [uniqueidentifier] NOT NULL,
	[PermissionType] [nvarchar](100) NULL,
	[Scope] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[IsChecked] [bit] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_SecurityRolePrivilege] PRIMARY KEY CLUSTERED 
(
	[SecurityRolePrivilegeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[RecordExistsForDegreeType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordExistsForDegreeType] --'60BD5256-0657-436F-BA16-03D383CFD5DC'
@DegreeTypeId uniqueidentifier

As
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [TableName] nvarchar(256)
	  ,[Count] int
)
Declare @CountDegreeType as int 
Declare @CountEntityLanguage as int 
Begin
Select @CountDegreeType =  COUNT(*)  from [dbo].EducationHistory where DegreeType = @DegreeTypeId 
Select @CountEntityLanguage =  COUNT(*)  from [dbo].EntityLanguage where RecordId = @DegreeTypeId 

insert into @tbl ([TableName],[Count]) values('EducationHistory',@CountDegreeType)
insert into @tbl ([TableName],[Count]) values('EntityLanguage',@CountEntityLanguage)
select * from @tbl

End
GO
/****** Object:  StoredProcedure [dbo].[RecordCountFromDivision]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordCountFromDivision] --'B0DA11C9-9D4D-4EC8-8326-009087C44066','33795F80-57D9-43C6-A466-03E816180694','17120BF4-7DC1-4B14-AAEB-E74AF12E6C64'
@ModuleId uniqueidentifier,
@LanguageId uniqueidentifier,
@UserId uniqueidentifier,
@IsDelete bit,
@ModuleName nvarchar(100) 
--@Tablename varchar(50),
--@Tablename1 varchar(50)

AS
begin
 if(@ModuleName ='Division') 
 begin
         select COUNT(*)as VacancyCount from Vacancy where CreatedBy = @UserId and IsDelete = 0 and LanguageId = @LanguageId and DivisionId = @ModuleId
         select COUNT(*)as JobLocationCount from JobLocation where  IsDelete = 0 and DivisionId = @ModuleId
         select COUNT(*)as UsersCount from Users where  IsDelete = 0 and DivisionId = @ModuleId
         
end
else If(@ModuleName = 'JobLocation')
begin 
     select COUNT(*)as VacancyCount from Vacancy where CreatedBy = @UserId and IsDelete = 0 and LanguageId = @LanguageId and Location = @ModuleId     
end

else If(@ModuleName = 'PositionType')
begin 
     select COUNT(*)as VacancyCount from Vacancy where CreatedBy = @UserId and IsDelete = 0 and LanguageId = @LanguageId and PositionTypeId = @ModuleId     
end

end

--declare @Sql nvarchar(250)
--declare @Sql1 nvarchar(250)
--declare @condition as nvarchar(250)
--declare @Count1 as nvarchar(100)
--set @condition = N'Where CreatedBy = '''+ CAST(@UserId as nvarchar(50)) +'''and IsDelete = 0 and LanguageId = '''+ CAST(@languageId as nvarchar(50)) + ''' and DivisionId = '''+CAST(@DivisionId as nvarchar(50)) +''''''''
--set @Sql = 'select COUNT(*) from '+@Tablename +' ' + @condition 
--select COUNT(*)as [count] from Vacancy where CreatedBy = @UserId and IsDelete = 0 and LanguageId = @LanguageId and DivisionId = @DivisionId
----set @Sql1 = 'select COUNT(*) as count1 from '+@Tablename1 
----' ' + @condition 
--select COUNT(*)as count1 from JobLocation where  IsDelete = 0 and DivisionId = @DivisionId



 --exec @Sql
--exec @Sql1
GO
/****** Object:  StoredProcedure [dbo].[InsertUserDivisionPermission]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUserDivisionPermission]


@UserDivisionPermissionId uniqueidentifier OUT,
@DivisionId uniqueidentifier,
@ClientId uniqueidentifier,
@UserId uniqueidentifier,
@Description nvarchar(max),
@IsDelete bit,
@CreatedOn datetime,
@CreatedBy uniqueidentifier

AS 
BEGIN
SET @UserDivisionPermissionId = newid();

INSERT INTO [dbo].[UserDivisionPermission]
           ([UserDivisionPermissionId]
           ,[DivisionId]
           ,[ClientId]
           ,[UserId]
           ,[Description]
           ,[IsDelete]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (@UserDivisionPermissionId
           ,@DivisionId
           ,@ClientId
           ,@UserId
           ,@Description
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy
           )
           
           END
GO
/****** Object:  StoredProcedure [dbo].[InsertUserDetails]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUserDetails]
@UserDetailsId uniqueidentifier out,
@UserId uniqueidentifier,
@HomeEmail nvarchar(100)= null,
@Affix nvarchaR(50) = null,
@FirstName nvarchar(100)= null,
@MiddleName nvarchar(100) = null,
@LastName nvarchar(100)= null,
@GivenName nvarchar(100) = null,
@Address  nvarchar(100)= null,
@City nvarchar(100)= null,
@Region nvarchar(100)= null,
@State  nvarchar(100)= null,
@Zip  nvarchar(50)= null,
@BusinessPhoneNo nvarchar(50)= null,
@HomePhone  nvarchar(50)= null,
@MobilePhone nvarchar(50)= null,
@Fax nvarchar(50) = null,
@Pager  nvarchar(50) = null,
@WebSite nvarchar(100) = null,
@WorkEmail nvarchar(100)= null,
@PostofficeBox nvarchar(50)= null,
@Misdemeanor bit,
@MisdemeanorExplain  nvarchar(100) = null,
@MilitaryService bit,
@MilitaryTypeDischarge  nvarchar(max)= null,
@EmergencyContact1  nvarchar(50)= null,
@EmergencyContact2 nvarchar(50)= null,
@EmergencyContact1Phone nvarchar(50)= null,
@EmergencyContact2Phone nvarchar(50) = null,
@IsDelete bit,
@CreatedBy uniqueidentifier = null,
@CreatedOn datetime = null

AS
BEGIN
SET @UserDetailsId = newid()


INSERT INTO [dbo].[UserDetails]
           ([UserDetailId]
           ,[UserId]
           ,[HomeEmail]
           ,[Affix] 
           ,[FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[GivenName]            
           ,[Address]           
           ,[City]
           ,[Region] 
           ,[State]
           ,[Zip]
           ,[BusinessPhoneNo]
           ,[HomePhone]
           ,[MobilePhone]
           ,[Fax] 
           ,[Pager]
           ,[WebSite]
           ,[WorkEmail]           
           ,[PostOfficeBox] 
           ,[Misdemeanor]
           ,[MisdemeanorExplain]
           ,[MilitaryService]
           ,[MilitaryTypeDischarge]
           ,[EmergencyContact1]
           ,[EmergencyContact2]
           ,[EmergencyContact1Phone]
           ,[EmergencyContact2Phone]
           ,[Isdelete]
           ,[CreatedOn]
           ,[CreatedBy]
           )
     VALUES
           (@UserDetailsId
           ,@UserId
           ,@HomeEmail
           ,@Affix
           ,@FirstName
           ,@MiddleName
           ,@LastName
           ,@GivenName
           ,@Address
           ,@City
           ,@Region 
           ,@State
           ,@Zip
           ,@BusinessPhoneNo
           ,@HomePhone
           ,@MobilePhone
           ,@Fax
           ,@Pager
           ,@WebSite
           ,@WorkEmail           
           ,@PostofficeBox
           ,@Misdemeanor
           ,@MisdemeanorExplain
           ,@MilitaryService
           ,@MilitaryTypeDischarge
           ,@EmergencyContact1
           ,@EmergencyContact2
           ,@EmergencyContact1Phone
           ,@EmergencyContact2Phone
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy
           )
end
GO
/****** Object:  Table [dbo].[Skills]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[SkillsId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[SkillAndQualification] [nvarchar](100) NULL,
	[SkillType] [uniqueidentifier] NULL,
	[Proficiency] [int] NULL,
	[Level] [int] NULL,
	[LastUsed] [nvarchar](100) NULL,
	[Experience] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[Createdon] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[SkillsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpeakingEventHistory]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpeakingEventHistory](
	[SpeakingEventHistoryId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](256) NULL,
	[StartDate] [datetime] NULL,
	[EventName] [nvarchar](256) NULL,
	[EventType] [nvarchar](100) NULL,
	[Location] [nvarchar](100) NULL,
	[Link] [nvarchar](256) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_SpeakingEventHistory] PRIMARY KEY CLUSTERED 
(
	[SpeakingEventHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SolrProfileDelta]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SolrProfileDelta] --'01-01-2014'
@UpdatedOn DateTime
AS
BEGIN

SELECT ProfileId from Profile where (UpdatedOn + GETDATE() - GETUTCDATE() > @UpdatedOn
       AND IsDelete=0)


END
GO
/****** Object:  StoredProcedure [dbo].[SolrProfileDelete]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SolrProfileDelete] --'01-01-2014'
@UpdatedOn DateTime
AS
BEGIN

SELECT ProfileId from Profile where (IsDelete=1)


END
GO
/****** Object:  StoredProcedure [dbo].[Solr_UserDetail]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_UserDetail] --'58CA38AC-DA88-42E3-B189-5E70C374421B'  
 (  
 @ProfileId as uniqueidentifier  ,
 @LanguageId as uniqueidentifier
 )  
 AS  
 BEGIN  
   
   
 if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)  
 BEGIN  
 SELECT _profile.ProfileId,  
 _profile.ClientId,  
 _UserDetails.UserId,  
FirstName,  
MiddleName,  
LastName,  
Affix,  
Address,  
City,  
State,  
BusinessPhoneNo,  
HomePhone,  
Fax,  
Pager,  
MobilePhone,  
HomeEmail,  
WebSite  

from UserDetails _UserDetails  
inner join Profile _profile on _profile.UserId = _UserDetails.UserId 
where _profile.ProfileId =@ProfileId   
 END  
 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_EmploymentHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_EmploymentHistory](
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId,

        STUFF(( SELECT ',' + CompanyName FROM EmploymentHistory e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHCompanyName 
       , STUFF(( SELECT ',' + cast(MayWeContact as nvarchar)  FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHMayWeContact 
       , STUFF(( SELECT ',' + SupervisorName FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHSupervisorName
       , STUFF(( SELECT ',' + Address FROM EmploymentHistory e where ProfileId =@ProfileId  FOR XML PATH('')), 1, 1, '') AS EMHAddress 
       , STUFF(( SELECT ',' + City FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHCity 
       , STUFF(( SELECT ',' + Zip FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHZip 
       , STUFF(( SELECT ',' + cast(Experience as nvarchar) FROM EmploymentHistory e where ProfileId =@ProfileId  FOR XML PATH('')), 1, 1, '') AS EMHExperience 
       , STUFF(( SELECT ',' + DutiesAndResponsibilities FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHDutiesAndResponsibilities 
       , MIN(StartingPay) AS EMHStartingPayMin
       ,MAX(StartingPay) As EMHStartingPayMax
       ,MAX(EndingPay) As EMHEMHEndingPayMax
       , MIN(EndingPay) AS EMHEndingPayMin
       , STUFF(( SELECT ',' + JobCategory FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHJobCategory
       , STUFF(( SELECT ',' + MostRecentPosition FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHMostRecentPosition
       ,COUNT(eh.ProfileId) as EMHNumberofJobMin
       ,COUNT(eh.ProfileId) as EMHNumberofJobMax
   , STUFF(( SELECT ',' + ReasonForLeaving FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHReasonForLeaving
   , STUFF(( SELECT ',' + StartigPosition FROM EmploymentHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EMHStartigPosition

FROM  EmploymentHistory eh
where ProfileId =@ProfileId AND IsDelete=0 
GROUP BY eh.ProfileId

END

 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_EducationHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_EducationHistory] --'FF1822ED-A813-4D56-8B03-07934DE94E33' 
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId, 
        STUFF(( SELECT ',' + InstitutionName FROM EducationHistory e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EHInstitutionName 
       , STUFF(( SELECT ',' + MeasureSystem  FROM EducationHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EHMeasureSystem 
    
       , STUFF(( SELECT ',' + MajorSubject FROM EducationHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EHMajorSubject
       , STUFF(( SELECT ',' + City FROM EducationHistory e where ProfileId =@ProfileId  FOR XML PATH('')), 1, 1, '') AS EHCity 
       , STUFF(( SELECT ',' + State FROM EducationHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EHState 
       , STUFF(( SELECT ',' + Country FROM EducationHistory e where ProfileId =@ProfileId  FOR XML PATH('')), 1, 1, '') AS EHCountry 
       , STUFF(( SELECT ',' + MeasureValue FROM EducationHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EHMeasureValue 
       
	   
	   , Min(StartDate) AS EHStartDateMin
	   , Max(StartDate) AS EHStartDateMax
       , Min(EndDate) AS EHEndDateMin
       , Max(EndDate) AS EHEndDateMax
       , Min(DegreeDate) AS EHDegreeDateMin
       , MAX(DegreeDate) AS EHDegreeDateMax
       ,(Select MAX(D.Priority) AS EHPriorityMax from EducationHistory e  inner join DegreeType D on  e.DegreeType = D.DegreeTypeId where e.ProfileId = @ProfileId) as EHPriorityMax 
       ,(Select Min(D.Priority) AS EHPriorityMin from EducationHistory e  inner join DegreeType D on  e.DegreeType = D.DegreeTypeId where e.ProfileId = @ProfileId)as EHPriorityMin
       
       
       ,COUNT(eh.ProfileId) as EHNumberofJobMin
       ,COUNT(eh.ProfileId) as EHNumberofJobMax
       ,STUFF(( SELECT ',' + Description FROM EducationHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS EHDescription 
FROM  EducationHistory eh
where ProfileId =@ProfileId  AND IsDelete=0
GROUP BY eh.ProfileId
END


 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_Profile]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_Profile] --'58CA38AC-DA88-42E3-B189-5E70C374421B'
 @ProfileId uniqueidentifier =null
 AS
 BEGIN
 
 if @ProfileId IS NULL
 begin
 SELECT Profileid,Profile.LanguageId from Profile 
 inner join Users on Users.UserId = Profile.UserId AND Users.IsActive=1 and Users.IsDelete=0 AND  Users.DivisionId IS NULL
 where Isdefault=1 
 END
 ELSE
 BEGIN
 SELECT Profileid,Profile.LanguageId from Profile 
 inner join Users on Users.UserId = Profile.UserId AND Users.IsActive=1 and Users.IsDelete=0 AND  Users.DivisionId IS NULL
 where Isdefault=1 AND Profileid =@ProfileId
 END
 
       

END
GO
/****** Object:  StoredProcedure [dbo].[SearchQueryNameExists]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SearchQueryNameExists] --'EmploymentHistory','EmployementHistoryId','BD6BA847-8ADD-4E36-8FB1-077185FD41DE'


@UserId uniqueIdentifier,
@SearchQueryName nvarchar(256),
@Exists as bit output

AS
BEGIN

If Exists(select 1 from SaveResumeSearch where UserId = @UserId AND SearchQueryName = @SearchQueryName AND IsDelete = 0)
 begin
 Set @Exists = 1
 end
 
 Else
 Begin
 Set @Exists = 0
 End

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateATSResume]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateATSResume]
	  @ATSResumeId uniqueidentifier output
	  ,@ATSDocumentId uniqueidentifier 
      ,@ProfileId uniqueidentifier
	  ,@UserId uniqueidentifier
      ,@Details nvarchar(MAX)= null
      ,@UploadedFileName nvarchar(250)
      ,@NewFileName nvarchar(250)
      ,@Path nvarchar(250)
      ,@TitleName nvarchar(100) = null
      ,@CreatedBy uniqueidentifier
      ,@CreatedOn datetime
	  ,@IsCoverLetter bit		
		   
		   AS
		   Declare @EmptyId uniqueidentifier 
		   select @EmptyId = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)
		   BEGIN
		   If @ATSDocumentId = @EmptyId
		   begin
		   --SET @ATSResumeId = NEWID() --Chnage by viral shah
				Execute [InsertATSResume] @ATSResumeId OUT,@ProfileId,@UserId,@Details,@UploadedFileName,@NewFileName,@Path,@TitleName,0,@CreatedBy,@CreatedOn,@IsCoverLetter
		   end
		   else
		   begin
		   set @ATSResumeId = @ATSDocumentId
		   UPDATE [dbo].[ATSResume]
   SET 
   [ProfileId] = @ProfileId
   ,[UserId] = @UserId
   ,[Details] = @Details
   ,[UploadedFileName] = @UploadedFileName
   ,[NewFileName]= @NewFileName 
   ,[Path]= @Path 
   ,[TitleName]= @TitleName 
   ,[UpdatedOn] =  @CreatedOn
   ,[UpdatedBy] = @CreatedBy
   ,[IsCoverLetter] = @IsCoverLetter
 WHERE [ATSResumeId] = @ATSDocumentId  
 END
 Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
 END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmploymentHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateEmploymentHistory]
@EmployementHistoryId uniqueidentifier,
@ProfileId uniqueidentifier,
@UserId uniqueidentifier,
       @CompanyName nvarchar(100),
       @MayWeContact bit,
       @SupervisorName nvarchar(100),
       @Telephone nvarchar(100),
       @Address nvarchar(256),
       @City nvarchar(100),
      @State nvarchar(100),
       @Zip nvarchar(100),
       @StartMonth int,
       @EndMonth int,
       @StartYear int,
       @EndYear int,
       @Experience int,
       @JobCategory nvarchar(100),
       @StartingPay decimal(10,2),
       @StartingPosition nvarchar(100),
       @MostRecentPosition nvarchar(100),
      @EndingPay decimal(10,2),
       @ReasonForLeaving nvarchar(max),
       @DutiesAndResponsibilities nvarchar(max),
       @IsDelete bit,
       @UpdatedOn datetime,
@UpdatedBy uniqueidentifier
As
Begin




UPDATE [dbo].[EmploymentHistory]
   SET [EmployementHistoryId] = @EmployementHistoryId
      ,[ProfileId] = @ProfileId
      ,[UserId] = @UserId
      ,[CompanyName] = @CompanyName
      ,[MayWeContact] = @MayWeContact
      ,[SupervisorName] = @SupervisorName
      ,[Telephone] = @Telephone
      ,[Address] = @Address
      ,[City] = @City
      ,[State] = @State
      ,[Zip] = @Zip
      ,[StartMonth] = @StartMonth
      ,[EndMonth] = @EndMonth
      ,[StartYear] = @StartYear
      ,[EndYear] = @EndYear
      ,[Experience] = @Experience
      ,[JobCategory] = @JobCategory
      ,[StartingPay] = @StartingPay
      ,[StartigPosition] = @StartingPosition 
      ,[MostRecentPosition] = @MostRecentPosition
      ,[EndingPay] = @EndingPay
      ,[ReasonForLeaving] = @ReasonForLeaving
      ,[DutiesAndResponsibilities] = @DutiesAndResponsibilities
      ,[IsDelete] = @IsDelete
            ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy 
 WHERE [EmployementHistoryId] = @EmployementHistoryId and [ProfileId] = @ProfileId
Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmergensyContactInfo]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEmergensyContactInfo]
@UserId uniqueidentifier,
@EmergencyContact1 nvarchar(50),
@EmergencyContact2 nvarchar(50),
@EmergencyContact1Phone nvarchar(50),
@EmergencyContact2Phone nvarchar(50),
@UpdatedBy uniqueidentifier,
@UpdatedOn datetime

AS
BEGIN

Update UserDetails
SET EmergencyContact1 = @EmergencyContact1,
   EmergencyContact2 = @EmergencyContact2,
   EmergencyContact1Phone = @EmergencyContact1Phone,
   EmergencyContact2Phone = @EmergencyContact2Phone,
   UpdatedBy = @UpdatedBy,
   UpdatedOn = @UpdatedOn
   where UserId = @UserId
   END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEducationHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEducationHistory]
@EducationHistoryId uniqueidentifier,
       @ProfileId uniqueidentifier,
       @UserId uniqueidentifier,
       @InstitutionName nvarchar(100),
      @StartDate datetime,
      @EndDate datetime,
      @DegreeDate datetime,
      @MeasureSystem nvarchar(50),
       @MeasureValue nvarchar(50),
       @DegreeType uniqueidentifier,
       @MajorSubject nvarchar(100),
       @City nvarchar(100),
      @State nvarchar(100),
       @Country nvarchar(50),
      @Description nvarchar(max),
       @IsDelete bit,
       @UpdatedOn datetime,
      
       @UpdatedBy uniqueidentifier
      
      AS
      BEGIN

UPDATE [dbo].[EducationHistory]
   SET [EducationHistoryId] = @EducationHistoryId
      ,[ProfileId] = @ProfileId
      ,[UserId] = @UserId
      ,[InstitutionName] = @InstitutionName
      ,[StartDate] = @StartDate
      ,[EndDate] = @EndDate
      ,[DegreeDate] = @DegreeDate
      ,[MeasureSystem] = @MeasureSystem
      ,[MeasureValue] = @MeasureValue
      ,[DegreeType] = @DegreeType
      ,[MajorSubject] = @MajorSubject
      ,[City] = @City
      ,[State] = @State
      ,[Country] = @Country
      ,[Description] = @Description
      ,[IsDelete] = @IsDelete
      ,[UpdatedOn] = @UpdatedOn
      
      ,[UpdatedBy] = @UpdatedBy
      
 WHERE [EducationHistoryId] = @EducationHistoryId and [ProfileId] = @ProfileId
 
 Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateDivisionPositiontype]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateDivisionPositiontype]

@DivisionPositionTypeId uniqueidentifier,
@ClientId uniqueidentifier,
@DivisionId uniqueidentifier,
@PositionTypeId uniqueidentifier,
@IsDelete bit,
@UpdatedBy uniqueidentifier,
@UpdatedOn datetime

AS
BEGIN
UPDATE [dbo].[DivisionPositionType]
   SET [DivisionPositionTypeId] = @DivisionPositionTypeId
      ,[ClientId] = @ClientId
      ,[DivisionId] = @DivisionId
      ,[PositionTypeId] = @PositionTypeId
      ,[IsDelete] = @IsDelete
      
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE [DivisionPositionTypeId] = @DivisionPositionTypeId
 END
GO
/****** Object:  StoredProcedure [dbo].[UpdateContactDetails]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateContactDetails]
@UserId uniqueidentifier,
 
 @HomeEmail nvarchar(50),
 @FirstName varchar(50),
 @LastName varchar(50),
 @Address nvarchar(200)= null,
 @City varchar(50),
 @State varchar(50),
 @Zip varchar(50),
 @BusinessPhoneNo varchar(11) = null,
 @HomePhone varchar(11)= null,
 @MobilePhone varchar(11) = null,
 @Pager varchar(20) = null,
 @WorkEmail varchar(50) = null,
 @Misdemeanor nvarchar(100) = null,
 @MisdemeanorExplain nvarchar(100)= null,
 @MilitaryService nvarchar(100) = null,
 @MilitaryTypeDischarge nvarchar(100) = null,
 @EmergencyContact1 nvarchar(100) = null,
 @EmergencyContact2 nvarchar(100)= null,
 @EmergencyContact1Phone nvarchar(100) = null,
 @EmergencyContact2Phone nvarchar(100) = null,
 @UpdatedOn datetime,
 @UpdatedBy uniqueidentifier
 As
 
 Begin
 
 UPDATE [dbo].[UserDetails]
   SET 
 
      [HomeEmail] = @HomeEmail
      ,[FirstName] = @FirstName
      ,[LastName] = @LastName
      ,[Address] = @Address
      ,[City] = @City
      ,[State] = @State
      ,[Zip] = @Zip
      ,[BusinessPhoneNo] = @BusinessPhoneNo
      ,[HomePhone] = @HomePhone
      ,[MobilePhone] = @MobilePhone
      ,[Pager] = @Pager
      ,[WorkEmail] = @WorkEmail
      ,[Misdemeanor] = @Misdemeanor
      ,[MisdemeanorExplain] = @MisdemeanorExplain
      ,[MilitaryService] = @MilitaryService
      ,[MilitaryTypeDischarge] = @MilitaryTypeDischarge
      ,[EmergencyContact1] = @EmergencyContact1
      ,[EmergencyContact2] = @EmergencyContact2
      ,[EmergencyContact1Phone] = @EmergencyContact1Phone
      ,[EmergencyContact2Phone] = @EmergencyContact2Phone
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE UserId = @UserId
 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateShowOnWeb]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateShowOnWeb] 

@VacancyId uniqueidentifier,
@FieldValue bit,
@PostedOn datetime,
@UpdatedOn datetime=null,
@CreatedOn datetime = null,
@UpdatedBy uniqueidentifier = null,
@CreatedBy uniqueidentifier = null

AS
BEGIN
SET NOCOUNT ON;
	
	
	Update [dbo].[Vacancy] Set 
	ShowOnWeb = @FieldValue,
	[PostedOn]=@PostedOn,
	[UpdatedOn] = getutcdate(),
	[UpdatedBy]= @UpdatedBy 
	where [VacancyId] = @VacancyId 
	
	
	
End
GO
/****** Object:  StoredProcedure [dbo].[UpdateSaveResumeSearch]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSaveResumeSearch]
 @SaveResumeSearchId uniqueidentifier,
      @JsonQuery nvarchar(max),
       @UpdatedOn datetime,
       @UpdatedBy uniqueidentifier
      
      AS
      BEGIN
      
UPDATE [dbo].[SaveResumeSearch]
   SET [SaveResumeSearchId] = @SaveResumeSearchId
      ,[JsonQuery] = @JsonQuery  
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE [SaveResumeSearchId] = @SaveResumeSearchId
 END
GO
/****** Object:  StoredProcedure [dbo].[ValidateProfile]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateProfile] --'295E4FCB-D23D-4EE0-BE4F-AAE5DD999971','fds Test','DF9C2730-08DE-467E-BC8D-3D065FE1F855',null

@UserId uniqueidentifier,
@ProfileName nvarchar(100),
@ClientId uniqueidentifier,
@IsError int OUTPUT
AS
BEGIN

declare @IsDelete bit


select @UserId = UserId,@IsDelete = IsDelete from [dbo].Profile
where ProfileName = @ProfileName and ClientId = @ClientId and UserId =@UserId
SET @IsError = 0

IF (@UserId  Is Not Null and @IsDelete = 0)
		Begin
			Set @IsError = 102 -- Profile Name already exists
			return
		End
		
Else
		Begin

		SET @IsError = 0
		end


end
GO
/****** Object:  Table [dbo].[UserVacancy]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserVacancy](
	[UserVacancyId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[VacancyId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[VersionNo] [timestamp] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_UserVacany] PRIMARY KEY CLUSTERED 
(
	[UserVacancyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSecurityRole]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSecurityRole](
	[UserSecurityRoleId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ATSSecurityRoleId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[VersionNo] [timestamp] NOT NULL,
 CONSTRAINT [PK_UserSecurityRole] PRIMARY KEY CLUSTERED 
(
	[UserSecurityRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[UpdateVacancy]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVacancy]  
@VacancyId uniqueidentifier,  
@ClientId uniqueidentifier,  
@LanguageId uniqueidentifier,  

@PositionTypeId uniqueidentifier,  
@JobTitle nvarchar(100),  
@PositionID nvarchar(100),  
@VacancyStatusId uniqueidentifier,
@JobType nvarchar(100),  
@EmploymentType nvarchar(100),  
@DivisionId uniqueidentifier,  
@Location uniqueidentifier,  
@StartDate datetime,  
@EndDate datetime,  
@TotalPositions int,  
@RemainingPositions int,  
@ShowOnWeb bit,  
@FeaturedOnWeb bit,  
@PositionRequestedBy uniqueidentifier,  
@PositionOwner uniqueidentifier,  
@JobDescription nvarchar(max)= null,  
@ShowOnWebJobDescription bit,  
@DutiesAndResponsibilities nvarchar(max) = null,  
@ShowOnWebDuties bit,  
@SkillsAndQualification nvarchar(max) = null,  
@ShowOnWebSkills bit,  
@Benefits nvarchar(max) = null,  
@ShowOnWebBenefits bit,  
@SalaryMin decimal(10,2),  
@SalaryMax decimal(10,2)= null,  
@ShowOnWebSal bit,  
@HourlyMin decimal(10,2)= null,  
@HourlyMax decimal(10,2)= null,  
@ShowonWebHour bit,  
--@Commission decimal(10,2)= null,  
@Commission nvarchar(max) = null,  
@ShowOnWebCommission bit,  
--@BonusPotential decimal(10,2)= null,  
@BonusPotential nvarchar(max) = null,  
@ShowOnWebBonus bit,  
@IsDelete bit= null,  
@UpdatedBy uniqueidentifier = null,  
@UpdatedOn datetime = null,  
@FieldValue nvarchar(max),  
@PostedOn datetime,  
@IsError int OUTPUT  
  
AS  
Set @IsError = 0  
declare @Result bit = 0  
declare @TableName varchar(50) = 'Vacancy'  
BEGIN  
Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output  
  
IF @Result = 0  
 begin  
UPDATE [dbo].[Vacancy]  
   SET   
      [ClientId] = @ClientId  
      ,[LanguageId] = @LanguageId  
      ,[PositionTypeId] = @PositionTypeId  
      
      ,[JobTitle] = @JobTitle  
      ,[PositionID] = @PositionID  
      ,[VacancyStatusId] = @VacancyStatusId   
      ,[JobType] = @JobType  
      ,[EmploymentType] = @EmploymentType  
      ,[DivisionId] = @DivisionId  
      ,[Location] = @Location  
      ,[StartDate] = @StartDate  
      ,[EndDate] = @EndDate  
      ,[TotalPositions] = @TotalPositions  
      ,[RemainingPositions] = @RemainingPositions  
      ,[ShowOnWeb] = @ShowOnWeb  
      ,[FeaturedOnWeb] = @FeaturedOnWeb  
      ,[PositionRequestedBy] = @PositionRequestedBy  
      ,[PositionOwner] = @PositionOwner  
         ,[JobDescription] = @JobDescription  
           ,[ShowOnWebJobDescription] = @ShowOnWebJobDescription  
           ,[DutiesAndResponsibilities] = @DutiesAndResponsibilities  
           ,[ShowOnWebDuties] = @ShowOnWebDuties  
           ,[SkillsAndQualification] = @SkillsAndQualification  
           ,[ShowOnWebSkills] = @ShowOnWebSkills  
           ,[Benefits] = @Benefits  
           ,[ShowOnWebBenefits] = @ShowOnWebBenefits  
      ,[SalaryMin] = @SalaryMin  
      ,[SalaryMax] = @SalaryMax  
      ,[ShowOnWebSal] =@ShowOnWebSal  
      ,[HourlyMin] = @HourlyMin  
      ,[HourlyMax] = @HourlyMax  
      ,[ShowonWebHour] = @ShowonWebHour  
      ,[Commission] = @Commission  
      ,[ShowOnWebCommission] = @ShowOnWebCommission  
      ,[BonusPotential] = @BonusPotential  
      ,[ShowOnWebBonus] = @ShowOnWebBonus  
      ,[IsDelete] = @IsDelete  
      ,[UpdatedBy] = @UpdatedBy  
      ,[UpdatedOn] = @UpdatedOn  
      ,[PostedOn] =@PostedOn
 WHERE [VacancyId] = @VacancyId AND [ClientId] = @ClientId  
end  
 else  
 begin  
  Set @IsError = 101 -- Position Id already exists  
 end  
  
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDivisionPermission]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUserDivisionPermission]

@UserDivisionPermissionId uniqueidentifier,
@DivisionId uniqueidentifier,
@ClientId uniqueidentifier,
@UserId uniqueidentifier,
@Description nvarchar(max),
@IsDelete bit,
@UpdatedOn datetime,
@UpdatedBy uniqueidentifier
AS
BEGIN
UPDATE [dbo].[UserDivisionPermission]
   SET [UserDivisionPermissionId] = @UserDivisionPermissionId
      ,[DivisionId] =@DivisionId 
      ,[ClientId] = @ClientId
      ,[UserId] = @UserId
      ,[Description] = @Description
      ,[IsDelete] = @IsDelete
      
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE [UserDivisionPermissionId] = @UserDivisionPermissionId AND [ClientId] = @ClientId
 END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[UpdateUserDetails]
@UserId uniqueidentifier,
 @Affix nvarchar(50),
 @HomeEmail nvarchar(50),
 @FirstName varchar(50),
 @MiddleName varchar(50),
 @LastName varchar(50),
 @Address nvarchar(200)= null,
 @City varchar(50),
 @State varchar(50),
 @Zip varchar(50),
 @Fax nvarchar(50), 
 @BusinessPhoneNo varchar(50) = null,
 @HomePhone varchar(50)= null,
 @MobilePhone varchar(50) = null,
 @Pager varchar(20) = null,
 @WorkEmail varchar(50) = null,
 @WebSite nvarchar(100)=null,
 @PostOfficeBox nvarchar(50) = null,  
 @Misdemeanor bit,
 @MisdemeanorExplain nvarchar(100)= null,
 @MilitaryService bit,
 @MilitaryTypeDischarge nvarchar(100) = null,
 @UpdatedOn datetime,
 @UpdatedBy uniqueidentifier
 As
 
 Begin
 
 UPDATE [dbo].[UserDetails]
   SET 
       [Affix] = @Affix 
      ,[HomeEmail] = @HomeEmail
      ,[FirstName] = @FirstName
      ,[MiddleName]  = @MiddleName
      ,[LastName] = @LastName      
      ,[Address] = @Address
      ,[City] = @City
      ,[State] = @State
      ,[Zip] = @Zip
      ,[BusinessPhoneNo] = @BusinessPhoneNo
      ,[HomePhone] = @HomePhone
      ,[MobilePhone] = @MobilePhone
      ,[Fax] = @Fax      
      ,[WebSite] = @WebSite
      ,[PostOfficeBox] = @PostOfficeBox
      ,[Pager] = @Pager      
      ,[WorkEmail] = @WorkEmail
      ,[Misdemeanor] = @Misdemeanor
      ,[MisdemeanorExplain] = @MisdemeanorExplain
      ,[MilitaryService] = @MilitaryService
      ,[MilitaryTypeDischarge] = @MilitaryTypeDischarge
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
 WHERE UserId = @UserId
END
GO
/****** Object:  View [dbo].[vUserDivision]    Script Date: 07/11/2014 17:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vUserDivision]
AS
SELECT     temp.UserId, temp.DivisionId, _Division.ParentDivisionId
FROM         (SELECT     UserId, DivisionId
                       FROM          dbo.Users AS _Users
                       UNION
                       SELECT     UserId, DivisionId
                       FROM         dbo.UserDivisionPermission AS _UserDivisionPermission) AS temp INNER JOIN
                      dbo.Division AS _Division ON _Division.DivisionId = temp.DivisionId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4[30] 2[40] 3) )"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2[66] 3) )"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2) )"
      End
      ActivePaneConfig = 14
   End
   Begin DiagramPane = 
      PaneHidden = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "temp"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 96
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "_Division"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 126
               Right = 403
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      PaneHidden = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUserDivision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vUserDivision'
GO
/****** Object:  StoredProcedure [dbo].[UpdateVacancyStatus]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVacancyStatus] 
@VacancyStatusId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@ClientId uniqueidentifier
           ,@VacancyStatusText nvarchar(100)
           ,@Category nvarchar(50)
           ,@IsDelete bit
           ,@FieldValue nvarchar(max)
           ,@IsError int OUTPUT
           ,@UpdatedOn datetime
           ,@UpdatedBy uniqueidentifier
          AS
          Set @IsError = 0
declare @Result bit = 0
declare @TableName varchar(50) = 'VacancyStatus'
          BEGIN 
          
           Execute CheckDuplicateRecord @FieldValue,@TableName,@Result output
           
           IF @Result = 0
begin
UPDATE [dbo].[VacancyStatus]
   SET [VacancyStatusId] = @VacancyStatusId
      ,[UserId] = @UserId
      ,[ClientId] = @ClientId
      ,[VacancyStatusText] = @VacancyStatusText
      ,[Category] = @Category
      ,[IsDelete] = @IsDelete
      
      
      ,[UpdatedOn] = @UpdatedOn
      ,[UpdatedBy] = @UpdatedBy
      
 WHERE  [VacancyStatusId]= @VacancyStatusId
END
else
begin
		Set @IsError = 101 -- Vacancy Status already exists
end
End
GO
/****** Object:  StoredProcedure [dbo].[UpdateSpeakingHistoryEvent]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[UpdateSpeakingHistoryEvent]
 @SpeakingEventHistoryId uniqueidentifier
      , @ProfileId uniqueidentifier
      , @UserId uniqueidentifier
      , @Title nvarchar(256)
      ,@StartDate datetime
      , @EventName nvarchar(256)
      , @EventType nvarchar(100)
      , @Location nvarchar(100)
      ,@Link nvarchar(256)
      , @IsDelete bit
    
      , @UpdatedBy uniqueidentifier
      , @UpdatedOn datetime
AS
BEGIN

UPDATE [dbo].[SpeakingEventHistory]
   SET [SpeakingEventHistoryId] = @SpeakingEventHistoryId
      ,[ProfileId] = @ProfileId
      ,[UserId] = @UserId
      ,[Title] = @Title
      ,[StartDate] = @StartDate
      ,[EventName] = @EventName
      ,[EventType] = @EventType
      ,[Location] = @Location
      ,[Link] = @Link
      ,[IsDelete] = @IsDelete
    
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedOn] = @UpdatedOn
 WHERE [SpeakingEventHistoryId] =@SpeakingEventHistoryId AND [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateSkills]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSkills]
@SkillsId uniqueidentifier,
      @ProfileId uniqueidentifier,
      @UserId uniqueidentifier,
      @SkillAndQualification nvarchar(100),
      @SkillType uniqueidentifier,
      @Proficiency int,
      @Level int,
      @LastUsed nvarchar(100),
      @Experience int,
      @Description nvarchar(max),
      @IsDelete bit,
      @UpdatedOn datetime,
      
      @UpdatedBy uniqueidentifier
      

AS
BEGIN


UPDATE [dbo].[Skills]
   SET [SkillsId] = @SkillsId
      ,[ProfileId] = @ProfileId
      ,[UserId] = @UserId
      ,[SkillAndQualification] = @SkillAndQualification
      ,[SkillType] = @SkillType
      ,[Proficiency] = @Proficiency
      ,[Level] = @Level
      ,[LastUsed] = @LastUsed
      ,[Experience] = @Experience
      ,[Description] = @Description
      ,[IsDelete] = @IsDelete
      ,[UpdatedOn] = @UpdatedOn
      
      ,[UpdatedBy] = @UpdatedBy
      
 WHERE [SkillsId] = @SkillsId and [ProfileId] = @ProfileId

Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateExecutiveSummary]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateExecutiveSummary]
@ExecutiveSummaryId uniqueidentifier,
@ProfileId uniqueidentifier,
@UserId uniqueidentifier,
@ExecutiveSummaryDetails nvarchar(MAX),
@UpdatedBy uniqueidentifier,
@UpdatedOn datetime,
@IsDelete bit

AS
BEGIN

UPDATE [dbo].[ExecutiveSummary]
   SET [ExecutiveSummaryId] = @ExecutiveSummaryId
      ,[ProfileId] = @ProfileId
      ,[UserId] = @UserId
      ,[ExecutiveSummaryDetails] = @ExecutiveSummaryDetails
      ,[IsDelete] = @IsDelete
   
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedOn] = @UpdatedOn
 WHERE [ExecutiveSummaryId] = @ExecutiveSummaryId
 Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateReferences]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[UpdateReferences]
@ReferenceId uniqueidentifier
      ,@ProfileId uniqueidentifier
      ,@UserId uniqueidentifier
      ,@ReferenceName nvarchar(100)
      ,@Relationship nvarchar(100)
      ,@ReferencePhone nvarchar(100)
      ,@ReferenceEmail nvarchar(100)
      ,@IsDelete bit
      ,@UpdatedOn datetime
      
      ,@UpdatedBy uniqueidentifier
      
AS
BEGIN


UPDATE [dbo].[Reference]
   SET [ReferenceId] = @ReferenceId 
      ,[ProfileId] = @ProfileId 
      ,[UserId] = @UserId 
      ,[ReferenceName] = @ReferenceName 
      ,[Relationship] = @Relationship 
      ,[ReferencePhone] = @ReferencePhone 
      ,[ReferenceEmail] = @ReferenceEmail 
      ,[IsDelete] = @IsDelete 
      ,[UpdatedOn] = @UpdatedOn 
      
      ,[UpdatedBy] = @UpdatedBy 
      
 WHERE [ReferenceId] = @ReferenceId AND [ProfileId] = @ProfileId 
 Update Profile set UpdatedOn = GetDate() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePublicationHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdatePublicationHistory]
@PublicationHistoryId uniqueidentifier
      ,@ProfileId uniqueidentifier
      ,@UserId uniqueidentifier
      , @Title nvarchar(100)
      ,@Type nvarchar(100)
      , @Role nvarchar(100)
      , @Name nvarchar(100)
      , @PublicationDate datetime
      , @Description nvarchar(max)
      , @Comments nvarchar(max)
      , @IsDelete bit
      , @UpdatedBy uniqueidentifier
      , @UpdatedOn datetime

AS
BEGIN


UPDATE [dbo].[PublicationHistory]
   SET [PublicationHistoryId] = @PublicationHistoryId
      ,[ProfileId] = @ProfileId
      ,[UserId] = @UserId
      ,[Title] = @Title
      ,[Type] = @Type
            ,[Role] = @Role
      ,[Name] = @Name
      ,[PublicationDate] = @PublicationDate
      ,[Description] = @Description
      ,[Comments] = @Comments
      ,[IsDelete] = @IsDelete
      
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedOn] = @UpdatedOn
 WHERE [PublicationHistoryId] = @PublicationHistoryId AND [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateObjective]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateObjective]
@ObjectiveId uniqueidentifier,
@ProfileId uniqueidentifier,
@UserId uniqueidentifier,
@ObjectiveDetails nvarchar(MAX),
@IsDelete bit,
@UpdatedBy uniqueidentifier,
@UpdatedOn Datetime 
AS
BEGIN
UPDATE [dbo].[Objective]
   SET [ObjectiveId] = @ObjectiveId
      ,[ProfileId] = @ProfileId
      ,[UserId] =@UserId
      ,[ObjectiveDetails] = @ObjectiveDetails
      ,[IsDelete] = @IsDelete
      
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdateOn] = @UpdatedOn
 WHERE [ObjectiveId] = @ObjectiveId
 Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateLicenceAndCertifications]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateLicenceAndCertifications]

@LicenceAndCertificationsId uniqueidentifier
      ,@ProfileId uniqueidentifier
      ,@UserId uniqueidentifier
      ,@Name nvarchar(256)
      ,@IssuingAuthority nvarchar(256)
      ,@Description nvarchar(max)
      ,@ValidFrom datetime
      ,@ValidTo datetime
      ,@IsDelete bit
      
      ,@UpdatedBy uniqueidentifier
      ,@UpdatedOn datetime
AS
BEGIN

UPDATE [dbo].[LicenceAndCertifications]
   SET [LicenceAndCertificationsId] = @LicenceAndCertificationsId
      ,[ProfileId] = @ProfileId
      ,[UserId] = @UserId
      ,[Name] = @Name
      ,[IssuingAuthority] = @IssuingAuthority
      ,[Description] = @Description
      ,[ValidFrom] = @ValidFrom
      ,[ValidTo] = @ValidTo
      ,[IsDelete] = @IsDelete
      
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedOn] = @UpdatedOn
 WHERE [LicenceAndCertificationsId] = @LicenceAndCertificationsId AND [ProfileId] = @ProfileId 
Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateLanguages]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateLanguages]
 @LanguagesId uniqueidentifier
      ,  @ProfileId uniqueidentifier
      , @UserId uniqueidentifier
      , @LanguageCode nvarchar(50)
      , @Read bit
      ,@Write bit
      , @Speak bit
      , @Comments nvarchar(max)
      , @IsDelete bit
      
      , @UpdatedBy uniqueidentifier
      , @UpdatedOn datetime
AS
BEGIN
UPDATE [dbo].[Languages]
   SET [LanguagesId] = @LanguagesId 
      ,[ProfileId] = @ProfileId 
      ,[UserId] = @UserId 
      ,[LanguageCode] = @LanguageCode 
      ,[Read] = @Read 
      ,[Write] = @Write 
            ,[Speak] = @Speak 
      ,[Comments] = @Comments 
      ,[IsDelete] = @IsDelete 
      
      ,[UpdatedBy] = @UpdatedBy 
      ,[UpdatedOn] = @UpdatedOn 
 WHERE [LanguagesId] =@LanguagesId AND [ProfileId] =@ProfileId
 Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAvailibility]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAvailibility]
@AvailibilityId uniqueidentifier
,@ProfileId uniqueidentifier
,@DateAvailability datetime
,@TargetIncome numeric(18,0)
,@HrsMon nvarchar(100)
      ,@HrsTue nvarchar(100)
      ,@HrsWed  nvarchar(100)
      ,@HrsThu nvarchar(100)
      ,@HrsFri nvarchar(100)
      ,@HrsSat nvarchar(100)
      ,@HrsSun nvarchar(100)
      ,@EmploymentPreference int
      ,@WillingToWorkOverTime bit
      ,@RelativesWorkingInCompany bit
      ,@RelativesIfYes nvarchar(100)
      ,@SubmittedApplicationBefore bit
      ,@AppicationSubmision nvarchar(256)
      ,@HearAboutThePosition nvarchar(100)
      ,@ReferredBy nvarchar(100)
      ,@HowOld bit
      ,@EligibleToWorkInUS bit
     
     ,@UpdatedBy uniqueidentifier = null
      ,@updatedOn datetime = null
	  
AS
BEGIN

UPDATE [dbo].[Availability]
   SET
      [DateAvailability] = @DateAvailability
      ,[ProfileId] = @ProfileId
      ,[TargetIncome] = @TargetIncome
      ,[HrsMon] = @HrsMon
      ,[HrsTue] = @HrsTue
      ,[HrsWed] = @HrsWed
      ,[HrsThu] = @HrsThu
      ,[HrsFri] = @HrsFri
      ,[HrsSat] = @HrsSat
      ,[HrsSun] = @HrsSun
      ,[EmploymentPreference] = @EmploymentPreference
      ,[WillingToWorkOverTime] = @WillingToWorkOverTime
      ,[RelativesWorkingInCompany] = @RelativesWorkingInCompany
      ,[RelativesIfYes] = @RelativesIfYes
      ,[SubmittedApplicationBefore] = @SubmittedApplicationBefore
      ,[AppicationSubmision] = @AppicationSubmision
      ,[HearAboutThePosition] = @HearAboutThePosition
      ,[ReferredBy] = @ReferredBy
      ,[HowOld] = @HowOld
      ,[EligibleToWorkInUS] = @EligibleToWorkInUS
      
      ,[UpdatedBy] = @UpdatedBy
      ,[updatedOn] = @updatedOn
 WHERE AvailibilityId = @AvailibilityId 
 Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAssociations]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAssociations]
 @AssociationsId uniqueidentifier
      , @ProfileId uniqueidentifier
      , @UserId uniqueidentifier
      , @Name nvarchar(100)
      , @AssociationType nvarchar(100)
      , @Link nvarchar(100)
      , @StartDate datetime
      , @EndDate datetime
      , @Role nvarchar(100)
      , @IsDelete bit
      
      , @UpdatedBy uniqueidentifier
      , @UpdatedOn datetime

AS
BEGIN
UPDATE [dbo].[Associations]
   SET [AssociationsId] = @AssociationsId 
      ,[ProfileId] = @ProfileId 
      ,[UserId] = @UserId 
      ,[Name] = @Name 
      ,[AssociationType] = @AssociationType 
      ,[Link] = @Link 
      ,[StartDate] = @StartDate 
      ,[EndDate] = @EndDate 
      ,[Role] = @Role
      ,[IsDelete] = @IsDelete
      
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedOn] = @UpdatedOn
 WHERE [AssociationsId] = @AssociationsId AND [ProfileId] = @ProfileId
 
 Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAchievements]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAchievements]
@AchievementId uniqueidentifier,
       @ProfileId uniqueidentifier,
       @UserId uniqueidentifier,
       @Date datetime,
       @Description nvarchar(max),
       @IssuingAuthority nvarchar(256),
       @IsDelete bit,
       @UpdatedBy uniqueidentifier,
       @UpdatedOn datetime
AS
BEGIN

UPDATE [dbo].[Achievement]
   SET [AchievementId] = @AchievementId
      ,[ProfileId] = @ProfileId
      ,[UserId] = @UserId
      ,[Date] = @Date
      ,[Description] = @Description
      ,[IssuingAuthority] = @IssuingAuthority
      ,[IsDelete] = @IsDelete
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedOn] = @UpdatedOn
 WHERE [AchievementId] = @AchievementId and [ProfileId]= @ProfileId
 
 Update Profile set UpdatedOn = GETUTCDATE() where [ProfileId] = @ProfileId
 
END
GO
/****** Object:  StoredProcedure [dbo].[Solr_Objective]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_Objective] --'58CA38AC-DA88-42E3-B189-5E70C374421B'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
  if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT ProfileId,
 
        ObjectiveDetails as OBObjectiveDetails FROM Objective e  where ProfileId =@ProfileId 
       
      END 

 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_LicenceAndCertifications]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_LicenceAndCertifications] --'58CA38AC-DA88-42E3-B189-5E70C374421B'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 
 if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId,
        STUFF(( SELECT ',' + Name FROM LicenceAndCertifications e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS LCName
       , STUFF(( SELECT ',' + IssuingAuthority  FROM LicenceAndCertifications e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS LCIssuingAuthority
      
       , STUFF(( SELECT ',' + Description FROM LicenceAndCertifications e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS LCDescription
       , MIN(ValidFrom) AS LCStartDateMin
       , MAx(ValidFrom) AS LCStartDateMax
       , MAX(ValidTO) AS LCEndDateMax
       ,Min(ValidTO) AS LCEndDateMin
        ,COUNT(eh.ProfileId) as LCNumberofJobMin
        ,COUNT(eh.ProfileId) as LCNumberofJobMax
FROM  LicenceAndCertifications eh
where ProfileId =@ProfileId AND IsDelete=0 
GROUP BY eh.ProfileId
END


 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_ExecutiveSummary]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_ExecutiveSummary] --'58CA38AC-DA88-42E3-B189-5E70C374421B'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 
 SELECT  ProfileId,
        ExecutiveSummaryDetails as ESExecutiveSummaryDetails FROM ExecutiveSummary e  where ProfileId =@ProfileId 
 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_Availability]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_Availability]-- 'FF1822ED-A813-4D56-8B03-07934DE94E33' --'58CA38AC-DA88-42E3-B189-5E70C374421B'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 
 SELECT 
		ProfileId,
        WillingToWorkOverTime AS AVWillingToWorkOverTime ,
        RelativesWorkingInCompany AS AVRelativesWorkingInCompany,
        RelativesIfYes   AS AVRelativesIfYes  ,
        SubmittedApplicationBefore AS AVSubmittedApplicationBefore ,
        EligibleToWorkInUS AS AVEligibleToWorkInUS,
        _drp.Text AS AVEmploymentPreference,
        TargetIncome AS AVTargetIncomeMax,
        TargetIncome AS AVTargetIncomeMin,
        DateAvailability AS AVDateAvailabilityMin,
        DateAvailability AS AVDateAvailabilityMax,
        HowOld AS AVHowOld
FROM  Availability 
left outer join DrpStringMapping _drp on Availability.EmploymentPreference = _drp.Value AND _drp.DrpName='JobType' AND _drp.LanguageId=@LanguageId
where ProfileId =@ProfileId 




 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_Associations]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_Associations] --'58CA38AC-DA88-42E3-B189-5E70C374421B'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 
if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId,
 
        STUFF(( SELECT ',' + Name FROM Associations e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS ASName
       , STUFF(( SELECT ',' + AssociationType  FROM Associations e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS ASAssociationType
        , STUFF(( SELECT ',' + Role  FROM Associations e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS ASRole
         
           , STUFF(( SELECT ',' + Link  FROM Associations e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS ASLink
           ,Max(EndDate)AS ASEndDateMax
           ,Min(EndDate)AS ASEndDateMin
		,Min(StartDate)AS ASStartDateMin
		,Max(StartDate)AS ASStartDateMax

           
       ,COUNT(eh.ProfileId) as ASNumberofJobMin
       ,COUNT(eh.ProfileId) as ASNumberofJobMax
FROM  Associations eh
where ProfileId =@ProfileId AND IsDelete=0 
GROUP BY eh.ProfileId
END


 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_Achievement]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_Achievement] --'58CA38AC-DA88-42E3-B189-5E70C374421B'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId,
        STUFF(( SELECT ',' + Description FROM Achievement e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS ACDescription 
       , STUFF(( SELECT ',' + IssuingAuthority  FROM Achievement e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS ACIssuingAuthority  
       ,Min(date) AS ACDateMin
       ,Max(date) AS ACDateMax
       
       ,COUNT(eh.ProfileId) as ACNumberofJobMin
       ,COUNT(eh.ProfileId) as ACNumberofJobMax
       
FROM  Achievement eh
where ProfileId =@ProfileId  AND IsDelete=0 
GROUP BY eh.ProfileId
END



END
GO
/****** Object:  StoredProcedure [dbo].[Solr_SpeakingEventHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_SpeakingEventHistory] --'58CA38AC-DA88-42E3-B189-5E70C374421B'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 
  if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId,
        STUFF(( SELECT ',' + Title FROM SpeakingEventHistory e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS SEHTitle 
       , STUFF(( SELECT ',' + EventName  FROM SpeakingEventHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS SEHEventName  
       , STUFF(( SELECT ',' + EventType FROM SpeakingEventHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS SEHEventType 
       , STUFF(( SELECT ',' + Location FROM SpeakingEventHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS SEHLocation
       , STUFF(( SELECT ',' + Link FROM SpeakingEventHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS SEHLink
       
FROM  SpeakingEventHistory eh
where ProfileId =@ProfileId AND IsDelete=0
GROUP BY eh.ProfileId
END


 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_Skills]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_Skills] --'6FE44BAD-9F16-4601-8AE8-08504C25734C'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 
  if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId,
        STUFF(( SELECT ',' + SkillAndQualification FROM Skills e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS SKSkillAndQualification 
       , STUFF(( SELECT ',' + Description  FROM Skills e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS SKDescription
       , STUFF(( SELECT ',' + _entity.LocalName  FROM Skills e 
					Inner join  EntityLanguage _entity on _entity.RecordId =e.SkillType AND _entity.LanguageId=@LanguageId	where ProfileId=@ProfileId FOR XML PATH('')), 1, 1, '') AS SKSkillType
       
       
FROM  Skills eh
where ProfileId =@ProfileId AND IsDelete=0
GROUP BY eh.ProfileId

END

 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_Reference]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_Reference] --'6FE44BAD-9F16-4601-8AE8-08504C25734C'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
 
 if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId,
        STUFF(( SELECT ',' + ReferenceName FROM Reference e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS RFReferenceName
       , STUFF(( SELECT ',' + ReferenceEmail  FROM Reference e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS RFReferenceEmail
       , STUFF(( SELECT ',' + ReferencePhone  FROM Reference e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS RFReferencePhone
        ,COUNT(eh.ProfileId) as RFNumberofJobMin
        ,COUNT(eh.ProfileId) as RFNumberofJobMax
FROM  Reference eh
where ProfileId =@ProfileId AND IsDelete=0
GROUP BY eh.ProfileId

END

 END
GO
/****** Object:  StoredProcedure [dbo].[Solr_PublicationHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Solr_PublicationHistory]-- '58CA38AC-DA88-42E3-B189-5E70C374421B'
 (
 @ProfileId as uniqueidentifier,
 @LanguageId as uniqueidentifier
 )
 AS
 BEGIN
 
  if EXISTS(select ProfileId from Profile where ProfileId=@ProfileId)
 BEGIN
 SELECT eh.ProfileId,
 
        STUFF(( SELECT ',' + Title FROM PublicationHistory e  where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS PHTitle
       , STUFF(( SELECT ',' + Type  FROM PublicationHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS PHType
       , STUFF(( SELECT ',' + Name FROM PublicationHistory e where ProfileId =@ProfileId FOR XML PATH('')), 1, 1, '') AS PHName 
       
FROM  PublicationHistory eh
where ProfileId =@ProfileId AND IsDelete=0
GROUP BY eh.ProfileId

END

 END
GO
/****** Object:  StoredProcedure [dbo].[InsertSpeakingEventHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertSpeakingEventHistory]
@SpeakingEventHistoryId uniqueidentifier OUT
           ,@ProfileId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@Title nvarchar(256)
           ,@StartDate datetime
           ,@EventName nvarchar(256)
           ,@EventType nvarchar(100)
           ,@Location nvarchar(100)
           ,@Link nvarchar(100)
           ,@IsDelete bit
           ,@CreatedBy uniqueidentifier
           ,@CreatedOn datetime
AS
BEGIN

SET @SpeakingEventHistoryId = NEWID()
INSERT INTO [dbo].[SpeakingEventHistory]
           ([SpeakingEventHistoryId]
           ,[ProfileId]
           ,[UserId]
           ,[Title]
           ,[StartDate]
           ,[EventName]
           ,[EventType]
           ,[Location]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
           (@SpeakingEventHistoryId 
           ,@ProfileId 
           ,@UserId 
           ,@Title 
           ,@StartDate 
           ,@EventName 
           ,@EventType 
           ,@Location 
           ,@IsDelete 
           ,@CreatedBy 
           ,@CreatedOn 
           )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUserVacancy]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 25-02-2014
-- Description:	Insert vacancy releated to User in UserVacancy Table
-- =============================================
CREATE PROCEDURE [dbo].[InsertUserVacancy]
	@UserVacanyId uniqueidentifier output,
	@UserId uniqueidentifier,
	@VacancyId uniqueidentifier,
	@ClientId uniqueidentifier,
	@LanguageId uniqueidentifier,
	@IsDelete bit,
	@CreatedBy uniqueidentifier,
	@CreatedOn datetime
AS
BEGIN
	set @UserVacanyId = NEWID();

	Insert into UserVacancy
	(UserVacancyId,UserId,VacancyId,ClientId,LanguageId,IsDelete,VersionNo,CreatedBy,CreatedOn)
	values(@UserVacanyId,@UserId,@VacancyId,@ClientId,@LanguageId,@IsDelete,default,@CreatedBy,@CreatedOn)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUserSecurityRole]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertUserSecurityRole] 
@UserSecurityRoleId uniqueidentifier OUT,
@UserId uniqueidentifier ,
@ClientId uniqueidentifier,
@ATSSecurityRole nvarchar(256) = null,
@IsDelete bit,
@CreatedOn datetime=null,
@CreatedBy uniqueidentifier=null,
@ATSSecurityRoleId uniqueidentifier = null

AS
BEGIN
--declare @ATSSecurityRoleId uniqueidentifier
If @ATSSecurityRoleId is null
begin
select @ATSSecurityRoleId  = ATSSecurityRoleId from 
[ATSSecurityRole] where [DefaultName]=@ATSSecurityRole
end

SET @UserSecurityRoleId = newid();

INSERT INTO [UserSecurityRole]
           ([UserSecurityRoleId]
			,[ClientId]
			,[UserId]
			,[ATSSecurityRoleId]
			,[IsDelete]
			,[CreatedOn]
			,[CreatedBy]
           )
     VALUES
           (@UserSecurityRoleId
           ,@ClientId
           ,@UserId
           ,@ATSSecurityRoleId
           ,@IsDelete
           ,@CreatedOn
           ,@CreatedBy
           )
           
           
          
  
  
           END
GO
/****** Object:  StoredProcedure [dbo].[RecordExistsForSkillType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordExistsForSkillType] --'1EBC64D7-BD32-4F46-8CB4-46F0204632E7',0
@SkillTypeId uniqueidentifier

As
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [TableName] nvarchar(256)
	  ,[Count] int
)
Declare @CountSkills as int 
Declare @CountEntityLanguage as int 
Begin
Select @CountSkills =  COUNT(*)  from [dbo].Skills where SkillType = @SkillTypeId 
Select @CountEntityLanguage =  COUNT(*)  from [dbo].EntityLanguage where RecordId = @SkillTypeId 

insert into @tbl ([TableName],[Count]) values('Skills',@CountSkills)
insert into @tbl ([TableName],[Count]) values('EntityLanguage',@CountEntityLanguage)
select * from @tbl

End
GO
/****** Object:  StoredProcedure [dbo].[RecordExistsForPositionType]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordExistsForPositionType] --'3779B086-C471-414C-8726-60ADA535C0F7'
@PositionTypeId uniqueidentifier 

As
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [TableName] nvarchar(256)
	  ,[Count] int
)

Declare @CountVacancy as int 
Declare @CountApplication as int 
Declare @CountUserVacancy as int 

Declare @VacancyId as nvarchar(max)
Begin


Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.PositionTypeId = @PositionTypeId  and IsDelete = 0 FOR XML PATH('')), 1, 1, '')   
,@CountVacancy =  COUNT(VacancyId) FROM  vacancy  where  PositionTypeId = @PositionTypeId AND IsDelete = 0
group by PositionTypeId 

select @CountApplication = COUNT(ApplicationId) from Application where Cast(VacancyId as nvarchar(40)) in (@VacancyId)
select @CountUserVacancy = COUNT(UserVacancyId) from UserVacancy where Cast(VacancyId as nvarchar(40)) in (@VacancyId)

insert into @tbl ([TableName],[Count]) values('Vacancy',@CountVacancy)
insert into @tbl ([TableName],[Count]) values('Applicants',@CountApplication)
insert into @tbl ([TableName],[Count]) values('User Saved Vacancy',@CountUserVacancy)

select * from @tbl
End
GO
/****** Object:  StoredProcedure [dbo].[RecordExistsForJobLocation]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordExistsForJobLocation] --'09CDCD42-1367-4FD9-AEBF-940338F9E336'
@JobLocationId uniqueidentifier

As
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [TableName] nvarchar(256)
	  ,[Count] int
)
 
Declare @CountVacancy as int 
Declare @CountApplication as int 
Declare @CountUserVacancy as int 

Declare @VacancyId as nvarchar(max)
Begin

--Select @CountVacancy =  COUNT(VacancyId)  from [dbo].Vacancy where Location = @JobLocationId and IsDelete = 0
Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.Location = @JobLocationId  and IsDelete = 0 FOR XML PATH('')), 1, 1, '')   
,@CountVacancy =  COUNT(VacancyId) FROM  vacancy  where  Location = @JobLocationId AND IsDelete = 0
group by Location 

select @CountApplication = COUNT(ApplicationId) from Application where Cast(VacancyId as nvarchar(40)) in (@VacancyId)
select @CountUserVacancy = COUNT(UserVacancyId) from UserVacancy where Cast(VacancyId as nvarchar(40)) in (@VacancyId)

insert into @tbl ([TableName],[Count]) values('Vacancy',@CountVacancy)
insert into @tbl ([TableName],[Count]) values('Applicants',@CountApplication)
insert into @tbl ([TableName],[Count]) values('User Saved Vacancy',@CountUserVacancy)


select * from @tbl
End
GO
/****** Object:  StoredProcedure [dbo].[RecordExistsForEmployeeVacancy]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordExistsForEmployeeVacancy] --'9B39908D-4A23-4A0C-BB91-F105F3C34921'
@VacancyId uniqueidentifier

As
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [TableName] nvarchar(256)
	  ,[Count] int
)

Declare @CountVacancy as int 
Declare @CountApplication as int 
Declare @CountUserVacancy as int 

Begin
Select @CountVacancy =  COUNT(VacancyId)  from [dbo].vacancy where VacancyId = @VacancyId
select @CountApplication = COUNT(ApplicationId) from Application where Cast(VacancyId as nvarchar(40)) in (@VacancyId)
select @CountUserVacancy = COUNT(UserVacancyId) from UserVacancy where Cast(VacancyId as nvarchar(40)) in (@VacancyId)

insert into @tbl ([TableName],[Count]) values('Vacancy',@CountVacancy)
insert into @tbl ([TableName],[Count]) values('Applicants',@CountApplication)
insert into @tbl ([TableName],[Count]) values('User Saved Vacancy',@CountUserVacancy)


select * from @tbl
End
GO
/****** Object:  StoredProcedure [dbo].[RecordExistsForEmployeeUser]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordExistsForEmployeeUser]--'64F5D4EB-AA57-40D3-BE6A-900EC57B19A1'
@UserId uniqueidentifier

As
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [TableName] nvarchar(256)
	  ,[Count] int
)

Declare @CountUserDivisionPermission as int 
Declare @CountAdHocPrivilegeId as int
Declare @CountUserSecurityRoleId as int
Declare @CountSaveResumeSearchId as int
Declare @CountUserVacancyId as int
Declare @CountCreatedBy as int
Declare @CountBlockCandidateId as int
Declare @CountSearchId as int
Declare @CountVacancy as int


Declare @VacancyId as nvarchar(max)


Begin
Select @CountUserDivisionPermission =  COUNT(UserDivisionPermissionId)  from [dbo].UserDivisionPermission where DivisionId = @UserId
Select @CountAdHocPrivilegeId =  COUNT(AdHocPrivilegeId)  from [dbo].AdHocPrivilege where UserId = @UserId
Select @CountUserSecurityRoleId =  COUNT(UserSecurityRoleId)  from [dbo].UserSecurityRole where UserId = @UserId
Select @CountSaveResumeSearchId =  COUNT(SaveResumeSearchId)  from [dbo].SaveResumeSearch where UserId = @UserId
Select @CountUserVacancyId =  COUNT(UserVacancyId)  from [dbo].UserVacancy where UserId = @UserId
Select @CountCreatedBy =  COUNT(CreatedBy)  from [dbo].[Application] where CreatedBy = @UserId
Select @CountBlockCandidateId =  COUNT(BlockCandidateId)  from [dbo].[BlockCandidate] where UserId = @UserId
Select @CountSearchId =  COUNT(SearchId)  from [dbo].Search where UserId = @UserId

Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.CreatedBy = @UserId  and IsDelete = 0 FOR XML PATH('')), 1, 1, '')   
,@CountVacancy =  COUNT(CreatedBy) FROM  vacancy  where  CreatedBy = @UserId AND IsDelete = 0
group by CreatedBy 


insert into @tbl ([TableName],[Count]) values('User Division Permission',@CountUserDivisionPermission)
insert into @tbl ([TableName],[Count]) values('Ad Hoc Privilege',@CountAdHocPrivilegeId)
insert into @tbl ([TableName],[Count]) values('User Security Role',@CountUserSecurityRoleId)
insert into @tbl ([TableName],[Count]) values('Save Resume Search',@CountSaveResumeSearchId)
insert into @tbl ([TableName],[Count]) values('UserVacancy',@CountUserVacancyId)
insert into @tbl ([TableName],[Count]) values('Users',@CountCreatedBy)
insert into @tbl ([TableName],[Count]) values('Block Candidate',@CountBlockCandidateId)
insert into @tbl ([TableName],[Count]) values('Search',@CountSearchId)
insert into @tbl ([TableName],[Count]) values('Vacancy',@CountVacancy)

select * from @tbl
End
GO
/****** Object:  StoredProcedure [dbo].[RecordExistsForDivision]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[RecordExistsForDivision] --'B9D2BFA6-8AF6-4DFB-991C-52DB438F0679'
@DivisionId uniqueidentifier

As
SET NOCOUNT ON;
declare @tbl TABLE 
(      
	   [TableName] nvarchar(256)
	  ,[Count] int
)
Declare @CountUserDivisionPermission as int 
Declare @CountDivisionPositionType as int
Declare @CountVacancy as int 
Declare @CountApplication as int 
Declare @CountUserVacancy as int 
Declare @CountUserId as int

Declare @VacancyId as nvarchar(max)
Declare @UserId as nvarchar(max)
Begin
Select @CountUserDivisionPermission =  COUNT(UserDivisionPermissionId)  from [dbo].UserDivisionPermission where DivisionId = @DivisionId
Select @CountDivisionPositionType =  COUNT(DivisionPositionTypeId)  from [dbo].DivisionPositionType where DivisionId = @DivisionId

Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.DivisionId = @DivisionId  and IsDelete = 0 FOR XML PATH('')), 1, 1, '')   
,@CountVacancy =  COUNT(VacancyId) FROM  vacancy  where  DivisionId = @DivisionId AND IsDelete = 0
group by DivisionId 

select @CountApplication = COUNT(ApplicationId) from Application where Cast(VacancyId as nvarchar(40)) in (@VacancyId)
select @CountUserVacancy = COUNT(UserVacancyId) from UserVacancy where Cast(VacancyId as nvarchar(40)) in (@VacancyId)

Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.DivisionId = @DivisionId  and IsDelete = 0 FOR XML PATH('')), 1, 1, '')   
,@CountVacancy =  COUNT(VacancyId) FROM  vacancy  where  DivisionId = @DivisionId AND IsDelete = 0
group by DivisionId 

Select @UserId = STUFF(( SELECT ',' + CAST(CreatedBy as nvarchar(40)) FROM Division  _D  where _D.DivisionId = @DivisionId and IsDelete = 0  FOR XML PATH('')), 1, 1, '')     
,@CountUserId =  COUNT(UserId) FROM  Users  where DivisionId = @DivisionId AND IsDelete = 0  
group by DivisionId  

insert into @tbl ([TableName],[Count]) values('UserDivisionPermission',@CountUserDivisionPermission)
insert into @tbl ([TableName],[Count]) values('DivisionPositionType',@CountDivisionPositionType)
insert into @tbl ([TableName],[Count]) values('Vacancy',@CountVacancy)
insert into @tbl ([TableName],[Count]) values('Applicants',@CountApplication)
insert into @tbl ([TableName],[Count]) values('User Saved Vacancy',@CountUserVacancy)
insert into @tbl ([TableName],[Count]) values('Users',@CountUserId)

select * from @tbl
End
GO
/****** Object:  StoredProcedure [dbo].[InsertSkills]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertSkills]
 @SkillsId as uniqueidentifier OUT
 ,@ProfileId uniqueidentifier
  ,@UserId as uniqueidentifier
           ,@SkillAndQualification nvarchar(100)
           ,@SkillType nvarchar(100)
           ,@Proficiency int
           ,@Level int
           ,@LastUsed nvarchar(100)
           ,@Experience int
          
           ,@Description nvarchar(max)
           ,@IsDelete bit
           
           ,@Createdon datetime = null
           
           ,@CreatedBy uniqueidentifier = null

		   AS
		   BEGIN

		   SET @SkillsId = newid();
		   INSERT INTO [dbo].[Skills]
           ([SkillsId]
		   ,[ProfileId]
           ,[UserId]
           ,[SkillAndQualification]
           ,[SkillType]
           ,[Proficiency]
           ,[Level]
           ,[LastUsed]
           ,[Experience]
         
           ,[Description]
           ,[IsDelete]
           
           ,[Createdon]
           
           ,[CreatedBy])
     VALUES
           (@SkillsId
		   ,@ProfileId
           ,@UserId
           ,@SkillAndQualification
           ,@SkillType
           ,@Proficiency
           ,@Level
           ,@LastUsed
           ,@Experience
           ,@Description
           ,@IsDelete
           
           ,@Createdon
           
           ,@CreatedBy)
		   END
GO
/****** Object:  StoredProcedure [dbo].[InsertSecurityRoleByUser]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 02-03-2014
-- Description:	Save Security Role by User in UserSecurityRole
-- =============================================
CREATE PROCEDURE [dbo].[InsertSecurityRoleByUser]
	@UserSecurityRoleId uniqueidentifier Output,
	@ClientId uniqueidentifier,
	@UserId uniqueidentifier,
	@ATSSecurityRoleId uniqueidentifier,
	@IsDelete bit,
	@CreatedBy uniqueidentifier,
	@CreatedOn datetime
AS
BEGIN
	set @UserSecurityRoleId = NEWID();
	insert into UserSecurityRole
	(UserSecurityRoleId,ClientId,UserId,ATSSecurityRoleId,IsDelete,CreatedOn,CreatedBy,VersionNo)
	values(@UserSecurityRoleId,@ClientId,@UserId,@ATSSecurityRoleId,@IsDelete,@CreatedOn,@CreatedBy,default)

END
GO
/****** Object:  StoredProcedure [dbo].[InsertReference]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertReference]
@ReferenceId uniqueidentifier OUT
,@ProfileId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@ReferenceName nvarchar(100)
           ,@Relationship nvarchar(100)
           ,@ReferencePhone nvarchar(100)
           ,@ReferenceEmail nvarchar(100)
           ,@IsDelete bit
           ,@CreatedOn datetime = null
           
           ,@CreatedBy uniqueidentifier = null 

		   AS
		   BEGIN
		   SET @ReferenceId = newid();

		   INSERT INTO [dbo].[Reference]
           ([ReferenceId]
		   ,[ProfileId]
           ,[UserId]
           ,[ReferenceName]
           ,[Relationship]
           ,[ReferencePhone]
           ,[ReferenceEmail]
           ,[IsDelete]
           
           ,[CreatedOn]
           
           ,[CreatedBy])
     VALUES
           (@ReferenceId
		   ,@ProfileId
           ,@UserId
           ,@ReferenceName
           ,@Relationship
           ,@ReferencePhone
           ,@ReferenceEmail
           ,@IsDelete
           
		   ,@CreatedOn
		   
           ,@CreatedBy)
		   END
GO
/****** Object:  StoredProcedure [dbo].[InsertPublicationHistory]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPublicationHistory]

@PublicationHistoryId uniqueidentifier OUT
           ,@ProfileId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@Title nvarchar(100)
           ,@Type nvarchar(100)
           ,@Role nvarchar(100)
           ,@Name nvarchar(100)
           ,@PublicationDate datetime
           ,@Description nvarchar(max)
           ,@Comments nvarchar(max)
           ,@IsDelete bit
           ,@CreatedBy uniqueidentifier
           ,@CreatedOn datetime
           

AS
BEGIN
SET  @PublicationHistoryId = NEWID()
INSERT INTO [dbo].[PublicationHistory]
           ([PublicationHistoryId]
           ,[ProfileId]
           ,[UserId]
           ,[Title]
           ,[Type]
           ,[Role]
           ,[Name]
           ,[PublicationDate]
           ,[Description]
           ,[Comments]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
           (@PublicationHistoryId
           ,@ProfileId
           ,@UserId
           ,@Title
           ,@Type
           ,@Role
           ,@Name
           ,@PublicationDate
           ,@Description
           ,@Comments
           ,@IsDelete
           ,@CreatedBy
           ,@CreatedOn
           )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertProject]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertProject]
@ProjectId uniqueidentifier OUT
           ,@EmploymentHistoryId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@ProfileId uniqueidentifier
           ,@ProjectName nvarchar(100)
           ,@TeamSize int
           ,@Description nvarchar(max)
           ,@IsDelete bit
           ,@CreatedBy uniqueidentifier = null	
           ,@CreatedOn datetime = null
        
AS
BEGIN
SET @ProjectId = newid();
INSERT INTO [dbo].[Project]
           ([ProjectId]
           ,[UserId]
           ,[ProfileId]
           ,[EmploymentHistoryId]
           ,[ProjectName]
           ,[TeamSize]
           ,[Description]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
          )
     VALUES
           (@ProjectId
           ,@UserId
           ,@ProfileId
           ,@EmploymentHistoryId
           ,@ProjectName
           ,@TeamSize
           ,@Description
           ,@IsDelete
           ,@CreatedBy
           ,@CreatedOn
           )

END
GO
/****** Object:  StoredProcedure [dbo].[InsertObjective]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[InsertObjective]
@ObjectiveId uniqueidentifier OUT,
@ProfileId uniqueidentifier,
@UserId uniqueidentifier,
@ObjectiveDetails nvarchar(MAX),
@IsDelete bit,
@CreatedBy uniqueidentifier,
@CreatedOn Datetime 

AS
BEGIN
SET @ObjectiveId = NEWID()

INSERT INTO [dbo].[Objective]
           ([ObjectiveId]
           ,[ProfileId]
           ,[UserId]
           ,[ObjectiveDetails]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
           (@ObjectiveId
           ,@ProfileId
           ,@UserId
           ,@ObjectiveDetails 
           ,@IsDelete 
           ,@CreatedBy 
           ,@CreatedOn)



END
GO
/****** Object:  StoredProcedure [dbo].[InsertLicenceAndCertifications]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertLicenceAndCertifications]
@LicenceAndCertificationsId uniqueidentifier OUT
           ,@ProfileId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@Name nvarchar(256)
           ,@IssuingAuthority nvarchar(256)
           ,@Description nvarchar(max)
           ,@ValidFrom datetime
           ,@ValidTo datetime
           ,@IsDelete bit
           ,@CreatedBy uniqueidentifier
           ,@CreatedOn datetime
AS
BEGIN
SET @LicenceAndCertificationsId = NEWID()
INSERT INTO [dbo].[LicenceAndCertifications]
           ([LicenceAndCertificationsId]
           ,[ProfileId]
           ,[UserId]
           ,[Name]
           ,[IssuingAuthority]
           ,[Description]
           ,[ValidFrom]
           ,[ValidTo]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
           (@LicenceAndCertificationsId
           ,@ProfileId
           ,@UserId
           ,@Name
           ,@IssuingAuthority
           ,@Description
           ,@ValidFrom
           ,@ValidTo
           ,@IsDelete
           ,@CreatedBy
           ,@CreatedOn
          )
END
GO
/****** Object:  StoredProcedure [dbo].[InsertLanguages]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertLanguages]

@LanguagesId uniqueidentifier OUT
           ,@ProfileId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@LanguageCode nvarchar(50)
           ,@Read bit
           ,@Write bit
           ,@Speak bit
           ,@Comments nvarchar(max)
           ,@IsDelete bit
           ,@CreatedBy uniqueidentifier
           ,@CreatedOn datetime
AS
BEGIN

SET  @LanguagesId = NEWID()
INSERT INTO [dbo].[Languages]
           ([LanguagesId]
           ,[ProfileId]
           ,[UserId]
           ,[LanguageCode]
           ,[Read]
           ,[Write]
           ,[Speak]
           ,[Comments]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
           (@LanguagesId 
           ,@ProfileId 
           ,@UserId 
           ,@LanguageCode 
           ,@Read 
           ,@Write 
           ,@Speak 
           ,@Comments 
           ,@IsDelete 
           ,@CreatedBy 
           ,@CreatedOn )
           
END
GO
/****** Object:  StoredProcedure [dbo].[InsertExecutiveSummary]    Script Date: 07/11/2014 17:23:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertExecutiveSummary]
@ExecutiveSummaryId uniqueidentifier OUT,
@ProfileId uniqueidentifier,
@UserId uniqueidentifier,
@ExecutiveSummaryDetails nvarchar(MAX),
@CreatedBy uniqueidentifier,
@CreatedOn datetime,
@IsDelete bit

AS
BEGIN

SET @ExecutiveSummaryId = NEWID()
INSERT INTO [dbo].[ExecutiveSummary]
           ([ExecutiveSummaryId]
           ,[ProfileId]
           ,[UserId]
           ,[ExecutiveSummaryDetails]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (@ExecutiveSummaryId
           ,@ProfileId
           ,@UserId
           ,@ExecutiveSummaryDetails
           ,@IsDelete
           ,@CreatedBy
           ,@CreatedOn
           )




END
GO
/****** Object:  StoredProcedure [dbo].[InsertAvailability]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertAvailability]
@AvailibilityId uniqueidentifier OUT
           ,@ProfileId uniqueidentifier
           ,@DateAvailability datetime = null
           ,@TargetIncome nvarchar(100) = null
           ,@HrsMon nvarchar(100) = null
           ,@HrsTue nvarchar(100) = null
           ,@HrsWed nvarchar(100) = null
           ,@HrsThu nvarchar(100) = null
           ,@HrsFri nvarchar(100) = null
           ,@HrsSat nvarchar(100) = null
           ,@HrsSun nvarchar(100) = null
           ,@EmploymentPreference int = null
           ,@WillingToWorkOverTime bit = null
           ,@RelativesWorkingInCompany bit = null
           ,@RelativesIfYes nvarchar(100) = null
           ,@SubmittedApplicationBefore bit = null
           ,@AppicationSubmision nvarchar(256) = null
           ,@HearAboutThePosition nvarchar(100) = null
           ,@ReferredBy nvarchar(100) = null
           ,@HowOld bit = null
           ,@EligibleToWorkInUS  bit = null
           ,@IsDelete bit
           ,@CreatedBy uniqueidentifier = null
           ,@CreatedOn datetime = null 
           ,@UpdatedBy uniqueidentifier = null 
           ,@updatedOn datetime = null 
           AS
BEGIN
SET @AvailibilityId = newid();
INSERT INTO [dbo].[Availability]
           ([AvailibilityId]
           ,[ProfileId]
           ,[DateAvailability]
           ,[TargetIncome]
           ,[HrsMon]
           ,[HrsTue]
           ,[HrsWed]
           ,[HrsThu]
           ,[HrsFri]
           ,[HrsSat]
           ,[HrsSun]
           ,[EmploymentPreference]
           ,[WillingToWorkOverTime]
           ,[RelativesWorkingInCompany]
           ,[RelativesIfYes]
           ,[SubmittedApplicationBefore]
           ,[AppicationSubmision]
           ,[HearAboutThePosition]
           ,[ReferredBy]
           ,[HowOld]
           ,[EligibleToWorkInUS]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[UpdatedBy]
           ,[updatedOn])
     VALUES
           (@AvailibilityId
           ,@ProfileId
           ,@DateAvailability
           ,@TargetIncome
           ,@HrsMon
           ,@HrsTue
           ,@HrsWed
           ,@HrsThu
           ,@HrsFri
           ,@HrsSat
           ,@HrsSun
           ,@EmploymentPreference
           ,@WillingToWorkOverTime
           ,@RelativesWorkingInCompany
		   ,@RelativesIfYes
           ,@SubmittedApplicationBefore
           ,@AppicationSubmision
           ,@HearAboutThePosition
           ,@ReferredBy 
           ,@HowOld 
           ,@EligibleToWorkInUS
           ,@IsDelete
           ,@CreatedBy
		   ,@CreatedOn
           ,@UpdatedBy
           ,@updatedOn)


END
GO
/****** Object:  StoredProcedure [dbo].[InsertAssociations]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertAssociations]
@AssociationsId uniqueidentifier OUT
           ,@ProfileId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@Name nvarchar(100)
           ,@StartDate datetime
           ,@EndDate datetime
           ,@Role nvarchar(100)
           ,@Link nvarchar(100)
           ,@IsDelete bit
           ,@CreatedBy uniqueidentifier
           ,@CreatedOn datetime
           ,@AssociationType nvarchar(100)

AS
BEGIN
SET @AssociationsId = NEWID()

INSERT INTO [dbo].[Associations]
           ([AssociationsId]
           ,[ProfileId]
           ,[UserId]
           ,[Name]
           ,[StartDate]
           ,[EndDate]
           ,[Role]
           ,[Link]
           ,[AssociationType]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
           (@AssociationsId
           ,@ProfileId
           ,@UserId
           ,@Name
           ,@StartDate
           ,@EndDate
           ,@Role
           ,@Link
           ,@AssociationType
           ,@IsDelete
           ,@CreatedBy
           ,@CreatedOn
          )


END
GO
/****** Object:  StoredProcedure [dbo].[InsertApplication]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 14-03-2014
-- Description:	Apply for job.Insert data into Application table with vacancyId,ResumeId an
--              CoverLetterId which is mapped in Resume Table.Here ATSResumeId used for Resume and CoverLetter Id
-- =============================================
CREATE PROCEDURE [dbo].[InsertApplication]
	@ApplicationId uniqueidentifier out,
	@ATSResumeId uniqueidentifier,
	@ATSCoverLetterId uniqueidentifier,
	@VacancyId uniqueidentifier,
	@LanguageId uniqueidentifier,
	@IsDelete bit,
	@CreatedBy uniqueidentifier,
	@CreatedOn datetime
AS
Declare @VacancyStatus uniqueidentifier
select @VacancyStatus = StringMappingId from StringMapping where Lower(KeyName) = 'draft'
BEGIN
set @ApplicationId = NEWID()

Insert into [Application]
(ApplicationId,ATSResumeId,ATSCoverLetterId,VacancyId,VacancyStatus,LanguageId,IsDelete,VersionNo,CreatedBy,CreatedOn)
values
(@ApplicationId,@ATSResumeId,@ATSCoverLetterId,@VacancyId,@VacancyStatus,@LanguageId,@IsDelete,default,@CreatedBy,@CreatedOn)

if exists(select 1 from UserVacancy where VacancyId = @VacancyId)
begin
  delete from UserVacancy where VacancyId = @VacancyId
end

END
GO
/****** Object:  StoredProcedure [dbo].[InsertAdHocPrivilege]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 02-03-2014
-- Description:	Insert Adhoc roles by user and client in AdHocPrivilege table
-- =============================================
CREATE PROCEDURE [dbo].[InsertAdHocPrivilege]
	@AdHocPrivilegeId uniqueidentifier output,
	@ClientId uniqueidentifier,
	@UserId uniqueidentifier,
	@ATSPrivilegeId uniqueidentifier,
	@PermissionType nvarchar(100),
	@Scope nvarchar(100),
	@IsDelete bit,
	@CreatedOn datetime,
	@CreatedBy uniqueidentifier
AS
BEGIN
	set @AdHocPrivilegeId = NEWID();
	insert into AdHocPrivilege
	(AdHocPrivilegeId,ClientId,UserId,ATSPrivilegeId,PermissionType,Scope,IsDelete,CreatedBy,CreatedOn,VersionNo)
	values(@AdHocPrivilegeId,@ClientId,@UserId,@ATSPrivilegeId,@PermissionType,@Scope,@IsDelete,@CreatedBy,@CreatedOn,default)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertAchievement]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertAchievement]
@AchievementId uniqueidentifier OUT
           ,@ProfileId uniqueidentifier
           ,@UserId uniqueidentifier
           ,@Date datetime
           ,@Description nvarchar(max)
           ,@IssuingAuthority nvarchar(256)
           ,@IsDelete bit
           ,@CreatedBy uniqueidentifier
           ,@CreatedOn datetime
AS
BEGIN
SET @AchievementId = NEWID()

INSERT INTO [dbo].[Achievement]
           ([AchievementId]
           ,[ProfileId]
           ,[UserId]
           ,[Date]
           ,[Description]
           ,[IssuingAuthority]
           ,[IsDelete]
           ,[CreatedBy]
           ,[CreatedOn]
           )
     VALUES
           (@AchievementId
           ,@ProfileId
           ,@UserId
           ,@Date
           ,@Description
           ,@IssuingAuthority
           ,@IsDelete
           ,@CreatedBy
           ,@CreatedOn
        )
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserSecurityRoleByUserId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserSecurityRoleByUserId]
@UserId uniqueidentifier

AS
BEGIN

select [UserSecurityRoleId]
      ,[UserId]
      ,_UserSecurityRole.[ATSSecurityRoleId]
	  ,_ATSSecurityRole.DefaultName
	  from 
		UserSecurityRole _UserSecurityRole
			inner join ATSSecurityRole _ATSSecurityRole 
					on _ATSSecurityRole .ATSSecurityRoleId = _UserSecurityRole.ATSSecurityRoleId 
						And _ATSSecurityRole.IsDelete = 0
	  Where UserId = @UserId And  _UserSecurityRole.IsDelete = 0
	  order by _ATSSecurityRole.SequenceNo asc


END
GO
/****** Object:  StoredProcedure [dbo].[GetSpeakingEventHistoryByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSpeakingEventHistoryByProfile]
@ProfileId uniqueidentifier,
@IsDelete bit
AS
BEGIN

SELECT [SpeakingEventHistoryId]
      ,[ProfileId]
      ,[UserId]
      ,[Title]
      ,[StartDate]
      ,[EventName]
      ,[EventType]
      ,[Location]
      ,[IsDelete]
      ,[CreatedBy]
      ,[CreatedOn]
      
  FROM [dbo].[SpeakingEventHistory]
WHERE [ProfileId] = @ProfileId And IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetSkillsByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSkillsByProfile] 
@ProfileId uniqueidentifier,
@IsDelete bit

AS
BEGIN

SELECT [SkillsId]
      ,[ProfileId]
      ,[UserId]
      ,[SkillAndQualification]
      ,[SkillType]
      ,[Proficiency]
      ,[Level]
      ,[LastUsed]
      ,[Experience]
      ,[Description]
      ,[UpdatedOn]
      ,[Createdon]
      ,[UpdatedBy]
      ,[CreatedBy]
   
  FROM [dbo].[Skills]
  WHERE ProfileId = @ProfileId And IsDelete = @IsDelete
  END
GO
/****** Object:  StoredProcedure [dbo].[GetSisterDivisionUsersByUserId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSisterDivisionUsersByUserId] --'0A007CAD-B148-429C-8C46-4758324954E8'
@UserId uniqueidentifier



AS
BEGIN
 SELECT  Distinct(DivisionId)from 
 vUserDivision
  where [ParentDivisionId] in 
  (select _vUserDivision.[ParentDivisionId] FROM vUserDivision _vUserDivision
	where _vUserDivision.UserId = @UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[GetSecurityRolePrivilageByPrivilageAndRole]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSecurityRolePrivilageByPrivilageAndRole]-- 'Application','SystemAdministrator'
@Userid uniqueidentifier,
@ATSPrivilage nvarchar(256), -- E.G Application,Candidate
@ATSSecurityRole nvarchar(256) -- E.G Division Manager,Location Manager

AS
BEGIN

Select SecurityRolePrivilegeId,@ATSPrivilage as ATSPrivilage,@ATSSecurityRole as ATSSecurityRole,
		[PermissionType],[Scope] 
From SecurityRolePrivilege _SecurityRolePrivilege
		inner join ATSSecurityRole _ATSSecurityRole on _SecurityRolePrivilege.AtsSecurityRoleId = _ATSSecurityRole.ATSSecurityRoleId 
					AND _ATSSecurityRole.DefaultName = @ATSSecurityRole And _ATSSecurityRole.IsDelete = 0
		inner join ATSPrivilege _ATSPrivilege on _SecurityRolePrivilege.ATSPrivilegeId = _ATSPrivilege.ATSPrivilegeId 
					AND _ATSPrivilege.Name = @ATSPrivilage And _ATSPrivilege.IsDelete = 0
where _SecurityRolePrivilege.IsChecked=1 And _SecurityRolePrivilege.IsDelete = 0
union
SELECT	[AdHocPrivilegeId] as SecurityRolePrivilegeId,
		@ATSPrivilage as ATSPrivilage,@ATSSecurityRole as ATSSecurityRole,
		[PermissionType],[Scope]
  FROM [AdHocPrivilege] _AdHocPrivilege 
  inner join ATSPrivilege _ATSPrivilege on _AdHocPrivilege.ATSPrivilegeId = _ATSPrivilege.ATSPrivilegeId 
			AND _ATSPrivilege.Name = @ATSPrivilage AND _AdHocPrivilege.UserId = @Userid 
			And _ATSPrivilege.IsDelete = 0 And _AdHocPrivilege.IsDelete = 0 

 
 


END
GO
/****** Object:  StoredProcedure [dbo].[GetSecurityRoleByUserAndClient]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 05-03-2014
-- Description:	Get all SecurityRole from UserSecurityRole table by UserId and ClientId
-- =============================================
CREATE PROCEDURE [dbo].[GetSecurityRoleByUserAndClient]
	@UserId uniqueidentifier,
	@ClientId uniqueidentifier,
	@IsDelete bit,
	@LanguageId uniqueidentifier
AS
BEGIN
  select USR.ATSSecurityRoleId,EL.LocalName
  from UserSecurityRole USR
  inner join EntityLanguage EL on EL.RecordId = USR.ATSSecurityRoleId and EL.LanguageId = @LanguageId 
			and USR.ClientId = @ClientId and USR.UserId = @UserId and USR.IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetReferenceByprofileId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetReferenceByprofileId]
@ProfileId uniqueidentifier,
@IsDelete bit

AS
BEGIN


SELECT [ReferenceId]
      ,[ProfileId]
      ,[UserId]
      ,[ReferenceName]
      ,[Relationship]
      ,[ReferencePhone]
      ,[ReferenceEmail]
	  
    
  FROM [dbo].[Reference]
  WHERE [ProfileId] = @ProfileId And IsDelete = @IsDelete


END
GO
/****** Object:  StoredProcedure [dbo].[GetPublicationHistoryByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPublicationHistoryByProfile]
@ProfileId uniqueidentifier,
@IsDelete bit


AS
BEGIN

SELECT [PublicationHistoryId]
      ,[ProfileId]
      ,[UserId]
      ,[Title]
      ,[Type]
      ,[Role]
      ,[Name]
      ,[PublicationDate]
      ,[Description]
      ,[Comments]
      ,[IsDelete]
      ,[CreatedBy]
      ,[CreatedOn]
     
  FROM [dbo].[PublicationHistory]
Where [ProfileId] = @ProfileId And IsDelete = @IsDelete
End
GO
/****** Object:  StoredProcedure [dbo].[GetProject]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProject]
@EmploymentHistoryId uniqueidentifier,
@IsDelete bit


AS 
BEGIN
SELECT [ProjectId]
      ,[EmploymentHistoryId]
      ,[UserId]
      ,[ProfileId]
      ,[ProjectName]
      ,[TeamSize]
      ,[Description]
     
  FROM [dbo].[Project]
  WHERE  EmploymentHistoryId =@EmploymentHistoryId  and IsDelete = @IsDelete
  END
GO
/****** Object:  StoredProcedure [dbo].[GetObjectiveByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetObjectiveByProfile]
@ProfileId uniqueidentifier,
@Isdelete bit
AS
BEGIN

SELECT [ObjectiveId]
      ,[ProfileId]
      ,[UserId]
      ,[ObjectiveDetails]
      ,[IsDelete]
      ,[CreatedBy]
      ,[CreatedOn]
      
  FROM [dbo].[Objective]
  
  Where ProfileId = @ProfileId and IsDelete = @Isdelete 

END
GO
/****** Object:  StoredProcedure [dbo].[GetLicenceAndCertificationsByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLicenceAndCertificationsByProfile]
@ProfileId uniqueidentifier,
@IsDelete bit
AS
BEGIN
SELECT [LicenceAndCertificationsId]
      ,[ProfileId]
      ,[UserId]
      ,[Name]
      ,[IssuingAuthority]
      ,[Description]
      ,[ValidFrom]
      ,[ValidTo]
      ,[IsDelete]
      ,[CreatedBy]
      ,[CreatedOn]
      
  FROM [dbo].[LicenceAndCertifications] 
  Where [ProfileId] = @ProfileId and IsDelete = @IsDelete
  END
GO
/****** Object:  StoredProcedure [dbo].[GetLanguagesByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLanguagesByProfile]
@ProfileId uniqueidentifier,
@IsDelete bit

AS
BEGIN
SELECT [LanguagesId]
      ,[ProfileId]
      ,[UserId]
      ,[LanguageCode]
      ,[Read]
      ,[Write]
      ,[Speak]
      ,[Comments]
      ,[IsDelete]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[VersionNo]
  FROM [dbo].[Languages]

WHERE [ProfileId] = @ProfileId and IsDelete = 0

END
GO
/****** Object:  StoredProcedure [dbo].[GetChildDivisionUsersByUserId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetChildDivisionUsersByUserId] --'0A007CAD-B148-429C-8C46-4758324954E8'
@UserId uniqueidentifier



AS
BEGIN
SELECT  Distinct(DivisionId) from [vUserDivision]
  where [ParentDivisionId] in (select _vUserDivision.[DivisionId] 
  FROM vUserDivision _vUserDivision
  where _vUserDivision.UserId =  @UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[GetAvailabilityByProfileId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAvailabilityByProfileId]
@ProfileId uniqueidentifier,
@IsDelete bit

AS
BEGIn

SELECT [AvailibilityId]
      ,[ProfileId]
      ,[DateAvailability]
      ,[TargetIncome]
      ,[HrsMon]
      ,[HrsTue]
      ,[HrsWed]
      ,[HrsThu]
      ,[HrsFri]
      ,[HrsSat]
      ,[HrsSun]
      ,[EmploymentPreference]
      ,[WillingToWorkOverTime] as WillingToWorkOverTime
      ,[RelativesWorkingInCompany] as RelativesWorkingInCompany
      ,[RelativesIfYes]
      ,[SubmittedApplicationBefore] as SubmittedApplicationBefore
      ,[AppicationSubmision]
      ,[HearAboutThePosition]
      ,[ReferredBy]
      ,[HowOld] as HowOld
      ,[EligibleToWorkInUS] as EligibleToWorkInUS
      ,[CreatedBy]       
	  
    
  FROM [dbo].[Availability]
  WHERE [ProfileId] = @ProfileId and [Availability].IsDelete = @IsDelete  

END
GO
/****** Object:  StoredProcedure [dbo].[GetExecutiveSummaryByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetExecutiveSummaryByProfile]
@ProfileId uniqueidentifier,
@IsDelete bit

AS
BEGIN
SELECT [ExecutiveSummaryId]
      ,[ProfileId]
      ,[UserId]
      ,[ExecutiveSummaryDetails]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[VersionNo]
  FROM [dbo].[ExecutiveSummary]
WHERE [ProfileId] = @ProfileId  And IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAssociations]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteAssociations]
@AssociationsId uniqueidentifier

As
Begin

Delete from dbo.Associations
Where AssociationsId = @AssociationsId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllSpeakingEventHistory]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllSpeakingEventHistory]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM SpeakingEventHistory  
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllSkills]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllSkills]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM Skills
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllReferences]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllReferences]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM Reference    
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllPublicationHistory]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllPublicationHistory]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM PublicationHistory 
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllProjectByProfileAndUser]    Script Date: 07/11/2014 17:23:40 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteAllProjectByProfileAndContact]    Script Date: 07/11/2014 17:23:40 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteAllLicenceAndCertifications]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllLicenceAndCertifications]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM LicenceAndCertifications  
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllLanguages]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllLanguages]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM Languages    
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllAssociations]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllAssociations]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM Associations   
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllAchievements]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAllAchievements]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS BEGIN

DELETE FROM Achievement
Where ProfileId = @ProfileId AND UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAchievement]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteAchievement]
@AchievementsId uniqueidentifier

As
Begin

Delete from dbo.Achievement
Where AchievementId = @AchievementsId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployeeVacancy]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteEmployeeVacancy]  
@VacancyId as nvarchar(max) 
As  
Begin  
--print('User Vacacny Delete')  
Delete from UserVacancy where VacancyId in (select * from dbo.SplitComaToTable(@VacancyId))
--print('Appl Vacacny Delete')  
Delete from Application where VacancyId in (select * from dbo.SplitComaToTable(@VacancyId))
--print('Update');  
Update Vacancy Set IsDelete = 1 where VacancyId in (select * from dbo.SplitComaToTable(@VacancyId))
End
GO
/****** Object:  StoredProcedure [dbo].[GetAssociationsByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAssociationsByProfile]
@ProfileId uniqueidentifier,
@IsDelete bit
AS
BEGIN

SELECT [AssociationsId]
      ,[ProfileId]
      ,[UserId]
      ,[Name]
      ,[StartDate]
      ,[EndDate]
      ,[Role]
      ,[AssociationType]      
      ,[Link]  
      ,[IsDelete]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[UpdatedBy]
      ,[UpdatedOn]
      ,[VersionNo]
  FROM [dbo].[Associations]
WHERE [ProfileId] = @ProfileId and IsDelete = @IsDelete 


END
GO
/****** Object:  StoredProcedure [dbo].[GetAllUser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 20-02-2014
-- Description:	Get all client 
-- =============================================
CREATE PROCEDURE [dbo].[GetAllUser]
	@IsDelete bit ,
	@ClientId uniqueidentifier,
	@DivisionId nvarchar(max) = null
AS
declare @IsActive bit = 1
BEGIN
IF @DivisionId is not null
begin
	select Distinct UD.UserId,FirstName + ' ' + LastName as 'FirstName'
	from UserDetails UD
	inner join Users on UD.Isdelete = @IsDelete and Users.UserId = UD.UserId
				and FirstName is not null 
				and ClientId = @ClientId
				and Users.IsActive = @IsActive
				and Users.IsDelete  = @IsDelete
	inner join UserSecurityRole USR on USR.UserId = Users.UserId and USR.IsDelete = @IsDelete
	inner join ATSSecurityRole ASR on ASR.ATSSecurityRoleId = USR.ATSSecurityRoleId and LOWER(ASR.DefaultName) != 'candidate' and ASR.IsDelete = @IsDelete
	inner join vUserDivision VUD on VUD.UserId = Users.UserId and VUD.DivisionId in (select Value from dbo.SplitComaToTable(@DivisionId))
	order by FirstName
end
else
begin
	select Distinct UD.UserId,FirstName + ' ' + LastName as 'FirstName'
	from UserDetails UD
	inner join Users on UD.Isdelete = @IsDelete and Users.UserId = UD.UserId
				and FirstName is not null 
				and ClientId = @ClientId
				and Users.IsActive = @IsActive
				and Users.IsDelete  = @IsDelete
	inner join UserSecurityRole USR on USR.UserId = Users.UserId and USR.IsDelete = @IsDelete
	inner join ATSSecurityRole ASR on ASR.ATSSecurityRoleId = USR.ATSSecurityRoleId and LOWER(ASR.DefaultName) != 'candidate' and ASR.IsDelete = @IsDelete
	order by FirstName
end
END
GO
/****** Object:  StoredProcedure [dbo].[GetAlLSaveJobDetailByUserAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 20-03-2014
-- Description:	Get all saved job by user detail
-- =============================================
CREATE PROCEDURE [dbo].[GetAlLSaveJobDetailByUserAndLanguage] --'0585CDCF-2E13-4D48-86AB-3F1E8DD01DA8','33795F80-57D9-43C6-A466-03E816180694'
	@UserId uniqueidentifier,
	@LanguageId uniqueidentifier
AS
BEGIN
	select JobTitle,_EL.LocalName as  Location,ShowOnWebJobDescription,JobDescription,UV.VacancyId,V.CreatedOn,SalaryMin,SalaryMax,
			_DRP2.[Text] as 'EmploymentTypeText'
from UserVacancy UV 
		inner join Vacancy V on V.VacancyId = UV.VacancyId and UV.LanguageId = @LanguageId and UV.UserId = @UserId and v.IsDelete = 0
		inner join [DrpStringMapping] _DRP2 on V.[EmploymentType] = _DRP2.Value AND _DRP2.FormName = 'Vacancy' 
															AND _DRP2.DrpName = 'EmploymentType' AND _DRP2.LanguageId = @LanguageId and _DRP2.IsDelete = 0
		inner join EntityLanguage _EL on V.Location = _El.RecordId AND _El.LanguageId = v.LanguageId and _EL.IsDelete = 0
		Where uv.IsDelete = 0
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllSaveJobByUserAndLanguage]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 19-03-2014
-- Description:	Get All Save Job
-- =============================================
CREATE PROCEDURE [dbo].[GetAllSaveJobByUserAndLanguage]
	@UserId uniqueidentifier,
	@LanguageId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select UV.VacancyId
from UserVacancy UV
where UserId = @UserId and languageId = @LanguageId
and IsDelete = @IsDelete
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllPrivilegeBySecurityRoleId]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 21-02-2014
-- Description:	Get All Privilege by security role ids 
-- =============================================
CREATE PROCEDURE [dbo].[GetAllPrivilegeBySecurityRoleId]
	@ClientId uniqueidentifier,
	@SecurityRoleId varchar(max),
	@IsDelete bit = 0
AS
BEGIN
	--select SRP.ATSPrivilegeId,SRP.PermissionType,SRP.Scope
	--from SecurityRolePrivilege SRP
	--where SRP.IsDelete = @IsDelete and SRP.ClientId = @ClientId and SRP.AtsSecurityRoleId in(''+ @SecurityRoleId + '')
	--group by SRP.ATSPrivilegeId,SRP.PermissionType,SRP.Scope
declare @ResultTempTable table(ATSPrivilegeId uniqueidentifier,PermissionType varchar(20),Scope varchar(5),IsChecked int)
declare @SecurityRole table(id int identity(1,1),val varchar(50))

insert into @SecurityRole
select Value from dbo.SplitComaToTable(@SecurityRoleId)
	
declare @count int = 0
declare @count1 int = 1
select @count = count(*) from @SecurityRole
declare @RoleId uniqueidentifier
While @count >= @count1
begin
select @RoleId = val from @SecurityRole where id = @count1
insert into @ResultTempTable
	select SRP.ATSPrivilegeId,SRP.PermissionType,SRP.Scope,SRP.IsChecked
	from SecurityRolePrivilege SRP
	where SRP.IsDelete = @IsDelete and SRP.ClientId = @ClientId and SRP.AtsSecurityRoleId = @RoleId 
	--group by SRP.ATSPrivilegeId,SRP.PermissionType,SRP.Scope
set @count1 +=  1
end

select ATSPrivilegeId,PermissionType,Scope,cast(MAX(IsChecked) as bit) as IsChecked
from @ResultTempTable
group by ATSPrivilegeId,PermissionType,Scope
order by PermissionType,Scope 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAdHocRolesByUserAndClient]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 05-03-2014
-- Description:	Get adhoc roles by user and client from AdHocPrivilege table
-- =============================================
CREATE PROCEDURE [dbo].[GetAdHocRolesByUserAndClient]
	@UserId uniqueidentifier,
	@ClientId uniqueidentifier,
	@IsDelete bit
AS
BEGIN
	select ATSPrivilegeId,
	case when PermissionType = 'Create' then 'C' when PermissionType = 'Modify' then 'M' when PermissionType = 'View' then 'V' when PermissionType = 'Delete' then 'D' end as PermissionType 
	,Scope
	from AdHocPrivilege 
	where ClientId = @ClientId and UserId = @UserId 
	and IsDelete = @IsDelete
	order by ATSPrivilegeId 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAchievementByProfile]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAchievementByProfile]
@ProfileId uniqueidentifier,
@IsDelete bit

AS
BEGIN


SELECT [AchievementId]
      ,[ProfileId]
      ,[UserId]
      ,[Date]
      ,[Description]
      ,[IssuingAuthority]
      ,[IsDelete]
      ,[CreatedBy]
      ,[CreatedOn]
      
  FROM [dbo].[Achievement]
  WHERE [ProfileId] = @ProfileId and IsDelete = 0

END
GO
/****** Object:  StoredProcedure [dbo].[DeleteSkills]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteSkills]
@SkillsId uniqueidentifier

As
Begin

Delete from dbo.Skills
Where SkillsId  = @SkillsId 

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteSkill]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteSkill]  
@SkillTypeId as nvarchar(max)  
  
  
As  
Begin  
 Update  dbo.Skills Set IsDelete = 1 Where Cast(SkillType as nvarchar(40))  in(@SkillTypeId) 
 
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteObjectiveByProfileAndUser]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteObjectiveByProfileAndUser]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier

AS
Begin
Delete from dbo.Objective
Where ProfileId = @ProfileId and UserId = @UserId
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteExecutiveSummaryByProfileAndUser]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[DeleteExecutiveSummaryByProfileAndUser]
@ProfileId uniqueidentifier,
@UserId uniqueidentifier
As
Begin

Delete from dbo.ExecutiveSummary
Where ProfileId = @ProfileId and UserId = @UserId
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteDegreeType]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteDegreeType]  
@DegreeTypeId as nvarchar(max)

  
As  
Begin  
  
--delete from Education History  
Exec DeleteDegreeTypeEducationHistory @DegreeTypeId  
  
--delete from Entity language  
exec DeleteEntityLanguage  @DegreeTypeId
  
Update  dbo.DegreeType Set IsDelete = 1 Where Cast(DegreeTypeId as nvarchar(40)) = @DegreeTypeId  
  
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteLicenceAndCertifications]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteLicenceAndCertifications]
@LicenceAndCertificationId uniqueidentifier

As
Begin

Delete from dbo.LicenceAndCertifications
Where LicenceAndCertificationsId = @LicenceAndCertificationId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteLanguages]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteLanguages]
@LanguagesId uniqueidentifier

As
Begin

Delete from dbo.Languages
Where LanguagesId = @LanguagesId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteRoleByUserAndClient]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Viral Shah
-- Create date: 05-03-2014
-- Description:	Delete exsisting roles from adhocprivilege and UserSecurityrole table
-- =============================================
CREATE PROCEDURE [dbo].[DeleteRoleByUserAndClient]
	@ClientId uniqueidentifier,
	@UserId uniqueidentifier
AS
BEGIN
	delete from UserSecurityRole where UserId = @UserId
	delete from AdHocPrivilege where UserId = @UserId and ClientId = @ClientId
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteReference]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteReference]
@ReferencesId uniqueidentifier

As
Begin

Delete from dbo.Reference
Where ReferenceId  = @ReferencesId

End
GO
/****** Object:  StoredProcedure [dbo].[DeletePublicationHistory]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeletePublicationHistory]
@PublicationHistoryId uniqueidentifier

As
Begin

Delete from dbo.PublicationHistory
Where PublicationHistoryId = @PublicationHistoryId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteProfile]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProfile]  
--To Delete all records of related profile and contact  
@UserId uniqueidentifier,  
@ProfileId uniqueidentifier  
  
AS  
BEGIN  
  
DELETE FROM dbo.Availability where profileid = @ProfileId  
DELETE FROM dbo.ExecutiveSummary where profileid = @ProfileId  
DELETE FROM dbo.Objective where profileid = @ProfileId

  
DELETE   
 FROM Child   
 from Application as Child  
 inner join ATSResume  parent on Child.AtsResumeId = parent.AtsResumeId    
where parent.ProfileId = @ProfileId  
DELETE FROM dbo.ATSResume where ProfileId = @ProfileId and UserId = @UserId  
  
DELETE FROM dbo.Project where ProfileId = @ProfileId and UserId = @UserId  
  
DELETE FROM dbo.EmploymentHistory where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.EducationHistory where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.LicenceAndCertifications where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.PublicationHistory where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.SpeakingEventHistory where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.Languages where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.Achievement where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.Associations where ProfileId = @ProfileId and UserId = @UserId  
  
DELETE FROM dbo.Skills where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.Reference where ProfileId = @ProfileId and UserId = @UserId  
DELETE FROM dbo.[Profile] where ProfileId = @ProfileId and UserId = @UserId  
  
  
END
GO
/****** Object:  StoredProcedure [dbo].[Deleteuser]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Deleteuser] --'2A1E1FF4-DBF6-4AD7-9A24-C06ADAE355AD'
 @userId  uniqueidentifier

As
Begin
--set @userId = '1B98A9BA-9BAC-458D-8628-0B5F382EB7C1'
 ---Delete from [Application] where UserId = @userId
 
 Delete from [UserSecurityRole] Where userId = @userId
DELETE From dbo.Application where CreatedBy = @userId
DELETE FROM dbo.ATSResume where  UserId = @userId
DELETE FROM dbo.Project where  UserId = @userId
DELETE FROM dbo.EmploymentHistory where  UserId = @userId
DELETE FROM dbo.EducationHistory where  UserId = @userId
DELETE FROM dbo.Skills where UserId = @UserId
DELETE FROM dbo.Reference where  UserId = @userId
DELETE FROM dbo.Languages where  UserId = @userId
DELETE FROM dbo.SpeakingEventHistory where  UserId = @userId
DELETE FROM dbo.Achievement where  UserId = @userId
DELETE FROM dbo.Associations where  UserId = @userId
DELETE FROM dbo.LicenceAndCertifications where  UserId = @userId
DELETE FROM dbo.PublicationHistory where  UserId = @userId

DELETE 
 FROM Child 
 from availability as Child
 inner join Profile  parent on Child.profileId = parent.ProfileId  
where userId = @userId
DELETE 
 FROM Child1
 from ExecutiveSummary as Child1
 inner join Profile  parent on Child1.profileId = parent.ProfileId  
where Child1.userId = @userId
DELETE 
 FROM Child2 
 from Objective as Child2
 inner join Profile  parent on Child2.profileId = parent.ProfileId  
where Child2.userId = @userId

Delete from parent
from profile as parent
where userId = @userId

delete from Search where userId = @userid

 Delete from [Userdetails] Where userId = @userid
 Delete from Users Where userId = @userid
 
end
 --Delete from [UserSecurity] Where userId = @userid
GO
/****** Object:  StoredProcedure [dbo].[DeleteSpeakinEventHistory]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteSpeakinEventHistory]
@SpeakingEventHistoryId uniqueidentifier

As
Begin

Delete from dbo.SpeakingEventHistory 
Where SpeakingEventHistoryId = @SpeakingEventHistoryId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteVacancyStatus]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteVacancyStatus] --'7B833998-7B77-451D-A295-F773E2956F02','33795F80-57D9-43C6-A466-03E816180694'  
@VacancyStatusId as nvarchar(max)

  
AS  
Declare @VacancyId as nvarchar(max)  
Begin  
--Get Vacacny Id   
  
Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.VacancyStatusId = @VacancyStatusId FOR XML PATH('')), 1, 1, '')     
FROM  vacancy  where VacancyStatusId = @VacancyStatusId   
group by VacancyStatusId   
--Delete from Vacancy  
  
exec DeleteEmployeeVacancy @VacancyId   
  
--delete from Entity language  
  
exec DeleteEntityLanguage  @VacancyStatusId
  
--Soft Delete from VacansyStatus  
Update VacancyStatus Set IsDelete = 1 where Cast(VacancyStatusId as nvarchar(40)) =  @VacancyStatusId  
  
--print(@VacancyId)  
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteSkillType]    Script Date: 07/11/2014 17:23:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteSkillType]
@SkillTypeId as nvarchar(max)


As
Begin

--Delete from SKill
Exec DeleteSkill @SkillTypeId

--Delete from Entity language
exec DeleteEntityLanguage  @SkillTypeId

Update  dbo.SkillType Set IsDelete = 1 Where Cast(SkillTypeId as nvarchar(40)) = @SkillTypeId

End

Select * from Skills
GO
/****** Object:  StoredProcedure [dbo].[DeletePositionType]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeletePositionType]
@PositionTypeId  as nvarchar(max)


As


Declare @VacancyId as nvarchar(max)
Begin
--Get Vacacny Id 
	Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.PositionTypeId = @PositionTypeId AND IsDelete = 0 FOR XML PATH('')), 1, 1, '')   
FROM  vacancy  where PositionTypeId = @PositionTypeId AND IsDelete = 0
group by PositionTypeId 
	--Delete from Vacancy
	exec DeleteEmployeeVacancy @VacancyId 

Update  dbo.DivisionPositionType Set IsDelete = 1 Where Cast(PositionTypeId as nvarchar(40)) = @PositionTypeId
--Delete from Entity language

	exec DeleteEntityLanguage  @PositionTypeId

Update  dbo.PositionType Set IsDelete = 1 Where Cast(PositionTypeId as nvarchar(40)) = @PositionTypeId
  

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteJobLocation]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteJobLocation]
@JobLocationId as nvarchar(max)



As
Declare @VacancyId as nvarchar(max)
Begin

--Delete from Entity language
exec DeleteEntityLanguage  @JobLocationId
Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.Location = @JobLocationId AND IsDelete = 0 FOR XML PATH('')), 1, 1, '')   
FROM  vacancy  where Location = @JobLocationId AND IsDelete = 0
group by Location 
exec DeleteEmployeeVacancy @VacancyId
update  dbo.JobLocation set IsDelete = 1 Where Cast(JobLocationId as nvarchar(40)) = @JobLocationId

End
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployeeUser]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteEmployeeUser]  
@UserId nvarchar(max)  
AS  
Declare @VacancyId as nvarchar(max)  
Begin  
Update UserDivisionPermission Set Isdelete = 1 where cast(UserId as nvarchar(40)) in (@UserId)  
Update AdHocPrivilege Set IsDelete = 1 where cast(UserId as nvarchar(40)) = (@UserId)  
Update UserSecurityRole Set IsDelete = 1 where cast(UserId as nvarchar(40)) = (@UserId)  
Delete from SaveResumeSearch where Cast(UserId as nvarchar(40)) = @UserId  
Delete from UserVacancy where Cast(UserId as nvarchar(40)) = @UserId  
Delete from [Application]  where Cast(CreatedBy as nvarchar(40)) = @UserId  
Delete from [BlockCandidate] where Cast(UserId as nvarchar(40)) = @UserId  
Delete From Search where Cast(UserId as nvarchar(40)) = @UserId   
--Get Vacancy Id  
Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.CreatedBy = @UserId FOR XML PATH('')), 1, 1, '')     
FROM  vacancy  where cast(CreatedBy as nvarchar(40))  = @UserId   
group by CreatedBy    
--Delete Vacancy  
exec DeleteEmployeeVacancy @VacancyId   
  
Update UserDetails Set Isdelete = 1 where cast(UserId as nvarchar(40)) = (@UserId)  
Update Users Set Isdelete = 1 where cast(UserId as nvarchar(40)) = (@UserId)  
End
GO
/****** Object:  StoredProcedure [dbo].[DeleteDivision]    Script Date: 07/11/2014 17:23:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteDivision]  
@DivisionId nvarchar(max)

As  
Declare @VacancyId as nvarchar(max)  
Declare @UserId as nvarchar(max)  
Begin  


Update DivisionPositionType Set IsDelete = 1 Where CAST(DivisionId as nvarchar(40))  = @DivisionId  
Update UserDivisionPermission Set IsDelete = 1 Where CAST(DivisionId as nvarchar(40))  = @DivisionId  
  
--Get Vacancy ID  
Select @VacancyId = STUFF(( SELECT ',' + CAST(vacancyid as nvarchar(40)) FROM vacancy  _V  where _V.DivisionId = @DivisionId FOR XML PATH('')), 1, 1, '')     
FROM  vacancy  where DivisionId = @DivisionId   
group by VacancyStatusId   
  
  
exec DeleteEmployeeVacancy @VacancyId    
  
exec DeleteEntityLanguage @DivisionId
  
Update Division Set IsDelete = 1 where Cast(DivisionId as nvarchar(40)) = @DivisionId  
  
  
  
--Get User ID  
Select @UserId = STUFF(( SELECT ',' + CAST(UserId as nvarchar(40)) FROM vUserDivision  _D  where _D.DivisionId = @DivisionId FOR XML PATH('')), 1, 1, '')     
FROM  Division  where DivisionId = @DivisionId   
group by DivisionId  
  
exec DeleteEmployeeUser @UserId   
  
  
  
  
  
  
End
GO
/****** Object:  Default [DF_Company_IsActive]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  ForeignKey [FK_Achievement_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Achievement]  WITH CHECK ADD  CONSTRAINT [FK_Achievement_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[Achievement] CHECK CONSTRAINT [FK_Achievement_Profile]
GO
/****** Object:  ForeignKey [FK_Achievement_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Achievement]  WITH CHECK ADD  CONSTRAINT [FK_Achievement_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Achievement] CHECK CONSTRAINT [FK_Achievement_User]
GO
/****** Object:  ForeignKey [FK_AdHocPrivilege_ATSPrivilege]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[AdHocPrivilege]  WITH CHECK ADD  CONSTRAINT [FK_AdHocPrivilege_ATSPrivilege] FOREIGN KEY([ATSPrivilegeId])
REFERENCES [dbo].[ATSPrivilege] ([ATSPrivilegeId])
GO
ALTER TABLE [dbo].[AdHocPrivilege] CHECK CONSTRAINT [FK_AdHocPrivilege_ATSPrivilege]
GO
/****** Object:  ForeignKey [FK_AdHocPrivilege_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[AdHocPrivilege]  WITH CHECK ADD  CONSTRAINT [FK_AdHocPrivilege_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[AdHocPrivilege] CHECK CONSTRAINT [FK_AdHocPrivilege_User]
GO
/****** Object:  ForeignKey [FK_AdHocPrivilege_UserCreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[AdHocPrivilege]  WITH CHECK ADD  CONSTRAINT [FK_AdHocPrivilege_UserCreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[AdHocPrivilege] CHECK CONSTRAINT [FK_AdHocPrivilege_UserCreatedBy]
GO
/****** Object:  ForeignKey [FK_AdHocPrivilege_UserUpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[AdHocPrivilege]  WITH CHECK ADD  CONSTRAINT [FK_AdHocPrivilege_UserUpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[AdHocPrivilege] CHECK CONSTRAINT [FK_AdHocPrivilege_UserUpdatedBy]
GO
/****** Object:  ForeignKey [FK_Application_ATSResume]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_ATSResume] FOREIGN KEY([ATSResumeId])
REFERENCES [dbo].[ATSResume] ([ATSResumeId])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_ATSResume]
GO
/****** Object:  ForeignKey [FK_Applications_UserCreated]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Applications_UserCreated] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Applications_UserCreated]
GO
/****** Object:  ForeignKey [FK_Applications_UserUpdated]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Applications_UserUpdated] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Applications_UserUpdated]
GO
/****** Object:  ForeignKey [FK_Associations_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Associations]  WITH CHECK ADD  CONSTRAINT [FK_Associations_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[Associations] CHECK CONSTRAINT [FK_Associations_Profile]
GO
/****** Object:  ForeignKey [FK_Associations_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Associations]  WITH CHECK ADD  CONSTRAINT [FK_Associations_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Associations] CHECK CONSTRAINT [FK_Associations_User]
GO
/****** Object:  ForeignKey [FK_ATSPrivilege_CreatedUser]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[ATSPrivilege]  WITH CHECK ADD  CONSTRAINT [FK_ATSPrivilege_CreatedUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ATSPrivilege] CHECK CONSTRAINT [FK_ATSPrivilege_CreatedUser]
GO
/****** Object:  ForeignKey [FK_ATSPrivilege_UpdatedUser]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[ATSPrivilege]  WITH CHECK ADD  CONSTRAINT [FK_ATSPrivilege_UpdatedUser] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ATSPrivilege] CHECK CONSTRAINT [FK_ATSPrivilege_UpdatedUser]
GO
/****** Object:  ForeignKey [FK_ATSSecurityRole_UserCreated]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[ATSSecurityRole]  WITH CHECK ADD  CONSTRAINT [FK_ATSSecurityRole_UserCreated] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ATSSecurityRole] CHECK CONSTRAINT [FK_ATSSecurityRole_UserCreated]
GO
/****** Object:  ForeignKey [FK_ATSSecurityRole_UserUpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[ATSSecurityRole]  WITH CHECK ADD  CONSTRAINT [FK_ATSSecurityRole_UserUpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ATSSecurityRole] CHECK CONSTRAINT [FK_ATSSecurityRole_UserUpdatedBy]
GO
/****** Object:  ForeignKey [FK_Availability_CreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Availability]  WITH CHECK ADD  CONSTRAINT [FK_Availability_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Availability] CHECK CONSTRAINT [FK_Availability_CreatedBy]
GO
/****** Object:  ForeignKey [FK_Availability_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Availability]  WITH CHECK ADD  CONSTRAINT [FK_Availability_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[Availability] CHECK CONSTRAINT [FK_Availability_Profile]
GO
/****** Object:  ForeignKey [FK_Availability_UpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Availability]  WITH CHECK ADD  CONSTRAINT [FK_Availability_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Availability] CHECK CONSTRAINT [FK_Availability_UpdatedBy]
GO
/****** Object:  ForeignKey [FK_BlockCandidate_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[BlockCandidate]  WITH CHECK ADD  CONSTRAINT [FK_BlockCandidate_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BlockCandidate] CHECK CONSTRAINT [FK_BlockCandidate_User]
GO
/****** Object:  ForeignKey [FK_Division_ParentDivision]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Division]  WITH CHECK ADD  CONSTRAINT [FK_Division_ParentDivision] FOREIGN KEY([ParentDivisionId])
REFERENCES [dbo].[Division] ([DivisionId])
GO
ALTER TABLE [dbo].[Division] CHECK CONSTRAINT [FK_Division_ParentDivision]
GO
/****** Object:  ForeignKey [FK_DivisionPositionType_Division]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[DivisionPositionType]  WITH CHECK ADD  CONSTRAINT [FK_DivisionPositionType_Division] FOREIGN KEY([DivisionId])
REFERENCES [dbo].[Division] ([DivisionId])
GO
ALTER TABLE [dbo].[DivisionPositionType] CHECK CONSTRAINT [FK_DivisionPositionType_Division]
GO
/****** Object:  ForeignKey [FK_DivisionPositionType_PositionType]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[DivisionPositionType]  WITH CHECK ADD  CONSTRAINT [FK_DivisionPositionType_PositionType] FOREIGN KEY([PositionTypeId])
REFERENCES [dbo].[PositionType] ([PositionTypeId])
GO
ALTER TABLE [dbo].[DivisionPositionType] CHECK CONSTRAINT [FK_DivisionPositionType_PositionType]
GO
/****** Object:  ForeignKey [FK_DivisionPositionType_UserCreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[DivisionPositionType]  WITH CHECK ADD  CONSTRAINT [FK_DivisionPositionType_UserCreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[DivisionPositionType] CHECK CONSTRAINT [FK_DivisionPositionType_UserCreatedBy]
GO
/****** Object:  ForeignKey [FK_DivisionPositionType_UserUpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[DivisionPositionType]  WITH CHECK ADD  CONSTRAINT [FK_DivisionPositionType_UserUpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[DivisionPositionType] CHECK CONSTRAINT [FK_DivisionPositionType_UserUpdatedBy]
GO
/****** Object:  ForeignKey [FK_EducationHistory_CreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[EducationHistory]  WITH CHECK ADD  CONSTRAINT [FK_EducationHistory_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EducationHistory] CHECK CONSTRAINT [FK_EducationHistory_CreatedBy]
GO
/****** Object:  ForeignKey [FK_EducationHistory_UpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[EducationHistory]  WITH CHECK ADD  CONSTRAINT [FK_EducationHistory_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EducationHistory] CHECK CONSTRAINT [FK_EducationHistory_UpdatedBy]
GO
/****** Object:  ForeignKey [FK_EducationHistory_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[EducationHistory]  WITH CHECK ADD  CONSTRAINT [FK_EducationHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EducationHistory] CHECK CONSTRAINT [FK_EducationHistory_User]
GO
/****** Object:  ForeignKey [FK_EmploymentHistory_CreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[EmploymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentHistory_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EmploymentHistory] CHECK CONSTRAINT [FK_EmploymentHistory_CreatedBy]
GO
/****** Object:  ForeignKey [FK_EmploymentHistory_UpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[EmploymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentHistory_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EmploymentHistory] CHECK CONSTRAINT [FK_EmploymentHistory_UpdatedBy]
GO
/****** Object:  ForeignKey [FK_EmploymentHistory_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[EmploymentHistory]  WITH CHECK ADD  CONSTRAINT [FK_EmploymentHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[EmploymentHistory] CHECK CONSTRAINT [FK_EmploymentHistory_User]
GO
/****** Object:  ForeignKey [FK_ExecutiveSummary_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[ExecutiveSummary]  WITH CHECK ADD  CONSTRAINT [FK_ExecutiveSummary_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[ExecutiveSummary] CHECK CONSTRAINT [FK_ExecutiveSummary_Profile]
GO
/****** Object:  ForeignKey [FK_ExecutiveSummary_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[ExecutiveSummary]  WITH CHECK ADD  CONSTRAINT [FK_ExecutiveSummary_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ExecutiveSummary] CHECK CONSTRAINT [FK_ExecutiveSummary_User]
GO
/****** Object:  ForeignKey [FK_Languages_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Languages]  WITH CHECK ADD  CONSTRAINT [FK_Languages_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[Languages] CHECK CONSTRAINT [FK_Languages_Profile]
GO
/****** Object:  ForeignKey [FK_Languages_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Languages]  WITH CHECK ADD  CONSTRAINT [FK_Languages_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Languages] CHECK CONSTRAINT [FK_Languages_User]
GO
/****** Object:  ForeignKey [FK_LicenceAndCertifications_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[LicenceAndCertifications]  WITH CHECK ADD  CONSTRAINT [FK_LicenceAndCertifications_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[LicenceAndCertifications] CHECK CONSTRAINT [FK_LicenceAndCertifications_Profile]
GO
/****** Object:  ForeignKey [FK_LicenceAndCertifications_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[LicenceAndCertifications]  WITH CHECK ADD  CONSTRAINT [FK_LicenceAndCertifications_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[LicenceAndCertifications] CHECK CONSTRAINT [FK_LicenceAndCertifications_User]
GO
/****** Object:  ForeignKey [FK_Objective_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Objective]  WITH CHECK ADD  CONSTRAINT [FK_Objective_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[Objective] CHECK CONSTRAINT [FK_Objective_Profile]
GO
/****** Object:  ForeignKey [FK_Objective_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Objective]  WITH CHECK ADD  CONSTRAINT [FK_Objective_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Objective] CHECK CONSTRAINT [FK_Objective_User]
GO
/****** Object:  ForeignKey [FK_Profile_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_User]
GO
/****** Object:  ForeignKey [FK_Project_CreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_CreatedBy]
GO
/****** Object:  ForeignKey [FK_Project_EmploymentHistory]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_EmploymentHistory] FOREIGN KEY([EmploymentHistoryId])
REFERENCES [dbo].[EmploymentHistory] ([EmployementHistoryId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_EmploymentHistory]
GO
/****** Object:  ForeignKey [FK_Project_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Profile]
GO
/****** Object:  ForeignKey [FK_Project_UpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_UpdatedBy]
GO
/****** Object:  ForeignKey [FK_Project_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_User]
GO
/****** Object:  ForeignKey [FK_PublicationHistory_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[PublicationHistory]  WITH CHECK ADD  CONSTRAINT [FK_PublicationHistory_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[PublicationHistory] CHECK CONSTRAINT [FK_PublicationHistory_Profile]
GO
/****** Object:  ForeignKey [FK_PublicationHistory_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[PublicationHistory]  WITH CHECK ADD  CONSTRAINT [FK_PublicationHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[PublicationHistory] CHECK CONSTRAINT [FK_PublicationHistory_User]
GO
/****** Object:  ForeignKey [FK_Reference_CreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Reference]  WITH CHECK ADD  CONSTRAINT [FK_Reference_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Reference] CHECK CONSTRAINT [FK_Reference_CreatedBy]
GO
/****** Object:  ForeignKey [FK_Reference_ProfileId]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Reference]  WITH CHECK ADD  CONSTRAINT [FK_Reference_ProfileId] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[Reference] CHECK CONSTRAINT [FK_Reference_ProfileId]
GO
/****** Object:  ForeignKey [FK_Reference_UpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Reference]  WITH CHECK ADD  CONSTRAINT [FK_Reference_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Reference] CHECK CONSTRAINT [FK_Reference_UpdatedBy]
GO
/****** Object:  ForeignKey [FK_Reference_UserId]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Reference]  WITH CHECK ADD  CONSTRAINT [FK_Reference_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Reference] CHECK CONSTRAINT [FK_Reference_UserId]
GO
/****** Object:  ForeignKey [FK_SaveResumeSearch_CreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SaveResumeSearch]  WITH CHECK ADD  CONSTRAINT [FK_SaveResumeSearch_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SaveResumeSearch] CHECK CONSTRAINT [FK_SaveResumeSearch_CreatedBy]
GO
/****** Object:  ForeignKey [FK_SaveResumeSearch_UpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SaveResumeSearch]  WITH CHECK ADD  CONSTRAINT [FK_SaveResumeSearch_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SaveResumeSearch] CHECK CONSTRAINT [FK_SaveResumeSearch_UpdatedBy]
GO
/****** Object:  ForeignKey [FK_SaveResumeSearch_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SaveResumeSearch]  WITH CHECK ADD  CONSTRAINT [FK_SaveResumeSearch_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SaveResumeSearch] CHECK CONSTRAINT [FK_SaveResumeSearch_User]
GO
/****** Object:  ForeignKey [FK_Search_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Search]  WITH CHECK ADD  CONSTRAINT [FK_Search_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Search] CHECK CONSTRAINT [FK_Search_User]
GO
/****** Object:  ForeignKey [FK_Search_UserCreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Search]  WITH CHECK ADD  CONSTRAINT [FK_Search_UserCreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Search] CHECK CONSTRAINT [FK_Search_UserCreatedBy]
GO
/****** Object:  ForeignKey [FK_Search_UserUpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Search]  WITH CHECK ADD  CONSTRAINT [FK_Search_UserUpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Search] CHECK CONSTRAINT [FK_Search_UserUpdatedBy]
GO
/****** Object:  ForeignKey [FK_SecurityRolePrivilege_ATSPrivilege]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SecurityRolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRolePrivilege_ATSPrivilege] FOREIGN KEY([ATSPrivilegeId])
REFERENCES [dbo].[ATSPrivilege] ([ATSPrivilegeId])
GO
ALTER TABLE [dbo].[SecurityRolePrivilege] CHECK CONSTRAINT [FK_SecurityRolePrivilege_ATSPrivilege]
GO
/****** Object:  ForeignKey [FK_SecurityRolePrivilege_AtsSecurityRole]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SecurityRolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRolePrivilege_AtsSecurityRole] FOREIGN KEY([AtsSecurityRoleId])
REFERENCES [dbo].[ATSSecurityRole] ([ATSSecurityRoleId])
GO
ALTER TABLE [dbo].[SecurityRolePrivilege] CHECK CONSTRAINT [FK_SecurityRolePrivilege_AtsSecurityRole]
GO
/****** Object:  ForeignKey [FK_SecurityRolePrivilege_UserCreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SecurityRolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRolePrivilege_UserCreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SecurityRolePrivilege] CHECK CONSTRAINT [FK_SecurityRolePrivilege_UserCreatedBy]
GO
/****** Object:  ForeignKey [FK_SecurityRolePrivilege_UserUpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SecurityRolePrivilege]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRolePrivilege_UserUpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SecurityRolePrivilege] CHECK CONSTRAINT [FK_SecurityRolePrivilege_UserUpdatedBy]
GO
/****** Object:  ForeignKey [FK_Skills_CreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD  CONSTRAINT [FK_Skills_CreatedBy] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Skills] CHECK CONSTRAINT [FK_Skills_CreatedBy]
GO
/****** Object:  ForeignKey [FK_Skills_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD  CONSTRAINT [FK_Skills_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[Skills] CHECK CONSTRAINT [FK_Skills_Profile]
GO
/****** Object:  ForeignKey [FK_Skills_Skills]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD  CONSTRAINT [FK_Skills_Skills] FOREIGN KEY([SkillsId])
REFERENCES [dbo].[Skills] ([SkillsId])
GO
ALTER TABLE [dbo].[Skills] CHECK CONSTRAINT [FK_Skills_Skills]
GO
/****** Object:  ForeignKey [FK_Skills_UpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD  CONSTRAINT [FK_Skills_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Skills] CHECK CONSTRAINT [FK_Skills_UpdatedBy]
GO
/****** Object:  ForeignKey [FK_Skills_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD  CONSTRAINT [FK_Skills_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Skills] CHECK CONSTRAINT [FK_Skills_User]
GO
/****** Object:  ForeignKey [FK_SpeakingEventHistory_Profile]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SpeakingEventHistory]  WITH CHECK ADD  CONSTRAINT [FK_SpeakingEventHistory_Profile] FOREIGN KEY([ProfileId])
REFERENCES [dbo].[Profile] ([ProfileId])
GO
ALTER TABLE [dbo].[SpeakingEventHistory] CHECK CONSTRAINT [FK_SpeakingEventHistory_Profile]
GO
/****** Object:  ForeignKey [FK_SpeakingEventHistory_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SpeakingEventHistory]  WITH CHECK ADD  CONSTRAINT [FK_SpeakingEventHistory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SpeakingEventHistory] CHECK CONSTRAINT [FK_SpeakingEventHistory_User]
GO
/****** Object:  ForeignKey [FK_SystemMenu_SystemMenu1]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SystemMenu]  WITH CHECK ADD  CONSTRAINT [FK_SystemMenu_SystemMenu1] FOREIGN KEY([ParentSystemMenuId])
REFERENCES [dbo].[SystemMenu] ([SystemMenuId])
GO
ALTER TABLE [dbo].[SystemMenu] CHECK CONSTRAINT [FK_SystemMenu_SystemMenu1]
GO
/****** Object:  ForeignKey [FK_SystemMenu_UserCreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SystemMenu]  WITH CHECK ADD  CONSTRAINT [FK_SystemMenu_UserCreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SystemMenu] CHECK CONSTRAINT [FK_SystemMenu_UserCreatedBy]
GO
/****** Object:  ForeignKey [FK_SystemMenu_UserUpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[SystemMenu]  WITH CHECK ADD  CONSTRAINT [FK_SystemMenu_UserUpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SystemMenu] CHECK CONSTRAINT [FK_SystemMenu_UserUpdatedBy]
GO
/****** Object:  ForeignKey [FK_UserDetails_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserDetails_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserDetails] CHECK CONSTRAINT [FK_UserDetails_User]
GO
/****** Object:  ForeignKey [FK_UserDivisionPermission_Division]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserDivisionPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserDivisionPermission_Division] FOREIGN KEY([DivisionId])
REFERENCES [dbo].[Division] ([DivisionId])
GO
ALTER TABLE [dbo].[UserDivisionPermission] CHECK CONSTRAINT [FK_UserDivisionPermission_Division]
GO
/****** Object:  ForeignKey [FK_UserDivisionPermission_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserDivisionPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserDivisionPermission_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserDivisionPermission] CHECK CONSTRAINT [FK_UserDivisionPermission_User]
GO
/****** Object:  ForeignKey [FK_UserDivisionPermission_UserCreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserDivisionPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserDivisionPermission_UserCreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserDivisionPermission] CHECK CONSTRAINT [FK_UserDivisionPermission_UserCreatedBy]
GO
/****** Object:  ForeignKey [FK_UserDivisionPermission_UserUpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserDivisionPermission]  WITH CHECK ADD  CONSTRAINT [FK_UserDivisionPermission_UserUpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserDivisionPermission] CHECK CONSTRAINT [FK_UserDivisionPermission_UserUpdatedBy]
GO
/****** Object:  ForeignKey [FK_Users_Users]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users]
GO
/****** Object:  ForeignKey [FK_UserSecurityRole_ATSSecurityRole]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserSecurityRole]  WITH CHECK ADD  CONSTRAINT [FK_UserSecurityRole_ATSSecurityRole] FOREIGN KEY([ATSSecurityRoleId])
REFERENCES [dbo].[ATSSecurityRole] ([ATSSecurityRoleId])
GO
ALTER TABLE [dbo].[UserSecurityRole] CHECK CONSTRAINT [FK_UserSecurityRole_ATSSecurityRole]
GO
/****** Object:  ForeignKey [FK_UserSecurityRole_User]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserSecurityRole]  WITH CHECK ADD  CONSTRAINT [FK_UserSecurityRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSecurityRole] CHECK CONSTRAINT [FK_UserSecurityRole_User]
GO
/****** Object:  ForeignKey [FK_UserSecurityRole_UserCreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserSecurityRole]  WITH CHECK ADD  CONSTRAINT [FK_UserSecurityRole_UserCreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSecurityRole] CHECK CONSTRAINT [FK_UserSecurityRole_UserCreatedBy]
GO
/****** Object:  ForeignKey [FK_UserSecurityRole_UserUpdatedABy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserSecurityRole]  WITH CHECK ADD  CONSTRAINT [FK_UserSecurityRole_UserUpdatedABy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserSecurityRole] CHECK CONSTRAINT [FK_UserSecurityRole_UserUpdatedABy]
GO
/****** Object:  ForeignKey [FK_UserVacany_Users]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserVacancy]  WITH CHECK ADD  CONSTRAINT [FK_UserVacany_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserVacancy] CHECK CONSTRAINT [FK_UserVacany_Users]
GO
/****** Object:  ForeignKey [FK_UserVacany_Vacancy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[UserVacancy]  WITH CHECK ADD  CONSTRAINT [FK_UserVacany_Vacancy] FOREIGN KEY([VacancyId], [LanguageId])
REFERENCES [dbo].[Vacancy] ([VacancyId], [LanguageId])
GO
ALTER TABLE [dbo].[UserVacancy] CHECK CONSTRAINT [FK_UserVacany_Vacancy]
GO
/****** Object:  ForeignKey [FK_Vacancy_Division]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Vacancy]  WITH CHECK ADD  CONSTRAINT [FK_Vacancy_Division] FOREIGN KEY([DivisionId])
REFERENCES [dbo].[Division] ([DivisionId])
GO
ALTER TABLE [dbo].[Vacancy] CHECK CONSTRAINT [FK_Vacancy_Division]
GO
/****** Object:  ForeignKey [FK_Vacancy_PositionType]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[Vacancy]  WITH CHECK ADD  CONSTRAINT [FK_Vacancy_PositionType] FOREIGN KEY([PositionTypeId])
REFERENCES [dbo].[PositionType] ([PositionTypeId])
GO
ALTER TABLE [dbo].[Vacancy] CHECK CONSTRAINT [FK_Vacancy_PositionType]
GO
/****** Object:  ForeignKey [FK_VacancyStatus_CreatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[VacancyStatus]  WITH CHECK ADD  CONSTRAINT [FK_VacancyStatus_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[VacancyStatus] CHECK CONSTRAINT [FK_VacancyStatus_CreatedBy]
GO
/****** Object:  ForeignKey [FK_VacancyStatus_UpdatedBy]    Script Date: 07/11/2014 17:23:38 ******/
ALTER TABLE [dbo].[VacancyStatus]  WITH CHECK ADD  CONSTRAINT [FK_VacancyStatus_UpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[VacancyStatus] CHECK CONSTRAINT [FK_VacancyStatus_UpdatedBy]
GO
