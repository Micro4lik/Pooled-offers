using Rewards.Bundles;

namespace Offers.Data
{
    public interface IOfferData
    {
        int Price { get; }
        string Id { get; }
        IRewardsBundle RewardsBundle { get; }
    }
}
