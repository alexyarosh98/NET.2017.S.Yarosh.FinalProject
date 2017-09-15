using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALInterface.DTO
{
    public class DALCategory:IDALEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<DALTask> Tasks { get; set; }
    }
}
