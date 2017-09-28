using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALInterface.DTO
{
    public class DALUser:IDALEntity
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte Role { get; set; }
        public double Rating { get; set; }
       //// public virtual DALUserInfo UserInfo { get; set; }
       // public virtual List<DALTask> CreatedTasks { get; set; }//DALTaskInfo
       // public virtual List<DALTask> DevelopedTasks { get; set; }//DALTaskInfo

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public short? Age { get; set; }
        public bool? Gender { get; set; }
    }

    
  
}
