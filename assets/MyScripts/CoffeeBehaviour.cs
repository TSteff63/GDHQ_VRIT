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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MouthAreaToEat")
        {
            Debug.Log("Consuming Food or Drink");
            //play sound for sipping coffee, this should not be interrupted if more coffee collides with mouth area
            if (MyAudioManager.Instance._SFXSource.isPlaying == false)
            {
                MyAudioManager.Instance.PlaySFXClip(6);
            }
        }
    }
}
