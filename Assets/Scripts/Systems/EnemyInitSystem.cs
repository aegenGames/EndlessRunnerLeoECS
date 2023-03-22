using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
	sealed class EnemyInitSystem : IEcsInitSystem {

		readonly EcsFilterInject<Inc<Enemy>> _enemies = default;

		readonly EcsCustomInject<MoveSettings> _moveSettings;

		public void Init (IEcsSystems systems) {

			foreach(var entity in _enemies.Value)
			{
				ref Enemy enemy = ref _enemies.Pools.Inc1.Get(entity);
				enemy.Rig.velocity = Vector2.down * _moveSettings.Value.EnemySpeed;
			}
		}
	}
}