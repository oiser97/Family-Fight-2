
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : 
    MonoBehaviour
{

    public int health;

    public void Eat(Player player)
    {
        player.health += health;
        Destroy(this.gameObject);
    }

}
