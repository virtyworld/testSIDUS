using System.Collections.Generic;
using BezierSolution;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private Graph graphPrefab;
    [SerializeField] private Node startNodePrefab;
    [SerializeField] private Node endNodePrefab;
    [SerializeField] private Transform[] objectList;
    [SerializeField] private BezierTest bezierTest;
   
    

    private Vector3 pos;
    private Graph graph;
    private string clickObject;
    private Node start;
    private Node end;

    public Node StartNode => start;

    public Node EndNode => end;

    public string ClickObject => clickObject;

    private void Start()
    {
        graph = graphPrefab;
        start = startNodePrefab;
        end = endNodePrefab;
       
    }

    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                if (raycastHit.transform.gameObject.layer == 7)
                {
                    clickObject = raycastHit.transform.gameObject.name;
                    pos = raycastHit.point;
                    
                    FindCube();
                    GetPath();
                    
                    
                }
                
            }
        }

        //transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

    }

    private void GetPath()
    {
        Path path = graph.GetShortestPath ( start, end );
        for ( int i = 0; i < path.nodes.Count; i++ ) {
           
            if (i == 1)
            {
                Debug.Log ( path.nodes [i] );  
            }
        }
        Debug.LogFormat ( "Path Length: {0}", path.length );
    }
    
    private void FindCube()
    {
        foreach (Transform potentialTarget in objectList)
        {
            if (potentialTarget.name.Equals(clickObject))
            {
                start = end;
                end.GetComponent<BezierPoint>().enabled = false;
                end = potentialTarget.GetComponent<Node>();
                if (potentialTarget.GetComponent<BezierPoint>())
                {
                    potentialTarget.GetComponent<BezierPoint>().enabled = true;
                }
                else
                {
                    potentialTarget.gameObject.AddComponent<BezierPoint>();
                }

                transform.GetComponent<BezierWalkerWithSpeed>().Execute(2);
            }
        }
    
    }
}