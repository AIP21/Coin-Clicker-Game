using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveLoad : MonoBehaviour
{
    #region References
    private MainGame Manager;
    private Settings _Settings;
    public UIAnim UIAnimator;
    #endregion

    #region Away screen
    [Header("Away screen")]
    public GameObject AwayScreen;
    public GameObject AwayScreenBlocker;
    public Text AwayCoinsText;
    #endregion

    #region Variables
    [Header("Variables")]
    public bool DoSaveAndLoad = true;
    #endregion

    private void Awake()
    {
        // Assign References
        print("Finding load script references");
        Manager = GetComponent<MainGame>();
        _Settings = GetComponent<Settings>();

        if (DoSaveAndLoad)
        {
            // Load Settings
            print("Loading settings save data");
            if (PlayerPrefs.HasKey("fancyGraphics"))
                _Settings.GraphicsChange(PlayerPrefs.GetInt("fancyGraphics") == 1 ? true : false);
            else
                _Settings.GraphicsChange(true);

            if (PlayerPrefs.HasKey("masterVol"))
                _Settings.LoadVolValue(PlayerPrefs.GetInt("masterVol"));

            // Load Main Game
            print("Loading player save data");
            if (PlayerPrefs.HasKey("coins"))
                Manager.coins = double.Parse(PlayerPrefs.GetString("coins"));
            if (PlayerPrefs.HasKey("totalCoins"))
                Manager.totalCoins = double.Parse(PlayerPrefs.GetString("totalCoins"));
            if (PlayerPrefs.HasKey("totalClicks"))
                Manager.totalClicks = double.Parse(PlayerPrefs.GetString("totalClicks"));
            if (PlayerPrefs.HasKey("secondsPlayed"))
                Manager.timePlayed = double.Parse(PlayerPrefs.GetString("secondsPlayed"));
            if (PlayerPrefs.HasKey("coinsPerSecond"))
                Manager.coinsPerSecond = double.Parse(PlayerPrefs.GetString("coinsPerSecond"));
            if (PlayerPrefs.HasKey("multiplier"))
                Manager.multiplier = double.Parse(PlayerPrefs.GetString("multiplier"));
            if (PlayerPrefs.HasKey("levelCPC"))
                Manager.levelCPC = double.Parse(PlayerPrefs.GetString("levelCPC"));
            if (PlayerPrefs.HasKey("levelCPS"))
                Manager.levelCPS = double.Parse(PlayerPrefs.GetString("levelCPS"));

            if (PlayerPrefs.HasKey("coinLevel") == false)
                Manager.coinLevel = 1;
            else
                Manager.coinLevel = double.Parse(PlayerPrefs.GetString("coinLevel"));

            if (PlayerPrefs.HasKey("coinsPerClick") == false)
                Manager.coinsPerClick = 1;
            else
                Manager.coinsPerClick = double.Parse(PlayerPrefs.GetString("coinsPerClick"));

            if (PlayerPrefs.HasKey("MultCost") == false)
                Manager.MultCost = 100;
            else
                Manager.MultCost = double.Parse(PlayerPrefs.GetString("MultCost"));

            if (PlayerPrefs.HasKey("CPCCost") == false)
                Manager.CPCCost = 10;
            else
                Manager.CPCCost = double.Parse(PlayerPrefs.GetString("CPCCost"));

            if (PlayerPrefs.HasKey("CPSCost") == false)
                Manager.CPSCost = 50;
            else
                Manager.CPSCost = double.Parse(PlayerPrefs.GetString("CPSCost"));

            if (PlayerPrefs.HasKey("CLvlCost") == false)
                Manager.CLvlCost = 250;
            else
                Manager.CLvlCost = double.Parse(PlayerPrefs.GetString("CLvlCost"));

            // Prestige stuff
            if (PlayerPrefs.HasKey("PrestigeCost") == false)
                Manager.PrestigeCost = 5000000;
            else
                Manager.PrestigeCost = double.Parse(PlayerPrefs.GetString("PrestigeCost"));

            if (PlayerPrefs.HasKey("PrestigeCount"))
                Manager.prestigeCount = double.Parse(PlayerPrefs.GetString("PrestigeCount"));
            if (PlayerPrefs.HasKey("PrestigeBonus"))
                Manager.prestigeBonus = double.Parse(PlayerPrefs.GetString("PrestigeBonus"));

            // Do away calculation
            if (PlayerPrefs.HasKey("logoffTime") && Manager.coinsPerSecond > 0)
            {
                DateTime currTime = DateTime.Now;
                DateTime logoffTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("logoffTime")));
                TimeSpan diffTime = currTime.Subtract(logoffTime);
                double secondsPassed = diffTime.TotalSeconds;
                if (secondsPassed >= 300)
                {
                    double coinsGained = secondsPassed * ((Manager.coinsPerSecond + Manager.coinLevel) / 50);
                    coinsGained = Math.Round(coinsGained);

                    // Show away screen
                    AwayScreenBlocker.SetActive(true);
                    AwayScreen.SetActive(true);
                    if (_Settings.FancyGraphics)
                        UIAnimator.UIBlurIn("AwayScreenBlocker*(0.3215686,0.3215686,0.3215686,7.5)*0.4*0.25");
                    else
                        UIAnimator.UIFadeIn("AwayScreenBlocker*1*0.4*0.25");
                    UIAnimator.UIScaleIn("AwayScreen*(1,1,1)*0.5*0.25");
                    AwayCoinsText.text = coinsGained.ToString() + " coins";

                    print("logoffTime: " + logoffTime.ToString() + "    currTime: " + currTime.ToString() + "   diffTime: " + diffTime.ToString() + "   secondsPassed: " + secondsPassed + " coinsGained: " + coinsGained);

                    Manager.coins += coinsGained;
                }
            }

            if (PlayerPrefs.HasKey("ColorCoinR") && Manager.coinLevel > 1)
            {
                print("Loading coin color");
                Manager.ColorCoin = new UnityEngine.Color(PlayerPrefs.GetFloat("ColorCoinR"), PlayerPrefs.GetFloat("ColorCoinG"), PlayerPrefs.GetFloat("ColorCoinB"));
                Manager.ImageCLvl.color = Manager.ColorCoin;
                Manager.ImageBackground.color = Manager.ColorCoin / 2;
                var rend = Manager.CoinBurst.GetComponent<ParticleSystemRenderer>();
                rend.material.color = Manager.ColorCoin * 0.8f;
            }
            print("Finished loading save data");
        }
    }

    public void HideAwayScreen()
    {
        if (_Settings.FancyGraphics)
            UIAnimator.UIBlurOut("AwayScreenBlocker*(1,1,1,0)*0.5");
        else
            UIAnimator.UIFadeOut("AwayScreenBlocker*0*0.5");
        UIAnimator.UIScaleOut("AwayScreen*(-1,-1,-1)*0.5");
    }

    bool saveStuff = true;

    public void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        saveStuff = false;
        print("Successfully reset all save data");
        Application.Quit();
    }

    public void OnApplicationQuit() // SAVE
    {
        if (saveStuff && DoSaveAndLoad)
        {
            // Save Settings
            print("Saving player settings");
            PlayerPrefs.SetInt("fancyGraphics", _Settings.FancyGraphics == true ? 1 : 0);
            PlayerPrefs.SetInt("masterVol", _Settings.MasterVolume);

            // Save Logoff Time
            print("timeToSave: " + System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("logoffTime", System.DateTime.Now.ToBinary().ToString());

            // Save Main Game
            print("Saving player data");
            PlayerPrefs.SetString("coins", Manager.coins.ToString());
            PlayerPrefs.SetString("totalCoins", Manager.totalCoins.ToString());
            PlayerPrefs.SetString("totalClicks", Manager.totalClicks.ToString());
            PlayerPrefs.SetString("secondsPlayed", Manager.timePlayed.ToString());
            PlayerPrefs.SetString("coinLevel", Manager.coinLevel.ToString());
            PlayerPrefs.SetString("multiplier", Manager.multiplier.ToString());
            PlayerPrefs.SetString("coinsPerClick", Manager.coinsPerClick.ToString());
            PlayerPrefs.SetString("coinsPerSecond", Manager.coinsPerSecond.ToString());
            PlayerPrefs.SetString("MultCost", Manager.MultCost.ToString());
            PlayerPrefs.SetString("CPCCost", Manager.CPCCost.ToString());
            PlayerPrefs.SetString("CPSCost", Manager.CPSCost.ToString());
            PlayerPrefs.SetString("CLvlCost", Manager.CLvlCost.ToString());
            PlayerPrefs.SetString("levelCPC", Manager.levelCPC.ToString());
            PlayerPrefs.SetString("levelCPS", Manager.levelCPS.ToString());
            PlayerPrefs.SetFloat("ColorCoinR", Manager.ColorCoin.r);
            PlayerPrefs.SetFloat("ColorCoinG", Manager.ColorCoin.g);
            PlayerPrefs.SetFloat("ColorCoinB", Manager.ColorCoin.b);

            // Prestige stuff
            PlayerPrefs.SetString("PrestigeCost", Manager.PrestigeCost.ToString());
            PlayerPrefs.SetString("PrestigeCount", Manager.prestigeCount.ToString());
            PlayerPrefs.SetString("PrestigeBonus", Manager.prestigeBonus.ToString());

            PlayerPrefs.Save();
            print("Successfully saved all save data");
        }
        else
            print("Reset the save data so we're not saving anything");
    }
}
