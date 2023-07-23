using System.Collections;
using UnityEngine;
public abstract class Troop : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _health;
    protected GameObject target;
    [SerializeField] protected float attackTime;
    [SerializeField] protected float damage;
    private bool canAttack = true;
    public abstract void Move();
    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }

    
    public void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (canAttack)
            {
                col.gameObject.GetComponent<Enemy>().GetDamaged(damage);
                canAttack = false;
                StartCoroutine(AttackTimer());
            }
        }
    }

    public void RecieveDamage(float Damage)
    {
        _health -= Damage;
    }
    private void Update()
    {
        if (_health <= 0)
            Destroy(gameObject);
        Move();
    }
}
