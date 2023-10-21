using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossfadeController : MonoBehaviour {

    public Animator transition;
    public float transitionTime = 1.5f;

    public float startTransition()
    {
        transition.SetTrigger("endLevel");
        return transitionTime;
    }
}
