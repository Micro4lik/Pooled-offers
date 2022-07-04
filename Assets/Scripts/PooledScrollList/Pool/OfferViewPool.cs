using Offers.View;
using UnityEngine;

namespace PooledScrollList.Pool
{
    public class OfferViewPool : MonoBehaviour
    {
        [SerializeField] protected OfferViewBase viewPrefab;
        [SerializeField] protected int poolCapacity = 5;

        public Pool<OfferViewBase> GetPool { get; private set; }
        public float GetElementSize() => viewPrefab.RectTransform.rect.height;

        public void Init()
        {
            GetPool = new Pool<OfferViewBase>(viewPrefab, transform, poolCapacity);
        }
    }
}