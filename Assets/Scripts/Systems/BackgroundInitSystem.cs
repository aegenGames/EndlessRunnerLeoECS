using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
	sealed class BackgroundInitSystem : IEcsInitSystem {

		readonly EcsFilterInject<Inc<Background, TransformComponent>> _background = default;

		readonly EcsPoolInject<Move> _moveComponent = default;

		readonly EcsCustomInject<MoveSettings> _moveConfig = default;

		public void Init (IEcsSystems systems) {
			foreach (var entity in _background.Value)
			{
				_moveComponent.Value.Add(entity);
				_moveComponent.Value.Get(entity).Direction = Vector3.down;
				_moveComponent.Value.Get(entity).Speed = _moveConfig.Value.BalloonSpeed;
			}
		}
	}
}