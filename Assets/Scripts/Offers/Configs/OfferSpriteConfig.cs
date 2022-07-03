using Offers.Data;
using UnityEngine;

namespace Offers.Configs
{
    [CreateAssetMenu(menuName = "Offers/Create offer sprite config")]
    public class OfferSpriteConfig : OfferConfigBase
    {
        [SerializeField] private OfferSpriteData offerSpriteData;

        public override IOfferData GetOfferData()
        {
            return offerSpriteData;
        }
    }
}