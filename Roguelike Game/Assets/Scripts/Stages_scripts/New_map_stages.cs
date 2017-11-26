using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System;
using System.Linq;
using System.Collections.Generic;


public class New_map_stages : MonoBehaviour
{
    [Header("цвет")]
    public Color color_black;
    public Color color_white;
    public Color color_colorless;

    [Space]

    private int map_num;
    private int map_num_max;

    private int num = 0;
    private int i = 0;
    private int j = 0;
    private int stuff_i = 0;
    //private int but_x = 0;
    //private int but_y = 0;

    private int max_num;
    private int min_num;

    private int step_num;
    private bool step_pause;

    private string map_name;
    private List<string> list_map_name = new List<string>();

    private Coverage_Data main_coverage_data;

    public Font my_font;

    [Header("Название карты")]
    public GameObject map_name_panel;
    public InputField map_name_InputField;

    [Header("Размер карты")]
    public GameObject size_panel;
    public GameObject size_label;
    public Text size_text;
    public GameObject width_panel;
    public InputField width_InputField;
    public GameObject height_panel;
    public InputField height_InputField;

    [Header("Основное покрытие")]
    public GameObject main_coverage;
    public Image main_coverage_image;
    public Text main_coverage_name;
    public Text main_coverage_type;

    [Header("Список покрытий (Земля)")]
    public GameObject coverage_list;
    public GameObject coverage_list_frame;
    public GameObject musk_coverage;
    public GameObject coverage_type_frame;
    public RectTransform coverage_type_frame_rectTransform;
    public VerticalLayoutGroup coverage_type_frame_verticalLayoutGroup;
    public float coverage_type_frame_size;

    [Space]
    public Button ready_button;

    [Space]
    private int mask_bar_size_step_num;
    private float mask_bar_size_step;
    private float mask_bar_progress_size_step;
    private float mask_bar_size;

    [Header("Процес обработки")]
    public GameObject new_map_progress_canvas;
    public GameObject new_map_progress_bar;
    public GameObject mask_progress_bar;
    public RectTransform mask_progress_bar_rectTransform;
    public Text progress_text;

    private bool next_scene = false;

    private List<GameObject> map_list;

    // Use this for initialization
    void Start()
    {
        Create_scene();
    }

    void Update()
    {
        if (step_pause == true)
        {
            step_pause = false;
            Calculation_limit_cell_num();
        }

        if (next_scene == true)
        {
            next_scene = false;
            Nest_scene_open();// Creat_default_Relief_data();
        }
    }

    public void Create_scene()
    {
        width_InputField.onEndEdit.AddListener(width_InputField_onEndEdit);
        height_InputField.onEndEdit.AddListener(height_InputField_onEndEdit);

        ready_button.onClick.AddListener(() => Ready_button_Click());

        width_InputField.onEndEdit.AddListener(width_InputField_onEndEdit);
        height_InputField.onEndEdit.AddListener(height_InputField_onEndEdit);

        Create_new_map_coverage_list();
    }

    #region Список из елементов земли, который разделен по типу.
    public void Create_new_map_coverage_list()
    {
        // Создаеться список из елементов земли, который разделен по типу.

        int temp_coverage_count;

        Dispay_new_map_main_coverage(1);

        coverage_type_frame_size = 0;

        foreach (Info_Type_name_Data temp_coverage_type in Main.db_data.coverage_type)
        {
            GameObject new_coverage_type_gameObject = Instantiate(Resources.Load("Prefab/Interface/New_map/new_map_object_panel", typeof(GameObject))) as GameObject;
            new_coverage_type_gameObject.transform.SetParent(coverage_type_frame.transform);
            new_coverage_type_gameObject.transform.SetAsLastSibling();

            new_coverage_type_gameObject.name = "" + temp_coverage_type._name;

            new_map_coverage_type_script new_coverage_type = new_coverage_type_gameObject.GetComponent<new_map_coverage_type_script>();

            new_coverage_type.main_panel_title.text = "" + temp_coverage_type._name_ru;

            IEnumerable<KeyValuePair<int, Coverage_Data>> temp_coverage_data = Main.db_data.coverage_data.Where(coverege => coverege.Value.type_id == temp_coverage_type.id);

            temp_coverage_count = 0;

            foreach (KeyValuePair<int, Coverage_Data> temp_coverage in temp_coverage_data)
            {
                temp_coverage_count = temp_coverage_count + 1;

                GameObject new_coverage_cell_gameObject = Instantiate(Resources.Load("Prefab/Interface/New_map/new_map_object", typeof(GameObject))) as GameObject;
                new_coverage_cell_gameObject.name = "" + temp_coverage.Value.id;
                new_coverage_cell_gameObject.transform.SetParent(new_coverage_type.main_panel.transform);

                new_map_coverage_button_script new_coverage_cell = new_coverage_cell_gameObject.GetComponent<new_map_coverage_button_script>();

                new_coverage_cell.object_id = temp_coverage.Value.id;

                if (temp_coverage.Value.image_id == 0)
                {
                    new_coverage_cell._image.sprite = null;
                    new_coverage_cell._image.color = color_black;
                }
                else
                {
                    new_coverage_cell._image.sprite = Main.Image_list[Main.db_data.image_data[temp_coverage.Value.image_id].image_name];
                }
                new_coverage_cell._button.onClick.AddListener(() => Dispay_new_map_main_coverage(new_coverage_cell.object_id));
            }

            new_coverage_type.Windows_resize(temp_coverage_count);

            coverage_type_frame_size = coverage_type_frame_size + coverage_type_frame_verticalLayoutGroup.spacing + new_coverage_type.size;
        }

        coverage_type_frame_size = coverage_type_frame_size - coverage_type_frame_verticalLayoutGroup.spacing + coverage_type_frame_verticalLayoutGroup.padding.bottom;

        coverage_type_frame_rectTransform.sizeDelta = new Vector2(coverage_type_frame_rectTransform.sizeDelta.x, coverage_type_frame_size);
    }
    #endregion

    #region Отображаем данные основного эелемента покрития земли
    public void Dispay_new_map_main_coverage(int item_id)
    {
        main_coverage_data = Main.db_data.coverage_data[item_id];

        if (main_coverage_data.image_id == 0)
        {
            main_coverage_image.sprite = null;
            main_coverage_image.color = color_colorless;
        }
        else
        {
            main_coverage_image.sprite = Main.Image_list[Main.db_data.image_data[main_coverage_data.image_id].image_name];
            main_coverage_image.color = color_white;
        }

        main_coverage_name.text = "" + main_coverage_data.coverage_name;
        main_coverage_type.text = "" + main_coverage_data.type_name_ru;
    }
    #endregion

    #region Ввод данрых, размер карты
    public void width_InputField_onEndEdit(string temp_string)
    {
        // проверяет какое значение было введено для шерены
        if (temp_string == "")
        {
            width_InputField.text = "1";
        }
        else if (Int32.Parse(temp_string) == 0)
        {
            width_InputField.text = "1";
        }

        Calculate_map_size();
    }

    public void height_InputField_onEndEdit(string temp_string)
    {
        // проверяет какое значение было введено для высоты
        if (temp_string == "")
        {
            height_InputField.text = "1";
        }
        else if (Int32.Parse(temp_string) == 0)
        {
            height_InputField.text = "1";
        }

        Calculate_map_size();
    }

    private void Calculate_map_size()
    {
        // вычисляется размер карты
        if (width_InputField.text == "")
        {
            width_InputField.text = "1";
        }

        if (height_InputField.text == "")
        {
            height_InputField.text = "1";
        }

        size_text.text = "" + (Int32.Parse(width_InputField.text) * Int32.Parse(height_InputField.text));
    }
    #endregion

    #region Создание данных новой карты
    public void Ready_button_Click()
    {
        step_pause = false;

        if ((width_InputField.text != "") && (height_InputField.text != ""))
        {
            Destroy(ready_button);

            Ready_button_process_beginning();
        }
    }

    private void Ready_button_process_beginning()
    {
        // Нажата кнопка "готово". Начало создание карты.

        Map_info_Blank Map_info = new Map_info_Blank();

        if (Main.db_data.map_list_count > 0)
        {
            Map_info.id = Main.db_data.map_list.Values.Last().id + 1;
        }
        else
        {
            Map_info.id = 1;
        }

        if (map_name_InputField.text == "")
        {
            map_name_InputField.text = "New Map " + width_InputField.text + " x " + height_InputField.text;
        }

        map_name = "" + Map_info.id;
        list_map_name.Add(map_name);

        Map_info.name = "" + map_name_InputField.text;

        Map_info.map_cell_number = Int32.Parse(size_text.text);
        Map_info.map_width = Int32.Parse(width_InputField.text);
        Map_info.map_height = Int32.Parse(height_InputField.text);

       //Debug.Log(map_name);

        Main.Map_info.Add(map_name, Map_info);

        Create_new_map_generation_windows();
    }

    private void Create_new_map_generation_windows()
    {
        // Удаляем старое окно и создаем новое окно которое показыет прогресс создания карты
        new_map_progress_canvas.SetActive(true);

        j = Main.Map_info.Count;

        mask_bar_size_step = mask_progress_bar_rectTransform.sizeDelta.x / j;

        mask_bar_size_step_num = 0;
        mask_progress_bar_rectTransform.sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar_rectTransform.sizeDelta.y);
        progress_text.text = "Заполняем данные карты";

        map_num_max = Main.Map_info.Count;
        map_num = 0;

        Begin_map_data_generation();
    }

    public void Begin_map_data_generation()
    {
        // функции которые создают данные для карт.

        j = 1;
        i = 1;

        step_num = 0;
        num = 1;
        min_num = 1;
        max_num = 0;

        step_pause = false;

        if ((Main.Map_info[list_map_name[map_num]].map_cell_number) > 1000)
        {
            step_num = 1000;
            max_num = 1000;

            if ((Main.Map_info[list_map_name[map_num]].map_cell_number % 1000) != 0)
            {
                mask_bar_progress_size_step = Main.Map_info[list_map_name[map_num]].map_cell_number / 1000 + 1;
            }
            else
            {
                mask_bar_progress_size_step = Main.Map_info[list_map_name[map_num]].map_cell_number / 1000;
            }

            mask_bar_progress_size_step = mask_bar_size_step / mask_bar_progress_size_step;
        }
        else
        {
            max_num = Main.Map_info[list_map_name[map_num]].map_cell_number;
        }

        Map_data_generation();
    }

    void Map_data_generation()
    {
        System.Random temp_rnd = new System.Random();

        for (num = min_num; num <= max_num; num++)
        {
            Cell_info_Blank temp_cell_info = new Cell_info_Blank();

            //Debug.Log(num);

            temp_cell_info.id = num;
            temp_cell_info.address = "" + j + "_" + i;

            temp_cell_info.position_x = i;
            temp_cell_info.position_y = j;

            //Debug.Log(num + " = " + j + "_" + i);

            temp_cell_info.cell_base_id = main_coverage_data.id;


            temp_cell_info.cell_decoration_id = 0; // temp_rnd.Next(0, 11);
            //temp_cell_info.cell_decoration_x = 0;
            //temp_cell_info.cell_decoration_y = 0;


            temp_cell_info.cell_stuffing_active = 0;

            temp_cell_info.cell_stuffing = new Cell_stuffing_Blank();
            temp_cell_info.cell_stuffing.id = 0;
            temp_cell_info.cell_stuffing.editor_id = "";
            temp_cell_info.cell_stuffing.editor_type = "";
            temp_cell_info.cell_stuffing.level = 0;
            temp_cell_info.cell_stuffing.image_id = 0;
            temp_cell_info.cell_stuffing.object_active = 0;
            temp_cell_info.cell_stuffing.object_id = 0;
            temp_cell_info.cell_stuffing.object_editor_id = "";
            temp_cell_info.cell_stuffing.object_editor_type = "";
            temp_cell_info.cell_stuffing.object_level = 0;
            temp_cell_info.cell_stuffing.object_image_id = 0;


            temp_cell_info.default_active = 0;

            temp_cell_info.default_bestiary = new Cell_stuffing_Blank();
            temp_cell_info.default_bestiary.id = 0;
            temp_cell_info.default_bestiary.editor_id = "";
            temp_cell_info.default_bestiary.editor_type = "";
            temp_cell_info.default_bestiary.level = 0;
            temp_cell_info.default_bestiary.image_id = 0;
            temp_cell_info.default_bestiary.object_active = 0;
            temp_cell_info.default_bestiary.object_id = 0;
            temp_cell_info.default_bestiary.object_editor_id = "";
            temp_cell_info.default_bestiary.object_editor_type = "";
            temp_cell_info.default_bestiary.object_level = 0;
            temp_cell_info.default_bestiary.object_image_id = 0;



            Main.Map_info[list_map_name[map_num]].cell_info.Add(temp_cell_info.address, temp_cell_info);

            if (i == Main.Map_info[list_map_name[map_num]].map_width)
            {
                i = 1;
                j = j + 1;

                if(j > Main.Map_info[list_map_name[map_num]].map_height)
                {
                    j = 1;
                }
            }
            else
            {
                i = i + 1;
            }

            progress_text.text = "" + Main.Map_info[list_map_name[map_num]].name + "  // клетка " + temp_cell_info.id + " (" + temp_cell_info.address + ")";

            //Debug.Log(temp_cell_info.address + " в Map_info = " + Main.Map_info[list_map_name[map_num]].cell_info.ContainsKey(temp_cell_info.address));
        }
        step_pause = true;
    }

    void Calculation_limit_cell_num()
    {
        mask_bar_size = (mask_bar_size_step_num * mask_bar_size_step) + (mask_bar_progress_size_step * max_num / step_num);

        mask_progress_bar_rectTransform.sizeDelta = new Vector2(mask_bar_size, mask_progress_bar_rectTransform.sizeDelta.y);
        progress_text.text = "";

        if (num >= Main.Map_info[list_map_name[map_num]].map_cell_number)
        {
            End_map_data_generation();
        }
        else
        {
            min_num = num;

            if ((max_num + step_num) < (Main.Map_info[list_map_name[map_num]].map_cell_number))
            {
                max_num = max_num + step_num;
            }
            else
            {
                max_num = Main.Map_info[list_map_name[map_num]].map_cell_number;
            }

            Map_data_generation();
        }
    }

    private void End_map_data_generation()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar_rectTransform.sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar_rectTransform.sizeDelta.y);
        progress_text.text = "";


        if ((map_num + 1) == map_num_max)
        {
            mask_progress_bar_rectTransform.sizeDelta = new Vector2(480, mask_progress_bar_rectTransform.sizeDelta.y);
            progress_text.text = "";

            next_scene = true;
        }
        else
        {
            map_num = map_num + 1;

            main_coverage_data = Main.db_data.coverage_data[map_num];

            num = main_coverage_data.image_id;

            Begin_map_data_generation();
        }
    }
    #endregion


    
    #region Создание default Relief обьектов
    /*public void Creat_default_Relief_data()
    {
        foreach (KeyValuePair<int, Relief_Data> item_info in Main.db_data.relief_data)
        {
            if (item_info.Value.type_id != 2)
            {
                if (item_info.Value.type_id != 4)
                {
                    switch (item_info.Value.type_id)
                    {
                        case 1:
                            switch (item_info.Value.id)
                            {
                                case 2:
                                    Edit_default_Relief_data(item_info.Value, 1, 29);
                                    break;
                                case 4:
                                    Edit_default_Relief_data(item_info.Value, 3, 29);
                                    break;
                            }
                            break;
                        case 3:
                            Edit_default_Relief_data(item_info.Value, 4, 28);
                            break;
                        case 5:
                            Edit_default_Relief_data(item_info.Value, item_info.Value.id, 0);
                            break;
                    }

                }
            }
        }

        Nest_scene_open();
    }

    public void Edit_default_Relief_data(Relief_Data default_info, int default_place_after_extraction_id, int default_resource_id)
    {
        Image_Data temp_image_info = new Image_Data();

        Info_Relief new_default_relief = new Info_Relief();

        new_default_relief.editor_id = "default_" + default_info.relief_name;
        new_default_relief.editor_type = "relief";

        new_default_relief.editor_name = "default_" + default_info.relief_name;

        new_default_relief.info_active = 1;


        new_default_relief.production_place_id = default_info.id;

        new_default_relief.place_after_extraction_id = default_place_after_extraction_id;


        new_default_relief.default_id = new_default_relief.production_place_id;


        new_default_relief.resource = new Cell_stuffing_Blank();
        if (default_resource_id != 0)
        {
            new_default_relief.resource.editor_id = "default_" + Main.db_data.stuff_data[default_resource_id].stuff_name;
            new_default_relief.resource.editor_type = "stuff";
            new_default_relief.resource.id = Main.db_data.stuff_data[default_resource_id].id;
            new_default_relief.resource.image_id = Main.db_data.stuff_data[default_resource_id].image_id;
            new_default_relief.resource.level = 1;
        }
        else
        {
            new_default_relief.resource.editor_id = "";
            new_default_relief.resource.editor_type = "";
            new_default_relief.resource.id = 0;
            new_default_relief.resource.image_id = 0;
            new_default_relief.resource.level = 0;
        }


        new_default_relief.resource_value = 3;
        new_default_relief.resource_value_min = 1;
        new_default_relief.resource_value_max = 3;

        new_default_relief.cooldown_value = 3;
        new_default_relief.cooldown_min = 1;
        new_default_relief.cooldown_max = 3;

        Main.item_data.relief.Add(new_default_relief.editor_name, new_default_relief);
    }*/
    #endregion
    

    private void Nest_scene_open()
    {
       //Debug.Log(" -------------------------- End_map_data_generation");
        SceneManager.LoadScene("5_Map_Editor");
    }
}