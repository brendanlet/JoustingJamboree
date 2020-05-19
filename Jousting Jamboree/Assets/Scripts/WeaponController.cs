using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    private WeaponStats weaponStats;

    private Vector3 originalPosition;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        weaponStats = transform.GetComponentInChildren<WeaponStats>();

        rb = GetComponent<Rigidbody>();
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        Fall();
        


        if (Input.GetMouseButton(0))
        {
            Rotate();
        }
        else
        {
            Move();
        }

        if (transform.localPosition.x < originalPosition.x - weaponStats.xMovementBounds)
        {
            var tmpVec = transform.localPosition;
            tmpVec.x = originalPosition.x - weaponStats.xMovementBounds;
            transform.localPosition = tmpVec;
        }
        else if (transform.localPosition.x > originalPosition.x + weaponStats.xMovementBounds)
        {
            var tmpVec = transform.localPosition;
            tmpVec.x = originalPosition.x + weaponStats.xMovementBounds;
            transform.localPosition = tmpVec;
        }

        if (transform.localPosition.y < originalPosition.y - weaponStats.yMovementBounds)
        {
            var tmpVec = transform.localPosition;
            tmpVec.y = originalPosition.y - weaponStats.yMovementBounds;
            transform.localPosition = tmpVec;
        }
        else if (transform.localPosition.y > originalPosition.y + weaponStats.yMovementBounds)
        {
            var tmpVec = transform.localPosition;
            tmpVec.y = originalPosition.y + weaponStats.yMovementBounds;
            transform.localPosition = tmpVec;
        }

    }

    private void Fall()
    {
        transform.Translate(weaponStats.fallingVector /100, Space.World);
    }

    private void Move()
    {
        var movementVector = new Vector3();

        var xMove = Input.GetAxis("Mouse X") * weaponStats.xInputMovementSpeed;
        var yMove = Input.GetAxis("Mouse Y") * weaponStats.yInputMovementSpeed;

        movementVector.x = xMove;
        movementVector.y = yMove;

        transform.Translate(movementVector, Space.World);

    }

    private void Rotate()
    {
        var rotationVector = new Vector3();

        var xRotation = Input.GetAxis("Mouse Y") * weaponStats.xInputRotationSpeed;
        var zRotation = Input.GetAxis("Mouse X") * -weaponStats.zInputRotationSpeed;

        rotationVector.x = xRotation;
        rotationVector.z = zRotation;

        transform.Rotate(rotationVector, Space.World);

    }
}
