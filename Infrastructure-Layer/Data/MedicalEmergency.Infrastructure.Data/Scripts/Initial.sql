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
    [Latitude] [varchar](25),
    [Longitude] [varchar](25),
    [LinkPT] [varchar](150),
    [LinkEN] [varchar](150),
    [LinkES] [varchar](150),
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
VALUES (N'201808270602556_Initial', N'MedicalEmergency.Infrastructure.Data.Migrations.Configuration',  0x1F8B0800000000000400ED5CDB6EDC36107D2FD07F10F45838AB758200A9B19BC059DBEDA2BE21B283BE05B4C45D13912855A45C1B45BFAC0FFDA4FE4287ABCBF2A2EBAED6715D234060F372861C1ECE0C3593FCF3D7DF930FF76160DDE18491884EEDFDD1D8B630F5229FD0E5D44EF9E2D53BFBC3FBEFBF9B1CFBE1BDF5B918F7468C8399944DED5BCEE303C761DE2D0E111B85C44B22162DF8C88B4207F991F37A3CFED1D9DF773040D8806559934F29E524C4AB5FE0D759443D1CF3140567918F0396B7438FBB42B5CE5188598C3C3CB5CFB04F3C141C873859C24A1F4673BA4810E349EAF134C1A323C4D1E8522C9471E8C7B675181004EB7471B0B02D4469C411875D1C5C33ECF224A24B378606145C3DC418C62D50C070BEBB83F5F0AE1B1DBF161B75D6130B282F653C0A7B02EEBFC935E7E8D337D2BF5D6A16747B0C67C01FC4AE57FA9DDA879E17C1C1D8962EEB601624625C85F68F601CA1A31516C16C7486285AE2649463ED593533F64A3201E7C49F3D6B9606E204A714A73C41C19E7599DE04C4FB053F5C455F319DD23408E40DC016A04F6980A6CB248A71C21F3EE145BEADF9916D39EA3C479F584E93E6643B9E53FEE6B56D9D83707413E0921F92765C1E25F8274C718238F62F11E73881E39DFB78A56143BA26EB345A125A880346C2D5B3AD33747F8AE992DF4EEDD76F6DEB84DC63BF68C857704D095C549803DCC7152B6C967A8918FB3D4AFC06C16FC7BB103C4BB050532117EE2BBE025BD036ED3AF6379976E87172878B591FA328C088AA9326CEFA2634DE8F92C3A26DAB5BB232352305EFE5A6B4DF9423CCBC84C499F9DB96B6CF95A53F6314F05BD8EC76867C0DF3C2CC7666CE298361A98012220DD1CDB31543D073AEF8BBE132EC0F721B0E7D3FC18C35C8811F079073791BD1A6DD0CB29953248ECA6F92D3D1E5B6F975BA7C1C41847EBDBCDA39098498E3F3C711E3EE5C8C1B630F827E61EB1A75370CB125698D2A1C5E5A93268791F6E45C254C3A477764B9B2EC75A6D6B63EE1603582DD92387B1E4A4EEF8B34F02489C24F51A0F8D675FF17374A13F1CEBC8A1A065D21F881775FA7E450DA56AA0CAD5CAB34A269B5F2B0AAF5760E41346F3840A8AC21BE84242FC1F226C1F22163115846A113235A966EBC2AFE98FA5697EB9FA958BD9CA06BA01F898170B09CA9FD83B1B916F4E21E4AE8DA335415301E8DF60D19405A9C08D6A060065604AE01A1DC6438A11E8951D06139DADC8EF7439C4B2945EF39C231A682DA1D74DD45BC11539B6B29456AB7B84D5D1347E251677AC966BA03052A6DF68014AB32F612BE61C21F8166154B7A5CA255E8BCD302CCC7DF23902DB3703087C30C9CE46BD1FDA2E8C7F7559F02AE19CEBD30CBADBCCE1A81EF62AE7F1D5E9BD69C2B658FC13B1541335C068ED6DF8226DF00034AEE6CC131986E8019233444E980AA96278792D2D0A69053674F4717546E4DD58E41C68E3E47826B3E1A47D5403FED28E16BA37E6A2D681F1BBA958EAA8CA604D846947A3D15014B799DD7293027CB8115B932A72659363943710CD19D943CCB5B2C37CB9CCD5EB9FD93466186E178AC227754AEB69404212B5A62AD1744C34A4F48C2B8C8CEDD201160CEFCD0185667BC6A2E702155B54FE64116D7B9182F7EAEB6966DC9C4518D90B5B24F60FFA1702EABE85DE246EDCC5596130528A9782BCCA2200D69BD9BAB9F9D27956480BCA93BC63A4524C3AC5BBB23950F0419A86CEC8E53BE18649CB2B13B4EF18490618A361365E268876B38778363DABDD729DB89D0AACDDD29AD1B45752077CBFCDD505C79E0CA304AC70B4955120E4C52C987EE94A1F5723AD0B369F26EB859F12450C0DA5F0CF5D8C6CB56466E7DF6D6E3669924192C6BE9C1D8224DA450B668ECE178B23490E275B2A61E0EB0CCF2283EB06CEDE34ACB3C8EEA4ECBE61E5879AA4601CADBFAA18834828E22DA7AA2B815286E1F142D8F2283695D1B61EADBD4BA36C374EB317BEDFDC5636CE031F447D24EDD468BB00EBEA315E125B8F98F53D5787BEB434AE9E51B5C7B6B4FF2776F7BF5AAF110CE86D816A8E78EF8E211EC3E0077C38CCAEE6FC12C20587C5B2B069C214A1698F12C7965C33BFD9D56E2FA74CA4D1DC6FC60D39AD339F5F1FDD4FEC3FAF3F1B37344ACA835FFD6B3F051A9F3BC4389778B12A31864CB2ACE2A5891E2DBAE4653DC6EBECACCAD71BAE405B504DFA6306AC2EF467CAE6B0218AC9EF3D931B02225DC4E980D12C0CF942F8D9595CF8E2CB5258DA4D95C8BD907D6FCD72F06C09E759180073BB0C6A0A49E675A5322D9752DDAF46D56923D8AEB6FCF7EFFEBA3955756A1BE1B8F7BC32AD594C35C74BD76B2DD8D6D54293910AC521739D059A955904382BA03835656380EC5ADCA82C65D80376A6513F0FF87AF6A2FC17B760EEB25BAE9C3982D4BE1B4D879ABF2B4AA54FBC6C54E1B1521B5E5BF765874F47C8BD90C13B46579D9B7A749FBD7C46F4D94A7548A66167DD47C1FEE5E60967D2D03DB7913C1C967266F47D56755A27654A056256AB7056C55129F52955BE559B517A2D51A894E455F4FAB8CADE654BBD499ED4C0D3DAAD4CC2FE2605FA4FFF5010C1D23CB3584F83F2028F614CB528E99D34554D83A6D45C5102D483AC31C4180850E134E16C8E3D0EDC10B7BF5AF1F3EA3205D71EC06FB737A91F238E5B0651CDE04CA3FAA1086B249FEAA144F5DF3E462157EB221B600CB242246BCA01F5312F8E5BA4FCC58B80E4258E03CC01667C945A0BD7C2891CE8D947A1D50AEBED2715CE1300E008C5D50178960B4FFDAAE193EC54BE43D14898D7A90F68350D53E39226899A090E518EBF9F02B70D80FEFDFFF0B9138EE9EFC440000 , N'6.2.0-61023')

