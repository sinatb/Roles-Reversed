using UnityEngine;

public class Square : Troop
{
    protected override void Move()
    {
        var dest = Vector3.Normalize(target.transform.position - transform.position);
        transform.Translate(dest* (_speed * Time.deltaTime));
    }
}
