using Dtol.Easydtol;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dtol
{
    public class EasyDtolContext: DbContext
    {
        public EasyDtolContext()
        {
        }

        public EasyDtolContext(DbContextOptions<EasyDtolContext> options)
            : base(options)
        {
        }

        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
