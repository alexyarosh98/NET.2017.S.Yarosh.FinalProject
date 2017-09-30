using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DALInterface.DTO;
using DALInterface.Repos;
using EF_ORM;

namespace DALLogic
{
    public class UserRepos:IUserRepository
    {
        private readonly DbContext context;

        public UserRepos(DbContext _context)
        {
            context = _context;
        }
        //public IEnumerable<DALUser> GetAllFullInfo()
        //{
        //    return context.Set<User>().Include("UserInfoes")
        //        .Select(user => new DALUser()
        //        {
        //            Nickname = user.Nickname,
        //            Password = user.Password,
        //            Email = user.Email,
        //            Role = user.Role,
        //            Rating = user.Rating,
        //            Firstname = user.UserInfo.Firstname,
        //            Lastname = user.UserInfo.Lastname,
        //            Age = user.UserInfo.Age,
        //            Gender = user.UserInfo.Gender,
        //            CreatedTasks = new List<DALTask>(user.CreatedTasks.Select(i => new DALTask()
        //            {
        //                Title = i.Task.Title,
        //                Price = i.Task.Price,
        //                Status = i.Task.Status
        //            })),
        //            DevelopedTasks = new List<DALTask>(user.CreatedTasks.Select(i => new DALTask()
        //            {
        //                Title = i.Task.Title,
        //                Price = i.Task.Price,
        //                Status = i.Task.Status
        //            }))
        //        });

        //}

        public IEnumerable<DALUser> GetAllShortInfo()
        {
            return context.Set<User>()
                .Select(user => new DALUser()
                {
                    Id = user.UserId,
                    Nickname = user.Nickname,
                    Password = user.Password,
                    Email = user.Email,
                    Role = user.Role,
                    Rating = user.Rating
                });
        }

        public DALUser GetByEmail(string email)
        {
           
            User expectedUser=context.Set<User>().FirstOrDefault(user => user.Email == email);
            if (ReferenceEquals(expectedUser, null)) return null;// throw new InvalidOperationException();
           ///// Запись в лог на уровень выше ы
            return new DALUser()
            {
                Id = expectedUser.UserId,
                Nickname = expectedUser.Nickname,
                Password = expectedUser.Password,
                Email = expectedUser.Email,
                Role = expectedUser.Role,
                Rating = expectedUser.Rating
               
            };
        }

        public DALUser GetAdditionalInfo(DALUser user)
        {
            User expectedUser = context.Set<User>().Include("UserInfo").FirstOrDefault(i => i.Email == user.Email);
            if (ReferenceEquals(expectedUser, null)) throw new InvalidOperationException();

            return new DALUser()
            {
                Id = expectedUser.UserId,
                Nickname = expectedUser.Nickname,
                Password = expectedUser.Password,
                Email = expectedUser.Email,
                Role = expectedUser.Role,
                Rating = expectedUser.Rating,
                Firstname =expectedUser.UserInfo==null?null: expectedUser.UserInfo.Firstname,
                Lastname = expectedUser.UserInfo == null ? null : expectedUser.UserInfo.Lastname,
                Age = expectedUser.UserInfo == null ? null : expectedUser.UserInfo.Age,
                Gender = expectedUser.UserInfo == null ? null : expectedUser.UserInfo.Gender,
                //CreatedTasks = new List<DALTask>(expectedUser.CreatedTasks.Select(i=>new DALTask()
                //{
                //    Title = i.Task.Title,
                //    Price = i.Task.Price,
                //    Status = i.Task.Status
                //})),
                //DevelopedTasks = new List<DALTask>(expectedUser.CreatedTasks.Select(i=>new DALTask()
                //{
                //    Title = i.Task.Title,
                //    Price = i.Task.Price,
                //    Status = i.Task.Status
                //}))
            };
        }

        public void Create(DALUser user)
        {
            if (context.Set<User>().FirstOrDefault(u=>u.Email==user.Email)!=null)
            {
                throw new InvalidOperationException();
            }

            UserInfo newUserInfo=null;
            if (user.Firstname != null || user.Lastname != null || user.Age != null || user.Gender != null)
            {
                newUserInfo=new UserInfo()
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Age = user.Age,
                    Gender = user.Gender
                };
            }
            
            User newUser=new User()
            {
                Nickname = user.Nickname,
                Password = user.Password,
                Email = user.Email,
                Role = (byte)user.Role,
                Rating = user.Rating,
                UserInfo = newUserInfo
            };
           // newUserInfo.User = newUser;

            context.Set<User>().Add(newUser);
            if(newUserInfo!=null)context.Set<UserInfo>().Add(newUserInfo);
            context.SaveChanges();
        }

        public void Delete(DALUser user)
        {
            User userToDel = context.Set<User>().FirstOrDefault(u => u.Email == user.Email);
            if(ReferenceEquals(userToDel,null)) throw new InvalidOperationException();

            UserInfo userInfoToDel = userToDel.UserInfo;

            context.Set<User>().Remove(userToDel);
            if (!ReferenceEquals(userInfoToDel, null)) context.Set<UserInfo>().Remove(userInfoToDel);
            context.SaveChanges();

        }

        public void Update(DALUser entity)
        {
            User userToUp = context.Set<User>().Find(entity.Id);
            //context.Entry(userToUp).CurrentValues.SetValues(entity);

            userToUp.Nickname = entity.Nickname;
            userToUp.Email = entity.Email;
            userToUp.Password = entity.Password;
            userToUp.Rating = entity.Rating;
            userToUp.Role = entity.Role;

            if (!string.IsNullOrEmpty(entity.Firstname) || !string.IsNullOrEmpty(entity.Lastname)
                || entity.Age != null || entity.Gender != null)
            {
                UserInfo userInfoToUp = userToUp.UserInfo;
                //context.Entry(userInfoToUp).CurrentValues.SetValues(entity);
                userInfoToUp.Firstname = entity.Firstname;
                userInfoToUp.Lastname = entity.Lastname;
                userInfoToUp.Age = entity.Age;
                
            }

            context.SaveChanges();
        }

        public IEnumerable<DALUser> FindByPredicate(Expression<Func<DALUser, bool>> func)
        {
            throw new NotImplementedException();
            //return context.Set<User>().Select(i=>new DALUser(){})
        }
        
    }
}
