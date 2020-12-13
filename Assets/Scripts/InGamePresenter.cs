using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePresenter : MonoBehaviour
{
    private const int StageSize = 4;
    private const int MinCellValue = 2;

    private InGameModel inGameModel;
    private InGameView inGameView;

    [SerializeField] private Cell[] cells;
    private readonly int[,] stageStates = new int[4, 4];

    /// <summary>
    /// 盤面の再描画を行う必要があるかのフラグ
    /// </summary>
    private bool isDirty;

    private float probability = 0.5f;

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
        stageStates[(int)posB.x, (int)posB.y] = Random.Range(0, 1.0f) <  probability? MinCellValue : MinCellValue * 2;

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
                    CheckCell(row, col, 1, 0);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (var row = 0; row < StageSize; row++)
            {
                for (var col = 0; col < StageSize; col++)
                {
                    CheckCell(row, col, -1, 0);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (var row = 0; row < StageSize; row++)
            {
                for (var col = 0; col < StageSize; col++)
                {
                    CheckCell(row, col, 0, -1);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (var row = StageSize; row >= 0; row--)
            {
                for (var col = 0; col < StageSize; col++)
                {
                    CheckCell(row, col, 0, 1);
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

    
    

    private bool CheckBorder(int row, int column, int horizontal, int vertical)
    {
        // チェックマスが4x4外ならそれ以上処理を行わない
        if (row < 0 || row >= StageSize || column < 0 || column >= StageSize)
        {
            return false;
        }

        // 移動先が4x4外ならそれ以上処理は行わない
        var nextRow = row + vertical;
        var nextCol = column + horizontal;
        if (nextRow < 0 || nextRow >= StageSize || nextCol < 0 || nextCol >= StageSize)
        {
            return false;
        }

        return true;
    }

    private void CheckCell(int row, int column, int horizontal, int vertical)
    {
        // 4x4の境界線チェック
        if (CheckBorder(row, column, horizontal, vertical) == false)
        {
            return;
        }
        // 空欄マスは移動処理をしない
        if (stageStates[row, column] == 0)
        {
            return;
        }
        // 移動可能条件を満たした場合のみ移動処理
        MoveCell(row, column, horizontal, vertical);
    }

    private void MoveCell(int row, int column, int horizontal, int vertical)
    {
        // 4x4境界線チェック
        // 再起呼び出し以降も毎回境界線チェックはするため冒頭で呼び出しておく
        if (CheckBorder(row, column, horizontal, vertical) == false)
        {
            return;
        }

        // 移動先の位置を計算
        var nextRow = row + vertical;
        var nextCol = column + horizontal;

        // 移動元と移動先の値を取得
        var value = stageStates[row, column];
        var nextValue = stageStates[nextRow, nextCol];

        // 次の移動先のマスが0の場合は移動する
        if (nextValue == 0)
        {
            // 移動元のマスは空欄になるので0を埋める
            stageStates[row, column] = 0;

            // 移動先のマスに移動元のマスの値を代入する
            stageStates[nextRow, nextCol] = value;

            // 移動先のマスでさらに移動チェック
            MoveCell(nextRow, nextCol, horizontal, vertical);
        }
        // 同じ値のときは合成処理
        else if (value == nextValue)
        {
            stageStates[row, column] = 0;
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

    private bool IsGameOver(int[,] stageStates)
    {
        // 空いている場所があればゲームオーバーにはならない
        for (var row = 0; row < stageStates.GetLength(0); row++)
        {
            for (var col = 0; col < stageStates.GetLength(1); col++)
            {
                if (stageStates[row, col] <= 0)
                {
                    return false;
                }
            }
        }

        // 合成可能なマスが一つでもあればゲームオーバーにはならない
        for (var row = 0; row < stageStates.GetLength(0); row++)
        {
            for (var col = 0; col < stageStates.GetLength(1); col++)
            {
                var state = stageStates[row, col];
                var canMerge = false;
                if (row > 0)
                {
                    canMerge |= state == stageStates[row - 1, col];
                }

                if (row < stageStates.GetLength(0) - 1)
                {
                    canMerge |= state == stageStates[row + 1, col];
                }

                if (col > 0)
                {
                    canMerge |= state == stageStates[row, col - 1];
                }

                if (col < stageStates.GetLength(1) - 1)
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

    private void LoadResultScene()
    {
        SceneManager.LoadScene(SceneNames.Result);
    }

}