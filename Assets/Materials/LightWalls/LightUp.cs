using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    public Vector3 axis;
    public float speed = 1.2f;
    public float timeRate = 2f;

    private Material mat;
    private float pos1;
    private float pos2;
    private float strength;

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (strength <= 0f)
        {
            Vector3 point = collision.transform.position;
            float set = point.x * axis.x + point.y * axis.y + point.z * axis.z;
            set *= -0.625f;

            pos1 = set;
            pos2 = set;
            strength = 1;

            StartCoroutine(SpreadLight());
        }
    }

    private IEnumerator SpreadLight()
    {
        mat.SetFloat("_Position1", pos1);
        mat.SetFloat("_Position2", pos2);
        mat.SetFloat("_Strength", Mathf.Clamp(strength, 0f, 1f));

        if (strength > 0f)
        {
            strength -= Time.deltaTime * timeRate;

            pos1 += speed * Time.deltaTime * strength;
            pos2 -= speed * Time.deltaTime * strength;

            yield return null;

            StartCoroutine(SpreadLight());
        }
        yield return null;
    }

    private void OnApplicationQuit()
    {
        pos1 = 0;
        pos2 = 0;
        strength = 0f;
        StartCoroutine(SpreadLight());
    }
}
