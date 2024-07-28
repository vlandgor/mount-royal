using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Riddle
{
    public class EinsteinPuzzleEditor : EditorWindow
    {
        private List<House> houses = new List<House>();
        private string saveFilePath;

        [MenuItem("Window/Einstein Puzzle Editor")]
        public static void ShowWindow()
        {
            GetWindow<EinsteinPuzzleEditor>("Einstein Puzzle Editor");
        }

        private void OnEnable()
        {
            saveFilePath = Path.Combine(Application.dataPath, "EinsteinPuzzleData.json");
            LoadData();
        }

        private void OnDisable()
        {
            SaveData();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Einstein Puzzle Editor", EditorStyles.boldLabel);

            if (GUILayout.Button("Add House"))
            {
                houses.Add(new House());
            }

            // Draw the grid of houses and parameters
            DrawGrid();

            EditorGUILayout.Space();

            if (GUILayout.Button("Save Data"))
            {
                SaveData();
            }

            if (GUILayout.Button("Load Data"))
            {
                LoadData();
            }
        }

        private void DrawGrid()
        {
            EditorGUILayout.BeginHorizontal();
        
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Color");
            EditorGUILayout.LabelField("Nationality");
            EditorGUILayout.LabelField("Drink");
            EditorGUILayout.LabelField("Cigarette");
            EditorGUILayout.LabelField("Pet");
            EditorGUILayout.EndVertical();

            // Draw houses
            for (int i = 0; i < houses.Count; i++)
            {
                DrawHouse(houses[i], i);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawHouse(House house, int index)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"House #{index + 1}");

            house.color = DrawDropdown("Color", house.color, House.Colors);
            house.nationality = DrawDropdown("Nationality", house.nationality, House.Nationalities);
            house.drink = DrawDropdown("Drink", house.drink, House.Drinks);
            house.cigarette = DrawDropdown("Cigarette", house.cigarette, House.Cigarettes);
            house.pet = DrawDropdown("Pet", house.pet, House.Pets);

            if (GUILayout.Button("Remove House"))
            {
                houses.RemoveAt(index);
            }

            EditorGUILayout.EndVertical();
        }

        private string DrawDropdown(string label, string current, string[] options)
        {
            int index = System.Array.IndexOf(options, current);
            if (index == -1) index = 0;
            index = EditorGUILayout.Popup(label, index, options);
            return options[index];
        }

        private void SaveData()
        {
            string json = JsonUtility.ToJson(new HouseListWrapper(houses), true);
            File.WriteAllText(saveFilePath, json);
            Debug.Log("Data saved to " + saveFilePath);
        }

        private void LoadData()
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                houses = JsonUtility.FromJson<HouseListWrapper>(json).houses;
                Debug.Log("Data loaded from " + saveFilePath);
            }
            else
            {
                Debug.LogWarning("Save file not found");
            }
        }
    }

    [System.Serializable]
    public class HouseListWrapper
    {
        public List<House> houses;

        public HouseListWrapper(List<House> houses)
        {
            this.houses = houses;
        }
    }

    [System.Serializable]
    public class House
    {
        public string color;
        public string nationality;
        public string drink;
        public string cigarette;
        public string pet;

        public static readonly string[] Colors = { "Red", "Green", "Blue", "Yellow", "White" };
        public static readonly string[] Nationalities = { "British", "Swedish", "Danish", "Norwegian", "German" };
        public static readonly string[] Drinks = { "Tea", "Coffee", "Milk", "Beer", "Water" };
        public static readonly string[] Cigarettes = { "Pall Mall", "Dunhill", "Blends", "Blue Master", "Prince" };
        public static readonly string[] Pets = { "Dog", "Bird", "Cat", "Horse", "Fish" };
    }
}