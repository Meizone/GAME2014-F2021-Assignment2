using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreenHandler : MonoBehaviour
{

    private Animator m_animator;


    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetInteger("AnimState", 1);
        m_animator.SetBool("Grounded", true);

        AudioManager.Stop("OverworldBGM");
        AudioManager.Play("MenuBGM");
    }



}

