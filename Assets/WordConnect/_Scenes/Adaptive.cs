using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class Adaptive : MonoBehaviour
{
    private BannerView bannerView;

    // Use this for initialization
    void Start()
    {
        RequestBanner();
    }

    public void OnGUI()
    {
        GUI.skin.label.fontSize = 60;
        Rect textOutputRect = new Rect(
          0.15f * Screen.width,
          0.25f * Screen.height,
          0.7f * Screen.width,
          0.3f * Screen.height);
        GUI.Label(textOutputRect, "");
    }

    private void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
            string adUnitId = "ca-app-pub-3908513305207951/7512217767";
#elif UNITY_IPHONE
            string adUnitId = "";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        AdRequest adRequest = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .Build();

        // Load a banner ad.
        this.bannerView.LoadAd(adRequest);
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        MonoBehaviour.print(String.Format("Ad Height: {0}, width: {1}",
            this.bannerView.GetHeightInPixels(),
            this.bannerView.GetWidthInPixels()));
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
                "HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion
}