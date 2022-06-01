using System;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Transform[] objectList;
    [SerializeField] private Move movePrefab;

    private Move moveScript;
    void Start()
    {
        moveScript = movePrefab;
    }

    private void FixedUpdate()
    {
       
    }

    public Transform GetClosestEnemy ()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        float closestDistanceSqr1 = Mathf.Infinity;
        Vector3 currentPosition = FindObjectInList();
        foreach(Transform potentialTarget in objectList)
        {
            if (currentPosition == potentialTarget.position) continue;
            
            Vector3 directionToFinishTarget = potentialTarget.position - currentPosition;
            float dSqrToFinishTarget = directionToFinishTarget.sqrMagnitude;
            
            Vector3 directionToTarget1 = potentialTarget.position - transform.position;
            float dSqrToTarget1 = directionToTarget1.sqrMagnitude;
            
            if(dSqrToFinishTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToFinishTarget;
                bestTarget = potentialTarget;
            }
            
        }
        
         
        return bestTarget;
    }

    private Vector3 FindObjectInList()
    {
        foreach (Transform potentialTarget in objectList)
        {
            if (potentialTarget.gameObject.name.Equals(moveScript.ClickObject))
            {
                return potentialTarget.position;
            }
        }

        return transform.position;
    }
    
    
}