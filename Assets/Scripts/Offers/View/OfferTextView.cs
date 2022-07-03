using Offers.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Offers.View
{
    public class OfferTextView : OfferViewBase
    {
        [SerializeField] private Text text;

        protected override void UpdateView(IOfferData data)
        {
            if (data is OfferTextData textData)
            {
                text.text = textData.Text;
                base.UpdateView(textData);
            }
        }
    }
}