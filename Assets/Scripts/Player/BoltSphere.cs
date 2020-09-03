using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
[RequireComponent(typeof(GameController))]
public class BoltSphere : EntityBehaviour<IPlayerBikeState>
{
    float accel; 
    GameController controller;
    private void Awake() {
            
    }
    public override void Attached(){
        Debug.Log("Sphere Attached");
        state.SetTransforms(state.Transform, transform);
        state.AddCallback("Transform",()=> transform.position = state.Transform.Position);
        controller = GetComponent<GameController>();
    }
    public override void ControlGained(){
        VirtualPlayerCamera.Instantiate();
        VirtualPlayerCamera.instance.FollowTarget(transform);
        VirtualPlayerCamera.instance.LookupTarget(transform);
    }
    void Update(){
        PollKey();
    }
    void PollKey(){
        accel = controller.accelerator;
        if(controller.brake)
            accel = 0;
        if(controller.isLeft)
        accel = -1;
        // brake = motorControl.brake;
        // jump = motorControl.isJump;
        // isLeft = motorControl.isLeft;
        // isRight = motorControl.isRight;
    }
    public override void SimulateController(){
        PollKey();
        IBikePlayerCommandInput input = BikePlayerCommand.Create();
        input.accel = accel;
        entity.QueueInput(input);
        //UpdateTransform();
    }
    public override void SimulateOwner(){
        //UpdateTransform();
    }
    public override void MissingCommand(Command previous){
        
    }
    public override void ExecuteCommand(Command command, bool resetState){
        BikePlayerCommand cmd = (BikePlayerCommand)command;
        if(resetState){
            // print(Depug.Log("resetState  "+resetState,Color.white));
            // print(Depug.Log("cmd .Accel  "+cmd.Input.accel,Color.yellow));
            // print(Depug.Log("cmd.Result.Position  "+cmd.Result.Position,Color.yellow));
            // print(Depug.Log("cmd.Result.Rotation  "+cmd.Result.Rotation,Color.yellow));
            // print(Depug.Log("cmd.Result.Velocity  "+cmd.Result.Velocity,Color.yellow));
            // print(Depug.Log("cmd.Result.accel  "+cmd.Input.accel,Color.yellow));
            //transform.position = cmd.Result.Position;
            //transform.rotation = cmd.Result.Rotation;
            //Rigidbody.velocity = cmd.Result.Velocity;
            // accel = cmd.Input.accel;
            // brake = cmd.Input.brake;
            // jump = cmd.Input.jump;
            // isLeft = cmd.Input.left;
            // isRight = cmd.Input.right;
            // UpdateWheel();

           // transform.localPosition = cmd.Result.Position;
            //transform.localRotation = cmd.Result.Rotation;
            //transform.rotation = cmd.Result.Rotation;
            //playerState.position = cmd.Result.Position;
            //playerState.rotation = cmd.Result.Rotation;
            //accel = cmd.Input.accel;
            
           // transform.position = Vector3.Lerp(transform.position,cmd.Result.Position,1);
            
        }else{
            
            accel = cmd.Input.accel;
            // if(entity.IsOwner){
            //     print(Depug.Log("UpdateTransform  "+resetState,Color.white));
            // }
            //UpdateTransform();
            // brake = cmd.Input.brake;
            // jump = cmd.Input.jump;
            // isLeft = cmd.Input.left;
            // isRight = cmd.Input.right;
            // UpdateWheel();
            // cmd.Result.Position = transform.position;
            // cmd.Result.Rotation = transform.rotation;
            // cmd.Result.Velocity = Rigidbody.velocity;
            
            //cmd.Result.Position = transform.position;
            //cmd.Result.Rotation = transform.rotation;
            UpdateTransform();
            cmd.Result.Position = transform.localPosition;
            cmd.Result.Rotation = transform.localRotation;
        }
        //UpdateTransform();
    }

    void UpdateTransform(){
        //Debug.Log("UpdateTransform "+entity.NetworkId);
        transform.Translate(Vector3.forward*accel*2*BoltNetwork.FrameDeltaTime);
        //GetComponent<Rigidbody>().AddTorque(-Vector3.forward*accel*2);
    }
}
