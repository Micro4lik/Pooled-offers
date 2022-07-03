using System;
using Rewards.Bundles;
using UnityEngine;

namespace Offers.Data
{
    [Serializable]
    public class OfferCurrencyData : IOfferData
    {
        [SerializeField] private int price;
        [SerializeField] private string id;
        [SerializeField] private int currencyCount;
        [SerializeField] private RewardsBundle rewardsBundle;

        public int Price => price;
        public string Id => id;
        public int CurrencyCount => currencyCount;
        public IRewardsBundle RewardsBundle => rewardsBundle;
    }
}