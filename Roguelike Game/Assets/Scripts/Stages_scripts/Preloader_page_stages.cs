using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader_page_stages : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Create_windows();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Create_windows()
    {
        SceneManager.LoadScene("1_Main");
    }
}
