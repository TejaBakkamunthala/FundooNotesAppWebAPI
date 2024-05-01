using ModelLayer;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICustomerRL
    {

        public CustomerEntity CustomerRegistration(CustomerRegistrationModel customerRegistrationModel);

        public object GetAllCustomers();

        public bool CustomerUpdate(long CustomerId,CustomerUpdateModel CustomerUpdate);

        public object CustomerUpdate2(long CustomerId, CustomerUpdateModel customerUpdate);

        public object DeleteByCustomerId(long CustomerId);


    }
}
