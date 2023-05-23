GetSpiralArray(new int[3, 3]
{
    { 1, 2, 3 },
    { 4, 5, 6 },
    { 7, 8, 9 },
}).ForEach(i => Console.Write(i + ", "));
Console.WriteLine();

GetSpiralArray(new int[3, 4]
{
    { 1, 2, 3, 4 },
    { 5, 6, 7, 8 },
    { 9, 10, 11, 12 },
}).ForEach(i => Console.Write(i + ", "));

static List<int> GetSpiralArray(int[,] matrix)
{
    var result = new List<int>(matrix.Length);
    var down = matrix.GetLength(0) - 1;
    var right = matrix.GetLength(1) - 1;
    var up = 0;
    var left = 0;

    while(result.Count < matrix.Length)
    {
        //downward
        for (int i = up; i <= down; i++)
            result.Add(matrix[i, left]);
        left++;
        //rightward
        for (int i = left; i <= right; i++)
            result.Add(matrix[down, i]);
        down--;
        //upward
        for (int i = down; i >= up; i--)
            result.Add(matrix[i, right]);
        right--;
        //leftward
        for (int i = right; i >= left; i--)
            result.Add(matrix[up, i]);
        up++;
    }

    return result;
}