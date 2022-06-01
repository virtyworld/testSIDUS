using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private Move move;

    private int routeToGo;
    private float tParam;
    private Vector3 objectPosition;
    private float speedModifier;
    private bool coroutineAllowed;

    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    public void StartGoing()
    {
        StartCoroutine(GoByTheRoute());
    }

    private IEnumerator GoByTheRoute()
    {
        coroutineAllowed = false;

        Vector3 p0 = move.StartPosition;
        Vector3 p1 = move.MiddlePosition;
        Vector3 p2 = move.MiddlePosition;
        Vector3 p3 = move.EndPosition;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                             3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            transform.LookAt(objectPosition);
            yield return new WaitForEndOfFrame();
        }

        tParam = 0;
        speedModifier = speedModifier * 0.90f;
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;
    }
}