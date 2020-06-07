using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Motorcycle_Controller : MonoBehaviour
{
    //if this is activated the controlls will be got from touches else it'll be keyboard or joystick buttons


    public bool is2D = false;
    public bool hasRagdoll = true;
    public bool usingAccelerometer = false;

    public bool MyThooo;
    public bool MyBreak;
    public bool RollLeft;
    public bool RollRight;

    //used to determine when player is crashed
    public bool crash = false;
    public bool crashed = false;

    //used to enable/disable motorcycle controlling
    public bool isControllable = true;

    //used to count scores
    public static int score = 0;

    //used to change motorcycle characteristics
    public Rigidbody body;
    public Rigidbody frontFork;
    public Rigidbody rearFork;
    public Rigidbody frontWheel;
    public Rigidbody rearWheel;

    public float speed = 60.0f;
    public float groundedWeightFactor = 20.0f;
    public float inAirRotationSpeed = 10.0f;
    public float wheelieStrength = 15.0f;
    public float forcebike = 200f;
    public int gearBike = 3;
    private bool isCooldownForceGear = false;
    public float cooldownTimeForceBike = 10f;
    public float forceJump = 20000f;
    public float forceBoost = 200f;
    public bool isForceBoost = false;

    //used to make biker detach from bike when crashed
    public HingeJoint leftHand;
    public HingeJoint rightHand;
    public HingeJoint leftFoot;
    public HingeJoint rightFoot;
    public ConfigurableJoint hips;

    bool fixedZ;
    float fixTime;

    //used for lean backward/forward, changes hips' targetposition value
    public Vector3 leanBackwardTargetPosition;
    public Vector3 leanForwardTargetPosition;
    public Vector3 leanDefaultTargetPosition;
    public float leanSpeed = 2.0f;

    //used to start/stop dirt particles
    //public ParticleSystem dirt;


    //used to determine if motorcycle is grounded or in air
    private RaycastHit hit;
    public bool onGround = false;
    public bool inAir = false;
    private bool touchGround = false;
    public bool inSand = false;
    public bool die = false;
    public bool playerStart;

    //used to manipulate engine sound pitch
    public AudioSource audioSource;
    private float pitch;

    //used to determine when flip is done
    private bool flip = false;

    //used for knowing input	
    public bool accelerate = false;
    public bool brake = false;
    public bool left = false;
    public bool right = false;
    public bool leftORright = false;
    
    [Header("Moto Variable")]
    public bool isLocalPlayer = false;
    public bool isBot = false;
    public string namePlayer = "Player";
    public GameObject frameObj;
    public ParticleSystem nitroObj;


    public bool isFreeCrashed = false;

    public Vector3 startSpawnPoint;
    public Vector3 respawnPoint;
    public float distanceRadiusRearWheel = 0;
    

    private void Start()
    {
        startSpawnPoint = transform.position;
        respawnPoint = startSpawnPoint;
        InitMoto();
    }

    public void InitMoto()
    {
        distanceRadiusRearWheel = rearWheel.GetComponent<SphereCollider>().radius;
        brake = true;

        rearWheel.GetComponent<Rigidbody>().maxAngularVelocity = speed;
        frontWheel.GetComponent<Rigidbody>().maxAngularVelocity = speed;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Ragdoll"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Motorcycle"), true);
        
        crash = false;
        crashed = false;
        isControllable = true;

        Physics.IgnoreCollision(frontWheel.GetComponent<Collider>(), body.GetComponent<Collider>());
        Physics.IgnoreCollision(rearWheel.GetComponent<Collider>(), body.GetComponent<Collider>());
        //SetStatOnStart();

        BikeStateLevel01();
    }

    public void SetStartUpdateController()
    {
        StartCoroutine(UpdateController(.1f));
    }
    private void Update()
    {
        if (isControllable)
        {
            pitch = rearWheel.angularVelocity.sqrMagnitude / speed;
            pitch *= Time.deltaTime * 2;
            pitch = Mathf.Clamp(pitch + 1, 0.5f, 1.8f);
            //if (accelerate)
            //{
            //}
            //else
            //    pitch = Mathf.Clamp(pitch - Time.deltaTime * 2, 0.5f, 1.8f);

            pitch = Mathf.Clamp(pitch - Time.deltaTime * 2, 0.5f, 1.8f);
            audioSource.pitch = pitch;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

     }

    private IEnumerator UpdateController(float delay)
    {
        while (true)
        {
            if (isControllable)
            {
                if (body.rotation.eulerAngles.z > 210 && body.rotation.eulerAngles.z < 220)
                    flip = true;

                if (body.rotation.eulerAngles.z > 320 && flip) //backflip is done
                {
                    flip = false;
                    //scoreText.text = "SCORE : " + score;
                }

                if (body.rotation.eulerAngles.z < 30 && flip) //frontflip is done
                {
                    flip = false;
                    //scoreText.text = "SCORE : " + score;					
                }

                //if any horizontal key (determined in edit -> project settings -> input)  is pressed or if "formobile" is activated, left or right buttons are touched or accelerometer is used

                //changing engine sound pitch depending rear wheel rotational speed
                if (accelerate)
                {
                    pitch = rearWheel.angularVelocity.sqrMagnitude / speed;
                    pitch *= Time.deltaTime * 2;
                    pitch = Mathf.Clamp(pitch + 1, 0.5f, 1.8f);
                }
                else
                    pitch = Mathf.Clamp(pitch - Time.deltaTime * 2, 0.5f, 1.8f);

                if (crash && !crashed) //if player just crashed
                {
                    Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Ragdoll"), false);

                    if (!is2D) //disable all physics constraints if 2D isn't activated for motorcycle in inspector menu, so physics calculation will occur on all axis
                    {
                        if (isFreeCrashed)
                            body.constraints = RigidbodyConstraints.None;
                        frontFork.constraints = RigidbodyConstraints.None;
                        frontWheel.constraints = RigidbodyConstraints.None;
                        rearFork.constraints = RigidbodyConstraints.None;
                        rearWheel.constraints = RigidbodyConstraints.None;
                    }

                    crashed = true;
                    isControllable = false;
                }

                pitch = Mathf.Clamp(pitch - Time.deltaTime * 2, 0.5f, 1.8f);
                audioSource.pitch = pitch;
            }
            yield return new WaitForSeconds(delay);
        }
    }

    public void BikeCrash()
    {
        if (crash && !crashed) //if player just crashed
        {
            respawnPoint = transform.position;
            RespawnAfterCrash();

            die = true;
            GetComponentInChildren<RagdollPlayer>().OnCrash();
            Destroy(leftHand);
            Destroy(rightHand);
            Destroy(leftFoot);
            Destroy(rightFoot);
            Destroy(hips);
            var ragdoll = GetComponentInChildren<RagdollPlayer>();
            if (ragdoll)
            {
                ragdoll.OnCrash();
                ragdoll.transform.SetParent(null);
            }

            //turn on collision between ragdoll and motorcycle
            //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Motorcycle"), LayerMask.NameToLayer("Ragdoll"), false);

            if (isFreeCrashed)
                body.constraints = RigidbodyConstraints.None;
            frontFork.constraints = RigidbodyConstraints.None;
            frontWheel.constraints = RigidbodyConstraints.None;
            rearFork.constraints = RigidbodyConstraints.None;
            rearWheel.constraints = RigidbodyConstraints.None;

            crashed = true;
            isControllable = false;
        }
    }
    
    //physics are calculated in FixedUpdate function
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.position = startSpawnPoint;
        }
        //if (NetworkManagerGameplay.Instance != null || !GetComponent<Player>().isLocalPlayer)

        if (isControllable)
        {
            if (accelerate && onGround)
            {
                rearWheel.freezeRotation = false; //allow rotation to rear wheel
                rearWheel.AddTorque(new Vector3(0, 0, -speed * Time.deltaTime), ForceMode.VelocityChange);
                brake = false;
                //dirt.transform.position = rearWheel.position;
                //allign dirt to rear wheel
                transform.Translate(Vector3.right * speed / 18 * Time.deltaTime);
                float f = speed * 3f;
                body.AddForce(Vector3.right * f);
                playerStart = true;
                /*
                if (onGround)//if motorcycle is standing on object tagged as "Ground"
                {

                    if (!dirt.isPlaying)
                        dirt.Play(); //play dirt particle

                    dirt.transform.position = rearWheel.position; //allign dirt to rear wheel

                }
                else dirt.Stop();
                            */
            }

            if (brake)
            {
                rearWheel.freezeRotation = true;
                //frontWheel.freezeRotation = true;
            }
            else
            {
                rearWheel.freezeRotation = false; //enable rotation for rear wheel if player isn't braking
                //frontWheel.freezeRotation = false;
            }

            //bool isMobile = gm.isPlatformMobile;
            bool isMobile = false;

            if (left)
            { //left horizontal key (determined in edit -> project settings -> input) is pressed or left button is touched on mobile if "formobile" is activated
                if (!inAir)
                {
                    body.AddTorque(new Vector3(0, 0, (isMobile ? Mathf.Abs(Input.acceleration.x) : 1) * groundedWeightFactor * 100 * Time.deltaTime));
                    body.AddForceAtPosition(body.transform.up * Mathf.Abs(Input.acceleration.x) * wheelieStrength * body.velocity.sqrMagnitude / 100,
                        new Vector3(frontWheel.position.x, frontWheel.position.y - 0.5f, body.transform.position.z));//add wheelie effect
                }
                else
                {
                    body.AddTorque(new Vector3(0, 0, (isMobile ? Mathf.Abs(Input.acceleration.x) : 1) * inAirRotationSpeed * 100 * Time.deltaTime));
                }
                if (hips)
                    hips.targetPosition = Vector3.Lerp(hips.targetPosition, leanBackwardTargetPosition, leanSpeed * Time.deltaTime);
            }
            else if (right)
            {
                if (!inAir)
                {
                    body.AddTorque(new Vector3(0, 0, (isMobile ? Mathf.Abs(Input.acceleration.x) : 1) * -groundedWeightFactor * 100 * Time.deltaTime));
                }
                else
                {
                    body.AddTorque(new Vector3(0, 0, (isMobile ? Mathf.Abs(Input.acceleration.x) : 1) * -inAirRotationSpeed * 100 * Time.deltaTime));
                }
                if (hips)
                    hips.targetPosition = Vector3.Lerp(hips.targetPosition, leanForwardTargetPosition, leanSpeed * Time.deltaTime);
            }
            else
            {
                if (inAir)
                {
                    if (hips)
                        hips.targetPosition = Vector3.Lerp(hips.targetPosition, leanForwardTargetPosition, (leanSpeed - 2f) * Time.deltaTime);
                }
                else
                {
                    if (hips)
                        hips.targetPosition = Vector3.Lerp(hips.targetPosition, leanDefaultTargetPosition, (leanSpeed - 1.5f) * Time.deltaTime);
                }
            }

            if (!left && !right)
            {

                body.ResetInertiaTensor();
            }

            if (Physics.Raycast(rearWheel.position, -body.transform.up, out hit, distanceRadiusRearWheel + .1f))
            {
                if (hit.collider.tag == "Ground")
                {
                    if (!touchGround && !onGround)
                    {
                        touchGround = true;
                        StartCoroutine("OnBumpGround");
                    }
                    onGround = true;
                }
                else
                    onGround = false;

                inAir = false;
            }
            else
            {
                touchGround = false;
                onGround = false;
                inAir = true;
            }

        }
        //else dirt.Stop();

        FixedRotate();
    }

    private IEnumerator OnBumpGround()
    {
        float time = .3f;
        float curTime = 0f;
        while (curTime <= time)
        {
            curTime += Time.deltaTime;
            if (hips)
                hips.targetPosition = Vector3.Lerp(hips.targetPosition, leanBackwardTargetPosition, leanSpeed * 2 * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        touchGround = false;
    }

    public void RespawnAfterCrash()
    {
        StartCoroutine(SetBeforeStart(.5f));
    }

    public void RespawnPlayer()
    {
        StartCoroutine(SetBeforeStart(0f));
    }

    private IEnumerator SetBeforeStart(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponentInChildren<AvatarSetup>().ClearRagdollPlayer();

        Rigidbody[] rbs = { frontFork, rearFork, frontWheel, rearWheel };

        body.transform.rotation = Quaternion.identity;
        body.transform.position = respawnPoint + Vector3.up ;

        body.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        foreach (var rb in rbs)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }

        GetComponent<AvatarSetup>().CreateRagdoll();
        body.isKinematic = true;
        yield return new WaitForSeconds(.1f);
        die = false;
        body.isKinematic = false;
    }

    public void LowGearForceBike()
    {
        if (!inAir && gearBike > 0)
        {
            gearBike--;
            StartCoroutine(ForceBike(.5f));

            if (!isCooldownForceGear && gearBike <= 0)
                StartCoroutine(CoolDownForceBike());
        }
    }
    public void MakeLowForce()
    {
        if (!inAir)
        {
            StartCoroutine(ForceBike(.5f));

            if (!isCooldownForceGear)
                StartCoroutine(CoolDownForceBike());
        }
    }
    private IEnumerator ForceBike(float time)
    {
        //frontFork.AddExplosionForce(forcebike, frontFork.transform.position - Vector3.right, 3f);
        //rearWheel.AddExplosionForce(forcebike, rearWheel.transform.position - Vector3.right, 3f);
        body.AddForce(forceBoost * Vector3.right * speed);
        //GameplayManager.Instance.speedLineObj.SetActive(true);
        yield return new WaitForSeconds(time);
        //GameplayManager.Instance.speedLineObj.SetActive(false);
        //SmoothFollow.Instance.SetShutterSpeed(false);
    }


    private IEnumerator CoolDownForceBike()
    {
        isCooldownForceGear = true;
        yield return new WaitForSeconds(cooldownTimeForceBike);
        gearBike = 3;

        if (gearBike <= 0)
            StartCoroutine(CoolDownForceBike());
        else
            isCooldownForceGear = false;
    }

    public void Jump()
    {
        if (onGround)
        {
            //frontFork.AddExplosionForce(forceJump, frontFork.transform.position - Vector3.up, 2f);
            //rearWheel.AddExplosionForce(forceJump, rearWheel.transform.position - Vector3.up, 2f);

            body.AddForce(transform.up * forceJump * rearWheel.mass * frontWheel.mass);

            if (accelerate)
            {
                body.AddForce(transform.forward * speed);
            }
        }
    }
    
    void FixedRotate()
    {

        if (!onGround && !leftORright)
        {
            bool isMobile = false;
            if (this.gameObject.transform.eulerAngles.z < -50f)
            {
                body.AddTorque(new Vector3(0, 0, (isMobile ? Mathf.Abs(Input.acceleration.x) : 1) * -inAirRotationSpeed * 100 * Time.deltaTime / 5));
            }

            if (this.gameObject.transform.eulerAngles.z > 50f)
            {
                body.AddTorque(new Vector3(0, 0, (isMobile ? Mathf.Abs(Input.acceleration.x) : 1) * inAirRotationSpeed * 100 * Time.deltaTime / 5));
            }

            if (fixTime < 0.025f)
            {
                fixedZ = true;
            }
        }

        else
        {
            fixedZ = false;
            fixTime = 0f;
        }

        if (fixedZ)
        {
            body.constraints = RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationZ |
            RigidbodyConstraints.FreezePositionZ;
            fixTime += Time.deltaTime;

            if (fixTime > 0.025f)
            {
                fixedZ = false;
                fixTime = 0.025f;
            }
        }
        else
        {
            body.constraints = RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezePositionZ;
        }

    }
    
    public void ForceBoost(float time)
    {
        if (!isForceBoost)
            StartCoroutine(Boost(time));
        //body.AddForce(forceBoost * Vector3.right);
    }
    private IEnumerator Boost(float time)
    {
        isForceBoost = true;
        nitroObj.Play();
        //.Instance.SetBoostSpeedLine(time + 1);
        //RimlightOnNitroState rns = GetComponent<RimlightOnNitroState>();
        //rns.RimlightState(true);
        float currentTime = 0;
        while (currentTime <= time)
        {
            //yield return new WaitForSeconds(Time.deltaTime);
            yield return null;
            Force(forceBoost);
            currentTime += Time.deltaTime;
        }
        //nitroObj.SetActive(false);
        isForceBoost = false;
        //rns.RimlightState(false);
    }
    public void Force(float power)
    {
        body.AddForce(power * Vector3.right);
    }
    /*
    void SetStatOnStart()
    {
        if (SceneManager.GetActiveScene().name != "AIRecord")
        {
            speed = LobbyGameManager.Instance.myAccount.speed;
            frontWheel.GetComponent<Rigidbody>().maxAngularVelocity = LobbyGameManager.Instance.myAccount.speed;
            rearWheel.GetComponent<Rigidbody>().maxAngularVelocity = LobbyGameManager.Instance.myAccount.speed;

            frontWheel.mass = speed / 10;
            rearWheel.mass = speed / 10;

            groundedWeightFactor = LobbyGameManager.Instance.myAccount.controllGround;
            inAirRotationSpeed = LobbyGameManager.Instance.myAccount.controllAir;

            wheelieStrength = 30;
            forcebike = LobbyGameManager.Instance.myAccount.gearLow;
            forceJump = LobbyGameManager.Instance.myAccount.jump;
            forceBoost = LobbyGameManager.Instance.myAccount.froceNitro;
        }

        else
        {
            BikeStateLevel01();
        }

    }
    */
    public void BikeStateLevel01()
    {
        frontWheel.GetComponent<Rigidbody>().maxAngularVelocity = 89f;
        rearWheel.GetComponent<Rigidbody>().maxAngularVelocity = 89f;
    }
    
    public void Freeze()
    {
        var rbs = GetComponentInChildren<RagdollPlayer>().GetComponentsInChildren<Rigidbody>();
        foreach (var item in rbs)
        {
            if(item.name != "FrontWheel" || item.name != "RearWheel")
                item.isKinematic = true;
            Debug.Log("F");
        }
    }
    public void UnFreeze()
    {
        var rbs = GetComponentsInChildren<Rigidbody>();
        foreach (var item in rbs)
        {
            item.isKinematic = false;
        }
    }
    /*
[Header("Trap")]
public Transform spawnPointTrap;
public GameObject missilePrefab;

public void SpawnTrap(Trap trap)
{
    var trapControl = Instantiate(trap.prefab, spawnPointTrap.position, spawnPointTrap.rotation, null);
}*/
}