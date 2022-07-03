using Offers.Data;
using UnityEngine;

namespace Offers.Configs
{
    [CreateAssetMenu(menuName = "Offers/Create offer double text config")]
    public class OfferDoubleTextConfig : OfferConfigBase
    {
        [SerializeField] private OfferDoubleTextData offerDoubleTextData;

        public override IOfferData GetOfferData()
        {
            return offerDoubleTextData;
        }
    }
}