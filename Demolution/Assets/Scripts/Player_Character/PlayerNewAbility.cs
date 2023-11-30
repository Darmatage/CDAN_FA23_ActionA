using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewAbility : MonoBehaviour
{
    public GameObject doubleJumpButton;
    public GameObject jumpScript;

    // Start is called before the first frame update
    void Start()
    {
        doubleJumpButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l")) //trigger for upgrades, can be replaced with a score threshhold
        {
            doubleJumpButton.SetActive(true);
        }
    }

    public void doubleJumpChoiceFunct()
    {
        
        doubleJumpButton.SetActive(false);
    }
}
