using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public string AddAddress(AddressModel addressModel, int userId);
        public List<AddressModel> GetAllAddresses(int userId);
        public AddressModel UpdateAddress(AddressModel addressModel, int addressId, int userId);
        public bool DeleteAddress(int addressId, int userId);
    }
}
