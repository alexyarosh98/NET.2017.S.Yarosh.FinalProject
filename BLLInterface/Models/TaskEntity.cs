using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLInterface.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public System.DateTime Deadline { get; set; }
        public short Implementation { get; set; }
        public virtual UserEntity CreatorUser { get; set; }
        public virtual UserEntity Developer { get; set; }
        public TimeSpan TimeLeft {get => Deadline - DateTime.Now; }
}
}
public enum Status : byte
{
    awating = 1,
    developing = 2,
    completed = 3
}