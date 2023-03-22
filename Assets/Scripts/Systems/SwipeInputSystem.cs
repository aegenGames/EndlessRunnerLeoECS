using Leopotam.EcsLite.Unity.Ugui;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.Scripting;

namespace Client
{
	sealed class SwipeInputSystem : EcsUguiCallbackSystem
	{
		readonly EcsFilterInject<Inc<Unit, Player, ActiveGameState>, Exc<TransitionState>> _player = default;

		readonly EcsPoolInject<TransitionDirection> _directionComponent = default;

		readonly EcsCustomInject<SceneConfiguration> _inputConfig = default;

		Vector2 _downPosition;

		[Preserve]
		[EcsUguiDownEvent("TouchListener")]
		void OnDownTouchListener(in EcsUguiDownEvent e)
		{
			_downPosition = e.Position;
		}

		[Preserve]
		[EcsUguiUpEvent("TouchListener")]
		void OnUpTouchListener(in EcsUguiUpEvent e)
		{
			Vector2 swipe = e.Position - _downPosition;
			if (Mathf.Abs(swipe.x) < Screen.width / _inputConfig.Value.InputSensitivity)
				return;

			foreach(var entity in _player.Value)
			{
				ref TransitionDirection direction = ref _directionComponent.Value.Add(entity);
				direction.value = (int) Mathf.Sign(swipe.x);
			}
		}
	}
}