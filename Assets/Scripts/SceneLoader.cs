using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] float timeToLoadGame = 3f;

    int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 0)
        {
            StartCoroutine(LoadGame());
        } else
        {
            if(fadeImage)
            {
                StartCoroutine(FadeIn());
            }
        }
    }

    private IEnumerator FadeIn()
    {
        float time = 1f;
        while(time > 0f)
        {
            time -= Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, time);
            yield return 0;
        }
    }

    private IEnumerator FadeOut(int sceneIndex)
    {
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, time);
            yield return 0;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(timeToLoadGame);
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if(Application.CanStreamedLevelBeLoaded(nextSceneIndex))
        {
            StartCoroutine(FadeOut(nextSceneIndex));
        } else
{
            Debug.Log("Nie ma kolejnego lvla");
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
