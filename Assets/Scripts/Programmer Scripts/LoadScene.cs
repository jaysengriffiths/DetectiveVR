﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    class LoadScene : MonoBehaviour  
    {

        void Start()
        {
            SceneManager.LoadScene("HQTESTER");
        }


    }
}
