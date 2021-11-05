﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSeneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();
        private Camera3D _camera = new Camera3D();
        


        /// <summary>
        /// Called to beging the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;

            //loop until the application is told to close
            while (!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Get How much time has passed since the application started
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time recorded to the current time
                deltaTime = currentTime - lastTime;

                //Update the application
                Update(deltaTime);
                //Draw all items
                Draw();

                //Set the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Call end for entire application
            End();
        }

        private void InitializeCamera()
        {
            //Camera position
            _camera.position = new System.Numerics.Vector3(0, 10, 10);
            // Point the camera is focus on
            _camera.target = new System.Numerics.Vector3(0, 0, 0); 
            // Point the camera is focus on
            _camera.up = new System.Numerics.Vector3(0, 1, 0); 
            // Camera field of view
            _camera.fovy = 45;
            //Camera mode type
            _camera.projection = CameraProjection.CAMERA_PERSPECTIVE; 
        }


        /// <summary>
        /// Calls when the application starts
        /// </summary>
        private void Start()
        {


            _stopwatch.Start();

            //Create a window using raylib
            Raylib.InitWindow(800, 450, "Math For Games");
            Raylib.SetTargetFPS(0);

            InitializeCamera();

            

            Scene scene = new Scene();

            Player player = new Player(0, 0, 0, 100, "player", Shape.SPHERE);
            player.SetScale(1, 1, 1);

            CircleCollider playercirclecollider = new CircleCollider(25, player);
            AABBCollider playerboxcollider = new AABBCollider(50, 50, player);
            player.Collider = playercirclecollider;

            scene.AddActor(player);

            //Enemy enemy = new Enemy(300, 300, 100, 50, player, "Enemy", "Images/enemy.png");
            //enemy.SetScale(50, 50);
            //CircleCollider enemyCircleCollider = new CircleCollider(10, enemy);
            //AABBCollider enemymyBoxCollider;
            //enemy.LookAt(new Vector2(4, 1));
            //enemy.Forward = new Vector2(4, 1);





            //UIText text = new UIText(10, 10, "TestBox", Color.LIME, 70, 70, 15, "This is the test text \n it is not to be taken seriously");

            //scene.AddActor(text);
            
            //scene.AddActor(enemy);
            



            _currentSeneIndex = AddScene(scene);

           

            Console.CursorVisible = false;
        }

        /// <summary>
        ///Called everytime the game loops 
        /// </summary>
        private void Update(float deltaTime)
        {
            _scenes[_currentSeneIndex].Update(deltaTime, _scenes[_currentSeneIndex]);

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private  void Draw()
        {
            
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera);

            Raylib.ClearBackground(Color.RAYWHITE);
            Raylib.DrawGrid(50, 1);

            //Adds all actor icons to buffer
            _scenes[_currentSeneIndex].Draw();

            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSeneIndex].End();
            Raylib.CloseWindow();
        }

        /// <summary>
        /// Adds a scene to the engines scene array
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index that new scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Create a new temp array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //copy all values from the old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i];
            }

            //set the last index to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //retunr the last index
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Gets the next key in the input stream
        /// </summary>
        /// <returns>The key that was pressed</returns>
        public static ConsoleKey GetNextKey()
        {   //If there is no key being pressed...
            if (!Console.KeyAvailable)
                //...return
                return 0;
            
            //Return the current key being pressed
            return Console.ReadKey(true).Key;
        }


        /// <summary>
        /// Ends the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true; 
        }
    }
}
