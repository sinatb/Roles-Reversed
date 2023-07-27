using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject TroopDaddy;
    [SerializeField] private Enemy _mainEnemy;
    [SerializeField] private int state;
    [SerializeField] private CanvasGroup unitPanelAlpha;
    [SerializeField] private GameObject Triangle;
    [SerializeField] private GameObject Square;
    [SerializeField] private GameObject Hexagon;
    private GameObject _currUnit;
    private GameObject _unitPanel;
    private GameObject _startPanel;
    private float v = 1.0f;

    private void Awake()
    { 
        _unitPanel = GameObject.Find("UnitSelection");
        _unitPanel.SetActive(false);
        _startPanel = GameObject.Find("StartPanel");
    }
    public void GameStart()
    {
        state = 1;
        _startPanel.SetActive(false);
    }
    private void CreateUnitPanel(float v)
    {
        _unitPanel.SetActive(true);
        unitPanelAlpha.alpha += v;
    }
    private void InputManagement()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (_currUnit == Triangle || _currUnit == Square || _currUnit == Hexagon)
            {
                Vector3 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                Instantiate(_currUnit, pos, Quaternion.identity,TroopDaddy.transform);
            }
        }
    }
    public void SelectUnit(int number)
    {
        switch (number)
        {
            case 1:
                _currUnit = Triangle;
                break;
            case 2:
                _currUnit = Square;
                break;
            case 3:
                _currUnit = Hexagon;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            _mainEnemy.MoveToZero(v);
            CreateUnitPanel(0.02f);
            v += 0.03f;
            if ((Vector3.zero - _mainEnemy.transform.position).magnitude < 0.01)
            {
                state = 2;
            }
        }
        else if (state == 2)
        {
            InputManagement();
            if (!_mainEnemy.GetIsAlive())
            {
                state = 3;
            }
        }
        else if (state == 3)
        {
            while (TroopDaddy.transform.childCount > 0) {
                DestroyImmediate(TroopDaddy.transform.GetChild(0).gameObject);
            }
            _mainEnemy.reset();
            _currUnit = null;
            _unitPanel.SetActive(false);
            _startPanel.SetActive(true);
        }
    }
}
