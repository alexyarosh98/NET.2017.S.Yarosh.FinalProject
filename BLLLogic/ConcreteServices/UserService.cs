using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface;
using BLLInterface.Models;
using BLLInterface.Services;
using BLLLogic.Mappers;
using DALInterface.Repos;

namespace BLLLogic.ConcreteServices
{
    public class UserService:IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger logger;

        public UserService(IUserRepository _userRepository,ILogger _logger)
        {
            userRepository = _userRepository;
            logger = _logger;
        }
        public void Register(UserEntity newUser)
        {
            if(newUser==null) throw new ArgumentNullException();
            userRepository.Create(newUser.ToDalUser());
            
            logger.Info($"User {newUser.Nickname} created with email {newUser.Email}");
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

        public void GetAdditionalInfo(ref UserEntity user)
        {
            user = userRepository.GetAdditionalInfo(user.ToDalUser()).ToBllUser();
        }

        public void Update(UserEntity user)
        {
            userRepository.Update(user.ToDalUser());
            logger.Info($"{user.Nickname}'s role was changed to {user.Role}");
        }

        public UserEntity GetUser(string email)
        {
            return userRepository.GetByEmail(email)==null?null:userRepository.GetByEmail(email).ToBllUser();
        }
    }
}
