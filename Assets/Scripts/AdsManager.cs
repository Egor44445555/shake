using UnityEngine;
using GamePush;

public class AdsManager : MonoBehaviour
{
    public static AdsManager main;

    void Awake()
    {
        main = this;
    }

    async void Start()
    {
        await GP_Init.Ready;

        GP_Ads.OnFullscreenStart += () => Debug.Log("Реклама началась");
        GP_Ads.OnFullscreenClose += (success) =>
        {
            if (success)
            {
                Debug.Log("Реклама завершена");
            }
            else
            {
                Debug.Log("Реклама пропущена");
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
        print("Plugin ready");
    }

    public void ShowRewarded(string placement = "default")
    {
        print("ShowRewarded");
        GP_Ads.ShowRewarded(placement);
    }

    public void ShowFullscreenAd()
    {
        print("ShowFullscreenAd");
        GP_Ads.ShowFullscreen();
    }

    void OnRewarded(bool success)
    {
        if (success)
        {
            Debug.Log("Награда получена!");
        }
        else
        {
            Debug.Log("Пользователь пропустил рекламу");
        }
    }
    
    void OnRewardedComplete(bool success)
    {
        if (success)
        {
            Debug.Log("Награда получена!");
        }
        else
        {
            Debug.Log("Пользователь пропустил рекламу");
        }
    }
}