using System;
using Rewards.Bundles;
using UnityEngine;

namespace Offers.Data
{
    [Serializable]
    public class OfferDoubleSpriteData : IOfferData
    {
        [SerializeField] private int price;
        [SerializeField] private string id;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Sprite secondSprite;
        [SerializeField] private RewardsBundle rewardsBundle;
    
        public int Price => price;
        public string Id => id;
        public Sprite Sprite => sprite;
        public Sprite SecondSprite => secondSprite;
        public IRewardsBundle RewardsBundle => rewardsBundle;
    }
}