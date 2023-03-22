using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
	sealed class GameObjectsInitSystem : IEcsInitSystem {

		readonly EcsFilterInject<Inc<TransformComponent>, Exc<Timer>> _gameObjects = default;
		readonly EcsPoolInject<ActiveGameState> _gameState = default;

		public void Init (IEcsSystems systems) {

			foreach(var entity in _gameObjects.Value)
			{
				_gameState.Value.Add(entity);
			}
		}
	}
}