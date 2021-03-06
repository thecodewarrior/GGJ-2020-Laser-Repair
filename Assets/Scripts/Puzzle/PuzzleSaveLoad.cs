using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
// ReSharper disable Unity.InefficientPropertyAccess

namespace Puzzle
{
    public class PuzzleSaveLoad : MonoBehaviour
    {
        public EmitterSerializer emitterSerializer;
        public ReceiverSerializer receiverSerializer;
        public MirrorSerializer mirrorSerializer;
        public SplitterSerializer splitterSerializer;
        
        private Dictionary<string, ISerializer> _serializers;

        private void Start()
        {
            _serializers = new Dictionary<string, ISerializer>
            {
                ["emitter"] = emitterSerializer, 
                ["receiver"] = receiverSerializer, 
                ["mirror"] = mirrorSerializer,
                ["splitter"] = splitterSerializer
            };
        }

        private string _levelPath => Application.dataPath + "/Resources/Levels/";
        public string LevelPath(string levelName) => _levelPath + levelName + ".json";
        
        public void SaveLevel(string levelName)
        {
            var json = JsonUtility.ToJson(SaveData());
            System.IO.Directory.CreateDirectory(levelName);
            System.IO.File.WriteAllText(LevelPath(levelName), json);
        }

        public void LoadLevel(string levelName)
        {
            string json = null;
            if (System.IO.File.Exists(LevelPath(levelName)))
            {
                json = System.IO.File.ReadAllText(LevelPath(levelName));
            } 
            if (json == null)
            {
                var asset = Resources.Load<TextAsset>("Levels/" + levelName);
                if (asset) json = asset.text;
            }

            if (json == null)
            {
                Debug.Log("Could not find level + `" + levelName + "`");
                return;
            }
            
            LoadData(JsonUtility.FromJson<PuzzleData>(json));
        }

        private string TestSaveName = "SavedLevel";
        public void TestSaveNameChanged(string saveName)
        {
            TestSaveName = saveName;
        }
        
        public void TestSaveToFile()
        {
            SaveLevel(TestSaveName);
        }

        public void TestLoadFromFile()
        {
            LoadLevel(TestSaveName);
        }

        public void TestSaveLoad()
        {
            var data = SaveData();
            LoadData(data);
        }
        
        public PuzzleData SaveData()
        {
            var markers = FindObjectsOfType<PuzzleDataMarker>();

            var objects = new List<PuzzleObjectData>();

            foreach (var marker in markers)
            {
                if (_serializers.ContainsKey(marker.type))
                {
                    var data = new PuzzleObjectData
                    {
                        x = marker.transform.position.x,
                        y = marker.transform.position.y,
                        rotation = marker.transform.rotation.eulerAngles.z,
                        type = marker.type,
                        data = _serializers[marker.type].Save(marker.gameObject)
                    };
                    objects.Add(data);
                }
            }
            
            var table = FindObjectOfType<OpticalTable>();
            
            return new PuzzleData
            {
                width = table.width,
                height = table.height,
                objects = objects.ToArray()
            };
        }

        public void LoadData(PuzzleData puzzleData)
        {
            foreach (var marker in FindObjectsOfType<PuzzleDataMarker>())
            {
                Destroy(marker.gameObject);
            }

            var table = FindObjectOfType<OpticalTable>();
            table.width = puzzleData.width;
            table.height = puzzleData.height;
            
            foreach (var objectData in puzzleData.objects)
            {
                if (_serializers.ContainsKey(objectData.type))
                {
                    var serializer = _serializers[objectData.type];
                    var obj = Instantiate(serializer.Prefab);
                    obj.transform.position = new Vector3(objectData.x, objectData.y, 0);
                    var rot = obj.transform.rotation;
                    rot.eulerAngles = new Vector3(0, 0, objectData.rotation);
                    obj.transform.rotation = rot;
                    serializer.Load(obj, objectData.data);
                }
            }
        }
    }
    
    public interface ISerializer
    {
        GameObject Prefab { get; }
        string[] Save(GameObject gameObject);
        void Load(GameObject gameObject, string[] data);
    }
    
    [Serializable]
    public class EmitterSerializer : ISerializer
    {
        public GameObject prefab;
        public GameObject Prefab => prefab;

        public string[] Save(GameObject gameObject)
        {
            var color = gameObject.GetComponent<LaserEmitter>().color;
            return new[] {color ? color.id : "NONE"};
        }

        public void Load(GameObject gameObject, string[] data)
        {
            var emitter = gameObject.GetComponent<LaserEmitter>();
            if (data[0] != "NONE")
                emitter.color = ColorRegistry.GetColor(data[0]);
        }
    }

    [Serializable]
    public class ReceiverSerializer : ISerializer
    {
        public GameObject prefab;
        public GameObject Prefab => prefab;

        public string[] Save(GameObject gameObject)
        {
            return new string[0];
        }

        public void Load(GameObject gameObject, string[] data)
        {
        }
    }

    [Serializable]
    public class MirrorSerializer : ISerializer
    {
        public GameObject prefab;
        public GameObject Prefab => prefab;

        public string[] Save(GameObject gameObject)
        {
            return new string[0];
        }

        public void Load(GameObject gameObject, string[] data)
        {
        }
    }
    
    [Serializable]
    public class SplitterSerializer : ISerializer
    {
        public GameObject prefab;
        public GameObject Prefab => prefab;

        public string[] Save(GameObject gameObject)
        {
            return new string[0];
        }

        public void Load(GameObject gameObject, string[] data)
        {
        }
    }
}