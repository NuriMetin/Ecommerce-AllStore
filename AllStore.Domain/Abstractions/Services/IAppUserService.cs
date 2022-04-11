using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Domain.Abstractions.Services
{
    public interface IAppUserService
    {
        SignInResult UserSignIn(AppUser user, string password);
        void UserBlock(string email);
        void UnlockUser(string email);
    }
}
