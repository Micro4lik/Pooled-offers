using System;
using Rewards.Bundles;
using UnityEngine;

namespace Offers.Data
{
    [Serializable]
    public class OfferSpriteData : IOfferData
    {
        [SerializeField] private int price;
        [SerializeField] private string id;
        [SerializeField] private Sprite sprite;
        [SerializeField] private RewardsBundle rewardsBundle;
    
        public int Price => price;
        public string Id => id;
        public Sprite Sprite => sprite;

        public OfferSpriteData(int price, Sprite sprite)
        {
            this.price = price;
            this.sprite = sprite;
        }
        public IRewardsBundle RewardsBundle => rewardsBundle;
    }
}