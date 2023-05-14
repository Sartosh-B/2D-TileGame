using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    int playerLives;

    public GameObject heartPrefab;
    public float nextHeartOffset = 40f;

    GameSesion gameSesion;

    void Start()
    {
        gameSesion = FindObjectOfType<GameSesion>();
        playerLives = gameSesion.GetPlayerLives();
        UpdateLifeUI(playerLives);
    }
    public void UpdateLifeUI(int playerLives)
    {
        //List<GameObject> hearts = new List<GameObject>();

        //foreach (GameObject)

        for (int i = 0; i < playerLives; i++)
        {
            GameObject obj = Instantiate(heartPrefab, transform);
            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition += new Vector2(i*nextHeartOffset, 0);
        }
    }
    /*public void ResetLifeUI()
    {
        FindObjectsByType<life>
    }*/

    


}
