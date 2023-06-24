using System.Collections;
using Additional;
using Gameplay.RoadSystem.EnemyCarSubsystem;
using Gameplay.RoadSystem.Item;
using Gameplay.RoadSystem.ObstacleSubsystem;
using Gameplay.VehicleSystem;
using InputSystem;
using UnityEngine;

namespace Gameplay.TutorialSystem
{
    public class Tutorial
    {
        private const string PrefabFileName = "TutorialCanvas";
        
        private readonly PromptWindow _promptWindow;
        private readonly VehicleMover _vehicleMover;
        private readonly EnemyCarSpawner _enemyCarSpawner;
        private readonly ItemSpawner _itemSpawner;
        private readonly ObstacleSpawner _obstacleSpawner;
        private readonly SwipeDetector _swipeDetector;
        private readonly ItemsCollector _itemsCollector;

        private Coroutine _coroutine;
        private int swipeCounter;
        private int itemsCounter;
        
        public Tutorial(VehicleMover vehicleMover, 
            EnemyCarSpawner enemyCarSpawner, 
            ItemSpawner itemSpawner, 
            SwipeDetector swipeDetector, 
            ItemsCollector itemsCollector, 
            ObstacleSpawner obstacleSpawner)
        {
            PromptWindow prefab = Resources.Load<PromptWindow>(PrefabFileName);
            
            _promptWindow = Object.Instantiate(prefab);
            
            _vehicleMover = vehicleMover;
            _enemyCarSpawner = enemyCarSpawner;
            _itemSpawner = itemSpawner;
            _obstacleSpawner = obstacleSpawner;
            _swipeDetector = swipeDetector;
            _itemsCollector = itemsCollector;
            
            _swipeDetector.OnSwipe += OnSwipe;
            _promptWindow.OnClick += OnPromptClick;
            _itemsCollector.OnItemsCollected += OnItemCollect;
        }

        public void StartTutorial()
        {
            _coroutine = CoroutineStarter.Start(TutorialRoutine());
        }

        private void OnItemCollect(int obj)
        {
            itemsCounter++;
        }

        private void OnSwipe(SwipeDirection obj)
        {
            swipeCounter++;
        }

        private void OnPromptClick()
        {
            _isOff = true;
        }

        private bool _isOff;
        
        private IEnumerator TutorialRoutine()
        {
            yield return new WaitForSecondsRealtime(1f);
            
            _vehicleMover.StopMovement();
            _promptWindow.SetText("To control car use left and right swipe");
            _isOff = false;
            _promptWindow.Show();
            
            yield return new WaitWhile(() => !_isOff);
            _vehicleMover.StartMovement();
            
            yield return new WaitWhile(() => swipeCounter <= 2);
            
            _vehicleMover.StopMovement();
            _promptWindow.SetText("As you move, you can collect different points and earn points");
            _isOff = false;
            _promptWindow.Show();
            
            yield return new WaitWhile(() => !_isOff);
            _vehicleMover.StartMovement();
            _itemSpawner.StartSpawn();
            
            yield return new WaitWhile(() => itemsCounter <= 5);
            _vehicleMover.StopMovement();
            _promptWindow.SetText("You will also need to avoid hitting obstacles such as cars, road signs...");
            _isOff = false;
            _promptWindow.Show();
            
            yield return new WaitWhile(() => !_isOff);
            _vehicleMover.StartMovement();
            _enemyCarSpawner.StartSpawn();
            _obstacleSpawner.StartSpawn();
        }
    }
}