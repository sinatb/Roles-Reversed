using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _shootTime;
    public List<GameObject> bullets;
    public List<Laser> lasers;
    private bool _shoot=true;
    //A simple timer to Determine the shooting time of enemy
    IEnumerator ShootTimer()
    {
        
        yield return new WaitForSeconds(_shootTime);
        _shoot = true;
    }
    private GameObject Target()
    {
        GameObject g = new GameObject();
        return g;
    }
    //Shooting Behaviour
    private void Shoot()
    {
        if (!_shoot) return;
        foreach (var b in bullets)
        {
            Instantiate(b, transform.position, quaternion.identity);
        }

        _shoot = false;
        StartCoroutine(ShootTimer());
    }
    void Update()
    {
        Shoot();

    }
}
