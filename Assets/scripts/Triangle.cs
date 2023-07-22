using UnityEngine;
public class Triangle : Troop
{
    public override void Move()
    {
        transform.Translate(Vector3.right* (_speed * Time.deltaTime));
    }
}
