﻿using AccountManagement.Domain.AccountAgg;
using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : IdentityRole<int>
    {
        public DateTime CreationDate { get; private set; }
        public List<Permission> Permissions { get; private set; }
        //public List<Account> Accounts { get; private set; }

        protected Role()
        {
        }
        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
            //Accounts = new List<Account>();
            CreationDate = DateTime.Now;    
        }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
