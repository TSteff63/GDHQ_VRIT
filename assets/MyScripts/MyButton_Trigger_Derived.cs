using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we want this button to trigger on, then turn back off with a delay, during the on time, it will instantiate coffee gameobjects at a spawnpoint
public class MyButton_Trigger_Derived : MyButton_AbstractParent
{
    [SerializeField]
    private GameObject coffeeSpheres;

    [SerializeField]
    private Transform coffeeSpawnpoint;

    [SerializeField]
    private float coffeeSpawnIntervalDelay;

    [SerializeField]
    private int currentCoffeeCount;  //Amount of coffee created so far...
    [SerializeField]
    private float maxCoffeeToProduce;  //amount of coffee spheres to create before breaking the loop.

    protected override void Start()
    {
        base.Start();

        //coffee will only instantiate as long as the audio clip is playing
        maxCoffeeToProduce = (MyAudioManager.Instance._SFX[7].length + 3);

        //event to trigger flashing buttons without needing to use VR
        EditorTool_Buttons.onClick += RunTrigger;
    }

    //used by event code
    private void RunTrigger()
    {
        if (_flashing)
        {
            StartCoroutine(DelaySwitch());
        }
    }

    protected override IEnumerator DelaySwitch()
    {
    
        //interactable is false
        _interactable = false;
    
        //play animation so it pushes in
        anim.SetTrigger("Pressed");

        //Play Audio from MyAudioManager singleton of button click
        MyAudioManager.Instance.PlaySFXClip(3);

        //wait for the sound effect to finish
        yield return new WaitForSeconds(0.33f);

        //bool triggers that this is ON
        //Color of button turns GREEN
        OnLogic();

        //play clip for coffee brewing
        MyAudioManager.Instance.PlaySFXClip(7);

        //reset coffee count, in the case user turns on coffee machine again.
        currentCoffeeCount = 0;

        Debug.Log("Coffee machine is ON, waiting " + buttonDelayTime + " seonds to proceed...");
        //this delay will allow the audio to begin and act like the machine is heating/brewing coffee
        yield return new WaitForSeconds(buttonDelayTime);


        //while buttonOn is true
        //currentCoffeeCount increases each loop, creates a coffee sphere each loop
        //after exceeding maxCoffeeToProduce, OffLogic() runs, turning buttonOn false
        while (buttonOn)
        {
            currentCoffeeCount++;
            Debug.Log("creating a coffee sphere...");
            Instantiate(coffeeSpheres, coffeeSpawnpoint.position, Quaternion.identity);  //this will produce one...
            yield return new WaitForSeconds(coffeeSpawnIntervalDelay);

            if(currentCoffeeCount > maxCoffeeToProduce) 
            {
                Debug.Log("Turning off coffee machine");
                //if this is OFF, the animation plays back into the OFF position
                anim.SetTrigger("Pressed");
                //after a set time, the button returns to OFF
                //Color of button turns RED
                //Button becomes interactable again.
                OffLogic();
            }
        }
    }

    //Logic for when the button is turned ON
    protected override void OnLogic()
    {
        buttonOn = true;
                Debug.Log("Turn Green");
        _meshRender.material.color = Color.green;
        //_interactable = true;
    }
}
