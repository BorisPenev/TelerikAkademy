$(document).ready(function () {

    $('#btnPrintNumbersFromOneToN').click(function () {
        var n = parseInt($('#txtPrintNumbersFromOneToN').val());
        var sb = [];
        for (var i = 1; i <= n; i++) {
            sb.push(i)
        }
        $('#pnlPrintNumbersFromOneToN').text(sb.toString());
    });

    $('#btnPrintNumberThatIsNotDevided').click(function () {
        var n = parseInt($('#txtPrintNumberThatIsNotDevided').val());
        var sb = [];
        for (var i = 1; i <= n; i++) {
            if (!(i % 3 == 0 && i % 7 == 0))
                sb.push(i)
        }
        $('#pnlPrintNumberThatIsNotDevided').text(sb.toString());
    });

    $('#btnFindMinMaxNumbers').click(function () {
        var numbers = $('#txtFindMinMaxNumbers').val().toString().split(',');
        var min = 0;
        var max = 0;
        for (var n in numbers) {
            if (min == 0)
                min = parseInt(numbers[n]);
            if (max == 0)
                max = parseInt(numbers[n]);

            if (max < parseInt(numbers[n]))
                max = parseInt(numbers[n]);
            else if (min > parseInt(numbers[n]))
                min = parseInt(numbers[n]);
        }
        $('#pnlFindMinMaxNumbers').text("min: " + min + " max: " + max);
    });

    $('#btnPrintProperties').click(function () {
        var objects = [window, navigator, document];
        var sbResult = [];
        for (var o in objects) {
            var sb = [];
            for (var p in objects[o]) {
                sb.push(p);
            }
            sb.sort();
            sbResult.push(objects[o] + ": min " + sb[0] + " max: " + sb[sb.length - 1])
        }
        $('#pnlPrintProperties').text(sbResult.toString());
    });

});