using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebApplication.DAL;
using CommonDll;
using CommonDll.ActualResult;
using CommonDll.Entities;

namespace ClientWebApplication.BL
{
    public static class ClientProvider
    {
        public static ActualResult<UserBase> Login(string login, string password)
        {
            return ClientRepository.FindClientByCredentials(login,
               // PasswordCrypter.CryptPassword(password));
               password);
        }

        public static ActualResult Register(Client client)
        {
            client.Password = PasswordCrypter.CryptPassword(client.Password);
            return ClientRepository.InsertClient(client);
        }
    }
}
