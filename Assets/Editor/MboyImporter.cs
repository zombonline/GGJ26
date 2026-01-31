using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, "mboy")]
public class MboyImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        // read the raw file as text
        string text = File.ReadAllText(ctx.assetPath);

        // create a TextAsset from it
        var textAsset = new TextAsset(text);

        // add it to the import context so Unity sees it as the main asset
        ctx.AddObjectToAsset("mboy", textAsset);
        ctx.SetMainObject(textAsset);
    }
}