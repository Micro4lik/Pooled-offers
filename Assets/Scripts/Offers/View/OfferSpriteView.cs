using Offers.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Offers.View
{
    public class OfferSpriteView : OfferViewBase
    {
        [SerializeField] private Image image;

        protected override void UpdateView(IOfferData data)
        {
            if (data is OfferSpriteData spriteData)
            {
                image.sprite = spriteData.Sprite;
                base.UpdateView(spriteData);
            }
        }
    }
}