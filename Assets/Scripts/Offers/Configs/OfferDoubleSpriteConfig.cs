using Offers.Data;
using UnityEngine;

namespace Offers.Configs
{
    [CreateAssetMenu(menuName = "Offers/Create offer double sprite config")]
    public class OfferDoubleSpriteConfig : OfferConfigBase
    {
        [SerializeField] private OfferDoubleSpriteData offerDoubleSpriteData;

        public override IOfferData GetOfferData()
        {
            return offerDoubleSpriteData;
        }
    }
}