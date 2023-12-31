﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Dtos.Users
{
    public class UserInListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public DateTime? StatusExpirationDate { get; set; }

        public decimal MoneySpent { get; set; }
    }
}
