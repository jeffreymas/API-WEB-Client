using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table ("tb_m_department")]
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }

    }
}