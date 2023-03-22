using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
	sealed class MoveSystem : IEcsRunSystem {

		readonly EcsFilterInject<Inc<TransformComponent, Move, ActiveGameState>> _move = default;

		public void Run (IEcsSystems systems) {
			foreach(var entity in _move.Value)
			{
				ref Move move = ref _move.Pools.Inc2.Get(entity);
				Transform transforn = _move.Pools.Inc1.Get(entity).Transform;
				transforn.position += move.Direction * move.Speed * Time.deltaTime;
			}
		}
	}
}