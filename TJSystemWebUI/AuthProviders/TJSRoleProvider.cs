using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLLInterface.Models;
using BLLInterface.Services;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace TJSystemWebUI.AuthProviders
{
    public class TJSRoleProvider:RoleProvider
    {
        public IUserService UserRepository
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        //public IRoleRepository RoleRepository
        //    => (IRoleRepository)DependencyResolver.Current.GetService(typeof(IRoleRepository));

        public override bool IsUserInRole(string email, string roleName)
        {

            UserEntity user = UserRepository.GetUser(email);

            if (user == null) return false;

            Role userRole = user.Role;

            if (userRole != null && userRole.ToString() == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        //public override string[] GetRolesForUser(string email)
        //{
        //    using (var context = new UserContext())
        //    {
        //        var roles = new string[] { };
        //        var user = context.Users.FirstOrDefault(u => u.Email == email);

        //        if (user == null) return roles;

        //        var userRole = user.Role;

        //        if (userRole != null)
        //        {
        //            roles = new string[] { userRole.Name };
        //        }
        //        return roles;
        //    }
        //}
        
    }
}