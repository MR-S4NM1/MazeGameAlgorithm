using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMazeGenerator : MonoBehaviour
{
    public Vector2Int mazeSize;
    public int maxCost;
    [SerializeField]
    float m_timeBetweenMazes;
    public GameObject wallPrefab;
    private bool m_up, m_move, m_arrived;
    private RandomCostGraph graph;
    private MST mst;

    private void Start()
    {
        InvokeRepeating("CreateMaze", 0.1f, m_timeBetweenMazes);
    }

    private void FixedUpdate()
    {
        if (m_move)
        {
            if (m_up)
            {
                transform.position += 1.5f * Vector3.down * Time.fixedDeltaTime;
            }
            else
            {
                transform.position += 1.5f * Vector3.up * Time.fixedDeltaTime;
            }
        }
        if (!m_arrived)
        {
            if (transform.position.y > 0 || transform.position.y < -1)
            {
                m_arrived = true;
                m_move = false;
                m_up = !m_up;
            }
        }
        if (transform.position.y < 0 && transform.position.y > -1)
        {
            m_arrived = false;
        }
    }


    private void MoveMaze()
    {
        m_move = true;
    }

    private void CreateMaze()
    {
        CreateGraph();
        CreateMST();
        CreateMazeWalls();
        CreateMazeBorders();
        transform.Rotate(new Vector3(-90, 0, 0));
        MoveMaze();
        Invoke("MoveMaze", m_timeBetweenMazes - 1.5f);
        Invoke("ClearMaze", m_timeBetweenMazes - 0.7f);
    }

    void CreateGraph()
    {
        graph = new RandomCostGraph(mazeSize, maxCost);
        graph.CreateGraph();
    }

    void CreateMST()
    {
        mst = new MST(graph);
        mst.InitAlgorithm();
        mst.MST_Algorithm();
    }

    void CreateMazeBorders()
    {

        for (int i = 0; i < mazeSize.x; i++)
        {
            Vector2 posBottom = new Vector2(i, -0.5f);
            Vector2 posTop = new Vector2(i, mazeSize.y - 0.5f);
            GameObject bottom = Instantiate(wallPrefab, posBottom, Quaternion.identity, transform);
            GameObject top = Instantiate(wallPrefab, posTop, Quaternion.identity, transform);
            bottom.transform.localScale = new Vector3(1, 0.1f, 1);
            top.transform.localScale = new Vector3(1, 0.1f, 1);
        }

        for (int j = 0; j < mazeSize.y; j++)
        {
            Vector2 posLeft = new Vector2(-0.5f, j);
            Vector2 posRight = new Vector2(mazeSize.x - 0.5f, j);

            GameObject left = Instantiate(wallPrefab, posLeft, Quaternion.identity, transform);
            GameObject right = Instantiate(wallPrefab, posRight, Quaternion.identity, transform);

            left.transform.localScale = new Vector3(0.1f, 1, 1);
            right.transform.localScale = new Vector3(0.1f, 1, 1);

        }
    }

    void CreateMazeWalls()
    {
        List<Vector4> singleConnections = new List<Vector4>();
        foreach(Vector4 connection in graph.connectionCosts.Keys)
        {
            Vector2 nodeA = graph.GetNodeA(connection);
            Vector2 nodeB = graph.GetNodeB(connection);
            Vector4 connectionRev = graph.CreateConnection(nodeB, nodeA);
            if (!singleConnections.Contains(connection) && !singleConnections.Contains(connectionRev))
            {
                singleConnections.Add(connection);
            }
        }

        foreach (Vector4 connection in singleConnections)
        {
            Vector2 nodeA = graph.GetNodeA(connection);
            Vector2 nodeB = graph.GetNodeB(connection);
            Vector4 connectionRev = graph.CreateConnection(nodeB, nodeA);
            if (!mst.T.Contains(connection) && !mst.T.Contains(connectionRev))
            {
                Vector2 wallPos = 0.5f * (nodeA + nodeB);
                GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity, transform);
                Vector3 scaleVector = new Vector3(Mathf.Abs(nodeA.x - nodeB.x), Mathf.Abs(nodeA.y - nodeB.y), 0);
                wall.transform.localScale = Vector3.one - 0.9f * scaleVector;
            }
        }
        
    }

    void ClearMaze()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        transform.Rotate(new Vector3(90, 0, 0));
    }

}
