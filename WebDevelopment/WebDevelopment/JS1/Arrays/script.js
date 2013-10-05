$(document).ready(function () {

    $('#btnInitializeArray').click(function () {
        var sbResult = new Array(20);
        for (var i = 0; i < sbResult.length; i++) {
            sbResult[i] = i * 5;
        }
        $('#pnlInitializeArray').text(sbResult.toString());
    });

    $('#btnIsArrayEqualLexicographically').click(function () {
        var firstArr = $('#txtIsArrayEqualLexicographically1').val().split('');
        var secondArr = $('#txtIsArrayEqualLexicographically2').val().split('');
        var isEqual = true;
        if (firstArr.length == secondArr.length)
        {
            for (var i = 0; i < firstArr.length; i++) {
                if (firstArr[i] != secondArr[i])
                {
                    isEqual = false;
                    break;
                }
            }
        }
        else
            isEqual = false;
        $('#pnlIsArrayEqualLexicographically').text(isEqual);
    });

    $('#btnMaximalSequence').click(function () {
        var arr = $('#txtMaximalSequence').val().split(',');
        var count = 0;
        var maxCount = 0;
        var lastIndex = 0;
        var sb = [];
        for (var i = 0; i < arr.length; i++) {
            if(i + 1 < arr.length){
                if (arr[i] == arr[i + 1])
                    count++;
                else
                {
                    if (count > maxCount)
                    {
                        maxCount = count;
                        lastIndex = i;
                    }
                    count = 0;
                }
            }
            else {
                if (count > maxCount) {
                    //maxCount = count;
                    lastIndex = i;
                }
                if (arr[i] == arr[lastIndex])
                {
                    if (count > maxCount) {
                        maxCount = count;
                        lastIndex = i;
                    }
                    count = 0;
                }
            }
        }
        for (var i = 0; i < maxCount + 1; i++) {
            sb.push(arr[i + lastIndex - maxCount]);
        }

        $('#pnlMaximalSequence').text(sb.toString());
    });

    $('#btnMaximalIncreasingSequence').click(function () {
        var arr = $('#txtMaximalIncreasingSequence').val().split(',');
        var count = 0;
        var maxCount = 0;
        var lastIndex = 0;
        var sb = [];
        for (var i = 0; i < arr.length; i++) {
            if (i + 1 < arr.length) {
                if (parseInt(arr[i]) + 1 == arr[i + 1])
                    count++;
                else {
                    if (count > maxCount) {
                        maxCount = count;
                        lastIndex = i;
                    }
                    count = 0;
                }
            }
            else {
                if (count > maxCount) {
                    //maxCount = count;
                    lastIndex = i;
                }
                if (parseInt(arr[i]) + 1 == arr[lastIndex]) {
                    if (count > maxCount) {
                        maxCount = count;
                        lastIndex = i;
                    }
                    count = 0;
                }
            }
        }
        for (var i = 0; i < maxCount + 1; i++) {
            sb.push(arr[i + lastIndex - maxCount]);
        }

        $('#pnlMaximalIncreasingSequence').text(sb.toString());
    });

    $('#btnSelectionSort').click(function () {
        var arr = $('#txtSelectionSort').val().split(',');
        var i = 0;
        var j = 0;
        var min = 0;
        var temp = 0;

        for (i = 0; i < arr.length - 1; i++)
        {
            min = i;

            for (j = i + 1; j < arr.length; j++)
            {
                if (parseInt(arr[j]) < parseInt(arr[min]))
                {
                    min = j;
                }
            }

            temp = parseInt(arr[i]);
            arr[i] = parseInt(arr[min]);
            arr[min] = temp;
        }

        $('#pnlSelectionSort').text(arr.toString());
    });

    $('#btnFindsMostFrequentNumber').click(function () {
        var arr = $('#txtFindsMostFrequentNumber').val().split(',');
        var sb = [];
        for (var i = 0; i < arr.length; i++) {
            if (sb[arr[i]] == undefined)
                sb[arr[i]] = 1;
            else
                sb[arr[i]]++;
        }

        var max = 0;
        var maxIndex = 0;
        for (var n in sb) {
            if (max == 0)
            {
                max = parseInt(sb[n]);
                maxIndex = n;
            }
            if (max < parseInt(sb[n]))
            {
                max = parseInt(sb[n]);
                maxIndex = n;
            }
        }

        $('#pnlFindsMostFrequentNumber').text("The frequent of number " + maxIndex + " is: " + max);
    });
   
    $('#btnBinarySearch').click(function () {
        var arr = $('#txtBinarySearch1').val().split(',');
        var n = $('#txtBinarySearch2').val();
        arr.sort();
        arr.binarySearchFast(n)

        $('#pnlBinarySearch').text(arr.binarySearchFast(n));
    });

    Array.prototype.binarySearchFast = function (search) {

        var size = this.length,
            high = size - 1,
            low = 0;

        while (high > low) {

            if (this[low] === search)
                return low;
            else if (this[high] === search)
                return high;

            target = (((search - this[low]) / (this[high] - this[low])) * (high - low)) >>> 0;

            if (this[target] === search)
                return target;
            else if (search > this[target])
                low = target + 1, high--;
            else
                high = target - 1, low++;
        }

        return -1;
    };
});