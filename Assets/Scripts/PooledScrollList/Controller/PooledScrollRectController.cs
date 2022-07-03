using System;
using System.Collections.Generic;
using Offers.Data;
using UnityEngine;
using UnityEngine.UI;

namespace PooledScrollList.Controller
{
    public class PooledScrollRectController : PooledScrollRectBase
    {
        private LayoutElement spaceElement;

        public override void Initialize(List<IOfferData> offersData)
        {
            var layoutGroup = scrollRect.content.GetComponent<HorizontalOrVerticalLayoutGroup>();
            if (layoutGroup != null)
            {
                layoutSpacing = layoutGroup.spacing;
                padding = layoutGroup.padding;
                elementSize += layoutSpacing;
            }

            spaceElement = CreateSpaceElement(0f);
            spaceElement.transform.SetParent(scrollRect.content.transform, false);

            base.Initialize(offersData);
        }

        protected override void UpdateContent()
        {
            CalculateAndAdjust(out var elementsVisibleInScrollArea, out var elementsCulledAbove);

            var requiredElementsInList = Mathf.Min(elementsVisibleInScrollArea + 1, TotalElementsCount);

            if (lastElementsCulledAbove != elementsCulledAbove)
            {
                ReorientElement(
                    elementsCulledAbove > lastElementsCulledAbove
                        ? ReorientMethod.TopToBottom
                        : ReorientMethod.BottomToTop, elementsCulledAbove);
            }

            if (NeedInitializeElements(requiredElementsInList))
            {
                InitializeElements(requiredElementsInList, elementsCulledAbove);
            }

            lastElementsCulledAbove = elementsCulledAbove;
        }

        private bool NeedInitializeElements(int requiredElementsInList)
        {
            return activeElements.Count != requiredElementsInList;
        }

        protected override void AdjustSpaceElement(float size)
        {
            if (size <= 0)
            {
                spaceElement.ignoreLayout = true;
            }
            else
            {
                spaceElement.ignoreLayout = false;
                size -= layoutSpacing;
            }

            spaceElement.minHeight = size;
            spaceElement.transform.SetSiblingIndex(0);
        }

        protected override void ReorientElement(ReorientMethod reorientMethod, int elementsCulledAbove)
        {
            if (activeElements.Count <= 1)
            {
                return;
            }

            if (reorientMethod == ReorientMethod.TopToBottom)
            {
                var top = activeElements[0];
                activeElements.RemoveAt(0);
                activeElements.Add(top);

                top.transform.SetSiblingIndex(activeElements[activeElements.Count - 2].transform.GetSiblingIndex() + 1);
                var nextData = data[elementsCulledAbove + activeElements.Count - 1];

                if (top.OfferData.GetType() != nextData.OfferData.GetType())
                {
                    activeElements.Remove(top);
                    ReturnItemToPool(top);
                }

                UpdateElementViewData(top, nextData);
            }
            else
            {
                var bottom = activeElements[activeElements.Count - 1];
                activeElements.RemoveAt(activeElements.Count - 1);
                activeElements.Insert(0, bottom);

                var nextData = data[elementsCulledAbove];

                if (bottom.OfferData.GetType() != nextData.OfferData.GetType())
                {
                    activeElements.Remove(bottom);
                    ReturnItemToPool(bottom);
                }
                else
                {
                    bottom.transform.SetSiblingIndex(activeElements[1].transform.GetSiblingIndex());
                    UpdateElementViewData(bottom, nextData);
                }
            }
        }

        protected override void ForceUpdateElements()
        {
            CalculateAndAdjust(out var elementsVisibleInScrollArea, out var elementsCulledAbove);

            var requiredElementsInList = Mathf.Min(elementsVisibleInScrollArea + 1, TotalElementsCount);

            ReorientElement(
                elementsCulledAbove > lastElementsCulledAbove
                    ? ReorientMethod.TopToBottom
                    : ReorientMethod.BottomToTop, elementsCulledAbove);

            InitializeElements(requiredElementsInList, elementsCulledAbove);
            lastElementsCulledAbove = elementsCulledAbove;
        }

        private void CalculateAndAdjust(out int elementsVisibleInScrollArea, out int elementsCulledAbove)
        {
            AdjustContentSize(elementSize * TotalElementsCount);

            var scrollAreaSize = externalViewPort != null
                ? GetScrollAreaSize(externalViewPort)
                : GetScrollAreaSize(scrollRect.viewport);
            elementsVisibleInScrollArea = Mathf.CeilToInt(scrollAreaSize / elementSize);
            elementsCulledAbove = Mathf.Clamp(
                Mathf.FloorToInt(GetScrollRectNormalizedPosition() *
                                 (TotalElementsCount - elementsVisibleInScrollArea)), 0,
                Mathf.Clamp(TotalElementsCount - (elementsVisibleInScrollArea + 1), 0, int.MaxValue));

            AdjustSpaceElement(elementsCulledAbove * elementSize);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Destroy(spaceElement.gameObject);
        }
    }
}