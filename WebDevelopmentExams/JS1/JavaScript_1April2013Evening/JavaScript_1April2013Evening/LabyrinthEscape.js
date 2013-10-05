var matrixDirections = [];
var currentRow = 0;
var currentCol = 0;
var sum = 0;
var vol = 0;
var count = 0;
function Solve(args) {
    newMultiArray(args[0].split(' ')[0], args[0].split(' ')[1], args)
    vol = args[0].split(' ')[1];
    var result = "";
    while (result === "" || result.length <= 1)
    {
        if (sum === 0) {
            result = getNewField(parseInt(args[1].split(' ')[0]), parseInt(args[1].split(' ')[1]));
        }
        else {
            switch (result) {
                case "l":
                    result = getNewField(currentRow, currentCol - 1);
                    break;
                case "r":
                    result = getNewField(currentRow, currentCol + 1);
                    break;
                case "u":
                    result = getNewField(currentRow - 1, currentCol);
                    break;
                case "d":
                    result = getNewField(currentRow + 1, currentCol);
                    break;
                default:
                    break;
            }
        }
    }
    if (result === "lost")
        return result + " " + (count - 1);
    else
    return result + " " + sum;
}

function getNewField(row, col) {
    var result = "";
    if (row >= 0 && row < matrixDirections.length && col >= 0 && col < matrixDirections[row].length) {
        if (matrixDirections[row][col] === 0)
        {
            sum = count;
            result = "lost";
        }
        
        sum += (row * vol) + col + 1;
        currentRow = row;
        currentCol = col;
        result = matrixDirections[currentRow][currentCol];
        if (result === 0)
            result = "lost";
        matrixDirections[currentRow][currentCol] = 0;
        count++;
    }
    else
        result = "out";
    return result;
}

function newMultiArray(rows, cols, values) {
    var i = 0;
    var j = 0;
    var count = 1;
    for (i = 0; i < rows; i++) {
        matrixDirections[i] = new Array(cols);
        for (j = 0; j < cols; j++) {
            matrixDirections[i][j] = values[i + 2].split('')[j];
            count++;
        }
    }
}