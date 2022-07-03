using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Ui
{
    public class SimpleView : MonoBehaviour, IView
    {
        [SerializeField] protected RectTransform mainPanel;

        [SerializeField] private float startScale = 1f;

        [SerializeField] protected float openScaleDuration = 0.2f;
        [SerializeField] protected Ease openScaleEase = Ease.OutBack;
        [SerializeField] protected float closeScaleDuration = 0.2f;
        [SerializeField] protected Ease closeScaleEase = Ease.InBack;

        [SerializeField] private UnityEvent onOpen;
        [SerializeField] private UnityEvent onClose;

        private Sequence openAnimation;
        private Sequence closeAnimation;

        private bool isActive;

        public virtual void OpenView()
        {
            if (openAnimation != null && openAnimation.IsPlaying())
            {
                return;
            }

            gameObject.SetActive(true);
            OpenViewAnimation().Play();

            isActive = true;
            onOpen?.Invoke();
        }

        public virtual void CloseView()
        {
            if (!isActive)
            {
                return;
            }

            if (closeAnimation != null && closeAnimation.IsPlaying())
            {
                return;
            }

            CloseViewAnimation().OnKill(() =>
            {
                isActive = false;
                gameObject.SetActive(false);
                onClose?.Invoke();
            }).Play();
        }

        protected virtual Sequence OpenViewAnimation()
        {
            openAnimation = DOTween.Sequence().Append(mainPanel.DOScale(startScale, openScaleDuration)
                .From(Vector3.zero).SetEase(openScaleEase));

            return openAnimation;
        }

        protected virtual Sequence CloseViewAnimation()
        {
            closeAnimation = DOTween.Sequence().Append(mainPanel.DOScale(Vector3.zero, closeScaleDuration)
                .From(startScale).SetEase(closeScaleEase));

            return closeAnimation;
        }

        private void OnDestroy()
        {
            onOpen.RemoveAllListeners();
            onClose.RemoveAllListeners();
        }
    }
}