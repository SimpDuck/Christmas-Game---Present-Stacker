using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleConveyor : MonoBehaviour
{
    [SerializeField] GameObject mainConveyor;
    [SerializeField] GameObject presentSpawner;
    Renderer _renderer;

    private bool shouldSpawn;

    PresentSpawner presentSpawn;

    public bool isCloseEnough = false;

    private void Start()
    {
        _renderer = mainConveyor.GetComponentInChildren<SpriteRenderer>();
        presentSpawn = presentSpawner.GetComponent<PresentSpawner>();
        shouldSpawn = true;
        isCloseEnough = false;
    }

    private void OnEnable()
    {
        StackController.OnStackChange += ActiveCheck;
    }

    private void OnDisable()
    {
        StackController.OnStackChange -= ActiveCheck;
    }

    private void FixedUpdate()
    {
        if (_renderer.isVisible )
        {
            if (shouldSpawn == true)
            {
                MakeActive();
            }

            else
            {
                return;
            }

        }
        else
        {
            MakeNonActive();
        }
    }

    public void ActiveCheck(Transform highestPresent)
    {
        if (highestPresent.position.y >= (transform.position.y -1))
        {
            shouldSpawn = false;
            presentSpawn.stopSpawning();
        }
        else
        {
            if (highestPresent.position.y < transform.position.y )
            {
                isCloseEnough = true;
                shouldSpawn = true;
                MakeActive();
            }
        }
       
    }

    private void MakeActive()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            presentSpawn.startSpawning();
            
        }
    }

    private void MakeNonActive()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

}
