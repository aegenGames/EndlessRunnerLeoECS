using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
	sealed class BreakRoundSystem : IEcsRunSystem {

		readonly EcsFilterInject<Inc<EndGame>> _endGame;
		readonly EcsFilterInject<Inc<TextDisplay, TransformComponent>> _timer;
		readonly EcsFilterInject<Inc<BreakGameState>> _breakState;

		readonly EcsPoolInject<StartGame> _startGameComponent;
		readonly EcsPoolInject<BreakGameState> _breakStateComponent;

		private float _time = -1;

		public void Run (IEcsSystems systems) {

			if (_endGame.Value.GetEntitiesCount() > 0)
			{
				foreach (var entity in _timer.Value)
				{
					_timer.Pools.Inc2.Get(entity).Transform.gameObject.SetActive(true);
					_breakStateComponent.Value.Add(entity);
				}
				_time = 3;
			}

			if (_time <= 0)
			{
				if(_breakState.Value.GetEntitiesCount() != 0)
				{
					foreach(var entity in _breakState.Value)
					{
						_breakState.Pools.Inc1.Del(entity);
						_startGameComponent.Value.Add(entity);
					}
				}
				return;
			}
			_time -= Time.deltaTime;

			foreach (var entity in _timer.Value)
			{
				_timer.Pools.Inc1.Get(entity).TimerDilsplay.text = _time.ToString("#");
			}
		}
	}
}