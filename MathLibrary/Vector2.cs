using System;


namespace MathLibrary
{
    public struct Vector2
    {
        public float X;
        public float Y;


        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the length of the vector 
        /// </summary>
        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y);
            }
        }

        /// <summary>
        /// Gets the normalized version of this vector without changing it
        /// </summary>
        public Vector2 Normalized
        {
            get
            {
                Vector2 value = this;
                return value.Normalize();
            }
        }

        /// <summary>
        /// Changes this vector to have a magnitude that is equal to one
        /// </summary>
        /// <returns>The result of the normalzition. Returns an epmty vevtor if the magnitude is zero</returns>
        public Vector2 Normalize()
        {
            if (Magnitude == 0)
                return new Vector2();

            return this / Magnitude;
        }

        
        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">the right hang side of the operation</param>
        /// <returns>The dot product of this first vector on to the second</returns>
        public static float DotProdcut(Vector2 lhs, Vector2 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y);
        }

        /// <summary>
        /// Find the distance from the first vector the second
        /// </summary>
        /// <param name="lhs">The starting point</param>
        /// <param name="rhs">The ending point</param>
        /// <returns>A scalar represnting the distance</returns>
        public static float Distance(Vector2 lhs, Vector2 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        /// <summary>
        /// Adds the x value of the second vector to the first, and adds the y value of the second vector to the first
        /// </summary>
        /// <param name="lhs">The vector that is increasing</param>
        /// <param name="rhs">The vector used to increase the 1st vector</param>
        /// <returns>The resuly of the vector addition</returns>
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y };
        }

        /// <summary>
        /// Subtracts the x value of the second vector to the first, and subtracts the y value of the second vector to the first
        /// </summary>
        /// <param name="lhs">The vector that is subtracting</param>
        /// <param name="rhs">The vector used to Subtract from the 1st vector</param>
        /// <returns>The resuly of the vector subtraction</returns>
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y };
        }


        /// <summary>
        /// Multiplies the vectors x and y values by the scalar
        /// </summary>
        /// <param name="lhs">the vector that is being scaled</param>
        /// <param name="rhs">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator *(Vector2 lhs, float rhs)
        {
            return new Vector2 { X = lhs.X * rhs, Y = lhs.Y * rhs };
        }

        /// <summary>
        /// Divides the vectors x and y values by the scalar given
        /// </summary>
        /// <param name="lhs">the vector that is being divided</param>
        /// <param name="rhs">The value to divide the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator /(Vector2 lhs, float rhs)
        {
            return new Vector2 { X = lhs.X / rhs, Y = lhs.Y / rhs };
        }

        /// <summary>
        /// Compares the x and y values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of the comparison</param>
        /// <param name="rhs">The right side of the left comparison</param>
        /// <returns>true if the x values of both vectors</returns>
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X != rhs.X && lhs.Y != rhs.Y;
        }



    }
}
