using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLInterface.Models;

namespace BLLInterface.Services
{
    public interface IUserService
    {
        void Register(UserEntity newUser);
        void Unregister(UserEntity userToDel);        
        IEnumerable<UserEntity> GetAllUserEntitiesShortInfo();
        UserEntity GetUser(string email);

        void GetAdditionalInfo(UserEntity user);
        void Update(UserEntity user);


    }
}
