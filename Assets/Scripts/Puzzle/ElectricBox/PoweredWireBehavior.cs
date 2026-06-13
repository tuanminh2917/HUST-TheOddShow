using UnityEngine;
using System.Collections.Generic;
public class PoweredWireBehavior : MonoBehaviour
{
    bool mouseDown = false;
    public PoweredWireStats powerWireS;
    LineRenderer line;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        powerWireS = gameObject.GetComponent<PoweredWireStats>();   
        line = gameObject.GetComponentInParent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveWire();
        line.SetPosition(3, new Vector3(gameObject.transform.localPosition.x - .1f, 
            gameObject.transform.localPosition.y - .0f, 
            0));
        line.SetPosition(2, new Vector3(gameObject.transform.localPosition.x - .4f,
            gameObject.transform.localPosition.y - .0f,
            gameObject.transform.localPosition.z));
    }

    private void OnMouseDown()
    {
        mouseDown = true;
    }

    private void OnMouseOver()
    {
        powerWireS.movable = true;
    }

    private void OnMouseExit()
    {
        if (!powerWireS.moving)
        {
            powerWireS.movable=false;
        }
    }

    private void OnMouseUp()
    {
        mouseDown=false;
        if (!powerWireS.connected)
            gameObject.transform.position = powerWireS.startPosition;
        else
            gameObject.transform.position = powerWireS.connectedPosition;
    }

    private void MoveWire()
    {
        if (mouseDown && powerWireS.movable)
        {
            powerWireS.movable = true;
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 1));
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
                gameObject.transform.position.y, 
                transform.parent.transform.position.z);
        }
        else { powerWireS.moving = false; }
    }
}
