namespace Warehouse.Infrastructure.Utils.Helper.Impl
{
    public class AddressHelper : IAddressHelper
    {
        public string ExtractCityProvince(string fullAddress)
        {
            string[] addressParts = fullAddress.Split(',');

            int maxIndex = Math.Min(5, addressParts.Length - 1);

            string city = addressParts[maxIndex - 1].Trim();
            string province = addressParts[maxIndex].Trim();

            return $"{city}, {province}";
        }
    }
}
