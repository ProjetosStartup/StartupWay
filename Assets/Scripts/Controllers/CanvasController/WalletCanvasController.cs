using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Globalization;

public class WalletCanvasController : MonoBehaviour
{
    public static WalletCanvasController Instance;
    private float actualBalance,realBalance;
    [SerializeField]
    private TextMeshProUGUI balanceText;
    [SerializeField]
    private RectTransform coinPile;

    private CultureInfo culture;
    private bool isCoroutineRunning;

    
    private void Awake()
    {
        Instance = this;
        culture = new CultureInfo("pt-BR");

        foreach (Transform coin in coinPile)
        {
            rotation.Add(coin.localRotation);
            position.Add(coin.localPosition);
        }
        balanceText.text = string.Format(culture, "{0:N2}", actualBalance);
    }
    public void RefreshBalanceUI(float delay,float duration,bool isAnimated = true)
    {

        if (isAnimated)
            CoinAnimation(delay);

        realBalance = StartupController.Instance.Startup.Wallet.Balance;

        float initialBalance = actualBalance;
        balanceText.transform.DOKill();

        DOVirtual.Float(initialBalance, realBalance, duration * 2, (value) =>
        {
            balanceText.text = string.Format(culture, "{0:N2}", value);

        })
        .OnStart(() =>
        {
            balanceText.transform.localScale = Vector3.one;
            balanceText.transform.DOShakeScale(duration * 2);

        })
        .SetDelay(delay)
        .OnComplete(() =>
        {
            actualBalance = realBalance;

        });
    }

  

    #region CoinAnimation
    [SerializeField]
    private RectTransform coinIcon;

    private List<Quaternion> rotation = new();
    private List<Vector3> position = new();
    private void CoinAnimation(float duration)
    {
        ResetCoins();

        Vector3 playerPos = Camera.main.WorldToScreenPoint(PlayerController.Instance.Player.transform.position);
        coinPile.position = playerPos;
        float durationPerCoin = duration / coinPile.childCount;

        Sequence sequence = DOTween.Sequence();

        foreach (Transform coin in coinPile)
        {
            sequence.Join(UtilAnimation.SetScaleAndChangeScale(coin, 0, 1, duration*0.8f , Ease.OutBack).SetDelay(durationPerCoin));
            sequence.Join(coin.DOMove(coinIcon.position, duration).SetDelay(durationPerCoin).SetEase(Ease.InOutBounce, 1).OnComplete(() => { AudioController.Instance.Play("Moeda");
                
            }));
            
        }
        sequence.OnComplete(() => coinPile.gameObject.SetActive(false));
        
    }
    private void ResetCoins()
    {
        coinPile.gameObject.SetActive(true);
        int i = 0;
        foreach(Transform coin in coinPile)
        {
            coin.SetLocalPositionAndRotation(position[i], rotation[i]);
            i++;
        }
    }
    #endregion
}
