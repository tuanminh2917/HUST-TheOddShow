using UnityEngine;
using System.Collections.Generic;

public class PoweredWireStats1 : MonoBehaviour
{
    public bool movable = false;
    public bool moving = false;
    public Vector3 startPosition;
    public Color objectColor;
    public bool connected = false;
    public Vector3 connectedPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
