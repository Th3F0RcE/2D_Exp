﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    // Start is called before the first frame update
    void Awake()
    {
        if(gm == null)
        {
            gm = this;
        }
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
    }
}
