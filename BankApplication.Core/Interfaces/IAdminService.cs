﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Core.Interfaces
{
    public interface IAdminService
    {
        Task<string> AdminSignInAsync(string username, string enteredPassword);
    }
}
