using System.Collections.Generic;

namespace Rewards.Bundles
{
    public interface IRewardsBundle
    {
        List<IRewardConfig> GetAllRewards();
    }
}