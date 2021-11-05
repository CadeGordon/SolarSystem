using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Bullet : Actor

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

        public Bullet(float x, float y, float z, float velocityX, float velocityY, float velocityZ, float speed, string name = "Actor", Shape shape = Shape.SPHERE)
            : base(x, y, z, name, shape)
        {
            _speed = speed;
            _velocity.X = velocityX;
            _velocity.Y = velocityY;
            _velocity.Z = velocityZ;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {
            

            //Create a vector that stores the move input
            Vector3 moveDirection = new Vector3(_velocity.X, _velocity.Y, _velocity.Z );

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            LocalPosition += Velocity;

            base.Update(deltaTime, currentScene);

        }

        

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if (actor is Enemy)
            {
                currentScene.RemoveActor(actor);
            }
                
        }
    }
}
