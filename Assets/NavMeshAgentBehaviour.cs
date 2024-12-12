using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentBehaviour : MonoBehaviour
{
    #region References

    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private Transform initialPosition;


    #endregion

    #region Runtime Variables

    private NavMeshAgent meshAgent;

    private bool entered;

    #endregion

    #region UnityMethods

    private void Start()
    {
        InitializeAgent();
        Invoke("PersecuteTarget", 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target") && !entered)
        {
            entered = true;
            meshAgent.isStopped = true;
            Debug.Log("OntriggerEnter - " + gameObject.name + " Detected Cheese");
            Invoke("PersecuteTarget", 3f);
            NewMazeGenerator.Instance.ResetMaze();
            TargetBehaviour.instance.ChooseRandomPosition();
        }
    }

    #endregion

    #region Runtime Methods

    private void InitializeAgent()
    {
        meshAgent = GetComponent<NavMeshAgent>();

        entered = false;
    }

    private void PersecuteTarget()
    {
        if(entered)
        {
            entered = false;
        }
        meshAgent.isStopped = false;
        meshAgent.SetDestination(targetGameObject.transform.position);
    }

    #endregion
}
