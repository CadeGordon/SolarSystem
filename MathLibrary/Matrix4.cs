using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Matrix4
    {
        public float M00, M01, M02, M03, M10, M11, M12, M13, M20, M21, M22, M23, M30, M31, M32, M33;

        public Matrix4(float m00, float m01, float m02, float m03,
                       float m10, float m11, float m12, float m13,
                       float m20, float m21, float m22, float m23,
                       float m30, float m31, float m32, float m33)
        {
            M00 = m00; M01 = m01; M02 = m02; M03 = m03;
            M10 = m10; M11 = m11; M12 = m12; M13 = m13;
            M20 = m20; M21 = m21; M22 = m22; M23 = m23;
            M30 = m30; M31 = m31; M32 = m32; M33 = m33;
        }

        public static Matrix4 Identity
        {
            get
            {
                return new Matrix4();
            }
        }

        /// <summary>
        /// Creates a new matric that has been rotated by the given value in radians
        /// </summary>
        /// <param name="raidans"></param>
        /// <returns>The result of the rotation</returns>
        public static Matrix4 CreateRotationZ(float radians)
        {
            return new Matrix4();

        }

        public static Matrix4 CreateRotationY(float radians)
        {
            return new Matrix4();

        }

        public static Matrix4 CreateRotationX(float radians)
        {
            return new Matrix4();

        }

        /// <summary>
        /// Creates a new matix that has been translted by the
        /// </summary>
        /// <param name="x">The x position of the new matrix</param>
        /// <param name="y">The y position of the new matrix </param>
        /// <returns></returns>
        public static Matrix4 CreateTranslation(float x, float y, float z)
        {
            return new Matrix4();
        }

        /// <summary>
        /// Creates a new matrix that has been scaled by the given value
        /// </summary>
        /// <param name="x">the value to use to scale the materix in the x axis</param>
        /// <param name="y">the value to use to scale the materix in the y axis</param>
        /// <returns>The result of the scale</returns>
        public static Matrix4 CreateScale(float x, float y, float z)
        {
            return new Matrix4();
        }

        public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4();

        }

        public static Matrix4 operator -(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4();
        }

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                  
                );

        }

        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            return new Vector4();
        }

    }
}
