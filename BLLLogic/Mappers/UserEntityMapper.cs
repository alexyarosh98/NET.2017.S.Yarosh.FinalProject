using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;
using DALInterface.DTO;

namespace BLLLogic.Mappers
{
    public static class UserEntityMapper
    {
        public static UserEntity ToBllUser(this DALUser user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Password = user.Password,
                Email = user.Email,
                Rating = (ushort)user.Rating,
                Role = (Role)user.Role,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Age = user.Age,
                Gender = user.Gender,
                //CreatedTasks = new List<TaskEntity>(user.CreatedTasks.Select(i=>i.ToBllEntity())),
                //DevelopedTasks = new List<TaskEntity>(user.DevelopedTasks.Select(i=>i.ToBllEntity()))
            };
        }

        public static DALUser ToDalUser(this UserEntity user)
        {
            return new DALUser()
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Password = user.Password,
                Email = user.Email,
                Role = (byte)user.Role,
                Rating = user.Rating,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Age = user.Age,
                Gender = user.Gender,
                //CreatedTasks = new List<DALTask>(user.CreatedTasks.Select(i=>i.ToDALTask())),
                //DevelopedTasks = new List<DALTask>(user.DevelopedTasks.Select(i=>i.ToDALTask()))
                
            };
        }
    }
}
