using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADManager : MonoBehaviour
{
    private string APP_ID = "ca-app-pub-8911531474291395~6625826096";

    private BannerView bannerAD;
    private InterstitialAd interstitialAD;
    void Start()
    {
        //PUBLISHING
        MobileAds.Initialize(APP_ID);

        RequestBanner();
        RequestInterstitial();
        OnEnable();
    }

    void RequestBanner()
    {
        //TEST
        //string banner_ID = "ca-app-pub-3940256099942544/6300978111";
        //PUBLISHING
        string banner_ID = "ca-app-pub-8911531474291395/3691421438";

        bannerAD = new BannerView(banner_ID, AdSize.SmartBanner, AdPosition.Bottom);

        //PUBLISHING
        AdRequest adRequest = new AdRequest.Builder().Build();
        //Test
        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        bannerAD.LoadAd(adRequest);
    }

    void RequestInterstitial()
    {
        //TEST
        //string interstitial_ID = "ca-app-pub-3940256099942544/1033173712";
        //PUBLISHING
        string interstitial_ID = "ca-app-pub-8911531474291395/9017568606";

        interstitialAD = new InterstitialAd(interstitial_ID);

        //PUBLISHING
        AdRequest adRequest = new AdRequest.Builder().Build();
        //Test
        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        interstitialAD.LoadAd(adRequest);
    }

    public void Display_Banner()
    {
        bannerAD.Show();
    }

    public void Display_Interstitial()
    {
        if (interstitialAD.IsLoaded())
        {
            interstitialAD.Show();
        }
    }
    //HANDLE EVENTS
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Display_Banner();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
        RequestInterstitial();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    void HandleBannerADEvents(bool subscribe)
    {
        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            this.bannerAD.OnAdLoaded += this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.bannerAD.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            this.bannerAD.OnAdOpening += this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            this.bannerAD.OnAdClosed += this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.bannerAD.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            this.bannerAD.OnAdLoaded -= this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.bannerAD.OnAdFailedToLoad -= this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            this.bannerAD.OnAdOpening -= this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            this.bannerAD.OnAdClosed -= this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.bannerAD.OnAdLeavingApplication -= this.HandleOnAdLeavingApplication;
        }
    }
    void OnEnable()
    {
        HandleBannerADEvents(true);
    }
    void OnDisable()
    {
        HandleBannerADEvents(false);
    }
}
