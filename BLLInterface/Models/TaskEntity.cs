using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLLInterface.Models
{
    public class TaskEntity
    {
        [ScaffoldColumn(false)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is requiered")]
        [MaxLength(30,ErrorMessage = "Title must consist of not more then 30 symbols")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price is requiered")]
        public decimal Price { get; set; }
        public Status Status { get; set; }
        [Required(ErrorMessage = "Category is requiered")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Description is requiered")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Deadline is required")]
        [DataType(DataType.Date,ErrorMessage = "Date must be dd/mm/yyy")]
        [Remote("CheckDate","Tasks",ErrorMessage = "Date must be in the future")]
        public System.DateTime Deadline { get; set; }
        public short Implementation { get; set; }
        public virtual UserEntity CreatorUser { get; set; }
        public virtual UserEntity Developer { get; set; }
        public TimeSpan TimeLeft {
            get
            {
                if(Deadline>DateTime.Now) return Deadline - DateTime.Now;
                else return new TimeSpan(0,0,0,0);
            }

        }
}
}
public enum Status : byte
{
    Awating = 1,
    Developing = 2,
    Completed = 3
}