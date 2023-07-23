using UnityEngine;
public abstract class Troop : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _health;
    public abstract void Move();
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
