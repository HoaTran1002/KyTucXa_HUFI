using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using QLKTX.Models;

namespace QLKTX.Identity
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public AppDBContext():base("KyTucXaEntities")
        {  }
    }
}