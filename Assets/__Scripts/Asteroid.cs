using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject _asteroidPrefab = null;
    public float RotationSpeed = 10f;
    public float MinSpeed = 15f;
    public float MaxSpeed = 30f;
    public float ScaleSize = 0.5f;

    private int _generation;
    private Gameplay _gp;
    private Rigidbody _rigb;

    private void Start()
    {
        if (_gp == null)
        {
            GameObject gameplayGO = GameObject.Find("Gameplay");
            _gp = gameplayGO.GetComponent<Gameplay>();
        } else { Debug.LogError("Asteroid not set."); }

        //Вращение астероида
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * RotationSpeed;
        
        _rigb = _asteroidPrefab.GetComponent<Rigidbody>();

        //Движение астероида
        //Направление по Х выбираем случайно
        float speedX = Random.Range(MaxSpeed, MinSpeed);
        int dirX = Random.Range(-1, 1);
        if (dirX == 0) { dirX = 1; }
        float finalSpeedX = speedX * dirX;
        _rigb.AddForce(transform.right * finalSpeedX);

        //Направление по Y тоже выбираем случайно
        float speedY = Random.Range(MaxSpeed, MinSpeed);
        int dirY = Random.Range(-1, 1);
        if (dirY == 0) { dirY = 1; }
        float finalSpeedY = speedY * dirY;
        _rigb.AddForce(transform.up * finalSpeedY);
    }

    public void SetGeneration(int generation)
    {
        _generation = generation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            //Пока поколение астероида не больше 3 при попадании пули создаем 2 астероида меньше
            if (_generation < 3)
            {
                CreateMiniAsteroids(2);
            }

            if (_gp != null)
            {
                _gp.IncrementScore();
            }
            Destroy(gameObject);
        }

        if (other.gameObject.GetComponent<Player>() != null)
        {
            print("You died.");
        }
    }

    private void CreateMiniAsteroids(int count)
    {
        //Увеличиваем поколение астероида на 1
        int newGeneration = _generation + 1;

        //Создаем астероиды поменьше
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidGO = Instantiate(_asteroidPrefab, transform.position, Quaternion.identity);
            asteroidGO.transform.localScale *= ScaleSize;
            asteroidGO.GetComponent<Asteroid>().SetGeneration(newGeneration);
        }
    }
}
