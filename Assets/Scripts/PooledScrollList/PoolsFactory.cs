using System;
using System.Collections.Generic;
using Offers.Data;
using Offers.View;
using UnityEngine;

namespace PooledScrollList
{
    public class PoolsFactory : MonoBehaviour
    {
        [SerializeField] private int poolsCapacity = 5;
        [SerializeField] private Transform poolsParentTransform;

        [Space, SerializeField] private OfferSpriteView offerSpriteViewPrefab;
        [SerializeField] private OfferTextView offerTextViewPrefab;
        [SerializeField] private OfferDoubleTextView offerDoubleTextViewPrefab;
        [SerializeField] private OfferDoubleSpriteView offerDoubleSpriteViewPrefab;
        [SerializeField] private OfferSpriteTextView offerSpriteTextViewPrefab;
        [SerializeField] private OfferCurrencyView offerCurrencyViewPrefab;
        
        private Pool<OfferSpriteView> spriteOffersPool;
        private Pool<OfferTextView> textOffersPool;
        private Pool<OfferDoubleTextView> doubleTextOffersPool;
        private Pool<OfferDoubleSpriteView> doubleSpriteOffersPool;
        private Pool<OfferSpriteTextView> spriteTextOffersPool;
        private Pool<OfferCurrencyView> currencyOffersPool;

        private Dictionary<Type, Func<OfferViewBase>> poolFactory;

        public void Init()
        {
            spriteOffersPool = new Pool<OfferSpriteView>(offerSpriteViewPrefab, poolsParentTransform, poolsCapacity);
            textOffersPool = new Pool<OfferTextView>(offerTextViewPrefab, poolsParentTransform, poolsCapacity);
            doubleTextOffersPool = new Pool<OfferDoubleTextView>(offerDoubleTextViewPrefab, poolsParentTransform, poolsCapacity);
            doubleSpriteOffersPool = new Pool<OfferDoubleSpriteView>(offerDoubleSpriteViewPrefab, poolsParentTransform, poolsCapacity);
            spriteTextOffersPool = new Pool<OfferSpriteTextView>(offerSpriteTextViewPrefab, poolsParentTransform, poolsCapacity);
            currencyOffersPool = new Pool<OfferCurrencyView>(offerCurrencyViewPrefab, poolsParentTransform, poolsCapacity);

            poolFactory = new Dictionary<Type, Func<OfferViewBase>>
            {
                {typeof(OfferSpriteData), () => spriteOffersPool.GetNext()},
                {typeof(OfferTextData), () => textOffersPool.GetNext()},
                {typeof(OfferDoubleSpriteData), () => doubleSpriteOffersPool.GetNext()},
                {typeof(OfferDoubleTextData), () => doubleTextOffersPool.GetNext()},
                {typeof(OfferSpriteTextData), () => spriteTextOffersPool.GetNext()},
                {typeof(OfferCurrencyData), () => currencyOffersPool.GetNext()}
            };
        }

        public float GetElementSize()
        {
            return offerSpriteViewPrefab.RectTransform.rect.height;
        }

        public OfferViewBase GetNextView(Type offerData)
        {
            OfferViewBase newElement = null;
            
            if (poolFactory.TryGetValue(offerData, out var func))
            {
                newElement = func.Invoke();
                newElement.Init();
            }

            return newElement;
        }

        public void ReturnItemToPool(OfferViewBase item)
        {
            if (item is OfferSpriteView offerSpriteView)
            {
                spriteOffersPool.Return(offerSpriteView);
            }
            else if (item is OfferTextView offerTextView)
            {
                textOffersPool.Return(offerTextView);
            }
            else if (item is OfferSpriteTextView spriteTextView)
            {
                spriteTextOffersPool.Return(spriteTextView);
            }
            else if (item is OfferDoubleTextView offerDoubleTextView)
            {
                doubleTextOffersPool.Return(offerDoubleTextView);
            }
            else if (item is OfferDoubleSpriteView offerDoubleSpriteView)
            {
                doubleSpriteOffersPool.Return(offerDoubleSpriteView);
            }
            else if (item is OfferCurrencyView offerCurrencyView)
            {
                currencyOffersPool.Return(offerCurrencyView);
            }
        }

        public void Dispose()
        {
            spriteOffersPool.Dispose();
            textOffersPool.Dispose();
            spriteTextOffersPool.Dispose();
            doubleTextOffersPool.Dispose();
            doubleSpriteOffersPool.Dispose();
            currencyOffersPool.Dispose();
        }
    }
}