using Offers.Data;
using UnityEngine;

namespace Offers.Configs
{
    [CreateAssetMenu(menuName = "Offers/Create offer text config")]
    public class OfferTextConfig : OfferConfigBase
    {
        [SerializeField] private OfferTextData offerTextData;

        public override IOfferData GetOfferData()
        {
            return offerTextData;
        }
    }
}