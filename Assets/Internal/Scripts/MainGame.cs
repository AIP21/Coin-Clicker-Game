using System;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    public double coins;

    // Money stats
    public double coinLevel;
    public double multiplier;
    public double coinsPerClick;
    public double coinsPerSecond;
    public double prestigeBonus;

    // Misc stats
    public double totalCoins;
    public double totalClicks;
    public double timePlayed;
    public double prestigeCount;

    // Levels for text
    public double levelCPC;
    public double levelCPS;

    // Cost
    public double MultCost;
    public double CPCCost;
    public double CPSCost;
    public double CLvlCost;
    public double PrestigeCost;

    // [HideInInspector]
    // public bool ShopOpen;
    // [HideInInspector]
    // public bool StatsOpen;
    // [HideInInspector]
    // public bool SettingsOpen;
    [HideInInspector]
    public int maxParticles;
    // LTDescr tween1, tween2, tween3;

    public float clickSpeed;
    public float ShakeSpeed = 50;
    public float ShakeAmount = 7;
    Vector2 startingPos;
    #endregion

    #region UI
    [Header("UI")]
    public Text coinsText;
    public Text totalCoinsText;
    public Text totalClicksText;
    public Text timePlayedText;
    public Text textMult;
    public Text textCPC;
    public Text textCPCTotal;
    public Text textCPS;
    public Text textCPSTotal;
    public Text textCLvl;
    public Text textCPCLvlCost;
    public Text textMultLvlCost;
    public Text textCPSLvlCost;
    public Text textCLvlCost;
    public Text textPrestigeBonus;
    public Text textPrestigeBonusUpg;
    public Text textPrestigeCost;
    public Text textTotalPrestiges;
    public Button coinButton;
    public Button upgradeMult;
    public Button upgradeCPC;
    public Button upgradeCPS;
    public Button upgradecoinLevel;
    public Image ImageBackground;
    public UnityEngine.Color ColorCoin;
    public CanvasGroup NewUpgradeDotIcon;
    public Image ImageCLvl;
    public GameObject PrestigeButton;
    public GameObject PrestigePopupBlocker;
    public GameObject PrestigePopup;
    public GameObject PrestigeAnimationFade;
    public GameObject PrestigeAnimationBlur;
    bool prestigeButtonShown = false;
    #endregion

    #region References
    [Header("References")]
    public ParticleSystem CoinBurst;
    public ParticleSystem CoinSparks;
    // public ParticleSystem HissParticle;
    // public GameObject canvas;
    private ParticlePooling pooler;
    public Notifications notifier;
    // ParticleSystem.EmissionModule hissEmitter;
    Settings settings;
    public UIAnim UIAnimator;
    #endregion

    public void Start() // LOAD
    {
        pooler = GetComponent<ParticlePooling>();
        settings = GetComponent<Settings>();
        // hissEmitter = HissParticle.emission;

        // tween1 = LeanTween.delayedCall(0, () => { });
        // tween2 = LeanTween.delayedCall(0, () => { });
        // tween3 = LeanTween.delayedCall(0, () => { });

        coinButton.onClick.AddListener(coinClick);
        upgradeMult.onClick.AddListener(upgm);
        upgradeCPC.onClick.AddListener(upgcpc);
        upgradeCPS.onClick.AddListener(upgcps);
        upgradecoinLevel.onClick.AddListener(upgcl);

        UpdateText();

        if (prestigeBonus > 1)
        {
            textPrestigeBonus.gameObject.SetActive(true);
            textPrestigeBonus.text = "Prestige coin collection bonus: +" + prestigeBonus.ToString() + "%";
        }

        InvokeRepeating("cpsMethod", 0f, 1f);

        startingPos = coinButton.GetComponent<RectTransform>().anchoredPosition;

        print("Successfully initiated the main game subsystem");
    }

    // Runs every second
    public void cpsMethod()
    {
        timePlayed++;
        timePlayedText.text = "Time played: " + Math.Round(((timePlayed / 60) / 60) / 24) + " days, " + Math.Round((timePlayed / 60) / 60) + " hours, and " + Math.Round(timePlayed / 60) + " minutes";
        totalCoinsText.text = "Total coins: " + totalCoins + " (all time)";
        totalClicksText.text = "Total clicks: " + totalClicks;

        if (coinsPerSecond > 0)
        {
            double coinsGained = ((coinsPerSecond * (multiplier + 1) + coinLevel) * prestigeBonus);
            print("Coins per second method called: " + coinsGained);
            coins += coinsGained;
            totalCoins += coinsGained;
            GameObject go = pooler.GetPooledUpArrow();
            LeanTween.cancel(go);
            go.SetActive(true);
            go.LeanMoveLocalY(go.transform.localPosition.y + 15, 1);
            LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 1, 0.25f);
            LeanTween.delayedCall(0.85f, () =>
            {
                LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 0, 0.25f).setOnComplete(() =>
                {
                    go.transform.localPosition = Vector3.zero;
                    go.SetActive(false);
                });
            });
            EmitParticles(false);
        }
    }

    public void coinClick()
    {
        double coinsGained = ((coinsPerClick * ((multiplier + 1) * coinLevel)) * prestigeBonus);
        print("Coins per click method called: " + coinsGained);
        coins += coinsGained;
        totalCoins += coinsGained;
        GameObject go = pooler.GetPooledUpArrow();
        LeanTween.cancel(go);
        go.SetActive(true);
        go.LeanMoveLocalY(go.transform.localPosition.y + 15, 1);
        LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 1, 0.25f);
        LeanTween.delayedCall(0.85f, () =>
        {
            LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 0, 0.25f).setOnComplete(() =>
            {
                go.transform.localPosition = Vector3.zero;
                go.SetActive(false);
            });
        });
        EmitParticles(true);
    }

    public void upgm()
    {
        if (coins >= MultCost)
        {
            print("Upgraded multiplier to level: " + (multiplier + 1));
            coins -= MultCost;
            GameObject go = pooler.GetPooledDownArrow();
            go.SetActive(true);
            go.LeanMoveLocalY(go.transform.localPosition.y - 15, 1);
            LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 1, 0.25f);
            LeanTween.delayedCall(0.85f, () =>
            {
                LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 0, 0.25f).setOnComplete(() =>
                {
                    go.transform.localPosition = Vector3.zero;
                    go.SetActive(false);
                });
            });
            //ImageMult.color = new UnityEngine.Color(Random.Range(1,255),Random.Range(1,255),Random.Range(1,255));
            multiplier++;
            MultCost += (((5500 * multiplier) + coinsPerClick) * (coinLevel * 2));
            UpdateText();
        }
    }

    public void upgcps()
    {
        if (coins >= CPSCost)
        {
            print("Upgraded coins per second to level: " + (coinsPerSecond + 1));
            coins -= CPSCost;
            GameObject go = pooler.GetPooledDownArrow();
            go.SetActive(true);
            go.LeanMoveLocalY(go.transform.localPosition.y - 15, 1);
            LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 1, 0.25f);
            LeanTween.delayedCall(0.85f, () =>
            {
                LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 0, 0.25f).setOnComplete(() =>
                {
                    go.transform.localPosition = Vector3.zero;
                    go.SetActive(false);
                });
            });
            //ImageCPS.color = new UnityEngine.Color(Random.Range(1,255),Random.Range(1,255),Random.Range(1,255));
            coinsPerSecond += (1 + coinLevel);
            levelCPS++;
            CPSCost += (((2000 * coinsPerSecond) + coinsPerClick) * coinLevel);
            UpdateText();
        }
    }

    public void upgcpc()
    {
        if (coins >= CPCCost)
        {
            print("Upgraded coins per click to level: " + (coinsPerClick + 1));
            coins -= CPCCost;
            GameObject go = pooler.GetPooledDownArrow();
            go.SetActive(true);
            go.LeanMoveLocalY(go.transform.localPosition.y - 15, 1);
            LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 1, 0.25f);
            LeanTween.delayedCall(0.85f, () =>
            {
                LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 0, 0.25f).setOnComplete(() =>
                {
                    go.transform.localPosition = Vector3.zero;
                    go.SetActive(false);
                });
            });
            //ImageCPC.color = new UnityEngine.Color(Random.Range(1,255),Random.Range(1,255),Random.Range(1,255));
            coinsPerClick += (coinLevel + 1);
            levelCPC++;
            CPCCost += (((500 * coinsPerClick) + coinsPerClick) * coinLevel);
            UpdateText();
        }
    }

    public void upgcl()
    {
        if (coins >= CLvlCost)
        {
            print("Upgraded coin level to " + (coinLevel + 1));
            coins -= CLvlCost;
            GameObject go = pooler.GetPooledDownArrow();
            go.SetActive(true);
            go.LeanMoveLocalY(go.transform.localPosition.y - 15, 1);
            LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 1, 0.25f);
            LeanTween.delayedCall(0.85f, () =>
            {
                LeanTweenExt.LeanAlpha(go.GetComponent<CanvasGroup>(), 0, 0.25f).setOnComplete(() =>
                {
                    go.transform.localPosition = Vector3.zero;
                    go.SetActive(false);
                });
            });

            LerpColors();

            coinLevel++;
            CLvlCost += ((10000 * coinLevel) * coinsPerClick) * (multiplier + 1);

            UpdateText();
        }
    }

    public void UpdateText()
    {
        textMultLvlCost.text = "Multiplier level " + (multiplier + 1).ToString() + ": $" + MultCost.ToString();
        textMult.text = (multiplier + 1).ToString() + "x multiplier";

        textCPSLvlCost.text = "Coins Per Second level " + (levelCPS + 1).ToString() + ": $" + CPSCost.ToString();
        textCPS.text = ((coinsPerSecond * (multiplier + 1)) * prestigeBonus).ToString() + " coins per sec.";

        textCPCLvlCost.text = "Coins Per Click level " + (levelCPC + 1).ToString() + ": $" + CPCCost.ToString();
        textCPC.text = ((coinsPerClick * ((multiplier + 1) * coinLevel)) * prestigeBonus).ToString() + " coins per click";

        textCLvlCost.text = "Coin Level " + (coinLevel + 1).ToString() + ": $" + CLvlCost.ToString();
        textCLvl.text = "Level " + coinLevel.ToString() + " coin";

        textPrestigeCost.text = "Next Prestige: " + PrestigeCost.ToString();
        textTotalPrestiges.text = "Total Prestiges: " + prestigeCount.ToString();

        if (prestigeBonus > 1)
        {
            textCPCTotal.text = "(" + coinsPerClick.ToString() + " coins per click * (" + (multiplier + 1).ToString() + "x multiplier * level " + coinLevel + " coin)) * +" + prestigeBonus + "% prestige bonus = " + ((coinsPerClick * ((multiplier + 1) * coinLevel)) * prestigeBonus).ToString() + " coins per click";
            textCPSTotal.text = "(" + coinsPerSecond.ToString() + " coins per second * " + (multiplier + 1).ToString() + "x multiplier + level " + coinLevel + " coin) * +" + prestigeBonus + "% prestige bonus = " + ((coinsPerClick * (multiplier + 1)) * prestigeBonus).ToString() + " coins per second";
        }
        else
        {
            textCPCTotal.text = coinsPerClick.ToString() + " coins per click * (" + (multiplier + 1).ToString() + "x multiplier * level " + coinLevel + " coin) = " + ((coinsPerClick * ((multiplier + 1) * coinLevel)) * prestigeBonus).ToString() + " coins per click";
            textCPSTotal.text = coinsPerSecond.ToString() + " coins per second * " + (multiplier + 1).ToString() + "x multiplier + level " + coinLevel + " coin = " + ((coinsPerClick * (multiplier + 1)) * prestigeBonus).ToString() + " coins per second";
        }
    }

    public void hidePrestigePanel()
    {
        if (settings.FancyGraphics)
            UIAnimator.UIBlurOut("PrestigePopupBlocker*(1,1,1,0)*0.5");
        else
            UIAnimator.UIFadeOut("PrestigePopupBlocker*0*0.5");
        UIAnimator.UIScaleOut("PrestigePopup*(-1,-1,-1)*0.5");
    }

    public void prestigeClick()
    {
        PrestigePopupBlocker.SetActive(true);
        PrestigePopup.SetActive(true);
        textPrestigeBonusUpg.text = "+" + (prestigeBonus * 2 * prestigeCount).ToString() + "%";

        if (settings.FancyGraphics)
            UIAnimator.UIBlurIn("PrestigePopupBlocker*(0.3215686,0.3215686,0.3215686,7.5)*0.4");
        else
            UIAnimator.UIFadeIn("PrestigePopupBlocker*1*0.4");
        UIAnimator.UIScaleIn("PrestigePopup*(1,1,1)*0.5");
    }

    public void prestigeConfirm()
    {
        PrestigeCost = (PrestigeCost * PrestigeCost);
        if (prestigeCount == 0)
            prestigeBonus = 25;
        else
            prestigeBonus *= 2 * prestigeCount;

        prestigeCount++;
        coins = 0;
        coinsPerClick = 1;
        coinsPerSecond = 0;
        multiplier = 0;
        levelCPC = 0;
        levelCPS = 0;
        coinLevel = 1;
        MultCost = 100;
        CPCCost = 10;
        CPSCost = 50;
        CLvlCost = 250;

        // Play prestige animation
        CanvasGroup group = PrestigeAnimationFade.GetComponent<CanvasGroup>();
        group.alpha = 0;
        LeanTweenExt.LeanAlpha(group, 1, 5).setOnStart(() =>
        {
            PrestigeAnimationFade.SetActive(true);
        }).setOnComplete(() =>
        {
            prestigeButtonShown = false;
            PrestigeButton.GetComponent<CanvasGroup>().alpha = 0;
            PrestigeButton.GetComponent<LayoutElement>().preferredWidth = 0;
            PrestigeButton.SetActive(false);

            textPrestigeBonus.gameObject.SetActive(true);
            textPrestigeBonus.text = "Prestige coin collection bonus: +" + prestigeBonus.ToString() + "%";

            if (settings.FancyGraphics)
                UIAnimator.UIBlurOut("PrestigePopupBlocker*(1,1,1,0)*0.5");
            else
                UIAnimator.UIFadeOut("PrestigePopupBlocker*0*0.5");
            UIAnimator.UIScaleOut("PrestigePopup*(-1,-1,-1)*0.5");


            LeanTweenExt.LeanAlpha(group, 0, 3).setOnComplete(() =>
            {
                PrestigeAnimationFade.SetActive(false);
            }).setDelay(2);
            LeanTweenExt.LeanBlur(PrestigeAnimationBlur, 0, 3).setOnComplete(() =>
            {
                PrestigeAnimationBlur.SetActive(false);
            }).setDelay(2);
        });
        LeanTweenExt.LeanBlur(PrestigeAnimationBlur, 6.5f, 3).setOnStart(() =>
        {
            PrestigeAnimationBlur.SetActive(true);
        });

        UpdateText();

        print("PLAYER PRESTIGED WITH NEW PRESTIGE COUNT: " + prestigeCount + "  AND NEW PRESTIGE BONUS: +" + prestigeBonus.ToString() + "%");
    }

    private void LerpColors()
    {
        ColorCoin = UnityEngine.Random.ColorHSV();
        LeanTweenExt.LeanColorParticle(CoinBurst.gameObject, ColorCoin * 0.8f, 5);
        LeanTweenExt.LeanColor(ImageCLvl.rectTransform, ColorCoin, 5);
        LeanTweenExt.LeanColor(ImageBackground.rectTransform, ColorCoin / 2, 5);
    }

    private void Shake()
    {
        print("Shaking coin button with click speed: " + clickSpeed);
        coinButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(startingPos.x + Mathf.Sin(UnityEngine.Random.Range(-1 * ShakeAmount, 1 * ShakeAmount) * ShakeSpeed) * clickSpeed, startingPos.y + (Mathf.Sin(UnityEngine.Random.Range(-1 * ShakeAmount, 1 * ShakeAmount) * ShakeSpeed) * clickSpeed));

        Color newColor = new Color(ColorCoin.r, ColorCoin.g - (clickSpeed / 10), ColorCoin.b);
        print("colorChange: " + (ColorCoin.g - (clickSpeed / 10)) + "      newColor: " + newColor);
        ImageCLvl.color = newColor;
    }

    int i = 0;
    float oldClickSpeed = 0;
    private void FixedUpdate()
    {
        if (i > 1)
        {
            i = 0;
            if (clickSpeed > 0.75f)
                Shake();
            else if ((Vector2)coinButton.GetComponent<RectTransform>().anchoredPosition != startingPos)
                coinButton.GetComponent<RectTransform>().anchoredPosition = startingPos;

            if (clickSpeed > 0)
            {
                clickSpeed -= 0.05f;

                // Debug.LogWarning("diff: " + (clickSpeed - oldClickSpeed));
                // if ((clickSpeed - oldClickSpeed) > -0.04f)
                // {
                //     Debug.LogWarning("HissParticle paused by not cooling off");
                //     if (hissEmitter.enabled)
                //         hissEmitter.enabled = false;
                // }
                // else if (!hissEmitter.enabled)
                // {
                //     Debug.LogWarning("HissParticle played by cooling off");
                //     hissEmitter.enabled = true;
                // }
            }
            else if (clickSpeed < 0)
                clickSpeed = 0;

            oldClickSpeed = clickSpeed;
        }
        i++;
    }

    bool upgradeVisisble = false;
    private void Update()
    {
        coinsText.text = "$" + coins.ToString();

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(3) || Input.GetMouseButtonDown(4))
        {
            totalClicks++;
            clickSpeed = Mathf.Clamp(clickSpeed + 0.2f, 0, 5);
            print("Clicked");
        }

        if (coins >= CLvlCost)
            upgradecoinLevel.interactable = true;
        else
            upgradecoinLevel.interactable = false;

        if (coins >= MultCost)
            upgradeMult.interactable = true;
        else
            upgradeMult.interactable = false;

        if (coins >= CPCCost)
            upgradeCPC.interactable = true;
        else
            upgradeCPC.interactable = false;

        if (coins >= CPSCost)
            upgradeCPS.interactable = true;
        else
            upgradeCPS.interactable = false;

        // Prestige stuff
        if (coins >= PrestigeCost && !prestigeButtonShown)
        {
            print("Showed prestige button");
            prestigeButtonShown = true;
            PrestigeButton.GetComponent<CanvasGroup>().alpha = 0;
            LeanTweenExt.LeanLayoutElementScale(PrestigeButton, 360, 0.5f).setOnStart(() =>
            {
                PrestigeButton.SetActive(true);
            }).setEaseOutCubic();
            LeanTweenExt.LeanAlpha(PrestigeButton.GetComponent<CanvasGroup>(), 1, 0.5f).setEaseOutCubic();
        }

        if (coins >= CLvlCost || coins >= MultCost || coins >= CPCCost || coins >= CPSCost)
        {
            if (!upgradeVisisble)
            {
                print("upgradeVisisble is false, showing dot icon");
                upgradeVisisble = true;
                NewUpgradeDotIcon.LeanAlpha(0.9f, 0.5f);
            }

            if (coins >= CLvlCost)
                notifier.CreateNotification(Notifications.NotificationType.Level, "Coin Level: " + (coinLevel + 1).ToString(), CLvlCost);
            if (coins >= MultCost)
                notifier.CreateNotification(Notifications.NotificationType.Mult, "Multiplier: " + (multiplier + 1).ToString(), MultCost);
            if (coins >= CPCCost)
                notifier.CreateNotification(Notifications.NotificationType.CPC, "Coins Per Click: " + (coinsPerClick + 1).ToString(), CPCCost);
            if (coins >= CPSCost)
                notifier.CreateNotification(Notifications.NotificationType.CPS, "Coins Per Second: " + (coinsPerSecond + 1).ToString(), CPSCost);
        }
        if (coins < CLvlCost || coins < MultCost || coins < CPCCost || coins < CPSCost || coins < PrestigeCost)
        {
            if (upgradeVisisble && (coins < CLvlCost && coins < MultCost && coins < CPCCost && coins < CPSCost))
            {
                print("upgradeVisisble is true and no upgrades are possible, hiding dot icon");
                upgradeVisisble = false;
                NewUpgradeDotIcon.LeanAlpha(0, 0.5f);
            }

            if (coins < CLvlCost)
                notifier.DestroyNotification(Notifications.NotificationType.Level);
            if (coins < MultCost)
                notifier.DestroyNotification(Notifications.NotificationType.Mult);
            if (coins < CPCCost)
                notifier.DestroyNotification(Notifications.NotificationType.CPC);
            if (coins < CPSCost)
                notifier.DestroyNotification(Notifications.NotificationType.CPS);

            if (coins < PrestigeCost && prestigeButtonShown)
            {
                print("Hid prestige button");
                prestigeButtonShown = false;
                PrestigeButton.GetComponent<CanvasGroup>().alpha = 1;
                LeanTweenExt.LeanLayoutElementScale(PrestigeButton, 0, 0.5f).setOnComplete(() =>
                {
                    PrestigeButton.SetActive(false);
                }).setEaseOutCubic();
                LeanTweenExt.LeanAlpha(PrestigeButton.GetComponent<CanvasGroup>(), 0, 0.5f).setEaseOutCubic();
            }
        }
    }

    private void EmitParticles(bool PlayerClick)
    {
        double clampedCount = coinsPerClick * ((multiplier + 1) * (PlayerClick ? coinLevel : 1));
        CoinBurst.Emit((int)Mathf.Clamp((uint)clampedCount, 1, maxParticles));
        if (PlayerClick && clickSpeed > 1.5f) CoinSparks.Emit((int)(UnityEngine.Random.Range(0, 25) * (clickSpeed / 10)));

        print("Emitted " + (int)Mathf.Clamp((uint)clampedCount, 1, maxParticles) + " coin particles");
    }
}