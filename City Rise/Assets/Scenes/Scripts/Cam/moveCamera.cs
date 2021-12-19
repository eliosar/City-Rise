using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public float movement_speed = 1;

    // Update is called once per frame
    void Update()
    {
        Vector2 mouspos = Input.mousePosition;

        if (Input.GetButton("Fire2"))
        {
            float x_movement = 0;
            float z_movement = 0;
            if (mouspos.x <= 150)
            {
                x_movement = -1 * movement_speed * Time.deltaTime;
            }
            else if (mouspos.x >= 1450)
            {
                x_movement = 1 * movement_speed * Time.deltaTime;
            }

            if (mouspos.y >= 750)
            {
                z_movement = 1 * movement_speed * Time.deltaTime;
            }
            else if (mouspos.y <= 500)
            {
                z_movement = -1 * movement_speed * Time.deltaTime;
            }

            transform.Translate(x_movement, 0, z_movement);
            transform.position = new Vector3(transform.position.x, 100, transform.position.z);
        }
    }
}
