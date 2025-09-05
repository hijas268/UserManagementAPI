using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Constants.Enums;

namespace UserManagement.Enities
{
    public static class Role
    {
        public const int Admin =(int)UserRole.Admin;
        public const int User = (int)UserRole.User;
        public const int ReadOnlyUser = (int)UserRole.ReadOnlyUser;
    }
}
