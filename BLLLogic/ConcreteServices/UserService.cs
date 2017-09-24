using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;
using BLLInterface.Services;
using BLLLogic.Mappers;
using DALInterface.Repos;

namespace BLLLogic.ConcreteServices
{
    public class UserService:IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public void Register(UserEntity newUser)
        {
            if(newUser==null) throw new ArgumentNullException();
            userRepository.Create(newUser.ToDalUser());
        }

        public void Unregister(UserEntity userToDel)
        {

            if (userToDel == null) throw new ArgumentNullException();
            userRepository.Delete(userToDel.ToDalUser());
        }

       

        public IEnumerable<UserEntity> GetAllUserEntitiesShortInfo()
        {
            return userRepository.GetAllShortInfo().Select(i => i.ToBllUser());
        }

        public void GetAdditionalInfo(UserEntity user)
        {
            user = userRepository.GetAdditionalInfo(user.ToDalUser()).ToBllUser();
        }

        public void Update(UserEntity user)
        {
            userRepository.Update(user.ToDalUser());
        }

        public UserEntity GetUser(string email)
        {
            return userRepository.GetByEmail(email)==null?null:userRepository.GetByEmail(email).ToBllUser();
        }
    }
}
