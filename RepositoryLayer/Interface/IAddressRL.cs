using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public string AddAddress(AddressModel addressModel, int userId);
        public List<AddressModel> GetAllAddresses(int userId);
        public AddressModel UpdateAddress(AddressModel addressModel, int addressId, int userId);
        public bool DeleteAddress(int addressId, int userId);
    }
}
