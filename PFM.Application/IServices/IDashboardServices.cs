using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IDashboardServices
    {
        DashboardDTO GetDashboard(int userId);
    }
}
