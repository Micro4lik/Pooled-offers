using System.Collections.Generic;
using Offers.Configs;
using Offers.Configs.Bundles;
using UnityEngine;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private OffersBundle offersBundle;
        
        [Space, SerializeField] private OffersManager offersManager;
        [SerializeField] private RewardsManager rewardsManager;
        [SerializeField] private UiManager uiManager;
        
        private CurrencyManager currencyManager;
        private IOffersDataParser<List<IOfferConfig>> offersDataParser;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            currencyManager = new CurrencyManager();
            offersDataParser = new ScriptableObjectDataParser();
            rewardsManager.Init(currencyManager);
            offersManager.Init(currencyManager);
            offersManager.CreateOffers(offersDataParser.ParseOfferConfigs(offersBundle.GetAllConfigs()));
            uiManager.UpdateCurrencyText(currencyManager.CurrencyCount);
        }
    }
}