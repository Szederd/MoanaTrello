﻿using MoanaTrello.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoanaTrello.Services
{
    public interface ILoginService
    {
        bool Login(LoginRequest loginRequest);
    }
}