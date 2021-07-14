using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FourGear.UI;
namespace FourGear.Mechanics
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private Camera cam;

        [SerializeField] private Camera main;
        Vector3 newPosition;
        Vector3 pos;

        private float speed;
        void Start()
        {
            speed = 5;
        }
        void Update()
        {
            ZoomInOnRightClick();
        }

        private void ZoomInOnRightClick()
        {
            if(ShowHint.canZoom)
            {
                newPosition = main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
                pos = transform.position;

                if (transform.position.x < -3.5f)
                    pos.x = -3.5f;
                else if (transform.position.x > 3.5f)
                    pos.x = 3.5f;
                if (transform.position.y < -2)
                    pos.y = -2;
                else if (transform.position.y > 2)
                    pos.y = 2;

                transform.position = pos;

                if (Input.GetMouseButtonDown(1))
                {
                    main.enabled = false;
                    cam.enabled = true;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    main.enabled = true;
                    cam.enabled = false;
                }
            }
        }
    }
}
