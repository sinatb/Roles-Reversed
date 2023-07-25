using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Enemy _mainEnemy;

    [SerializeField] private int state;
    private float v = 1.0f;
    public void GameStart()
    {
        state = 1;
        var startPanel = GameObject.Find("StartPanel");
        startPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            _mainEnemy.MoveToZero(v);
            v += 0.03f;
            
        }
        
    }
}
