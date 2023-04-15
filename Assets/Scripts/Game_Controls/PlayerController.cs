using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ball")]
    [SerializeField] Ball _ball;
    public float _ballJumpForce = 0;
    private Rigidbody _ballRigidbody;
    private bool _ballInAir = false;

    [Header("Stick")]
    [SerializeField] Stick _stick;
    [SerializeField] StickJointTail _stickJointTail;
    private SkinnedMeshRenderer stickRender;
    private StickMesh stickMesh;

    private bool ballIsCharged = false;

    [SerializeField] private Camera _camera;
    public float startMouseY;
    private float currentMouseY;
    public float multiplierJumpForce;

    private void Start()
    {
        _ballRigidbody = _ball.GetComponent<Rigidbody>();

        stickMesh = _stick.GetComponentInChildren<StickMesh>();

        if (stickMesh)
        {
            Debug.Log("Стик меш получен");
        }

        stickRender = stickMesh.GetComponent<SkinnedMeshRenderer>();
    }


    private void Update()
    {
        if (_ballInAir == false)
        {
            _ball.transform.position = _stickJointTail.transform.position;

            currentMouseY = GetMouseY();
            _ballJumpForce = Mathf.Abs(multiplierJumpForce * (startMouseY - currentMouseY));
        }

        if (_ballInAir == true && Input.GetMouseButtonDown(0))
        {
            startMouseY = GetMouseY();
        }


        if (_ballInAir == false && Input.GetMouseButtonUp(0) && _ballJumpForce > 3)
        {
            StartBallMoving();
        }



        if (_ballInAir && Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(_ball.transform.position, _ball.transform.forward);

            //Debug.DrawRay(_ball.transform.position, _ball.transform.forward, color:Color.red, 4);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out Block block))
                {
                    Debug.Log("попали в Блок");
                }

                else if (hitInfo.collider.TryGetComponent(out Segment segment))
                {
                    StopBallMoving();
                    Debug.Log("попали в Сегмент");
                }

                else if (hitInfo.collider.TryGetComponent(out Finish finish))
                {
                    StopBallMoving();
                }
            }
        }




        //Finish Update
    }


    private void StartBallMoving()
    {
        _ballInAir = true;
        _ballRigidbody.isKinematic = false;
        _ballRigidbody.AddForce(Vector3.up * _ballJumpForce, ForceMode.Impulse);

        stickRender.enabled = false;
    }


    private void StopBallMoving()
    {
        _ballRigidbody.isKinematic = true;
        _ballRigidbody.velocity = Vector3.zero;

        _stick.transform.position = new Vector3(_stick.transform.position.x, _ball.transform.position.y, _stick.transform.position.z);
        _ballInAir = false;
        stickRender.enabled = true;
        _ballJumpForce = 0;

        ballIsCharged = false;
    }


    private float GetMouseY()
    {
        Vector3 screenMousePosition = Input.mousePosition;
        screenMousePosition.z = _camera.nearClipPlane + 1;

        // вариант для возврата координат мыши Viewport
        Vector2 viewportMousePosition = _camera.ScreenToViewportPoint(screenMousePosition);
        return viewportMousePosition.y * 10;

        // вариант для возврата Мировых координат мыши
        //Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
        //return worldMousePosition.y;
    }


    //public Vector2 GetDirectionFromClick(Vector2 headPosition)
    //{
    //    Vector3 mousePosition = Input.mousePosition;

    //    //Преобразует координаты экрана в координаты мира игры, так как координаты экрана могут быть разными для
    //    //разных телефонов, ведь экраны у разных телефонов имеют разное разрешение
    //    //Это вариант кода для клика в любом месте экрана
    //    //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

    //    //В данном варианте при каждом клике Y всегда равен 1 (максимум в режиме ScreenToViewportPoint)
    //    mousePosition = _camera.ScreenToViewportPoint(mousePosition);
    //    mousePosition.y = 1;
    //    mousePosition = _camera.ViewportToWorldPoint(mousePosition);


    //    Vector2 direction = new Vector2(mousePosition.x - headPosition.x, mousePosition.y - headPosition.y);

    //    return direction;
    //}




}