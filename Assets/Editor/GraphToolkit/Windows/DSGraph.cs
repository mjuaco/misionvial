using Unity.GraphToolkit.Editor;
using UnityEditor;
using System;

[Graph(AssetExtension)]
[Serializable]
public class DSGraph : Graph
{
    public const string AssetExtension = "simpleg";
    [MenuItem("Assets/Create/Graph Toolkit /Graph", false)]

    private static void createAsste()
    {
        GraphDatabase.PromptInProjectBrowserToCreateNewAsset<DSGraph>();
    }
}
