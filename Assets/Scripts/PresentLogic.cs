using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentLogic : MonoBehaviour
{
    Rigidbody2D rbody2d;

    private HashSet<GameObject> conveyorsTouched;

    private StackController stackController;

    private bool isCaught = false;
    private bool isInverted = false;
    private bool leftConveyor = false;
    private bool isInStack = false;

    [SerializeField]float flightSpeed = 10f;
    public float conveyorSpeed = 2;


    private void Awake()
    {
        rbody2d = GetComponent<Rigidbody2D>();
        stackController = FindObjectOfType<StackController>();

        RefreshPresent();
    }
    public void RefreshPresent()
    { 
        isCaught = false;
        isInStack = false;
        leftConveyor = false;
        rbody2d.isKinematic = false;
        rbody2d.constraints = RigidbodyConstraints2D.None;
        rbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        conveyorsTouched = new HashSet<GameObject>();
        if (rbody2d == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }

    }


    private void Update()
    {
        if (conveyorsTouched.Count > 0)
        {

            if (isInverted == false)
            {
                transform.position += transform.right * (conveyorSpeed * Time.deltaTime);

            }
            else
            {
                transform.position -= transform.right * (conveyorSpeed * Time.deltaTime);

            }
        }

        else if (conveyorsTouched.Count == 0 && isCaught == false && leftConveyor == true)
        {
            if (isInverted == false)
            {
                transform.position += transform.right * (flightSpeed * Time.deltaTime);

            }
            else
            {
                transform.position -= transform.right * (flightSpeed * Time.deltaTime);

            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision )
    { 
         if(collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                rbody2d.constraints = RigidbodyConstraints2D.FreezePositionX;
                rbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                isCaught = true;
                collision.gameObject.GetComponentInParent<PlayerController>().CanClimb();
            }
            else if (collision.transform.tag == "Stack Controller" || collision.transform.parent.tag == "Stack Controller")
            {
                if (isCaught == true && isInStack == false)
                {
                    stackController.AddToStack(transform);
                    transform.gameObject.tag = "Stack Controller";
                    isInStack = true;
                    Destroy(rbody2d);
                }

                else
                {
                    gameObject.SetActive(false);
                }


            }
            else
            {
                return; ;
            }
        }
    }

    public void ConveyorDetails(bool invertStatus, float sentSpeed)
    {
        isInverted = invertStatus;
        sentSpeed = conveyorSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Conveyor")
        {
            conveyorsTouched.Add(collision.gameObject);
            leftConveyor = false;
        }
        if (collision.gameObject.tag == "Player")
        {
                rbody2d.constraints = RigidbodyConstraints2D.FreezePositionX;
                rbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                isCaught = true;
                collision.gameObject.GetComponentInParent<PlayerController>().CanClimb();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Conveyor")
        {
            conveyorsTouched.Remove(collision.gameObject);
            if (conveyorsTouched.Count == 0)
            {
                leftConveyor = true;
            }

        }
    }

}
