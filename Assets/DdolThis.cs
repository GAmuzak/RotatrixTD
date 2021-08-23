using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdolThis : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
