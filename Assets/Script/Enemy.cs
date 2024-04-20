using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int speed;
    private GameObject bulletInst;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
        Move();
        
        
    }

    private void Move()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void LookAt()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 4000;
        transform.LookAt(targetPos, Vector3.forward);
    }

    IEnumerator shoot()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(1);
            bulletInst = Instantiate(bullet, bulletSpawn.position, gameObject.transform.rotation);
            yield return new WaitForSeconds(2);
        }
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
}
