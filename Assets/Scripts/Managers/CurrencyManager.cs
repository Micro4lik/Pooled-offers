using UnityEngine;

namespace Managers
{
    public class CurrencyManager
    {
        private int currencyCount = 5;
        public int CurrencyCount => currencyCount;

        public bool CanSpend(int price)
        {
            if (currencyCount < price)
            {
                return false;
            }

            return true;
        }

        public void Spend(int price)
        {
            price = Mathf.Max(0, price);
            currencyCount -= price;
        }

        public void Earn(int earnCount)
        {
            currencyCount += earnCount;
        }
    }
}