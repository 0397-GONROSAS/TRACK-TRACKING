using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DTO
{
    public class EmployeeDTO 
    {
        public List<EmployeeDetailDTO> Employees { get; set; }
        public List<PERMISSIONS> permission { get; set; }
        
    }
}
