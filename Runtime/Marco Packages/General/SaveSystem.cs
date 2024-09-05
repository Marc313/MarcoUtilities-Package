using System.IO;
using UnityEngine;

namespace MarcoUtilities
{
    /// <summary>
    /// Generic C# Saving System, depending on System.Text.Json.
    /// </summary>
    public static class SaveSystem
    {
        public static string currentFileName = "./Test.txt";

        /// <summary>
        /// Writes a json formatted version of an object T to a file.
        /// </summary>
        /// <typeparam name="T"> The data type we want to write to a file. </typeparam>
        /// <param name="saveData"> The data object we want to write to a file. </param>
        /// <param name="appendDataToFile"> Whether we should override the file, or append text to the file. </param>
        public static void Save<T>(T saveData, bool appendDataToFile)
        {
            StreamWriter writer = new StreamWriter(currentFileName, appendDataToFile);
            writer.WriteLine(JsonUtility.ToJson(saveData));
            writer.Close();
            writer.Dispose();
        }

        public static T Load<T>()
        {
            if (!File.Exists(currentFileName))
            {
                Debug.LogWarning("File with name {currentFileName} was not found! Make sure to change the fileName with SaveSystem.SetFileName()");
                return default;
            }

            StreamReader reader = new StreamReader(currentFileName);
            T data = JsonUtility.FromJson<T>(reader.ReadToEnd());
            reader.Close();
            reader.Dispose();

            return data;
        }

        public static void SetFileName(string fileName)
        {
            currentFileName = fileName;
        }

        public static void DeleteDataFile()
        {
            if (DataFileExists())
            {
                File.Delete(currentFileName);
            }
        }

        public static bool DataFileExists()
        {
            return File.Exists(currentFileName);
        }
    }

}
