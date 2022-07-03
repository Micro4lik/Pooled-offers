using System.Collections.Generic;
using UnityEngine;

namespace Offers.Configs.Bundles
{
    [CreateAssetMenu(menuName = "Offers/Bundles/Create offers bundle")]
    public class OffersBundle : ScriptableObject, IOffersBundle
    {
        [SerializeField] private List<OfferConfigBase> configs;

        public List<IOfferConfig> GetAllConfigs()
        {
            return new List<IOfferConfig>(configs);
        }
    }
}