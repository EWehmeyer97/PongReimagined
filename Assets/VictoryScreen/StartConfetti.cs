using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartConfetti : MonoBehaviour
{
    public GameObject turnOn;
    private void OnEnable()
    {
        turnOn.SetActive(true);
    }
}
