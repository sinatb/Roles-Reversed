using UnityEngine;

public class Square : Troop
{
    public override void Move()
    {
        transform.Translate(Vector3.left*(Time.deltaTime*_speed));
    }
}
