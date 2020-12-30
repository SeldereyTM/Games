using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public Animator buttonAnim;
    public Animator chestAnim;
    public LayerMask chestCollision;

    bool openChest = false;  //  открыт ли сундук
    bool emptyChest = false;  //  есть ли в нем монеты
    private bool isChestNear; //  рядом ли игрок
    public Transform feetPos;  //  положение ног игрока

    private void Update()
    {
        isChestNear = Physics2D.OverlapCircle(feetPos.position, 0.5f, chestCollision);

        if ((openChest == false) && (Input.GetKeyDown(KeyCode.F)) && (isChestNear == true))
        {  //  если сундук закрыт и сундук рядом и игрок нажал F
            openChest = true;  // открываем сундук
            chestAnim.SetTrigger("openChest");
        }

        if ((openChest == true) && (Input.GetKeyDown(KeyCode.F)) && (isChestNear == true))
        {  //  если сундук открыт и сундук рядом и игрок нажал F
            buttonAnim.SetBool("ButtonUp", false);  //  опускаем кнопку
            
            chestAnim.SetTrigger("takeMoney");  //  опустошаем сундук
            CoinCollect.coin += 50;  //  прибавляем монеты
            emptyChest = true;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {  // если игрок рядом, а сундук не пуст, поднимаем кнопку
        if(emptyChest == false)
        buttonAnim.SetBool("ButtonUp", true);
       
    }

    public void OnTriggerExit2D(Collider2D other)
    {  //  если игрока рядом нет, опускаем кнопку
        buttonAnim.SetBool("ButtonUp", false);
    }
}
