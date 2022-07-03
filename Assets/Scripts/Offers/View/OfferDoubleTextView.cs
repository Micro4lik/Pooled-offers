using Offers.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Offers.View
{
    public class OfferDoubleTextView : OfferViewBase
    {
        [SerializeField] private Text text;
        [SerializeField] private Text secondText;

        protected override void UpdateView(IOfferData data)
        {
            if (data is OfferDoubleTextData doubleTextData)
            {
                text.text = doubleTextData.Text;
                secondText.text = doubleTextData.SecondText;
                base.UpdateView(doubleTextData);
            }
        }
    }
}