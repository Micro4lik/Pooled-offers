using Offers.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Offers.View
{
    public class OfferSpriteTextView : OfferViewBase
    {
        [SerializeField] private Image image;
        [SerializeField] private Text text;

        protected override void UpdateView(IOfferData data)
        {
            if (data is OfferSpriteTextData spriteTextData)
            {
                image.sprite = spriteTextData.Sprite;
                text.text = spriteTextData.Text;
                base.UpdateView(spriteTextData);
            }
        }
    }
}
