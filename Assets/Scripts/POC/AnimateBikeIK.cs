using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBikeIK : MonoBehaviour
{
    // Start is called before the first frame update
    public bool ikActive;
    [SerializeField]Animator animator;
    [SerializeField]AbikeChopSystem bikeSystem;
    public IKPointsClass IKPoints;
    public Transform myBike;
    public Transform player;
    private Rigidbody bikeRigidbody;
    private float speed = 0.0f;
    bool grounded = true;
    
    [System.Serializable]
    public class IKPointsClass
    {
        public Transform rightHand, leftHand;
        public Transform rightFoot, leftFoot;
    }
    void Awake(){
        DisableRagdoll(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = bikeSystem.speed;
        grounded = bikeSystem.grounded;
        animator.SetFloat("speed", speed);
        animator.SetBool("grounded", grounded);
    }
     void DisableRagdoll(bool active)
    {


        Component[] Rigidbodys = player.GetComponentsInChildren(typeof(Rigidbody));

        foreach (Rigidbody RigidbodyChild in Rigidbodys)
        {
            RigidbodyChild.isKinematic = !active;
        }


        Component[] Colliders = player.GetComponentsInChildren(typeof(Collider));

        foreach (Collider ColliderChild in Colliders)
        {
            ColliderChild.enabled = active;
        }

    }
    void OnAnimatorIK()
    {




        if (player.GetComponent<Animator>().enabled != true) return;




        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {


                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);



                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1.0f);

                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1.0f);




                if (IKPoints.leftHand != null)
                {
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, IKPoints.leftHand.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, IKPoints.leftHand.rotation);


                }


                if (speed > -1)
                {

                    //set the position and the rotation of the right hand where the external object is
                    if (IKPoints.rightHand != null)
                    {
                        animator.SetIKPosition(AvatarIKGoal.RightHand, IKPoints.rightHand.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, IKPoints.rightHand.rotation);
                    }

                    if (IKPoints.rightFoot != null)
                    {
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, IKPoints.rightFoot.position);
                        animator.SetIKRotation(AvatarIKGoal.RightFoot, IKPoints.rightFoot.rotation);
                    }

                    if (IKPoints.leftFoot != null && speed > 30.0f)
                    {

                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, IKPoints.leftFoot.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftFoot, IKPoints.leftFoot.rotation);
                    }


                }
            }

                //if the IK is not active, set the position and rotation of the hand back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }
}
