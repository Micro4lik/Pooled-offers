using Currency;
using Rewards;
using UnityEngine;

namespace Offers.Configs
{
    [CreateAssetMenu(menuName = "Rewards/Create currency reward config")]
    public class CurrencyRewardConfig : RewardConfigBase
    {
        [SerializeField] private CurrencyData rewardCurrencyData;

        public override IRewardData GetRewardData()
        {
            return rewardCurrencyData;
        }
    }
}