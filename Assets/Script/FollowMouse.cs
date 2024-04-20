using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D m_Rigidbody;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        Move();
   

    }
    private void FixedUpdate()
    {
/*        if (Input.GetButton("Jump"))
        {
            m_Rigidbody.AddForce(transform.up * 1000);
        }
        else
        {
            // Move();
        }*/
    }
    void Move()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;
        transform.position = targetPos;
    }
}
