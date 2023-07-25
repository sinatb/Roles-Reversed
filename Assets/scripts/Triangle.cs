using UnityEngine;
public class Triangle : Troop
{
    protected override void Move()
    {
        Debug.Log(target.transform.position);
        var dest = Vector3.Normalize(target.transform.position - transform.position);
        transform.Translate(dest* (_speed * Time.deltaTime));
    }
}
