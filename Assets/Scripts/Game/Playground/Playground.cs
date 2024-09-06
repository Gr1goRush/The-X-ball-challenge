using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour
{
    public Ball Ball => ball;
    [SerializeField] private Ball ball;

    public GoalContainer GoalContainer => goalContainer;
    [SerializeField] private GoalContainer goalContainer;

    public GameObject Obstacles => obstacles;
    [SerializeField] private GameObject obstacles;

    public HealthSpawnSystem HealthSpawnSystem => healthSpawnSystem;
    [SerializeField] private HealthSpawnSystem healthSpawnSystem;

    public void SetObstaclesActive(bool v)
    {
        if (Obstacles != null)
        {
            Obstacles.SetActive(v);
        }
    }

}
