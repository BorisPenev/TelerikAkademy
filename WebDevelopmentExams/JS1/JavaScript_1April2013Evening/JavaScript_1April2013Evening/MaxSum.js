function Solve(args) {
    var max = -99999999;
    var currentSum = 0;
    for (var i = 1; i < args.length; i++)
    {
        currentSum = currentSum + parseInt(args[i]);
        if (currentSum > max)
        {
            max = currentSum;
        }
        if (currentSum < 0)
        {
            currentSum = 0;
        }
    }
    return max;
}