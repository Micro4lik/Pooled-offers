using System;
using System.Collections.Generic;
using Offers.Data;
using Offers.View;
using UnityEngine;

namespace PooledScrollList.Pool
{
    public class PoolsFactory : MonoBehaviour
    {
        [SerializeField] private OfferViewPool spriteOfferPool;
        [SerializeField] private OfferViewPool textOfferPool;
        [SerializeField] private OfferViewPool doubleTextOfferPool;
        [SerializeField] private OfferViewPool doubleSpriteOfferPool;
        [SerializeField] private OfferViewPool spriteTextOfferPool;
        [SerializeField] private OfferViewPool currencyOfferPool;
        
        private Dictionary<Type, OfferViewPool> poolsFactory;

        public void Init()
        {
            spriteOfferPool.Init();
            textOfferPool.Init();
            doubleTextOfferPool.Init();
            doubleSpriteOfferPool.Init();
            spriteTextOfferPool.Init();
            currencyOfferPool.Init();

            poolsFactory = new Dictionary<Type, OfferViewPool>
            {
                {typeof(OfferSpriteData), spriteOfferPool},
                {typeof(OfferTextData), textOfferPool},
                {typeof(OfferDoubleTextData), doubleTextOfferPool},
                {typeof(OfferDoubleSpriteData), doubleSpriteOfferPool},
                {typeof(OfferSpriteTextData), spriteTextOfferPool},
                {typeof(OfferCurrencyData), currencyOfferPool}
            };
        }

        public float GetElementSize() => spriteOfferPool.GetElementSize();

        public OfferViewBase GetNextView(Type offerData)
        {
            OfferViewBase newElement = null;
            
            if (poolsFactory.TryGetValue(offerData, out var pool))
            {
                newElement = pool.GetPool.GetNext();
                newElement.Init();
            }

            return newElement;
        }

        public void ReturnItemToPool(OfferViewBase item)
        {
            if (poolsFactory.TryGetValue(item.OfferData.GetType(), out var pool))
            {
                pool.GetPool.Return(item);
            }
        }

        public void Dispose()
        {
            spriteOfferPool.GetPool.Dispose();
            textOfferPool.GetPool.Dispose();
            spriteTextOfferPool.GetPool.Dispose();
            doubleTextOfferPool.GetPool.Dispose();
            doubleSpriteOfferPool.GetPool.Dispose();
            currencyOfferPool.GetPool.Dispose();
        }
    }
}