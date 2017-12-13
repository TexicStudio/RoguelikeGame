using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;

using System.Collections.Generic;

public class Loading_objects_stages : MonoBehaviour
{
    private Database_connection Data_Base = new Database_connection();

    private IDataReader sql_data;

    private int sql_num;
    //private int sql_line_num;

    private int mask_bar_size_step_num;
    private int mask_bar_size_step = 20;

    public GameObject mask_progress_bar;
    public Text loading_text;

    public Sprite[] sprites;

    public Sprite[] level_sprites;

    // Use this for initialization
    void Start()
    {
        Create_scene();
    }

    public void Create_scene()
    {
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2(0, mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        mask_bar_size_step_num = 0;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);
        loading_text.text = "Подключение к БД";

        Data_Base.Connection();
        
        Map_list_count();
    }

    #region Подсчет колисечтва карт в БД, и загрузка списка карт, если их больше 0
    private void Map_list_count()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        string sqlQuery = "SELECT count(*) FROM map";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            sql_num = sql_data.GetInt32(0);
        }
        sql_data.Close();
        sql_data = null;

        Main.db_data.map_list_count = sql_num;

        if (Main.db_data.map_list_count > 0)
        {
            Map_list_loading();
        }
        else
        {
            Image_count();
        }
    }

    private void Map_list_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Map_list";

        string sqlQuery = "SELECT map.id, map.map_name, map.file_name FROM map"; //map.minimum_level, map.maximum_level FROM map";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Map_list_Data Map_list = new Map_list_Data();

            Map_list.id = sql_data.GetInt32(0);
            Map_list.map_name = sql_data.GetString(1);
            Map_list.file_name = sql_data.GetString(2);
            //Map_list.minimum_level = sql_data.GetInt32(3);
            //Map_list.maximum_level = sql_data.GetInt32(4);

            Main.db_data.map_list.Add(Map_list.id, Map_list);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Map_list[0].map_name);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */

        Map_type_loading();
    }

    private void Map_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Map_type";

        string sqlQuery = "SELECT map_type.id, map_type.type_name, map_type.type_name_ru FROM map_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Map_type = new Info_Type_name_Data();

            sql_num = 0;
            Map_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Map_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Map_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.map_type.Add(Map_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Map_list[0].map_name);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Image_count();
    }

    #endregion

    #region Подсчет, и загрузка данных с таблици Image, Image_type
    private void Image_count()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        string sqlQuery = "SELECT count(*) FROM image";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            sql_num = sql_data.GetInt32(0);
        }
        sql_data.Close();
        sql_data = null;

        loading_text.text = "Загрузка изображений";

        foreach (Sprite sprite in sprites)
        {
            loading_text.text = "Загрузка изображения '\"' " + sprite.name + " '\"'";
            Main.Image_list.Add(sprite.name, sprite);
        }

        for (int temp_num = 1; temp_num <= 100; temp_num++)
        {
            loading_text.text = "Загрузка изображения LvL '\"' " + temp_num + " '\"'";
            Main.level_image_list.Add(temp_num, level_sprites[temp_num - 1]);
        }

        Image_data_loading();
    }

    private void Image_data_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Image_data";

        string sqlQuery = "SELECT image.id, image.type_id, image_type.type_name, image_type.type_name_ru, image.image_name, image.width, image.height, image.x, image.y FROM image, image_type WHERE image.type_id = image_type.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Image_Data Image_data = new Image_Data();

            sql_num = 0;
            Image_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Image_data.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Image_data.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Image_data.type_name_ru = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Image_data.image_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Image_data._width = sql_data.GetFloat(sql_num);
            sql_num = sql_num + 1;
            Image_data._height = sql_data.GetFloat(sql_num);
            sql_num = sql_num + 1;
            Image_data._x = sql_data.GetFloat(sql_num);
            sql_num = sql_num + 1;
            Image_data._y = sql_data.GetFloat(sql_num);

            Main.db_data.image_data.Add(Image_data.id, Image_data);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Image_data[0].image_name);
       //Debug.Log(Main.Image_data[105].image_name);
       //Debug.Log(Main.Image_data.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Image_type_loading();
    }

    private void Image_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Image_type";

        string sqlQuery = "SELECT image_type.id, image_type.type_name, image_type.type_name_ru FROM image_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Image_type = new Info_Type_name_Data();

            sql_num = 0;
            Image_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Image_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Image_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.image_type.Add(Image_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Image_type.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Point_type_loading();
    }
    #endregion

    #region Загрузка данных с таблици Point_type
    private void Point_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Point_type";

        string sqlQuery = "SELECT point_type.id, point_type.type_name, point_type.type_name_ru FROM point_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Point_type = new Info_Type_name_Data();

            sql_num = 0;
            Point_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Point_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Point_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.point_type.Add(Point_type.id, Point_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Point_type.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Coverage_loading();
    }
    #endregion

    #region Загрузка данных с таблици Coverage, Coverage_type
    private void Coverage_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Coverage";

        string sqlQuery = "SELECT coverage.id, coverage.type_id, coverage_type.type_name, coverage_type.type_name_ru, coverage.coverage_name, coverage.image_id " +
                "FROM coverage, coverage_type WHERE coverage.type_id = coverage_type.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Coverage_Data Coverage_data = new Coverage_Data();

            sql_num = 0;
            Coverage_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Coverage_data.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Coverage_data.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Coverage_data.type_name_ru = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Coverage_data.coverage_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Coverage_data.image_id = sql_data.GetInt32(sql_num);

            Main.db_data.coverage_data.Add(Coverage_data.id, Coverage_data);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Coverage_data.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Coverage_type_loading();
    }

    private void Coverage_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Coverage_type";

        string sqlQuery = "SELECT coverage_type.id, coverage_type.type_name, coverage_type.type_name_ru FROM coverage_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Coverage_type = new Info_Type_name_Data();

            sql_num = 0;
            Coverage_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Coverage_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Coverage_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.coverage_type.Add(Coverage_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Coverage_type.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Cell_decoration_loading();
    }
    #endregion

    #region Загрузка данных с таблици Cell_decoration, Cell_decoration_type
    private void Cell_decoration_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Cell_decoration";

        string sqlQuery = "SELECT cell_decoration.id, cell_decoration.type_id, cell_decoration_type.type_name, cell_decoration_type.type_name_ru, cell_decoration.decoration_name, cell_decoration.image_id " +
                "FROM cell_decoration, cell_decoration_type WHERE cell_decoration.type_id = cell_decoration_type.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Cell_decoration_Data Cell_decoration_data = new Cell_decoration_Data();

            sql_num = 0;
            Cell_decoration_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Cell_decoration_data.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Cell_decoration_data.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Cell_decoration_data.type_name_ru = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Cell_decoration_data.decoration_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Cell_decoration_data.image_id = sql_data.GetInt32(sql_num);

            Main.db_data.cell_decoration.Add(Cell_decoration_data.id, Cell_decoration_data);
        }
        sql_data.Close();
        sql_data = null;

        Cell_decoration_type_loading();
    }

    private void Cell_decoration_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Cell_decoration_type";

        string sqlQuery = "SELECT cell_decoration_type.id, cell_decoration_type.type_name, cell_decoration_type.type_name_ru FROM cell_decoration_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Cell_decoration_type = new Info_Type_name_Data();

            sql_num = 0;
            Cell_decoration_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Cell_decoration_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Cell_decoration_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.cell_decoration_type.Add(Cell_decoration_type);
        }
        sql_data.Close();
        sql_data = null;
        
        Relief_loading();
    }
    #endregion

    #region Загрузка данных с таблици Relief, Relief_type
    private void Relief_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Relief";

        string sqlQuery = "SELECT relief.id, relief.type_id, relief_type.type_name, relief_type.type_name_ru, relief.relief_name, relief.image_id, image.image_name FROM relief, relief_type, image " +
                "WHERE relief.type_id = relief_type.id AND relief.image_id = image.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Relief_Data Relief_data = new Relief_Data();

            sql_num = 0;
            Relief_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Relief_data.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Relief_data.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Relief_data.type_name_ru = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Relief_data.relief_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Relief_data.image_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Relief_data.image_name = sql_data.GetString(sql_num);

            Main.db_data.relief_data.Add(Relief_data.id, Relief_data);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Relief_data.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Relief_type_loading();
    }

    private void Relief_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Relief_type";

        string sqlQuery = "SELECT relief_type.id, relief_type.type_name, relief_type.type_name_ru FROM relief_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Relief_type = new Info_Type_name_Data();

            sql_num = 0;
            Relief_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Relief_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Relief_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.relief_type.Add(Relief_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Relief_type.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Dungeon_loading();
    }
    #endregion

    #region Загрузка данных с таблици Dungeon, Dungeon_type
    private void Dungeon_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Dungeon";

        string sqlQuery = "SELECT dungeon.id, dungeon.type_id, dungeon_type.type_name, dungeon_type.type_name_ru, dungeon.dungeon_name, dungeon.image_id " +
                "FROM dungeon, dungeon_type WHERE dungeon.type_id = dungeon_type.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Dungeon_Data Dungeon_data = new Dungeon_Data();

            sql_num = 0;
            Dungeon_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Dungeon_data.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Dungeon_data.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Dungeon_data.type_name_ru = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Dungeon_data.dungeon_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Dungeon_data.image_id = sql_data.GetInt32(sql_num);

            Main.db_data.dungeon_data.Add(Dungeon_data.id, Dungeon_data);
        }
        sql_data.Close();
        sql_data = null;

        Dungeon_type_loading();
    }

    private void Dungeon_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Dungeon_type";

        string sqlQuery = "SELECT dungeon_type.id, dungeon_type.type_name, dungeon_type.type_name_ru FROM dungeon_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Dungeon_type = new Info_Type_name_Data();

            sql_num = 0;
            Dungeon_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Dungeon_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Dungeon_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.dungeon_type.Add(Dungeon_type);
        }
        sql_data.Close();
        sql_data = null;

        Point_buffs_loading();
    }
    #endregion

    #region Загрузка данных с таблици Point_buffs, Point_buffs_type
    private void Point_buffs_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Point_buffs";

        string sqlQuery = "SELECT point_buffs.id, point_buffs.type_id, point_buffs_type.type_name, point_buffs_type.type_name_ru, point_buffs.point_buffs_name, point_buffs.image_id " +
                "FROM point_buffs, point_buffs_type WHERE point_buffs.type_id = point_buffs_type.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Point_buffs_Data Point_buffs_data = new Point_buffs_Data();

            sql_num = 0;
            Point_buffs_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Point_buffs_data.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Point_buffs_data.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Point_buffs_data.type_name_ru = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Point_buffs_data.point_buffs_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Point_buffs_data.image_id = sql_data.GetInt32(sql_num);

            Main.db_data.point_buffs_data.Add(Point_buffs_data.id, Point_buffs_data);
        }
        sql_data.Close();
        sql_data = null;

        Point_buffs_type_loading();
    }

    private void Point_buffs_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Point_buffs_type";

        string sqlQuery = "SELECT point_buffs_type.id, point_buffs_type.type_name, point_buffs_type.type_name_ru FROM point_buffs_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Point_buffs_type = new Info_Type_name_Data();

            sql_num = 0;
            Point_buffs_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Point_buffs_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Point_buffs_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.point_buffs_type.Add(Point_buffs_type);
        }
        sql_data.Close();
        sql_data = null;

        Region_loading();
    }
    #endregion

    #region Загрузка данных с таблици Region, Region_type
    private void Region_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Region";

        string sqlQuery = "SELECT region.id, region.type_id, region_type.type_name, region_type.type_name_ru, region.region_name, region.image_id " +
                "FROM region, region_type WHERE region.type_id = region_type.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Region_Data Region_data = new Region_Data();

            sql_num = 0;
            Region_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Region_data.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Region_data.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Region_data.type_name_ru = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Region_data.region_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Region_data.image_id = sql_data.GetInt32(sql_num);

            Main.db_data.region_data.Add(Region_data.id, Region_data);
        }
        sql_data.Close();
        sql_data = null;

        Region_type_loading();
    }

    private void Region_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Region_type";

        string sqlQuery = "SELECT region_type.id, region_type.type_name, region_type.type_name_ru FROM region_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Region_type = new Info_Type_name_Data();

            sql_num = 0;
            Region_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Region_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Region_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.region_type.Add(Region_type);
        }
        sql_data.Close();
        sql_data = null;

        Stuff_loading();
    }
    #endregion

    #region Загрузка данных с таблици Stuff, Stuff_type, Stuff_class, Stuff_abilities, Stuff_abilities_type, Stuff_abilities_class
    private void Stuff_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Stuff";

        string sqlQuery = "SELECT stuff.id, stuff.type_id, stuff_type.type_name_ru, stuff.class_id, stuff_class.class_name_ru, stuff.stuff_name, " +
            "stuff.image_id, image.image_name, stuff.phys_damage_min, stuff.phys_damage_max, stuff.magic_damage_min, stuff.magic_damage_max, " +
            "stuff.strength_min, stuff.strength_max, stuff.agility_min, stuff.agility_max, stuff.vitality_min, stuff.vitality_max, " +
            "stuff.intelligence_min, stuff.intelligence_max, stuff.all_stats_min, stuff.all_stats_max, stuff.crit_chance_min_p, stuff.crit_chance_max_p, " +
            "stuff.crit_power_min_p, stuff.crit_power_max_p, stuff.initiative_min, stuff.initiative_max, stuff.health_regeneration_min_p, stuff.health_regeneration_max_p, " +
            "stuff.penetration_min_p, stuff.penetration_max_p, stuff.phys_armor_min_p, stuff.phys_armor_max_p, stuff.magic_armor_min, stuff.magic_armor_max, " +
            "stuff.health_min_p, stuff.health_max_p, stuff.evasion_min, stuff.evasion_max, stuff.close_combat_dmg_min, stuff.close_combat_dmg_max, " +
            "stuff.bleed_dmg_min, stuff.bleed_dmg_max, stuff.satiety_min, stuff.satiety_max, stuff.endurance_min, stuff.endurance_max, stuff.stackable, " +
            "stuff.durability, stuff.price, stuff.cooldown, stuff.duration, stuff.ability_id, stuff.description " +
            "FROM stuff , stuff_class , stuff_type , image " +
            "WHERE stuff.type_id = stuff_type.id AND stuff.class_id = stuff_class.id AND stuff.image_id = image.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Stuff_Data Stuff_data = new Stuff_Data();

            sql_num = 0;
            Stuff_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.class_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.class_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.stuff_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.image_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.image_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.phys_damage_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.phys_damage_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.magic_damage_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.magic_damage_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.strength_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.strength_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.agility_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.agility_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.vitality_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.vitality_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.intelligence_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.intelligence_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.all_stats_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.all_stats_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.crit_chance_min_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.crit_chance_max_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.crit_power_min_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.crit_power_max_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.initiative_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.initiative_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.health_regeneration_min_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.health_regeneration_max_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.penetration_min_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.penetration_max_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.phys_armor_min_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.phys_armor_max_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.magic_armor_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.magic_armor_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.health_min_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.health_max_p = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.evasion_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.evasion_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.close_combat_dmg_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.close_combat_dmg_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.bleed_dmg_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.bleed_dmg_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.satiety_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.satiety_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.endurance_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.endurance_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.stackable = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.durability = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.price = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.cooldown = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.duration = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.ability_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_data.description = sql_data.GetString(sql_num);

            Main.db_data.stuff_data.Add(Stuff_data.id, Stuff_data);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_data.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Stuff_type_loading();
    }

    private void Stuff_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Stuff_type";

        string sqlQuery = "SELECT stuff_type.id, stuff_type.type_name, stuff_type.type_name_ru FROM stuff_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Stuff_type = new Info_Type_name_Data();

            sql_num = 0;
            Stuff_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.stuff_type.Add(Stuff_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_type.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Stuff_class_loading();
    }

    private void Stuff_class_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Stuff_class";

        string sqlQuery = "SELECT stuff_class.id, stuff_class.class_name, stuff_class.class_name_ru FROM stuff_class";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Stuff_class = new Info_Type_name_Data();

            sql_num = 0;
            Stuff_class.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_class._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_class._name_ru = sql_data.GetString(sql_num);

            Main.db_data.stuff_class.Add(Stuff_class);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Stuff_abilities_loading();
    }

    private void Stuff_abilities_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Stuff_abilities";

        string sqlQuery = "SELECT stuff_abilities.id, stuff_abilities.type_id, stuff_abilities_type.type_name_ru, stuff_abilities.class_id, stuff_abilities_class.class_name_ru, " +
            "stuff_abilities.ability_name, stuff_abilities.cooldown, stuff_abilities.duration, stuff_abilities.game_id, stuff_abilities.image_id, stuff_abilities.description " +
            "FROM stuff_abilities , stuff_abilities_class , stuff_abilities_type " +
            "WHERE stuff_abilities.type_id = stuff_abilities_type.id AND stuff_abilities.class_id = stuff_abilities_class.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Ability_info_Data Stuff_abilities = new Ability_info_Data();

            sql_num = 0;
            Stuff_abilities.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.class_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.class_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.ability_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.cooldown = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.duration = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.game_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.image_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities.description = sql_data.GetString(sql_num);

            Main.db_data.stuff_abilities.Add(Stuff_abilities.id, Stuff_abilities);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Stuff_abilities_type_loading();
    }

    private void Stuff_abilities_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Stuff_abilities_type";

        string sqlQuery = "SELECT stuff_abilities_type.id, stuff_abilities_type.type_name, stuff_abilities_type.type_name_ru FROM stuff_abilities_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Stuff_abilities_type = new Info_Type_name_Data();

            sql_num = 0;
            Stuff_abilities_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.stuff_abilities_type.Add(Stuff_abilities_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Stuff_abilities_class_loading();
    }

    private void Stuff_abilities_class_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Stuff_abilities_class";

        string sqlQuery = "SELECT stuff_abilities_class.id, stuff_abilities_class.class_name, stuff_abilities_class.class_name_ru FROM stuff_abilities_class";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Stuff_abilities_class = new Info_Type_name_Data();

            sql_num = 0;
            Stuff_abilities_class.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities_class._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Stuff_abilities_class._name_ru = sql_data.GetString(sql_num);

            Main.db_data.stuff_abilities_class.Add(Stuff_abilities_class);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Bestiary_loading();
    }
    #endregion

    #region Загрузка данных с таблици Bestiary, Bestiary_fight_type, Bestiary_abilities, Bestiary_abilities_type, Bestiary_abilities_type, Bestiary_abilities_class
    private void Bestiary_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Bestiary";

        string sqlQuery = "SELECT bestiary.id, bestiary.map_id, bestiary.bestiary_name, bestiary.point_dungeon_id, bestiary.icon_image_id, bestiary.image_id, " +
            "bestiary.fight_type_id, bestiary_fight_type.type_name_ru, bestiary.phys_damage_min, bestiary.phys_damage_max, bestiary.damage_bonus, bestiary.magic_damage_min, " +
            "bestiary.magic_damage_max, bestiary.magic_damage_bonus, bestiary.penetration, bestiary.phys_def, bestiary.mag_def, bestiary.health, bestiary.accuracy, " +
            "bestiary.evasion, bestiary.crit_chance, bestiary.initiative, bestiary.ability_id, " +
            "bestiary.first_ability_proc_chance_d, bestiary.first_ability_proc_chance, bestiary.first_ability_cooldown, bestiary.first_ability_id, " +
            "bestiary.second_ability_proc_chance_d, bestiary.second_ability_proc_chance, bestiary.second_ability_cooldown, bestiary.second_ability_id, " +
            "bestiary.third_ability_proc_chance_d, bestiary.third_ability_proc_chance, bestiary.third_ability_cooldown, bestiary.third_ability_id " +
            "FROM bestiary , bestiary_fight_type " +
            "WHERE bestiary.fight_type_id = bestiary_fight_type.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Bestiary_Data Bestiary_data = new Bestiary_Data();

            sql_num = 0;
            Bestiary_data.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.map_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.bestiary_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.point_dungeon_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.icon_image_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.image_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.fight_type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.fight_type = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.phys_damage_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.phys_damage_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.damage_bonus = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.magic_damage_min = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.magic_damage_max = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.magic_damage_bonus = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.penetration = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.phys_def = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.mag_def = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.health = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.accuracy = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.evasion = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.crit_chance = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.initiative = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.ability_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.first_ability_proc_chance_d = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.first_ability_proc_chance = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.first_ability_cooldown = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.first_ability_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.second_ability_proc_chance_d = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.second_ability_proc_chance = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.second_ability_cooldown = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.second_ability_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.third_ability_proc_chance_d = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.third_ability_proc_chance = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.third_ability_cooldown = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_data.third_ability_id = sql_data.GetInt32(sql_num);

            Main.db_data.bestiary_data.Add(Bestiary_data.id, Bestiary_data);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Bestiary_fight_type_loading();
    }

    private void Bestiary_fight_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка bestiary_fight_type";

        string sqlQuery = "SELECT bestiary_fight_type.id, bestiary_fight_type.type_name, bestiary_fight_type.type_name_ru FROM bestiary_fight_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Bestiary_fight_type = new Info_Type_name_Data();

            sql_num = 0;
            Bestiary_fight_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_fight_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Bestiary_fight_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.bestiary_fight_type.Add(Bestiary_fight_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Bestiary_abilities_loading();
    }

    private void Bestiary_abilities_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Bestiary_abilities";

        string sqlQuery = "SELECT bestiary_abilities.id, bestiary_abilities.type_id, bestiary_abilities_type.type_name_ru, bestiary_abilities.class_id, bestiary_abilities_class.class_name_ru, " +
            "bestiary_abilities.ability_name, bestiary_abilities.cooldown, bestiary_abilities.duration, bestiary_abilities.game_id, bestiary_abilities.image_id,  bestiary_abilities.description " +
            "FROM bestiary_abilities , bestiary_abilities_class , bestiary_abilities_type " +
            "WHERE bestiary_abilities.type_id = bestiary_abilities_type.id AND bestiary_abilities.class_id = bestiary_abilities_class.id";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Ability_info_Data Bestiary_abilities = new Ability_info_Data();

            sql_num = 0;
            Bestiary_abilities.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.type_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.type_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.class_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.class_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.ability_name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.cooldown = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.duration = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.game_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.image_id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities.description = sql_data.GetString(sql_num);

            Main.db_data.bestiary_abilities.Add(Bestiary_abilities.id, Bestiary_abilities);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Bestiary_abilities_type_loading();
    }

    private void Bestiary_abilities_type_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Bestiary_abilities_type";

        string sqlQuery = "SELECT bestiary_abilities_type.id, bestiary_abilities_type.type_name, bestiary_abilities_type.type_name_ru FROM bestiary_abilities_type";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Bestiary_abilities_type = new Info_Type_name_Data();

            sql_num = 0;
            Bestiary_abilities_type.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities_type._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities_type._name_ru = sql_data.GetString(sql_num);

            Main.db_data.bestiary_abilities_type.Add(Bestiary_abilities_type);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Bestiary_abilities_class_loading();
    }

    private void Bestiary_abilities_class_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "Загрузка Bestiary_abilities_class";

        string sqlQuery = "SELECT bestiary_abilities_class.id, bestiary_abilities_class.class_name, bestiary_abilities_class.class_name_ru FROM bestiary_abilities_class";
        sql_data = Data_Base.SQL_Query(sqlQuery);
        while (sql_data.Read())
        {
            Info_Type_name_Data Bestiary_abilities_class = new Info_Type_name_Data();

            sql_num = 0;
            Bestiary_abilities_class.id = sql_data.GetInt32(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities_class._name = sql_data.GetString(sql_num);
            sql_num = sql_num + 1;
            Bestiary_abilities_class._name_ru = sql_data.GetString(sql_num);

            Main.db_data.bestiary_abilities_class.Add(Bestiary_abilities_class);
        }
        sql_data.Close();
        sql_data = null;
        /*
       //Debug.Log("----------------------------------------------------------------------------------------");
       //Debug.Log(Main.Stuff_class.Count);
       //Debug.Log("----------------------------------------------------------------------------------------");
        */
        Creat_default_Relief_data();
    }
    #endregion



    #region Создание default Relief обьектов
    public void Creat_default_Relief_data()
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

        End_loading();
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
    }
    #endregion



    private void End_loading()
    {
        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);

        loading_text.text = "End Загрузки";

        Data_Base.Connection_close();

        mask_bar_size_step_num = mask_bar_size_step_num + 1;
        mask_progress_bar.GetComponent<RectTransform>().sizeDelta = new Vector2((mask_bar_size_step_num * mask_bar_size_step), mask_progress_bar.GetComponent<RectTransform>().sizeDelta.y);
        
        switch (Main.action_name)
        {
            case "new_map":
               //Debug.Log("Открыть окно '\"'New_map'\"'");
                SceneManager.LoadScene("4_New_map");
                break;
            case "loading_map":
               //Debug.Log("Открыть окно '\"'Loading_map'\"'");
                SceneManager.LoadScene("5_Map_Editor");
                break;
        }
    }
}