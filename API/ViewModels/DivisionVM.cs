using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    public class DivisionVM
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}