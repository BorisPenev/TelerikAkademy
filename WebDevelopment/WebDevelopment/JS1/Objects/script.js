$(document).ready(function () {

    $('#btnShape').click(function () {
        var x1 = parseInt($('#txtShapeX1').val());
        var x2 = parseInt($('#txtShapeX2').val());
        var x3 = parseInt($('#txtShapeX3').val());
        var y1 = parseInt($('#txtShapeY1').val());
        var y2 = parseInt($('#txtShapeY2').val());
        var y3 = parseInt($('#txtShapeY3').val());

        var p1 = new Point(x1, y1);
        var p2 = new Point(x2, y2);
        var p3 = new Point(x3, y3);

        var result = [];
        result.push("distance between point1 and point2 is: " + p1.GetDistance(p2));
        result.push("distance between point2 and point3 is: " + p2.GetDistance(p3));
        result.push("distance between point3 and point1 is: " + p3.GetDistance(p1));

        result.push(IsTriangle(new Line(p1, p2), new Line(p2, p3), new Line(p3, p1)));
        
        $('#pnlShape').text(result.toString());
    });

    function Point(x, y) {
        this.X = x;
        this.Y = y;
        this.GetDistance = function (p) {
            return Math.sqrt(Math.pow(Math.abs(this.X - p.X), 2) + Math.pow(Math.abs(this.Y - p.Y), 2));
        }
    }

    function Line(startPoint, endPoint) {
        this.Start = startPoint;
        this.End = endPoint;
        this.Length = this.Start.GetDistance(this.End);
    }

    function IsTriangle(line1, line2, line3) {
        var a;
        var b;
        var c;
        if (line1.Length > line2.Length) {
            if (line1.Length > line3.Length) {
                a = line1.Length;
                if (line2.Length > line3.Length) {
                    b = line2.Length;
                    c = line3.Length;
                }
                else {
                    c = line2.Length;
                    b = line3.Length;
                }
            }
            else {
                a = line3.Length;
                b = line1.Length;
                c = line2.Length;
            }
        }
        else {
            if (line2.Length > line3.Length) {
                a = line2.Length;
                if (line1.Length > line3.Length) {
                    b = line1.Length;
                    c = line3.Length;
                }
                else {
                    b = line3.Length;
                    c = line1.Length;
                }
            }
            else {
                a = line3.Length;
                b = line2.Length;
                c = line1.Length;
            }
        }
        return (b + c > a);
    }

    $('#btnRemoveValue').click(function () {
        var arr = [1, 2, 1, 4, 1, 3, 4, 1, 111, 3, 2, 1, "1"];
        arr.remove(1); //arr = [2,4,3,4,111,3,2,"1"];
        $('#pnlRemoveValue').text(arr.toString());
    });

    Array.prototype.remove = function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) {
                this.splice(i, 1);
            }
        }
    }

    $('#btnDeepCopy').click(function () {
        var arr = [1, 2, 1, 4, 1, 3, 4, 1, 111, 3, 2, 1, "1"];
        var arrCopy = deepCopy(arr);
        arrCopy.remove(2);
        $('#pnlDeepCopy').text(arrCopy.toString());
    });

    function deepCopy(obj) {
        if (Object.prototype.toString.call(obj) === '[object Array]') {
            var out = [], i = 0, len = obj.length;
            for (; i < len; i++) {
                out[i] = arguments.callee(obj[i]);
            }
            return out;
        }
        if (typeof obj === 'object') {
            var out = {}, i;
            for (i in obj) {
                out[i] = arguments.callee(obj[i]);
            }
            return out;
        }
        return obj;
    }

    $('#btnHasOwnProperty').click(function () {
        var arr = [1, 2, 1, 4, 1, 3, 4, 1, 111, 3, 2, 1, "1"];
        $('#pnlHasOwnProperty').text(hasProperty(arr, 'length'));
    });

    function hasProperty(obj, property){
        return obj.hasOwnProperty(property)
    }
    
    $('#btnFindYoungestPerson').click(function () {
        var persons = [
          { firstname : "Gosho", lastname: "Petrov", age: 32 },
          { firstname: "Bay", lastname: "Ivan", age: 81 },
          { firstname: "Pesho", lastname: "Peshev", age: 22 }];
        var result;
        for (var p in persons) {
            if (result == undefined)
                result = persons[p];
            else if(result.age > persons[p].age)
                result = persons[p];
        }
        $('#pnlFindYoungestPerson').text(result.age);
    });

    $('#btnGroupBy').click(function () {
        var result = [];
        var persons = [
          { firstname: "Gosho", lastname: "Petrov", age: 32 },
          { firstname: "Bay", lastname: "Ivan", age: 32 },
          { firstname: "Gosho", lastname: "Peshev", age: 22 }];

        var groupedByFname = groupBy(persons, function (obj) {
            return obj.firstname;
        });

        for (var name in groupedByFname) {
            result.push("Name: " + name + " count: " + groupedByFname[name].length)
        }

        var groupedByAge = groupBy(persons, function (obj) {
            return obj.age;
        });

        for (var age in groupedByAge) {
            result.push("Age: " + age + " count: " + groupedByAge[age].length)
        }

        $('#pnlGroupBy').text(result.toString());
    });

    var groupBy = function (array, predicate) {
        var grouped = {};
        for (var i = 0; i < array.length; i++) {
            var groupKey = predicate(array[i]);
            if (typeof (grouped[groupKey]) === "undefined")
                grouped[groupKey] = [];
            grouped[groupKey].push(array[i]);
        }

        return grouped;
    }

});