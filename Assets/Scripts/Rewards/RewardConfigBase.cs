using UnityEngine;

namespace Rewards
{
    public abstract class RewardConfigBase : ScriptableObject, IRewardConfig
    {
        public abstract IRewardData GetRewardData();
    }
}