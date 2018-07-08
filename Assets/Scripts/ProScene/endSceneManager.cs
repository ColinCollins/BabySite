using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class endSceneManager : MonoBehaviour
{

    public List<Sprite> pic = new List<Sprite>();
    public Image sliderGround;
    private float curValue = 0;
    private float gameOverValue = 0;
    private int count = 0;
    public Image gameOver;

    void Update()
    {
        Starting();
        GameOverDisappear();
        if (Input.GetMouseButtonDown(0))
        {
            if (count == 0) {
                gameOver.gameObject.SetActive(false);
            }
            SwitchPic();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            JumpToGame();
        }
    }

    private void GameOverDisappear() {
        gameOverValue += Time.deltaTime / 3;
        if (gameOverValue >= 255)
        {
            gameOverValue = 255;
        }
        gameOver.color = new Color(gameOverValue, gameOverValue, gameOverValue);
    }
    private void Starting()
    {
        curValue += Time.deltaTime / 3;
        if (curValue >= 255)
        {
            curValue = 255;
        }
        sliderGround.color = new Color(curValue, curValue, curValue);
    }
    private void SwitchPic()
    {
        if (count < pic.Count)
        {
            curValue = 0;
            sliderGround.sprite = pic[count];
            count++;
        }
        else
        {
            JumpToGame();
        }
    }
    private void JumpToGame()
    {
        Application.Quit();
    }
}
