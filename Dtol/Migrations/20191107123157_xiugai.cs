using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dtol.Migrations
{
    public partial class xiugai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_Depart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Depart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_Rights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RightsName = table.Column<string>(nullable: true),
                    RightsValue = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Rights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: true),
                    RightsId = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    RightsName = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    RoleType = table.Column<string>(nullable: true),
                    RoleCode = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Createdate = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    Level = table.Column<DateTime>(nullable: true),
                    Flow_NodeDefineId = table.Column<int>(nullable: true),
                    Flow_ProcedureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "V_GetToken",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    token = table.Column<string>(nullable: true),
                    addtime = table.Column<DateTime>(nullable: false),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_GetToken", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VA_Handle",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ContentID = table.Column<string>(nullable: true),
                    Participant = table.Column<string>(nullable: true),
                    VID = table.Column<string>(nullable: true),
                    VNO = table.Column<string>(nullable: true),
                    CheckFace = table.Column<string>(nullable: true),
                    CheckFaceScore = table.Column<string>(nullable: true),
                    CheckFaceResult = table.Column<string>(nullable: true),
                    CheckAddress = table.Column<string>(nullable: true),
                    Checklongitude = table.Column<string>(nullable: true),
                    Checklatitude = table.Column<string>(nullable: true),
                    CheckTime = table.Column<DateTime>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VA_Handle", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VA_Sign",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ContentID = table.Column<string>(nullable: true),
                    Participant = table.Column<string>(nullable: true),
                    VID = table.Column<string>(nullable: true),
                    VNO = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ramark = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VA_Sign", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VActivity_Relate_Type",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityID = table.Column<string>(nullable: true),
                    TypeID = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VActivity_Relate_Type", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VAttachment",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    InternalName = table.Column<string>(nullable: true),
                    formid = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Length = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAttachment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VBaseType",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentCode = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VBaseType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VHA_Handle",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ContentID = table.Column<string>(nullable: true),
                    Participant = table.Column<string>(nullable: true),
                    VID = table.Column<string>(nullable: true),
                    VNO = table.Column<string>(nullable: true),
                    Results = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VHA_Handle", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VHA_Sign",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ContentID = table.Column<string>(nullable: true),
                    Participant = table.Column<string>(nullable: true),
                    VID = table.Column<string>(nullable: true),
                    VNO = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    flag = table.Column<string>(maxLength: 50, nullable: false),
                    peroperid = table.Column<string>(maxLength: 50, nullable: false),
                    operid = table.Column<string>(maxLength: 50, nullable: false),
                    opinion = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VHA_Sign", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VHelpArea",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ProblemType = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CommunityID = table.Column<string>(nullable: true),
                    Community = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    TypeID = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    longitude = table.Column<string>(nullable: true),
                    latitude = table.Column<string>(nullable: true),
                    Score = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VHelpArea", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer_Info",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    VNO = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CertificateType = table.Column<string>(nullable: true),
                    CertificateID = table.Column<string>(nullable: true),
                    Birthday = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Political = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    CommunityID = table.Column<string>(nullable: true),
                    Community = table.Column<string>(nullable: true),
                    SubdistrictID = table.Column<string>(nullable: true),
                    Subdistrict = table.Column<string>(nullable: true),
                    UnitID = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Degree = table.Column<string>(nullable: true),
                    Marriage = table.Column<string>(nullable: true),
                    ServiceTime = table.Column<string>(nullable: true),
                    Career = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    openid = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    unionid = table.Column<string>(nullable: true),
                    Headimgurl = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DataSource = table.Column<string>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer_Info", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer_Relate_Type",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VolunteerID = table.Column<string>(nullable: true),
                    TypeID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer_Relate_Type", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerActivity",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    TypeIDs = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    CommunityID = table.Column<string>(nullable: true),
                    Community = table.Column<string>(nullable: true),
                    Page = table.Column<string>(nullable: true),
                    Stime = table.Column<DateTime>(nullable: true),
                    Etime = table.Column<DateTime>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    longitude = table.Column<string>(nullable: true),
                    latitude = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Score = table.Column<string>(nullable: true),
                    PerMinScore = table.Column<string>(nullable: true),
                    SignStime = table.Column<DateTime>(nullable: true),
                    SignEtime = table.Column<DateTime>(nullable: true),
                    SignUpStime = table.Column<DateTime>(nullable: true),
                    SignUpEtime = table.Column<DateTime>(nullable: true),
                    SignOutStime = table.Column<DateTime>(nullable: true),
                    SignOutEtime = table.Column<DateTime>(nullable: true),
                    CreateUser = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    UpdateUser = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    flag = table.Column<string>(nullable: true),
                    bak1 = table.Column<string>(nullable: true),
                    bak2 = table.Column<string>(nullable: true),
                    bak3 = table.Column<string>(nullable: true),
                    bak4 = table.Column<string>(nullable: true),
                    bak5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerActivity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "user_Info",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    UserPwd = table.Column<string>(nullable: true),
                    RoleNames = table.Column<string>(nullable: true),
                    RoleIds = table.Column<string>(nullable: true),
                    Dept = table.Column<string>(nullable: true),
                    DeptId = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    AddInfoDate = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Birthdate = table.Column<string>(nullable: true),
                    Nation = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Idcard = table.Column<string>(nullable: true),
                    NativePlace = table.Column<string>(nullable: true),
                    PoliticalBackground = table.Column<string>(nullable: true),
                    JoinPartyDate = table.Column<string>(nullable: true),
                    BloodType = table.Column<string>(nullable: true),
                    HomeAddress = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    PhoneCall = table.Column<string>(nullable: true),
                    MobileCall = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Interest = table.Column<string>(nullable: true),
                    InitialEducation = table.Column<string>(nullable: true),
                    InitialGraduated = table.Column<string>(nullable: true),
                    InitialSpecialty = table.Column<string>(nullable: true),
                    FinalEducation = table.Column<string>(nullable: true),
                    FinalGraduated = table.Column<string>(nullable: true),
                    FinalSpecialty = table.Column<string>(nullable: true),
                    Degree = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ForeignLanguageLevel = table.Column<string>(nullable: true),
                    ComputerLevel = table.Column<string>(nullable: true),
                    DriveLevel = table.Column<string>(nullable: true),
                    EntryDate = table.Column<string>(nullable: true),
                    EmployNature = table.Column<string>(nullable: true),
                    Post = table.Column<string>(nullable: true),
                    ContractSignDate = table.Column<string>(nullable: true),
                    ContractMaturityDate = table.Column<string>(nullable: true),
                    WorkExperience = table.Column<string>(nullable: true),
                    TrainSituation = table.Column<string>(nullable: true),
                    FamilyMembers = table.Column<string>(nullable: true),
                    JobPerformance = table.Column<string>(nullable: true),
                    AwardAndPunish = table.Column<string>(nullable: true),
                    AddDate = table.Column<DateTime>(nullable: true),
                    updateDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DeptLeaderId = table.Column<string>(nullable: true),
                    DeptLeaderName = table.Column<string>(nullable: true),
                    Levels = table.Column<string>(nullable: true),
                    Files = table.Column<string>(nullable: true),
                    ServiceExperience = table.Column<string>(nullable: true),
                    RoleNameNiwen = table.Column<string>(nullable: true),
                    RoleIdNiwen = table.Column<string>(nullable: true),
                    DzbId = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    User_DepartId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_Info_user_Depart_User_DepartId",
                        column: x => x.User_DepartId,
                        principalTable: "user_Depart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_Relate_Role_Right",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_RightsId = table.Column<int>(nullable: false),
                    User_RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Relate_Role_Right", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_Relate_Role_Right_user_Rights_User_RightsId",
                        column: x => x.User_RightsId,
                        principalTable: "user_Rights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_Relate_Role_Right_user_Role_User_RoleId",
                        column: x => x.User_RoleId,
                        principalTable: "user_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_Relate_Info_Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_InfoId = table.Column<int>(nullable: false),
                    User_RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Relate_Info_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_Relate_Info_Role_user_Info_User_InfoId",
                        column: x => x.User_InfoId,
                        principalTable: "user_Info",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_Relate_Info_Role_user_Role_User_RoleId",
                        column: x => x.User_RoleId,
                        principalTable: "user_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_Info_User_DepartId",
                table: "user_Info",
                column: "User_DepartId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Relate_Info_Role_User_InfoId",
                table: "user_Relate_Info_Role",
                column: "User_InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Relate_Info_Role_User_RoleId",
                table: "user_Relate_Info_Role",
                column: "User_RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Relate_Role_Right_User_RightsId",
                table: "user_Relate_Role_Right",
                column: "User_RightsId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Relate_Role_Right_User_RoleId",
                table: "user_Relate_Role_Right",
                column: "User_RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_Relate_Info_Role");

            migrationBuilder.DropTable(
                name: "user_Relate_Role_Right");

            migrationBuilder.DropTable(
                name: "V_GetToken");

            migrationBuilder.DropTable(
                name: "VA_Handle");

            migrationBuilder.DropTable(
                name: "VA_Sign");

            migrationBuilder.DropTable(
                name: "VActivity_Relate_Type");

            migrationBuilder.DropTable(
                name: "VAttachment");

            migrationBuilder.DropTable(
                name: "VBaseType");

            migrationBuilder.DropTable(
                name: "VHA_Handle");

            migrationBuilder.DropTable(
                name: "VHA_Sign");

            migrationBuilder.DropTable(
                name: "VHelpArea");

            migrationBuilder.DropTable(
                name: "Volunteer_Info");

            migrationBuilder.DropTable(
                name: "Volunteer_Relate_Type");

            migrationBuilder.DropTable(
                name: "VolunteerActivity");

            migrationBuilder.DropTable(
                name: "user_Info");

            migrationBuilder.DropTable(
                name: "user_Rights");

            migrationBuilder.DropTable(
                name: "user_Role");

            migrationBuilder.DropTable(
                name: "user_Depart");
        }
    }
}
