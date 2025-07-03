using UnityEngine;
using GamePush;

public class AdsManager : MonoBehaviour
{
    async void Start()
    {
        await GP_Init.Ready;
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

    void ShowRewarded(string idOrTag)
    {
        GP_Ads.ShowRewarded(idOrTag);
    }
    
    void OnRewarded()
    {
        print("OnRewarded");
    }
}