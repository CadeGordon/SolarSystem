using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        /// <summary>
        /// Array that contains all actors in the scene
        /// </summary>
        private Actor[] _actors;
        private Actor[] _UIElements;


        public Scene()
        {
            _actors = new Actor[0];
            _UIElements = new Actor[0];
        }

        /// <summary>
        /// Calls start for all actors in the actors array
        /// </summary>
        public virtual void Start()
        {
        }

        /// <summary>
        /// Calls update for every actor in the scene. 
        /// Calls start for the actor if it hasnt already been called.
        /// </summary>
        public virtual void Update(float deltaTime, Scene currentScene)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                   _actors[i].Start();

                _actors[i].Update(deltaTime, currentScene);

                //Check for collision
                for (int j = 0; j < _actors.Length; j++)
                {
                    if (i < _actors.Length)
                    {
                        if (_actors[i].CheckForCollision(_actors[j]) && j != i)
                            _actors[i].OnCollision(_actors[j], currentScene);
                    }
                    
                }
            }
        }

        public virtual void UpdateUI(float deltaTime, Scene currentScene)
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (!_UIElements[i].Started)
                    _UIElements[i].Start();

                _UIElements[i].Update(deltaTime, currentScene);
            }
            
        }

        public virtual void DrawUI()
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                _UIElements[i].Draw();
            }
        }



        /// <summary>
        /// Calls draw for every actor in the array
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                {
                    _actors[i].Start();
                }
                    
                _actors[i].Draw();
            }
        }

        /// <summary>
        /// Call end for every actor in the array
        /// </summary>
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].End();
            }
        }

        public virtual void AddUIElement(Actor UI)
        {
            Actor[] tempArray = new Actor[_UIElements.Length + 1];

            for(int i = 0; i < _UIElements.Length; i++)
            {
                tempArray[i] = _UIElements[i];
            }

            tempArray[_UIElements.Length] = UI;

            _UIElements = tempArray;
        }

        public virtual bool RemoveUIElement(Actor UI)
        {
            bool actorRemoved = false;

            Actor[] tempArray = new Actor[_UIElements.Length - 1];

            int j = 0;
            for ( int i = 0; i < tempArray.Length; i++)
            {
                if (_UIElements[i] != UI)
                {
                    tempArray[j] = _UIElements[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                }
            }

            if (actorRemoved)
                _UIElements = tempArray;

            return actorRemoved;
        }

        /// <summary>
        /// Adds an actor to the scenes list of actors
        /// </summary>
        /// <param name="actor">The actor to add to the scene</param>
        public virtual void AddActor(Actor actor)
        {
            //Create a temp array latger than the original
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copy all values from the original array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
            }

            //Add the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            //Set the old array to be the new array
            _actors = tempArray;
        }

        /// <summary>
        /// Removes the actor from the scene 
        /// </summary>
        /// <param name="actor">The actor to remove from the scene</param>
        /// <returns>False if the actor was not in the scene array</returns>
        public virtual bool RemoveActor(Actor actor)
        {
            //Create a variable to store if the removal as successful
            bool actorRemoved = false;

            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Copy all values except the actor we dont want into the new array
            int j = 0;
            for (int i = 0; i < _actors.Length; i++)
            {
                //if the actor taht the loop is on is not the one to remove...
                if ( _actors[i] != actor)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _actors[i];
                    j++;
                }
                //Otherwise if the actor is the one to remove...
                else
                {
                    //...Set actorRemoved to true
                    actorRemoved = true;
                    
                }
            }

            //If the actor removal was successful...
            if (actorRemoved)
                //...set the old array to be the new array
                _actors = tempArray;

            return actorRemoved;
        }

    }
}
