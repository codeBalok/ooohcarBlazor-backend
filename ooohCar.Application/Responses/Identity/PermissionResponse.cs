﻿using System.Collections.Generic;

namespace ooohCar.Application.Responses.Identity
{
    public class PermissionResponse
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleClaimsResponse> RoleClaims { get; set; }
    }
}