
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : 
    MonoBehaviour
{

    public int      damage;

    public float    speed,
        
                    lifeTime;

    // Start is called before the first frame update.

    void Start()
    {
        if(lifeTime > 0)
            Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame.

    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().health -= damage;
            //GameManager.instance.UpdateUI();
            Destroy(this.gameObject);
        }
    }

}
