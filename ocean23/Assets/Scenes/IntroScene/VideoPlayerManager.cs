using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] UnityEngine.Video.VideoPlayer videoPlayer;
    [SerializeField] string nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
