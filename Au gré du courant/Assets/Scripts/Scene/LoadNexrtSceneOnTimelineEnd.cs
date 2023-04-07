using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine;

public class LoadNexrtSceneOnTimelineEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector timeline; // référence vers la timeline dans la scène actuelle
    public string nextSceneName; // nom de la scène suivante à charger

    void Start()
    {
        timeline.stopped += LoadNextScene;
    }
    private void LoadNextScene(PlayableDirector pd)
    {
        // charge la scène suivante en utilisant son nom
        SceneManager.LoadScene(nextSceneName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
