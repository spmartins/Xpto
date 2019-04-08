USE [XPTO]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 4/7/2019 11:59:20 PM ******/
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
/****** Object:  Table [dbo].[Employee]    Script Date: 4/7/2019 11:59:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](520) NOT NULL,
	[LastName] [nvarchar](520) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
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
/****** Object:  Table [dbo].[EmployeeRoles]    Script Date: 4/7/2019 11:59:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeRoles](
	[EmployeeId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[LastUpdate] [datetime] NULL,
 CONSTRAINT [PK_EmployeeId_Roles] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/7/2019 11:59:20 PM ******/
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
SET IDENTITY_INSERT [dbo].[Employee] ON 

GO
INSERT [dbo].[Employee] ([EmployeeId], [FirstName], [LastName], [Password], [MobilePhoneNumber], [OfficePhoneNumber], [DepartmentId], [HireDate], [Email], [ExitDate], [Deleted], [ModifiedBy], [LastUpdate]) VALUES (9, N'AdminName', N'AdminLastName', N'23fd44228071730e3457dc5de887b3ae', N'123456789', N'123456789', 1, CAST(N'2019-04-07 23:32:17.613' AS DateTime), N'TestEmail1@enear.co', NULL, 0, 1, CAST(N'2019-04-07 23:32:17.613' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
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
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [fk_Emp_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [fk_Emp_Department]
GO
ALTER TABLE [dbo].[EmployeeRoles]  WITH CHECK ADD  CONSTRAINT [fk_EmpRoles_Emp] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeeRoles] CHECK CONSTRAINT [fk_EmpRoles_Emp]
GO
ALTER TABLE [dbo].[EmployeeRoles]  WITH CHECK ADD  CONSTRAINT [fk_EmpRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[EmployeeRoles] CHECK CONSTRAINT [fk_EmpRoles_Roles]
GO
