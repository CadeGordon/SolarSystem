using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Enemy : Actor
    {
        private Vector3 _velocity;
        private float _speed;
        public Actor _target;
        private float _viewDistance;
        private float _lookAngle;
        

        

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

        public Enemy( float x, float y, float z, float viewDistance, float speed, Actor actor, string name = "Actor", Shape shape = Shape.CUBE)
            : base(x, y, z, name, shape)
        {
            _target = actor;
            _speed = speed;
            _viewDistance = viewDistance;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {

            float xDirection = _target.LocalPosition.X - LocalPosition.X;
            float yDirection = _target.LocalPosition.Y - LocalPosition.Y;
            float zDirection = _target.LocalPosition.Z - LocalPosition.Z;


            //Create a vector that stores the move input
            Vector3 moveDirection = new Vector3(xDirection, yDirection, zDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if(GetTargetInSight())
                LocalPosition += Velocity;

            base.Update(deltaTime, currentScene);

        }

        public bool GetTargetInSight()
        {
            Vector3 directionOfTarget = (_target.LocalPosition - LocalPosition).Normalized;

            float distance = Vector3.Distance(_target.LocalPosition, LocalPosition);


            float dotProduct = Vector3.DotProduct(directionOfTarget, Forward);

            return Vector3.DotProduct(directionOfTarget, Forward) > 0.5 && distance < _viewDistance;
        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            currentScene.RemoveActor(actor);
            currentScene.RemoveActor(this);
        }

        public override void Draw()
        {
            //base.Draw();
            //Collider.Draw();
        }
    }
}
