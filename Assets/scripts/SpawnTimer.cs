using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTimer : MonoBehaviour
{
    [SerializeField] private Triangle t;
    [SerializeField] private Square s;
    [SerializeField] private Hexagon h;
    [SerializeField] private Slider ts;
    [SerializeField] private Slider ss;
    [SerializeField] private Slider hs;
    private Dictionary<char, bool> _spawnDict;
    private Dictionary<char, float> _spawnTime;
    private Dictionary<char, Slider> _spawnUi;
    private void Awake()
    {
        _spawnDict = new Dictionary<char, bool>();
        _spawnTime = new Dictionary<char, float>();
        _spawnUi = new Dictionary<char, Slider>();
        _spawnDict.Add('t',true);
        _spawnDict.Add('s',true);
        _spawnDict.Add('h',true);
        _spawnTime.Add('t',t.GetSpawnTime());
        _spawnTime.Add('s',s.GetSpawnTime());
        _spawnTime.Add('h',h.GetSpawnTime());
        _spawnUi.Add('t',ts);
        _spawnUi.Add('s',ss);
        _spawnUi.Add('h',hs);
    }
    private IEnumerator wait(float time,char c)
    {
        float timer = 0.0f;
        while (timer < time)
        {
            yield return new WaitForSeconds(0.01f);
            _spawnUi[c].value = timer / time;
            timer += 0.01f;
        }
        _spawnUi[c].value = 1.0f;
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
