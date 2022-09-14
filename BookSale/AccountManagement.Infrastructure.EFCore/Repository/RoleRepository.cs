﻿using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _accountContext;

        public RoleRepository(AccountContext accountContext):base(accountContext)
        {
            _accountContext = accountContext;
        }

        public EditRole GetDetails(long id)
        {
            return _accountContext.Roles.Select(r => new EditRole
            {
                Id = r.Id,
                Name = r.Name,
                MappedPermissions = MapPermissions(r.Permissions)
            }).AsNoTracking()
            .FirstOrDefault(r => r.Id == id);
        }

        private static List<PermissionDto> MapPermissions(IEnumerable<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }

        public List<RoleViewModel> List()
        {
            return _accountContext.Roles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                CreationDate = r.CreationDate.ToFarsi()
            }).ToList();
        }
    }
}
