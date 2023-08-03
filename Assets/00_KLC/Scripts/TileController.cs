using UnityEngine;

/// <summary>
/// タイルを初期化して管理するコントローラー
/// </summary>
public class TileController : MonoBehaviour
{
    /// <summary>
    /// タイルイメージ
    /// </summary>
    [SerializeField]
    private Sprite tileImage;
    /// <summary>
    /// 2Dグリッドタイプのタイル配列
    /// </summary>
    private Tile[,] _tiles;

    private void Start() => InitializeTiles();

    /// <summary>
    /// タイル配列を初期化し、各タイルを生成
    /// </summary>
    private void InitializeTiles()
    {
        _tiles = new Tile[KLCDefine.TILE_SIZE, KLCDefine.TILE_SIZE];

        for (int i = 0; i < KLCDefine.TILE_SIZE; i++)
        {
            for (int j = 0; j < KLCDefine.TILE_SIZE; j++)
                _tiles[i, j] = CreateAndReturnTile(i, j);
        }
    }

    /// <summary>
    /// 指定された場所にタイルを生成し、返却
    /// </summary>
    /// <param name="i">タイルの行位置</param>
    /// <param name="j">タイルの列位置</param>
    /// <returns>生成されたタイル</returns>
    private Tile CreateAndReturnTile(int i, int j)
    {
        var tileObj = CreateTileObject(i, j);
        return new Tile(i, j, tileObj);
    }

    /// <summary>
    /// 指定された場所にタイルゲームオブジェクトを作成
    /// </summary>
    /// <param name="i">タイルの行位置</param>
    /// <param name="j">タイルの列位置</param>
    /// <returns>生成された、タイルゲームオブジェクト</returns>
    private GameObject CreateTileObject(int i, int j)
    {
        // タイル位置計算
        var position = CalculateTilePosition(i, j);
        var tileObj = new GameObject($"Tile_{i}_{j}")
        {
            // ゲームオブジェクトの位置と親設定
            transform = { position = position, parent = transform }
        };
        
        var spriteRenderer = tileObj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = tileImage;

        return tileObj;
    }

    /// <summary>
    /// タイルの位置を計算し、返却
    /// </summary>
    /// <param name="i">タイルの行位置</param>
    /// <param name="j">タイルの列位置</param>
    /// <returns>計算された位置</returns>
    private Vector3 CalculateTilePosition(int i, int j) 
        => new Vector3(j - i, (i + j) * 0.5f);
}