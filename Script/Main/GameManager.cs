using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            return instance;
        }
    }

    public int frame;
    public float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        gameTime = 0;
        for(int i = 0; i < 100; i++)
        {
            PlayerData.SetItemData(0, i);
            PlayerData.GetItemData(0);
            PlayerData.SetItemData(0, Random.Range(0, 99999999));
            PlayerData.GetItemData(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        gameTime += Time.deltaTime;
    }

    private void Prepare()
    {

    }


}
