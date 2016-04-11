using System;
using System.IO;
using UnityEngine;

namespace TileDraw
{
    public static class Util
    {
        public static float RoundTo(float i, float round)
        {
            i /= round;
            i = (float)Math.Round(i, MidpointRounding.AwayFromZero);
            i *= round;
            return i;
        }

        public static void SaveImage(Texture2D texture, string path)
        {
            var bytes = texture.EncodeToPNG();
            using (var file = File.Open(path, FileMode.Create))
            {
                var binary = new BinaryWriter(file);
                binary.Write(bytes);
            }
        }

        public static T[,] ResizeArray<T>(T[,] oldArray, int size)
        {
            if (oldArray == null)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                System.Reflection.MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                throw new UnityException("Array is null. Called from: " + methodBase.Name); // e.g.
            }

            //Check which method called another method
            var oldSizeX = oldArray.GetLength(1);
            var oldSizeY = oldArray.GetLength(0);

            var newArray = new T[size, size];

            // If old array has a length of zero, just return a new array of max size and default values
            if (oldSizeX == 0 || oldSizeY == 0)
                return newArray;

            var xFactor = oldSizeX / (float)size;
            var yFactor = oldSizeY / (float)size;

            for (var x = 0; x < size; ++x)
                for (var y = 0; y < size; ++y)
                {
                    newArray[y, x] = oldArray[(int)Math.Floor(y * yFactor), (int)Math.Floor(x * xFactor)];
                }

            return newArray;
        }
        public static T[] ResizeArray<T>(T[] oldArray, int size)
        {
            if (oldArray == null)
            {
                var stackTrace = new System.Diagnostics.StackTrace();
                System.Reflection.MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
                throw new UnityException("Array is null. Called from: " + methodBase.Name); // e.g.
            }

            if (oldArray.Length == 0)
                return new T[size * size];

            var dim = Mathf.Sqrt(oldArray.Length);
            if (Math.Abs(oldArray.Length % dim) > float.Epsilon)
            {
                throw new UnityException("Array isn't square");
            }
            var oldSize = (int)dim;

            //if (size > oldSize) return oldArray; // no need to scale up

            var newArray = new T[size * size];
            var nFactor = oldSize / (float)size;

            for (var x = 0; x < size; ++x)
                for (var y = 0; y < size; ++y)
                {
                    newArray[y * size + x] = oldArray[(int)Math.Floor(y * nFactor) * oldSize + (int)Math.Floor(x * nFactor)];
                }

            return newArray;
        }
        public static T[] InitilizeArray<T>(int size, T value)
        {
            var array = new T[size*size];

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    array[y*size + x] = value;
                }
            }

            return array;
        }
    }
}
