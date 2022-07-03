using Offers.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Offers.View
{
    public abstract class OfferViewBase : MonoBehaviour
    {
        [SerializeField] private Button purchaseButton;
        [SerializeField] private Text priceText;
        [SerializeField] private Text sortingId;

        private RectTransform rectTransform;
        private bool isInit;

        public IOfferData OfferData { get; private set; }

        public RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                {
                    rectTransform = GetComponent<RectTransform>();
                }

                return rectTransform;
            }
        }

        public UnityEvent<IOfferData> OnClickEvent { get; private set; }

        public virtual void Init()
        {
            if (!isInit)
            {
                isInit = true;
                OnClickEvent = new UnityEvent<IOfferData>();
                BindPurchaseButtonClick();
            }
        }

        private void BindPurchaseButtonClick()
        {
            purchaseButton.onClick.AddListener(OnClick);
        }

        protected virtual void OnClick()
        {
            OnClickEvent?.Invoke(OfferData);
        }

        public void SetData(IOfferData data)
        {
            OfferData = data;
            UpdateView(data);
        }

        protected virtual void UpdateView(IOfferData data)
        {
            UpdatePrice(data.Price);
        }

        private void UpdatePrice(int price)
        {
            priceText.text = price <= 0 ? "FREE!" : $"{price}$";
        }

        public void UpdateSortingId(int id)
        {
            sortingId.text = id.ToString();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            OnClickEvent?.RemoveAllListeners();
        }
    }
}