using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public enum Menu { MAIN_MENU, OPTIONS, RESOLUTION, ABOUT }
	public enum Difficulty { EASY, NORMAL }

	public GUISkin guiSkin;
	public Texture2D LOGO;
	//public string[] AboutTextLines = new string[0];
	
	//private string MessageDisplayOnAbout = "About \n ";
	private Menu current;
	private float volume = 1.0f;
	private Rect MainWindowRect = new Rect((Screen.width / 2) - 100, Screen.height-220, 200, 200);

	// Difficulty
	private static int difficulty = 0;
	private string[] difficultyStrings = {"Easy", "Normal"};
	private Rect DifficultyWindowRect = new Rect(20, Screen.height-120, 200, 100);
	public static Difficulty ChosenDifficulty{
		get {
			switch(difficulty){
			case 0: return Difficulty.EASY;
			case 1: return Difficulty.NORMAL;
			default: return Difficulty.EASY;
			}
		}
	}

	// Level select
	private int level = 0;
	private string[] levelDisplayStrings = { "Grassland", "Desert" };
	private string[] levelLoadStrings = { Level.LEVEL1, Level.LEVEL1 };
	private Rect LevelWindowRect = new Rect(Screen.width-220, Screen.height-120, 200, 100);
	
	private void Start(){
		Screen.showCursor = true;
		/*for (int x = 0; x < AboutTextLines.Length;x++ ){
			MessageDisplayOnAbout += AboutTextLines[x] + " \n ";
		}
		MessageDisplayOnAbout += "Press Esc To Go Back";*/
	}
	
	private void OnGUI(){
		GUI.skin = guiSkin;

		/*
		if (LOGO != null && clicked != "about")
			GUI.DrawTexture(new Rect((Screen.width / 2) - 100, 30, 200, 200), LOGO);
		*/

		switch(current){
		case Menu.MAIN_MENU:
			MainWindowRect = GUI.Window(0, MainWindowRect, showMenu, "Main Menu");
			DifficultyWindowRect = GUI.Window(2, DifficultyWindowRect, showDifficulty, "Difficulty");
			LevelWindowRect = GUI.Window(3, LevelWindowRect, showLevel, "Select level");
			break;
		case Menu.OPTIONS:
			MainWindowRect = GUI.Window(1, MainWindowRect, showOptions, "Options");
			break;
		case Menu.RESOLUTION:
			GUILayout.BeginVertical();
			for (int x = 0; x < Screen.resolutions.Length;x++ ){
				if (GUILayout.Button(Screen.resolutions[x].width + "X" + Screen.resolutions[x].height)){
					Screen.SetResolution(Screen.resolutions[x].width,Screen.resolutions[x].height, true);
				}
			}
			GUILayout.EndVertical();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Back"))
				current = Menu.OPTIONS;
			GUILayout.EndHorizontal();
			break;
		}
		/*
		else if (clicked == "about")
		{
			GUI.Box(new Rect (0,0,Screen.width,Screen.height), MessageDisplayOnAbout);
		}*/
	}

	private void showDifficulty(int id){
		GUILayout.Space(10);
		difficulty = GUILayout.SelectionGrid(difficulty, difficultyStrings, 1);
	}

	private void showLevel(int id){
		GUILayout.Space(10);
		level = GUILayout.SelectionGrid(level, levelDisplayStrings, 1);
	}

	private void showOptions(int id){
		GUILayout.Space(20);

		if (GUILayout.Button("Resolution"))
			current = Menu.RESOLUTION;
		GUILayout.Space(20);

		GUILayout.Box("Volume");
		volume = GUILayout.HorizontalSlider(volume ,0.0f,1.0f);
		AudioListener.volume = volume;
		GUILayout.Space(20);

		if (GUILayout.Button("Back"))
			current = Menu.MAIN_MENU;
	}
	
	private void showMenu(int id){
		GUILayout.Space(10);
		if (GUILayout.Button("Play Game"))
			Application.LoadLevel(levelLoadStrings[level]);

		GUILayout.Space(30);
		if (GUILayout.Button("Options"))
			current = Menu.OPTIONS;
		if (GUILayout.Button("About"))
			current = Menu.ABOUT;

		GUILayout.Space(20);
		if (GUILayout.Button("Quit Game"))
			Application.Quit();
	}
	
	private void Update(){
		if( current == Menu.ABOUT && Input.GetKey(KeyCode.Escape))
			current =Menu.MAIN_MENU;
	}
}
