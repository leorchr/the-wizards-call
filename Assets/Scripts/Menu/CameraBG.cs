using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBG : MonoBehaviour
{
    public float speed;
    public GameObject cameraObject;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 359));
    }

    void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        if (transform.position.x > 10)
        {
            transform.position = new Vector3(-11, transform.position.y, -1);
        }

        if (transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, -7.5f, -1);
        }

        if (transform.position.x < -11)
        {
            transform.position = new Vector3(10, transform.position.y, -1);
        }

        if (transform.localPosition.y < -7.5f)
        {
            transform.position = new Vector3(transform.position.x, 6, -1);
        }
        cameraObject.transform.position = transform.position;
    }
}
