using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorGenerator : MonoBehaviour
{
    private const float Present_Distance_Spawn_Next_Conveyor = 6f;

    [SerializeField] private Transform startingConveyor;
    [SerializeField] private List<Transform> conveyorList;

    private Vector3 lastEndPosition;

    private Transform highestInStack;

    private void OnEnable()
    {
        StackController.OnStackChange += NewHighestPoint;
    }

    private void OnDisable()
    {
        StackController.OnStackChange -= NewHighestPoint;
    }

    private void Awake()
    {
        lastEndPosition = startingConveyor.Find("EndPosition").position;
        highestInStack = FindObjectOfType<StackController>().gameObject.transform;
    
    }

    private void Update()
    {
        if (Vector3.Distance(highestInStack.position, lastEndPosition) < Present_Distance_Spawn_Next_Conveyor)
        {
            SpawnNextConveyor();
        }
    }

    private void NewHighestPoint(Transform newHighest)
    {
        highestInStack = newHighest;
    }

    private void SpawnNextConveyor()
    {
        Transform chosenConveyor = conveyorList[Random.Range(0, conveyorList.Count)];
        Transform lastConveyorTransform = SpawnConveyor(chosenConveyor, lastEndPosition);
        lastEndPosition = lastConveyorTransform.Find("EndPosition").position;
    
    }

    private Transform SpawnConveyor(Transform conveyorPart, Vector3 spawnPosition)
    {
        Transform lastConveyorTransform = Instantiate(conveyorPart, spawnPosition, Quaternion.identity);
        return lastConveyorTransform;
    }
}
