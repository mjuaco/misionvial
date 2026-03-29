using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SCBCarsEntity", menuName = "Scriptable Objects/SCBCarsEntity")]
public class SCBCarsEntity : ScriptableObject
{
    [field:SerializeField] public Sprite background { get; private set; }
    [field:SerializeField] public Sprite sprite { get; private set; }
    [field:SerializeField] public Sprite isblock { get; private set; }

}
