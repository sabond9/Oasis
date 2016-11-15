using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Oasis.Common;
using Oasis.DataAccess;
using Oasis.DataModel;
using Oasis.DataModel.Model;

namespace Oasis.Business
{
    public class AcccountManager
    {
        public bool CheckOldPassword(string oldPassword, int userId)
        {
            var unitOfWork = new UnitOfWork(new OasisContext());
            var userPasswordHash = unitOfWork.GetBaseRepository<User>().Get(userId).PasswordHash;
            return HashPasswordHelper.VerifyHashedPassword(userPasswordHash, oldPassword);
        }

        public void SaveUserPassword(string newPassword, int userId)
        {
            var unitOfWork = new UnitOfWork(new OasisContext());
            var user = unitOfWork.GetBaseRepository<User>().Get(userId);
            user.PasswordHash = HashPasswordHelper.HashPassword(newPassword);
            SaveUserPasswordHistory(user.PasswordHash, userId, unitOfWork);
            unitOfWork.SaveChanges();
        }

        public bool IsPasswordNew(string newPassword, int userId)
        {
            var unitOfWork = new UnitOfWork(new OasisContext());
            var userPasswords = unitOfWork.GetBaseRepository<UserPassword>().GetAll();
            return userPasswords.All(userPassword => !HashPasswordHelper.VerifyHashedPassword(userPassword.PasswordHash, newPassword));
        }

        private void SaveUserPasswordHistory(string passwordHash, int userId, UnitOfWork unitOfWork)
        {
            var userPasswordRepository = unitOfWork.GetBaseRepository<UserPassword>();
            userPasswordRepository.Add(new UserPassword
            {
                PasswordHash = passwordHash,
                UserId = userId,
                PasswordCreatedDate = DateTime.UtcNow
            });
        }
    }
}
