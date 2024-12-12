using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] targetPositions;

    public static TargetBehaviour instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        ChooseRandomPosition();
    }

    public void ChooseRandomPosition()
    {
        Debug.Log("Cheese Random Pos Activated");
        Vector3 position = targetPositions[Random.Range(0, targetPositions.Length)].position;
        if(transform.position != position)
        {
            transform.position = position;
        }
        else
        {
            ChooseRandomPosition();
        }
    }

}
