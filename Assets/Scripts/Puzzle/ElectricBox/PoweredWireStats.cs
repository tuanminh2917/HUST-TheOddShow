using UnityEngine;
using System.Collections.Generic;
public enum Color { blue, red, yellow, green };

public class PoweredWireStats : MonoBehaviour
{
    public bool movable = false;
    public bool moving = false;
    public Vector2 startPosition;
    public Color objectColor;
    public bool connected = false;
    public Vector2 connectedPosition;

    [SerializeField] RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        startPosition = rectTransform.anchoredPosition;
    }

}
