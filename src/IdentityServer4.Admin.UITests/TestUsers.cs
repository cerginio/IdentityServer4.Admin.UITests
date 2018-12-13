using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer4.Admin.UITests
{
    public static class TestUsers
    {
        public static User LocalTestUser => new User("alice", "alice");
        // top secret while alfa environment is public and alfa contains copy of partners ATEST configuration,
        // TODO: secure - remove from source code.
        // Consider aspnetcore SecretManager for dev purposes https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=windows
        // Project convertion net452 to aspnetcore2.1 required
    }

    public class User
    {
        public string Login { get; set; } = null;
        public string Password { get; set; } = null;

        public User(string login, string password)
        {
            Password = password;
            Login = login;
        }
    }
}
