using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject menuCanvas;
    public Player player;
    public void PlayeGame()
    {
        menuCanvas.SetActive(false);
        player.gameStarted = true;
    }
}
