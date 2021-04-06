using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    [SerializeField] int blockCount = 0; // serialized for debugging

    // cache reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void AddBlock()
    {
        blockCount++;
    }

    public void RemoveBreakableBlock()
    {
        blockCount--;
        if (blockCount <= 0)
        {
            StartCoroutine(LoadNextSceneWithWait(1f));
        }
    }

    private IEnumerator LoadNextSceneWithWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sceneLoader.LoadNextScene();
    }
}
