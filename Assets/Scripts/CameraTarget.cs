using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{

    private void OnEnable()
    {
        StackController.OnStackChange += RaiseCamera;
    }

    private void OnDisable()
    {
        StackController.OnStackChange -= RaiseCamera;
    }
    private Vector3 targetposition;
    void Start()
    {
        targetposition = new Vector3(0, 0, -10);
    }

    
    void Update()
    {
        transform.position = targetposition;
    }

    public void RaiseCamera(Transform cameraNewTarget)
    {

        if (cameraNewTarget.position.y > transform.position.y)
        { 
        targetposition.y = cameraNewTarget.position.y;
        }        
    }
}
