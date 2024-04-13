using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public Vector2 travelRange;
    public float speed;

    private bool goUp = false;
    private bool goDown = false;

    // Update is called once per frame
    void Update()
    {
        goUp = Input.GetKey(upKey);
        goDown = Input.GetKey(downKey);
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if(goUp)
            pos += Vector3.forward * speed * Time.fixedDeltaTime;

        if(goDown)
            pos -= Vector3.forward * speed * Time.fixedDeltaTime;

        pos.z = Mathf.Clamp(pos.z, travelRange.x, travelRange.y);

        transform.position = pos;
    }
}
