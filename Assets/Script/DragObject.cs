using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    LayerMask clickLayerMask = ~0;

    TargetJoint2D joints;

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            if (joints)
            {
                Destroy(joints);
                joints = null;
            }

            //return;
        }

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (joints)
        {
            joints.target = pos;
            return;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        Collider2D collider = Physics2D.OverlapPoint(pos, clickLayerMask);
/*        if (!collider)
        {
            return;
        }*/

        Rigidbody2D attachedRigidbody = collider.attachedRigidbody;
/*        if (!attachedRigidbody)
        {
            return;
        }*/
        if(collider.tag == "Moveable")
        {
            joints = attachedRigidbody.gameObject.AddComponent<TargetJoint2D>();
            joints.autoConfigureTarget = false;
            joints.anchor = attachedRigidbody.transform.InverseTransformPoint(pos);
        }
        
    }
}
