using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PreSceneManager : MonoBehaviour
{

    public List<Sprite> pic = new List<Sprite>();
    public Image sliderGround;
    private float curValue = 0;
    private int count = 0;
    void Update()
    {
        Starting();
        if (Input.GetMouseButtonDown(0))
        {
            SwitchPic();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            JumpToGame();
        }
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
        SceneManager.LoadScene("MainScene");
    }
}
