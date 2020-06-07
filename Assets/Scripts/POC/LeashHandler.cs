using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeashHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startPosition;
    GameController controller;
    bool isLeft;
    bool isRight;
    [SerializeField]Vector3 handleingPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
