﻿CREATE TABLE [dbo].[Account] (
    [ID] [int] NOT NULL IDENTITY,
    [Login] [varchar](25) NOT NULL,
    [Password] [varchar](50) NOT NULL,
    [Created] [datetime] DEFAULT GETDATE(),
    [Updated] [datetime],
    [Active] [bit],
    CONSTRAINT [PK_dbo.Account] PRIMARY KEY ([ID])
)
CREATE TABLE [dbo].[EmergencyType] (
    [ID] [int] NOT NULL IDENTITY,
    [Description] [varchar](50),
    [Created] [datetime] DEFAULT GETDATE(),
    [Updated] [datetime],
    [Active] [bit],
    CONSTRAINT [PK_dbo.EmergencyType] PRIMARY KEY ([ID])
)
CREATE TABLE [dbo].[HealthUnit] (
    [ID] [int] NOT NULL IDENTITY,
    [InstitutionTypeID] [int],
    [EmergencyTypeID] [int],
    [Name] [varchar](150),
    [Address] [varchar](8000),
    [Phone] [varchar](50),
    [Latitude] [varchar](20),
    [Longitude] [varchar](20),
    [LinkPT] [varchar](100),
    [LinkEN] [varchar](150),
    [SpecialtiesPT] [varchar](8000),
    [SpecialtiesEN] [varchar](8000),
    [SpecialtiesES] [varchar](8000),
    [Created] [datetime] DEFAULT GETDATE(),
    [Updated] [datetime],
    [Active] [bit],
    CONSTRAINT [PK_dbo.HealthUnit] PRIMARY KEY ([ID])
)
CREATE INDEX [IX_InstitutionTypeID] ON [dbo].[HealthUnit]([InstitutionTypeID])
CREATE INDEX [IX_EmergencyTypeID] ON [dbo].[HealthUnit]([EmergencyTypeID])
CREATE TABLE [dbo].[InstitutionType] (
    [ID] [int] NOT NULL IDENTITY,
    [Description] [varchar](50),
    [Created] [datetime] DEFAULT GETDATE(),
    [Updated] [datetime],
    [Active] [bit],
    CONSTRAINT [PK_dbo.InstitutionType] PRIMARY KEY ([ID])
)
ALTER TABLE [dbo].[HealthUnit] ADD CONSTRAINT [FK_dbo.HealthUnit_dbo.EmergencyType_EmergencyTypeID] FOREIGN KEY ([EmergencyTypeID]) REFERENCES [dbo].[EmergencyType] ([ID])
ALTER TABLE [dbo].[HealthUnit] ADD CONSTRAINT [FK_dbo.HealthUnit_dbo.InstitutionType_InstitutionTypeID] FOREIGN KEY ([InstitutionTypeID]) REFERENCES [dbo].[InstitutionType] ([ID])
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201808261956062_Initial', N'MedicalEmergency.Infrastructure.Data.Migrations.Configuration',  0x1F8B0800000000000400ED5CDB6EDB46107D2FD07F20F85838A2EC20406A48091CD96E85C6178476D0B7604DAEE445C825CB5DBA368A7E591FFA49FD85CE8A17ED855789725CD70810587B39333B7B76765633F63F7FFD3D797F1F06D61D4E1889E8D4DE1F8D6D0B532FF2095D4EED942F5EBDB5DFBFFBFEBBC9891FDE5B9F8B71AFC5389849D9D4BEE53C3E741CE6DDE210B15148BC2462D1828FBC2874901F3907E3F18FCEFEBE8301C2062CCB9A7C4A2927215E7D808FB3887A38E6290ACE221F072C6F871E77856A9DA310B31879786A9F619F7828380971B2044D1F4673BA4810E349EAF134C1A363C4D1E85228CA38F463DB3A0A08023D5D1C2C6C0B511A71C4611587D70CBB3C89E8D28DA10105570F3186710B14309CAFEE703DBCEB42C70762A1CE7A6201E5A58C47614FC0FDD7B9E51C7DFA46F6B74BCB826D4F600FF88358F5CABE53FBC8F322D818DBD2651DCE82448CABB0FE318C2374B4C222988DCE10454B9C8C72AC3DAB66C65E4926E09CF8B767CDD240ECE094E2942728D8B32ED39B8078BFE087ABE82BA6539A0681BC005802F4290DD0749944314EF8C327BCC897353FB62D479DE7E813CB69D29C6CC573CA5F1FD8D639084737012EF92159C7E551827FC214278863FF12718E13D8DEB98F571636A46BB23E464B420B71C048387AB67586EE3F62BAE4B753FBE08D6D9D927BEC170DB906D794C0418539C07D5CA161B3D44BC4D8EF51E237087E33DE85E0598285990AB9705EF115F882B669D7B1BFC9B4238F933B5CCCFA10450146549D3471D627A1F17C941C166D5B9D9295AB1929782F27A5FDA41C63E62524CEDCDFB6B47DAE2CFD19A380DFC262B773E46B981766B633734E190C4B05941069886E9EAD38829E73C5FF0D87617F90D370E4FB0966AC410EFC38809CCBDB8836AD6690C57C4462ABFC26390783C881D0F2710411FAF5F2AA8904E3A1C49C9CEF9C6B6E8C3D88C685136A5CD4308C93A435AE6D7869EECEA53DB93B0C269DA33BB25CB9DC3A1F685B9F70B01AC16E499CBDDBA4DBE88B34F03489C24F51A05C7AEBFE2F6E9426E2017815350CBA42F003EFAEA7E4E9DB34558656EA2A8D68D2561E56A56FE7D840BBA606886135C49758E1258ADD248A3D622C02CF286C6284B1D28957C59F50DFEA72FC3313AB87136C0DF42331100ED499DA3F188B6B412FCEA184AEBD0F5501E3D168DF9001A4C589600D0A66E045E01810CA4D8613EA9118051DD4D1E6763C1F625F4A297ACF318E3115D4EE60EB2EE28D60D7D4A514A99DE236734D1C89479DE925BBE90E14A8F4D90352ACCAD94BF8860B7F049A55A8F4B844ABB0792705CC57D923902DF3703087C30C9CE4BAE8F7A2E8C7F7556FF46B86F35B98E55E5E678DC07731D7BFB65DBBD69C2B658FC13B1541735C068ED6DF82269F00034AEE6CC131986E80192334446983AAD4934349696853C8A9B3A7E315542E4DB58E41C68E778E04D7BC358E6A817ED651C2D746FBD47AD03E3E742B1B55394D09B08D28F5762A0296F238AF73534E969C2A92584E4D166B7286E218A23B29AB95B7586E96D29ABD72FB6773C20CC3F1584552A7D4B69404212B5A62AD174483A6A724615CA4CD6E900830677E680CAB735E3507B890AAFA2773238BE35C8C173F577BCBB62CDFA846C8DAD8A7B0FE505C2EABE85DE246EDCC55FA110528A9782BCCA2200D69FD35573F3BCFF6C8007953778C75EE468659B776472A1F083250D9D81DA77C31C8386563779CE20921C3146D26CAC4D136D7B8DC0D8E69E75EA76C2742AB3E77A7B46E14D581DC2DF3774371E5812BC3281D2F245549383049A53B74A70CAD97D3819E4D9377C3CD8A278102D6FE62A8C7365EB63272EBB3B71E374BF1C860594B0FC616F91B85B245638F8B27CBCF28B74ED6D4E3022CD32FCA1D58B6F6B94ACB048B7A9D96CD3DB0F21C8A0294B7F5431169041D45B47547D132203298D6B511A6AEA0D6B519A65B8FE9BEF8FA75FF4E7CBDFEBCD9A9C36F11D6C1EBB722BC8425FF71AA1AAF667D4829BD7C3D6BAFE449FE626D2F08359EB0D910DB02F3DC115F3C5FDD07E06E9851D9FD2D9805048B6FC58A0167889205663C4B3BD9F0C27EAB558D3E9D0A4E87313FD8B48C734E7D7C3FB5FFB0FE7CFCBC1A111AB566CE7AD6122AA5937728F16E5162D44E6E591859052B9273DB953D8AD3CD5739B5354E978C9E969ADB14464DD5DD882FDA9A00062B917C760CAC48E6B6136683D4ED33E54B63B1E2B3234B6D95206976D762F6A135FFF58B01B0675D2470831D5A633052CF3DADA93AECAA8B367D1B4DB2E76CFDE9D9EF7F7CB48AC52AD4B7E3716F58A540719883AE9723565E63FD51F5E2C381609552C3CABDEA6F54B5B07020025496110E4583CAAAC15D80BB4383FF3FAE95F63AB76777B7BC04227D18B365BD9916E66E55035695CFDEB8A268A34A9FB624D30E2B7B9E6FC598E182B6ACE1FAF63469FFE2EF5B13E529D57B999515355FE576AFE2CABED802DF7913C1CE672E6F47255E55A276540556256AB7556255129F522959E55EB5577BD53A894E95554FAB56AC6657BB1473EDCC0C3D4AC1CC2FAFC1BF487FF3001C1D23CB3584F80B08147B8A6729C7CCE9222A7C9DA65131440B92CE30471060A1A3849305F238747BF0185EFD8AC16714A42B8EDD607F4E2F521EA71C968CC39B40F9CD05E1289BE4AFEADD549D2717ABF0930DB10450938818F1827E4849E0977A9F9AB1701D84F0C079802DF6928B407BF950229D1B79EB3AA0DC7CE5C57185C338003076415D2482D1FEBA5D33FC112F91F750E420EA41DA374235FBE498A06582429663ACE7C347E0B01FDEBFFB178BDDC201FA430000 , N'6.2.0-61023')

