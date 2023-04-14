using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 

public class Ball : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private bool _isPressed = false;

    private Rigidbody _rigidbody;

    private Camera _camera;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _camera = Camera.main;
    }



    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            _isPressed = true;

            _rigidbody.isKinematic = false;

            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);


        }

        if (Input.GetMouseButtonUp(0))
        {
            _isPressed = false;

            _rigidbody.isKinematic = false;



            // Делает рейкаст в направлении Вперед
            Ray ray = new Ray(transform.position, Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.TryGetComponent(out Block block))
                {


                }

                else if (hitInfo.collider.TryGetComponent(out Segment segment))
                {
                    _rigidbody.isKinematic = true;
                    _rigidbody.velocity = Vector3.zero;
                }

                else if (hitInfo.collider.TryGetComponent(out Finish finish))
                {


                }
            }
        }

    }



    public Vector2 GetDirectionFromClick(Vector2 headPosition)
    {
        Vector3 mousePosition = Input.mousePosition;

        //Преобразует координаты экрана в координаты мира игры, так как координаты экрана могут быть разными для
        //разных телефонов, ведь экраны у разных телефонов имеют разное разрешение
        //Это вариант кода для клика в любом месте экрана
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //В данном варианте при каждом клике Y всегда равен 1 (максимум в режиме ScreenToViewportPoint)
        mousePosition = _camera.ScreenToViewportPoint(mousePosition);
        mousePosition.y = 1;
        mousePosition = _camera.ViewportToWorldPoint(mousePosition);


        Vector2 direction = new Vector2(mousePosition.x - headPosition.x, mousePosition.y - headPosition.y);

        return direction;
    }


}



