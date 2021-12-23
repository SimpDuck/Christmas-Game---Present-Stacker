using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StackController : MonoBehaviour
{
    public delegate void StackChange(Transform topOfQueue);
    public static event StackChange OnStackChange;


    public float stackSize = 0f;
    [SerializeField] GameObject cameraTarget;

    private GameSession gameSession;


    public void Start()
    {
        ResetStack();
        gameSession = FindObjectOfType<GameSession>();
              
    }



    public void AddToStack(Transform newTransform)
    {
        if (newTransform != null)
        {
            newTransform.SetParent(transform);
            
            stackSize++;
            UpdateStackPosition();
            gameSession.AddToScore(1);
        }
        else 
        {
            return;
        }

        if (stackSize == 1)
        {

            newTransform.position = transform.position;
        }
    }

    public void ResetStack()
    {
        stackSize = 0;
        cameraTarget.transform.position = new Vector2(0, 0);
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

    }


    private void UpdateStackPosition()
    {
       
            Transform cameranewTarget = GetTopOfQueue();
            OnStackChange(cameranewTarget);

    }

    private Transform GetTopOfQueue()
    {
        var topMost = transform.Cast<Transform>().OrderBy(t => t.position.z).Last();
        return topMost;

    }

}
