using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALInterface.DTO
{
    public class DALTask:IDALEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public byte Status { get; set; }
        public string Category { get; set; }
        //public virtual DALTaskInfo TaskInfo { get; set; }
        public string Description { get; set; }
        public System.DateTime Deadline { get; set; }
        public short Implementation { get; set; }
        //public int CreatorUserId { get; set; }
        //public int DeveloperUserId { get; set; }
        public virtual DALUser CreatorUser { get; set; }
        public virtual DALUser Developer { get; set; }
    }

   
}
