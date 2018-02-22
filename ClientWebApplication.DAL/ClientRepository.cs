using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDll.Entities;
using CommonDll;
using CommonDll.ActualResult;

namespace ClientWebApplication.DAL
{
    public static class ClientRepository
    {

        public static ActualResult<UserBase> FindClientByCredentials(string email, string password)
        {
            var result = new ActualResult<UserBase>();
            try
            {
                using (var context = new AllocAccokContext())
                {
                    result.Result = context.UserAccsess.FirstOrDefault(x => x.Email == email &&
                        x.Password == password && x.Role == Role.Client);
                }   
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }

        public static ActualResult InsertClient(Client client)
        {
            var result = new ActualResult();
            try
            {
                using (var context = new AllocAccokContext())
                {
                    var cc = context.Clients.Where(x => x.Id > 0).ToList();
                    context.Clients.Add(client);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.IsValid = false;
                result.ErrorsList.Add(new Error(DateTime.Now, e.Message));
            }
            return result;
        }
    }
}
