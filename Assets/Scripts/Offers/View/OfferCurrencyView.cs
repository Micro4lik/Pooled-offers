using Offers.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Offers.View
{
    public class OfferCurrencyView : OfferViewBase
    {
        [SerializeField] private Text currencyText;

        protected override void UpdateView(IOfferData data)
        {
            if (data is OfferCurrencyData currencyData)
            {
                currencyText.text = $"{currencyData.CurrencyCount}$";
                base.UpdateView(currencyData);
            }
        }
    }
}