using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ViewModel.System.Role
{
    public class RoleAssignRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}
