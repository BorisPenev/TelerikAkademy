$(document).ready(function () {

    $('#btnReturnLastNumber').click(function () {
        var n = parseInt($('#txtReturnLastNumber').val());
        var numbers = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"]
        $('#pnlReturnLastNumber').text(numbers[n % 10]);
    });

    $('#btnReverseNumber').click(function () {
        var n = $('#txtReverseNumber').val();
        var s = n.split('');
        s.reverse();
        $('#pnlReverseNumber').text(s.join(''));
    });

    $('#btnSearchInText').click(function () {
        var source = $('#txtSearchInText1').val();
        var word = $('#txtSearchInText2').val();
        var result = [];

        result.push("CaseSensitive: " + SearchInText(source, word));
        result.push("Case-InSensitive: " + SearchInText(source, word, true));
        $('#pnlSearchInText').text(result.toString());
    });

    function SearchInText(source, word, isCaseSensitive) {
        var re = new RegExp('\\b' + word + '\\b', "g");
        if (isCaseSensitive === true)
            re = new RegExp('\\b' + word + '\\b', "gi");
        return source.match(re);
    }

    $('#btnCountDivTags').click(function () {
        var divs = document.getElementsByTagName('div');
        $('#pnlCountDivTags').text(divs.length);
    });

    $('#btnCountNumber').click(function () {
        var arr = $('#txtCountNumber1').val().split(',');
        var number = parseInt($('#txtCountNumber2').val());
        $('#pnlCountNumber').text(CountNumber(number, arr));
    });

    function CountNumber(number, array) {
        var count = 0;
        for (var i = 0; i < array.length; i++) {
            if (parseInt(array[i]) == number)
                count++;
        }
        return count;
    }

    $('#btnCountNumberTest').click(function () {
        var arr = $('#txtCountNumber1').val().split(',');
        var result = [];
        result.push(" Number 5 count 4 time:");
        if (CountNumber(5, arr))
            result.push("true");
        else
            result.push("false");

        result.push(" Number 9 count 1 time:");
        if (CountNumber(9, arr))
            result.push("true");
        else
            result.push("false");

        result.push(" Number 1 count 2 time:");
        if (CountNumber(1, arr))
            result.push("true");
        else
            result.push("false");

        $('#pnlCountNumberTest').text(result.toString());
    });

    $('#btnIsBiggerThanNighboars').click(function () {
        var source = $('#txtIsBiggerThanNighboars1').val().split(',');
        var position = parseInt($('#txtIsBiggerThanNighboars2').val());

        $('#pnlIsBiggerThanNighboars').text(IsBiggerThanNighboars(source, position));
    });

    function IsBiggerThanNighboars(source, position) {
        if (position - 1 >= 0 && position + 1 < source.length) {
            if (source[position - 1] < source[position] && source[position] > source[position + 1])
                return true;
        }
        return false;
    }

    

    $('#btnGetIndexOfFirstBiggerNumber').click(function () {
        var source = $('#txtGetIndexOfFirstBiggerNumber').val().split(',');
        $('#pnlGetIndexOfFirstBiggerNumber').text(GetIndexOfFirstBiggerNumbe(source));
    });

    function GetIndexOfFirstBiggerNumbe(source){
        for (var i = 0; i < source.length; i++) {
            if (IsBiggerThanNighboars(source, i))
                return i;
        }
        return -1;
    }
});