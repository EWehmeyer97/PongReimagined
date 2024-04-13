using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject spark;
    public ScoreTracker score;
    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            score.PointScored();
            Instantiate(spark, collision.contacts[0].point, Quaternion.LookRotation(-collision.contacts[0].normal, Vector3.up));
            ball.Destroy();
        }
    }
}
