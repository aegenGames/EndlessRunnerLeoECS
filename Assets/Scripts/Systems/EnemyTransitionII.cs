using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
	sealed class EnemyTransitionII : IEcsRunSystem {

		readonly EcsFilterInject<Inc<BotTransition>> _bot;

		readonly EcsPoolInject<TransitionDirection> _transitionComponent;

		readonly EcsCustomInject<MoveSettings> _settings;

		float time = 0;

		public void Run (IEcsSystems systems) {

			time += Time.deltaTime;

			if (time < _settings.Value.EnemyTransitiCD)
				return;

			foreach(var entity in _bot.Value)
			{
				if (Random.Range(1, 101) < _settings.Value.EnemyTransitiChange)
					continue;

				ref TransitionDirection transition = ref _transitionComponent.Value.Add(entity);
				transition.value = Random.Range(-1, 2);
			}

			time = 0;
		}
	}
}