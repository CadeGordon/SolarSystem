using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Enemy : Actor
    {
        private Vector2 _velocity;
        private float _speed;
        public Actor _target;
        private float _viewDistance;
        private float _lookAngle;
        

        

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Enemy( float x, float y, float viewDistance, float speed, Actor actor, string name = "Actor", string path = "")
            : base(x, y, name, path)
        {
            _target = actor;
            _speed = speed;
            _viewDistance = viewDistance;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {

            float xDirection = _target.Postion.X - Postion.X;
            float yDirection = _target.Postion.Y - Postion.Y;

            //Create a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if(GetTargetInSight())
               Postion += Velocity;

            base.Update(deltaTime, currentScene);

        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_target.Postion - Postion).Normalized;

            float distance = Vector2.Distance(_target.Postion, Postion);


            float dotProduct = Vector2.DotProdcut(directionOfTarget, Forward);

            return Vector2.DotProdcut(directionOfTarget, Forward) > 0.5 && distance < _viewDistance;
        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            currentScene.RemoveActor(actor);
            currentScene.RemoveActor(this);
        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }
    }
}
