
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL.Models
{
	public class ApplicationUser : IdentityUser
	{
        public bool  IsAgree {	 get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
