using System.Collections.Generic;
using UnityEngine;

namespace Rewards.Bundles
{
    [CreateAssetMenu(menuName = "Rewards/Bundles/Create rewards bundle")]
    public class RewardsBundle : ScriptableObject, IRewardsBundle
    {
        [SerializeField] private List<RewardConfigBase> configs;

        public List<IRewardConfig> GetAllRewards()
        {
            return new List<IRewardConfig>(configs);
        }
    }
}