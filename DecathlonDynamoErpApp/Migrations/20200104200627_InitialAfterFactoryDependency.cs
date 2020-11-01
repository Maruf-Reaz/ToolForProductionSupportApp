using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class InitialAfterFactoryDependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalculationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    FromDate = table.Column<string>(nullable: true),
                    ToDate = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StatusColour = table.Column<string>(nullable: true),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    AllDay = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(nullable: false),
                    SentBy = table.Column<string>(nullable: true),
                    SentByUserName = table.Column<string>(nullable: true),
                    SentTo = table.Column<string>(nullable: true),
                    SentToUserName = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CssClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CssClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CalculationFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Factories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncentiveVariables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncentiveVariables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Physiology = table.Column<double>(nullable: true),
                    BasicTiredness = table.Column<double>(nullable: true),
                    NoiseyEnvironment = table.Column<double>(nullable: true),
                    ChangeBobin = table.Column<double>(nullable: true),
                    StandWorking = table.Column<double>(nullable: true),
                    Other = table.Column<double>(nullable: true),
                    Field1 = table.Column<double>(nullable: true),
                    Field2 = table.Column<double>(nullable: true),
                    Field3 = table.Column<double>(nullable: true),
                    Total = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventGuests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CalendarEventId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGuests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventGuests_CalendarEvents_CalendarEventId",
                        column: x => x.CalendarEventId,
                        principalTable: "CalendarEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FactoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ELearnings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FactoryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DateOfUpload = table.Column<DateTime>(nullable: false),
                    ELearningFileName1 = table.Column<string>(nullable: true),
                    ELearningFileName2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELearnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ELearnings_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManualJobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FactoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManualJobs_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FactoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processes_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignSports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FactoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignSports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignSports_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillMatrixRanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    FactoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillMatrixRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillMatrixRanges_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TPSProjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FactoryId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DateOfUpload = table.Column<DateTime>(nullable: false),
                    ProjectFileName1 = table.Column<string>(nullable: true),
                    ProjectFileName2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPSProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TPSProjects_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationGroups_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    AdderId = table.Column<string>(nullable: true),
                    LinkAction = table.Column<string>(nullable: true),
                    LinkController = table.Column<string>(nullable: true),
                    PostingTime = table.Column<DateTime>(nullable: false),
                    NotificationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FactoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ShortName = table.Column<string>(nullable: false),
                    ProcessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sections_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SotModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FactoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Dipl = table.Column<string>(nullable: true),
                    ProcessId = table.Column<int>(nullable: false),
                    SignSportId = table.Column<int>(nullable: false),
                    Photo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SotModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SotModels_Factories_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "Factories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SotModels_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SotModels_SignSports_SignSportId",
                        column: x => x.SignSportId,
                        principalTable: "SignSports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NotificationId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotifications_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectionId = table.Column<int>(nullable: false),
                    LineNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lines_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlySectionEarningPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectionId = table.Column<int>(nullable: false),
                    TargetPoint = table.Column<double>(nullable: false),
                    MoneyPerPoint = table.Column<double>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlySectionEarningPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlySectionEarningPoints_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyVariableValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectionId = table.Column<int>(nullable: false),
                    IncentiveVariableId = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyVariableValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyVariableValues_IncentiveVariables_IncentiveVariableId",
                        column: x => x.IncentiveVariableId,
                        principalTable: "IncentiveVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonthlyVariableValues_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectionId = table.Column<int>(nullable: false),
                    MachineId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandardOperationTimes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SotModelId = table.Column<int>(nullable: false),
                    CalculationStatusId = table.Column<int>(nullable: true),
                    ValidationStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardOperationTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardOperationTimes_CalculationStatuses_CalculationStatusId",
                        column: x => x.CalculationStatusId,
                        principalTable: "CalculationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StandardOperationTimes_SotModels_SotModelId",
                        column: x => x.SotModelId,
                        principalTable: "SotModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandardOperationTimes_ValidationStatuses_ValidationStatusId",
                        column: x => x.ValidationStatusId,
                        principalTable: "ValidationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineIncentives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineId = table.Column<int>(nullable: false),
                    LineIncentiveDateTime = table.Column<DateTime>(nullable: false),
                    Total = table.Column<double>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineIncentives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineIncentives_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineId = table.Column<int>(nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IdCardNumber = table.Column<string>(nullable: false),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operators_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operators_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticularLineBalancings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineId = table.Column<int>(nullable: true),
                    StandardOperationTimeId = table.Column<int>(nullable: true),
                    ActualLineBalancingRatio = table.Column<double>(nullable: false),
                    Who = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticularLineBalancings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticularLineBalancings_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParticularLineBalancings_StandardOperationTimes_StandardOperationTimeId",
                        column: x => x.StandardOperationTimeId,
                        principalTable: "StandardOperationTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectionStandardOperationTimes",
                columns: table => new
                {
                    StandardOperationTimeId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    SotValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionStandardOperationTimes", x => new { x.StandardOperationTimeId, x.SectionId });
                    table.ForeignKey(
                        name: "FK_SectionStandardOperationTimes_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionStandardOperationTimes_StandardOperationTimes_StandardOperationTimeId",
                        column: x => x.StandardOperationTimeId,
                        principalTable: "StandardOperationTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StandardOperationTimeItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<int>(nullable: true),
                    StandardOperationTimeId = table.Column<int>(nullable: false),
                    DataSourceId = table.Column<int>(nullable: false),
                    OperationId = table.Column<int>(nullable: false),
                    OperationCode = table.Column<string>(nullable: true),
                    SectionId = table.Column<int>(nullable: false),
                    VideoLink = table.Column<string>(nullable: true),
                    CycleTime = table.Column<double>(nullable: true),
                    Size = table.Column<double>(nullable: true),
                    Rating = table.Column<double>(nullable: true),
                    Cycle = table.Column<double>(nullable: true),
                    SotValue = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardOperationTimeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardOperationTimeItems_DataSources_DataSourceId",
                        column: x => x.DataSourceId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandardOperationTimeItems_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandardOperationTimeItems_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StandardOperationTimeItems_StandardOperationTimes_StandardOperationTimeId",
                        column: x => x.StandardOperationTimeId,
                        principalTable: "StandardOperationTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AchievedValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineIncentiveId = table.Column<int>(nullable: false),
                    IncentiveVariableId = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievedValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchievedValues_LineIncentives_LineIncentiveId",
                        column: x => x.LineIncentiveId,
                        principalTable: "LineIncentives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineIncentiveId = table.Column<int>(nullable: false),
                    IncentiveVariableId = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PointValues_LineIncentives_LineIncentiveId",
                        column: x => x.LineIncentiveId,
                        principalTable: "LineIncentives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineIncentiveId = table.Column<int>(nullable: false),
                    IncentiveVariableId = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TargetValues_LineIncentives_LineIncentiveId",
                        column: x => x.LineIncentiveId,
                        principalTable: "LineIncentives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillMatrixs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperationId = table.Column<int>(nullable: false),
                    OperatorId = table.Column<int>(nullable: false),
                    StandardSotInSecond = table.Column<double>(nullable: false),
                    StandardRft = table.Column<double>(nullable: false),
                    SotScore = table.Column<double>(nullable: false),
                    RftScore = table.Column<double>(nullable: false),
                    TargetMonth = table.Column<double>(nullable: false),
                    TargetGrade = table.Column<string>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillMatrixs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillMatrixs_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillMatrixs_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LineBalancingStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParticularLineBalancingId = table.Column<int>(nullable: false),
                    OperationName = table.Column<string>(nullable: true),
                    OperationId = table.Column<int>(nullable: false),
                    MachineId = table.Column<int>(nullable: false),
                    OperatorId1 = table.Column<int>(nullable: true),
                    OperatorId2 = table.Column<int>(nullable: true),
                    OperatorId3 = table.Column<int>(nullable: true),
                    OperatorId4 = table.Column<int>(nullable: true),
                    OperatorNo1 = table.Column<int>(nullable: true),
                    OperatorNo2 = table.Column<int>(nullable: true),
                    OperatorNo3 = table.Column<int>(nullable: true),
                    OperatorNo4 = table.Column<int>(nullable: true),
                    CycleTime1 = table.Column<double>(nullable: true),
                    CycleTime2 = table.Column<double>(nullable: true),
                    CycleTime3 = table.Column<double>(nullable: true),
                    CycleTime4 = table.Column<double>(nullable: true),
                    AllocatedTime1 = table.Column<double>(nullable: true),
                    AllocatedTime2 = table.Column<double>(nullable: true),
                    AllocatedTime3 = table.Column<double>(nullable: true),
                    AllocatedTime4 = table.Column<double>(nullable: true),
                    Output1 = table.Column<double>(nullable: true),
                    Output2 = table.Column<double>(nullable: true),
                    Output3 = table.Column<double>(nullable: true),
                    Output4 = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineBalancingStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineBalancingStates_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineBalancingStates_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LineBalancingStates_ParticularLineBalancings_ParticularLineBalancingId",
                        column: x => x.ParticularLineBalancingId,
                        principalTable: "ParticularLineBalancings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManualJobStandardOperationTimeItems",
                columns: table => new
                {
                    StandardOperationTimeItemId = table.Column<int>(nullable: false),
                    ManualJobId = table.Column<int>(nullable: false),
                    Time = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManualJobStandardOperationTimeItems", x => new { x.StandardOperationTimeItemId, x.ManualJobId });
                    table.ForeignKey(
                        name: "FK_ManualJobStandardOperationTimeItems_ManualJobs_ManualJobId",
                        column: x => x.ManualJobId,
                        principalTable: "ManualJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManualJobStandardOperationTimeItems_StandardOperationTimeItems_StandardOperationTimeItemId",
                        column: x => x.StandardOperationTimeItemId,
                        principalTable: "StandardOperationTimeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StandardOperationTimeSubItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StandardOperationTimeItemId = table.Column<int>(nullable: false),
                    IsNeglected = table.Column<bool>(nullable: false),
                    CycleStartTime = table.Column<double>(nullable: true),
                    CycleEndTime = table.Column<double>(nullable: true),
                    Wastage1StartTime = table.Column<double>(nullable: true),
                    Wastage1EndTime = table.Column<double>(nullable: true),
                    Wastage2StartTime = table.Column<double>(nullable: true),
                    Wastage2EndTime = table.Column<double>(nullable: true),
                    Wastage3StartTime = table.Column<double>(nullable: true),
                    Wastage3EndTime = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardOperationTimeSubItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardOperationTimeSubItems_StandardOperationTimeItems_StandardOperationTimeItemId",
                        column: x => x.StandardOperationTimeItemId,
                        principalTable: "StandardOperationTimeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedOn", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f686b56a-6135-4221-a0b0-bdec547e3waw", "da9a3b0e-8b6f-8529-71d0-4fd255e23f15", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has All Permissions", "SuperAdmin", "SUPERADMIN" },
                    { "v212b59c-3113-4888-m2n2-bdec136e4ibi", "ql5a3b1g-5c2g-6512-81e1-4fd511e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Minimum Permissions", "EDISONViewer", "EDISONVIEWER" },
                    { "x212b59c-9191-4777-k2l2-bdec135e4gbg", "lq5a3b1g-6c2g-3254-93e1-4fd255e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Minimum Permissions", "APEXViewer", "APEXVIEWER" },
                    { "z323b59c-7878-4666-i2j2-bdec134e4gbg", "al5a3b0g-7c3p-4517-45e1-4fd377e23i20", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Minimum Permissions", "RFLViewer", "RFLVIEWER" },
                    { "g734b69c-5656-4555-g2h2-bdec133e4ibi", "ri5a3b0g-8d4j-2184-95e1-4fd352e23i19", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Minimum Permissions", "YSSViewer", "YSSVIEWER" },
                    { "a212b59c-3434-4333-e2f2-bdec132e4lbl", "eu5a3b0g-9c5g-9126-45e1-4fd366e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Minimum Permissions", "KSIViewer", "KSIVIEWER" },
                    { "e858c58l-1212-4222-c2d2-bdec131e3pbp", "ah8b4b9j-3n6o-3491-73e1-4fd800e23g17", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Minimum Permissions", "MAFViewer", "MAFVIEWER" },
                    { "v212b59c-6348-6121-m1n1-bdec785e4iki", "ql5a3b1g-5c2g-6512-81e1-4fd511e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "EDISONUser", "EDISONUSER" },
                    { "z323b59c-5209-5344-i1j1-bdec735e4gpg", "al5a3b0g-7c3p-4517-45e1-4fd377e23i20", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "RFLUser", "RFLUSER" },
                    { "x212b59c-3269-1666-k1l1-bdec785e4gig", "lq5a3b1g-6c2g-3254-93e1-4fd255e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "APEXUser", "APEXUSER" },
                    { "a212b59c-5209-3999-e1f1-bdec785e4lil", "eu5a3b0g-9c5g-9126-45e1-4fd366e23i18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "KSIUser", "KSIUSER" },
                    { "e858c58l-3251-9888-c1d1-bdec765e3pip", "ah8b4b9j-3n6o-3491-73e1-4fd800e23g17", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "MAFUser", "MAFUSER" },
                    { "m354b57g-4013-9227-m0n0-bdec765e3oeo", "la9a3b0g-4b9f-4102-41e0-4fd366e23f21", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Maximum Permissions", "EDISONAdmin", "EDISONADMIN" },
                    { "m173b57f-8519-9226-k0l0-bdec765e3rir", "la9a3b0g-5b1f-7852-31e0-4fd366e23f20", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Maximum Permissions", "APEXAdmin", "APEXADMIN" },
                    { "m915b57e-2431-9225-i0j0-bdec765e3pgp", "la9a3b0g-6b2f-9864-21e0-4fd366e23f19", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Maximum Permissions", "RFLAdmin", "RFLADMIN" },
                    { "m347b57d-8617-9224-g0h0-bdec765e3lml", "la9a3b0g-7b3f-8412-11e0-4fd366e23f18", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Maximum Permissions", "YSSAdmin", "YSSADMIN" },
                    { "m121b57c-9025-9223-e0f0-bdec765e3bgb", "la9a3b0g-8b4f-6218-91e0-4fd366e23f17", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Maximum Permissions", "KSIAdmin", "KSIADMIN" },
                    { "w585b57b-7456-8222-c0d0-bdec765e3awa", "ea9a3b0f-9b5f-7153-81e0-4fd799e23f16", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Maximum Permissions", "MAFAdmin", "MAFADMIN" },
                    { "g734b69c-9461-6111-g1h1-bdec785e4imi", "ri5a3b0g-8d4j-2184-95e1-4fd352e23i19", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Has Several Permissions", "YSSUser", "YSSUSER" }
                });

            migrationBuilder.InsertData(
                table: "CalculationStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Not Done" },
                    { 3, "In Progress" },
                    { 2, "Done" }
                });

            migrationBuilder.InsertData(
                table: "CssClasses",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 5, false, "Dark Mode" },
                    { 1, true, "ToogleSideMenuClass" },
                    { 3, false, "Dark Navigation" },
                    { 2, false, "Sidebar Radius" },
                    { 4, false, "Light Background" }
                });

            migrationBuilder.InsertData(
                table: "DataSources",
                columns: new[] { "Id", "CalculationFileName", "Name" },
                values: new object[,]
                {
                    { 1, "STOPWATCH/VIDEO", "Supplier Vdo" },
                    { 2, "PACE", "Sam Pace" }
                });

            migrationBuilder.InsertData(
                table: "Factories",
                columns: new[] { "Id", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Chittagong", "KSI", "A-001" },
                    { 2, "Chittagong", "YSS", "A-002" },
                    { 3, "Chittagong", "MAF", "A-003" },
                    { 4, "Chittagong", "RFL", "A-004" },
                    { 5, "Chittagong", "APEX", "A-005" },
                    { 6, "Chittagong", "EDISON", "A-006" }
                });

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Chat" },
                    { 2, "Event" }
                });

            migrationBuilder.InsertData(
                table: "ValidationStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2, "Not Validated" },
                    { 1, "Validated" },
                    { 3, "In Progress" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FactoryId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", 0, "26d21881-0a3a-44ab-aa4d-10664ace1e2d", "monir.hossain@decathlon.com", false, 1, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "5TDMS5CNA2GYJK2URB4GDOCQI2NI7EHJ", false, "SuperAdmin" },
                    { "8231b097-f123-45a8-b9b5-e7c4ddad8b7c", 0, "3ioo142a-2145-4e3c-9fmb-b6d7234d4770", "monir.hossain@decathlon.com", false, 4, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINRFL", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "LKTIC77KPNK6Q54FLRUM6OXJAV14LW4W", false, "SuperAdminRFL" },
                    { "8lh7oo83-f918-45a8-b9b5-e7c4ddad8b7c", 0, "3lqq352a-7534-4e3c-9fbc-b6d7234d4660", "monir.hossain@decathlon.com", false, 3, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINMAF", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "FRPOV99KPNK6QHM5IUUM6OXJAV32LW4Q", false, "SuperAdminMAF" },
                    { "133a8219-f819-45a8-b9b5-e7c4ddad8b7c", 0, "3bee968a-9516-4e3c-9fad-b6d7234d4550", "monir.hossain@decathlon.com", false, 2, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINYSS", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "WSZPQ45KPNK6QHM5RTUM6OXJAV46LW4P", false, "SuperAdminYSS" },
                    { "8fg9ww95-f792-45a8-b9b5-e7c4ddad8b7c", 0, "3fjj357a-8632-4e3c-9flk-b6d7234d4880", "monir.hossain@decathlon.com", false, 5, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINAPEX", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "VJRSO58KPNK5RCP5JRUM6OXJAV49LW4E", false, "SuperAdminAPEX" },
                    { "7846b041-f643-45a8-b9b5-e7c4ddad8b7c", 0, "3zaz461a-2136-4e3c-9fpo-b6d7234d4990", "monir.hossain@decathlon.com", false, 6, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINEDISON", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "JWERP12KPNK6QHM5JRUM6OXJAV85LW4R", false, "SuperAdminEDISON" },
                    { "1891b070-42ce-40ad-9c4e-5a47f81dc0ad", 0, "504dcd7c-cfdc-49ec-a455-7397cbfcccdc", "monir.hossain@decathlon.com", false, 1, false, null, "MONIR.HOSSAIN@DECATHLON.COM", "SUPERADMINKSI", "AQAAAAEAACcQAAAAEEO6XuvdZ4p/fzatnYaxaH4psRTnOEp3N+Ez6FFxAh1S76HPtt/YScM8RIguweFeoQ==", null, false, "MHNEMETEQV4TNCQ3YRP7WC5X74GNV7HI", false, "SuperAdminKSI" }
                });

            migrationBuilder.InsertData(
                table: "SkillMatrixRanges",
                columns: new[] { "Id", "Code", "FactoryId", "Level" },
                values: new object[,]
                {
                    { 5, 5, 1, "" },
                    { 1, 1, 1, "" },
                    { 2, 2, 1, "" },
                    { 30, 5, 1, "" },
                    { 29, 4, 1, "" },
                    { 28, 3, 1, "" },
                    { 27, 2, 1, "" },
                    { 26, 1, 1, "" },
                    { 25, 5, 1, "" },
                    { 24, 4, 1, "" },
                    { 23, 3, 1, "" },
                    { 22, 2, 1, "" },
                    { 21, 1, 1, "" },
                    { 4, 4, 1, "" },
                    { 20, 5, 1, "" },
                    { 18, 3, 1, "" },
                    { 3, 3, 1, "" },
                    { 16, 1, 1, "" },
                    { 15, 5, 1, "" },
                    { 14, 4, 1, "" },
                    { 13, 3, 1, "" },
                    { 12, 2, 1, "" },
                    { 11, 1, 1, "" },
                    { 10, 5, 1, "" },
                    { 9, 4, 1, "" },
                    { 8, 3, 1, "" },
                    { 7, 2, 1, "" },
                    { 6, 1, 1, "" },
                    { 19, 4, 1, "" },
                    { 17, 2, 1, "" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "8ab6ee62-f36c-48b6-ae27-dbb22cbfb507", "f686b56a-6135-4221-a0b0-bdec547e3waw" },
                    { "1891b070-42ce-40ad-9c4e-5a47f81dc0ad", "m121b57c-9025-9223-e0f0-bdec765e3bgb" },
                    { "133a8219-f819-45a8-b9b5-e7c4ddad8b7c", "m347b57d-8617-9224-g0h0-bdec765e3lml" },
                    { "8lh7oo83-f918-45a8-b9b5-e7c4ddad8b7c", "w585b57b-7456-8222-c0d0-bdec765e3awa" },
                    { "8231b097-f123-45a8-b9b5-e7c4ddad8b7c", "m915b57e-2431-9225-i0j0-bdec765e3pgp" },
                    { "8fg9ww95-f792-45a8-b9b5-e7c4ddad8b7c", "m173b57f-8519-9226-k0l0-bdec765e3rir" },
                    { "7846b041-f643-45a8-b9b5-e7c4ddad8b7c", "m354b57g-4013-9227-m0n0-bdec765e3oeo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievedValues_LineIncentiveId",
                table: "AchievedValues",
                column: "LineIncentiveId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FactoryId",
                table: "AspNetUsers",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ELearnings_FactoryId",
                table: "ELearnings",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGuests_CalendarEventId",
                table: "EventGuests",
                column: "CalendarEventId");

            migrationBuilder.CreateIndex(
                name: "IX_LineBalancingStates_MachineId",
                table: "LineBalancingStates",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_LineBalancingStates_OperationId",
                table: "LineBalancingStates",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_LineBalancingStates_ParticularLineBalancingId",
                table: "LineBalancingStates",
                column: "ParticularLineBalancingId");

            migrationBuilder.CreateIndex(
                name: "IX_LineIncentives_LineId",
                table: "LineIncentives",
                column: "LineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lines_SectionId",
                table: "Lines",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualJobs_FactoryId",
                table: "ManualJobs",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ManualJobStandardOperationTimeItems_ManualJobId",
                table: "ManualJobStandardOperationTimeItems",
                column: "ManualJobId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlySectionEarningPoints_SectionId",
                table: "MonthlySectionEarningPoints",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyVariableValues_IncentiveVariableId",
                table: "MonthlyVariableValues",
                column: "IncentiveVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyVariableValues_SectionId",
                table: "MonthlyVariableValues",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationGroups_GroupId",
                table: "NotificationGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_MachineId",
                table: "Operations",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_SectionId",
                table: "Operations",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Operators_LineId",
                table: "Operators",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Operators_SectionId",
                table: "Operators",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticularLineBalancings_LineId",
                table: "ParticularLineBalancings",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticularLineBalancings_StandardOperationTimeId",
                table: "ParticularLineBalancings",
                column: "StandardOperationTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_PointValues_LineIncentiveId",
                table: "PointValues",
                column: "LineIncentiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_FactoryId",
                table: "Processes",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_FactoryId",
                table: "Sections",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ProcessId",
                table: "Sections",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionStandardOperationTimes_SectionId",
                table: "SectionStandardOperationTimes",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SignSports_FactoryId",
                table: "SignSports",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillMatrixRanges_FactoryId",
                table: "SkillMatrixRanges",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillMatrixs_OperationId",
                table: "SkillMatrixs",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillMatrixs_OperatorId",
                table: "SkillMatrixs",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SotModels_FactoryId",
                table: "SotModels",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SotModels_ProcessId",
                table: "SotModels",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_SotModels_SignSportId",
                table: "SotModels",
                column: "SignSportId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimeItems_DataSourceId",
                table: "StandardOperationTimeItems",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimeItems_OperationId",
                table: "StandardOperationTimeItems",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimeItems_SectionId",
                table: "StandardOperationTimeItems",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimeItems_StandardOperationTimeId",
                table: "StandardOperationTimeItems",
                column: "StandardOperationTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimes_CalculationStatusId",
                table: "StandardOperationTimes",
                column: "CalculationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimes_SotModelId",
                table: "StandardOperationTimes",
                column: "SotModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimes_ValidationStatusId",
                table: "StandardOperationTimes",
                column: "ValidationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardOperationTimeSubItems_StandardOperationTimeItemId",
                table: "StandardOperationTimeSubItems",
                column: "StandardOperationTimeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetValues_LineIncentiveId",
                table: "TargetValues",
                column: "LineIncentiveId");

            migrationBuilder.CreateIndex(
                name: "IX_TPSProjects_FactoryId",
                table: "TPSProjects",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_NotificationId",
                table: "UserNotifications",
                column: "NotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievedValues");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "CssClasses");

            migrationBuilder.DropTable(
                name: "ELearnings");

            migrationBuilder.DropTable(
                name: "EventGuests");

            migrationBuilder.DropTable(
                name: "LineBalancingStates");

            migrationBuilder.DropTable(
                name: "ManualJobStandardOperationTimeItems");

            migrationBuilder.DropTable(
                name: "MonthlySectionEarningPoints");

            migrationBuilder.DropTable(
                name: "MonthlyVariableValues");

            migrationBuilder.DropTable(
                name: "NotificationGroups");

            migrationBuilder.DropTable(
                name: "PointValues");

            migrationBuilder.DropTable(
                name: "SectionStandardOperationTimes");

            migrationBuilder.DropTable(
                name: "SkillMatrixRanges");

            migrationBuilder.DropTable(
                name: "SkillMatrixs");

            migrationBuilder.DropTable(
                name: "StandardOperationTimeSubItems");

            migrationBuilder.DropTable(
                name: "TargetValues");

            migrationBuilder.DropTable(
                name: "TPSProjects");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "ParticularLineBalancings");

            migrationBuilder.DropTable(
                name: "ManualJobs");

            migrationBuilder.DropTable(
                name: "IncentiveVariables");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "StandardOperationTimeItems");

            migrationBuilder.DropTable(
                name: "LineIncentives");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "DataSources");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "StandardOperationTimes");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "CalculationStatuses");

            migrationBuilder.DropTable(
                name: "SotModels");

            migrationBuilder.DropTable(
                name: "ValidationStatuses");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "SignSports");

            migrationBuilder.DropTable(
                name: "Processes");

            migrationBuilder.DropTable(
                name: "Factories");
        }
    }
}
