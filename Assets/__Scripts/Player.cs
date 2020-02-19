using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject _bulletPrefab = null;
    public float RotationSpeed = 180f;
    public float Thrust = 6f;
    public float BulletSpeed = 350;

    private Rigidbody _rigb;

    private void Start()
    {
        _rigb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Выстрел на каждый клик ЛКМ или пробела
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("space"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime, 0); //Поворот корабля

        _rigb.AddForce(transform.forward * Thrust * Input.GetAxis("Vertical")); //Движение корабля
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bulletGO.SetActive(true);

        bulletGO.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed);
    }
}
