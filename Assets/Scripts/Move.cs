using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Graph graphPrefab;
    [SerializeField] private Node startNodePrefab;
    [SerializeField] private Node endNodePrefab;
    [SerializeField] private Transform[] objectList;
    [SerializeField] private Follow follow;


    private Vector3 pos;
    private Graph graph;
    private string clickObject;
    private Node start;
    private Node end;
    private Vector3 startPosition;
    private Vector3 middlePosition;
    private Vector3 endPosition;

    public Vector3 StartPosition => startPosition;

    public Vector3 MiddlePosition => middlePosition;

    public Vector3 EndPosition => endPosition;

    public string ClickObject => clickObject;

    private void Start()
    {
        graph = graphPrefab;
        start = startNodePrefab;
        end = endNodePrefab;

        startPosition = new Vector3(transform.position.x, transform.position.x, transform.position.z);
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

                    startPosition = endPosition;
                    FindNodeInClickedCube();
                    GetMiddlePosition();

                    endPosition = new Vector3(pos.x, pos.y, pos.z);

                    follow.StartGoing();
                }
            }
        }
    }

    private void GetMiddlePosition()
    {
        Path path = graph.GetShortestPath(start, end);
        for (int i = 0; i < path.nodes.Count; i++)
        {
            if (i == 1)
            {
                middlePosition = new Vector3(path.nodes[i].transform.position.x, path.nodes[i].transform.position.y,
                    path.nodes[i].transform.position.z);
            }
        }

        Debug.LogFormat("Path Length: {0}", path.length);
    }

    private void FindNodeInClickedCube()
    {
        foreach (Transform potentialTarget in objectList)
        {
            if (potentialTarget.name.Equals(clickObject))
            {
                start = end;
                end = potentialTarget.GetComponent<Node>();
            }
        }
    }
}