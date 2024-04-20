using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float force, radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            KnockBack();
        }
    }

    private void Move()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 20 * Time.deltaTime);
    }

    private void LookAt()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 4000;
        transform.LookAt(targetPos, Vector3.forward);
    }

    public void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider c in colliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

    }
}
