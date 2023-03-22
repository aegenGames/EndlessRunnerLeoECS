using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using LeoEcsPhysics;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client {
	sealed class EcsStartup : MonoBehaviour
	{
		[SerializeField]
		EcsUguiEmitter _uguiEmitter;
		[SerializeField]
		MoveSettings _moveSettings;
		[SerializeField]
		SceneConfiguration _inputConfig;
		[SerializeField]
		SpawnSettings _spawnSettings;
		EcsWorld _world;
		IEcsSystems _systems;

		void Start () {
			_world = new EcsWorld ();
			_systems = new EcsSystems (_world);
			EcsPhysicsEvents.ecsWorld = _world;
			_systems
				.Add(new GameObjectsInitSystem())
				.Add(new BackgroundInitSystem())
				.Add(new EnemyInitSystem())
				.Add(new CheckObjectOnScreen())
				.Add(new SpawnSystem())
				.Add(new SwipeInputSystem())
				.Add(new EnemyTransitionII())
				.Add(new TransistionSystem())
				.Add(new MoveSystem())
				.Add(new CollisionHandlerSystem())
				.Add(new BreakRoundSystem())
				.Add(new EndGameSystem())
				.Add(new StartGameSystem())
				.AddWorld (new EcsWorld (), "events")
				// register your systems here, for example:
				// .Add (new TestSystem1 ())
				// .Add (new TestSystem2 ())

				// register additional worlds here, for example:
				// .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
				// add debug systems for custom worlds here, for example:
				// .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
				.Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
				//.Inject()
				.Inject(_moveSettings, _inputConfig, _spawnSettings)
				.InjectUgui(_uguiEmitter)
				.ConvertScene()
				.Init ();
		}

		void Update () {
			// process systems here.
			_systems?.Run ();
		}

		private void FixedUpdate()
		{
			_systems?.Run();
		}

		void OnDestroy () {
			if (_systems != null) {
				// list of custom worlds will be cleared
				// during IEcsSystems.Destroy(). so, you
				// need to save it here if you need.
				_systems.Destroy ();
				_systems.GetWorld().Destroy();
				_systems = null;
			}
			
			// cleanup custom worlds here.
			
			// cleanup default world.
			if (_world != null) {
				_world.Destroy ();
				_world = null;
			}
		}
	}
}