using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characterList;
    public static int index;
    // Start is called before the first frame update
    private void Start()
    {
        index = PlayerPrefs.GetInt("Characterselected");

        //index = PlayerPrefs.GetInt("Characterselected")-1;
        Debug.Log(index);


        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in characterList)
            go.SetActive(false);

        Debug.Log("characterlist size    " + characterList.Length);
        if (characterList[index])
        {
            characterList[index].SetActive(true);

        } 
    }



    public void toggleLeft()
    {

        characterList[index].SetActive(false);
        index--;

        if (index < 0)
        {
            index = characterList.Length - 1;
        }

        characterList[index].SetActive(true);
    }


    public void toggleRight()
    {

        characterList[index].SetActive(false);
        index++;

        if (index == characterList.Length)
        {
            index = 0;
        }

        characterList[index].SetActive(true);
    }


   public void PlayButton()
    {
        PlayerPrefs.SetInt("Characterselected", index);
        SceneManager.LoadScene("Level");
    }
}
