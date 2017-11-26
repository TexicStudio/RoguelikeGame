using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using SFB;

[RequireComponent(typeof(Button))]
public class OpenFileMap : MonoBehaviour, IPointerDownHandler
{
    public Initial_menu_stages initial_menu;

    public string Title = "";
    public string FileName = "";
    public string Directory = "";
    public string Extension = "";
    public bool Multiselect = false;

    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(Title, Directory, Extension, Multiselect);

        if (paths.Length > 0)
        {
            if (File.Exists(paths[0]))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(paths[0], FileMode.Open);

                Map_info_Blank savedMap = (Map_info_Blank)bf.Deserialize(file);

                string map_name = "" + savedMap.id;

                Main.Map_info.Add(map_name, savedMap);

               //Debug.Log(map_name);

                file.Close();

                Main.action_name = "loading_map";

                initial_menu.Next_stage();
            }
            else
            {
                Debug.Log("Map doesn't excist or didn't loaded");
                initial_menu.no_maps_windows.SetActive(true);
            }
        }
    }
}