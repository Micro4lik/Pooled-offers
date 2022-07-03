using System.Collections.Generic;
using Offers.Configs;
using Offers.Data;

namespace Managers
{
    public class ScriptableObjectDataParser : IOffersDataParser<List<IOfferConfig>>
    {
        public List<IOfferData> ParseOfferConfigs(List<IOfferConfig> offerConfigs)
        {
            var offersData = new List<IOfferData>();

            foreach (var config in offerConfigs)
            {
                offersData.Add(config.GetOfferData());
            }

            return offersData;
        }
    }
}