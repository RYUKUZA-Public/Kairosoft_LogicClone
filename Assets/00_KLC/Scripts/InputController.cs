using UnityEngine;

/// <summary>
/// ユーザー入力処理コントローラ
/// </summary>
public class InputController : MonoBehaviour
{
    /// <summary>
    /// マウスポインターイメージ
    /// </summary>
    [SerializeField]
    private Sprite mousePointerSprite;
    
    /// <summary>
    /// マウスポインターオブジェクト
    /// </summary>
    private GameObject _mousePointer;
    /// <summary>
    /// 最後に記録されたマウスの位置
    /// </summary>
    private Vector3 _lastMousePos;
    /// <summary>
    /// Camera.mainキャッシング用
    /// </summary>
    private Camera _mainCamera;

    private void Start() => InitializeMousePointer();

    /// <summary>
    /// 初期化
    /// </summary>
    private void InitializeMousePointer()
    {
        _mousePointer = new GameObject("MousePointer");
        var spriteRenderer = _mousePointer.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = mousePointerSprite;
        
        // カメラキャッシング
        _mainCamera = Camera.main; 
    }
    
    private void Update() => UpdateMousePointerPosition();

    /// <summary>
    /// マウスポインターの位置を更新
    /// </summary>
    private void UpdateMousePointerPosition()
    {
        _lastMousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _lastMousePos.z = 0;
        _mousePointer.transform.position = CalculateTileCenterPositionFromMousePosition(_lastMousePos);
    }

    /// <summary>
    /// 現マウス位置で、中央位置を計算
    /// </summary>
    /// <param name="mousePos">現マウス位置</param>
    /// <returns>タイル中央位置</returns>
    private Vector3 CalculateTileCenterPositionFromMousePosition(Vector3 mousePos)
    {
        mousePos.y *= 2;
        int mPosX = Mathf.FloorToInt(mousePos.x);
        int mPosY = Mathf.FloorToInt(mousePos.y);

        int checkEven = (mPosX + mPosY) & 1;
        int tempEven = (checkEven * -2) + 1;

        if (checkEven == 1) 
            mPosX += 1;

        float remainX = mousePos.x - mPosX;
        float remainY = mousePos.y - mPosY;

        float remainSum = (tempEven * remainX) + remainY;
        int floorSum = Mathf.FloorToInt(remainSum);

        return new Vector3(mPosX + (floorSum * tempEven), (mPosY + floorSum) * 0.5f, -1);
    }
}