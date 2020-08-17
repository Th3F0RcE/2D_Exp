using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int health = 5;
    }

    public PlayerStats playerStats = new PlayerStats();

    public void DamagePlayer(int damage)
    {
        playerStats.health -= damage;
        Debug.Log("Player got damaged");

        if(playerStats.health <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

}
