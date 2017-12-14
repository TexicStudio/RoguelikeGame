using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map_object_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public Transform main_transform;

    [Space]
    [Header("   карта")]
    public GameObject main_map;
    public Transform main_map_transform;

    [Space]
    [Header("   данные")]
    public int row_max;
    public int column_max;

    [Space]
    public int cell_adress_row;
    public int cell_adress_column;

    [Space]
    public int cell_adress_row_max;
    public int cell_adress_column_max;

    [Space]
    private int num_max = 0;
    private int num = 0;
    private int num_cell = 0;

    private int pin_x = 1; //значение по Оси Х
    private int pin_y = 1; //значение по Оси Y

    private int i;
    private int j;

    private float pos_x;
    private float pos_y;

    private int map_num;
    private int map_num_max;


    private bool step_pause;
    private int step_limit = 200;
    private int step_min_num;
    private int step_max_num;
    private int step_num;

    [Space]
    public string cell_address;

    public string map_name;

    [Space]
    public int begin_row;
    public int end_row;
    public int begin_column;
    public int end_column;

    [Space]
    public Dictionary<string, map_cell_script> cell = new Dictionary<string, map_cell_script>();

    [Space]
    public float position_x;
    public float position_y;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (step_pause == true)
        {
            step_pause = false;
            Calculation_limit_cell_num();
        }
    }

    #region Создание карты
    public void Create_map(string new_map_name, int new_begin_row, int new_end_row, int new_begin_column, int new_end_column, int new_row_max, int new_column_max)
    {
        //main_object.SetActive(false);

        map_name = new_map_name;

        begin_row = new_begin_row;
        begin_column = new_begin_column;

        end_row = new_end_row;
        end_column = new_end_column;

        //end_row = Main.Map_info[map_name].map_height;
        //end_column = Main.Map_info[map_name].map_width;

        /*
        row_max = new_row_max;
        column_max = new_column_max;
        */
        row_max = new_end_row;
        column_max = new_end_column;


        //Debug.Log("colum = " + column_max + " / " + Main.Map_info[map_name].map_width);
        //Debug.Log("row = " + row_max + " / " + Main.Map_info[map_name].map_height);

        step_min_num = 0;

        if (Main.Story_info["0"].Map_data[map_name].map_cell_number > step_limit)
        {
            step_max_num = step_limit;
        }
        else
        {
            step_max_num = Main.Story_info["0"].Map_data[map_name].map_cell_number;
        }

        step_num = 1;

        pin_x = 1;
        pin_y = 1;

        Calculation_cell_adress();
    }

    void Calculation_cell_adress()
    {
        for (step_num = step_min_num; step_num <= step_max_num; step_num++)
        {
            cell_address = "" + Main.cell_x_label + pin_x + "_" + Main.cell_y_label + pin_y;


            Creat_cell(cell_address, pin_x, pin_y, true);

            if (pin_x == column_max)
            {
                pin_x = 1;
                pin_y = pin_y + 1;

                if (pin_y > row_max)
                {
                    pin_y = 1;
                }
            }
            else
            {
                pin_x = pin_x + 1;
            }
        }

        step_pause = true;
    }

    void Calculation_limit_cell_num()
    {
        if (step_num >= Main.Story_info["0"].Map_data[map_name].map_cell_number)
        {
            step_pause = false;
            //Debug.Log("Создание клеток карты завершено");
            //main_object.SetActive(true);
            //End_map_data_generation();
        }
        else
        {
            step_min_num = num;

            if ((step_max_num + step_limit) < (Main.Story_info["0"].Map_data[map_name].map_cell_number))
            {
                step_max_num = step_max_num + step_limit;
            }
            else
            {
                step_max_num = Main.Story_info["0"].Map_data[map_name].map_cell_number;
            }

            Calculation_cell_adress();
        }
    }
    #endregion



    #region Создание клетки карты
    public void Creat_cell(string new_cell_address, int new_cell_x, int new_cell_y, bool creat_map)
    {
        if (Main.Story_info["0"].Map_data[map_name].cell_info.ContainsKey(new_cell_address) == true)
        {
            if (cell.ContainsKey(new_cell_address) == false)
            {
                Cell_info_Blank temp_cell_info = Main.Story_info["0"].Map_data[map_name].cell_info[new_cell_address];

                GameObject new_cell_object = Instantiate(Resources.Load("Prefab/Map/Cell/map_cell", typeof(GameObject))) as GameObject;
                //new_cell_object.name = "" + temp_cell_info.id;

                map_cell_script new_cell = new_cell_object.GetComponent<map_cell_script>();

                new_cell._x = temp_cell_info.position_x;
                new_cell._y = temp_cell_info.position_y;


                //new_cell.Passability = temp_cell_info.Passability;
                //Debug.Log("map_object_script - " + temp_cell_info.address);


                new_cell.Display_cell(temp_cell_info);


                new_cell.main_transform.localScale = new Vector3(Main.scale_cell, Main.scale_cell, 1);

                new_cell_object.transform.SetParent(main_map.transform);

                pos_x = 240 * Main.scale_cell * (temp_cell_info.position_x - 1);
                pos_y = 240 * Main.scale_cell * (temp_cell_info.position_y - 1);


                //здесь будет прописываться наполнение клетки тагами
                //if (temp_cell_info.cell_type == 1)
                //{
                //new_cell_object.tag = "Passable Obj";
                string message = new_cell_object.name + " " + temp_cell_info.cell_stuffing_active + ", " + temp_cell_info.cell_stuffing.editor_type;
                if (temp_cell_info.cell_stuffing_active == 1)
                {
                    switch (temp_cell_info.cell_stuffing.editor_type)
                    {
                        case "bestiary":
                            {
                                int dbMonsterId = temp_cell_info.cell_stuffing.id;
                                Bestiary_Data monterInfo = Main.db_data.bestiary_data[dbMonsterId];
                                Debug.Log(monterInfo.bestiary_name);
                                new_cell_object.tag = "Monster Cell";
                                break;
                            }
                        case "relief":
                            {
                                new_cell_object.tag = "Resources";
                                break;
                            }
                    }
                }
                else
                {
                    if (temp_cell_info.cell_type == 0)
                    {
                        new_cell_object.tag = "Not Passable Obj";
                    }
                }
                Debug.Log(message);
                //}
                //else
                //{
                //    new_cell_object.tag = "Not Passable Obj";
                //}


                new_cell.main_transform.localPosition = new Vector3(pos_x, pos_y, 0);


                new_cell.map_frame = main_object.GetComponent<map_object_script>();

                cell.Add(new_cell_address, new_cell);

                //Debug.Log(new_cell_address + " СОЗДАНА ");
            }
            //else
            //{
            //   //Debug.Log(new_cell_address + " уже создана (лож) = " + cell.ContainsKey(new_cell_address));
            //}
        }
        //else
        //{
        //   //Debug.Log(new_cell_address + " НЕТ в Map_info = " + Main.Map_info[map_name].cell_info.ContainsKey(new_cell_address));
        //}
    }
    #endregion
}