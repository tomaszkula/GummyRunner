using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject levelPrefab;
    [SerializeField] GameObject parent;

    void Start()
    {
        int scenesCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < scenesCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int sceneNameStart = scenePath.LastIndexOf("/") + 1;
            int sceneNameEnd = scenePath.LastIndexOf(".");
            var sceneNameLength = sceneNameEnd - sceneNameStart;
            string sceneName = scenePath.Substring(sceneNameStart, sceneNameLength);

            if (sceneName.ToLower().Contains("level"))
            {
                GameObject level = (GameObject)Instantiate(levelPrefab, parent.transform);
                level.GetComponentInChildren<Text>().text = sceneName;
                level.GetComponent<Button>().onClick.AddListener(delegate()
                {
                    FindObjectOfType<SceneLoader>().LoadScene(sceneName);
                });
            }
        }
    }
}
