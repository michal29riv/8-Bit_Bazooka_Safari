using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int pathsCount = 5;

    public int defaultPath = 3;

    public float pathDistance = 1;

    public float speed = 0.1f;

    public int maxArrows = 10;

    private int currentArrows;

    public int maxHearts = 5;

    private int currentHearts;

    private GameObject[] hearts;

    public Animator playerAnimator;

    public GameObject arrowPrefab;

    public GameObject heartPrefab;

    public Transform heartsParent;

    public Sprite heartBlackSprite;

    public GameObject arrowUIPrefab;

    public Transform arrowsUIParent;

    private List<GameObject> arrowsUI = new List<GameObject>();

    private int currentPath;

    public GameObject endScreen;

    public GameObject lostText;

    public GameObject winText;

    private void Start()
    {
        currentPath = defaultPath;

        currentArrows = maxArrows;

        currentHearts = maxHearts;

        SpawnHearts();

        SpawnArrows();
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, (currentPath - defaultPath) * pathDistance), speed);

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.O) == true)
        {
            RemoveHeart();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            GoUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            GoDown();
        }
    }

    private void SpawnHearts()
    {
        hearts = new GameObject[maxHearts];

        for (int i = 0; i < maxHearts; i++)
        {
            hearts[i] = Instantiate(heartPrefab, heartsParent);
        }
    }

    private void SpawnArrows()
    {
        for (int i = 0; i < maxArrows; i++)
        {
            arrowsUI.Add(Instantiate(arrowUIPrefab, arrowsUIParent));
        }
    }

    public void SupplyArrows()
    {
        currentArrows = maxArrows;

        for (int i = arrowsUI.Count; i < maxArrows; i++)
        {
            arrowsUI.Add(Instantiate(arrowUIPrefab, arrowsUIParent));
        }
    }

    public void GoUp()
    {
        if (currentPath < pathsCount)
        {
            playerAnimator.SetTrigger("walk");

            currentPath++;
        }
    }

    public void GoDown()
    {
        if (currentPath > 1)
        {
            playerAnimator.SetTrigger("walk");

            currentPath--;
        }
    }

    public void Attack()
    {
        if (currentArrows > 0)
        {
            playerAnimator.SetTrigger("attack");

            Invoke("SpawnArrow", 0.15f);

            currentArrows--;

            Destroy(arrowsUI[arrowsUI.Count - 1]);

            arrowsUI.RemoveAt(arrowsUI.Count - 1);
        }
    }

    private void SpawnArrow()
    {
        Instantiate(arrowPrefab, transform.position, Quaternion.identity);
    }

    public void RemoveHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--;

            hearts[currentHearts].GetComponent<Image>().sprite = heartBlackSprite;

            if (currentHearts == 0)
            {
                Death();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") == true)
        {
            RemoveHeart();
        }
    }

    private void Death()
    {
        Time.timeScale = 0;

        endScreen.SetActive(true);

        winText.SetActive(false);

        lostText.SetActive(true);
    }
}
