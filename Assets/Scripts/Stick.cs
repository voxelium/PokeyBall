using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] private float  _power;

        private void Update()
    {
        _power = Mathf.Clamp(_power, 0, 1);

        _animator.SetFloat("Blend", _power);


    }


}
