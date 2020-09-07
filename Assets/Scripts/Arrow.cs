using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10;

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ArrowCatcher") == true)
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy") == true)
        {
            collision.gameObject.GetComponent<Enemy>().Hit();

            Destroy(gameObject);
        }
    }
}
