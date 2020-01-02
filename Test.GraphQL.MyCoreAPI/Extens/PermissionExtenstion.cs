using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace Test.GraphQL.MyCoreAPI.Extens
{
    public static class PermissionExtenstion
    {
        public static readonly string PermissionKey = "permission";
        public static bool RequiresPermission(this IProvideMetadata type)
        {
            var permissions = type.GetMetadata<IEnumerable<string>>(PermissionKey, new List<string> { });
            return permissions.Any();
        }

        public static bool CanAccess(this IProvideMetadata type, IEnumerable<string> claimes)
        {
            var permissions = type.GetMetadata<IEnumerable<string>>(PermissionKey, new List<string> { });
            return permissions.Any(x => claimes.Contains(x));
        }

        public static void AddPermissions(this IProvideMetadata type, string permission)
        {
            var permissions = type.GetMetadata<List<string>>(PermissionKey);
            if (permissions == null)
            {
                permissions = new List<string>();

            }
            permissions.Add(permission);
            type.Metadata[PermissionKey] = permissions;
        }
    }
}
