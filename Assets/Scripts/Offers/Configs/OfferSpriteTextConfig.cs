using Offers.Data;
using UnityEngine;

namespace Offers.Configs
{
    [CreateAssetMenu(menuName = "Offers/Create offer sprite text config")]
    public class OfferSpriteTextConfig : OfferConfigBase
    {
        [SerializeField] private OfferSpriteTextData offerSpriteTextData;

        public override IOfferData GetOfferData()
        {
            return offerSpriteTextData;
        }
    }
}