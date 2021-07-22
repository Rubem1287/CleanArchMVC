using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMVC.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage="The Name is Required")]
        [MinLength(3, ErrorMessage ="Minimum 3 Characteres")]
        [MaxLength(100, ErrorMessage ="Maximum 100 Characteres")]
        public string Name { get; set; }
    }
}
