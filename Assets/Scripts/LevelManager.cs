using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] Text coinsCountText;
    [SerializeField] Text collectedCoinsCountText;

    [Header("Level Start")]
    [SerializeField] Image levelStartImage;
    [SerializeField] Text levelStartText;
    [SerializeField] float timeForFade = 2f;


    int coinsCount;
    int collectedCoinsCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LevelStart());
        CoinsStart();
    }

    private void CoinsStart()
    {
        GameObject coins = GameObject.Find("Coins");
        if (coins)
        {
            coinsCount = coins.transform.childCount;
        }

        coinsCountText.text = coinsCount.ToString();
        collectedCoinsCountText.text = collectedCoinsCount.ToString();
    }

    private IEnumerator LevelStart()
    {
        levelStartText.text = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(timeForFade);

        float time = 1f;
        while (time > 0f)
        {
            time -= Time.deltaTime;

            Color imageColor = levelStartImage.color;
            imageColor.a = time / 5;
            levelStartImage.color = imageColor;

            Color textColor = levelStartText.color;
            textColor.a = time;
            levelStartText.color = textColor;

            yield return 0;
        }
    }

    public void CollectCoin()
    {
        collectedCoinsCount++;
        collectedCoinsCountText.text = collectedCoinsCount.ToString();
    }

    public bool CanWin()
    {
        return coinsCount - collectedCoinsCount <= 0;
    }
}
