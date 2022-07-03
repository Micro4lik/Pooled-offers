using Ui;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Text currencyView;

        [SerializeField] private SimpleView notEnoughCurrencyView;
        [SerializeField] private SimpleButtonView acceptPurchaseView;

        public void UpdateCurrencyText(int count)
        {
            currencyView.text = $"{count}$";
        }

        public void ShowNotEnoughCurrencyView()
        {
            notEnoughCurrencyView.OpenView();
        }

        public void ShowAcceptPurchaseView(UnityAction agreeButtonClickAction)
        {
            acceptPurchaseView.OnAgreeButtonClickEvent.RemoveAllListeners();
            acceptPurchaseView.OnAgreeButtonClickEvent.AddListener(agreeButtonClickAction);
            acceptPurchaseView.OpenView();
        }
    }
}