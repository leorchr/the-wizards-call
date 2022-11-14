using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float minXPosition;
    public float maxXPosition; 
    public float minYPosition;
    public float maxYPosition;

    private float posX;
    private float posY;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        posX = Mathf.Clamp(player.transform.position.x, minXPosition, maxXPosition);
        posY = Mathf.Clamp(player.transform.position.y, minYPosition, maxYPosition);
        transform.position = new Vector3(posX, posY);
    }
}
