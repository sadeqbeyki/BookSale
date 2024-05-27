﻿namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public long Id { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public ApplicationRole Role { get; private set; }
        public Permission(int code)
        {
            Code = code;
        }
    }
}
