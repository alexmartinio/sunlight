using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject player;
    public GameObject wrongPart1;
    public GameObject wrongPart2;
    public GameObject correctPart;

    Player playerData;
    // Start is called before the first frame update
    void Start()
    {
        playerData = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerData.inventory[0] == "SpareWrong1")
        {
            wrongPart1.SetActive(true);
            wrongPart2.SetActive(false);
            correctPart.SetActive(true);
        }
        else if (playerData.inventory[0] == "SpareWrong2")
        {
            wrongPart1.SetActive(false);
            wrongPart2.SetActive(true);
            correctPart.SetActive(false);
        }
        else if (playerData.inventory[0] == "SpareCorrect")
        {
            wrongPart1.SetActive(false);
            wrongPart2.SetActive(false);
            correctPart.SetActive(true);
        }
        else
        {
            wrongPart1.SetActive(false);
            wrongPart2.SetActive(false);
            correctPart.SetActive(false);
        }
    }
}
