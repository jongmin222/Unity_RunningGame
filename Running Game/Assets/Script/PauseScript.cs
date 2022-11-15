using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        this.isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (this.isPause == false)
                this.isPause = true;
            else if (this.isPause == true)
                this.isPause = false;
        }

        if (this.isPause == false)
        {
            Time.timeScale = 1;

        }
        else if (this.isPause == true)
        {
            Time.timeScale = 0;
        }

    }
}
