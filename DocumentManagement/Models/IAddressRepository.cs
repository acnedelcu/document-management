﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class IAddressRepository
    {
        public IEnumerable<Address> Addresses { get; }
    }
}
