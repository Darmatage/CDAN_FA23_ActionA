using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterimScript : MonoBehaviour
{
  public GameObject level1text;
  public GameObject level2text;
  public GameObject level3text;
  public GameObject level4text;
  public GameObject level1button;
  public GameObject level2button;
  public GameObject level3button;
  public GameObject level4button;

    // Start is called before the first frame update
    void Start()
    {
level1text.SetActive(false);
level2text.SetActive(false);
level3text.SetActive(false);
level4text.SetActive(false);
level1button.SetActive(false);
level2button.SetActive(false);
level3button.SetActive(false);
level4button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      if (GameHandler.levelNumber == 1)//going into level 2
      {
        level1text.SetActive(true);
        level1button.SetActive(true);
      }
      if (GameHandler.levelNumber == 2)//going into level 3
      {
        level2text.SetActive(true);
        level2button.SetActive(true);
      }
      if (GameHandler.levelNumber == 3)//going into level 4
      {
        level3text.SetActive(true);
        level3button.SetActive(true);
      }
      if (GameHandler.levelNumber == 4)//going into level 5
      {
        level4text.SetActive(true);
        level4button.SetActive(true);
      }
    }

    public void toLevel2Funct()
    {
      level1text.SetActive(false);
      level1button.SetActive(false);
      GameHandler.levelNumber = 2;
      GameHandler.doubleJumpUnlocked = true;
      SceneManager.LoadScene("Work_Thomas");
    }
    public void toLevel3Funct()
    {
      level2text.SetActive(false);
      level2button.SetActive(false);
      GameHandler.levelNumber = 3;
      GameHandler.fireballUnlocked = true;
      SceneManager.LoadScene("Level3");
    }
    public void toLevel4Funct()
    {
      level3text.SetActive(false);
      level3button.SetActive(false);
      GameHandler.levelNumber = 4;

      SceneManager.LoadScene("Level4");
    }
    public void toLevel5Funct()
    {
      level1text.SetActive(false);
      level1button.SetActive(false);
      GameHandler.levelNumber = 5;

      SceneManager.LoadScene("Level5");
    }
}
