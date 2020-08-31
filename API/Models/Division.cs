using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table ("tb_m_division")]
    public class Division
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int department_id { get; set; }

        [ForeignKey("department_id")]
        public Department Department { get; set; }
    }
}