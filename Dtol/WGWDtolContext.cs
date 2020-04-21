using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Dtol.WGWdtol;

namespace Dtol
{
    public class WGWDtolContext: DbContext
    {
        public WGWDtolContext()
        {
        }

        public WGWDtolContext(DbContextOptions<WGWDtolContext> options)
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


        //}

 
        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
