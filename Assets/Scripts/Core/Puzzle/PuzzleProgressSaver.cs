using System;
using System.Collections.Generic;
using System.IO;
using Core.Puzzle.Parameters;
using Cysharp.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Converters;
using UnityEngine;

namespace Core.Puzzle
{
    public class PuzzleProgressSaver
    {
        private const string FILE_PATH = "puzzle_progress.json";

        public static void SavePuzzle(Dictionary<string, HouseParameters> puzzleProgress)
        {
            if (puzzleProgress == null)
            {
                Debug.LogError("Puzzle progress is null.");
                return;
            }

            foreach (var key in puzzleProgress.Keys)
            {
                if (puzzleProgress[key] == null)
                {
                    Debug.LogError($"HouseParameters for key {key} is null.");
                    return;
                }
            }

            string json = JsonConvert.SerializeObject(new PuzzleProgressWrapper(puzzleProgress), Formatting.Indented);
            File.WriteAllText(FILE_PATH, json);
        }

        public static async UniTask<Dictionary<string, HouseParameters>> LoadPuzzle()
        {
            if (!File.Exists(FILE_PATH))
            {
                GeneratePuzzleFile();
            }

            string json = await File.ReadAllTextAsync(FILE_PATH);
            if (string.IsNullOrEmpty(json))
            {
                Debug.LogError("Loaded JSON is null or empty.");
                return new Dictionary<string, HouseParameters>();
            }

            PuzzleProgressWrapper wrapper;
            try
            {
                wrapper = JsonConvert.DeserializeObject<PuzzleProgressWrapper>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to deserialize JSON: {e.Message}");
                return new Dictionary<string, HouseParameters>();
            }

            if (wrapper == null || wrapper.keys == null || wrapper.values == null)
            {
                Debug.LogError("Deserialized wrapper or its contents are null.");
                return new Dictionary<string, HouseParameters>();
            }

            return wrapper.ToDictionary();
        }

        private static void GeneratePuzzleFile()
        {
            Dictionary<string, HouseParameters> initialPuzzleProgress = new()
            {
                { "House1", new HouseParameters { owner = OwnerParameter.Select, drink = DrinkParameter.Beer, pet = PetParameter.Select } },
                { "House2", new HouseParameters { owner = OwnerParameter.Select, drink = DrinkParameter.Select, pet = PetParameter.Select } },
                { "House3", new HouseParameters { owner = OwnerParameter.Select, drink = DrinkParameter.Select, pet = PetParameter.Select } }
            };

            Debug.Log("Puzzle file was generated");

            SavePuzzle(initialPuzzleProgress);
        }

        [Serializable]
        private class PuzzleProgressWrapper
        {
            public List<string> keys;
            public List<HouseParameters> values;

            // Default constructor to ensure keys and values are initialized
            public PuzzleProgressWrapper()
            {
                keys = new List<string>();
                values = new List<HouseParameters>();
            }

            public PuzzleProgressWrapper(Dictionary<string, HouseParameters> dictionary) : this()
            {
                if (dictionary == null)
                {
                    throw new ArgumentNullException(nameof(dictionary), "Dictionary cannot be null");
                }

                foreach (var kvp in dictionary)
                {
                    if (kvp.Key == null)
                    {
                        throw new ArgumentNullException(nameof(kvp.Key), "Key cannot be null");
                    }

                    if (kvp.Value == null)
                    {
                        throw new ArgumentNullException(nameof(kvp.Value), "Value cannot be null");
                    }

                    keys.Add(kvp.Key);
                    values.Add(kvp.Value);
                }
            }

            public Dictionary<string, HouseParameters> ToDictionary()
            {
                Dictionary<string, HouseParameters> dictionary = new();
                for (int i = 0; i < keys.Count; i++)
                {
                    if (keys[i] == null || values[i] == null)
                    {
                        Debug.LogError("Key or Value in the wrapper is null.");
                        continue;
                    }
                    dictionary[keys[i]] = values[i];
                }
                return dictionary;
            }
        }
    }
    
    [Serializable]
    public class HouseParameters
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public OwnerParameter? owner;
        
        [JsonConverter(typeof(StringEnumConverter))]
        public DrinkParameter? drink;
        
        [JsonConverter(typeof(StringEnumConverter))]
        public PetParameter? pet;

        public HouseParameters()
        {
            owner = OwnerParameter.Select;
            drink = DrinkParameter.Select;
            pet = PetParameter.Select;
        }
    }
}