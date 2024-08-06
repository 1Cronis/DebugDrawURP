using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDrawMethod : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DebugDraw.DrawSphere(transform.position + Vector3.left *3,1,Color.red,Color.yellow);
    }

    void FixedUpdate()
    {
        DebugDraw.DrawSphere(transform.position + Vector3.right *3,1,Color.magenta);
    }
}
