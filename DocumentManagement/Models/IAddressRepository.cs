using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public interface IAddressRepository
    {
        public IEnumerable<Address> AllAddresses { get; }
    }
}
