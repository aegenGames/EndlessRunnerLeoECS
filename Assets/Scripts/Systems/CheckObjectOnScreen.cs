using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
	sealed class CheckObjectOnScreen : IEcsRunSystem {

		readonly EcsFilterInject<Inc<ActiveState, TransformComponent>> _activeObjects;
		readonly EcsFilterInject<Inc<NoActiveState, BotTransition>> _noActiveObjects;

		readonly EcsPoolInject<NoActiveState> _noActiveComponent;

		public void Run (IEcsSystems systems) {

			float exitPoint = -Camera.main.orthographicSize;

			foreach(var entity in _activeObjects.Value)
			{
				if (_activeObjects.Pools.Inc2.Get(entity).Transform.position.y < exitPoint)
				{
					_activeObjects.Pools.Inc1.Del(entity);
					_noActiveComponent.Value.Add(entity);
				}
			}

			foreach (var entity in _noActiveObjects.Value)
			{
				_noActiveObjects.Pools.Inc2.Del(entity);
			}
		}
	}
}