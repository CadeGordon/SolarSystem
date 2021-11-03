using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    
    class Actor
    {
        private string _name;
        private bool _started;
        private Vector2 _forward = new Vector2(1,0);
        private Collider _collider;
        private Matrix4 _globalTransform = Matrix4.Identity;
        private Matrix4 _localTransform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.Identity;
        private Matrix4 _rotation = Matrix4.Identity;
        private Matrix4 _scale = Matrix4.Identity;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Sprite _sprite;

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public Vector2 LocalPosition
        {
            get { return new Vector2(_translation.M02, _translation.M12); }
            set 
            {
                SetTranslation(value.X, value.Y);
            }
        }

        public void UpdateTransform()
        {
            if (Parent != null)
                GlobalTransform = _parent.GlobalTransform * LocalTransform;
            else
                GlobalTransform = LocalTransform;

            
        }

        public void AddChild(Actor child)
        {
            
            Actor[] tempArray = new Actor[_children.Length + 1];

           
            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            
            tempArray[_children.Length] = child;

            child.Parent = this;

            _children = tempArray;
        }

        public bool RemoveChild(Actor child)
        {
            //Create a variable to store if the removal was successful
            bool childRemoved = false;

            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_children.Length - 1];

            //Copy all values except the actor we don't want into the new array
            int j = 0;
            for (int i = 0; i < tempArray.Length; i++)
            {
                //If the actor that the loop is on is not the one to remove...
                if (_children[i] != child)
                {
                    //...add the actor back into the new array
                    tempArray[j] = _children[i];
                    j++;
                }
                //Otherwise if this actor is the one to remove...
                else
                    //...set actorRemoved to true
                    childRemoved = true;
            }

            //If the actor removal was successful...
            if (childRemoved)
                //...set the old array to be the new array
                _children = tempArray;

            return childRemoved;
        }

        public Vector2 WorldPosition
        {
            get { return new Vector2(_globalTransform.M02, _globalTransform.M12); }
            set
            {
                SetTranslation(value.Y, value.X);
            }
        }

        public Matrix4 GlobalTransform
        {
            get { return _globalTransform; }
            set { _globalTransform = value; }
        }

        public Matrix4 LocalTransform
        {
            get { return _localTransform; }
            set { _localTransform = value; }
        }

        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public Actor[] Children
        {
            get { return _children; }
        }


        public Vector2 Size
        {
            get { return new Vector2(_scale.M00, _scale.M11); }
            set { SetScale(value.X, value.Y); }
        }

        

        public Actor( float x, float y, string name = "Actor", string path = "" ) :
            this( new Vector2 { X = x, Y = y }, name, path)
        { }
        
        
        
        


        public Actor( Vector2 position, string name = "Actor" , string path = "")

        {
            LocalPosition = position;
            _name = name;

            if (path != "")
                _sprite = new Sprite(path);
        }

        public Vector2 Forward
        {
            get { return new Vector2(_rotation.M00, _rotation.M10); }
            set 
            {
                Vector2 point = value.Normalized + LocalPosition;
                LookAt(point);
                    
            }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime, Scene currentScene)
        {
            Rotate(2 * deltaTime);

            _localTransform = _translation * _rotation * _scale;
            Console.WriteLine(_name + ": " + LocalPosition.X + ", " + LocalPosition.Y);
            UpdateTransform();

        }

        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(GlobalTransform);
        }

        public void End()
        {

        }

        public virtual void OnCollision(Actor actor, Scene currentScene)
        {

        }

        /// <summary>
        /// Check if this actor collided with another actor
        /// </summary>
        /// <param name="other">Thea ctor to check for a collision</param>
        /// <returns>True if the distance bewteen  the actors is less than the radii of the two combined</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            //Retunr false if either actor doesnt have a collider attached
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }

        /// <summary>
        /// Sets the position of the actor
        /// </summary>
        /// <param name="translationX">the new x position</param>
        /// <param name="translationY">The new y position</param>
        public void SetTranslation(float translationX, float translationY)
        {
            _translation = Matrix4.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Changes the position of the actor by the given values
        /// </summary>
        /// <param name="translationX">the amount to move on x</param>
        /// <param name="translationY">the amount to move on y</param>
        public void Translate(float translationX, float translationY)
        {
            _translation *= Matrix4.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">the angle of the new rotation in radians</param>
        public void SetRotation(float radians)
        {
            _rotation = Matrix4.CreateRotation(radians);
        }

        /// <summary>
        /// Adds a rotation to the current transformation rotations
        /// </summary>
        /// <param name="radians">the angle in radians to tunr</param>
        public void Rotate(float radians)
        {
            _rotation *= Matrix4.CreateRotation(radians);
        }

        /// <summary>
        /// Sets the scale of the actor
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the x axis</param>
        public void SetScale(float x, float y)
        {
            _scale = Matrix4.CreateScale(x, y);
            
        }

        /// <summary>
        /// Scales the actor by the given amount
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y)
        {
            _scale *= Matrix4.CreateScale(x, y);
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The posittion the actor should be looking towards</param>
        public void LookAt(Vector2 position)
        {
            //FInd the direction that the actor should look in
            Vector2 direction = (position - LocalPosition).Normalized;

            //Use the dot product to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProdcut(direction, Forward);

            if (dotProd > 1)
                dotProd = 1;

            float angle = (float)Math.Acos(dotProd);

            //Find a perpendicular vector to the direction 
            Vector2 perpDirection = new Vector2(direction.Y, -direction.X);

            //Find the dot prodcut of the perpendicluar vectoar and the current foward
            float perpDot = Vector2.DotProdcut(perpDirection, Forward);

            //If the result isnt 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);

        }
    }
}
