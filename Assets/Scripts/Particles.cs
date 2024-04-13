using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public GameObject ball;
    private void OnParticleSystemStopped()
    {
        Instantiate(ball, Vector3.up * 0.25f, Quaternion.identity);
        Destroy(gameObject);
    }
}
