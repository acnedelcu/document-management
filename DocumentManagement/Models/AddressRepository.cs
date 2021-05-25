using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Models
{
    public class AddressRepository
    {
        private readonly AppDbContext appDbContext;

        public AddressRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Address> AllAddresses
        {
            get
            {
                return this.appDbContext.Addresses;
            }
        }
    }
}
