using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public string hitRegisterTag = "PlayerWeaponHitBox";
    private bool alreadyHit = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == hitRegisterTag && !alreadyHit)
        {
            alreadyHit = true;
            gameObject.SendMessageUpwards("OnHit");
            enabled = false;
        }
        
    }
}
