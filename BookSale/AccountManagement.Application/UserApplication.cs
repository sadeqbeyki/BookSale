﻿using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Auth;
using AccountManagement.Domain.Entities.Auth;
using AccountManagement.Domain.Entities.RoleAgg;
using AccountManagement.Domain.Entities.UserAgg;
using AppFramework.Application;

namespace AccountManagement.Application;

public class UserApplication : IUserApplication
{
    private readonly IFileUploader _fileUploader;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _accountRepository;
    private readonly IAuthHelper _authHelper;
    private readonly IRoleRepository _roleRepository;


    public UserApplication(IUserRepository accountRepository, IPasswordHasher passwordHasher,
        IFileUploader fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _fileUploader = fileUploader;
        _authHelper = authHelper;
        _roleRepository = roleRepository;
    }

    public OperationResult ChangePassword(ChangePassword command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.Get(command.Id);
        if (account == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (command.Password != command.RePassword)
            return operation.Failed(ApplicationMessages.PasswordNotMatch);

        var password = _passwordHasher.Hash(command.Password);
        account.ChangePassword(password);
        _accountRepository.SaveChanges();
        return operation.Succeeded();
    }

    public OperationResult Register(RegisterAccount command)
    {
        InitialUserData();
        var operation = new OperationResult();
        if (_accountRepository.Exists(x => x.UserName == command.UserName || x.PhoneNumber == command.Mobile))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var password = _passwordHasher.Hash(command.Password);
        var path = $"ProfilePhotos";
        var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);

        var account = new ApplicationUser(
            command.FullName,
            command.UserName,
            password,
            command.Mobile,
            command.RoleId,
            picturePath);

        _accountRepository.Create(account);
        _accountRepository.SaveChanges();

        AddUserToRole(account);

        return operation.Succeeded();
    }

    private void AddUserToRole(ApplicationUser account)
    {
        var users = _accountRepository.GetAccounts();
        if (users.Count == 1)
            _accountRepository.AddUserToRole(account, ["SystemUser", "Administrator"]);
        _accountRepository.AddUserToRole(account, ["SystemUser"]);
    }

    private void InitialUserData()
    {
        var roles = _roleRepository.GetRoles();
        if (roles.Count <= 0)
        {
            var addRoles = new List<ApplicationRole>
                {
                    new("Administrator", new List<Permission>()),
                    new("SystemUser", new List<Permission>()),
                    new("ContentUploader", new List<Permission>()),
                    new("ColleagueUser", new List<Permission>())
                };
            foreach (var item in addRoles)
            {
                _roleRepository.Create(item);
            }
            _roleRepository.SaveChanges();
        };
    }

    public OperationResult Edit(EditAccount command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.Get(command.Id);
        if (account == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (_accountRepository.Exists(x => (x.UserName == command.UserName ||
                                            x.PhoneNumber == command.Mobile) && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var path = $"ProfilePhotos";
        var picturePath = _fileUploader.Upload(command.ProfilePhoto, path);
        account.Edit(command.FullName, command.UserName, command.Mobile, command.RoleId, picturePath);
        _accountRepository.SaveChanges();
        return operation.Succeeded();
    }

    public EditAccount GetDetails(long id)
    {
        return _accountRepository.GetDetails(id);
    }

    public async Task<OperationResult> Login(Login command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.GetBy(command.UserName);
        if (account == null)
            return operation.Failed(ApplicationMessages.WrongUserPass);

        (bool Verified, bool NeedsUpgrade) = _passwordHasher.Check(account.Password, command.Password);
        if (!Verified)
            return operation.Failed(ApplicationMessages.WrongUserPass);


        //
        var roleList = await _accountRepository.GetUserRolesAsync(account.Id);

        var permissions = new List<int>();
        int roleId = 0;
        foreach (var item in roleList)
        {
            roleId = _roleRepository.GetRolesId(item);
            permissions = _roleRepository.Get(roleId).Permissions.Select(x => x.Code).ToList();
        }
        //
        var authViewModel = new AuthViewModel(account.Id, account.FullName, account.UserName, account.PhoneNumber, permissions);

        _authHelper.SignIn(authViewModel);
        return operation.Succeeded();
    }

    public void LogOut()
    {
        _authHelper.SignOut();
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        return _accountRepository.Search(searchModel);
    }

    public List<AccountViewModel> GetAccounts()
    {
        return _accountRepository.GetAccounts();
    }

    public AccountViewModel GetAccountBy(long id)
    {
        var account = _accountRepository.Get(id);
        return new AccountViewModel()
        {
            FullName = account.FullName,
            Mobile = account.PhoneNumber
        };
    }
}