using Microsoft.AspNetCore.Identity;
using PFM.Application.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class PasswordServices :IPasswordServices
    {
        private readonly PasswordHasher<Object> _passwordHasher;
        public PasswordServices()
        {
            _passwordHasher = new PasswordHasher<object>();
        }
        public string HashPassword(string  password)
        {
            return _passwordHasher.HashPassword(null, password);
        }
        public bool VerifyPassword(string password,string PasswordHash)
        {
            if (string.IsNullOrWhiteSpace(password) ||
        string.IsNullOrWhiteSpace(PasswordHash))
            {
                return false;
            }

            var result = _passwordHasher.VerifyHashedPassword(null,PasswordHash,password);

            // Accept valid hashes, including hashes that need upgrading.
            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }

       

    }
}
