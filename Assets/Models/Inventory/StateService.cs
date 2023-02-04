using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models.Inventory
{
    public class StateService
    {
        private const string SavesDirectory = "Saves";
        private static string path
        {
            get
            {
                string defaultPath = Application.dataPath + "/" + SavesDirectory;
                if (!Directory.Exists(defaultPath))
                {
                    Directory.CreateDirectory(defaultPath);
                }
                return defaultPath;
            }
        }
        public static void SaveState(CharacterController player)
        {
            StateModel model = new StateModel
            {
                PlayerPosition = new Vector3S(player.transform.position),
                PlayerRotation = new Vector3S(player.transform.rotation),
                Balance = InventoryManager.GetBalance(),
                PotsCount = InventoryManager.PotsCount()
            };
            string jsonState = JsonConvert.SerializeObject(model, Formatting.Indented);
            try
            {
                if (SaveState(jsonState))
                {
                    Debug.Log("Pomyślnie zapisano stan");
                }
                else
                {
                    Debug.Log($"Nieznany problem przy zapisywaniu");
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Problem przy zapisywaniu {ex}");
            }
        }
        public static void LoadLastState()
        {

        }
        public static List<Tuple<Guid, string>> GetAllSavesList()
        {
            List<Tuple<Guid, string>> response = new List<Tuple<Guid, string>>();

            string[] fileNames = Directory.GetFiles(path, "*.json").Select(p => Path.GetFileName(p)).ToArray();

            try
            {
                for (int i = 0; i < fileNames.Length; i++)
                {
                    var splited = fileNames[i].Replace(".json", "").Split("&&");
                    response.Add(Tuple.Create(Guid.Parse(splited[0]), $"Data: {splited[1]}/{splited[2]}/{splited[3]} Godz.: {splited[4]}:{splited[5]}:{splited[6]}"));
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                return null;
            }
            return response;
        }
        private static bool SaveState(string jsonState)
        {
            string fileName = $"{path}/{Guid.NewGuid()}&&{DateTime.Now.ToString("dd&&MM&&yyyy&&hh&&mm&&ss")}&&save.json";
            File.WriteAllText(fileName, jsonState);
            return true;
        }
        private class StateModel
        {
            public Guid SaveGuid { get; set; }
            public Vector3S PlayerPosition { get; set; }
            public Vector3S PlayerRotation { get; set; }
            public float Balance { get; set; }
            public int PotsCount { get; set; }
            
        }
        [Serializable]
        public struct Vector3S
        {
            public float x { get; set; }
            public float y { get; set; }
            public float z { get; set; }
            public Vector3S(UnityEngine.Vector3 vector)
            {
                this.x = vector.x;
                this.y = vector.y;
                this.z = vector.z;
            }
            public Vector3S(UnityEngine.Quaternion quaternion)
            {
                this.x = quaternion.x;
                this.y = quaternion.y;
                this.z = quaternion.z;
            }
        }
    }
    
}
