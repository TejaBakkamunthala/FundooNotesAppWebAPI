using BusinessLayer.Interface;
using ModelLayer;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class CustomerBL : ICustomerBL
    {
        public ICustomerRL icustomerRL;

        public CustomerBL(ICustomerRL icustomerRL)
        {
            this.icustomerRL = icustomerRL;
        }
        public CustomerEntity CustomerRegistartion(CustomerRegistrationModel customerRegistrationModel)
        {
            try
            {
                return icustomerRL.CustomerRegistration(customerRegistrationModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            
        }

        public object GetAllCustomers()
        {
            return icustomerRL.GetAllCustomers();
        }


        public bool CustomerUpdate(long CustomerId,CustomerUpdateModel CustomerUpdate)
        {
            return icustomerRL.CustomerUpdate(CustomerId, CustomerUpdate);
        }


        public object CustomerUpdate2(long  CustomerId,CustomerUpdateModel CustomerUpdate) {
            return icustomerRL.CustomerUpdate2(CustomerId, CustomerUpdate);
        
        }

        public object DeleteByCustomerId(long CustomerId)
        {
            return icustomerRL.DeleteByCustomerId(CustomerId);
        }




    }
}
