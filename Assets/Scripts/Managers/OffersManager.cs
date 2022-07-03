using System.Collections.Generic;
using Offers.Data;
using PooledScrollList.Controller;
using PooledScrollList.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class OffersManager : MonoBehaviour
    {
        [SerializeField] private PooledScrollRectController pooledScrollRectController;
        [SerializeField] private UiManager uiManager;
        
        private CurrencyManager currencyManager;

        public event UnityAction<IOfferData> OnConfirmPurchaseAction;

        public void Init(CurrencyManager currencyManager)
        {
            this.currencyManager = currencyManager;
            pooledScrollRectController.OnViewClickEvent += OnClickOffer;
        }

        public void CreateOffers(List<IOfferData> offerData)
        {
            pooledScrollRectController.Initialize(offerData);
        }

        private void OnClickOffer(PooledData pooledData)
        {
            if (!currencyManager.CanSpend(pooledData.OfferData.Price))
            {
                uiManager.ShowNotEnoughCurrencyView();
                return;
            }

            uiManager.ShowAcceptPurchaseView(() =>
            {
                currencyManager.Spend(pooledData.OfferData.Price);
                pooledScrollRectController.Remove(pooledData);
                uiManager.UpdateCurrencyText(currencyManager.CurrencyCount);
            });
            
            OnConfirmPurchaseAction?.Invoke(pooledData.OfferData);
        }

        private void OnDestroy()
        {
            pooledScrollRectController.OnViewClickEvent -= OnClickOffer;
        }
    }
}