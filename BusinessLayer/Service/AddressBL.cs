using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
   public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public string AddAddress(AddressModel addressModel, int userId)
        {
            try
            {
                return addressRL.AddAddress(addressModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AddressModel> GetAllAddresses(int userId)
        {
            try
            {
                return addressRL.GetAllAddresses(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AddressModel UpdateAddress(AddressModel addressModel, int addressId, int userId)
        {
            try
            {
                return addressRL.UpdateAddress(addressModel,addressId,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteAddress(int addressId, int userId)
        {
            try
            {
                return addressRL.DeleteAddress(addressId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
