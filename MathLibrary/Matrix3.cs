using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Matrix4
    {
        public float M00, M01, M02, M10, M11, M12, M20, M21, M22;

        public Matrix4(float m00, float m01, float m02,
                       float m10, float m11, float m12,
                       float m20, float m21, float m22)
        {
            M00 = m00; M01 = m01; M02 = m02;
            M10 = m10; M11 = m11; M12 = m12;
            M20 = m20; M21 = m21; M22 = m22;
        }

        public static Matrix4 Identity
        {
            get
            {
                return new Matrix4(1, 0, 0,
                                   0, 1, 0,
                                   0, 0, 1);
            }
        }

        /// <summary>
        /// Creates a new matric that has been rotated by the given value in radians
        /// </summary>
        /// <param name="raidans"></param>
        /// <returns>The result of the rotation</returns>
        public static Matrix4 CreateRotation(float radians)
        {
            return new Matrix4((float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                                -(float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                                0, 0, 1);

        }

        /// <summary>
        /// Creates a new matix that has been translted by the
        /// </summary>
        /// <param name="x">The x position of the new matrix</param>
        /// <param name="y">The y position of the new matrix </param>
        /// <returns></returns>
        public static Matrix4 CreateTranslation(float x, float y)
        {
            return new Matrix4(1, 0, x,
                               0, 1, y,
                               0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been scaled by the given value
        /// </summary>
        /// <param name="x">the value to use to scale the materix in the x axis</param>
        /// <param name="y">the value to use to scale the materix in the y axis</param>
        /// <returns>The result of the scale</returns>
        public static Matrix4 CreateScale(float x, float y)
        {
            return new Matrix4(x, 0, 0,
                               0, y, 0,
                               0, 0, 1);
        }
        
        public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4  (lhs.M00 + rhs.M00, lhs.M01 + rhs.M01, lhs.M02 + rhs.M02,
                                 lhs.M10 + rhs.M10, lhs.M11 + rhs.M11, lhs.M12 + rhs.M12,
                                 lhs.M20 + rhs.M20, lhs.M21 + rhs.M21, lhs.M22 + rhs.M22);
             
        }

        public static Matrix4 operator -(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4  (lhs.M00 - rhs.M00, lhs.M01 - rhs.M01, lhs.M02 - rhs.M02,
                                 lhs.M10 - rhs.M10, lhs.M11 - rhs.M11, lhs.M12 - rhs.M12,
                                 lhs.M20 - rhs.M20, lhs.M21 - rhs.M21, lhs.M22 - rhs.M22);
        }

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                  //Row 1, Column1
                  lhs.M00 * rhs.M00 + lhs.M01 * rhs.M10 + lhs.M02 * rhs.M20,
                  //Row 1, Column 2
                  lhs.M00 * rhs.M01 + lhs.M01 * rhs.M11 + lhs.M02 * rhs.M21,
                  //Row 1, Column 3
                  lhs.M00 * rhs.M02 + lhs.M01 * rhs.M12 + lhs.M02 * rhs.M22,

                  //Row 2, Column 1
                  lhs.M10 * rhs.M00 + lhs.M11 * rhs.M10 + lhs.M12 * rhs.M20,
                  //Row 2, Column 2
                  lhs.M10 * rhs.M01 + lhs.M11 * rhs.M11 + lhs.M12 * rhs.M21,
                  //Row 2, Column 3
                  lhs.M10 * rhs.M02 + lhs.M11 * rhs.M12 + lhs.M12 * rhs.M22,

                  //Row 3, Column 1
                  lhs.M20 * rhs.M00 + lhs.M21 * rhs.M10 + lhs.M22 * rhs.M20,
                  //Row 3 , Column 2
                  lhs.M20 * rhs.M01 + lhs.M21 * rhs.M11 + lhs.M22 * rhs.M21,
                  //Row 3, Coloum 2
                  lhs.M20 * rhs.M02 + lhs.M21 * rhs.M12 + lhs.M22 * rhs.M22
                );

        }
    }
}
