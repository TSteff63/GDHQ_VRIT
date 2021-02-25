using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrabber : OVRGrabber
{
    protected override void Start()
    {
        base.Start();

    }

    public override void Update()
    {
        base.Update();

        //Debug.DrawRay(transform.position, transform.forward, Color.red, 0.1f);
    }
}
