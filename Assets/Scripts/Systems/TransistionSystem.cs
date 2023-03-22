using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using DG.Tweening;

namespace Client {
	sealed class TransistionSystem : IEcsRunSystem {

		readonly EcsFilterInject<Inc<Unit, TransitionDirection, TransformComponent>, Exc<TransitionState>> _notMoving = default;
		readonly EcsFilterInject<Inc<TransitionState>> _isMoving = default;

		readonly EcsPoolInject<TransitionState> _isMovingComponent = default;

		readonly EcsCustomInject<MoveSettings> _moveConfig = default;
		readonly EcsCustomInject<SceneConfiguration> _sceneConfig = default;

		public void Run(IEcsSystems systems)
		{
			foreach (var entity in _isMoving.Value)
			{
				if(_isMoving.Pools.Inc1.Get(entity).DeltaTime > _moveConfig.Value.TimeTransiton)
				{
					_isMoving.Pools.Inc1.Del(entity);
				}
				else
				{
					_isMoving.Pools.Inc1.Get(entity).DeltaTime += Time.deltaTime;
				}
			}

			foreach (var entity in _notMoving.Value)
			{
				ref var unit = ref _notMoving.Pools.Inc1.Get(entity);
				ref var transition = ref _notMoving.Pools.Inc2.Get(entity);
				ref var movable = ref _notMoving.Pools.Inc3.Get(entity);
				Vector3 rotate = Vector3.back * transition.value * _moveConfig.Value.AngleTransition;
				int index = unit.LinePosition + transition.value;

				_notMoving.Pools.Inc2.Del(entity);
				if (index == _sceneConfig.Value.LinePositions.Count || index < 0 || index == unit.LinePosition)
					continue;

				float moveX = _sceneConfig.Value.LinePositions[index];
				unit.LinePosition = index;
				movable.Transform.DOMoveX(moveX, _moveConfig.Value.TimeTransiton);
				movable.Transform.DORotate(rotate, _moveConfig.Value.TimeTransiton).SetInverted();
				_isMovingComponent.Value.Add(entity);
			}
		}
	}
}