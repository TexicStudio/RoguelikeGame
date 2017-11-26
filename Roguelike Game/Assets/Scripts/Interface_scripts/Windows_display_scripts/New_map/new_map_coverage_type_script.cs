using UnityEngine;
using UnityEngine.UI;

public class new_map_coverage_type_script : MonoBehaviour
{
    [Header("   основной обьект")]
    public GameObject main_object;
    public RectTransform main_rectTransform;
    public LayoutElement main_layoutElement;

    [Header("   дочерний обьект")]
    public GameObject main_panel;
    public RectTransform main_panel_rectTransform;
    public GridLayoutGroup main_panel_gridLayout;
    public Text main_panel_title;

    public int object_num;

    public float size;

    // Use this for initialization
    void Start () {		
	}

    public void Creat_object(string object_name)
    {
        main_panel_title.text = "" + object_name;
    }

    public void Windows_resize(int object_count)
    {
        if ((object_count % object_num) != 0)
        {
            size = object_count / object_num + 1;
        }
        else
        {
            size = object_count / object_num;
        }

        //Debug.Log("размер панельки списка сохраненных данных " + size);

        size = size * main_panel_gridLayout.cellSize.y + (size - 1) * main_panel_gridLayout.spacing.y;

        //Debug.Log("размер панельки " + size);
        main_panel_rectTransform.sizeDelta = new Vector2(main_panel_rectTransform.sizeDelta.x, size);

        size = size + 22;

        main_layoutElement.minHeight = size;
        main_layoutElement.preferredHeight = main_layoutElement.minHeight;
    }
}
