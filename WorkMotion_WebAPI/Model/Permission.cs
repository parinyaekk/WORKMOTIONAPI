using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMotion_WebAPI.Model
{
    public class Permission
    {
        public class PermissionModel
        {
            public int? PerPage { get; set; }
            public int? Page { get; set; }
            public string Search { get; set; }
            public int? ID { get; set; }
        }

        public class UpdateDatPermissionModel
        {
            public int? ID { get; set; }
            public string GroupName { get; set; }
            public string PermissionWarranty { get; set; }
            public string PermissionMenu { get; set; }
            public string PermissionContent { get; set; }
            public string PermissionSparepartAndInstallation { get; set; }
            public string PermissionModelSparepart { get; set; }
            public string PermissionClassifiedSparepart { get; set; }
            public string PermissionModelInstallation { get; set; }
            public string PermissionClassifiedInstallation { get; set; }
            public string PermissionManageProduct { get; set; }
            public string PermissionMembershipRegistration { get; set; }
            public string PermissionManageMembership { get; set; }
            public string PermissionMembershipRenew { get; set; }
            public string PermissionServiceMembershipInformation { get; set; }
            public string PermissionManageEmployee { get; set; }
            public string PermissionManageSatisfaction { get; set; }
        }
    }
}
