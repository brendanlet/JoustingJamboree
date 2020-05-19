using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float xInputMovementSpeed = 1f;
    public float yInputMovementSpeed = 1f;

    public float xMovementBounds = 10f;
    public float yMovementBounds = 10f;

    public float xInputRotationSpeed = 1f;
    public float zInputRotationSpeed = 1f;

    public Vector3 fallingVector = new Vector3(0, -1f, 0);
}
