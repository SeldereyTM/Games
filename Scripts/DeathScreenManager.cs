using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenManager : MonoBehaviour
{
    private Animator anim;

    private void Start()  
    {
        anim = GetComponent<Animator>();     
    }

    void Update()
    {
        if (PlayerController.health == 0) {
            DeathScreen.isDeathScreen = true;
            anim.SetBool("IsDeathScreen", true);
        }
    }
}
