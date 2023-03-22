using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LeoEcsPhysics;

namespace Client
{
	sealed class CollisionHandlerSystem : IEcsRunSystem {

		readonly EcsFilterInject<Inc<OnTriggerEnter2DEvent>> _enterEvent = default;

		readonly EcsPoolInject<EndGame> _endGameComponent = default;

		public void Run (IEcsSystems systems) {

			foreach (var entity in _enterEvent.Value)
			{
				_endGameComponent.Value.Add(entity);
				_enterEvent.Pools.Inc1.Del(entity);
			}
		}
	}
}