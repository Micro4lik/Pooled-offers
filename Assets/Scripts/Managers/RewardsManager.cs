using Currency;
using Offers.Data;
using UnityEngine;

namespace Managers
{
    public class RewardsManager : MonoBehaviour
    {
        [SerializeField] private OffersManager offersManager;

        private CurrencyManager currencyManager;
        
        public void Init(CurrencyManager currencyManager)
        {
            this.currencyManager = currencyManager;
            offersManager.OnConfirmPurchaseAction += EarnReward;
        }

        private void EarnReward(IOfferData offerData)
        {
            var rewards = offerData.RewardsBundle?.GetAllRewards();

            if (rewards == null || rewards.Count <= 0)
            {
                return;
            }
            
            foreach (var reward in rewards)
            {
                var rewardData = reward.GetRewardData();
                
                if (rewardData != null && reward.GetRewardData() is CurrencyData)
                {
                    currencyManager.Earn(reward.GetRewardData().Count);
                }
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            offersManager.OnConfirmPurchaseAction -= EarnReward;
        }
    }
}
