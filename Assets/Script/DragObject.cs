using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    LayerMask clickLayerMask = ~0;
    TargetJoint2D joints;

    public Camera targetCamera;

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            if (joints)
            {
                Destroy(joints);
                joints = null;
            }
        }

        Vector2 pos = targetCamera.ScreenToWorldPoint(Input.mousePosition);

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
        Rigidbody2D attachedRigidbody = collider.attachedRigidbody;
        if(collider.tag == "Moveable")
        {
            joints = attachedRigidbody.gameObject.AddComponent<TargetJoint2D>();
            joints.autoConfigureTarget = false;
            joints.anchor = attachedRigidbody.transform.InverseTransformPoint(pos);
        }
        
    }
}
