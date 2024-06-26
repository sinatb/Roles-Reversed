using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    private Vector3 targetDir;
    public void SetTarget(Vector3 targetDir)
    {
        this.targetDir = Vector3.Normalize(targetDir);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            col.gameObject.GetComponent<Troop>().RecieveDamage(damage);
        if (!col.gameObject.CompareTag("Bullet") && !col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.Translate(targetDir*(speed*Time.deltaTime));
    }
}
