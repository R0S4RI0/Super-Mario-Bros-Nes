using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase
{
    [SerializeField]
    private int coins;
    public GameObject coin;
    public GameObject bloqueInvencible;

    public  void Hit()
    {
   
        base.Hit();
        Instantiate(coin, new Vector3(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);

        this.coins--;
        if (coins <= 0)
        {

            Instantiate(bloqueInvencible, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    
}
