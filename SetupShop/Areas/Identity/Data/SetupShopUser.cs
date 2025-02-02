﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SetupShop.Models;

namespace SetupShop.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SetupShopUser class
public class SetupShopUser : IdentityUser
{
    public string DisplayName { get; set; }

    public ICollection<UserSetup> UserSetups { get; set; }
}

