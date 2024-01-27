using WarehouseWebMVC.Services.Helper;

namespace WarehouseWebMVC.Services.Helper.Impl
{
    public class AddressHelper : IAddressHelper
    {
        public string ExtractCityProvince(string fullAddress)
        {
            string[] addressParts = fullAddress.Split(',');

            if (addressParts.Length >= 4)
            {
                string city = addressParts[3].Trim();
                string province = addressParts[4].Trim();

                return city + ", " + province;
            }

            return null!;
        }
    }
}
