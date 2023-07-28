using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [SerializeField] private Triangle t;
    [SerializeField] private Square s;
    [SerializeField] private Hexagon h;
    private Dictionary<char, bool> _spawnDict;
    private Dictionary<char, float> _spawnTime;
    private void Awake()
    {
        _spawnDict = new Dictionary<char, bool>();
        _spawnTime = new Dictionary<char, float>();
        _spawnDict.Add('t',true);
        _spawnDict.Add('s',true);
        _spawnDict.Add('h',true);
        _spawnTime.Add('t',t.GetSpawnTime());
        _spawnTime.Add('s',s.GetSpawnTime());
        _spawnTime.Add('h',h.GetSpawnTime());
    }
    private IEnumerator wait(float time,char c)
    {
        yield return new WaitForSeconds(time);
        _spawnDict[c] = true;
    }

    public char SelChar(GameObject g)
    {
        if (g == t.gameObject)
            return 't';
        if (g == s.gameObject)
            return 's';
        return 'h';
    }
    public bool CanSpawn(char c)
    {
        return _spawnDict[c];
    }
    public void SetTimer(char c)
    {
        _spawnDict[c] = false;
        StartCoroutine(wait(_spawnTime[c],c));
    }
    
}
