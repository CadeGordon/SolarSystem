using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    class Player : Actor
    {
        private Vector3 _velocity;
        private float _speed;
        

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player( float x, float y, float z, float speed, string name = "Actor", Shape shape = Shape.CUBE) 
            : base(x, y, z, name, shape)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));

            int zDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
               + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //if (Convert.ToBoolean(Raylib.IsKeyPressed(KeyboardKey.KEY_UP)))
            //{
            //    Bullet bullet = new Bullet(LocalPosition.X, LocalPosition.Y, LocalPosition.Z, 0, 0, 0, 100, "Bullet", Shape.SPHERE);
            //    bullet.SetScale(50, 50, 50);
            //    currentScene.AddActor(bullet);
            //    //CircleCollider bulletCircleCollider = new CircleCollider(5, bullet);
            //    //AABBCollider bulletBoxCollider = new AABBCollider(10, 10, bullet);
            //    //bullet.Collider = bulletBoxCollider;

            //}

            //if (Convert.ToBoolean(Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN)))
            //{
            //    Bullet bullet = new Bullet(LocalPosition.X, LocalPosition.Y, LocalPosition.Z, 0, 0, 0, 100, "Bullet", Shape.CUBE);
            //    bullet.SetScale(50, 50, 50);
            //    currentScene.AddActor(bullet);
            //    //CircleCollider bulletCircleCollider = new CircleCollider(5, bullet);
            //    //AABBCollider bulletBoxCollider = new AABBCollider(10, 10, bullet);
            //    //bullet.Collider = bulletBoxCollider;

            //}

            //if (Convert.ToBoolean(Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT)))
            //{
            //    Bullet bullet = new Bullet(LocalPosition.X, LocalPosition.Y, LocalPosition.Z, 0, 0, 0, 100, "Bullet", Shape.CUBE);
            //    bullet.SetScale(50, 50, 50);
            //    currentScene.AddActor(bullet);
            //    //CircleCollider bulletCircleCollider = new CircleCollider(5, bullet);
            //    //AABBCollider bulletBoxCollider = new AABBCollider(10, 10, bullet);
            //    //bullet.Collider = bulletBoxCollider;

            //}

            //if (Convert.ToBoolean(Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT)))
            //{
            //    Bullet bullet = new Bullet(LocalPosition.X, LocalPosition.Y, LocalPosition.Z, 0, 0, 0, 100, "Bullet", Shape.CUBE);
            //    bullet.SetScale(50, 50, 50);
            //    currentScene.AddActor(bullet);
            //    //CircleCollider bulletCircleCollider = new CircleCollider(5, bullet);
            //    //AABBCollider bulletBoxCollider = new AABBCollider(10, 10, bullet);
            //    //bullet.Collider = bulletBoxCollider;

            //}








            //Create a vector that stores the move input
            Vector3 moveDirection = new Vector3(xDirection, 0, zDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (Velocity.Magnitude > 0)
               Forward = Velocity.Normalized;

            //Translate(_velocity.X, 0, _velocity.Z);

            LocalPosition += Velocity;


            base.Update(deltaTime, currentScene);
            
        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if (actor is Enemy)
                Engine.CloseApplication();
        }

        public override void Draw()
        {
            base.Draw();
            //Collider.Draw();
        }

    }
}
