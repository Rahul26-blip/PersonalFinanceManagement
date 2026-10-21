using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IJWTServices
    {
        string GenerateToken(int userId, string userName,string userCode);
    }
}
