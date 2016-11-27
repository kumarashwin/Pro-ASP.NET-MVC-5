using System.Globalization;
using System.Web.Mvc;

namespace MvcModels.Infrastructure {
    public class CountryValueProvider : IValueProvider {
        public bool ContainsPrefix(string prefix) =>
            prefix.ToLower().IndexOf("country") > -1;

        public ValueProviderResult GetValue(string key) =>
            ContainsPrefix(key) ? new ValueProviderResult("USA", "USA", CultureInfo.InvariantCulture) : null;
    }
}