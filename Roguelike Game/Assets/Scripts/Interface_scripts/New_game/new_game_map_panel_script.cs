using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class new_game_map_panel_script : MonoBehaviour
{
    [Header("   основные элементы")]
    public GameObject main_object;
    public Transform main_transform;

    [Space]
    [Header("   название")]
    public Text name_text;

    [Space]
    [Header("   кнопка")]
    public Button button_delete;

    [Space]
    [Header("   данные")]
    public int id = 0;
    public string map_type = "";

    public string path;

    // Use this for initialization
    void Start () {
		
	}

    public void Delete_map()
    {
        Destroy(main_object);
    }
}
