using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGunFire : MonoBehaviour
{
    //FX
    [SerializeField]
    private ParticleSystem _smoke;
    [SerializeField]
    private ParticleSystem _bulletCasing;
    [SerializeField]
    private ParticleSystem _muzzleFlashSide;
    [SerializeField]
    private ParticleSystem _Muzzle_Flash_Front;

    [SerializeField]
    private ParticleSystem bulletImpactFX_Dust;      //Gameobject that spawns a bullet impact effect through particle system

    //Script declarations
    ThrowGrabbable script_throwGrabbable;

    //Components
    [SerializeField]
    private Animator _anim;

    //Audio
    [SerializeField]
    private AudioClip _gunShotAudioClip;
    [SerializeField]
    private AudioSource _audioSource;

    //Raycasting
    [SerializeField]
    private Transform laserOriginPos;
    public LineRenderer laserLine;
    Ray shootLaserRay;
    RaycastHit shootLaserHit;

    private float range = 5000f;                 //range for raycast
    public LayerMask mask;                      //Layermask for raycast
    [SerializeField]
    private Color laserColor = Color.red;        //Dynamic laser line color

    //behaviour variables
    public bool FullAuto;
    //used for semi-auto fire to know if trigger has been released between shots
    private bool triggerPulled;

    //Gun variables
    [SerializeField]
    private float fireRate_pistol;                //cooldown time required to shoot again
    [SerializeField]
    private int damagePerShot = 1;               //Damage each bullet does on impact
    [SerializeField]
    private float timer = 0;                    //timer associated with fire rates

    private OVRInput.Axis1D _hand;




    // Start is called before the first frame update
    void Start()
    {
        //get grabbbable script
        script_throwGrabbable = GetComponentInParent<ThrowGrabbable>();
        //get animator
        //_anim = GetComponent<Animator>();

        triggerPulled = false;
    }




    // Update is called once per frame
    public void Update()
    {
        OVRInput.Update();

        if (script_throwGrabbable.isGrabbed)
        {
            Debug.Log("Gun is being held in right hand");
            //create a timer to track fire rate...
            timer += Time.deltaTime;

            //define shootray
            shootLaserRay.origin = laserOriginPos.transform.position;
            shootLaserRay.direction = -laserOriginPos.transform.right;

            //shoots a line from originPoint to range point, if it hits something on the shootableMask.....
            if (Physics.Raycast(shootLaserRay, out shootLaserHit, range, mask))
            {
                laserLine.enabled = true;                                       //laser enabled
                laserLine.startColor = laserColor;                              //laser color
                laserLine.SetPosition(0, shootLaserRay.origin);                 //laser origin point
                laserLine.SetPosition(1, shootLaserHit.point);                  //laser end point

                Debug.Log("Raycast created and is hitting " + shootLaserHit.transform.name);


                //determines if we are using left or right hand to hold the gun...
                if(script_throwGrabbable.grabbedUsingRightHand)
                {
                    _hand = OVRInput.Axis1D.SecondaryIndexTrigger;
                }
                else
                {
                    _hand = OVRInput.Axis1D.PrimaryIndexTrigger;
                }

                //if player pulls trigger on right controller after timer cooldown...
                if ((OVRInput.Get(_hand) > 0.35f) && (timer > fireRate_pistol) && (triggerPulled == false))
                {
                    Debug.Log("Player is trying to fire weapon using Right trigger");

                    if (FullAuto == false)
                    {
                        _anim.SetTrigger("Fire");
                    }

                    if (FullAuto == true)
                    {
                        _anim.SetBool("Automatic_Fire", true);
                    }

                    //play particle systems for smoke, muzzle flash, and play audio
                    FireGunParticles();

                    //If raycast hits object or enemy with this tag...
                    if (shootLaserHit.collider.tag == ("EnemyHitArea"))
                    {
                        Debug.Log("hit enemy / enemy object");
                        //instantiate impact blood or decal at the location the 'hit' location
                        //GameObject impactFXblood = Instantiate(bulletImpactFX_Blood, shootLaserHit.point, Quaternion.identity);
                        bulletImpactFX_Dust.Stop();
                        bulletImpactFX_Dust.Clear();
                        bulletImpactFX_Dust.transform.position = shootLaserHit.point;
                        bulletImpactFX_Dust.Play();
                        //^ set up object pool in future for this


                        if (shootLaserHit.collider.TryGetComponent(out Enemy_Targets enemy))
                        {
                            enemy.TakeDamage(damagePerShot);
                        }
                    }

                    //If raycast hits object or enemy with this tag...
                    if (shootLaserHit.collider.tag == ("GroundWallsCeiling"))
                    {
                        Debug.Log("hit wall / floor / ceiling");
                        //instantiate impact blood or decal at the location the 'hit' location
                        //GameObject impactFXdust = Instantiate(bulletImpactFX_Dust, shootLaserHit.point, Quaternion.identity);
                        bulletImpactFX_Dust.Stop();
                        bulletImpactFX_Dust.Clear();
                        bulletImpactFX_Dust.transform.position = shootLaserHit.point;
                        bulletImpactFX_Dust.Play();
                    }

                    //If raycast hits object or enemy with this tag...
                    if (shootLaserHit.collider.tag == ("ShootableProps"))
                    {
                        Debug.Log("hit prop");
                        //instantiate impact blood or decal at the location the 'hit' location
                        //GameObject impactFXdust = Instantiate(bulletImpactFX_Dust, shootLaserHit.point, Quaternion.identity);
                        bulletImpactFX_Dust.Stop();
                        bulletImpactFX_Dust.Clear();
                        bulletImpactFX_Dust.transform.position = shootLaserHit.point;
                        bulletImpactFX_Dust.Play();
                    }

                    //reset timer
                    timer = 0;

                    triggerPulled = true;
                }

                //When the player release the right trigger on the right controller...
                if (OVRInput.Get(_hand) < 0.10f)
                {
                    if (triggerPulled)
                    {
                        triggerPulled = false;
                    }

                    if (FullAuto == true)
                    {
                        _anim.SetBool("Automatic_Fire", false);
                    }

                    if (FullAuto == false)
                    {
                        _anim.SetBool("Fire", false);
                    }
                }
            }
        }

        if((script_throwGrabbable._grabbed == false) && (laserLine.enabled = true))
        {
            DisableLineRenderer();
        }
    }


    /// <summary>
    /// Coroutine with While loop for grabcheck.  
    /// Use iaction or iInterface on.
    /// 
    /// </summary>





    //Play particle systems for smoke, muzzleflash, and bullet casing...  Play Audio Method...
    private void FireGunParticles()
    {
        Debug.Log("Fired gun particles");
        _smoke.Play();
        _bulletCasing.Play();
        _muzzleFlashSide.Play();
        _Muzzle_Flash_Front.Play();
        GunFireAudio();
    }

    //play audio for gun shot sound
    private void GunFireAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }



    //disable laser
    void DisableLineRenderer()
    {
        laserLine.enabled = false;
    }
}