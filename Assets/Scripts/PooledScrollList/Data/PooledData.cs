using System;
using Offers.Data;

namespace PooledScrollList.Data
{
    [Serializable]
    public class PooledData
    {
        public int Id { get; }
        public IOfferData OfferData { get; }

        public PooledData(int id, IOfferData offerData)
        {
            Id = id;
            OfferData = offerData;
        }
    }
}