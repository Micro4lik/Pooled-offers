using UnityEngine;
using UnityEngine.Events;

namespace Ui
{
    public class SimpleButtonView : SimpleView
    {
        [SerializeField] private UnityEvent onAgreeButtonClickEvent;

        public UnityEvent OnAgreeButtonClickEvent
        {
            get => onAgreeButtonClickEvent;
            set => onAgreeButtonClickEvent = value;
        }

        public void OnAgreeButtonClick()
        {
            OnAgreeButtonClickEvent?.Invoke();
        }
    }
}