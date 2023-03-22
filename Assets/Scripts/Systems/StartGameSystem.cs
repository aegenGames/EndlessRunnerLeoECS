using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
	sealed class StartGameSystem : IEcsRunSystem {

		readonly EcsFilterInject<Inc<StartGame>> _startGame;
		readonly EcsFilterInject<Inc<TransformComponent, NoActiveGameState>, Exc<Timer>> _units;
		readonly EcsFilterInject<Inc<TransformComponent, Background>> _background;
		readonly EcsFilterInject<Inc<Enemy>> _enemies;

		readonly EcsPoolInject<ActiveGameState> _activeGameComponent;

		readonly EcsCustomInject<MoveSettings> _moveSettings;

		public void Run (IEcsSystems systems)
		{
			if (_startGame.Value.GetEntitiesCount() == 0)
				return;

			foreach (var entity in _units.Value)
			{
				_units.Pools.Inc1.Get(entity).Transform.gameObject.SetActive(true);
				_units.Pools.Inc2.Del(entity);
				_activeGameComponent.Value.Add(entity);
			}

			foreach(var entity in _enemies.Value)
			{
				_enemies.Pools.Inc1.Get(entity).Rig.velocity = Vector2.down * _moveSettings.Value.EnemySpeed;
			}

			foreach(var entity in _background.Value)
			{
				Transform transform = _background.Pools.Inc1.Get(entity).Transform;
				float positionY = transform.GetComponent<RectTransform>().sizeDelta.y - Screen.height;
				transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
			}

			foreach (var entity in _startGame.Value)
			{
				_startGame.Pools.Inc1.Del(entity);
			}
		}
	}
}