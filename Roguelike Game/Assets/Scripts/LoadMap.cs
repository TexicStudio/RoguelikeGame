using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadMap : MonoBehaviour
{
    private bool _load = true;
    private float _timer = 5f;
    void Awake()
    {
        if (Directory.Exists("Maps") == false)
        {
            Directory.CreateDirectory("Maps");
        }
        Loading_objects_stages loadingObjectsStages = GetComponent<Loading_objects_stages>();
        loadingObjectsStages.Create_scene();//запускает весь процесс

        New_game_stagesForFastMapLoad newGameStagesForFastMapLoad = GetComponent<New_game_stagesForFastMapLoad>();
        newGameStagesForFastMapLoad.Add_map_Click("basic");
        newGameStagesForFastMapLoad.Add_map_Click("dungeon");
        newGameStagesForFastMapLoad.Button_story_Click();

        Debug.Log("----------------------------------------------------------------");
        Debug.Log("5_Map_Editor");
        Debug.Log("----------------------------------------------------------------");

        //Main.action_name = "";
        //Map_editor_stages mapEditorStages = GameObject.Find("SCENE SCRIPT").GetComponent<Map_editor_stages>();
        //mapEditorStages.Create_scene();
    }


    void Update()
    {
        if (_load)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                Main.action_name = "";
                Map_editor_stages mapEditorStages = GameObject.Find("SCENE SCRIPT").GetComponent<Map_editor_stages>();
                mapEditorStages.Create_scene();
                _load = false;
            }
        }
    }
}
