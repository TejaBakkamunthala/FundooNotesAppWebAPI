using ModelLayer;
using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CustomerRL : ICustomerRL
    {
        private readonly FundooContext fundooContext;

        public CustomerRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }



        public CustomerEntity CustomerRegistration(CustomerRegistrationModel customerRegistrationModel)
        {

            try
            {
                CustomerEntity customerEntity = new CustomerEntity();
                customerEntity.CustomerName = customerRegistrationModel.CustomerName;
                customerEntity.CustomerAddress = customerRegistrationModel.CustomerAddress;
                customerEntity.CustomerCity = customerRegistrationModel.CustomerCity;
                customerEntity.CustomerEmail = customerRegistrationModel.CustomerEmailId;
                customerEntity.CustomerPassword = customerRegistrationModel.CustomerPassword;
                customerEntity.CustomerPhoneNumber = customerRegistrationModel.CustomerPhoneNumber;
                fundooContext.CustomerTablee.Add(customerEntity);
                int result = fundooContext.SaveChanges();

                if (result != 0)
                {
                    return customerEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        
        public object GetAllCustomers()
        {

            var result=fundooContext.CustomerTablee.ToList();

            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public bool CustomerUpdate(long CustomerId,CustomerUpdateModel CustomerUpdate )
        {

            var result = fundooContext.CustomerTablee.FirstOrDefault(customer=>customer.CustomerId==CustomerId);

            if (result != null)
            {
                result.CustomerName = CustomerUpdate.CustomerName;
                result.CustomerAddress = CustomerUpdate.CustomerName;
                result.CustomerCity = CustomerUpdate.CustomerCity;
                result.CustomerEmail = CustomerUpdate.CustomerEmail;
                result.CustomerPassword = CustomerUpdate.CustomerPassword;
                result.CustomerPhoneNumber = CustomerUpdate.CustomerPhoneNumber;

                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }            


        public object CustomerUpdate2(long CustomerId,CustomerUpdateModel customerUpdate)
        {
            var result=fundooContext.CustomerTablee.FirstOrDefault(customer=> customer.CustomerId==CustomerId);

            if (result != null)
            {
                result.CustomerName=customerUpdate.CustomerName;
                result.CustomerAddress=customerUpdate.CustomerAddress;
                result.CustomerCity=customerUpdate.CustomerCity;
                result.CustomerEmail=customerUpdate.CustomerEmail;
                result.CustomerPassword=customerUpdate.CustomerPassword;
                result.CustomerPhoneNumber=customerUpdate.CustomerPhoneNumber;
                fundooContext.SaveChanges();
                return result;
            }
            else
            {
                return false;
            }
            {
                
            }
        }

        public object DeleteByCustomerId(long CustomerId)
        {
            var result=fundooContext.CustomerTablee.FirstOrDefault(customer=>customer.CustomerId==CustomerId);

            if (result != null)
            {
               
                fundooContext.Remove(result);
                fundooContext.SaveChanges();
                return result;
            }
            else
            {
                return false;
            }
        }
        


       
    }
}
