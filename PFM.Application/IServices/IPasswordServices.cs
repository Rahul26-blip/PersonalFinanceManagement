using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IPasswordServices
    {
        string HashPassword(string password);

        bool VerifyPassword(string password,string passwordHash);
    }
}
