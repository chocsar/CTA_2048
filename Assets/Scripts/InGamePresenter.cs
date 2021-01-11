using UnityEngine;

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
        InitStage();

        // ステージの初期状態をViewに反映
        ApplyStage();
    }

    private void Update()
    {

        isDirty = false;

        InputKey();

        if (isDirty)
        {
            CreateNewRandomCell();

            //状態をステージに反映
            ApplyStage();

            if (IsGameOver(stageStates))
            {
                SaveScore(inGameModel.GetScore());
                LoadResultScene();
            }
        }

    }

    /// <summary>
    /// ステージの初期状態を作成する
    /// </summary>
    private void InitStage()
    {
        for (var row = 0; row < StageSize; row++)
        {
            for (var col = 0; col < StageSize; col++)
            {
                stageStates[row, col] = 0;
            }
        }

        //セルを新規作成
        var posA = new Vector2(Random.Range(0, StageSize), Random.Range(0, StageSize));
        var posB = new Vector2((posA.x + Random.Range(1, StageSize - 1)) % StageSize, (posA.y + Random.Range(1, StageSize - 1)) % StageSize);
        stageStates[(int)posA.x, (int)posA.y] = MinCellValue;
        stageStates[(int)posB.x, (int)posB.y] = Random.Range(0, 1.0f) < Probability ? MinCellValue : MinCellValue * 2;

        ApplyStage();
    }

    /// <summary>
    /// ステージの状態を画面上に反映させる
    /// </summary>
    private void ApplyStage()
    {
        for (var row = 0; row < StageSize; row++)
        {
            for (var col = 0; col < StageSize; col++)
            {
                cells[row * StageSize + col].SetText(stageStates[row, col]);
            }
        }
    }

    /// <summary>
    /// キー入力を受け付けて、セルを移動させる
    /// </summary>
    private void InputKey()
    {
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
        if (IsOutsideStage(row, col) || IsZeroState(row, col))
        {
            return;
        }

        // 移動先の位置を計算
        var nextRow = row + vertical;
        var nextCol = col + horizontal;

        if (IsOutsideStage(nextRow, nextCol)) { return; }

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
        // 同じ値のときは合成してスコア反映
        else if (value == nextValue)
        {
            MergeCells(row, col, nextRow, nextCol, value);
            SetScore(value);
        }
        // 異なる値のときは移動処理を終了
        else if (value != nextValue)
        {
            return;
        }

        isDirty = true;
    }

    /// <summary>
    /// セルを合成する
    /// </summary>
    /// <param name="row">対象セル1の行</param>
    /// <param name="col">対象セル1の列</param>
    /// <param name="nextRow">対象セル2の行</param>
    /// <param name="nextCol">対象セル2の列</param>
    /// <param name="value">元の値</param>
    private void MergeCells(int row, int col, int nextRow, int nextCol, int value)
    {
        stageStates[row, col] = 0;
        stageStates[nextRow, nextCol] = value * 2;
    }

    /// <summary>
    /// スコアを加算し、UIに反映させる
    /// </summary>
    /// <param name="cellValue">セルの値</param>
    private void SetScore(int cellValue)
    {
        inGameModel.SetScore(cellValue);
    }

    /// <summary>
    /// スコアをセーブする
    /// </summary>
    /// <param name="score">スコア</param>
    private void SaveScore(int score)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.Score, score);
    }


    /// <summary>
    /// 対象セルの状態がゼロかどうかを返す
    /// </summary>
    /// <param name="row">対象セルの行</param>
    /// <param name="col">対象セルの列</param>
    /// <returns>対象セルの状態がゼロかどうか</returns>
    private bool IsZeroState(int row, int col)
    {
        return stageStates[row, col] == 0;
    }

    /// <summary>
    /// 対象セルがステージ外かどうかを返す
    /// </summary>
    /// <param name="row">対象セルの行</param>
    /// <param name="col">対象セルの列</param>
    /// <returns>対象セルがステージ外かどうか</returns>
    private bool IsOutsideStage(int row, int col)
    {
        if (row < 0 || row >= StageSize || col < 0 || col >= StageSize)
        {
            return true;
        }

        return false;
    }



    /// <summary>
    /// セルを新しく生成する
    /// </summary>
    private void CreateNewRandomCell()
    {
        // ゲーム終了時はスポーンしない
        if (IsGameOver(stageStates))
        {
            return;
        }

        //ランダムな箇所にセルを作成
        var row = Random.Range(0, StageSize);
        var col = Random.Range(0, StageSize);
        while (stageStates[row, col] != 0)
        {
            row = Random.Range(0, StageSize);
            col = Random.Range(0, StageSize);
        }
        stageStates[row, col] = Random.Range(0, 1f) < Probability ? 2 : 4;
    }

    /// <summary>
    /// ゲームオーバーかどうか調べる
    /// </summary>
    /// <param name="stageStates">ステージの状態</param>
    /// <returns>ゲームオーバーかどうか</returns>
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
    /// リザルトシーンへ遷移する
    /// </summary>
    private void LoadResultScene()
    {
        SceneController.Instance.LoadScene(SceneNames.Result);
    }

}