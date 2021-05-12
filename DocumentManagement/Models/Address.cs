﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    /// <summary>
    /// Model class representing the address enitity
    /// </summary>
    public class Address
    {
        public int AddressId { get; set; }

        public string Street { get; set; }
       
        public string Number { get; set; }

        public string Apartment { get; set; }
        
        public string City { get; set; }
        
        public string County { get; set; }
       
        public string Country { get; set; }

        public List<Faculty> Faculties { get; set; }
    }
}
