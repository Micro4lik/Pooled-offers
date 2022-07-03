using Offers.Data;
using UnityEngine;

namespace Offers.Configs
{
    public abstract class OfferConfigBase : ScriptableObject, IOfferConfig
    {
        public abstract IOfferData GetOfferData();
    }
}