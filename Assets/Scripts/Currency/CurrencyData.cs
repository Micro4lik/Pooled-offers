using System;
using Rewards;
using UnityEngine;

namespace Currency
{
    [Serializable]
    public class CurrencyData : IRewardData
    {
        [SerializeField] private int count;
        [SerializeField] private string id;

        public int Count => count;
        public string Id => id;
    }
}