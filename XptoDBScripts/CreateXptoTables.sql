USE [XPTO]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](520) NOT NULL,
	[Active] [bit] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[LastUpdate] [datetime] NULL,
 CONSTRAINT [PK_DepartmentId] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](520) NOT NULL,
	[LastName] [nvarchar](520) NOT NULL,
	[MobilePhoneNumber] [nvarchar](520) NULL,
	[OfficePhoneNumber] [nvarchar](520) NULL,
	[DepartmentId] [int] NOT NULL,
	[HireDate] [datetime] NOT NULL,
	[Email] [nvarchar](520) NULL,
	[ExitDate] [datetime] NULL,
	[Deleted] [bit] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[LastUpdate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeId] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](520) NOT NULL,
	[Active] [bit] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[LastUpdate] [datetime] NULL,
 CONSTRAINT [PK_RoleId] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](520) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[LastUpdate] [datetime] NULL,
 CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[LastUpdate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Department] ON 

GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [Active], [ModifiedBy], [LastUpdate]) VALUES (1, N'Xpto - Lisbon', 1, 1, CAST(N'2019-04-07 16:07:55.950' AS DateTime))
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [Active], [ModifiedBy], [LastUpdate]) VALUES (2, N'Xpto - London', 1, 1, CAST(N'2019-04-07 16:07:55.950' AS DateTime))
GO
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName], [Active], [ModifiedBy], [LastUpdate]) VALUES (3, N'Xpto - Dublin', 1, 1, CAST(N'2019-04-07 16:07:55.950' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Active], [ModifiedBy], [LastUpdate]) VALUES (1, N'Administrator', 1, 1, CAST(N'2019-04-07 16:16:47.790' AS DateTime))
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Active], [ModifiedBy], [LastUpdate]) VALUES (2, N'ManageDepartments', 1, 1, CAST(N'2019-04-07 16:16:47.790' AS DateTime))
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName], [Active], [ModifiedBy], [LastUpdate]) VALUES (3, N'ManageEmployees', 1, 1, CAST(N'2019-04-07 16:16:47.790' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([UserId], [Email], [Password], [Deleted], [ModifiedBy], [LastUpdate]) VALUES (1, N'AdminUser@enear.co', N'e10adc3949ba59abbe56e057f20f883e', 0, -1, CAST(N'2019-04-08 17:40:59.197' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId], [Deleted], [ModifiedBy], [LastUpdate]) VALUES (1, 1, 0, -1, CAST(N'2019-04-08 17:47:53.727' AS DateTime))
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [fk_Emp_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [fk_Emp_Department]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [fk_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserRoles_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [fk_UserRoles_User]
GO
USE [master]
GO
ALTER DATABASE [XPTO] SET  READ_WRITE 
GO