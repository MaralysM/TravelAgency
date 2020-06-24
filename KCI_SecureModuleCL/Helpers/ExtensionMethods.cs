using KCI_SecureModuleCL.Models;
using System.Collections.Generic;
using System.Linq;

namespace KCI_SecureModuleCL.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<SM_USER> WithoutPasswords(this IEnumerable<SM_USER> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static SM_USER WithoutPassword(this SM_USER user)
        {
            if (user == null) return null;

            user.TX_Password = null;
            return user;
        }
    }
}