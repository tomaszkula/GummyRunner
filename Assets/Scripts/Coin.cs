using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpClip;

    bool isExist = true;

    private void Awake()
    {
        GameObject coinsCointener = GameObject.Find("Coins");
        if(!coinsCointener)
        {
            coinsCointener = new GameObject("Coins");
        }
        transform.parent = coinsCointener.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isExist) { return; }

        isExist = false;
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(coinPickUpClip, Camera.main.transform.position);

        FindObjectOfType<LevelManager>().CollectCoin();
    }
}
