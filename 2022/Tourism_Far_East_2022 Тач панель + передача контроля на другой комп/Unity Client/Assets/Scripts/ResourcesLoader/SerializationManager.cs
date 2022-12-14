using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using UnityEngine;

namespace Tourism.ResourcesLoader
{
    public static class SerializationManager
    {
        /// <summary>
        ///  Load data from PlayerPrefs. If function return false - use default constructor for serialized object
        ///  Loaded object must have empty constructor and public fields
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool LoadLocal<T> ( string filePath, out T result )
        {
            result = default;

            if ( TryLoad( out result, filePath ) ) return true;

            Debug.Log( $"Load ({filePath}) failed" );

            return false;
        }

        public static void SaveLocal<T> ( string filePath, T data )
        {
            try
            {
                var streamWriter = new StreamWriter(filePath);
                streamWriter.Write(JsonUtility.ToJson( data ));
                streamWriter.Close();
            }
            catch ( Exception e )
            {
                Debug.Log( e );
            }
        }

        private static bool TryLoad<T> ( out T data, string filePath )
        {
            data = default;
            try
            {
                var streamReader = new StreamReader(filePath);
                data = JsonUtility.FromJson<T>( streamReader.ReadLine() );
                streamReader.Close();
            }
            catch ( Exception e )
            {
                Debug.Log( e );
            }

            return data != null;
        }
    }
}