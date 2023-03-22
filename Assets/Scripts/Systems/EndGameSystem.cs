using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
	sealed class EndGameSystem : IEcsRunSystem {

		readonly EcsFilterInject<Inc<EndGame>, Exc<TextDisplay>> _endGame;
		readonly EcsFilterInject<Inc<TransformComponent>, Exc<Background, Timer>> _unit;
		readonly EcsFilterInject<Inc<TransformComponent, ActiveState>> _activeEnemies;
		readonly EcsFilterInject<Inc<ActiveGameState>> _activeGame;

		readonly EcsPoolInject<NoActiveState> _noActiveEmenyComponent;
		readonly EcsPoolInject<NoActiveGameState> _noActiveGameComponent;

		public void Run (IEcsSystems systems) {

			if (_endGame.Value.GetEntitiesCount() == 0)
				return;

			foreach(var entity in _unit.Value)
			{
				_unit.Pools.Inc1.Get(entity).Transform.gameObject.SetActive(false);
			}

			foreach (var entity in _activeEnemies.Value)
			{
				_activeEnemies.Pools.Inc1.Get(entity).Transform.position = Vector3.down * 100;
				_activeEnemies.Pools.Inc2.Del(entity);
				_noActiveEmenyComponent.Value.Add(entity);
			}

			foreach (var entity in _activeGame.Value)
			{
				_noActiveGameComponent.Value.Add(entity);
				_activeGame.Pools.Inc1.Del(entity);
			}

			foreach (var entity in _endGame.Value)
			{
				_endGame.Pools.Inc1.Del(entity);
			}
		}
	}
}