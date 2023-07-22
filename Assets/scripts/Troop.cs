using UnityEngine;
public abstract class Troop : MonoBehaviour
{
    [SerializeField] protected float _speed;
    public abstract void Move();
    private void Update()
    {
        Move();
    }
}
