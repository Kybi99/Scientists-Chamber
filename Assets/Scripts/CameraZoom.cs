using System.Collections;
using System.Collections.Generic;
using UnityEngine;    
public class CameraZoom : MonoBehaviour
{
    float speed = 5f;
    [SerializeField] private Camera camera2;
    
    private void Start()
    {
        //camera2.enabled = false;
    }
    private void Update()
    {
        Camera cam = Camera.main;
        Vector3 moveVector = cam.ScreenToWorldPoint(Input.mousePosition) - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        transform.position = cam.transform.position + moveVector;

       /* Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = camera2.ScreenToWorldPoint(mousePos);

        if (mousePos.x >= -3.5 && mousePos.y >= -2 && mousePos.x <= 3.5 && mousePos.y <= 2)
                transform.position = mousePos;
        //Debug.Log("yyy");
        if(Input.GetMouseButtonDown(1))
        {
            camera2.enabled = false;
            Debug.Log("clicked");
        }
        else if(Input.GetMouseButtonUp(1))
            camera2.enabled = true;
        //ZoomInOnRightClick();
        // transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,  Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
*/
    }

    private void ZoomInOnRightClick()
    {
       
    }

}