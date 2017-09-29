using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using BLLInterface.Services;
using System.Web.Mvc;
using BLLInterface.Models;

namespace TJSystemWebUI.AuthProviders
{
    public class TJSMembershipProvider:MembershipProvider
    {
        public IUserService UserService
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        //public IRoleRepository RoleRepository
        //    => (IRoleRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleRepository));





        //public MembershipUser CreateUser(string email, string password,string nickname,string firstname=null,
        //        string lastname=null,short? age=null,bool? gender=null)
        //{
        //    MembershipUser membershipUser = GetUser(email, false);

        //    if (membershipUser != null)
        //    {
        //        return null;
        //    }

        //    var user = new UserEntity()
        //    {
        //        Nickname = nickname,
        //        Email = email,
        //        Password = Crypto.HashPassword(password),
        //        Role = Role.user,
        //        Firstname = firstname,
        //        Lastname = lastname,
        //        Age = age,
        //        Gender = gender
                
        //    };
            

        //    UserService.Register(user);
        //    membershipUser = GetUser(email, false);
        //    return membershipUser;
        //}
        public MembershipUser CreateUser(UserEntity user)
        {
            MembershipUser membershipUser = GetUser(user.Email, false);

            if (membershipUser != null)
            {
                return null;
            }
            user.Password = Crypto.HashPassword(user.Password);
            user.Role=Role.user;
            UserService.Register(user);
            membershipUser = GetUser(user.Email, false);
            return membershipUser;


        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string email, string password)
        {
            UserEntity user = UserService.GetUser(email);

            Debug.WriteLine("!!!!!!!!!!!!" + user.Email + user.Nickname);
            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }
            return false;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = UserService.GetUser(email);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Email,
                null, null, null, null,
                false, false,DateTime.MinValue, 
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }
    }
}