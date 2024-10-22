using System.Diagnostics;

public class SudokuChecker
{
    private static bool IsValidRow(char[][] board, int row)
    {
        HashSet<char> seen = new HashSet<char>();
        for (int j = 0; j < 9; j++)
        {
            if (board[row][j] != '.' && !seen.Add(board[row][j]))
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsValidColumn(char[][] board, int col)
    {
        HashSet<char> seen = new HashSet<char>();
        for (int i = 0; i < 9; i++)
        {
            if (board[i][col] != '.' && !seen.Add(board[i][col]))
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsValidBlock(char[][] board, int row, int col)
    {
        HashSet<char> seen = new HashSet<char>();
        for (int i = row; i < row + 3; i++)
        {
            for (int j = col; j < col + 3; j++)
            {
                if (board[i][j] != '.' && !seen.Add(board[i][j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsValidSudoku(char[][] board)
    {
        for (int i = 0; i < 9; i++)
        {
            if (!IsValidRow(board, i))
            {
                return false;
            }
        }

        for (int j = 0; j < 9; j++)
        {
            if (!IsValidColumn(board, j))
            {
                return false;
            }
        }

        for (int i = 0; i < 9; i += 3)
        {
            for (int j = 0; j < 9; j += 3)
            {
                if (!IsValidBlock(board, i, j))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public async Task<bool> IsValidSudokuAsync(char[][] board)
    {
        var tasks = new List<Task<bool>>();

        tasks.Add(Task.Run(() =>
        {
            for (int i = 0; i < 9; i++)
            {
                if (!IsValidRow(board, i))
                {
                    return false;
                }
            }
            return true;
        }));

        tasks.Add(Task.Run(() =>
        {
            for (int j = 0; j < 9; j++)
            {
                if (!IsValidColumn(board, j))
                {
                    return false;
                }
            }
            return true;
        }));

        tasks.Add(Task.Run(() =>
        {
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    if (!IsValidBlock(board, i, j))
                    {
                        return false;
                    }
                }
            }
            return true;
        }));

        await Task.WhenAll(tasks);
        return tasks.All(t => t.Result);
    }
}

class Program
{
    public static void Main(string[] args)
    {
        char[][] board1 = {
            new char[] {'5','3','.','.','7','.','.','.','.'},
            new char[] {'6','.','.','1','9','5','.','.','.'},
            new char[] {'.','9','8','.','.','.','.','6','.'},
            new char[] {'8','.','.','.','6','.','.','.','3'},
            new char[] {'4','.','.','8','.','3','.','.','1'},
            new char[] {'7','.','.','.','2','.','.','.','6'},
            new char[] {'.','6','.','.','.','.','2','8','.'},
            new char[] {'.','.','.','4','1','9','.','.','5'},
            new char[] {'.','.','.','.','8','.','.','7','9'}
        };

        SudokuChecker checker = new SudokuChecker();

        Debug.Assert(checker.IsValidSudoku(board1) == checker.IsValidSudokuAsync(board1).Result);
        Debug.Assert(checker.IsValidSudokuAsync(board1).Result == true);

        board1[0][0] = '8';
        Debug.Assert(checker.IsValidSudoku(board1) == checker.IsValidSudokuAsync(board1).Result);
        Debug.Assert(checker.IsValidSudokuAsync(board1).Result == false);

        Console.WriteLine("Success");
    }
}
