using UnityEngine;

/// <summary>
/// ・2Dグリッド上のタイルを表現
/// ・各タイルは、グリッド上の(x、y)位置と、
///   そのタイルを視覚的に表現するGameObjectを持つ。
/// </summary>
public class Tile
{
    private readonly int _posX, _posY;
    private readonly GameObject _tileObj;

    public Tile(int y, int x, GameObject obj)
    {
        _posX = x;
        _posY = y;
        _tileObj = obj;
    }
}