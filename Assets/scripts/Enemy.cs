using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float shootTime;
    public List<GameObject> bullets;
    public float range;
    public float MaxHealth;
    public float speed;
    public GameObject BulletDaddy;
    private bool _isAlive = true;
    private bool _shoot=true;
    private float health;
    private List<GameObject> _front;
    private List<GameObject> _right;
    private List<GameObject> _bottom;
    private List<GameObject> _left;
    //TODO
    //implement perks
    private void Awake()
    {
        _front = new List<GameObject>();
        _bottom = new List<GameObject>();
        _right = new List<GameObject>();
        _left = new List<GameObject>();
        health = MaxHealth;
    }
    //A simple timer to Determine the shooting time of enemy
    IEnumerator ShootTimer()
    {
        
        yield return new WaitForSeconds(shootTime);
        _shoot = true;
    }
    //given an angle i in degrees returns the region of the angle
    private RayDir RegionSelect(int i)
    {
        if (i<45 || (i>=315 && i<360))
        {
            return RayDir.Front;
        }
        if (i >= 45 && i < 135)
        {
            return RayDir.Left;
        }
        if (i >= 135 && i < 225)
        {
            return RayDir.Bottom;
        }
        return RayDir.Right;
    }
    //counts enemies in each region and updates _left _right _front and _bottom
    private void CountRegion()
    {
        _front.Clear();
        _right.Clear();
        _bottom.Clear();
        _left.Clear();
        var hitobj = new List<GameObject>();
        for (int i = 0; i < 360; i++)
        {
            //select the group which this ray corresponds to
            RayDir currRayGroup = RegionSelect(i);
            //the physical stuff for casting a ray happens here
            Quaternion q = Quaternion.AngleAxis(i, Vector3.forward);
            var direction = Vector3.up;
            direction = q * direction;
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction,range);
            //select the proper list for adding new detected enemies
            switch (currRayGroup)
            {
                case RayDir.Right:
                    hitobj = _right;
                    break;
                case RayDir.Left:
                    hitobj = _left;
                    break;
                case RayDir.Front:
                    hitobj = _front;
                    break;
                case RayDir.Bottom:
                    hitobj = _bottom;
                    break;
            }
            //add newly detected objects to their group
            foreach (var t in hit)
            {
                if (t.transform.CompareTag("Enemy") && !hitobj.Contains(t.transform.gameObject))
                {
                    hitobj.Add(t.transform.gameObject);
                }
            }
            //Debug.DrawLine(transform.position,transform.position + range*direction,Color.red,1);   
        }
    }
    //selects the most crowded direction as the direction to shoot
    private List<GameObject> SelectShootTargets()
    {
        if (_front.Count >= _right.Count && _front.Count >= _left.Count && _front.Count >= _bottom.Count)
            return _front;
        if (_right.Count >= _front.Count && _right.Count >= _left.Count && _right.Count >= _bottom.Count)
            return _right;
        if (_left.Count >= _front.Count && _left.Count >= _right.Count && _left.Count >= _bottom.Count)
            return _left;
        if (_bottom.Count >= _right.Count && _bottom.Count >= _left.Count && _bottom.Count >= _front.Count)
            return _bottom;
        return null;
    }
    //selects the direction for the circle to move. at the moment,The Direction is the least crowded direction
    private Vector2 SelectMoveDirection()
    {
        Vector2 ret;
        ret.y = _bottom.Count - _front.Count;
        ret.x = _left.Count - _right.Count;
        return Vector3.Normalize(ret);
    }
    //Shooting Behaviour
    private void Shoot()
    {
        if (!_shoot) return;
        var enemies = SelectShootTargets();
        if (enemies == null || enemies.Count == 0) return;
        foreach (var b in bullets)
        {
            var enemy = enemies[Random.Range(0, enemies.Count)];
            var dir = Vector3.Normalize(enemy.transform.position - transform.position);
            var g = Instantiate(b, transform.position + dir,
                quaternion.identity,BulletDaddy.transform);
            g.GetComponent<Bullet>().SetTarget(dir);
        }
        _shoot = false;
        StartCoroutine(ShootTimer());
    }
    //moving behaviour
    private void Move()
    {
        var newDir = SelectMoveDirection();
        transform.Translate(newDir*(speed*Time.deltaTime));
    }
    //a function to move the player to 0,0 point
    public void MoveToZero(float v)
    {
        var dir = Vector3.zero - transform.position;
        if (dir.magnitude > 0.01f)
            transform.Translate(dir*(v*Time.deltaTime));
    }
    public bool GetIsAlive()
    {
        return _isAlive;
    }
    //reduces players hp the amount of damage
    public void GetDamaged(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            _isAlive = false;
        }
    }

    public void reset()
    {
        health = MaxHealth;
        _isAlive = true;
    }
    void Update()
    {
        if (_isAlive)
        {
            //IMPORTANT
            //count region should always be called before shoot and move!
            CountRegion();
            Shoot();
            Move();
        }
    }
}
