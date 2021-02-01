using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Services;
using TravelAgency.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Manager
{
    public class UserManager : IUserManager
    {
        public IUserService UserService;

        public UserManager(IUserService userService)

        {UserService = userService;}


        public async Task<IEnumerable<SM_USER>> All()
        {
            try
            {
                    return UserService.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to get to list");
            }
        }
     
        public async Task<SM_USER> FindById(object id)
        {
            try
            {
                return UserService.GetById((int)id);
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to search");
            }
        }

        public async Task<SM_USER> Save(SM_USER user)
        {
            try
            {
                if (user.ID_User == 0)
                {
                    var userSuccessfully = UserService.Create(user);
                    return userSuccessfully;
                }
                else
                {
                    UserService.Edit(user);
                    return user;
                }
            }
            catch (UniqueKeyException ex)
            {
                throw new Exception("Cannont insert or update a value duplicate");
            }
            catch (Exception ex)
            {
                throw new Exception("Cannont add a register");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var obj = UserService.Delete(id);
            }
            catch (DeleteWithRelationshipException ex)
            {
                throw new Exception("The record you are trying to delete is related to another");
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to delete");
            }
        }        
    }
}
