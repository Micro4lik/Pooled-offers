using System.Collections.Generic;
using System.Linq;
using Offers.Data;
using Offers.View;
using PooledScrollList.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PooledScrollList.Controller
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class PooledScrollRectBase : MonoBehaviour
    {
        protected enum ReorientMethod
        {
            TopToBottom,
            BottomToTop
        }

        protected readonly List<OfferViewBase> activeElements = new List<OfferViewBase>();
        protected readonly List<PooledData> data = new List<PooledData>();

        protected float layoutSpacing;
        protected RectOffset padding;
        protected float elementSize;
        protected int lastElementsCulledAbove = -1;

        [SerializeField] protected PoolsFactory poolsFactory;

        [SerializeField] protected ScrollRect scrollRect;
        [SerializeField] protected RectTransform externalViewPort;

        protected int TotalElementsCount => data.Count;
        public event UnityAction<PooledData> OnViewClickEvent;

        public virtual void Initialize(List<IOfferData> offersData)
        {
            poolsFactory.Init();

            scrollRect.onValueChanged.AddListener(ScrollMoved);
            elementSize = poolsFactory.GetElementSize();

            var pooledData = new List<PooledData>();

            for (var i = 0; i < offersData.Count; i++)
            {
                var offerData = offersData[i];
                var newData = new PooledData(i + 1, offerData);
                pooledData.Add(newData);
            }

            this.data.Clear();
            this.data.AddRange(pooledData);
            Initialize();
        }

        private void Initialize()
        {
            lastElementsCulledAbove = -1;
            ResetPosition();
            UpdateContent();
            UpdateActiveElements();
        }

        public void Remove(PooledData item)
        {
            data.Remove(item);
            ForceUpdateElements();
        }

        protected abstract void UpdateContent();
        protected abstract void AdjustSpaceElement(float size);
        protected abstract void ReorientElement(ReorientMethod reorientMethod, int elementsCulledAbove);
        protected abstract void ForceUpdateElements();

        protected virtual void UpdateActiveElements()
        {
            for (var i = 0; i < activeElements.Count; i++)
            {
                var activeElement = activeElements[i];
                var activeData = data[lastElementsCulledAbove + i];

                if (activeElement.OfferData.GetType() != activeData.OfferData.GetType())
                {
                    UpdateElementViewData(activeElement, activeData);
                }
            }
        }

        protected void ReturnItemToPool(OfferViewBase item)
        {
            poolsFactory.ReturnItemToPool(item);
        }

        protected void InitializeElements(int requiredElementsInList, int numElementsCulledAbove)
        {
            for (var i = 0; i < activeElements.Count; i++)
            {
                if (activeElements[i])
                {
                    ReturnItemToPool(activeElements[i]);
                }
            }

            activeElements.Clear();

            for (var i = 0; i < requiredElementsInList && i + numElementsCulledAbove < TotalElementsCount; i++)
            {
                activeElements.Add(CreateElement(i + numElementsCulledAbove));
            }
        }

        protected virtual void OnElementCLick(IOfferData offerData)
        {
            var pooledData = data.FirstOrDefault(d => d.OfferData.Id.Equals(offerData.Id));
            OnViewClickEvent?.Invoke(pooledData);
        }

        protected virtual OfferViewBase CreateElement(int index)
        {
            var newElement = poolsFactory.GetNextView(data[index].OfferData.GetType());

            newElement.transform.SetParent(scrollRect.content, false);
            newElement.transform.SetSiblingIndex(index);
            UpdateElementViewData(newElement, data[index]);

            newElement.OnClickEvent.RemoveAllListeners();
            newElement.OnClickEvent.AddListener(OnElementCLick);

            return newElement;
        }

        protected void UpdateElementViewData(OfferViewBase view, PooledData pooledData)
        {
            view.SetData(pooledData.OfferData);
            view.UpdateSortingId(pooledData.Id);
        }

        protected void AdjustContentSize(float size)
        {
            var currentSize = scrollRect.content.sizeDelta;
            size -= layoutSpacing;

            if (padding != null)
            {
                size += padding.top + padding.bottom;
            }

            currentSize.y = size;
            scrollRect.content.sizeDelta = currentSize;
        }

        protected float GetScrollAreaSize(RectTransform viewPort)
        {
            return viewPort.rect.height;
        }

        protected virtual void ResetPosition()
        {
            if (scrollRect.vertical)
            {
                scrollRect.verticalNormalizedPosition = 1f;
            }
            else
            {
                scrollRect.horizontalNormalizedPosition = 0f;
            }
        }

        protected float GetScrollRectNormalizedPosition()
        {
            return Mathf.Clamp01(1 - scrollRect.verticalNormalizedPosition);
        }

        protected LayoutElement CreateSpaceElement(float elementSize)
        {
            var spaceElement = new GameObject("SpaceElement").AddComponent<LayoutElement>();
            spaceElement.minHeight = elementSize;
            return spaceElement;
        }

        private void ScrollMoved(Vector2 delta)
        {
            UpdateContent();
            UpdateActiveElements();
        }

        protected virtual void OnDestroy()
        {
            scrollRect.onValueChanged.RemoveListener(ScrollMoved);

            for (var i = 0; i < activeElements.Count; i++)
            {
                poolsFactory.ReturnItemToPool(activeElements[i]);
            }

            poolsFactory.Dispose();
            activeElements.Clear();
        }
    }
}