using UnityEngine;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator _animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Fade()
    {
        _animator.Play("Fade");
    }
}
