using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.DataModel.Model
{
    public class UserPassword
    {
        public int Id { get; set; }

        public string PasswordHash { get; set; }

        public DateTime PasswordCreatedDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
