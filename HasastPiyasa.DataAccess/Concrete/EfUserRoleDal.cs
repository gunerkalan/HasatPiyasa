﻿using HasastPiyasa.DataAccess.Abstract;
using HasatPiyasa.Entity.DataAccess.EntityFrameworkCore;
using HasatPiyasa.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HasastPiyasa.DataAccess.Concrete
{
    public class EfUserRoleDal : EfEntityRepositoryBase<UserRoles>, IUserRoleDal
    {

    }
}
