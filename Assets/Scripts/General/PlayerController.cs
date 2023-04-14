using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Ball _ball;
    [SerializeField] Stick _stick;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }



    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButtonUp(0))
        {


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