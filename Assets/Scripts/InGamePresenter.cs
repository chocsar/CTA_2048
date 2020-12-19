using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePresenter : MonoBehaviour
{
    private const int StageSize = 4;
    private const int MinCellValue = 2;
    private const float Probability = 0.5f;
    private const int FirstDimension = 0;
    private const int SecondDimension = 1;

    private readonly int[,] stageStates = new int[4, 4];

    private InGameModel inGameModel;
    private InGameView inGameView;

    [SerializeField] private Cell[] cells;
    

    /// <summary>
    /// 盤面の再描画を行う必要があるかのフラグ
    /// </summary>
    private bool isDirty;

    

    private void Start()
    {
        inGameModel = GetComponent<InGameModel>();
        inGameView = GetComponent<InGameView>();

        // Modelの値の変更を監視する
        inGameModel.changeScore += inGameView.SetScore;
        

        // ステージの初期状態を生成
        for (var row = 0; row < StageSize; row++)
        {
            for (var col = 0; col < StageSize; col++)
            {
                stageStates[row, col] = 0;
            }
        }
        var posA = new Vector2(Random.Range(0, StageSize), Random.Range(0, StageSize));
        var posB = new Vector2((posA.x + Random.Range(1, StageSize - 1)) % StageSize, (posA.y + Random.Range(1, StageSize - 1)) % StageSize);
        stageStates[(int)posA.x, (int)posA.y] = MinCellValue;
        stageStates[(int)posB.x, (int)posB.y] = Random.Range(0, 1.0f) <  Probability? MinCellValue : MinCellValue * 2;

        // ステージの初期状態をViewに反映
        for (var row = 0; row < StageSize; row++)
        {
            for (var col = 0; col < StageSize; col++)
            {
                cells[row * StageSize + col].SetText(stageStates[row, col]);
            }
        }
    }

    

    private void Update()
    {

        isDirty = false;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (var col = StageSize; col >= 0; col--)
            {
                for (var row = 0; row < StageSize; row++)
                {
                    MoveCell(row, col, 1, 0);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (var row = 0; row < StageSize; row++)
            {
                for (var col = 0; col < StageSize; col++)
                {
                    MoveCell(row, col, -1, 0);
                    
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (var row = 0; row < StageSize; row++)
            {
                for (var col = 0; col < StageSize; col++)
                {
                    MoveCell(row, col, 0, -1);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (var row = StageSize; row >= 0; row--)
            {
                for (var col = 0; col < StageSize; col++)
                {
                    MoveCell(row, col, 0, 1);
                }
            }
        }

        if (isDirty)
        {
            CreateNewRandomCell();

            for (var row = 0; row < StageSize; row++)
            {
                for (var col = 0; col < StageSize; col++)
                {
                    cells[row * 4 + col].SetText(stageStates[row, col]);
                }
            }

            if (IsGameOver(stageStates))
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.Score, inGameModel.GetScore());
                LoadResultScene();
            }
        }

    }

    /// <summary>
    /// 対象セルを移動させる
    /// </summary>
    /// <param name="row">対象セルの行</param>
    /// <param name="col">対象セルの列</param>
    /// <param name="horizontal">横方向の移動量</param>
    /// <param name="vertical">縦方向の移動量</param>
    private void MoveCell(int row, int col, int horizontal, int vertical)
    {
        //対象のセルが移動可能かどうか調べる
        if (isOutsideStage(row, col) || isZeroState(row, col) || CheckBorder(row, col, horizontal, vertical) == false)
        {
            return;
        }

        // 移動先の位置を計算
        var nextRow = row + vertical;
        var nextCol = col + horizontal;

        // 移動元と移動先の値を取得
        var value = stageStates[row, col];
        var nextValue = stageStates[nextRow, nextCol];

        // 次の移動先のマスが0の場合は移動する
        if (nextValue == 0)
        {
            // 移動元のマスは空欄になるので0を埋める
            stageStates[row, col] = 0;

            // 移動先のマスに移動元のマスの値を代入する
            stageStates[nextRow, nextCol] = value;

            // 移動先のマスでさらに移動チェック
            MoveCell(nextRow, nextCol, horizontal, vertical);
        }
        // 同じ値のときは合成処理
        else if (value == nextValue)
        {
            stageStates[row, col] = 0;
            stageStates[nextRow, nextCol] = value * 2;
            inGameModel.SetScore(value);
            
        }
        // 異なる値のときは移動処理を終了
        else if (value != nextValue)
        {
            return;
        }

        isDirty = true;
    }
    
    /// <summary>
    /// 対象セルの状態がゼロかどうかを返す
    /// </summary>
    /// <param name="row">対象セルの行</param>
    /// <param name="col">対象セルの列</param>
    /// <returns>対象セルの状態がゼロかどうか</returns>
    private bool isZeroState(int row, int col)
    {
        return stageStates[row, col] == 0;
    }
    
    /// <summary>
    /// 対象セルがステージ外かどうかを返す
    /// </summary>
    /// <param name="row">対象セルの行</param>
    /// <param name="col">対象セルの列</param>
    /// <returns>対象セルがステージ外かどうか</returns>
    private bool isOutsideStage(int row, int col)
    {
        if (row < 0 || row >= StageSize || col < 0 || col >= StageSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 移動先がステージの外でないか調べる
    /// </summary>
    /// <param name="row">対象セルの行</param>
    /// <param name="col">対象セルの列</param>
    /// <param name="horizontal">横方向の移動量</param>
    /// <param name="vertical">縦方向の移動量</param>
    /// <returns>移動可能かどうか</returns>
    private bool CheckBorder(int row, int col, int horizontal, int vertical)
    {
        // 移動先が4x4外ならそれ以上処理は行わない
        var nextRow = row + vertical;
        var nextCol = col + horizontal;
        if (nextRow < 0 || nextRow >= StageSize || nextCol < 0 || nextCol >= StageSize)
        {
            return false;
        }

        return true;
    }

    

    /// <summary>
    /// 
    /// </summary>
    private void CreateNewRandomCell()
    {
        // ゲーム終了時はスポーンしない
        if (IsGameOver(stageStates))
        {
            return;
        }

        var row = Random.Range(0, StageSize);
        var col = Random.Range(0, StageSize);
        while (stageStates[row, col] != 0)
        {
            row = Random.Range(0, StageSize);
            col = Random.Range(0, StageSize);
        }

        stageStates[row, col] = Random.Range(0, 1f) < 0.5f ? 2 : 4;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stageStates"></param>
    /// <returns></returns>
    private bool IsGameOver(int[,] stageStates)
    {
        // 空いている場所があればゲームオーバーにはならない
        for (var row = 0; row < stageStates.GetLength(FirstDimension); row++)
        {
            for (var col = 0; col < stageStates.GetLength(SecondDimension); col++)
            {
                if (stageStates[row, col] <= 0)
                {
                    return false;
                }
            }
        }

        // 合成可能なマスが一つでもあればゲームオーバーにはならない
        for (var row = 0; row < stageStates.GetLength(FirstDimension); row++)
        {
            for (var col = 0; col < stageStates.GetLength(SecondDimension); col++)
            {
                var state = stageStates[row, col];
                var canMerge = false;
                if (row > 0)
                {
                    canMerge |= state == stageStates[row - 1, col];
                }

                if (row < stageStates.GetLength(FirstDimension) - 1)
                {
                    canMerge |= state == stageStates[row + 1, col];
                }

                if (col > 0)
                {
                    canMerge |= state == stageStates[row, col - 1];
                }

                if (col < stageStates.GetLength(SecondDimension) - 1)
                {
                    canMerge |= state == stageStates[row, col + 1];
                }

                if (canMerge)
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    /// <summary>
    /// 
    /// </summary>
    private void LoadResultScene()
    {
        SceneManager.LoadScene(SceneNames.Result);
    }

}