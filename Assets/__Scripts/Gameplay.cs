using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gameplay : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject _scorePrefab = null;
    [SerializeField] private GameObject _asteroid = null;
    public int NumAsteroidsStart = 6;
    public int SpawnScore = 1800;

    private int _score;
    private int _numAsteroidsNew = 3;

    private void Start()
    {
        _score = 0;
        _scorePrefab.GetComponent<TextMeshProUGUI>().text = "score: " + _score;

        SpawnAsteroids(NumAsteroidsStart);
    }

    private void Update()
    {
        //Выход из игры
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void IncrementScore()
    {
        _score += 50;
        _scorePrefab.GetComponent<TextMeshProUGUI>().text = "score: " + _score;

        //Если скорость достигла определенного порога
        if (_score == SpawnScore)
        {
            SpawnScore += 1000;
            SpawnAsteroids(_numAsteroidsNew); //Спавним еще астероиды
        }
    }

    private void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidGO = Instantiate(_asteroid, new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), 0), Quaternion.identity);
            asteroidGO.GetComponent<Asteroid>().SetGeneration(1); //При создании устанавливаем первое поколение астероиду
        }
    }
}
