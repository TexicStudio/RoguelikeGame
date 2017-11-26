using UnityEngine;
using UnityEngine.UI;

public class object_library_script : MonoBehaviour
{
    [Header("   object_library основной обьект")]
    public GameObject main_object;

    [Header("   object_library дочерний обьект")]
    public GameObject main_panel;
    public VerticalLayoutGroup main_panel_verticalLayoutGroup;
    public ContentSizeFitter main_panel_contentSizeFitter;

    private bool library_exists = false;

    // Use this for initialization
    void Start () {
		
	}

    public void Click_library_Button()
    {
        switch (library_exists)
        {
            case true:
                library_exists = false;
                Hide_library();
                break;
            case false:
                library_exists = true;
                Show_library();
                break;
        }
    }

    private void Show_library()
    {
        main_object.SetActive(true);
    }

    private void Hide_library()
    {
        main_object.SetActive(false);
    }
}
