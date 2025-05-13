using UnityEngine;
using UnityEngine.EventSystems;

public class UI_JoyStick : UI_Base, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject JoyStick;
    public GameObject Handler;

    private Vector2 _moveDir;   //���̽�ƽ �̵� ����
    private Vector2 _touchPos;  //����ڰ� ��ġ�� �ʱ���ġ
    private Vector2 _originPos; //���̽�ƽ�� �⺻ ��ġ
    private float _radius;      //���̽�ƽ�� ������ �� �ִ� �ִ� �Ÿ�

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


    //���콺�� ������ �� ����Ǵ� �Լ�
    public void OnPointerDown(PointerEventData eventData)
    {
        SetActiveJoyStick(true);
        _touchPos = Input.mousePosition;
        JoyStick.transform.position = Input.mousePosition;
        Handler.transform.position = Input.mousePosition;
    }

    //���콺�� �巡�� ���� �� ����Ǵ� �Լ�
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