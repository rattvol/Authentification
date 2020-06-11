using Authentification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentification
{
    public class GetPublic
    {
        public static PublicUser GetPublicUser(User user)
        {
            return (new PublicUser()
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                MiddleName = user.MiddleName,
                SurName = user.SurName,
                Token = user.Token,
                DateOfRegistration = user.DateOfRegistration
            });
        }
    }
}
