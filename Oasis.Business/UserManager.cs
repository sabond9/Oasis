using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oasis.Common;
using Oasis.DataAccess;
using Oasis.DataModel;
using Oasis.DataModel.Model;

namespace Oasis.Business
{
    public class UserManager
    {
        public bool VerifyCredentials(string userName, string password)
        {
            var unitOfWork = new UnitOfWork(new OasisContext());
            var user = unitOfWork.GetBaseRepository<User>()
                                 .GetAll(r => r.UserRoles)
                                 .SingleOrDefault(r => r.UserName == userName);

            return user != null && HashPasswordHelper.VerifyHashedPassword(user.PasswordHash, password);
        }
    }
}
