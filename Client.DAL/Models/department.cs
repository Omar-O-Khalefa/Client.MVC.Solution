using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL.Models
{
    internal class department
    {
        public int Id { get; set; }
        //[Required(ErrorMessage ="Code Is Required ya Hamada !!")]
        public string  Code { get; set; }
       // [Required]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}
