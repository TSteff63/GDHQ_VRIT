using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject ChestCore, DummyCore, fx_sparks;

    [SerializeField]
    MyButton_ReactorChest _chestButton;

    [SerializeField]
    GameManager script_GameManager;

    [SerializeField]
    MeshRenderer mesh_BadCore;
    [SerializeField]
    AudioSource audio_BadCore;

    public bool _flashing;

    private bool _audioPlayedAlready_badCore;
    private bool _audioPlayedAlready_goodCore;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NewCore")
        {
            //if audio hasn't played yet, play specific clip
            if (!_audioPlayedAlready_goodCore)
            {
                //Tells player to hit 4 switches to power up reactor
                MyAudioManager.Instance.PlayVOClip(13);

                //reference the ThrowGrabbable script so that the player releases the object before it is disabled.
                ThrowGrabbable script_chestCoreGrabbable = ChestCore.GetComponent<ThrowGrabbable>();
                script_chestCoreGrabbable.ForceRelease();
                //deactivate the power core from the chest to trick user into thinking the new one snapped into place.
                ChestCore.SetActive(false);

                //Dummy core material emission should be black, only turns white when buttons are pressed to 'restart reactor'
                DummyCore.SetActive(true);

                //after buttons are pressed, emission turns white on dummy core, chair rotates back to original position
                script_GameManager.enumPosition = GameManager.timelinePosition.case5_TurnOnReactor;
                script_GameManager.StateMachine(GameManager.timelinePosition.case5_TurnOnReactor);
            }
            _audioPlayedAlready_goodCore = true;
        }
    }

    //when the bad core leaves the reactor trigger area, chest button begins flashing as active and reactor stops flashing
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "BadCore")
        {
            //if audio hasn't played yet, play specific clip
            if (!_audioPlayedAlready_badCore)
            {
                //tells player to push button for good core in chest
                MyAudioManager.Instance.PlayVOClip(12);

                //Button on power core chest will now blink and be able to be pushed
                _chestButton.ActivateBtnFlash();

                //bad core stops flashing
                _flashing = false;
                //hissing noise stops playing
                audio_BadCore.Stop();

                //enable good power core in chest
                ChestCore.SetActive(true);

                //disable the sparks fx
                fx_sparks.SetActive(false);
            }
            _audioPlayedAlready_badCore = true;
        }
    }


    //this method is called externally by TimeLine signal
    public void StartFlashingReactor()
    {
        _flashing = true;
        fx_sparks.SetActive(true);
        StartCoroutine(FlashingReactor());
    }

    private IEnumerator FlashingReactor()
    {
        //while loop to simulate flashing using emission on set material.  Loop ends when core is removed from reactor.
        while (_flashing)
        {
            mesh_BadCore.material.SetColor("_EmissionColor", Color.red);
            yield return new WaitForSeconds(0.33f);
            mesh_BadCore.material.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(0.33f);
            if (!_flashing)
            {
                //when bad power core is not flashing, it should default to black / unpowered / not working
                mesh_BadCore.material.SetColor("_EmissionColor", Color.black);
            }
        }
    }
}
