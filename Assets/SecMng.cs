using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecMng : MonoBehaviour
{
    public AudioSource titleTheme;
    public AudioSource gameTheme;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title Menu") {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) {
                SceneManager.LoadScene("Main Menu");
            }
            if (!titleTheme.isPlaying) {
                titleTheme.Play();
            }
            if (gameTheme.isPlaying) {
                gameTheme.Stop();
            }
        }
    }
}
