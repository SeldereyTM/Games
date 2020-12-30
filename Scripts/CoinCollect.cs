using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{
    public int coinMax;
    public static int coin;

    private Text coinCounter;
    public Image coinBar;
    public Sprite coinFull;
    public Sprite coinFive;
    public Sprite coinFour;
    public Sprite coinThree;
    public Sprite coinTwo;
    public Sprite coinOne;
    public Sprite coinEmpty;
   
    void Start()
    {
        coinCounter = GetComponent<Text>();
        coin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinCounter.text = coin.ToString();


        if (coin < 0)
            coin = 0;
        if (coin > coinMax)
            coin = coinMax;

        if (coin == coinMax)
        {
            coinBar.sprite = coinFull;
        }  //  выбор спрайта в зависимости от количества жизней
        else if ((coin < coinMax) && (coin > coinMax / 5 * 4))
        {
            coinBar.sprite = coinFive;
        }
        else if ((coin < coinMax / 5 * 4) && (coin > coinMax / 5 * 3))
        {
            coinBar.sprite = coinFour;
        }
        else if ((coin < coinMax / 5 * 3) && (coin > coinMax / 5 * 2))
        {
            coinBar.sprite = coinThree;
        }
        else if ((coin < coinMax / 5 * 2) && (coin > coinMax / 5 * 1))
        {
            coinBar.sprite = coinTwo;
        }
        else if ((coin < coinMax / 5 * 1) && (coin > 0))
        {
            coinBar.sprite = coinOne;
        }
        else if (coin == 0)
        {
            coinBar.sprite = coinEmpty;
        }



    }
}
