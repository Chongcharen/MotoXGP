using UnityEngine;
using System.Collections;

public class BodyTrigger : MonoBehaviour {
	public static bool finish = false;
	
	//used to play sounds
	public AudioClip bonesCrackSound;
	public AudioClip hitSound;
	public AudioClip oohCrowdSound;
	
	private AudioSource bonesCrackSC;
	private	AudioSource hitSC;
	private AudioSource oohCrowdSC;	
	

    private Motorcycle_Controller mcc;

    void Start()
	{
        mcc = GetComponentInParent<Motorcycle_Controller>();
        finish = false;
        
		
		//ignoring collision between biker's bodytrigger and motorcycle body
		Physics.IgnoreCollision (GetComponent<Collider>(), mcc.body.GetComponent<Collider>());
		
		//add new audio sources and add audio clips to them, used to play sounds
		bonesCrackSC = gameObject.AddComponent<AudioSource>();
		hitSC = gameObject.AddComponent<AudioSource>();
		oohCrowdSC = gameObject.AddComponent<AudioSource>();
		
		bonesCrackSC.playOnAwake = false;
		hitSC.playOnAwake = false;
		oohCrowdSC.playOnAwake = false;
		
		bonesCrackSC.rolloffMode = AudioRolloffMode.Linear;
		hitSC.rolloffMode = AudioRolloffMode.Linear;
		oohCrowdSC.rolloffMode = AudioRolloffMode.Linear;
		
		bonesCrackSC.clip = bonesCrackSound;
		hitSC.clip = hitSound;
		oohCrowdSC.clip = oohCrowdSound;
		//--------------------------------------------------
	}

    void OnTriggerEnter(Collider obj)
    {
        if (mcc == null)
            return;

        if (obj.tag == "Ground")
        {
            if (!mcc.crash)
            {
                mcc.crash = true;
                mcc.BikeCrash();

                //play sounds
                if (mcc.isLocalPlayer)
                {
                    bonesCrackSC.Play();
                    hitSC.Play();
                }
            }
        }

        if (!mcc.isLocalPlayer)
            return;
        
        if (obj.tag == "EndPoint" && !mcc.crash)//if entered in finish trigger
        {
            Debug.Log("End");
			finish = true;

            mcc.isControllable = false; //disable motorcycle controlling

			//disable rear wheel rotation
            mcc.rearWheel.freezeRotation = true;
            MotoUiGameplay.Instance.scorePanel.SetActive(true);
            //GameplayManager.Instance.LocalPlayerGoal();
            GetComponentInParent<AvatarSetup>().PlayerGoal();
        }
    }
    public bool isCrash = false;
    IEnumerator RespawnPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponentInParent<Motorcycle_Controller>().transform.position = GetComponentInParent<Motorcycle_Controller>().respawnPoint + Vector3.up;
        GetComponentInParent<Motorcycle_Controller>().transform.rotation = Quaternion.identity;
        isCrash = false;
    }
    private void SetPositionRespawn()
    {
        mcc.respawnPoint = mcc.body.transform.position;
    }
    private void OnTriggerStay(Collider obj)
    {
        if (mcc == null || !mcc.isLocalPlayer)
            return;

        if (obj.tag == "Fall")
        {
            Debug.Log(obj.tag);
            Vector3 pos = obj.GetComponent<FallManager>().respawnPoint.position;
            mcc.respawnPoint = new Vector3(pos.x, pos.y, mcc.transform.position.z);
            if(mcc.die)
            {
                //SmoothFollow.Instance.playerfall = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (mcc == null || !mcc.isLocalPlayer)
            return;

       /* if (other.tag == "PanCam")
        {
            var ac = other.GetComponent<AreaPanCamera>();
            if (ac)
            {
                SmoothFollow.Instance.PanCam(ac.anglePan, false);
            }
        }*/
    }
    private void OnCrash()
    {

    }
}