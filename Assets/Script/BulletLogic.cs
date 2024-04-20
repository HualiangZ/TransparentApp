using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private Rigidbody2D rb;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveBullet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveBullet()
    {
        
        rb.velocity = transform.up * -speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
        if(collision.collider.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
