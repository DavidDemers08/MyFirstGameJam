using UnityEngine;

public class Tile : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }
    [field:SerializeField]public bool walkable { get; set; }
}
