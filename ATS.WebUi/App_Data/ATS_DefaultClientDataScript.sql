
USE [ATS_Client]
GO
Declare @clientId uniqueidentifier = 'ClientID'
Declare @languageId uniqueidentifier = '33795f80-57d9-43c6-a466-03e816180694'

declare @UserId uniqueidentifier = newid()
declare @UserName varchar(100) = 'gordondarling67@gmail.com'
declare @Password varchar(100) = 'YyK/wC0RtLfS73LgsysXuA=='

declare @preApplication		uniqueidentifier
declare @preCandidate		uniqueidentifier
declare @preCompany			uniqueidentifier
declare @preDivision		uniqueidentifier
declare @preInterview		uniqueidentifier
declare @preJobLocation		uniqueidentifier
declare @preJobOffer		uniqueidentifier
declare @prePortalContent	uniqueidentifier
declare @prePositionType	uniqueidentifier
declare @preQuestion		uniqueidentifier
declare @preRating			uniqueidentifier
declare @preResume			uniqueidentifier
declare @preReviewPanel		uniqueidentifier
declare @preSecurityRole	uniqueidentifier
declare @preUser			uniqueidentifier
declare @preVacancy			uniqueidentifier
declare @preVacancyQuestion uniqueidentifier
declare @preVacancyTmplate	uniqueidentifier

declare @srSysAdmin		uniqueidentifier
declare @srSetupMgr		uniqueidentifier
declare @srCorMgr		uniqueidentifier
declare @srRegMgr		uniqueidentifier
declare @srDiviMgr		uniqueidentifier
declare @srLocMgr		uniqueidentifier
declare @srCandidate	uniqueidentifier



declare @ptCreate varchar(100)
declare @ptView varchar(100)
declare @ptModify varchar(100)
declare @ptDelete varchar(100)

set @ptCreate  = 'Create'
set @ptView  = 'View'
set @ptModify  = 'Modify'
set @ptDelete  = 'Delete'

declare @scAllD varchar(100)
declare @scSisterD varchar(100)
declare @scChildD varchar(100)
declare @scOwnD varchar(100)

set @scAllD  = 'A'
set @scSisterD  = 'S'
set @scChildD  = 'C'
set @scOwnD  = 'O'



set @preApplication			=newid()		
set @preCandidate			=newid()		
set @preCompany				=newid()		
set @preDivision			=newid()		
set @preInterview			=newid()		
set @preJobLocation			=newid()		
set @preJobOffer			=newid()		
set @prePortalContent		=newid()		
set @prePositionType		=newid()		
set @preQuestion			=newid()		
set @preRating				=newid()		
set @preResume				=newid()		
set @preReviewPanel			=newid()		
set @preSecurityRole		=newid()		
set @preUser				=newid()		
set @preVacancy				=newid()		
set @preVacancyQuestion		=newid()		
set @preVacancyTmplate		=newid()		


set @srSysAdmin	=newid()
set @srSetupMgr	=newid()
set @srCorMgr	=newid()
set @srRegMgr	=newid()
set @srDiviMgr	=newid()
set @srLocMgr	=newid()
set @srCandidate=newid()


insert into [Users]
(UserId,Username,[Password],ClientId,DivisionId,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy,IsDelete,IsActive)
values(@UserId,	@UserName,@Password,@clientId,NULL,getutcDate(),NULL,NULL,NULL,0,1)


INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@prePortalContent, @clientId, N'PortalContent', 0, GETUTCDATE(), NULL, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@prePortalContent,@languageId,'Portal Content')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preVacancyTmplate, @clientId, N'VacancyTemplate', 0, GETUTCDATE(), NULL, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preVacancyTmplate,@languageId,'Vacancy Template')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preApplication, @clientId, N'Application', 0, GETUTCDATE(), NULL, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preApplication,@languageId,'Application')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preSecurityRole, @clientId, N'SecurityRole', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preSecurityRole,@languageId,'Security Role')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preJobLocation, @clientId, N'JobLocation', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preJobLocation,@languageId,'Job Location')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preRating, @clientId, N'Rating', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preRating,@languageId,'Rating')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preUser, @clientId, N'User', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preUser,@languageId,'User')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preInterview, @clientId, N'Interview', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preInterview,@languageId,'Interview')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preQuestion, @clientId, N'Question', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preQuestion,@languageId,'Question')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preVacancy, @clientId, N'Vacancy', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preVacancy,@languageId,'Vacancy')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preCandidate, @clientId, N'Candidate', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preCandidate,@languageId,'Candidate')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preResume,@clientId, N'Resume', 0,GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preResume,@languageId,'Resume')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preCompany,@clientId, N'Company', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preCompany,@languageId,'Company')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preJobOffer, @clientId, N'JobOffer', 0, GETUTCDATE(),Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preJobOffer,@languageId,'Job Offer')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preVacancyQuestion, @clientId, N'VacancyQuestion', 0, GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preVacancyQuestion,@languageId,'Vacancy Question')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@prePositionType, @clientId, N'PositionType', 0,GETUTCDATE(), Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@prePositionType,@languageId,'Position Type')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preDivision, @clientId, N'Division', 0, GETUTCDATE(),Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preDivision,@languageId,'Division')

INSERT [dbo].[ATSPrivilege] ([ATSPrivilegeId], [ClientId], [Name], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (@preReviewPanel, @clientId, N'ReviewPanel', 0, GETUTCDATE(),Null, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'Privilege',@preReviewPanel,@languageId,'Review Panel')



INSERT [dbo].[ATSSecurityRole] ([ATSSecurityRoleId], [ClientId], [DefaultName], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy],[SequenceNo]) VALUES (@srLocMgr, @clientId, N'LocationManager', 0, GETUTCDATE(), NULL, NULL, NULL,306)

INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'SecurityRole',@srLocMgr,@languageId,'Location Manager')

INSERT [dbo].[ATSSecurityRole] ([ATSSecurityRoleId], [ClientId], [DefaultName], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy],[SequenceNo]) VALUES (@srCorMgr, @clientId, N'CorporateManager', 0, GETUTCDATE(), NULL, NULL, NULL,303)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'SecurityRole',@srCorMgr,@languageId,'Corporate Manager')

INSERT [dbo].[ATSSecurityRole] ([ATSSecurityRoleId], [ClientId], [DefaultName], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy],[SequenceNo]) VALUES (@srSysAdmin, @clientId, N'SystemAdministrator', 0, GETUTCDATE(), NULL, NULL, NULL,301)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'SecurityRole',@srSysAdmin,@languageId,'System Administrator')



INSERT [dbo].[ATSSecurityRole] ([ATSSecurityRoleId], [ClientId], [DefaultName], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy],[SequenceNo]) VALUES (@srRegMgr, @clientId, N'RegionalManager', 0, GETUTCDATE(), NULL, NULL, NULL,304)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'SecurityRole',@srRegMgr,@languageId,'Regional Manager')

INSERT [dbo].[ATSSecurityRole] ([ATSSecurityRoleId], [ClientId], [DefaultName], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy],[SequenceNo]) VALUES (@srCandidate, @clientId, N'Candidate', 0, GETUTCDATE(),NULL, NULL, NULL,307)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'SecurityRole',@srCandidate,@languageId,'Candidate')

INSERT [dbo].[ATSSecurityRole] ([ATSSecurityRoleId], [ClientId], [DefaultName], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy],[SequenceNo]) VALUES (@srDiviMgr, @clientId, N'DivisionManager', 0,GETUTCDATE(), NULL, NULL, NULL,305)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'SecurityRole',@srDiviMgr,@languageId,'Division Manager')

INSERT [dbo].[ATSSecurityRole] ([ATSSecurityRoleId], [ClientId], [DefaultName], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy],[SequenceNo]) VALUES (@srSetupMgr, @clientId, N'SetupManager', 0,GETUTCDATE(), NULL, NULL, NULL,302)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'SecurityRole',@srSetupMgr,@languageId,'Setup Manager')



INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientId, @srCorMgr, @preJobOffer, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preApplication, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preDivision, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preReviewPanel, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preCompany, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preDivision, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preRating, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preInterview, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preQuestion, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preJobOffer, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @prePositionType, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preCompany, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preSecurityRole, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preVacancy, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancyTmplate, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preApplication, @ptView, @scOwnD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobLocation, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preInterview, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preRating, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancy, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preDivision, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preQuestion, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preCompany, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preJobLocation, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preApplication, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preInterview, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preJobOffer, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preReviewPanel, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preInterview, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobLocation, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preReviewPanel, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preApplication, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preDivision, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preReviewPanel, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @prePositionType, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preRating, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @prePortalContent, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @prePortalContent, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preRating, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @prePositionType, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preJobLocation, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preQuestion, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobOffer, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancy, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @prePositionType, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @prePortalContent, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preDivision, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @prePositionType, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preCompany, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preUser, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preApplication, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preRating, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preVacancy, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preDivision, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preInterview, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preJobLocation, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preReviewPanel, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preQuestion, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preCompany, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preReviewPanel, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preQuestion, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancyQuestion, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preSecurityRole, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @prePositionType, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preDivision, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preUser, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancyQuestion, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preResume, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preReviewPanel, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preApplication, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobLocation, @ptView, @scOwnD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preApplication, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancy, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancyTmplate, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preCandidate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @prePositionType, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancyQuestion, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preQuestion, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancyQuestion, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preDivision, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preReviewPanel, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preDivision, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancy, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preJobLocation, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @prePositionType, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preInterview, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preApplication, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preSecurityRole, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preQuestion, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preVacancyTmplate, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancy, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preDivision, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preJobOffer, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preJobLocation, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preUser, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preJobOffer, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preSecurityRole, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancy, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preJobLocation, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preVacancy, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preInterview, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preReviewPanel, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancy, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @prePortalContent, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preQuestion, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @prePositionType, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preUser, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preReviewPanel, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancyQuestion, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preDivision, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preDivision, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preInterview, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @prePortalContent, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @prePositionType, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preReviewPanel, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preJobOffer, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preApplication, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preInterview, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancy, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancy, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preInterview, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preUser, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @prePositionType, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preVacancy, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preReviewPanel, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preQuestion, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobLocation, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preVacancyTmplate, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preReviewPanel, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancy, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @prePortalContent, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preQuestion, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preJobOffer, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preRating, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preResume, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preResume, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preRating, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preRating, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preJobOffer, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preRating, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preRating, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preQuestion, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preInterview, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preRating, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preJobLocation, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @prePortalContent, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobLocation, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobLocation, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobLocation, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobLocation, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobLocation, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobLocation, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preSecurityRole, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preRating, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preUser, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @prePortalContent, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preSecurityRole, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @prePositionType, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preJobLocation, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preReviewPanel, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preReviewPanel, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preResume, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobOffer, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preCompany, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preVacancyTmplate, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preJobOffer, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preVacancyQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preUser, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preReviewPanel, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preRating, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preReviewPanel, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobLocation, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preDivision, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @prePortalContent, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preInterview, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preInterview, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preUser, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @prePositionType, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preReviewPanel, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobOffer, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preUser, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preUser, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preVacancy, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preQuestion, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preDivision, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preInterview, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preUser, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preDivision, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preJobOffer, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preApplication, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preJobOffer, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobLocation, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancyTmplate, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preQuestion, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preApplication, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preInterview, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobLocation, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobOffer, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preVacancy, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancy, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancyTmplate, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preCompany, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @prePositionType, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preSecurityRole, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preVacancy, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preCandidate, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preReviewPanel, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preUser, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobLocation, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preInterview, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preUser, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preVacancyQuestion, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @preRating, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preUser, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preInterview, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobOffer, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preQuestion, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preJobLocation, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preCandidate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preQuestion, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobOffer, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preQuestion, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preVacancyTmplate, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preRating, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srLocMgr, @preUser, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srCorMgr, @preJobOffer, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @prePortalContent, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSetupMgr, @preCompany, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srRegMgr, @prePositionType, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srDiviMgr, @preUser, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (NEWID(), @clientid, @srSysAdmin, @preQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePortalContent, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preApplication, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePortalContent, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCompany, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePortalContent, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preApplication, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preApplication, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCandidate, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preApplication, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCandidate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePortalContent, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePositionType, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preApplication, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preDivision, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCompany, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preApplication, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCandidate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCompany, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preResume, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preCandidate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCandidate, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCompany, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preApplication, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePositionType, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preResume, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCompany, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePortalContent, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCompany, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preResume, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCompany, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCompany, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preApplication, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preSecurityRole, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePositionType, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePortalContent, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preApplication, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCompany, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preDivision, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePortalContent, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePortalContent, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)
 
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCompany, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePortalContent, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePortalContent, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePortalContent, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preDivision, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preApplication, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preApplication, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePortalContent, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePortalContent, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePositionType, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCompany, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preSecurityRole, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePortalContent, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preSecurityRole, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)
 
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCandidate, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preSecurityRole, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePortalContent, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preApplication, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preApplication, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCandidate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePositionType, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCompany, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCompany, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCandidate, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preApplication, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCompany, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preApplication, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCandidate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preApplication, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preApplication, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePositionType, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCompany, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCompany, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preSecurityRole, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preApplication, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preResume, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preResume, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePortalContent, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePortalContent, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preResume, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePortalContent, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePortalContent, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preCompany, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePositionType, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preResume, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preApplication, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePortalContent, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePortalContent, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePortalContent, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preSecurityRole, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preResume, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preApplication, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preApplication, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePortalContent, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preApplication, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCompany, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCompany, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCandidate, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCompany, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCompany, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCompany, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCompany, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preSecurityRole, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preResume, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preApplication, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCompany, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)
 
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preDivision, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preApplication, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preDivision, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCandidate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preSecurityRole, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyTmplate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePortalContent, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preApplication, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePortalContent, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preResume, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCandidate, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preResume, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preCandidate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCompany, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preResume, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preApplication, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)
  
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preApplication, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preApplication, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCompany, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePortalContent, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePortalContent, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCompany, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preApplication, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCandidate, @ptView, @scOwnD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePositionType, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePortalContent, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePortalContent, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePortalContent, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobLocation, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preResume, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptView, @scOwnD, NULL, 1, 0, GETUTCDATE(),NULL,NULL,NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCompany, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preApplication, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preApplication, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCompany, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preApplication, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobOffer, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preApplication, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePortalContent, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)
 
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preCandidate, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preReviewPanel, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preResume, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePortalContent, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preApplication, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePortalContent, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCandidate, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preSecurityRole, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preSecurityRole, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preUser, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)
  
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @prePositionType, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preApplication, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCompany, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preDivision, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePortalContent, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preInterview, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preCompany, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preUser, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preDivision, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preCompany, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preSecurityRole, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preUser, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preDivision, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCompany, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preReviewPanel, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preResume, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @prePortalContent, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preSecurityRole, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)
  
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptView, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preResume, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCompany, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyTmplate, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preCompany, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancyQuestion, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preInterview, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preSecurityRole, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preResume, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preSecurityRole, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @prePortalContent, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePositionType, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancy, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preReviewPanel, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)
 
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preCandidate, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCompany, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptCreate, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyTmplate, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptDelete, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preRating, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preJobLocation, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preDivision, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobOffer, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preReviewPanel, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preCompany, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preInterview, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preReviewPanel, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancy, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)
  
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preQuestion, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptCreate, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preResume, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCandidate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preUser, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)
  
INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preSecurityRole, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePortalContent, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preApplication, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preApplication, @ptView, @scSisterD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptDelete, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preRating, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preInterview, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancyQuestion, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyTmplate, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preCompany, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preInterview, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preDivision, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyTmplate, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preVacancy, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePortalContent, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preSecurityRole, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preDivision, @ptModify, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preRating, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @prePortalContent, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preInterview, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyQuestion, @ptDelete, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preDivision, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preRating, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preRating, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preQuestion, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancy, @ptCreate, @scAllD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobLocation, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preResume, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preVacancyQuestion, @ptModify, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preQuestion, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptView, @scOwnD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @prePositionType, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCompany, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobLocation, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srRegMgr, @preJobLocation, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @prePositionType, @ptDelete, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobLocation, @ptDelete, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preResume, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preUser, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCandidate, @ptView, @scAllD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptCreate, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preJobOffer, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preJobOffer, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancyTmplate, @ptCreate, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preRating, @ptModify, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preCandidate, @ptView, @scChildD, NULL, 0, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preSecurityRole, @ptView, @scAllD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preJobOffer, @ptDelete, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preQuestion, @ptCreate, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preVacancyQuestion, @ptView, @scSisterD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preVacancyQuestion, @ptModify, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preUser, @ptCreate, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srLocMgr, @preQuestion, @ptModify, @scSisterD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSysAdmin, @preVacancy, @ptModify, @scSisterD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preReviewPanel, @ptModify, @scChildD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srCorMgr, @preCandidate, @ptView, @scChildD, NULL, 1, 0,  GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preCompany, @ptModify, @scChildD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preVacancy, @ptCreate, @scOwnD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srSetupMgr, @preDivision, @ptDelete, @scOwnD, NULL, 1, 0,GETUTCDATE(),NULL, NULL, NULL)

INSERT [dbo].[SecurityRolePrivilege] ([SecurityRolePrivilegeId], [ClientId], [AtsSecurityRoleId], [ATSPrivilegeId], [PermissionType], [Scope], [Description], [IsChecked], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (newid(), @clientid, @srDiviMgr, @preJobOffer, @ptDelete, @scAllD, NULL, 0, 0,GETUTCDATE(),NULL, NULL, NULL)




INSERT [dbo].[UserSecurityRole] ([UserSecurityRoleId], [ClientId], [UserId], [ATSSecurityRoleId], [Description], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES 
								(newid(),@clientId,@UserId,@srSysAdmin,NULL,0,GETUTCDATE(),@UserId, NULL, NULL)


INSERT into [dbo].[UserDetails]  ( [UserDetailId],[UserId],[FirstName],[LastName],[CreatedOn],[CreatedBy],[IsDelete]) values
								 (newid(),@UserId,'Gordon','Darling',GETUTCDATE(),@UserId,0)


								


INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'42021482-8fcf-41d6-b749-0c53029bec03', @languageId, N'Vacancy', N'VacancyStatus', N'5', N'Placed', 5, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'3fbdc79a-ab0a-4a31-bcae-0f63ecd2bb6a', @languageId, N'TestDrp_Index', N'drp_test', N'2', N'Female', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'e8a7566f-7b59-4f6c-b4e9-160b6bcc8842', @languageId, N'Vacancy', N'VacancyStatus', N'7', N'Canceled', 7, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'da403a97-d861-46c4-81ba-3033764a03f3', @languageId, N'Vacancy', N'VacancyState', N'1', N'Active', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'6e5b333b-8f6f-4e0e-a312-3767b144d66f', @languageId, N'Vacancy', N'VacancyStatus', N'4', N'Offer', 4, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'e97fc78a-0efd-4be4-a136-43c86c327e59', @languageId, N'Vacancy', N'JobType', N'2', N'Part-time', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'7e7e6476-ad7e-4762-839c-46c9ebf33acc', @languageId, N'Vacancy', N'JobType', N'1', N'Full-Time', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'a56d4aef-32d7-4ae8-8dc9-47077cedd229', @languageId, N'Vacancy', N'VacancyStatus', N'10', N'Pulled', 10, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'065ffc64-fc35-4a1f-b203-52879bfd41c7', @languageId, N'Vacancy', N'VacancyStatus', N'2', N'Shortlist', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'aaf7d484-d72d-4bb7-880a-702ab188e22c', @languageId, N'Vacancy', N'EmploymentType', N'2', N'Temporary', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'437b7ed0-a9f9-46e0-92ad-831ffbafeb69', @languageId, N'Vacancy', N'VacancyStatus', N'6', N'Unfilled', 6, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'e818a412-301a-4212-9d68-85649a51fae3', @languageId, N'Vacancy', N'EmploymentType', N'1', N'Permanent', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'e7de91fe-33af-4ffe-807e-859bbbe85a17', @languageId, N'TestDrp_Index', N'drp_test', N'1', N'Male', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'61031879-25c7-4487-94f8-905a67242501', @languageId, N'Profile', N'Sex', N'2', N'Female', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'4d75fd7d-c0f0-49e7-bca2-a27f178de899', @languageId, N'Vacancy', N'VacancyState', N'2', N'Inactive', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'a8efb494-1d73-4370-9f11-c20034489069', @languageId, N'Vacancy', N'VacancyStatus', N'8', N'Filled', 8, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'e632489c-6338-44e3-b46b-cdcd12f1bad3', @languageId, N'Vacancy', N'VacancyStatus', N'1', N'New', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'6b568702-0d5c-4f2e-9ebb-d3cbf74d6a88', @languageId, N'Vacancy', N'VacancyStatus', N'11', N'Postponed', 11, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DrpStringMapping] ([DrpStringMappingId], [LanguageId], [FormName], [DrpName], [Value], [Text], [SortOrder], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'ff257876-45cd-49dd-a440-f8fada4ef36a', @languageId, N'Vacancy', N'VacancyStatus', N'3', N'Interview', 3, 0, NULL, NULL, NULL, NULL)

INSERT [StringMapping] ([StringMappingId],[KeyName],[Description]) VALUES (newid(),'Withdraw','VacancyStatus')
INSERT [StringMapping] ([StringMappingId],[KeyName],[Description]) VALUES (newid(),'Save','VacancyStatus')
INSERT [StringMapping] ([StringMappingId],[KeyName],[Description]) VALUES (newid(),'Draft','VacancyStatus')
INSERT [StringMapping] ([StringMappingId],[KeyName],[Description]) VALUES (newid(),'Submitted','VacancyStatus')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category],[Ordinal], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'f78e4b6e-5f84-4c9f-abf1-0b0614209ae3', @UserId, @clientId, N'Interview', N'Open',2, 0, CAST(0x0000A33C00C28EBF AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'f78e4b6e-5f84-4c9f-abf1-0b0614209ae3',@languageId,'Interview')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category],[Ordinal], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'de82a681-d1f0-43da-aeb0-1dbc5c4ad817', @UserId, @clientId, N'Shortlist', N'Open',3, 0, CAST(0x0000A33C00C2D634 AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'de82a681-d1f0-43da-aeb0-1dbc5c4ad817',@languageId,'Shortlist')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category], [Ordinal],[IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'2c847d4e-f682-469f-893e-279bc402e5f6', @UserId, @clientId, N'Canceled', N'Closed',10, 0, CAST(0x0000A33C00B49E69 AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'2c847d4e-f682-469f-893e-279bc402e5f6',@languageId,'Canceled')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category],[Ordinal], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'fb7b8839-9a7a-4137-baef-382fb79746d4', @UserId, @clientId, N'Pulled', N'Closed',9, 0, CAST(0x0000A33C00A86C29 AS DateTime), @UserId, CAST(0x0000A33C00B2D6B5 AS DateTime), @UserId)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'fb7b8839-9a7a-4137-baef-382fb79746d4',@languageId,'Pulled')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category],[Ordinal], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'2a7af666-222e-4ed8-9733-4454e9ff5f58', @UserId, @clientId, N'Offer', N'Open', 4,0, CAST(0x0000A33C00C252CB AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'2a7af666-222e-4ed8-9733-4454e9ff5f58',@languageId,'Offer')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category],[Ordinal], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'5f180fa8-caf3-46c0-9b73-a7c885ee73c0', @UserId, @clientId, N'Filled', N'Closed',8, 0, CAST(0x0000A33C00B47623 AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'5f180fa8-caf3-46c0-9b73-a7c885ee73c0',@languageId,'Filled')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category],[Ordinal], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'648a0568-f3e7-4847-ab73-dd4059dfc1cf', @UserId, @clientId, N'Postponed', N'Closed',7, 0, CAST(0x0000A33C00B4B6E2 AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'648a0568-f3e7-4847-ab73-dd4059dfc1cf',@languageId,'Postponed')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category], [Ordinal],[IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'1245af47-90c1-4eb2-bc56-ddeda8452871', @UserId, @clientId, N'Placed', N'Closed', 6,0, CAST(0x0000A33C00B3F4F4 AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'1245af47-90c1-4eb2-bc56-ddeda8452871',@languageId,'Placed')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category],[Ordinal], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'da1e6183-9b32-4a5d-aad0-f40c9ae22763', @UserId, @clientId, N'Unfilled', N'Closed',5, 0, CAST(0x0000A33C00B4236F AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'da1e6183-9b32-4a5d-aad0-f40c9ae22763',@languageId,'Unfilled')

INSERT [dbo].[VacancyStatus] ([VacancyStatusId], [UserId], [ClientId], [VacancyStatusText], [Category],[Ordinal], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'7b833998-7b77-451d-a295-f773e2956f02', @UserId, @clientId, N'New', N'Open',1, 0, CAST(0x0000A33C00C27282 AS DateTime), @UserId, NULL, NULL)
INSERT [dbo].[EntityLanguage]  ([EntityLanguageId],[EntityName],[RecordId],[LanguageId],[LocalName]) VALUES (newid(),'VacancyStatus',N'7b833998-7b77-451d-a295-f773e2956f02',@languageId,'New')


/****** Object:  Table [dbo].[SearchResume]    Script Date: 06/05/2014 14:39:33 ******/
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'6e90f538-535c-4d5f-b9e0-000bdf41d915', NULL, N'EducationHistory', N'EducationHistory', N'/Content/images/Education_16.png', 2, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'6e90f538-535c-4d5f-b9e0-000bdf41d915', @languageId, N'Education History')

INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'40005ce1-3593-4679-801d-001505d2964f', N'b209392d-dc88-40d5-96dd-00ed77fccc96', N'ACIssuingAuthority', N'ACIssuingAuthority', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'40005ce1-3593-4679-801d-001505d2964f', @languageId, N'Issuing Authority')

INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'42b0db30-7ce7-45b5-b1d1-007676d0bac7', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', N'EMHDutiesAndResponsibilities', N'EMHDutiesAndResponsibilities', NULL, 3, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'42b0db30-7ce7-45b5-b1d1-007676d0bac7', @languageId, N'Duties And Responsibilities')

INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'b209392d-dc88-40d5-96dd-00ed77fccc96', NULL, N'Achievements', N'Achievements', N'/Content/images/Achievements_16.png', 9, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'b209392d-dc88-40d5-96dd-00ed77fccc96', @languageId, N'Achievements')

INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'3a89b217-6fef-44fe-87c2-08aad22a1526', N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', N'EligibleToWorkInUS', N'AVEligibleToWorkInUS', NULL, 1, N'bit', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'3a89b217-6fef-44fe-87c2-08aad22a1526', @languageId, N'Eligible to Work in the US')



INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'143758c0-97ac-44c7-aab1-18a13d4ccddd', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', N'EMHCity', N'EMHCity', NULL, 2, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'6a5f8afd-1b66-42b3-8ee3-1cb8f386a69c', N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', N'AVDateAvailability', N'AVDateAvailability', NULL, 7, N'DateTime', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'5f8f85e5-d92b-49a7-817c-1d058a0379bc', N'4800625c-fe9b-4b6f-8922-afca0320f39b', N'LCIssuingAuthority', N'LCIssuingAuthority', NULL, 2, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'7c62dc04-c138-4aba-98f3-22c99f255465', N'4800625c-fe9b-4b6f-8922-afca0320f39b', N'LCName', N'LCName', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'46b392eb-13e2-4d27-ae0a-2aa6a0cdae3b', N'82ca3f55-1331-4170-bf5c-ed3a4e1f6b9d', N'ASLink', N'ASLink', NULL, 3, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'38bd03f3-7a75-43f8-b921-2bbf8cfe3bc1', NULL, N'References', N'References', N'/Content/images/References_16.png', 7, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'4dce7cbb-689d-45a3-bc2a-2e98b66101f7', N'38bd03f3-7a75-43f8-b921-2bbf8cfe3bc1', N'RFReferenceEmail', N'RFReferenceEmail', NULL, 2, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'4dbd6a8c-24f6-41bb-bef5-2eb52bc3670a', N'38bd03f3-7a75-43f8-b921-2bbf8cfe3bc1', N'RFReferencePhone', N'RFReferencePhone', NULL, 3, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'3e000fb1-0b40-4608-8a94-3992da2fe59c', NULL, N'ProfileInfo', N'ProfileInfo', N'/Content/images/Profile_16.png', 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'39626056-7e21-436d-a859-3aaca3c58e9b', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'Fax', N'Fax', NULL, 16, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'9ddba405-9de4-4755-b392-3d9e43bfa1a1', N'82ca3f55-1331-4170-bf5c-ed3a4e1f6b9d', N'ASName', N'ASName', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'bf7d7b5b-4864-4694-b5f7-3f702be9f160', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'Address', N'Address', NULL, 8, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'ba49f43c-7a74-44dd-8b96-4392ff6be151', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', N'MostRecentPosition', N'EMHMostRecentPosition', NULL, 7, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'13201aa5-d12b-4bdf-b575-442ceeb2c136', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'MiddleName', N'MiddleName', NULL, 3, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'f5b7e2a7-47ea-408c-9d7e-48cdbec90e52', N'82ca3f55-1331-4170-bf5c-ed3a4e1f6b9d', N'ASRole', N'ASRole', NULL, 4, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'b76f82da-994d-4830-bb33-4bc656102f68', N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', N'RelativesWorkingInCompany', N'AVRelativesWorkingInCompany', NULL, 3, N'bit', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'7182e2f8-dcc3-42ab-899f-4e70659317ae', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', N'CompanyName', N'EMHCompanyName', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'f08d1e8d-e45d-4a6d-9cca-50029c293e03', N'b1d49af2-0064-425c-a9e0-baf3d5dcdc9b', N'Description', N'SKDescription', NULL, 2, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'bafe4ed4-1d45-4c1c-a11e-50507742e3a8', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'LastName', N'LastName', NULL, 4, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'862b1df5-bb66-4274-8c34-5bd9d4a0f3b1', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'Pager', N'Pager', NULL, 17, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'1056b7f2-2b32-4b1f-8631-5c354f767519', N'6e90f538-535c-4d5f-b9e0-000bdf41d915', N'Degree Type', N'EHDegreeType', NULL, 2, N'Priority', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'8664979b-3f98-4bb5-a11b-6a8be41430f0', NULL, N'EmploymentHistory', N'EmploymentHistory', N'/Content/images/Employment_16.png', 3, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'2a59fe39-0aad-4753-bd34-750f25cf92f6', N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', N'AVTargetIncome', N'AVTargetIncome', NULL, 6, N'int', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'6ac3bd14-7301-4b85-9dad-76417b6f5d4f', N'b1d49af2-0064-425c-a9e0-baf3d5dcdc9b', N'SkillAndQualification', N'SKSkillAndQualification', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'f247eb97-72c8-4079-bb8e-765a727392d4', N'6e90f538-535c-4d5f-b9e0-000bdf41d915', N'EHInstitutionName', N'EHInstitutionName', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'9751041d-3310-4468-9513-77ee05668cf6', N'b209392d-dc88-40d5-96dd-00ed77fccc96', N'ACDescription', N'ACDescription', NULL, 2, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'09a02798-14cb-4045-afc9-797655bcad62', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'State', N'State', NULL, 7, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'508718e0-af3f-4347-83c5-856eb6b9eaeb', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'City', N'City', NULL, 6, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'e2005525-5996-4951-8028-86a83e390143', N'b1d49af2-0064-425c-a9e0-baf3d5dcdc9b', N'SkillType', N'SKSkillType', NULL, 3, N'Select', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'91be7de7-118b-41a4-8bc3-890648d342c9', N'6e90f538-535c-4d5f-b9e0-000bdf41d915', N'MajorValue', N'EHMeasureValue', NULL, 4, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'8e5e9aa1-e0e5-4466-b415-9ceb3c26fc2c', N'82ca3f55-1331-4170-bf5c-ed3a4e1f6b9d', N'ASAssociationType', N'ASAssociationType', NULL, 2, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'fcdb4a98-94ea-4061-9445-a148912cdfb2', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'CountryCode', N'CountryCode', NULL, 18, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'cec49982-c997-448c-b860-aaa1e3103242', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'HomePhone', N'HomePhone', NULL, 14, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'39305c7a-1764-490c-89d8-acdb91e753e2', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'FirstName', N'FirstName', NULL, 2, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'19cf70e5-d468-4337-a475-ae279446ab0b', N'6e90f538-535c-4d5f-b9e0-000bdf41d915', N'MajorSubject', N'EHMajorSubject', NULL, 3, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'a4af1252-975d-4c6f-8e35-aeb40e05d790', N'38bd03f3-7a75-43f8-b921-2bbf8cfe3bc1', N'RFReferenceName', N'RFReferenceName', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'4800625c-fe9b-4b6f-8922-afca0320f39b', NULL, N'LicenceAndCertification', N'LicenceAndCertification', N'/Content/images/Profile_16.png', 6, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'31a4efef-7bc2-4872-81b4-b6a99dc2f4cf', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'MobilePhone', N'MobilePhone', NULL, 15, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'b1d49af2-0064-425c-a9e0-baf3d5dcdc9b', NULL, N'Skills', N'Skills', N'/Content/images/Skills_16.png', 4, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'f5f9022a-49d4-4f52-a7fd-c507cc9cf59f', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', N'EndingPay', N'EMHEndingPay', NULL, 6, 'int', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', NULL, N'Availibility', N'Availibility', N'/Content/images/Availability_16.png', 5, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'435421ff-18dd-471f-ad7b-ca99e43b1911', N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', N'AVEmploymentPreference', N'AVEmploymentPreference', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'7f8db95b-3566-436d-9fce-cb4061e4a962', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', N'Experience', N'EMHExperience', NULL, 4, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'063659a4-e1ed-4050-9eed-cd8584a63e33', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', N'StartingPay', N'EMHStartingPay', NULL, 5, 'int', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'e188a550-70e2-404a-9b0d-dc610a32aa8e', N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', N'WillingToWorkOverTime', N'AVWillingToWorkOverTime', NULL, 1,  N'bit', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'6725f88a-0fce-45df-876f-dd6a0a0ac1b3', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'BussinessPhone', N'BusinessPhoneNo', NULL, 12, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'1eaa714a-9610-4373-b8b8-e102ea3307c0', N'6e90f538-535c-4d5f-b9e0-000bdf41d915', N'MajorSystem', N'EHMeasureSystem', NULL, 5, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'759edb8e-80a6-4f02-bd7c-e98096647c35', N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', N'SubmittedApplicationBefore', N'AVSubmittedApplicationBefore', NULL, 2, N'bit', 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'82ca3f55-1331-4170-bf5c-ed3a4e1f6b9d', NULL, N'Associations', N'Associations', N'/Content/images/Associations_16.png', 8, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'7fabf099-61c6-4468-aa2c-f30a1af48517', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', N'JobCategory', N'EMHJobCategory', NULL, 7, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'24b706fa-4d19-4cac-a492-f4cffa98f8f3', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'CountryCode', N'CountryCode', NULL, 10, NULL, 1, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[SearchResume] ([SearchResumeId], [ParentDivisionId], [DefaultName], [Value], [Icon], [Ordinal], [Type], [IsActive], [IsDelete], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (N'78335d2b-0a01-4e96-9c6e-f4e98846b540', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', N'Affix', N'Affix', NULL, 1, NULL, 1, 0, NULL, NULL, NULL, NULL)


INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'143758c0-97ac-44c7-aab1-18a13d4ccddd', @languageId, N'City')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'6a5f8afd-1b66-42b3-8ee3-1cb8f386a69c', @languageId, N'Date Available')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'5f8f85e5-d92b-49a7-817c-1d058a0379bc', @languageId, N'Issuing Authority')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'7c62dc04-c138-4aba-98f3-22c99f255465', @languageId, N'Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'46b392eb-13e2-4d27-ae0a-2aa6a0cdae3b', @languageId, N'Link')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'38bd03f3-7a75-43f8-b921-2bbf8cfe3bc1', @languageId, N'References')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'4dce7cbb-689d-45a3-bc2a-2e98b66101f7', @languageId, N'Reference Email')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'4dbd6a8c-24f6-41bb-bef5-2eb52bc3670a', @languageId, N'Reference Phone')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'3e000fb1-0b40-4608-8a94-3992da2fe59c', @languageId, N'Profile Info')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'39626056-7e21-436d-a859-3aaca3c58e9b', @languageId, N'Fax')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'9ddba405-9de4-4755-b392-3d9e43bfa1a1', @languageId, N'Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'bf7d7b5b-4864-4694-b5f7-3f702be9f160', @languageId, N'Address')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'ba49f43c-7a74-44dd-8b96-4392ff6be151', @languageId, N'Most Recent Position')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'13201aa5-d12b-4bdf-b575-442ceeb2c136', @languageId, N'Middle Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'f5b7e2a7-47ea-408c-9d7e-48cdbec90e52', @languageId, N'Role')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'b76f82da-994d-4830-bb33-4bc656102f68', @languageId, N'Relatives Working In Company')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'7182e2f8-dcc3-42ab-899f-4e70659317ae', @languageId, N'Company Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'f08d1e8d-e45d-4a6d-9cca-50029c293e03', @languageId, N'Description')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'bafe4ed4-1d45-4c1c-a11e-50507742e3a8', @languageId, N'Last Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'862b1df5-bb66-4274-8c34-5bd9d4a0f3b1', @languageId, N'Pager')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'1056b7f2-2b32-4b1f-8631-5c354f767519', @languageId, N'Degree Type')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'7ba3ba5a-5e23-407e-a0f5-6828e4f34ef5', @languageId, N'Company Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'8664979b-3f98-4bb5-a11b-6a8be41430f0', @languageId, N'Employment History')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'2a59fe39-0aad-4753-bd34-750f25cf92f6', @languageId, N'Target Annual Income')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'6ac3bd14-7301-4b85-9dad-76417b6f5d4f', @languageId, N'Skill / Qualification')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'f247eb97-72c8-4079-bb8e-765a727392d4', @languageId, N'Institution Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'9751041d-3310-4468-9513-77ee05668cf6', @languageId, N'Description')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'09a02798-14cb-4045-afc9-797655bcad62', @languageId, N'State')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'508718e0-af3f-4347-83c5-856eb6b9eaeb', @languageId, N'City')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'e2005525-5996-4951-8028-86a83e390143', @languageId, N'Skill Type')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'91be7de7-118b-41a4-8bc3-890648d342c9', @languageId, N'Measure Value')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'8e5e9aa1-e0e5-4466-b415-9ceb3c26fc2c', @languageId, N'Association Type')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'fcdb4a98-94ea-4061-9445-a148912cdfb2', @languageId, N'Country Code')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'cec49982-c997-448c-b860-aaa1e3103242', @languageId, N'Home Phone')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'39305c7a-1764-490c-89d8-acdb91e753e2', @languageId, N'First Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'19cf70e5-d468-4337-a475-ae279446ab0b', @languageId, N'Major Subject')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'a4af1252-975d-4c6f-8e35-aeb40e05d790', @languageId, N'Reference Name')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'4800625c-fe9b-4b6f-8922-afca0320f39b', @languageId, N'Licenses and Certifications')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'31a4efef-7bc2-4872-81b4-b6a99dc2f4cf', @languageId, N'Mobile Phone')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'b1d49af2-0064-425c-a9e0-baf3d5dcdc9b', @languageId, N'Skills')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'f5f9022a-49d4-4f52-a7fd-c507cc9cf59f', @languageId, N'Ending Pay')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'9f10ebf1-d0a8-463c-8eb2-c9a262dd6919', @languageId, N'Availability')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'435421ff-18dd-471f-ad7b-ca99e43b1911', @languageId, N'Employment Preference')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'7f8db95b-3566-436d-9fce-cb4061e4a962', @languageId, N'Experience')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'063659a4-e1ed-4050-9eed-cd8584a63e33', @languageId, N'Starting Pay')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'e188a550-70e2-404a-9b0d-dc610a32aa8e', @languageId, N'Willing to Work Over-time')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'6725f88a-0fce-45df-876f-dd6a0a0ac1b3', @languageId, N'Best Phone to Reach Me')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'4dc83be0-be50-4f30-8502-df1ece810ff5', @languageId, N'Skill And Qualification')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'1eaa714a-9610-4373-b8b8-e102ea3307c0', @languageId, N'Measure System')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'759edb8e-80a6-4f02-bd7c-e98096647c35', @languageId, N'Submitted Application Before')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'82ca3f55-1331-4170-bf5c-ed3a4e1f6b9d', @languageId, N'Associations')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'7fabf099-61c6-4468-aa2c-f30a1af48517', @languageId, N'Job Category')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'24b706fa-4d19-4cac-a492-f4cffa98f8f3', @languageId, N'CountryCode')
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName]) VALUES (NEWID(), N'SearchResume', N'78335d2b-0a01-4e96-9c6e-f4e98846b540', @languageId, N'Affix')

Declare @myid uniqueidentifier

--EducationHistory
set @myid = newid()
Insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn) values
(@myid ,'6E90F538-535C-4D5F-B9E0-000BDF41D915','NumberOfEducation','EHNumberofJob',NULL,0,'count',1,0,NULL,NULL,NULL,NULL)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) Values
(newid(),'SearchResume',@myid,@languageId,'Number of Education Entries',0)

--Achievements
set @myid = newid()
Insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn) values
(@myid ,'B209392D-DC88-40D5-96DD-00ED77FCCC96','NumberOfAchievements','ACNumberofJob',NULL,0,'count',1,0,NULL,NULL,NULL,NULL)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) Values
(newid(),'SearchResume',@myid,@languageId,'Number Of Achievements',0)

--References
set @myid = newid()
Insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn) values
(@myid ,'38BD03F3-7A75-43F8-B921-2BBF8CFE3BC1','NumberOfReferences','RFNumberofJob',NULL,0,'count',1,0,NULL,NULL,NULL,NULL)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) Values
(newid(),'SearchResume',@myid,@languageId,'Number Of References',0)

--EmploymentHistory
set @myid = newid()
Insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn) values
(@myid ,'8664979B-3F98-4BB5-A11B-6A8BE41430F0','NumberOfJobs','EMHNumberofJob',NULL,0,'count',1,0,NULL,NULL,NULL,NULL)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) Values
(newid(),'SearchResume',@myid,@languageId,'Number Of Jobs',0)

--LicenceAndCertification
set @myid = newid()
Insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn) values
(@myid ,'4800625C-FE9B-4B6F-8922-AFCA0320F39B','NumberOfLicenceAndCertification','LCNumberofJob',NULL,0,'count',1,0,NULL,NULL,NULL,NULL)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) Values
(newid(),'SearchResume',@myid,@languageId,'Number Of Licenses And Certification',0)

--Associations
set @myid = newid()
Insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn) values
(@myid ,'82CA3F55-1331-4170-BF5C-ED3A4E1F6B9D','NumberOfAssociations','ASNumberofJob',NULL,0,'int',1,0,NULL,NULL,NULL,NULL)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) Values
(newid(),'SearchResume',@myid,@languageId,'Number Of Associations',0)

set @myid = newid()
insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)
values(@myid,'3E000FB1-0B40-4608-8A94-3992DA2FE59C','HomeEmail','HomeEmail',null,19,'contains',1,0,null,null,null,null)
  
Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete)
values(NEWID(),'SearchResume',@myid,@languageId,'Home Email',null)

set @myid = newid()
insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)
values(@myid,'3E000FB1-0B40-4608-8A94-3992DA2FE59C','Website','Website',null,20,'contains',1,0,null,null,null,null)
  
Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete)
values(NEWID(),'SearchResume',@myid,@languageId,'Website',null)

set @myid = newid()
insert into SearchResume (SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn)
values(@myid,'3E000FB1-0B40-4608-8A94-3992DA2FE59C','PostOfficeBox','PostOfficeBox',null,21,null,1,0,null,null,null,null)
  
Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete)
values(NEWID(),'SearchResume',@myid,@languageId,'Post Office Box No',null)

declare @parentId uniqueidentifier
declare @newid uniqueidentifier

select @parentId =SearchResumeID from SearchResume where DefaultName  like '%Availibility%'

set @newid=newid();

Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'18yearsolder','AVHowOld',NULL,0,N'bit',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Are you 18 years or older?',0)


select @parentId =SearchResumeID from SearchResume where DefaultName  like 'EmploymentHistory%'
--Suerpviser name
set @newid=newid();

Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'SuperwiserName','EMHSuperwiserName',NULL,0,N'contains',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Super wiser Name and title',0)

--Starting Position

set @newid=newid();

Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'StartingPosition','EMHStartigPosition',NULL,0,NULL,1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Starting Position',0)

--REason for leaving

--Starting Position

set @newid=newid();

Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'ReasonForLeaving','EMHReasonForLeaving',NULL,0,N'contains',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Reason For Leaving',0)

--Achievements
select @parentId =SearchResumeID from SearchResume where DefaultName  like 'Achievements%'
--DATE

set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'Date','ACDate',NULL,0,N'DateTime',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Date',0)

--EDucation History
select @parentId =SearchResumeID from SearchResume where DefaultName  like 'EDucationHistory%'
--Start DATE

set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'StartDate','EHStartDate',NULL,0,N'DateTime',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Start Date',0)

--End Date

set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'EndDate','EHEndDate',NULL,0,N'DateTime',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'End Date',0)


--Degree Date

set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'DegreeDate','EHDegreeDate',NULL,0,N'DateTime',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Degree Date',0)

--Description

set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'Description','EHDescription',NULL,0,NULL,1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Description',0)

--Licence And Certification

select @parentId =SearchResumeID from SearchResume where DefaultName  like 'LICENCEAND%'
--Valid From


set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'ValidFrom','LCStartDate',NULL,0,N'DateTime',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Valid From',0)

--Valid To

set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'ValidTo','LCEndDate',NULL,0,N'DateTime',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Valid To',0)

--description
set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'Description','LCDescription',NULL,0,NULL,1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Description',0)


--EDucation History
select @parentId =SearchResumeID from SearchResume where DefaultName  like 'ASSOCIA%'
--Start DATE

set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'StartDate','ASStartDate',NULL,0,N'DateTime',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'Start Date',0)

--End Date

set @newid=newid();
Insert into SearchResume 
(SearchResumeId,ParentDivisionId,DefaultName,Value,Icon,Ordinal,Type,IsActive,IsDelete) 
VALUES 
(@newid,@parentId,'EndDate','ASEndDate',NULL,0,N'DateTime',1,0)

Insert into EntityLanguage (EntityLanguageId,EntityName,RecordId,LanguageId,LocalName,Isdelete) values(NEWID(),'SearchResume',@newid,@languageId,'End Date',0)


--Degree Type

INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'60bd5256-0657-436f-ba16-03d383cfd5dc',@clientid, N'M.Phil. (Master of Philosophy)', 4, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'086f9ea7-697e-48da-a8bf-057ccfee39b0',@clientid, N'A.A. (Associate of Arts)dsa', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'ec70e11a-5417-461a-bd56-0679876a11f0',@clientid, N'B.F.A. (Bachelor of Fine Arts)', 3, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'e32f3b62-8841-46b2-b4f0-0c737dd21251',@clientid, N'M.F.A. (Master of Fine Arts)', 4, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'9b5665bc-952d-4172-9615-1aeb56840cbf',@clientid, N'M.Res. (Master of Research)', 4, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'82e008ad-86a1-496e-af70-1c501a199997',@clientid, N'B.A. (Bachelor of Arts)', 3, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'089d5daa-a3be-49b1-a111-2913c648f4a9',@clientid, N'B.Arch. (Bachelor of Architecture)', 3, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'9b0d3c0f-21c5-491a-bdf4-3ea530cceb7f',@clientid, N'M.A. (Master of Arts)', 4, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'd4a016c8-f004-4097-98d8-4dcca51cb37b',@clientid, N'Ed.D. (Doctor of Education)', 5, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'4385d0c4-3b27-4865-a767-60273fdee3db',@clientid, N'M.B.A. (Master of Business Administration)', 4, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'1b36d734-4e9f-4016-a461-64919da3b535',@clientid, N'LL.M. (Master of Laws)', 4, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'5ca77a12-6848-4bff-93f0-71db46eeb9fb',@clientid, N'J.D. (Juris Doctor)', 5, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'b25cf14c-6dac-45ec-bf6f-75e275189651',@clientid, N'B.B.A. (Bachelor of Business Administration)', 3, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'99c5a1a1-8d9e-4b0b-94c2-9e3be0211a8a',@clientid, N'A.A.S. (Associate of Applied Science)', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'6562ae4a-2675-4753-8c37-b145cb871d65',@clientid, N'A.E. (Associate of Engineering)', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'f098f106-9a53-4d84-b3fb-bc8b6d5fe793',@clientid, N'B.S. (Bachelor of Science)', 3, 0,NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'd9d710a0-3628-4091-99ea-bd01e31dd947',@clientid, N'M.D. (Doctor of Medicine)', 5, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'90cdf8f1-a941-42bf-9fab-bf9e93d8c158',@clientid, N'PhD (Doctor of Philosophy)', 5, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'a5a4ecb7-4231-4f79-9c68-c16822321330',@clientid, N'M.S. (Master of Science)', 4, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'dd6deb35-ced7-46d2-ba90-d0be96a2fd76',@clientid, N'A.S (Associate of Science)', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'16d0e53a-1866-4c41-af52-dd9e814718e8',@clientid, N'A.P.S. (Associate of Political Science)', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'851c3063-590a-4f0d-b526-e6b5966da0f2',@clientid, N'A.A. (Associate of Arts) h', 2, 0, NULL, NULL, NULL, NULL)
INSERT [dbo].[DegreeType] ([DegreeTypeId], [ClientId], [DefaultName], [Priority], [IsDelete], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (N'0555229a-5f5e-4d90-af75-fdc17f5288bd',@clientid, N'A.A.A. (Associate of Applied Arts)', 2, 0, NULL, NULL, NULL, NULL)

INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'60bd5256-0657-436f-ba16-03d383cfd5dc', @languageId, N'M.Phil. (Master of Philosophy)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'086f9ea7-697e-48da-a8bf-057ccfee39b0', @languageId, N'A.A. (Associate of Arts) h', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'ec70e11a-5417-461a-bd56-0679876a11f0', @languageId, N'B.F.A. (Bachelor of Fine Arts)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'e32f3b62-8841-46b2-b4f0-0c737dd21251', @languageId, N'M.F.A. (Master of Fine Arts)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'9b5665bc-952d-4172-9615-1aeb56840cbf', @languageId, N'M.Res. (Master of Research)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'82e008ad-86a1-496e-af70-1c501a199997', @languageId, N'B.A. (Bachelor of Arts)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'089d5daa-a3be-49b1-a111-2913c648f4a9', @languageId, N'B.Arch. (Bachelor of Architecture)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'9b0d3c0f-21c5-491a-bdf4-3ea530cceb7f', @languageId, N'M.A. (Master of Arts)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'd4a016c8-f004-4097-98d8-4dcca51cb37b', @languageId, N'Ed.D. (Doctor of Education)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'4385d0c4-3b27-4865-a767-60273fdee3db', @languageId, N'M.B.A. (Master of Business Administration)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'1b36d734-4e9f-4016-a461-64919da3b535', @languageId, N'LL.M. (Master of Laws)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'5ca77a12-6848-4bff-93f0-71db46eeb9fb', @languageId, N'J.D. (Juris Doctor)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'b25cf14c-6dac-45ec-bf6f-75e275189651', @languageId, N'B.B.A. (Bachelor of Business Administration)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'99c5a1a1-8d9e-4b0b-94c2-9e3be0211a8a', @languageId, N'A.A.S. (Associate of Applied Science)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'6562ae4a-2675-4753-8c37-b145cb871d65', @languageId, N'A.E. (Associate of Engineering)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'f098f106-9a53-4d84-b3fb-bc8b6d5fe793', @languageId, N'B.S. (Bachelor of Science)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'd9d710a0-3628-4091-99ea-bd01e31dd947', @languageId, N'M.D. (Doctor of Medicine)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'90cdf8f1-a941-42bf-9fab-bf9e93d8c158', @languageId, N'PhD (Doctor of Philosophy)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'a5a4ecb7-4231-4f79-9c68-c16822321330', @languageId, N'M.S. (Master of Science)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'dd6deb35-ced7-46d2-ba90-d0be96a2fd76', @languageId, N'A.S (Associate of Science)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'16d0e53a-1866-4c41-af52-dd9e814718e8', @languageId, N'A.P.S. (Associate of Political Science)', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'851c3063-590a-4f0d-b526-e6b5966da0f2', @languageId, N'A.A. (Associate of Arts) h', 0)
INSERT [dbo].[EntityLanguage] ([EntityLanguageId], [EntityName], [RecordId], [LanguageId], [LocalName], [IsDelete]) VALUES (newid(), N'DegreeType', N'0555229a-5f5e-4d90-af75-fdc17f5288bd', @languageId, N'A.A.A. (Associate of Applied Arts)', 0)



GO

