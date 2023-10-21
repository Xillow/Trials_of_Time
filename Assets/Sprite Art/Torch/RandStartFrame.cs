using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandStartFrame : MonoBehaviour
{

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(0);

        _animator.Play(state.fullPathHash, 0, Random.Range(0f, 1f));


    }


}
