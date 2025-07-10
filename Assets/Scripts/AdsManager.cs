using UnityEngine;
using GamePush;

public class AdsManager : MonoBehaviour
{
    public static AdsManager main;
    [HideInInspector] public bool rewardedSuccess = false;

    void Awake()
    {
        main = this;
    }

    async void Start()
    {
        await GP_Init.Ready;

        GP_Ads.OnRewardedStart += () => Debug.Log("Rewarded реклама началась");
        GP_Ads.OnFullscreenStart += () => Debug.Log("Реклама началась");

        GP_Ads.OnRewardedClose += (success) => OnRewarded(success);

        GP_Ads.OnFullscreenClose += (success) =>
        {
            if (success)
            {
                Debug.Log("Advertising completed");
            }
            else
            {
                Debug.Log("Advertising skipped");
            }
        };

        OnPluginReady();
    }

    void CheckReady()
    {
        if (GP_Init.isReady)
        {
            OnPluginReady();
        }
    }

    void OnPluginReady()
    {
        // print("Plugin ready");
    }

    public void ShowRewarded(string placement = "default")
    {
        GP_Ads.ShowRewarded(placement);
    }

    public void ShowFullscreenAd()
    {
        GP_Ads.ShowFullscreen();
    }

    void OnRewarded(bool success)
    {
        if (success)
        {
            rewardedSuccess = true;
            Debug.Log("Награда получена!");
        }
        else
        {
            rewardedSuccess = false;
            Debug.Log("Пользователь пропустил рекламу");
        }
    }
}