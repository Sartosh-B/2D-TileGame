using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    public int maxPlayerLives;
    //int playerLives;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    GameSesion gameSesion;
    private void Start()
    {
        //gameSesion = FindObjectOfType<GameSesion>();
    }
    public void UpdateLifeUI(int playerLives)
    {        
        //playerLives = gameSesion.GetPlayerLives();
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < playerLives)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < maxPlayerLives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
