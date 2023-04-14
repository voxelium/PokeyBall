using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] Animator _stickAnimator;
    public float  _bendForce;

    private void Update()
    {
        _bendForce = Mathf.Clamp(_bendForce, 0, 1);
        _stickAnimator.SetFloat("Blend", _bendForce);
    }

}
