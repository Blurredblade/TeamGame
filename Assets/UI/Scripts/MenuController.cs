using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {


	public void QuitGame(){
		Application.Quit();
	}

	public void LoadGame(){
		SceneManager.LoadScene("Main");
	}

	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

}
