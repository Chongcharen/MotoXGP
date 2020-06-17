using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
   [SerializeField]Motorcycle_Controller mcc;
   public bool isAccelerate = false;
   public bool isLeft = false;
   public bool isRight = false;

   public bool isJump = false;

   public bool isBreak = false;



//    void UpdateInputControll(){
//        if(Input.GetKeyDown(KeyCode.D)){
//            isRight = true;
//        }
//        else if(Input.GetKeyUp(KeyCode.D)){
//            isRight = false;
//        }

//        if(Input.GetKeyDown(KeyCode.S)){
//            isLeft = true;
//        }
//        else if(Input.GetKeyUp(KeyCode.S)){
//            isLeft = false;
//        }

//        if(Input.GetKeyDown(KeyCode.Space)){
//            isJump = true;
//             mcc.Jump();
//        }
//        else if(Input.GetKeyUp(KeyCode.Space)){
//            isJump = false;
//        }

//        if(Input.GetKeyDown(KeyCode.J)){
//            isAccelerate = true;
          
//        }
//        else if(Input.GetKeyUp(KeyCode.J)){
//            isAccelerate = false;
//        }
//        if(Input.GetKeyDown(KeyCode.K)){
//            isBreak = true;
//        }
//        else if(Input.GetKeyUp(KeyCode.K)){
//            isBreak = false;
//        }
//    }
//    void UpdateMotorControll(){
//         mcc.accelerate = isAccelerate;
//         mcc.MyBreak = isBreak;
//         mcc.left = isLeft;
//         mcc.right = isRight;
        
//    }
//    void Update(){
//        UpdateInputControll();
//        UpdateMotorControll();
//    }
}
