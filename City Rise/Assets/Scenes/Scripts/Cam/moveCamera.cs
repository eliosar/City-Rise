using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public float movement_speed = 1;

    // Update is called once per frame
    void Update()
    {
        float x_movement = Input.GetAxis("Horizontal") * movement_speed * Time.deltaTime;
        float z_movement = Input.GetAxis("Vertical") * movement_speed * Time.deltaTime;

        transform.Translate(x_movement, 0, z_movement);
        transform.position = new Vector3(transform.position.x, 200, transform.position.z);
    }
}
