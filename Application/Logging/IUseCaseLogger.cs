﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Logging
{
    public interface IUseCaseLogger
    {
        void Log(UseCaseLog log);
    }

}
