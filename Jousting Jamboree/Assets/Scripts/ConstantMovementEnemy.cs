using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovementEnemy : MonoBehaviour
{
    public float speed = 0;
    public Vector3 constantMovementVector = new Vector3(0, 0,0);
    private Rigidbody[] rbs;
    private bool shouldMove;

    // Start is called before the first frame update
    void Start()
    {
        constantMovementVector = new Vector3(0, 0, speed);
        shouldMove = true;
        rbs = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            foreach (var rb in rbs)
            {
                constantMovementVector.y = rb.velocity.y;
                rb.velocity = constantMovementVector;
            }
        }
    }

    void OnHit()
    {
        shouldMove = false;
    }
}
