using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILineRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _rayLine;

    [SerializeField]
    private GameObject playerHand;

    // Start is called before the first frame update
    void Start()
    {
        _rayLine.startWidth = 0.01f;
        _rayLine.endWidth = 0.025f;
        _rayLine.startColor = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        _rayLine.SetPosition(0, playerHand.transform.position);
        _rayLine.SetPosition(1, this.transform.position);
    }
}
