using Offers.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Offers.View
{
    public class OfferDoubleSpriteView : OfferViewBase
    {
        [SerializeField] private Image image;
        [SerializeField] private Image secondImage;

        protected override void UpdateView(IOfferData data)
        {
            if (data is OfferDoubleSpriteData doubleSpriteData)
            {
                image.sprite = doubleSpriteData.Sprite;
                secondImage.sprite = doubleSpriteData.SecondSprite;
                base.UpdateView(doubleSpriteData);
            }
        }
    }
}