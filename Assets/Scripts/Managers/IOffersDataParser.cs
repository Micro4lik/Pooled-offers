using System.Collections.Generic;
using Offers.Data;

namespace Managers
{
    public interface IOffersDataParser<T>
    {
        public List<IOfferData> ParseOfferConfigs(T data);
    }
}