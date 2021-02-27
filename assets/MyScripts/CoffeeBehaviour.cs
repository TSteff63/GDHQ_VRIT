using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeBehaviour : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    Vector3 initialForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(initialForce, ForceMode.Impulse);
    }
}
