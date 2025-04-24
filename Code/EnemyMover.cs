using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [Header("Enemy Settings")]

    [Tooltip("Enemy speed toward the end")]
    [SerializeField] [Range(0f,5f)] float speed = 1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    PathFinder pathFinder;
    GridManager gridManager;

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);


    }

    void Awake ()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath )
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowThePath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.GoldPenalty();

        gameObject.SetActive(false);
    }
    IEnumerator FollowThePath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosion = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosion);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosion, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        

        
        FinishPath();

    }


}
