$(document).ready(function () {

    $('#btnIsOdd').click(function () {
        var i = parseInt($('#txtIsOdd').val());
        var result = false;
        if (i % 2 == 0)
            result = true;
        $('#pnlIsOdd').text(result);
    });

    $('#btnDividedWithoutRemainder').click(function () {
        var i = parseInt($('#txtDividedWithoutRemainder').val());
        var result = false;
        if (i % 7 == 0 && i % 5 == 0)
            result = true;
        $('#pnlDividedWithoutRemainder').text(result);
    });

    $('#btnCalculateRectangle').click(function () {
        var w = parseInt($('#txtWidth').val());
        var h = parseInt($('#txtHeight').val());
        $('#pnlCalculateRectangle').text(w * h);
    });

    $('#btnIsThirdDigitIsSeven').click(function () {
        var i = parseInt($('#txtIsThirdDigitIsSeven').val()).toString();
        var result = false;
        if (i.length > 3)
        {
            if(i.split('').reverse()[2] == 7)
                result = true;
        }
            
        $('#pnlIsThirdDigitIsSeven').text(result);
    });

    $('#btnGetBitByPosition').click(function () {
        var i = parseInt($('#txtGetBitByPosition').val());
        var result = false;

        var mask = 1 << 3;
        var nAndMask = i & mask;
        result = nAndMask >> 3;

        $('#pnlGetBitByPosition').text(result);
    });

    $('#btnIsInCircle').click(function () {
        var x = parseInt($('#txtIsInCircleX').val());
        var y = parseInt($('#txtIsInCircleY').val());
        var result = false;
        if (Math.sqrt(x - 0) + Math.sqrt(y - 0) <= Math.sqrt(5))
            result = true;
        $('#pnlIsInCircle').text(result);
    });

    $('#btnIsPrime').click(function () {
        var source = parseInt($('#txtIsPrime').val());
        var result = true;
        if (source <= 1)
            result = false;
        else {
            for (var i = 2; i < Math.sqrt(source) ; i++) {
                if (source % i == 0) {
                    result = false;
                }
            }
        }
        $('#pnlIsPrime').text(result);
    });

    $('#btnCalculatesTrapezoid').click(function () {
        var a = parseInt($('#txtCalculatesTrapezoidA').val());
        var b = parseInt($('#txtCalculatesTrapezoidB').val());
        var h = parseInt($('#txtCalculatesTrapezoidH').val());
        var result = (a + b) / 2 * h;
        $('#pnlCalculatesTrapezoid').text(result);
    });

    $('#btnIsPointInTheCircleOutRectangle').click(function () {
        var x = parseInt($('#txtIsPointInTheCircleOutRectangleX').val());
        var y = parseInt($('#txtIsPointInTheCircleOutRectangleY').val());
        var result = false;

        var inCircle = false;
        if (Math.sqrt(x - 1) + Math.sqrt(y - 1) <= 3)
            inCircle = true;

        var inRectangle = false;
        if ((1 <= x && x <= 6) && (-1 <= y  && y <= 2))
            inRectangle = true;

        if(inCircle && inRectangle)
            result = true;
        $('#pnlIsPointInTheCircleOutRectangle').text(result);
    });
});