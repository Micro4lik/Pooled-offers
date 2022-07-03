using Offers.Data;
using UnityEngine;

namespace Offers.Configs
{
    [CreateAssetMenu(menuName = "Offers/Create offer currency config")]
    public class OfferCurrencyConfig : OfferConfigBase
    {
        [SerializeField] private OfferCurrencyData offerCurrencyData;

        public override IOfferData GetOfferData()
        {
            return offerCurrencyData;
        }
    }
}