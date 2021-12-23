using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] float conveyorSpeed = 5f;
    [SerializeField] bool isInverted = true;


    private void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Present")
        {
            
            collision.gameObject.GetComponent<PresentLogic>().ConveyorDetails(isInverted, conveyorSpeed);
      
        }
    }


}
