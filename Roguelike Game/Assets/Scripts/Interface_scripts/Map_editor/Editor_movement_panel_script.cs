using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class Editor_movement_panel_script : MonoBehaviour, IPointerClickHandler
{
    public Map_editor_stages map_editor;

    [Header("   основные элементы")]
    public GameObject main_object;
    public Transform main_transform;

    [Space]
    public GameObject button_gameObject;

    [Space]
    public RectTransform Main_Canvas_RectTransform;

    [Space]
    public RectTransform Editor_Canvas_RectTransform;
    public editor_cell_grid_script Editor_grid;

    [Space]
    private float movement_x;
    private float movement_y;
    private Vector3 mousePos;

    private float editor_x;
    private float editor_y;

    private int num_x;
    private int num_y;


    [Space]
    private bool one_click = false;
    private float dclick_threshold = 0.25f;
    private float timerdclick = 0;
    public string cell_address;

    private int cell_x;
    private int cell_y;


    [Space]
    private bool edit = false;

    private Cell_info_Blank cell_info;
    private bool new_editor = false;


    private bool drawing_cells = false;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        if (Main.movement_allowed == true)
        {
            Map_navigation();
        }


        if (drawing_cells == true)
        {
            Map_drawing_cells();
        }
    }





    #region Перемещение по карте
    private void Map_navigation()
    {
        movement_x = 0;
        movement_y = 0;

        mousePos = Input.mousePosition;

        movement_x = Main.movement.x - mousePos.x;
        movement_y = Main.movement.y - mousePos.y;

        //Debug.Log(mousePos.x + " = " + Main.movement.x);

        movement_x = Main_Canvas_RectTransform.position.x + movement_x;
        movement_y = Main_Canvas_RectTransform.position.y + movement_y;

        //Debug.Log("temp_x = " + movement_x + "       temp_y = " + movement_y);

        Main_Canvas_RectTransform.position = new Vector2(movement_x, movement_y);

        num_x = Convert.ToInt32(Math.Floor(movement_x / 240));
        num_y = Convert.ToInt32(Math.Floor(movement_y / 240));

        Editor_grid.Display_grid(num_x, num_y);

        editor_x = Convert.ToSingle(num_x * 240);// - 240);
        editor_y = Convert.ToSingle(num_y * 240);// - 240);

        //Debug.Log("editor_x = " + editor_x + "       editor_y = " + editor_y);

        Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);

        Main.movement = Input.mousePosition;



        // нижний левый угол
        if ((Editor_Canvas_RectTransform.position.x < 0) && (Editor_Canvas_RectTransform.position.y < 0))
        {
            editor_x = 0;
            editor_y = 0;
            Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);
        }

        // нижний правый угол
        if ((Editor_Canvas_RectTransform.position.x > (Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - 9 * 240)) && (Editor_Canvas_RectTransform.position.y < 0))
        {
            editor_x = Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - 9 * 240;
            editor_y = 0;
            Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);
        }

        // верхний правый угол
        if ((Editor_Canvas_RectTransform.position.x > (Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - 9 * 240)) && (Editor_Canvas_RectTransform.position.y > (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - 6 * 240)))
        {
            editor_x = Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - 9 * 240;
            editor_y = Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - 6 * 240;
            Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);
        }
        
        // верхний левый угол
        if ((Editor_Canvas_RectTransform.position.x < 0) && (Editor_Canvas_RectTransform.position.y > (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - 6 * 240)))
        {
            editor_x = 0;
            editor_y = Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - 6 * 240;
            Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);
        }
        
        // левая сторона
        if (Editor_Canvas_RectTransform.position.x < 0)
        {
            editor_x = 0;
            Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);
        }
        
        // нижняя сторона
        if (Editor_Canvas_RectTransform.position.y < 0)
        {
            editor_y = 0;
            Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);
        }
        
        // правая сторона
        if (Editor_Canvas_RectTransform.position.x > (Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - 9 * 240))
        {
            editor_x = Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - 9 * 240;
            Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);
        }

        // верхняя сторона
        if (Editor_Canvas_RectTransform.position.y > (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - 6 * 240))
        {
            editor_y = Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - 6 * 240;
            Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);
        }



        if (editor_x == 0)
        {
            num_x = 0;
        }
        else
        {
            num_x = Convert.ToInt32(Math.Floor(editor_x / 240));
        }

        if (editor_y == 0)
        {
            num_y = 0;
        }
        else
        {
            num_y = Convert.ToInt32(Math.Floor(editor_y / 240));
        }

        Editor_grid.Display_grid(num_x, num_y);
    }


    public void Mouse_Down()
    {
       //Debug.Log("Начинаем перетаскивание");
        Main.movement = Input.mousePosition;
        //Debug.Log(Input.mousePosition);
        Main.movement_allowed = true;
    }
    

    public void Mouse_Up()
    {
       //Debug.Log("мышка отжата за пределом");
        
        // нижний левый угол
        if ((Main_Canvas_RectTransform.position.x < -10) && (Main_Canvas_RectTransform.position.y < -60))
        {
            Main_Canvas_RectTransform.position = new Vector2(-10, -60);
        }
        
        // нижний правый угол
        if ((Main_Canvas_RectTransform.position.x > (Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - Main_Canvas_RectTransform.sizeDelta.x + 10)) && (Main_Canvas_RectTransform.position.y < -60))
        {
            Main_Canvas_RectTransform.position = new Vector2((Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - Main_Canvas_RectTransform.sizeDelta.x + 10), -60);
        }
        
        // верхний правый угол
        if ((Main_Canvas_RectTransform.position.x > (Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - Main_Canvas_RectTransform.sizeDelta.x + 10)) && (Main_Canvas_RectTransform.position.y > (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - Main_Canvas_RectTransform.sizeDelta.y + 60)))
        {
            Main_Canvas_RectTransform.position = new Vector2((Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - Main_Canvas_RectTransform.sizeDelta.x + 10), (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - Main_Canvas_RectTransform.sizeDelta.y + 60));
        }
        
        // верхний левый угол
        if ((Main_Canvas_RectTransform.position.x < -10) && (Main_Canvas_RectTransform.position.y > (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - Main_Canvas_RectTransform.sizeDelta.y + 60)))
        {
            Main_Canvas_RectTransform.position = new Vector2(-10, (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - Main_Canvas_RectTransform.sizeDelta.y + 60));
        }
        
        // левая сторона
        if (Main_Canvas_RectTransform.position.x < -10)
        {
            Main_Canvas_RectTransform.position = new Vector2(-10, movement_y);
        }
        
        // нижняя сторона
        if (Main_Canvas_RectTransform.position.y < -60)
        {
            Main_Canvas_RectTransform.position = new Vector2(movement_x, -60);
        }
        
        // правая сторона
        if (Main_Canvas_RectTransform.position.x > (Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - Main_Canvas_RectTransform.sizeDelta.x + 10))
        {
            Main_Canvas_RectTransform.position = new Vector2((Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 - Main_Canvas_RectTransform.sizeDelta.x + 10), movement_y);
        }
        
        // верхняя сторона
        if (Main_Canvas_RectTransform.position.y > (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - Main_Canvas_RectTransform.sizeDelta.y + 60))
        {
            Main_Canvas_RectTransform.position = new Vector2(movement_x, (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 - Main_Canvas_RectTransform.sizeDelta.y + 60));
        }



        if (Main.Story_info["0"].Map_data[Main.selected_map].map_height * 240 <= 4 * 240)
        {
            Main_Canvas_RectTransform.position = new Vector2(Main_Canvas_RectTransform.position.x, -60);
        }

        if (Main.Story_info["0"].Map_data[Main.selected_map].map_width * 240 <= 8 * 240)
        {
            Main_Canvas_RectTransform.position = new Vector2(-10, Main_Canvas_RectTransform.position.y);
        }

        Main.movement_allowed = false;
    }
    #endregion





    // Редактирование клеток карты
    #region Редактирование клетки по наведению мышки
    public void Mouse_Enter()
    {
        edit = true;
    }

    public void Mouse_Exit()
    {
        edit = false;
    }

    private void Double_Click__drawing_cells()
    {
        switch (drawing_cells)
        {
            case true:
                drawing_cells = false;
                break;
            case false:
                drawing_cells = true;
                break;
        }
    }

    private void Map_drawing_cells()
    {
        Double_Click_cell_draw();
    }
    #endregion


    #region Редактирование клетки по двойному щелчку
    public void OnPointerClick(PointerEventData eventData)
    {
        //  https://gamedev.stackexchange.com/questions/116455/how-to-properly-differentiate-single-clicks-and-double-click-in-unity3d-using-c

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (one_click != true)
            {
                timerdclick = eventData.clickTime;
                one_click = true;
            }
            else if (one_click && ((eventData.clickTime - timerdclick) < dclick_threshold))
            {
                //Debug.Log("double click");
                one_click = false;
                switch (Main.action_name)
                {
                    case "cell_draw":
                        Double_Click_cell_draw();
                        break;
                    case "drawing_cells":
                        Double_Click__drawing_cells();
                        break;
                }
            }
            else
            {
                timerdclick = eventData.clickTime;
                one_click = true;
            }
        }
    }

    public void Double_Click_cell_draw()
    {
        cell_x = Convert.ToInt32(Math.Floor((Main_Canvas_RectTransform.position.x + Input.mousePosition.x) / 240)) + 1;
        cell_y = Convert.ToInt32(Math.Floor((Main_Canvas_RectTransform.position.y + Input.mousePosition.y) / 240)) + 1;

        //cell_address = "" + cell_y + "_" + cell_x;
        cell_address = "" + Main.cell_x_label + cell_x + "_" + Main.cell_y_label + cell_y; //"" + cell_x + "_" + cell_y;

        //Debug.Log("Нажата клетка " + cell_address + " x = " + Main_Canvas_RectTransform.position.x + " + " + Input.mousePosition.x + " y = " + Main_Canvas_RectTransform.position.y + " + " + Input.mousePosition.y);

        Cell_Editor();
    }
    #endregion


    #region Редактирование клетки
    private void Cell_Editor()
    {
        cell_info = Main.Story_info["0"].Map_data[Main.selected_map].cell_info[cell_address];

        Debug.Log("Переходем на map_name = [ " + cell_info.cell_stuffing.object_editor_id + " ] cell_adress = [ " + cell_info.cell_stuffing.object_editor_type + " ]");

        Editing_cell_data();
    }
    #endregion

    #region Замена данных клетки обьекта выделеным обьектом
    public void Editing_cell_data()
    {
        Debug.Log("" + cell_info.cell_stuffing_active);

        if (cell_info.cell_stuffing_active != 0)
        {
            switch (cell_info.cell_stuffing.editor_type)
            {
                case "dungeon":
                    Edit_dungeon_data();
                    break;
                case "region":
                    Edit_dungeon_data();
                    break;
            }
        }
    }


    private void Edit_dungeon_data()
    {
        Test_move_MAP(Main.Story_info["0"].Map_data[cell_info.cell_stuffing.object_editor_id].cell_info[cell_info.cell_stuffing.object_editor_type].position_x, Main.Story_info["0"].Map_data[cell_info.cell_stuffing.object_editor_id].cell_info[cell_info.cell_stuffing.object_editor_type].position_y);
        //Debug.Log("map_name = " + cell_info.cell_stuffing.object_editor_id + " cell_adress = " + cell_info.cell_stuffing.object_editor_type);
        map_editor.Map_cell_Click(cell_info.cell_stuffing.object_editor_id, cell_info.cell_stuffing.object_editor_type);
    }


    private void Edit_region_data()
    {
        if (cell_info.cell_stuffing.id == 1)
        {
            Test_move_MAP(Main.Story_info["0"].Map_data[cell_info.cell_stuffing.object_editor_id].cell_info[cell_info.cell_stuffing.object_editor_type].position_x, Main.Story_info["0"].Map_data[cell_info.cell_stuffing.object_editor_id].cell_info[cell_info.cell_stuffing.object_editor_type].position_y);
            //Debug.Log("map_name = " + cell_info.cell_stuffing.object_editor_id + " cell_adress = " + cell_info.cell_stuffing.object_editor_type);
            map_editor.Map_cell_Click(cell_info.cell_stuffing.object_editor_id, cell_info.cell_stuffing.object_editor_type);
        }
        else if (cell_info.cell_stuffing.id == 5)
        {
            Test_move_MAP(Main.Story_info["0"].Map_data[cell_info.cell_stuffing.object_editor_id].cell_info[cell_info.cell_stuffing.object_editor_type].position_x, Main.Story_info["0"].Map_data[cell_info.cell_stuffing.object_editor_id].cell_info[cell_info.cell_stuffing.object_editor_type].position_y);
            //Debug.Log("map_name = " + cell_info.cell_stuffing.object_editor_id + " cell_adress = " + cell_info.cell_stuffing.object_editor_type);
            map_editor.Map_cell_Click(cell_info.cell_stuffing.object_editor_id, cell_info.cell_stuffing.object_editor_type);
        }
    }

    private void Test_move_MAP(int point_x, int point_y)
    {
        point_x = point_x - 1;
        point_y = point_y - 1;

        movement_x = point_x * 240;
        movement_y = point_y * 240;

        //Debug.Log("temp_x = " + movement_x + "       temp_y = " + movement_y);

        Main_Canvas_RectTransform.position = new Vector2(movement_x, movement_y);

        num_x = Convert.ToInt32(Math.Floor(movement_x / 240));
        num_y = Convert.ToInt32(Math.Floor(movement_y / 240));

        Editor_grid.Display_grid(num_x, num_y);

        editor_x = Convert.ToSingle(num_x * 240);// - 240);
        editor_y = Convert.ToSingle(num_y * 240);// - 240);

        //Debug.Log("editor_x = " + editor_x + "       editor_y = " + editor_y);

        Editor_Canvas_RectTransform.position = new Vector2(editor_x, editor_y);

        Map_navigation();

        Mouse_Up();
    }
    #endregion
}