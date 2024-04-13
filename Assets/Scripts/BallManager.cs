using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject spawn;

    private static BallManager _instance;
    public static BallManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        Instantiate(spawn, Vector3.up * 0.25f, Quaternion.identity);
    }
}
