using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    //general fields
    public GameObject endPanel;
    public PanelColorChange panelColorChange;
    private bool gameOver;
    public float minTime = 5;
    public float maxTime = 10;
    public float lowSensitivity = 0.22f;
    public float highSensitivity = 3f;

    [SerializeField] private float currentSens;


    //JoyCon variables
    private List<Joycon> joycons;

    // Values made available via Unity
    private int p1_jc_ind = 0; //player 1
    private int p2_jc_ind = 1; //player 2

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        currentSens = highSensitivity;
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < p2_jc_ind + 1)
        {
            gameOver = true;
            Debug.Log("Joycon count too low");
        }


        StartCoroutine(changeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) //if game is ongoing
        {
            if (joycons.Count > 0)
            {
                Joycon p1 = joycons[p1_jc_ind]; //initialize joycons
                Joycon p2 = joycons[p2_jc_ind];


                float p1_accelMagnitude = p1.GetAccel().magnitude - 1; //gravity factor
                if (p1_accelMagnitude > currentSens)
                {
                    playerWinGame(p2_jc_ind+1);
                    p1.SetRumble(160, 320, 0.6f, 3000);
                }
                float p2_accelMagnitude = p2.GetAccel().magnitude - 1; //gravity factor
                if (p2_accelMagnitude > currentSens)
                {
                    playerWinGame(p1_jc_ind+1);
                    p2.SetRumble(160, 320, 0.6f, 3000);
                }
            }
        }
        else //if game is ended
        {
            StopAllCoroutines();
            if (Input.GetKeyDown("r"))
            {
                Debug.Log("reload scene");
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void playerWinGame(int playerNumber)
    {
        gameOver = true;
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Player " + playerNumber + " wins!\n Press r to restart";
    }

    IEnumerator changeCoroutine()
    {
        Debug.Log("Coroutine start");
        while (!gameOver)
        {
            float x = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(x);//wait for x seconds and then go
            panelColorChange.ChangeColor();
            ChangeSensitivity();
            Debug.Log("coroutine color change");

        }
        Debug.Log("exit coroutine");
        yield return null;
    }

    private void ChangeSensitivity()
    {
        if (currentSens == lowSensitivity) //if low sens, change to high sens
        {
            currentSens = highSensitivity;
        }

        else //if sens high, change to low sens
        {
            currentSens = lowSensitivity;
        }
    }
    
}
