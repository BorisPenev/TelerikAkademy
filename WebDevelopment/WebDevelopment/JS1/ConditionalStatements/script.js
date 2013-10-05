$(document).ready(function () {

    $('#brnExchangeValue').click(function () {
        var first = parseInt($('#txtExchangeValue1').val());
        var second = parseInt($('#txtExchangeValue2').val());
        if (first > second)
        {
            first += second;
            second = first - second;
            first -= second;
        }
        $('#pnlExchangeValue').text(first + " " + second);
    });

    $('#btnShowNumberSign').click(function () {
        var first = parseInt($('#txtShowNumberSign1').val());
        var second = parseInt($('#txtShowNumberSign2').val());
        var third = parseInt($('#txtShowNumberSign3').val());
        var result = "+";
        if (first < 0 || second < 0 || third < 0)
            result = "-";
        $('#pnlShowNumberSign').text(result);
    });

    $('#btnFindBiggerNumber').click(function () {
        var first = parseInt($('#txtFindBiggerNumber1').val());
        var second = parseInt($('#txtFindBiggerNumber2').val());
        var third = parseInt($('#txtFindBiggerNumber3').val());
        var result = third;
        if (first > second) {
            if (first > third)
                result = first;
        }
        else {
            if (second > third)
                result = second;
        }
        $('#pnlFindBiggerNumber').text(result);
    });

    $('#btnOrderByDescending').click(function () {
        var a = parseInt($('#txtOrderByDescending1').val());
        var b = parseInt($('#txtOrderByDescending2').val());
        var c = parseInt($('#txtOrderByDescending3').val());
        var result;
        if (a > b) {
            if (a > c) {
                result = a;
                if (b > c) {
                    result = result + " " + b;
                    result = result + " " + c;
                }
                else {
                    result = result + " " + c;
                    result = result + " " + b;
                }
            }
            else {
                result = c;
                result = result + " " + a;
                result = result + " " + b;
            }
        }
        else {
            if (b > c) {
                result = b;
                if (a > c) {
                    result = result + " " + a;
                    result = result + " " + c;
                }
                else {
                    result = result + " " + c;
                    result = result + " " + a;
                }
            }
            else {
                result = c;
                result = result + " " + b;
                result = result + " " + a;
            }
        }
        $('#pnlOrderByDescending').text(result);
    });

    $('#btnNameOfDigit').click(function () {
        var source = parseInt($('#txtNameOfDigit').val());
        var result;
        switch (source)
        {
            case 0:
                result = "Zero";
                break;
            case 1:
                result = "One";
                break;
            case 2:
                result = "Two";
                break;
            case 3:
                result = "Three";
                break;
            case 4:
                result = "Four";
                break;
            case 5:
                result = "Five";
                break;
            case 6:
                result = "Six";
                break;
            case 7:
                result = "Seven";
                break;
            case 8:
                result = "Eight";
                break;
            case 9:
                result = "Nine";
                break;
            default:
                result = "Unknown digit!";
                break;
        }
        $('#pnlNameOfDigit').text(result);
    });

    $('#btnSolveQuadratic').click(function () {
        var a = parseInt($('#txtSolveQuadraticA').val());
        var b = parseInt($('#txtSolveQuadraticB').val());
        var c = parseInt($('#txtSolveQuadraticC').val());
        var result;
        //Quadratic Formula: x = (-b +- sqrt(b^2 - 4ac)) / 2a
        var insideSquareRoot = (b * b) - 4 * a * c;
        if (insideSquareRoot < 0)
        {
            result = "No Solution";
        }
        else
        {
            var sqrt = Math.sqrt(insideSquareRoot);
            var x1 = (-b + sqrt) / (2 * a);
            var x2 = (-b - sqrt) / (2 * a);
            if (x1 == x2)
                result = "Only one solution: " + x1;
            else
                result = "2 Solutions: " + x1 + " and " + x2;
        }
        $('#pnlSolveQuadratic').text(result);
    });

    $('#btnFindGreatestNumber').click(function () {
        var source = $('#txtFindGreatestNumber').val();
        var n = source.split(",");
        var result = 0;

        for (var i = 0; i < n.length; i++) {
            if (result == 0)
                result = n[i];
            else
            {
                if (result < n[i])
                    result = n[i];
            }
        }

        $('#pnlFindGreatestNumber').text(result);
    });

    $('#btnConvertNumberstToText').click(function () {
        var source = parseInt($('#txtConvertNumberstToText').val());
        var digits = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirdtheen", "fourthen", "fiftheen", "sixtheen", "seventheen", "eightheen", "ninetheen"];
        var dec = ["and", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"];
        var sb = [];

        var iArray = source.toString().split('');
        switch (iArray.length) {
            case 1:
                sb.push(digits[parseInt(iArray[0])] + " ");
                break;
                case 2:
                    if (source < 20)
                        sb.push(digits[source]);
                    else
                    {
                        switch (parseInt(iArray[source]))
                        {
                            case 0:
                                sb.push(dec[parseInt(iArray[0])] + " ");
                                break;
                            default:
                                sb.push(dec[parseInt(iArray[0])] + "-" + digits[parseInt(iArray[1])] + " ");
                                break;
                        }
                    }
                    break;
                case 3:
                    if (parseInt(iArray[1]) == 0 && parseInt(iArray[2]) == 0)
                        sb.push(digits[parseInt(iArray[0])] + " hundred" + " ");
                    else
                    {
                        for (var j = 0; j < iArray.length; j++)
                        {
                            switch (j)
                            {
                                case 0:
                                    sb.push(digits[parseInt(iArray[j])] + " hundred" + " ");
                                    break;
                                default:
                                    if (j == 1)
                                    {
                                        if (parseInt(iArray[j]) == 0)
                                        {
                                            sb.push(" and " + digits[parseInt(iArray[j + 1])] + " ");
                                        }
                                        else if (parseInt(iArray[j]) == 1)
                                        {
                                            sb.push(" and " + digits[10 + parseInt(iArray[j + 1])] + " ");
                                        }
                                        else if (parseInt(iArray[j + 1]) == 0)
                                        {
                                            sb.push(" " + dec[parseInt(iArray[j])] + " ");
                                        }
                                        else
                                        {
                                            sb.push(" " + dec[parseInt(iArray[j])] + "-" + digits[parseInt(iArray[j + 1])] + " ");
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    break;
            default:
                break;
        }
        $('#pnlConvertNumberstToText').text(sb.toString());
    });

});