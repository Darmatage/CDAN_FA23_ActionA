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

      }
      if (GameHandler.levelNumber == 2)//going into level 3
      {

      }
      if (GameHandler.levelNumber == 3)//going into level 4
      {

      }
      if (GameHandler.levelNumber == 4)//going into level 5
      {

      }
    }

    public void toLevel2Funct()
    {

    }
    public void toLevel3Funct()
    {

    }
    public void toLevel4Funct()
    {

    }
    public void toLevel5Funct()
    {

    }
}
