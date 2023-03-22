using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneConfiguration : ScriptableObject
{
	[Min(1)]
	public float InputSensitivity;
	public List<float> LinePositions;
}