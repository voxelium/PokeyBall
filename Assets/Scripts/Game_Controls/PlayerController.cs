using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("Ball")]
    [SerializeField] Ball _ball;
    private float _ballJumpForce = 0;
    private Rigidbody _ballRigidbody;
    private bool _ballInAir = false;

    [Header("Stick")]
    [SerializeField] Stick _stick;
    [SerializeField] StickJointTail _stickJointTail;
    private SkinnedMeshRenderer stickRender;
    private StickMesh stickMesh;

    [SerializeField] private Camera _camera;
    private float startStretchPosition;
    private float currentStretchPosition;
    public float multiplierJumpForce = 6;

    public event UnityAction <int> EventGameWin;
    public event UnityAction <int> EventGameOver;
    public event UnityAction<int> EventCurrentVolumeUpdate;

    private int bonusCount = 0;

    private void Start()
    {
        _ballRigidbody = _ball.GetComponent<Rigidbody>();
        stickMesh = _stick.GetComponentInChildren<StickMesh>();
        stickRender = stickMesh.GetComponent<SkinnedMeshRenderer>();
    }


    private void Update()

{
            if (_ball)
            {

            // Начало Stretch управления
            if (Input.touchCount > 0)
            {
                if (_ballInAir == false && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startStretchPosition = GetMouseY();
                }


                // Stretch управление 
                if (_ballInAir == false && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _ball.transform.position = _stickJointTail.transform.position;

                    currentStretchPosition = GetMouseY();
                    _ballJumpForce = Mathf.Abs(multiplierJumpForce * (startStretchPosition - currentStretchPosition));

                    _ballJumpForce = Mathf.Clamp(_ballJumpForce, 0, 25);
                    _stick._stickAnimator.SetFloat("Blend", _ballJumpForce / 25f);

                }

                // Условие для полета шара
                if (_ballInAir == false && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    StartBallMoving();
                    HideStick();
                    StartCoroutine(SetBallInAirTrue());
                }

                // Условие для остановки шара
                if (_ballInAir == true && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray ray = new Ray(_ball.transform.position, _ball.transform.forward);

                    //Debug.DrawRay(_ball.transform.position, _ball.transform.forward, color:Color.red, 4);

                    if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                        if (hitInfo.collider.TryGetComponent(out DamageBlock damageBlock))
                        {
                            ShowStick();
                            StartCoroutine(HideStickOnBlock());

                            bonusCount--;
                            EventCurrentVolumeUpdate?.Invoke(bonusCount);
                        }

                        else if (hitInfo.collider.TryGetComponent(out SimpleBlock simpleBlock))
                        {
                            StopBallMoving();
                            ShowStick();
                            _ball.transform.position = _stickJointTail.transform.position;
                        }

                        else if (hitInfo.collider.TryGetComponent(out BonusBlock bonusBlock))
                        {
                            StopBallMoving();
                            ShowStick();
                            _ball.transform.position = _stickJointTail.transform.position;

                            bonusCount++;
                            EventCurrentVolumeUpdate?.Invoke(bonusCount);
                        }


                        else if (hitInfo.collider.TryGetComponent(out FinishBlock finish))
                        {
                            StopBallMoving();
                            Destroy(_ball);
                            Destroy(_stick);

                            if (bonusCount > 0)
                            {
                                EventGameWin?.Invoke(bonusCount);
                            }
                            else if (bonusCount <= 0)
                            {
                                GameOver();
                            }

                            
                            
                        }
                    }
                }

            }

        }

    }


    private void StartBallMoving()
    {
        _ballRigidbody.isKinematic = false;
        _ballRigidbody.AddForce(Vector3.up * _ballJumpForce, ForceMode.Impulse);
        _stick._stickAnimator.SetFloat("Blend", 0);

        StartCoroutine(SetBallInAirTrue());

        if (_ballJumpForce == 0)
        {
            Debug.Log(_ballJumpForce);
        }
    }


    private void StopBallMoving()
    {
        _ballRigidbody.isKinematic = true;
        _ballRigidbody.velocity = Vector3.zero;
        _ballJumpForce = 0;

        StartCoroutine(SetBallInAirFalse());
    }


    private float GetMouseY()
    {
        Vector3 screenMousePosition = Input.mousePosition;
        screenMousePosition.z = _camera.nearClipPlane + 1;

        // вариант для возврата координат мыши Viewport
        Vector2 viewportMousePosition = _camera.ScreenToViewportPoint(screenMousePosition);
        return viewportMousePosition.y * 10f;

        // вариант для возврата Мировых координат мыши
        //Vector3 worldMousePosition = _camera.ScreenToWorldPoint(screenMousePosition);
        //return worldMousePosition.y;
    }


    private void ShowStick()
    {
        _stick.transform.position = new Vector3(_stick.transform.position.x, _ball.transform.position.y, _stick.transform.position.z);
        stickRender.enabled = true;
    }


    private void HideStick()
    {
         stickRender.enabled = false;
    }


    IEnumerator HideStickOnBlock()
    {
        yield return new WaitForSeconds(0.05f);
        HideStick();
    }


    IEnumerator SetBallInAirFalse()
    {
        yield return new WaitForSeconds(0.3f);
        _ballInAir = false;
    }


    IEnumerator SetBallInAirTrue()
    {
        yield return new WaitForSeconds(0.3f);
        _ballInAir = true;
    }



    public void GameOver()
    {
        EventGameOver?.Invoke(0);
    }


}