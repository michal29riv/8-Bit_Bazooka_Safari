using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public int pathsCount = 5;

    public int defaultPath = 3;

    public float pathDistance = 1;

    public float timeBetweenLevels = 5;

    public GameObject[] animals;

    private int currentLevel = -1;

    public PlayerController playerController;

    public GameObject waveText;

    public GameObject endScreen;

    public GameObject lostText;

    public GameObject winText;

    public Level[] levels =
    {
        new Level(1,3,new int[10]{0,0,0,0,0,0,0,0,0,0}),
        new Level(0.8f,2,new int[10]{0,0,1,0,0,1,1,0,0,1}),
        new Level(0.6f,1,new int[10]{1,1,0,1,0,1,1,0,1,1}),
        new Level(0.4f,0.8f,new int[10]{0,1,2,1,0,0,1,1,0,2}),
        new Level(0.3f,0.7f,new int[9]{2,1,2,2,0,2,0,0,2}),
        new Level(0.2f,0.6f,new int[7]{2,2,2,2,2,2,1})
    };

    private void Start()
    {
        NextLevel();
    }

    public void NextLevel()
    {

        currentLevel++;

        if (currentLevel < levels.Length)
        {

            waveText.SetActive(true);

            Invoke("DisableWaveText", 1);

            StartCoroutine(SpawnAnimals());
        }
        else
        {
            Win();
        }
    }

    private void DisableWaveText()
    {
        waveText.SetActive(false);
    }

    private void Win()
    {
        Time.timeScale = 0;

        endScreen.SetActive(true);

        winText.SetActive(true);

        lostText.SetActive(false);
    }

    IEnumerator SpawnAnimals()
    {
        for (int i = 0; i < levels[currentLevel].animalsIndex.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(levels[currentLevel].minTimeToNextAnimal, levels[currentLevel].maxTimeToNextAnimal));

            GameObject animal = Instantiate(animals[levels[currentLevel].animalsIndex[i]]);

            animal.transform.position = transform.position + Vector3.up * ((Random.Range(1, pathsCount + 1) - defaultPath) * pathDistance);
        }

        yield return new WaitForSeconds(timeBetweenLevels);

        NextLevel();

        playerController.SupplyArrows();
    }

    public class Level
    {
        public Level(float MinTimeToNextAnimal, float MaxTimeToNextAnimal, int[] AnimalsIndex)
        {
            minTimeToNextAnimal = MinTimeToNextAnimal;

            maxTimeToNextAnimal = MaxTimeToNextAnimal;

            animalsIndex = AnimalsIndex;
        }

        public float minTimeToNextAnimal;

        public float maxTimeToNextAnimal;

        public int[] animalsIndex;
    }
}
