using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DAL.Models
{
    public class Department :ModelBase
    {
        public int Id { get; set; }
        //[Required(ErrorMessage ="Code Is Required ya Hamada !!")]
        public string  Code { get; set; }
       // [Required]
        public string Name { get; set; }

        [Display(Name ="Date Of Creation")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;


        //navigationl Property => [Many]

        //[InverseProperty(nameof(Employee.Department))]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
