﻿// <auto-generated />
using System;
using Dtol;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dtol.Migrations
{
    [DbContext(typeof(DtolContext))]
    [Migration("20200510035539_addlogintable")]
    partial class addlogintable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dtol.dtol.LoginType", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Memo");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("days");

                    b.Property<string>("hours");

                    b.Property<string>("name");

                    b.Property<string>("status");

                    b.Property<string>("type");

                    b.HasKey("ID");

                    b.ToTable("LoginType");
                });

            modelBuilder.Entity("Dtol.dtol.LoginType_Log", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateDate");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("status");

                    b.Property<string>("typeid");

                    b.Property<string>("uid");

                    b.HasKey("ID");

                    b.ToTable("LoginType_Log");
                });

            modelBuilder.Entity("Dtol.dtol.MydutyClaim_Info", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateDate");

                    b.Property<DateTime?>("EndDutyTime");

                    b.Property<string>("OndutyClaims_InfoId");

                    b.Property<DateTime?>("StartDutyTime");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("UserName");

                    b.Property<string>("Userid");

                    b.Property<string>("status");

                    b.HasKey("id");

                    b.HasIndex("OndutyClaims_InfoId");

                    b.ToTable("MydutyClaim_Info");
                });

            modelBuilder.Entity("Dtol.dtol.MydutyClaim_Sign", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CheckTime");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("MydutyClaim_InfoID");

                    b.Property<string>("OndutyClaims_InfoId");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("UserName");

                    b.Property<string>("Userid");

                    b.Property<string>("type");

                    b.HasKey("id");

                    b.HasIndex("OndutyClaims_InfoId");

                    b.ToTable("MydutyClaim_Sign");
                });

            modelBuilder.Entity("Dtol.dtol.Normalization_Info", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CheckInTime");

                    b.Property<string>("CheckOutTime");

                    b.Property<string>("CommunityName");

                    b.Property<string>("CommunityNameCode");

                    b.Property<DateTime?>("CreateaDate");

                    b.Property<DateTime?>("DutyEndTime");

                    b.Property<DateTime?>("DutyStartTime");

                    b.Property<string>("PointsEarned");

                    b.Property<string>("ServiceContent");

                    b.Property<string>("XiaoCommunityName");

                    b.Property<string>("XiaoCommunityNameeCode");

                    b.Property<string>("status");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.ToTable("Normalization_Info");
                });

            modelBuilder.Entity("Dtol.dtol.OndutyClaims_Info", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ClaimTime");

                    b.Property<DateTime?>("EndTime");

                    b.Property<string>("Normalization_InfoId");

                    b.Property<DateTime?>("StartTime");

                    b.Property<string>("Subdistrict");

                    b.Property<string>("SubdistrictID");

                    b.Property<int>("TotalReportNum");

                    b.HasKey("id");

                    b.HasIndex("Normalization_InfoId");

                    b.ToTable("OndutyClaims_Info");
                });

            modelBuilder.Entity("Dtol.dtol.UserLogin_Log", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("status");

                    b.Property<string>("uid");

                    b.HasKey("ID");

                    b.ToTable("UserLogin_Log");
                });

            modelBuilder.Entity("Dtol.dtol.User_Depart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.Property<string>("Remark");

                    b.Property<int?>("Sort");

                    b.HasKey("Id");

                    b.ToTable("user_Depart");
                });

            modelBuilder.Entity("Dtol.dtol.User_Info", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AddDate");

                    b.Property<string>("AddInfoDate");

                    b.Property<string>("Address");

                    b.Property<string>("AwardAndPunish");

                    b.Property<string>("Birthdate");

                    b.Property<string>("BloodType");

                    b.Property<string>("ComputerLevel");

                    b.Property<string>("ContractMaturityDate");

                    b.Property<string>("ContractSignDate");

                    b.Property<string>("Degree");

                    b.Property<string>("Dept");

                    b.Property<string>("DeptId");

                    b.Property<string>("DeptLeaderId");

                    b.Property<string>("DeptLeaderName");

                    b.Property<string>("DriveLevel");

                    b.Property<string>("DzbId");

                    b.Property<string>("Email");

                    b.Property<string>("EmployNature");

                    b.Property<string>("EntryDate");

                    b.Property<string>("FamilyMembers");

                    b.Property<string>("Files");

                    b.Property<string>("FinalEducation");

                    b.Property<string>("FinalGraduated");

                    b.Property<string>("FinalSpecialty");

                    b.Property<string>("ForeignLanguageLevel");

                    b.Property<string>("Gender");

                    b.Property<string>("HomeAddress");

                    b.Property<string>("Idcard");

                    b.Property<string>("InitialEducation");

                    b.Property<string>("InitialGraduated");

                    b.Property<string>("InitialSpecialty");

                    b.Property<string>("Interest");

                    b.Property<string>("JobPerformance");

                    b.Property<string>("JoinPartyDate");

                    b.Property<string>("Levels");

                    b.Property<string>("MaritalStatus");

                    b.Property<string>("MobileCall");

                    b.Property<string>("Nation");

                    b.Property<string>("NativePlace");

                    b.Property<string>("Number");

                    b.Property<int?>("OrderId");

                    b.Property<string>("PhoneCall");

                    b.Property<string>("PoliticalBackground");

                    b.Property<string>("Post");

                    b.Property<string>("Remark");

                    b.Property<string>("RoleIdNiwen");

                    b.Property<string>("RoleIds");

                    b.Property<string>("RoleNameNiwen");

                    b.Property<string>("RoleNames");

                    b.Property<string>("ServiceExperience");

                    b.Property<string>("Title");

                    b.Property<string>("TrainSituation");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.Property<string>("UserPwd");

                    b.Property<int?>("User_DepartId");

                    b.Property<string>("WorkExperience");

                    b.Property<string>("ZipCode");

                    b.Property<string>("status");

                    b.Property<DateTime?>("updateDate");

                    b.HasKey("Id");

                    b.HasIndex("User_DepartId");

                    b.ToTable("user_Info");
                });

            modelBuilder.Entity("Dtol.dtol.User_Relate_Info_Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("User_InfoId");

                    b.Property<int>("User_RoleId");

                    b.HasKey("Id");

                    b.HasIndex("User_InfoId");

                    b.HasIndex("User_RoleId");

                    b.ToTable("user_Relate_Info_Role");
                });

            modelBuilder.Entity("Dtol.dtol.User_Relate_Role_Right", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("User_RightsId");

                    b.Property<int>("User_RoleId");

                    b.HasKey("Id");

                    b.HasIndex("User_RightsId");

                    b.HasIndex("User_RoleId");

                    b.ToTable("user_Relate_Role_Right");
                });

            modelBuilder.Entity("Dtol.dtol.User_Rights", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ParentId");

                    b.Property<string>("Remark");

                    b.Property<string>("RightsName");

                    b.Property<string>("RightsValue");

                    b.Property<int?>("Sort");

                    b.Property<string>("Type");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("user_Rights");
                });

            modelBuilder.Entity("Dtol.dtol.User_Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Createdate");

                    b.Property<int?>("Flow_NodeDefineId");

                    b.Property<int?>("Flow_ProcedureId");

                    b.Property<DateTime?>("Level");

                    b.Property<string>("ParentId");

                    b.Property<string>("Remark");

                    b.Property<string>("RightsId");

                    b.Property<string>("RightsName");

                    b.Property<string>("RoleCode");

                    b.Property<string>("RoleName");

                    b.Property<string>("RoleType");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("user_Role");
                });

            modelBuilder.Entity("Dtol.dtol.VA_Handle", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CheckAddress");

                    b.Property<string>("CheckFace");

                    b.Property<string>("CheckFaceResult");

                    b.Property<string>("CheckFaceScore");

                    b.Property<DateTime?>("CheckTime");

                    b.Property<string>("Checklatitude");

                    b.Property<string>("Checklongitude");

                    b.Property<string>("ContentID");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Participant");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VID");

                    b.Property<string>("VNO");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("type");

                    b.HasKey("ID");

                    b.ToTable("VA_Handle");
                });

            modelBuilder.Entity("Dtol.dtol.VA_Sign", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentID");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Participant");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VID");

                    b.Property<string>("VNO");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("ramark");

                    b.HasKey("ID");

                    b.ToTable("VA_Sign");
                });

            modelBuilder.Entity("Dtol.dtol.VActivity_PublicShow", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Community");

                    b.Property<string>("CommunityID");

                    b.Property<string>("ContentID");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Experience");

                    b.Property<string>("Headimgurl");

                    b.Property<string>("Memo");

                    b.Property<string>("NickName");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VID");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<int>("isPublic");

                    b.HasKey("ID");

                    b.ToTable("VActivity_PublicShow");
                });

            modelBuilder.Entity("Dtol.dtol.VActivity_PublicShow_GiveLike", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Headimgurl");

                    b.Property<string>("Memo");

                    b.Property<string>("NickName");

                    b.Property<string>("PublicShowID");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VID");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.HasKey("ID");

                    b.ToTable("VActivity_PublicShow_GiveLike");
                });

            modelBuilder.Entity("Dtol.dtol.VActivity_Relate_Type", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentID");

                    b.Property<int>("Count");

                    b.Property<string>("Type");

                    b.Property<string>("TypeID");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.HasKey("ID");

                    b.ToTable("VActivity_Relate_Type");
                });

            modelBuilder.Entity("Dtol.dtol.VAttachment", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("FileName");

                    b.Property<string>("InternalName");

                    b.Property<string>("Length");

                    b.Property<string>("Path");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("Url");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("formid");

                    b.Property<string>("type");

                    b.HasKey("ID");

                    b.ToTable("VAttachment");
                });

            modelBuilder.Entity("Dtol.dtol.VBaseType", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("Memo");

                    b.Property<string>("Name");

                    b.Property<string>("ParentCode");

                    b.Property<int>("Sort");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.HasKey("ID");

                    b.ToTable("VBaseType");
                });

            modelBuilder.Entity("Dtol.dtol.VHA_Handle", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentID");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Participant");

                    b.Property<string>("Remark");

                    b.Property<string>("Results");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VID");

                    b.Property<string>("VNO");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.HasKey("ID");

                    b.ToTable("VHA_Handle");
                });

            modelBuilder.Entity("Dtol.dtol.VHA_Sign", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentID");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Participant");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VID");

                    b.Property<string>("VNO");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("flag");

                    b.Property<string>("operid");

                    b.Property<string>("opinion");

                    b.Property<string>("peroperid");

                    b.HasKey("ID");

                    b.ToTable("VHA_Sign");
                });

            modelBuilder.Entity("Dtol.dtol.VHelpArea", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Community");

                    b.Property<string>("CommunityID");

                    b.Property<string>("Contact");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Mobile");

                    b.Property<string>("ProblemType");

                    b.Property<string>("Score");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.Property<string>("Type");

                    b.Property<string>("TypeID");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("latitude");

                    b.Property<string>("longitude");

                    b.HasKey("ID");

                    b.ToTable("VHelpArea");
                });

            modelBuilder.Entity("Dtol.dtol.V_GetToken", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("addtime");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("token");

                    b.HasKey("ID");

                    b.ToTable("V_GetToken");
                });

            modelBuilder.Entity("Dtol.dtol.VolunteerActivity", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Community");

                    b.Property<string>("CommunityID");

                    b.Property<string>("Contact");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<DateTime?>("Etime");

                    b.Property<string>("Mobile");

                    b.Property<string>("Note");

                    b.Property<string>("Number");

                    b.Property<string>("Page");

                    b.Property<string>("PerMinScore");

                    b.Property<string>("Score");

                    b.Property<DateTime?>("SignEtime");

                    b.Property<DateTime?>("SignOutEtime");

                    b.Property<DateTime?>("SignOutStime");

                    b.Property<DateTime?>("SignStime");

                    b.Property<DateTime?>("SignUpEtime");

                    b.Property<DateTime?>("SignUpStime");

                    b.Property<string>("Skills");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("Stime");

                    b.Property<string>("Title");

                    b.Property<string>("Type");

                    b.Property<string>("TypeIDs");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("flag");

                    b.Property<string>("latitude");

                    b.Property<string>("longitude");

                    b.HasKey("ID");

                    b.ToTable("VolunteerActivity");
                });

            modelBuilder.Entity("Dtol.dtol.Volunteer_Info", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Birthday");

                    b.Property<string>("Career");

                    b.Property<string>("CertificateID");

                    b.Property<string>("CertificateType");

                    b.Property<string>("Community");

                    b.Property<string>("CommunityID");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("DataSource");

                    b.Property<string>("Degree");

                    b.Property<string>("Gender");

                    b.Property<string>("Headimgurl");

                    b.Property<string>("Marriage");

                    b.Property<string>("Mobile");

                    b.Property<string>("Name");

                    b.Property<string>("Nickname");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("Political");

                    b.Property<string>("ServiceTime");

                    b.Property<string>("Status");

                    b.Property<string>("Subdistrict");

                    b.Property<string>("SubdistrictID");

                    b.Property<string>("Unit");

                    b.Property<string>("UnitID");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VNO")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("openid");

                    b.Property<string>("unionid");

                    b.HasKey("ID");

                    b.ToTable("Volunteer_Info");
                });

            modelBuilder.Entity("Dtol.dtol.Volunteer_Message", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contents");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Name");

                    b.Property<string>("Status");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VID");

                    b.Property<string>("VNO");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.HasKey("ID");

                    b.ToTable("Volunteer_Message");
                });

            modelBuilder.Entity("Dtol.dtol.Volunteer_Relate_Type", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeID");

                    b.Property<string>("VolunteerID");

                    b.HasKey("ID");

                    b.ToTable("Volunteer_Relate_Type");
                });

            modelBuilder.Entity("Dtol.dtol.Volunteer_Score", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentID");

                    b.Property<DateTime?>("CreateDate");

                    b.Property<string>("CreateUser");

                    b.Property<string>("Memo");

                    b.Property<int>("Score");

                    b.Property<DateTime?>("UpdateDate");

                    b.Property<string>("UpdateUser");

                    b.Property<string>("VID");

                    b.Property<string>("bak1");

                    b.Property<string>("bak2");

                    b.Property<string>("bak3");

                    b.Property<string>("bak4");

                    b.Property<string>("bak5");

                    b.Property<string>("tableName");

                    b.Property<string>("type");

                    b.HasKey("ID");

                    b.ToTable("Volunteer_Score");
                });

            modelBuilder.Entity("Dtol.dtol.MydutyClaim_Info", b =>
                {
                    b.HasOne("Dtol.dtol.OndutyClaims_Info", "OndutyClaims_Info")
                        .WithMany("mydutyClaim_Infos")
                        .HasForeignKey("OndutyClaims_InfoId");
                });

            modelBuilder.Entity("Dtol.dtol.MydutyClaim_Sign", b =>
                {
                    b.HasOne("Dtol.dtol.OndutyClaims_Info", "OndutyClaims_Info")
                        .WithMany()
                        .HasForeignKey("OndutyClaims_InfoId");
                });

            modelBuilder.Entity("Dtol.dtol.OndutyClaims_Info", b =>
                {
                    b.HasOne("Dtol.dtol.Normalization_Info", "Normalization_Info")
                        .WithMany("ondutyClaims_Infos")
                        .HasForeignKey("Normalization_InfoId");
                });

            modelBuilder.Entity("Dtol.dtol.User_Info", b =>
                {
                    b.HasOne("Dtol.dtol.User_Depart", "User_Depart")
                        .WithMany()
                        .HasForeignKey("User_DepartId");
                });

            modelBuilder.Entity("Dtol.dtol.User_Relate_Info_Role", b =>
                {
                    b.HasOne("Dtol.dtol.User_Info", "User_Info")
                        .WithMany("User_Relate_Info_Role")
                        .HasForeignKey("User_InfoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dtol.dtol.User_Role", "User_Role")
                        .WithMany("User_Relate_Info_Role")
                        .HasForeignKey("User_RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dtol.dtol.User_Relate_Role_Right", b =>
                {
                    b.HasOne("Dtol.dtol.User_Rights", "User_Rights")
                        .WithMany()
                        .HasForeignKey("User_RightsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dtol.dtol.User_Role", "User_Role")
                        .WithMany("User_Relate_Role_Right")
                        .HasForeignKey("User_RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
