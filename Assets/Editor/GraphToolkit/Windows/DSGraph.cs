using Unity.GraphToolkit.Editor;
using UnityEditor;
using System;

[Graph(AssetExtensions)]
[Serializable]
public class DSGraph : Graph
{
    public const string AssetExtensions = "simpleg";
    [MenuItem("Assets/Create/Graph Toolkit /Graph", false)]

    private static void createAsste()
    {
        GraphDatabase.PromptInProjectBrowserToCreateNewAsset<DSGraph>();
    }
}
