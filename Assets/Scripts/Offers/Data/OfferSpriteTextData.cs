using System;
using Rewards.Bundles;
using UnityEngine;

namespace Offers.Data
{
    [Serializable]
    public class OfferSpriteTextData : IOfferData
    {        
        [SerializeField] private int price;
        [SerializeField] private string id;
        [SerializeField] private Sprite sprite;
        [SerializeField] private string text;
        [SerializeField] private RewardsBundle rewardsBundle;
    
        public int Price => price;
        public string Id => id;
        public string Text => text;
        public Sprite Sprite => sprite;
        public IRewardsBundle RewardsBundle => rewardsBundle;
    }
}
