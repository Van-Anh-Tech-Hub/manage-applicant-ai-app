using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class JobDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Salary { get; set; }
        public int Experience { get; set; }
        public DateTime Deadline { get; set; }
        public string CompanyId { get; set; }
        public string LocationId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
