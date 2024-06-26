﻿using Microsoft.AspNetCore.Identity;

namespace AccountManagement.Domain.Entities.UserAgg;

public class ApplicationUser : IdentityUser<long>
{
    public string FullName { get; private set; }
    public string Password { get; private set; }

    public string ProfilePhoto { get; private set; }
    public DateTime CreationDate { get; set; }

    public ApplicationUser(string fullName, string userName, string password, string phoneNumber, long roleId, string profilePhoto)
    {
        FullName = fullName;
        UserName = userName;
        Password = password;
        PhoneNumber = phoneNumber;

        ProfilePhoto = profilePhoto;
        CreationDate = DateTime.Now;
    }
    public void Edit(string fullName, string userName, string phoneNumber, long roleId, string profilePhoto)
    {
        FullName = fullName;
        UserName = userName;
        PhoneNumber = phoneNumber;

        if (!string.IsNullOrEmpty(profilePhoto))
            ProfilePhoto = profilePhoto;
    }

    public void ChangePassword(string password)
    {
        Password = password;
    }
}
