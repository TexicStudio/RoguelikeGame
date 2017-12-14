using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using SFB;
using UnityEditor;

public class New_game_stagesForFastMapLoad : MonoBehaviour
{
    [Header("   Основные Элементы")]
    public GameObject main_object;
    public Transform main_transform;


    [Space]
    [Header("   список карт")]
    public GameObject basic_map_list_container;



    [Space]
    public List<new_game_map_panel_script> basic_map_panel_list = new List<new_game_map_panel_script>();





    [Space]
    public List<new_game_map_panel_script> dungeon_map_panel_list = new List<new_game_map_panel_script>();








    [Space]
    [Header("   данные")]
    public string Title = "";
    public string FileName = "";
    public string Directory = "";
    public string Extension = "";
    private string normal_map = "mp";
    private string dungeon_map = "md";
    public bool Multiselect = false;


    [Space]
    public string dungeon_map_type = "dungeon";
    public string basic_map_type = "basic";

    [Space]//Процес обработки
    private int mask_bar_size_step_num;
    private float mask_bar_size_step;
    private float mask_bar_progress_size_step;
    private float mask_bar_size;

    [Space]
    private bool change_action = false;
    public string action_name = ""; // тип действия которое сейчас выполняеться

    private bool map_loading;

    private System.Random main_rnd = new System.Random();
    private List<string> list_map_name = new List<string>();


    private string main_map_name = "";


    private string story_map_name = "";
    private int story_map_num = 0;
    public int story_map_num_max = 1;//private int story_map_num_max = 2;


    private int dungeon_num = 0;
    private int dungeon_max = 0;

    private int map_num = 0;

    private Story_data_script Story_info = new Story_data_script();
    private string map_name = "";

    private map_cell_name_script dungeon_transition = new map_cell_name_script();


    private string dungeon_entrance_null = "";

    private int dungeon_entrance_num = 0;
    private int dungeon_transition_num = 0;


    private List<string> point_list = new List<string>();

    private List<map_cell_name_script> dungeon_list = new List<map_cell_name_script>();

    private List<map_cell_name_script> region_teleportation = new List<map_cell_name_script>();

    private List<map_cell_name_script> dungeon_transition_list = new List<map_cell_name_script>();

    private List<map_cell_name_script> region_dungeon_exit = new List<map_cell_name_script>();

    private List<map_cell_name_script> start_dungeon_transition_list = new List<map_cell_name_script>();
    private List<map_cell_name_script> end_dungeon_transition_list = new List<map_cell_name_script>();


    private float delay_num = 1f;

    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        if (change_action == true)
        {
            Select_action();
        }
    }


    #region Создание и редактирование списка карт (основные / подзимелья)

    public void Add_map_Click(string mapType)
    {
        bool startAdd = false;

        switch (mapType)
        {
            case "basic":
                {
                    startAdd = true;
                    Extension = normal_map;
                    break;
                }
            case "dungeon":
                {
                    startAdd = true;
                    Extension = dungeon_map;
                    break;
                }
        }

        if (startAdd)
        {
            //var paths = StandaloneFileBrowser.OpenFilePanel(Title, Directory, Extension, Multiselect);
            //if (paths.Length > 0)
            //{
            var path1 = "D:\\Gamedev\\_Projects\\Roguelike Game\\Maps\\1_c2__x7_y4.mp";
            var path2 = "D:\\Gamedev\\_Projects\\Roguelike Game\\Maps\\0_c2__x5_y3.md";
            switch (mapType)
            {
                case "basic":
                    {
                        if (File.Exists(path1))
                        {
                            Add_new_basic_map(path1);
                        }
                        else
                        {
                            Debug.Log("Map doesn't excist or didn't loaded");
                        }
                        break;
                    }
                case "dungeon":
                    {
                        if (File.Exists(path2))
                        {
                            Add_new_dungeon_map(path2);
                        }
                        else
                        {
                            Debug.Log("Map doesn't excist or didn't loaded");
                        }
                        break;
                    }
            }
        }
    }


    private void Add_new_basic_map(string newPath)
    {
        new_game_map_panel_script newMapPanel = new new_game_map_panel_script
        {
            id = basic_map_panel_list.Count,
            map_type = basic_map_type,
            path = newPath
        };
        basic_map_panel_list.Add(newMapPanel);
    }


    private void Add_new_dungeon_map(string newPath)
    {
        new_game_map_panel_script newMapPanel = new new_game_map_panel_script
        {
            id = basic_map_panel_list.Count,
            map_type = basic_map_type,
            path = newPath
        };
        dungeon_map_panel_list.Add(newMapPanel);
    }
    #endregion


    #region Загрузка и обработка карты
    private void Map_loading(string paths)
    {
        map_loading = false;

        if (paths.Length > 0)
        {
            if (File.Exists(paths))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(paths, FileMode.Open);

                Map_info_Blank savedMap = (Map_info_Blank)bf.Deserialize(file);

                string new_map_name = "" + map_num;

                /////////////////////////////////////////////Main.Map_info.Add(map_name, savedMap);
                Story_map_Data new_story_map = new Story_map_Data();

                new_story_map.id = "" + new_map_name;

                //Debug.Log("карта " + "   " + new_map_name + "   " + savedMap.name);

                new_story_map.name = savedMap.name;
                new_story_map.test_name = main_map_name + " " + savedMap.name;
                new_story_map.map_cell_number = savedMap.map_cell_number;
                new_story_map.map_height = savedMap.map_height;
                new_story_map.map_width = savedMap.map_width;

                new_story_map.protagonist_position_x = savedMap.protagonist_position_x;
                new_story_map.protagonist_position_y = savedMap.protagonist_position_y;


                new_story_map.short_info.relief = savedMap.short_info.relief;

                new_story_map.short_info.dungeon = savedMap.short_info.dungeon;
                new_story_map.short_info.dungeon_transition = savedMap.short_info.dungeon_transition;

                new_story_map.short_info.region = savedMap.short_info.region;
                new_story_map.short_info.region_teleportation = savedMap.short_info.region_teleportation;
                new_story_map.short_info.region_dungeon_exit = savedMap.short_info.region_dungeon_exit;
                new_story_map.short_info.region_protagonist_start_point = savedMap.short_info.region_protagonist_start_point;


                new_story_map.cell_info = savedMap.cell_info;

                Story_info.Map_data.Add(new_map_name, new_story_map);
                //Debug.Log(map_name);

                file.Close();

                map_num = map_num + 1;

                map_loading = true;
            }
            else
            {
                Debug.Log("Map doesn't excist or didn't loaded");
            }
        }

        change_action = true;
    }
    #endregion



    private void Select_action()
    {
        change_action = false;

        switch (action_name)
        {
            case "Loading_Story_main_map":
                if (map_loading == true)
                {
                    map_loading = false;
                    Story_main_map_generation();
                }
                break;
            case "Loading_map_story_main_chain_dungeon":
                if (map_loading == true)
                {
                    map_loading = false;
                    Story_main_chain_dungeon_generation();
                }
                break;
            case "Loading_map_story_dungeon_transition":
                if (map_loading == true)
                {
                    map_loading = false;
                    Story_dungeon_transition_generation();
                }
                break;
            case "Loading_map_story_dungeon":
                if (map_loading == true)
                {
                    map_loading = false;
                    Story_dungeon_generation();
                }
                break;
            case "Loading_map_story_teleportation":
                Start_region_teleportation_generation();
                break;
            case "Next_scene_open":
                if (delay_num > 0)
                {
                    delay_num -= Time.deltaTime;
                    change_action = true;
                }
                else
                {
                    change_action = false;
                    Next_scene_open();
                }
                break;
        }
    }



    #region Начало генирации истории
    public void Button_story_Click()
    {
        bool start_add = false;

        if ((basic_map_panel_list.Count > 0) && (dungeon_map_panel_list.Count > 0))
        {
            Debug.Log("4_New_game_stages --- 0  [" + basic_map_panel_list.Count + " = " + story_map_num_max + "]");
            if (basic_map_panel_list.Count >= story_map_num_max)
            {
                Debug.Log("4_New_game_stages --- 1");
                if (story_map_num_max >= 1)
                {
                    Debug.Log("4_New_game_stages --- 2");
                    start_add = true;
                }
            }
        }

        if (start_add == true)
        {
            Story_info.name = "Теставая версия истории";

            Debug.Log("4_New_game_stages --- Start");
            //new_map_progress_canvas.SetActive(true);

            map_num = 0;

            Story_map_list_generation();
        }
    }

    public void Story_map_list_generation()
    {
        int temp_num = 0;

        List<string> temp_namp_list = new List<string>();

        for (int temp_i = 0; temp_i < basic_map_panel_list.Count; temp_i++)
        {
            temp_namp_list.Add(basic_map_panel_list[temp_i].path);
        }

        for (int temp_j = 0; temp_j < story_map_num_max; temp_j++)
        {
            temp_num = main_rnd.Next(0, temp_namp_list.Count);

            list_map_name.Add(temp_namp_list[temp_num]);

            temp_namp_list.RemoveAt(temp_num);
        }

        action_name = "Loading_Story_main_map";

        Loading_Story_main_map();
    }
    #endregion


    #region Создаем основную карту
    public void Loading_Story_main_map()
    {
        main_map_name = "основная карта";
        Map_loading(list_map_name[story_map_num]);

    }

    public void Story_main_map_generation()
    {
        System.Random temp_rnd = new System.Random();


        map_name = "" + (map_num - 1);

        story_map_name = map_name;

        Debug.Log("4_New_game_stages --- Story[" + story_map_name + "] generation");



        if (Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Count > 0)
        {
            int temp_region_protagonist_start_point_num = 0;

            point_list = Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Keys.ToList();

            if (story_map_num < 1)
            {
                temp_region_protagonist_start_point_num = temp_rnd.Next(0, point_list.Count);

                Story_info.protagonist_position_x = Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].position_x;
                Story_info.protagonist_position_y = Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].position_y;
                PlayerController playerController = GameObject.Find("protagonist").GetComponent<PlayerController>();
                playerController.MaxCellHeight = Story_info.Map_data[map_name].map_height;
                playerController.MaxCellWidth = Story_info.Map_data[map_name].map_width;
            }

            if (point_list.Count > 0)
            {
                for (temp_region_protagonist_start_point_num = 0; temp_region_protagonist_start_point_num < point_list.Count; temp_region_protagonist_start_point_num++)
                {
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing_active = 0;

                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.editor_id = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.editor_type = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.level = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.image_id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_editor_id = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_editor_type = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_level = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_image_id = 0;

                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].default_active = 1;

                    Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Remove(point_list[temp_region_protagonist_start_point_num]);
                }
            }
            point_list.Clear();
        }



        if (story_map_num_max > 1)
        {
            if (Story_info.Map_data[map_name].short_info.dungeon_transition.Count > 0)
            {
                int temp_transition_num = 0;

                point_list = Story_info.Map_data[map_name].short_info.dungeon_transition.Keys.ToList();
                /*
                for(int i = 0; i < point_list.Count; i++)
                {
                    Debug.Log("map[ " + map_name + " ].cell[ " + point_list[i] + " ]");
                }
                */
                if (point_list.Count > 0)
                {
                    temp_transition_num = temp_rnd.Next(0, point_list.Count);

                    map_cell_name_script new_start_point = new map_cell_name_script();
                    new_start_point.map_name = map_name;
                    new_start_point.cell_address = point_list[temp_transition_num];

                    start_dungeon_transition_list.Add(new_start_point);

                    point_list.RemoveAt(temp_transition_num);
                }

                if (point_list.Count > 0)
                {
                    temp_transition_num = temp_rnd.Next(0, point_list.Count);

                    map_cell_name_script new_end_point = new map_cell_name_script();
                    new_end_point.map_name = map_name;
                    new_end_point.cell_address = point_list[temp_transition_num];

                    end_dungeon_transition_list.Add(new_end_point);

                    point_list.RemoveAt(temp_transition_num);
                }

                if (point_list.Count > 0)
                {
                    for (temp_transition_num = 0; temp_transition_num < point_list.Count; temp_transition_num++)
                    {
                        map_cell_name_script new_point = new map_cell_name_script();
                        new_point.map_name = map_name;
                        new_point.cell_address = point_list[temp_transition_num];

                        dungeon_transition_list.Add(new_point);
                    }
                }
                point_list.Clear();
            }
        }
        else
        {
            if (Story_info.Map_data[map_name].short_info.dungeon_transition.Count > 0)
            {
                point_list = Story_info.Map_data[map_name].short_info.dungeon_transition.Keys.ToList();

                for (int i = 0; i < point_list.Count; i++)
                {
                    map_cell_name_script new_point = new map_cell_name_script();
                    new_point.map_name = map_name;
                    new_point.cell_address = point_list[i];

                    dungeon_transition_list.Add(new_point);
                }

                point_list.Clear();
            }
        }


        if (Story_info.Map_data[map_name].short_info.region_teleportation.Count > 0)
        {
            point_list = Story_info.Map_data[map_name].short_info.region_teleportation.Keys.ToList();

            for (int i = 0; i < point_list.Count; i++)
            {
                map_cell_name_script new_teleport = new map_cell_name_script();
                new_teleport.map_name = map_name;
                new_teleport.cell_address = point_list[i];

                region_teleportation.Add(new_teleport);
            }

            point_list.Clear();
        }


        if (Story_info.Map_data[map_name].short_info.dungeon.Count > 0)
        {
            point_list = Story_info.Map_data[map_name].short_info.dungeon.Keys.ToList();

            for (int i = 0; i < point_list.Count; i++)
            {
                map_cell_name_script new_point = new map_cell_name_script();
                new_point.map_name = map_name;
                new_point.cell_address = point_list[i];

                dungeon_list.Add(new_point);
            }

            point_list.Clear();
        }

        //Debug.Log("New_game_stages --- Story_info.Map_data[map_name].short_info.dungeon_transition = " + Story_info.Map_data[map_name].short_info.dungeon_transition.Count + "   " + dungeon_transition_list.Count);

        if (story_map_num_max > 1)
        {
            story_map_num = story_map_num + 1;

            if (story_map_num < story_map_num_max)
            {
                Loading_Story_main_map();
            }
            else
            {
                story_map_num = 0;

                action_name = "Loading_map_story_main_chain_dungeon";
                Debug.Log("New_game_stages --- Начало создания подземелей-переходов");
                /*
                for (int i = 0; i < start_dungeon_transition_list.Count; i++)
                {
                    Debug.Log("map[ " + start_dungeon_transition_list[i].map_name + " ].cell[ " + start_dungeon_transition_list[i].cell_address + " ]");
                }

                //Debug.Log("New_game_stages ---------------------------------------------------------------------");

                for (int i = 0; i < end_dungeon_transition_list.Count; i++)
                {
                    Debug.Log("map[ " + end_dungeon_transition_list[i].map_name + " ].cell[ " + end_dungeon_transition_list[i].cell_address + " ]");
                }

                //Debug.Log("New_game_stages ---------------------------------------------------------------------");
                */
                Loading_map_story_main_chain_dungeon();
            }
        }
        else
        {
            story_map_num = 0;

            action_name = "";
            Debug.Log("New_game_stages --- Начало создания дополнительных подземелей-переходов");

            Start_dungeon_transition_generation();
        }
    }
    #endregion


    #region Создаем цепочку переходов
    public void Loading_map_story_main_chain_dungeon()
    {
        main_map_name = "переход цепочки";
        Map_loading(dungeon_map_panel_list[main_rnd.Next(0, dungeon_map_panel_list.Count)].path);
    }

    public void Story_main_chain_dungeon_generation()
    {
        System.Random temp_rnd = new System.Random();

        int temp_transition_num = 0;

        map_name = "" + (map_num - 1);

        Debug.Log("4_New_game_stages --- Story_chain_dungeon[" + map_name + "] generation");


        if (Story_info.Map_data[map_name].short_info.region_teleportation.Count > 0)
        {
            point_list = Story_info.Map_data[map_name].short_info.region_teleportation.Keys.ToList();

            for (int i = 0; i < point_list.Count; i++)
            {
                map_cell_name_script new_teleport = new map_cell_name_script();
                new_teleport.map_name = map_name;
                new_teleport.cell_address = point_list[i];

                region_teleportation.Add(new_teleport);
            }

            point_list.Clear();
        }


        if (Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Count > 0)
        {
            int temp_region_protagonist_start_point_num = 0;

            point_list = Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Keys.ToList();

            if (point_list.Count > 0)
            {
                for (temp_region_protagonist_start_point_num = 0; temp_region_protagonist_start_point_num < point_list.Count; temp_region_protagonist_start_point_num++)
                {
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing_active = 0;

                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.editor_id = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.editor_type = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.level = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.image_id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_editor_id = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_editor_type = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_level = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_image_id = 0;

                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].default_active = 1;

                    Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Remove(point_list[temp_region_protagonist_start_point_num]);
                }
            }
            point_list.Clear();
        }


        if (Story_info.Map_data[map_name].short_info.region_dungeon_exit.Count > 0)
        {
            point_list = Story_info.Map_data[map_name].short_info.region_dungeon_exit.Keys.ToList();
        }

        if (story_map_num < (start_dungeon_transition_list.Count - 1))
        {
            temp_transition_num = temp_rnd.Next(0, point_list.Count);

            Story_info.Map_data[start_dungeon_transition_list[story_map_num].map_name].cell_info[start_dungeon_transition_list[story_map_num].cell_address].cell_stuffing.object_editor_id = map_name;
            Story_info.Map_data[start_dungeon_transition_list[story_map_num].map_name].cell_info[start_dungeon_transition_list[story_map_num].cell_address].cell_stuffing.object_editor_type = point_list[temp_transition_num];

            Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_id = start_dungeon_transition_list[story_map_num].map_name;
            Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_type = start_dungeon_transition_list[story_map_num].cell_address;

            //Debug.Log("map[ " + start_dungeon_transition_list[story_map_num].map_name + " ].cell[ " + start_dungeon_transition_list[story_map_num].cell_address + " ] --> map[ " + map_name + " ].cell[ " + point_list[temp_transition_num] + " ]");

            point_list.RemoveAt(temp_transition_num);


            temp_transition_num = temp_rnd.Next(0, point_list.Count);

            Story_info.Map_data[end_dungeon_transition_list[story_map_num + 1].map_name].cell_info[end_dungeon_transition_list[story_map_num + 1].cell_address].cell_stuffing.object_editor_id = map_name;
            Story_info.Map_data[end_dungeon_transition_list[story_map_num + 1].map_name].cell_info[end_dungeon_transition_list[story_map_num + 1].cell_address].cell_stuffing.object_editor_type = point_list[temp_transition_num];

            Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_id = end_dungeon_transition_list[story_map_num + 1].map_name;
            Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_type = end_dungeon_transition_list[story_map_num + 1].cell_address;

            //Debug.Log("map[ " + end_dungeon_transition_list[story_map_num + 1].map_name + " ].cell[ " + end_dungeon_transition_list[story_map_num + 1].cell_address + " ] --> map[ " + map_name + " ].cell[ " + point_list[temp_transition_num] + " ]");

            point_list.RemoveAt(temp_transition_num);

            if (point_list.Count > 0)
            {
                for (int i = 0; i < point_list.Count; i++)
                {
                    map_cell_name_script new_dungeon_exit = new map_cell_name_script();
                    new_dungeon_exit.map_name = map_name;
                    new_dungeon_exit.cell_address = point_list[i];

                    region_dungeon_exit.Add(new_dungeon_exit);
                }
            }

            point_list.Clear();

            story_map_num = story_map_num + 1;

            //Debug.Log("---------------------------------------------------------------------");

            action_name = "Loading_map_story_main_chain_dungeon";

            Loading_map_story_main_chain_dungeon();
        }
        else
        {
            temp_transition_num = temp_rnd.Next(0, point_list.Count);

            Story_info.Map_data[start_dungeon_transition_list[story_map_num].map_name].cell_info[start_dungeon_transition_list[story_map_num].cell_address].cell_stuffing.object_editor_id = map_name;
            Story_info.Map_data[start_dungeon_transition_list[story_map_num].map_name].cell_info[start_dungeon_transition_list[story_map_num].cell_address].cell_stuffing.object_editor_type = point_list[temp_transition_num];

            Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_id = start_dungeon_transition_list[story_map_num].map_name;
            Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_type = start_dungeon_transition_list[story_map_num].cell_address;

            //Debug.Log("map[ " + start_dungeon_transition_list[story_map_num].map_name + " ].cell[ " + start_dungeon_transition_list[story_map_num].cell_address + " ] --> map[ " + map_name + " ].cell[ " + point_list[temp_transition_num] + " ]");

            point_list.RemoveAt(temp_transition_num);


            temp_transition_num = temp_rnd.Next(0, point_list.Count);

            Story_info.Map_data[end_dungeon_transition_list[0].map_name].cell_info[end_dungeon_transition_list[0].cell_address].cell_stuffing.object_editor_id = map_name;
            Story_info.Map_data[end_dungeon_transition_list[0].map_name].cell_info[end_dungeon_transition_list[0].cell_address].cell_stuffing.object_editor_type = point_list[temp_transition_num];

            Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_id = end_dungeon_transition_list[0].map_name;
            Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_type = end_dungeon_transition_list[0].cell_address;

            //Debug.Log("map[ " + end_dungeon_transition_list[0].map_name + " ].cell[ " + end_dungeon_transition_list[0].cell_address + " ] --> map[ " + map_name + " ].cell[ " + point_list[temp_transition_num] + " ]");

            point_list.RemoveAt(temp_transition_num);

            if (point_list.Count > 0)
            {
                for (int i = 0; i < point_list.Count; i++)
                {
                    map_cell_name_script new_dungeon_exit = new map_cell_name_script();
                    new_dungeon_exit.map_name = map_name;
                    new_dungeon_exit.cell_address = point_list[i];

                    region_dungeon_exit.Add(new_dungeon_exit);
                }
            }

            point_list.Clear();

            story_map_num = 0;

            action_name = "";

            Start_dungeon_transition_generation();
        }
    }
    #endregion


    #region Создаем дополнительные переходы
    public void Start_dungeon_transition_generation()
    {
        System.Random temp_rnd = new System.Random();

        Debug.Log("Start_dungeon_transition_generation");


        int temp_transition_num = 0;

        if (dungeon_transition_list.Count > region_dungeon_exit.Count)
        {
            action_name = "";

            story_map_num_max = region_dungeon_exit.Count;
        }
        else
        {
            action_name = "region_dungeon_exit_clear";

            story_map_num_max = dungeon_transition_list.Count;
        }

        for (int i = 0; i < story_map_num_max; i++)
        {
            temp_transition_num = temp_rnd.Next(0, region_dungeon_exit.Count);

            Story_info.Map_data[dungeon_transition_list[0].map_name].cell_info[dungeon_transition_list[0].cell_address].cell_stuffing.object_editor_id = region_dungeon_exit[temp_transition_num].map_name;
            Story_info.Map_data[dungeon_transition_list[0].map_name].cell_info[dungeon_transition_list[0].cell_address].cell_stuffing.object_editor_type = region_dungeon_exit[temp_transition_num].cell_address;

            Story_info.Map_data[region_dungeon_exit[temp_transition_num].map_name].cell_info[region_dungeon_exit[temp_transition_num].cell_address].cell_stuffing.object_editor_id = dungeon_transition_list[0].map_name;
            Story_info.Map_data[region_dungeon_exit[temp_transition_num].map_name].cell_info[region_dungeon_exit[temp_transition_num].cell_address].cell_stuffing.object_editor_type = dungeon_transition_list[0].cell_address;

            //Debug.Log("map[ " + dungeon_transition_list[0].map_name + " ].cell[ " + dungeon_transition_list[0].cell_address + " ] --> map[ " + region_dungeon_exit[temp_transition_num].map_name + " ].cell[ " + region_dungeon_exit[temp_transition_num].cell_address + " ]");

            region_dungeon_exit.RemoveAt(temp_transition_num);

            dungeon_transition_list.RemoveAt(0);
        }

        story_map_num = story_map_num_max;

        if (action_name != "region_dungeon_exit_clear")
        {
            Debug.Log("New_game_stages --- Начало создания дополнительных подземелей-переходов");

            action_name = "Loading_map_story_dungeon_transition";
            Loading_map_story_dungeon_transition();
        }
        else
        {
            for (int i = 0; i < region_dungeon_exit.Count; i++)
            {
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing_active = 0;
                // обнулить полностью данные stuffing чтоб удалить выход

                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.id = 0;
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.editor_id = "";
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.editor_type = "";
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.level = 0;
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.image_id = 0;
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.object_id = 0;
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.object_editor_id = "";
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.object_editor_type = "";
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.object_level = 0;
                Story_info.Map_data[region_dungeon_exit[i].map_name].cell_info[region_dungeon_exit[i].cell_address].cell_stuffing.object_image_id = 0;

                Story_info.Map_data[region_dungeon_exit[i].map_name].short_info.region_dungeon_exit.Remove(region_dungeon_exit[i].cell_address);
            }

            region_dungeon_exit.Clear();



            Start_dungeon_generation();
        }
    }

    public void Loading_map_story_dungeon_transition()
    {
        main_map_name = "переход дополнительный";
        Map_loading(dungeon_map_panel_list[main_rnd.Next(0, dungeon_map_panel_list.Count)].path);
    }

    public void Story_dungeon_transition_generation()
    {
        System.Random temp_rnd = new System.Random();

        int temp_transition_num = 0;

        map_name = "" + (map_num - 1);

        Debug.Log("4_New_game_stages --- Story_dungeon_transition[" + map_name + "] generation");


        if (Story_info.Map_data[map_name].short_info.region_teleportation.Count > 0)
        {
            point_list = Story_info.Map_data[map_name].short_info.region_teleportation.Keys.ToList();

            for (int i = 0; i < point_list.Count; i++)
            {
                map_cell_name_script new_teleport = new map_cell_name_script();
                new_teleport.map_name = map_name;
                new_teleport.cell_address = point_list[i];

                region_teleportation.Add(new_teleport);
            }

            point_list.Clear();
        }


        if (Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Count > 0)
        {
            int temp_region_protagonist_start_point_num = 0;

            point_list = Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Keys.ToList();

            if (point_list.Count > 0)
            {
                for (temp_region_protagonist_start_point_num = 0; temp_region_protagonist_start_point_num < point_list.Count; temp_region_protagonist_start_point_num++)
                {
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing_active = 0;

                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.editor_id = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.editor_type = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.level = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.image_id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_editor_id = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_editor_type = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_level = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_image_id = 0;

                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].default_active = 1;

                    Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Remove(point_list[temp_region_protagonist_start_point_num]);
                }
            }
            point_list.Clear();
        }


        point_list = Story_info.Map_data[map_name].short_info.region_dungeon_exit.Keys.ToList();
        temp_transition_num = temp_rnd.Next(0, point_list.Count);

        Story_info.Map_data[dungeon_transition_list[0].map_name].cell_info[dungeon_transition_list[0].cell_address].cell_stuffing.object_editor_id = map_name;
        Story_info.Map_data[dungeon_transition_list[0].map_name].cell_info[dungeon_transition_list[0].cell_address].cell_stuffing.object_editor_type = point_list[temp_transition_num];

        Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_id = dungeon_transition_list[0].map_name;
        Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_type = dungeon_transition_list[0].cell_address;

        //Debug.Log("map[ " + dungeon_transition_list[0].map_name + " ].cell[ " + dungeon_transition_list[0].cell_address + " ] --> map[ " + map_name + " ].cell[ " + point_list[temp_transition_num] + " ]");

        point_list.RemoveAt(temp_transition_num);

        dungeon_transition_list.RemoveAt(0);


        if (point_list.Count > 0)
        {
            if (dungeon_transition_list.Count >= point_list.Count)
            {
                story_map_num_max = point_list.Count;
            }
            else
            {
                story_map_num_max = dungeon_transition_list.Count;
            }

            for (int i = 0; i < story_map_num_max; i++)
            {
                temp_transition_num = temp_rnd.Next(0, dungeon_transition_list.Count);

                Story_info.Map_data[map_name].cell_info[point_list[0]].cell_stuffing.object_editor_id = dungeon_transition_list[temp_transition_num].map_name;
                Story_info.Map_data[map_name].cell_info[point_list[0]].cell_stuffing.object_editor_type = dungeon_transition_list[temp_transition_num].cell_address;

                Story_info.Map_data[dungeon_transition_list[temp_transition_num].map_name].cell_info[dungeon_transition_list[temp_transition_num].cell_address].cell_stuffing.object_editor_id = map_name;
                Story_info.Map_data[dungeon_transition_list[temp_transition_num].map_name].cell_info[dungeon_transition_list[temp_transition_num].cell_address].cell_stuffing.object_editor_type = point_list[0];

                //Debug.Log("map[ " + dungeon_transition_list[temp_transition_num].map_name + " ].cell[ " + dungeon_transition_list[temp_transition_num].cell_address + " ] --> map[ " + map_name + " ].cell[ " + point_list[0] + " ]");

                dungeon_transition_list.RemoveAt(temp_transition_num);

                point_list.RemoveAt(0);
            }
        }

        point_list.Clear();

        if (dungeon_transition_list.Count > 0)
        {
            action_name = "Loading_map_story_dungeon_transition";
            Loading_map_story_dungeon_transition();
        }
        else
        {
            story_map_num = 0;

            action_name = "Loading_map_story_dungeon";
            Debug.Log("New_game_stages --- Начало создания подземелей");

            Start_dungeon_generation();
        }
    }
    #endregion


    #region Создаем подземелья
    public void Start_dungeon_generation()
    {
        action_name = "Loading_map_story_dungeon";

        story_map_num = 0;

        if (dungeon_list.Count > 0)
        {
            Loading_map_story_dungeon();
        }
        else if (region_teleportation.Count > 0)
        {
            Start_region_teleportation_generation();
        }
        else
        {
            End_story_generation();

        }
    }

    public void Loading_map_story_dungeon()
    {
        main_map_name = "подзимелья";
        Map_loading(dungeon_map_panel_list[main_rnd.Next(0, dungeon_map_panel_list.Count)].path);
    }

    public void Story_dungeon_generation()
    {
        int temp_transition_num = 0;

        map_name = "" + (map_num - 1);

        Debug.Log("4_New_game_stages --- Story_dungeon[" + map_name + "] generation");


        if (Story_info.Map_data[map_name].short_info.region_teleportation.Count > 0)
        {
            point_list = Story_info.Map_data[map_name].short_info.region_teleportation.Keys.ToList();

            for (int i = 0; i < point_list.Count; i++)
            {
                map_cell_name_script new_teleport = new map_cell_name_script();
                new_teleport.map_name = map_name;
                new_teleport.cell_address = point_list[i];

                region_teleportation.Add(new_teleport);
            }

            point_list.Clear();
        }


        if (Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Count > 0)
        {
            int temp_region_protagonist_start_point_num = 0;

            point_list = Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Keys.ToList();

            if (point_list.Count > 0)
            {
                for (temp_region_protagonist_start_point_num = 0; temp_region_protagonist_start_point_num < point_list.Count; temp_region_protagonist_start_point_num++)
                {
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing_active = 0;

                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.editor_id = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.editor_type = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.level = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.image_id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_id = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_editor_id = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_editor_type = "";
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_level = 0;
                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].cell_stuffing.object_image_id = 0;

                    Story_info.Map_data[map_name].cell_info[point_list[temp_region_protagonist_start_point_num]].default_active = 1;

                    Story_info.Map_data[map_name].short_info.region_protagonist_start_point.Remove(point_list[temp_region_protagonist_start_point_num]);
                }
            }
            point_list.Clear();
        }


        point_list = Story_info.Map_data[map_name].short_info.region_dungeon_exit.Keys.ToList();

        Story_info.Map_data[dungeon_list[0].map_name].cell_info[dungeon_list[0].cell_address].cell_stuffing.object_editor_id = map_name;
        Story_info.Map_data[dungeon_list[0].map_name].cell_info[dungeon_list[0].cell_address].cell_stuffing.object_editor_type = point_list[temp_transition_num];

        Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_id = dungeon_list[0].map_name;
        Story_info.Map_data[map_name].cell_info[point_list[temp_transition_num]].cell_stuffing.object_editor_type = dungeon_list[0].cell_address;

        point_list.RemoveAt(temp_transition_num);

        dungeon_list.RemoveAt(0);


        if (point_list.Count > 0)
        {
            map_name = "" + (map_num - 1);

            for (int i = 0; i < point_list.Count; i++)
            {
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing_active = 0;
                // обнулить полностью данные stuffing чтоб удалить выход

                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.id = 0;
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.editor_id = "";
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.editor_type = "";
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.level = 0;
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.image_id = 0;
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.object_id = 0;
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.object_editor_id = "";
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.object_editor_type = "";
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.object_level = 0;
                Story_info.Map_data[map_name].cell_info[point_list[i]].cell_stuffing.object_image_id = 0;

                Story_info.Map_data[map_name].short_info.region_dungeon_exit.Remove(point_list[i]);
            }
        }

        point_list.Clear();

        if (dungeon_list.Count > 0)
        {
            action_name = "Loading_map_story_dungeon";
            Loading_map_story_dungeon();
        }
        else
        {
            story_map_num = 0;

            action_name = "";
            Debug.Log("New_game_stages --- Начало создания телепортов");

            if (region_teleportation.Count > 0)
            {
                Start_region_teleportation_generation();
            }
            else
            {
                End_story_generation();
            }
        }
    }
    #endregion


    #region Создаем телепорты
    public void Start_region_teleportation_generation()
    {
        change_action = false;
        action_name = "Loading_map_story_teleportation";

        story_map_num = 0;

        if (region_teleportation.Count > 0)
        {
            Story_region_teleportation_generation();
        }
        else
        {
            End_story_generation();
        }
    }

    public void Story_region_teleportation_generation()
    {
        System.Random temp_rnd = new System.Random();

        int temp_transition_num = 0;


        Debug.Log("4_New_game_stages --- Story_teleportation[" + region_teleportation.Count + "] generation");

        if (region_teleportation.Count > 1)
        {
            temp_transition_num = temp_rnd.Next(0, region_teleportation.Count);
        }

        Story_info.Map_data[region_teleportation[0].map_name].cell_info[region_teleportation[0].cell_address].cell_stuffing.object_editor_id = region_teleportation[temp_transition_num].map_name;
        Story_info.Map_data[region_teleportation[0].map_name].cell_info[region_teleportation[0].cell_address].cell_stuffing.object_editor_type = region_teleportation[temp_transition_num].cell_address;

        Story_info.Map_data[region_teleportation[temp_transition_num].map_name].cell_info[region_teleportation[temp_transition_num].cell_address].cell_stuffing.object_editor_id = region_teleportation[0].map_name;
        Story_info.Map_data[region_teleportation[temp_transition_num].map_name].cell_info[region_teleportation[temp_transition_num].cell_address].cell_stuffing.object_editor_type = region_teleportation[0].cell_address;

        region_teleportation.RemoveAt(temp_transition_num);

        if (temp_transition_num != 0)
        {
            region_teleportation.RemoveAt(0);
        }


        if (region_teleportation.Count > 0)
        {
            action_name = "Loading_map_story_teleportation";
            change_action = true;
        }
        else
        {
            End_story_generation();
        }
    }
    #endregion


    #region Создаем телепорты
    public void End_story_generation()
    {
        action_name = "";
        Debug.Log("New_game_stages - Загрузка завершена");

        Main.Story_info.Add("0", Story_info);

        action_name = "Next_scene_open";
        change_action = true;
    }
    #endregion


    private void Next_scene_open()
    {
        Debug.Log(" -------------------------- End_map_data_generation");
    }
}