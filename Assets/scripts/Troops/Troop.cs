using System.Collections;
using UnityEngine;
public abstract class Troop : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _health;
    [SerializeField] private float _spwanTimer;
    protected GameObject target;
    [SerializeField] protected float attackTime;
    [SerializeField] protected float damage;
    private bool canAttack = true;
    protected abstract void Move();
    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (canAttack)
            {
                GameManager.xp += damage/2;
                col.gameObject.GetComponent<Enemy>().GetDamaged(damage);
                canAttack = false;
                StartCoroutine(AttackTimer());
            }
        }
    }
    public float GetSpawnTime()
    {
        return _spwanTimer;
    }
    public void RecieveDamage(float Damage)
    {
        _health -= Damage;
    }
    private void Update()
    {
        target = GameObject.FindWithTag("Player");
        if (_health <= 0)
        {
            GameManager.xp += _health / 10;
            Destroy(gameObject);
        }
        Move();
    }
}
