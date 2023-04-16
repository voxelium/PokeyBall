using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 

public class Ball : MonoBehaviour
{
    [SerializeField] PlayerController playerController;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor floor))
        {
           playerController.GameOver();
        }
    }


}

