using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using System.IO;

public class Main_stages : MonoBehaviour
{
    public Font my_font;

    public GameObject stage_canvas;
    public GameObject stage_eventsystem;

    public GameObject logo_background;

    private float delay_num = 1f;

    // Use this for initialization
    void Start()
    {
        if (Directory.Exists("Maps") == false)
        {
            Directory.CreateDirectory("Maps");
        }
    }

    void Update()
    {
        delay_num -= Time.deltaTime;

        //stage_title.GetComponent<Text>().text = "Осталось подождать " + Mathf.Round(delay_num) + "        " + Time.deltaTime;

        if (delay_num < 1)
        {
            SceneManager.LoadScene("2_Initial_menu");
        }
    }
}