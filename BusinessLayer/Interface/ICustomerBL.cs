using ModelLayer;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICustomerBL
    {
        public CustomerEntity CustomerRegistartion(CustomerRegistrationModel customerRegistrationModel);

        public object GetAllCustomers();

        public bool CustomerUpdate(long CustomerId, CustomerUpdateModel CustomerUpdate);

        public object CustomerUpdate2(long CustomerId, CustomerUpdateModel CustomerUpdate);

        public object DeleteByCustomerId(long CustomerId);





    }
}
