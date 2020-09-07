using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int lives = 1;

    public float speed = 5;

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
    }

    public void Hit()
    {
        lives--;

        if (lives == 0)
        {
            Destroy(gameObject);
        }
    }
}
