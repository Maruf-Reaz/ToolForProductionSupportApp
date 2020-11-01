using System;
using System.Collections.Generic;
using System.Text;

namespace Dynamo.Model.Common.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}
