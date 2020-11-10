using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;

    private Animator pauseMenuAnimator;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                TogglePause(true);
            }
            else
            {
                TogglePause(false);
            }
        }
    }

    public void TogglePause(bool pauseToggle)
    {
        if (pauseToggle == true)
        {
            pauseMenuAnimator.SetBool("Paused", true);
            paused = true;
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuAnimator.SetBool("Paused", false);
            paused = false;
            Time.timeScale = 1f;
        }
    }
}
