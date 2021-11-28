using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAudio : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);   
    }
}
