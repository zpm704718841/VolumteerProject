using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dtol.dtol;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
namespace Dtol
{
    public class   DtolContext: DbContext
    {
        [Obsolete]
        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider((_, __) => true) });
        public DtolContext()
        {
        }

        public DtolContext(DbContextOptions<DtolContext> options)
            : base(options)
        {
        }

        //连接复原
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    optionsBuilder
        //        .UseSqlServer(
        //            @"Server = DESKTOP - QEJHC80\\SQL2014; Database = User_DateBase; Trusted_Connection = True;ConnectRetryCount=0",
        //            options => options.EnableRetryOnFailure());

        //optionsBuilder.UseMySQL("server=localhost;user=root;database=test;port=3306;password=****;SslMode=None")
        //}

        public DbSet<MydutyClaim_Info> MydutyClaim_Info { get; set; }
        public DbSet<Normalization_Info> Normalization_Info { get; set; }
        public DbSet<OndutyClaims_Info> OndutyClaims_Info { get; set; }
        public DbSet<MydutyClaim_Sign> MydutyClaim_Sign { get; set; }
        

        public DbSet<User_Info> user_Info { get; set; }
        public DbSet<User_Rights> user_Rights { get; set; }
        public DbSet<User_Role> user_Role { get; set; }
        public DbSet<User_Relate_Role_Right> user_Relate_Role_Right { get; set; }
        public DbSet<User_Relate_Info_Role> user_Relate_Info_Role { get; set; }
        public DbSet<User_Depart>user_Depart { get; set; }
        public DbSet<Volunteer_Info> Volunteer_Info { get; set; }
        public DbSet<VBaseType> VBaseType { get; set; }
        public DbSet<VHelpArea> VHelpArea { get; set; }
        public DbSet<VHA_Sign> VHA_Sign { get; set; }
        public DbSet<VHA_Handle> VHA_Handle { get; set; }
        public DbSet<VolunteerActivity> VolunteerActivity { get; set; }
        public DbSet<VA_Sign> VA_Sign { get; set; }
        public DbSet<VA_Handle> VA_Handle { get; set; }
        public DbSet<VAttachment> VAttachment { get; set; }
        public DbSet<V_GetToken> V_GetToken { get; set; }
        public DbSet<Volunteer_Relate_Type> Volunteer_Relate_Type { get; set; }
        public DbSet<VActivity_Relate_Type> VActivity_Relate_Type { get; set; }
        public DbSet<Volunteer_Score> Volunteer_Score { get; set; }
        public DbSet<VActivity_PublicShow> VActivity_PublicShow { get; set; }
        public DbSet<VActivity_PublicShow_GiveLike> VActivity_PublicShow_GiveLike { get; set; }
        //消息推送
        public DbSet<Volunteer_Message> Volunteer_Message { get; set; }
    }
}
