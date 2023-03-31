using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using QLKTX.Models;

namespace QLKTX.Identity
{
    public class AppUserStore : UserStore<ApplicationUser>
    {
        public AppUserStore(AppDBContext dBContext) : base(dBContext) { } 
    }
}