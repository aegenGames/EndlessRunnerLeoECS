using UnityEngine;

[CreateAssetMenu]
public class SpawnSettings : ScriptableObject
{
	public float PauseBetweenSpawn;
	public int MaxEnemiesOnScene;
	public int MaxEnemiesOnLine;
	public float PosY;
}