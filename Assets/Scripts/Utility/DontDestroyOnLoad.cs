/*
Nathan Nguyen
101268067

12/12/2021

Simple script to create do not destroy objects


*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
