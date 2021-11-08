using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
 
    public enum Shape
    {
        CUBE,
        SPHERE
    }

    class Actor
    {
        private string _name;
        private bool _started;
        private Vector3 _forward = new Vector3(0,0,1);
        private Collider _collider;
        private Matrix4 _globalTransform = Matrix4.Identity;
        private Matrix4 _localTransform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.Identity;
        private Matrix4 _rotation = Matrix4.Identity;
        private Matrix4 _scale = Matrix4.Identity;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Sprite _sprite;
        private Shape _shape;
        private Color _color;

        public Color ShapeColor
        {
            get { return _color; }
        }

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public Vector3 LocalPosition
        {
            get { return new Vector3(_translation.M03, _translation.M13,_translation.M23); }
            set 
            {
                SetTranslation(value.X, value.Y, value.Z);
            }
        }

        public void UpdateTransform()
        {
            if (Parent != null)
                _globalTransform = _parent.GlobalTransform * LocalTransform;
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
            child.Parent = null;

            return childRemoved;
        }

        /// <summary>
        /// The position of this actor in the world
        /// </summary>
        public Vector3 WorldPosition
        {
            //Return gloabl transforms T column
            get { return new Vector3(_globalTransform.M03, _globalTransform.M13, _globalTransform.M23); }
            set
            {
                // If the actor has a parent..
                if (Parent != null)
                {
                    //..convert the world coordiantes into local coordiates and transalte the actor
                    float xOffSet = (value.X - Parent.WorldPosition.X) / new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    float yOffSet = (value.Y - Parent.WorldPosition.Y) / new Vector3(GlobalTransform.M01, GlobalTransform.M11, GlobalTransform.M21).Magnitude;
                    float zOffSet = (value.Z - Parent.WorldPosition.Z) / new Vector3(GlobalTransform.M02, GlobalTransform.M12, GlobalTransform.M22).Magnitude;
                    SetTranslation(xOffSet, yOffSet, zOffSet);

                }
                //If this actor doesnt have a parent ...
                else
                    //...set local position to be the given value
                    LocalPosition = value;
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


        public Vector3 Size
        {
            get 
            {
                float xScale = new Vector3(_scale.M00, _scale.M10, _scale.M20).Magnitude;
                float yScale = new Vector3(_scale.M01, _scale.M11, _scale.M21).Magnitude;
                float zScale = new Vector3(_scale.M02, _scale.M12, _scale.M22).Magnitude;

                return new Vector3(xScale, yScale, zScale);
            }
            set { SetScale(value.X, value.Y, value.Z); }
        }

        

        public Actor( float x, float y, float z, string name = "Actor", Shape shape = Shape.CUBE ) :
            this( new Vector3 { X = x, Y = y, Z = z }, name, shape)
        { }
        
        
        
        


        public Actor( Vector3 position, string name = "Actor" , Shape shape = Shape.CUBE)

        {
            LocalPosition = position;
            _name = name;
            _shape = shape;

           
        }

        public Vector3 Forward
        {
            get { return new Vector3(_rotation.M02, _rotation.M12, _rotation.M22); }

            set
            {
                Vector3 point = value.Normalized + WorldPosition;
                LookAt(point);

            }

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
            //Rotate(2 * deltaTime);

            _localTransform = _translation * _rotation * _scale;
            Console.WriteLine(_name + ": " + LocalPosition.X + ", " + LocalPosition.Y);
            UpdateTransform();

        }

        public virtual void Draw()
        {
            System.Numerics.Vector3 startPos = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z);
            System.Numerics.Vector3 endPos = new System.Numerics.Vector3(WorldPosition.X + Forward.X * 50, WorldPosition.Y + Forward.Y * 50, WorldPosition.Z + Forward.Z * 50);


            switch (_shape) 
            {
                case Shape.CUBE:
                    float sizeX = new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    float sizeY = new Vector3(GlobalTransform.M01, GlobalTransform.M11, GlobalTransform.M21).Magnitude;
                    float sizeZ = new Vector3(GlobalTransform.M02, GlobalTransform.M12, GlobalTransform.M22).Magnitude;
                    Raylib.DrawCube(startPos, sizeX, sizeY, sizeZ, ShapeColor);
                    break;
                case Shape.SPHERE:
                    sizeX = new Vector3(GlobalTransform.M00, GlobalTransform.M10, GlobalTransform.M20).Magnitude;
                    Raylib.DrawSphere(startPos, sizeX, ShapeColor);
                    break;

                

            }
            
                Raylib.DrawLine3D(startPos, endPos, Color.RED);

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
        public void SetTranslation(float translationX, float translationY, float translationZ)
        {
            _translation = Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Changes the position of the actor by the given values
        /// </summary>
        /// <param name="translationX">the amount to move on x</param>
        /// <param name="translationY">the amount to move on y</param>
        public void Translate(float translationX, float translationY, float translationZ)
        {
            _translation *= Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Set the rotation of the actor
        /// </summary>
        /// <param name="radians">the angle of the new rotation in radians</param>
        public void SetRotation(float radiansX, float radiansY, float RadiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(RadiansZ);
            _rotation = rotationX * rotationY * rotationZ; 
           
        }

        /// <summary>
        /// Adds a rotation to the current transformation rotations
        /// </summary>
        /// <param name="radians">the angle in radians to tunr</param>
        public void Rotate(float radiansX, float radiansY, float RadiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(RadiansZ);
            _rotation *= rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// Sets the scale of the actor
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the x axis</param>
        public void SetScale(float x, float y, float z)
        {
            
            _scale = Matrix4.CreateScale(x, y, z);
            
        }

        /// <summary>
        /// Scales the actor by the given amount
        /// </summary>
        /// <param name="x">The value to scale on the x axis</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y, float z)
        {
            _scale *= Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The posittion the actor should be looking towards</param>
        public void LookAt(Vector3 position)
        {
            //Get the direction for the actor to look in
            Vector3 direction = (position - WorldPosition).Normalized;

            //If the direction has a length of zero..
            if (direction.Magnitude == 0)
                //...set it to be the deault forward
                direction = new Vector3(0, 0, 1);

            //Create a vector that points directly upwards
            Vector3 alignAxis = new Vector3(0, 1, 0);

            //Creates two new vectors that will be the new x and why axis
            Vector3 newYAxis = new Vector3(0, 1, 0);
            Vector3 newXAxis = new Vector3(1, 0, 0);

            //If the direction vector is parallel to the aling axis vector...
            if (Math.Abs(direction.Y) > 0 && direction.X == 0 && direction.Z == 0)
            {
                //...set the alignAxis vecto to point to the right
                alignAxis = new Vector3(1, 0, 0);

                //Get he cross product of the directionand the right to find the new y axis
                newYAxis = Vector3.CrossProduct(direction, alignAxis);
                //Normalize the new y axis to prevent the matrix form being scaled
                newYAxis.Normalize();

                //Get the cross product of the new y axis and fid the direcon of the new x axis
                newXAxis = Vector3.CrossProduct(newYAxis, direction);
                //Normalize the new x to prevent the matrix form being scaled
                newXAxis.Normalize();
            }
            //if the direction vector is not parallel
            else
            {
                //Get the cross poduct of the alignAxis and the direction vector
                newXAxis = Vector3.CrossProduct(alignAxis, direction);
                //Normalize the newXAxis to prevent out matrix from being scaled
                newXAxis.Normalize();
                //Get the cross poduct of the alignAxis and the direction vector
                newYAxis = Vector3.CrossProduct(direction, newXAxis);
                //Normalize the newYAxis to prevent out matrix from being scaled
                newYAxis.Normalize();
            }

            //Create a new matrix with new Axis
            _rotation = new Matrix4(newXAxis.X, newYAxis.X, direction.X, 0,
                                    newXAxis.Y, newYAxis.Y, direction.Y, 0,
                                    newXAxis.Z, newYAxis.Z, direction.Z, 0,
                                    0, 0, 0, 1);
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

        //Can modify the color value of red, green, blue and transparency
        public void SetColor(Vector4 colorValue)
        {
            _color = new Color((int)colorValue.X, (int)colorValue.Y, (int)colorValue.Z, (int)colorValue.W);
        }
    }
}
