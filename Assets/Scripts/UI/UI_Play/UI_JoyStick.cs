using UnityEngine;
using UnityEngine.EventSystems;

public class UI_JoyStick : UI_Base, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject JoyStick;
    public GameObject Handler;

    private Vector2 _moveDir;   //조이스틱 이동 방향
    private Vector2 _touchPos;  //사용자가 터치한 초기위치
    private Vector2 _originPos; //조이스틱의 기본 위치
    private float _radius;      //조이스틱이 움직일 수 있는 최대 거리

    protected override void Initialize()
    {
        _originPos = JoyStick.transform.position;
        _radius = JoyStick.GetComponent<RectTransform>().sizeDelta.y / 3;
        SetActiveJoyStick(false);
    }

    void SetActiveJoyStick(bool isActive)
    {
        JoyStick.SetActive(isActive);
        Handler.SetActive(isActive);
    }


    //마우스를 눌렀을 때 실행되는 함수
    public void OnPointerDown(PointerEventData eventData)
    {
        SetActiveJoyStick(true);
        _touchPos = Input.mousePosition;
        JoyStick.transform.position = Input.mousePosition;
        Handler.transform.position = Input.mousePosition;
    }

    //마우스를 드래그 했을 때 실행되는 함수
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position;
        _moveDir = (dragPos - _touchPos).normalized;
        float distance = (dragPos - _touchPos).sqrMagnitude;

        Vector3 newPos = (distance < _radius) ? _touchPos + (_moveDir * distance) :
            _touchPos + (_moveDir * _radius);
        Handler.transform.position = newPos;
        GameManager.Instance.MoveDir = _moveDir;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _moveDir = Vector2.zero;
        Handler.transform.position = _originPos;
        JoyStick.transform.position = _originPos;
        SetActiveJoyStick(false);
        GameManager.Instance.MoveDir = _moveDir;
    }
}