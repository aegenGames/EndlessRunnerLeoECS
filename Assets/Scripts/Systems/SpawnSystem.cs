using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
	sealed class SpawnSystem : IEcsRunSystem {

		readonly EcsFilterInject<Inc<ActiveState, ActiveGameState>> _activeEnemies = default;
		readonly EcsFilterInject<Inc<NoActiveState, Enemy, Unit, ActiveGameState>> _noActiveEnemies = default;

		readonly EcsPoolInject<ActiveState> _activeComponent = default;
		readonly EcsPoolInject<BotTransition> _BotComponent = default;

		readonly EcsCustomInject<SceneConfiguration> _sceneConfig = default;
		readonly EcsCustomInject<SpawnSettings> _spawnSettings = default;

		private float _cooldawn = 0;
		
		public void Run (IEcsSystems systems) {

			if (_cooldawn > 0)
			{
				_cooldawn -= Time.deltaTime;
				return;
			}
			_cooldawn = _spawnSettings.Value.PauseBetweenSpawn;

			int freeSlots = _spawnSettings.Value.MaxEnemiesOnScene - _activeEnemies.Value.GetEntitiesCount();
			if (freeSlots == 0)
				return;

			if (freeSlots > _spawnSettings.Value.MaxEnemiesOnLine)
				freeSlots = _spawnSettings.Value.MaxEnemiesOnLine;

			int respCount = Random.Range(0, freeSlots + 1);
			if (respCount == 0)
				return;

			List<float> positionsX = _sceneConfig.Value.LinePositions.GetRange(0, _sceneConfig.Value.LinePositions.Count);
			int respIndex = respCount;
			foreach (var entity in _noActiveEnemies.Value)
			{
				if (respIndex == 0)
					break;

				_activeComponent.Value.Add(entity);
				int posIndex = Random.Range(0, positionsX.Count);

				Vector2 position = new Vector2(positionsX[posIndex], _spawnSettings.Value.PosY);
				_noActiveEnemies.Pools.Inc2.Get(entity).Rig.transform.position = position;
				_noActiveEnemies.Pools.Inc3.Get(entity).LinePosition = _sceneConfig.Value.LinePositions.IndexOf(positionsX[posIndex]);
				positionsX.RemoveAt(posIndex);

				if (respCount == 1)
					_BotComponent.Value.Add(entity);

				_noActiveEnemies.Pools.Inc1.Del(entity);
				--respIndex;
			}
		}
	}
}