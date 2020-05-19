using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var xRotation = Random.Range(0, 35);
        var zRotation = Random.Range(-45, 0);
        transform.Rotate(xRotation, 0, 0);
        transform.Rotate(0, 0, zRotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
