using UnityEngine;
using UnityEngine.UI;

public class custom_object_button_script : MonoBehaviour
{
    [Header("   основной обьект")]
    public GameObject main_object;
    public LayoutElement main_layoutElement;

    [Space]
    public GameObject default_frame;
    public GameObject default_frame_default;
    public GameObject default_frame_selected;

    [Space]
    public GameObject selected_frame;

    [Header("   изображение")]
    public GameObject object_image_gameObject;
    public Image object_image;
    public RectTransform object_image_rectTransform;

    [Space]
    public GameObject object_image_frame;
    public RectTransform object_image_frame_rectTransform;

    [Space]
    public GameObject main_button;
    public Button _button;

    [Space]
    [Header("   фон")]
    public Image object_background;
    public Image image_background;
    [Space]
    [Header("   цвет")]
    public Color bg_image;
    public Color bg_num;


    [Space]
    [Header("   данные")]
    public string editor_id;
    public string editor_type;

    public string editor_name;

    [Space]
    public int id;

    [Space]
    public int image_id;

    [Space]
    public string name_text;

    [Space]
    public float zoom;
    public float size_width;
    public float size_height;

    // Use this for initialization
    void Start () {
		
	}

    public void Show_Pointer()
    {
        default_frame_selected.SetActive(true);
        default_frame_default.SetActive(false);
    }

    public void Hide_Pointer()
    {
        default_frame_default.SetActive(true);
        default_frame_selected.SetActive(false);
    }

    public void Creat_object(string new_editor_type, string new_editor_name, int new_id, int new_image_id)
    {
        id = new_id;

        editor_name = new_editor_name;
        editor_id = new_editor_name;
        editor_type = new_editor_type;

        image_id = new_image_id;

        object_background.color = bg_image;
        image_background.color = bg_image;

        Display_image(new_image_id);
    }

    private void Display_image(int new_image_id)
    {
        if (new_image_id != 0)
        {
            Image_Data image_info = Main.db_data.image_data[new_image_id];

            object_image.sprite = Main.Image_list[image_info.image_name];

            if ((object_image_frame_rectTransform.sizeDelta.x < image_info._width) || (object_image_frame_rectTransform.sizeDelta.y < image_info._height))
            {
                if (image_info._width <= image_info._height)
                {
                    size_width = image_info._height;
                    size_height = object_image_frame_rectTransform.sizeDelta.y;
                }
                else
                {
                    size_width = image_info._width;
                    size_height = object_image_frame_rectTransform.sizeDelta.x;
                }

                zoom = (size_width - size_height) / size_width;


                size_width = image_info._width - image_info._width * zoom;
                size_height = image_info._height - image_info._height * zoom;

                object_image_rectTransform.sizeDelta = new Vector2(size_width, size_height);
            }
            else
            {
                object_image_rectTransform.sizeDelta = new Vector2(image_info._width, image_info._height);
            }
        }
        else
        {
            object_image_gameObject.SetActive(false);
        }
    }



    public void Creat_level_object(string new_editor_type, string new_editor_name, int new_id, int new_image_id)
    {
        id = new_id;

        editor_name = new_editor_name;
        editor_id = new_editor_name;
        editor_type = new_editor_type;

        image_id = new_image_id;

        object_background.color = bg_num;
        image_background.color = bg_num;

        Display_level_image(new_image_id);
    }

    private void Display_level_image(int new_image_id)
    {
        if (new_image_id != 0)
        {
            object_image.sprite = Main.level_image_list[new_image_id];

            if ((object_image_frame_rectTransform.sizeDelta.x < 100) || (object_image_frame_rectTransform.sizeDelta.y < 70))
            {

                    size_width = 100;
                    size_height = object_image_frame_rectTransform.sizeDelta.x;
                

                zoom = (size_width - size_height) / size_width;


                size_width = 100 - 100 * zoom;
                size_height = 70 - 70 * zoom;

                object_image_rectTransform.sizeDelta = new Vector2(size_width, size_height);
            }
            else
            {
                object_image_rectTransform.sizeDelta = new Vector2(100, 70);
            }
        }
        else
        {
            object_image_gameObject.SetActive(false);
        }
    }



    public void Show_selected_frame()
    {
        selected_frame.SetActive(true);
        default_frame.SetActive(false);
    }

    public void Hide_selected_frame()
    {
        default_frame.SetActive(true);
        selected_frame.SetActive(false);
    }
}
