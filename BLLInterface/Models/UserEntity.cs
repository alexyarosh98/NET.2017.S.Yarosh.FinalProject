using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLLInterface.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nickname is requiered")]
        [MaxLength(15,ErrorMessage = "Nickname's length must be from 3 to 15 symbols")]
        [MinLength(3,ErrorMessage = "Nickname's length must be from 3 to 15 symbols")]
        public string  Nickname { get; set; }
        [Required(ErrorMessage = "Possword is requiered")]
        [MinLength(6,ErrorMessage = "Possword must consist of 6 or more symbols")]
        [System.ComponentModel.DataAnnotations.Compare("PosswordConfirm",ErrorMessage = "Posswords are not the same")]
        public string Possword { get; set; }
        public string PosswordConfirm { get; set; }
        [Required(ErrorMessage = "Email is requiered")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage = "Invalid email")]
        [Remote("CheckUsersEmail","Account",ErrorMessage = "This email is already exists")]
        public string Email { get; set; }
        public Role Role { get; set; }
        public double Rating { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Range(14,int.MaxValue,ErrorMessage = "Sorry, but your are not allowed to register")]
        public short? Age { get; set; }
        public bool? Gender { get; set; }

        //public virtual List<TaskEntity> CreatedTasks { get; set; }
        //public virtual List<TaskEntity> DevelopedTasks { get; set; }
        /// <summary>
        /// TRUE if user is online, otherwise FALSE 
        /// </summary>
        public bool Status { get; set; } 

    }
    public enum Role : byte
    {
        user = 1,
        manager = 2,
        admin = 3
    }
}
