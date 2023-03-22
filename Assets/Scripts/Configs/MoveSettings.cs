using UnityEngine;

[CreateAssetMenu]
public class MoveSettings : ScriptableObject
{
	[Min(0)]
	public float TimeTransiton;
	public float AngleTransition;
	[Min(1)]
	public float BalloonSpeed;
	public float EnemyTransitiCD;
	public float EnemyTransitiChange;
	public float EnemySpeed;
}