using System.Collections.Generic;

namespace Offers.Configs.Bundles
{
    public interface IOffersBundle
    {
        List<IOfferConfig> GetAllConfigs();
    }
}