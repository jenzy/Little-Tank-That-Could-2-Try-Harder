﻿using UnityEngine;
using System.Collections;

public class Tags : MonoBehaviour {
	public const string TERRAIN = "Terrain";
	public const string PROJECTILE = "Projectile";
	public const string PROJECTILE_START = "ProjectileStart";
	public const string GAME_CONTROLER = "GameController";
	public const string CAMERA_MAIN = "MainCamera";
	public const string TURRET_GROUP = "TurretGroup";
	public const string PLAYER = "Player";
	public const string DESTRUCTIBLE = "Destructible";
	public const string TREE = "Tree";

	public static GameObject findParentWithTag(string tagToFind, GameObject startingObject) {
		Transform par = startingObject.transform.parent;
		while (par != null) { 
			if (par.CompareTag(tagToFind)) {
				return par.gameObject as GameObject;
			}
			par = par.parent;
		}
		return null;
	}
}
