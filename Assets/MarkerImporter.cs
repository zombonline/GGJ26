using UnityEditor;
using UnityEngine;
using System.IO;

public class MarkerImporter
{
    [MenuItem("FMOD/Import Markers CSV")]
    static void ImportCSV()
    {
        string path = "Assets/Markers/song1_markers.csv";
        string[] lines = File.ReadAllLines(path);

        MarkerDataList asset = ScriptableObject.CreateInstance<MarkerDataList>();
        asset.markers = new MarkerData[lines.Length - 1]; // skip header

        for (int i = 1; i < lines.Length; i++)
        {
            var split = lines[i].Split(',');
            asset.markers[i - 1] = new MarkerData
            {
                name = split[0],
                time = float.Parse(split[1])
            };
        }

        AssetDatabase.CreateAsset(asset, "Assets/Markers/Song1Markers.asset");
        AssetDatabase.SaveAssets();

        Debug.Log("MarkerDataList imported!");
    }
}