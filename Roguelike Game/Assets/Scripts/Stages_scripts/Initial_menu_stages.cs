using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;

public class Initial_menu_stages : MonoBehaviour
{
    public Button new_map_Button;
    public Button loading_map_Button;

    public GameObject no_maps_windows;
    public Button background_windows;
    public Button no_Button;
    public Button yes_Button;

    private Database_connection Data_Base = new Database_connection();
    private IDataReader sql_data;

    private int sql_num;

    // Use this for initialization
    void Start()
    {
        Create_scene();
    }

    void Create_scene()
    {
        new_map_Button.onClick.AddListener(() => New_map_Click());
        
        //loading_map_Button.onClick.AddListener(() => loading_map_Click());
        
        
        background_windows.onClick.AddListener(() => Background_windows_Click());
        
        no_Button.onClick.AddListener(() => Background_windows_Click());
        
        yes_Button.onClick.AddListener(() => New_map_Click());
    }

    private void Background_windows_Click()
    {
        no_maps_windows.SetActive(false);
    }

    private void loading_map_Click()
    {
        /*
        Функция подключаеться к БД, подсчитует количество записей в таблице "map". И если записей больше 0 то перехолим к выбору и загрузки карты. 
        Если 0 то предлагаем создать новую карту, либо повторить все заного.
        */
        Data_Base.Connection();

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
            Main.action_name = "loading_map";
            Next_stage();
        }
        else
        {
            no_maps_windows.SetActive(true);
        }

        Data_Base.Connection_close();
    }

    private void New_map_Click()
    {
        Main.action_name = "new_map";
        Next_stage();
    }

    public void Next_stage()
    {
        SceneManager.LoadScene("3_Loading_objects");

        /*
        switch (Main.action_name)
        {
            case "new_map":
               //Debug.Log("Открыть окно '\"'New_map'\"'");
                SceneManager.LoadScene("New_map");
                break;
            case "loading_map":
               //Debug.Log("Открыть окно '\"'Loading_map'\"'");
                break;
        }
        */
    }
}
