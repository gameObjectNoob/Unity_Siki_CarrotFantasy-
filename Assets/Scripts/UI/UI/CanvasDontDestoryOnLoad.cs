﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDontDestoryOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
