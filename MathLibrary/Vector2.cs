using System;


namespace MathLibrary
{
    public struct Vector2
    {
        public float x;
        public float y;



        /// <summary>
        /// Adds the x value of the second vector to the first, and adds the y value of the second vector to the first
        /// </summary>
        /// <param name="lhs">The vector that is increasing</param>
        /// <param name="rhs">The vector used to increase the 1st vector</param>
        /// <returns>The resuly of the vector addition</returns>
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { x = lhs.x + rhs.x, y = lhs.y + rhs.y };
        }

        /// <summary>
        /// Subtracts the x value of the second vector to the first, and subtracts the y value of the second vector to the first
        /// </summary>
        /// <param name="lhs">The vector that is subtracting</param>
        /// <param name="rhs">The vector used to Subtract from the 1st vector</param>
        /// <returns>The resuly of the vector subtraction</returns>
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { x = lhs.x - rhs.x, y = lhs.y - rhs.y };
        }


        /// <summary>
        /// Multiplies the vectors x and y values by the scalar
        /// </summary>
        /// <param name="lhs">the vector that is being scaled</param>
        /// <param name="rhs">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator *(Vector2 lhs, float rhs)
        {
            return new Vector2 { x = lhs.x * rhs, y = lhs.y * rhs };
        }

        /// <summary>
        /// Divides the vectors x and y values by the scalar given
        /// </summary>
        /// <param name="lhs">the vector that is being divided</param>
        /// <param name="rhs">The value to divide the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator /(Vector2 lhs, float rhs)
        {
            return new Vector2 { x = lhs.x / rhs, y = lhs.y / rhs };
        }

        /// <summary>
        /// Compares the x and y values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of the comparison</param>
        /// <param name="rhs">The right side of the left comparison</param>
        /// <returns>true if the x values of both vectors</returns>
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x != rhs.x && lhs.y != rhs.y;
        }



    }
}
