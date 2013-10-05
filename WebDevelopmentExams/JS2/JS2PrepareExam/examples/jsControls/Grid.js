/******************************************************************************************
 * Developed by Shawn Lawsure - shawnl@maine.rr.com - http://www.linkedin.com/in/shawnlawsure

 * The Grid class supports the following functionality:
        -> Use JSON object as the data source.
        -> Row selection.
        -> Row identifier (i.e. data key name).
        -> Sorting by clicking on the column header (not applicable when grouping).
        -> Alternate row display.
        -> Scroll bars.
        -> Group/display by a given group field (see below).

 * Future potential enhancements:
        -> Paging
        -> Fixed-header when scrolling.

 * Example of use:

    $(document).ready(function ()
    {
         var myGrid;
         var testData = [
                { "Id": 22, "make": "Honda", "model": "CR-V", "year": "1998" },
                { "Id": 23, "make": "Toyota", "model": "Sienna", "year": "2005" },
                { "Id": 24, "make": "Nissan", "model": "Sentra", "year": "2001" },
                { "Id": 25, "make": "Toyota", "model": "Corolla", "year": "2011" },
                { "Id": 26, "make": "Ford", "model": "Focus", "year": "2008" },
                { "Id": 27, "make": "Dodge", "model": "Charger", "year": "2004" },
                { "Id": 28, "make": "Ford", "model": "Fiesta", "year": "2013" },
                { "Id": 29, "make": "Toyota", "model": "Camry", "year": "2008" },
                { "Id": 30, "make": "Dodge", "model": "Durango", "year": "2004"}];
        myGrid = new Grid('myGrid', testData);
        myGrid.bindFields =
                [
                    { "FieldName": "make", "Header": "Make", "Width": 150 },
                    { "FieldName": "model", "Header": "Model", "Width": 150 },
                    { "FieldName": "year", "Header": "Year", "Width": 50 }
                ];
        myGrid.dataKeyName = 'Id';
        myGrid.altRowClassName = "alt";

        myGrid.Build();
    });
******************************************************************************************/

function Grid(tableElementName, data)
{
    /*=============================================================================
    Public Properties
    =============================================================================*/

    // gridName is the id of the table HTML element that will be used as the top-level UI 
    // element.  This is required and is passed in via the constructor.
    this.gridName = tableElementName;

    // data is the JSON object that will act as the data source.  This is required and is 
    // passed in via the constructor.
    this.data = data;

    // The bindFields property is required and must contain FieldName and Header values for each
    // column you want displayed in the grid where FieldName is the corresponding field in
    // the JSON object data source (above) and Header is the title you want displayed as the 
    // column header for that column.  The Width field is not necessary but it helps to lineup 
    // the columns when grouping rows (see below).
    this.bindFields = null;

    // The dataKeyName property is the name of the field in the JSON object that contains the
    // identifying key for each row in the data source (e.g. primary key value).  This is
    // required if you want to be able to get the key for the selected row.
    this.dataKeyName = '';

    // IsSelectable indicates that the user can select a row.  The row will be highlighted and
    // the 'selected' attribute will be set for that row.  Default is true;
    this.IsSelectable = true;

    // IsSortable indicates that the rows can be sorted by clicking on the column headers (both
    // ascending and descending).  This is not applicable when grouping by rows (see below). 
    // Default is true.
    this.IsSortable = true;

    // Set ScrollBars to true if the grid should use a vertical scrollbar when it reaches a
    // maximum height, as defined by the MaximumHeight property.
    this.ScrollBars = false;
    this.MaximumHeight = 300;

    // Set the groupedByKeyName to have the grid be grouped by the given JSON object field from
    // the data source and allow the user to show and collapse the grouped rows.  When first
    // displaying the screen only the unique values for the groupedByKeyName field will be
    // shown in the grid.  When the user clicks on one of these rows the grouped rows will be
    // displayed below the clicked row.
    this.groupedByKeyName = '';

    // Set altRowClassName to the CSS class to use for displaying alternate rows differently.
    this.altRowClassName = '';

    // Set groupRowClassName to the CSS class to use for displaying the grouped rows differently.
    this.groupRowClassName = '';

    // When grouping set the groupExpandImage to true if you want to display an collapse/expand image.
    this.groupExpandImage = false;
    this.groupRowHighlight = '';

    /*=============================================================================
    Private Properties
    =============================================================================*/
    var gridSelf = this;
    var grid = document.getElementById(this.gridName);
    var sortOrder = "";
    var groupedFieldCellIndex = 0;

  /*=============================================================================
    Public Methods
    =============================================================================*/

    // Call Build to create the grid after setting the desired properties.
    this.Build = function () { Build(); }

    // Call selectedKey to get the key value for the selected row.
    this.selectedKey = function ()
    {
        if (!gridSelf.dataKeyName || gridSelf.dataKeyName == '')
            throw new Error("The key field must be set when using the 'selectedKey' function.");

        var rows = grid.getElementsByTagName("tr");
        for (var index = 0; index < rows.length; index++)
        {
            if (rows[index].attributes['selected'])
                if (rows[index].attributes['selected'].value == 'true')
                    return rows[index].cells[gridSelf.dataKeyName].innerHTML;
        }
        return -1;
    }

    // Call selectedRowIndex to get the index of the selected row.
    this.selectedRowIndex = function ()
    {
        var rows = grid.getElementsByTagName("tr");
        for (var index = 0; index < rows.length; index++)
        {
            if (rows[index].attributes['selected'])
                if (rows[index].attributes['selected'].value == 'true')
                    return index - 1;       // Ignore the header row.
        }
        return -1;
    }

  /*=============================================================================
    Private Methods
    =============================================================================*/

    function rebuild()
    {
        var length = grid.childNodes.length;
        for (var index = 0; index < length; index++)
            grid.removeChild(grid.childNodes[0]);
        Build();
    }

    function rowClickHandler()
    {
        var rows = grid.getElementsByTagName("tr");
        for (var index = 0; index < rows.length; index++)
        {
            rows[index].setAttribute('selected', 'false');

            if (index % 2 == 1)
                rows[index].className = gridSelf.altRowClassName;
            else
                rows[index].className = "";
        }

        this.setAttribute('selected', 'true');
        this.className = "selected";
    }

    function headerClickHandler(sender)
    {
        if (sortOrder == 'asc')
            sortOrder = 'desc';
        else
            sortOrder = 'asc';

        sortObjectsByKey(data, this.id, sortOrder);

        rebuild();
    }

    function groupRowClick() 
	{
		var expand = true;
        if (this.parentElement && this.parentElement.nextSibling)
        {
            var nextBody = this.parentElement.nextSibling;
            if (nextBody.style.display == "none") 
			{
				expand = true;
				nextBody.style.display = "";
				if (gridSelf.groupRowHighlight != '')
				    this.className = gridSelf.groupRowHighlight;
			}
            else 
			{
               	expand = false;
               	nextBody.style.display = "none";
               	if (gridSelf.groupRowHighlight != '')
               	    this.className = (this.tabIndex % 2 == 1 ? gridSelf.altRowClassName : '');
           }
        }

        if (gridSelf.groupExpandImage) 
		{
		    var images = this.cells[0].getElementsByTagName('img');
			if (expand) 
			{
			    images[1].style.display = 'inline';
			    images[0].style.display = 'none';
			}
			else 
			{
			    images[1].style.display = 'none';
			    images[0].style.display = 'inline';
			}
		}
    }

    function Build()
    {
        if (!grid)
            return;

        if (gridSelf.groupedByKeyName)
            sortObjectsByKey(data, gridSelf.groupedByKeyName, "asc");

        var tbody = grid.getElementsByTagName('tbody')[0];
        if (!tbody)
            tbody = document.createElement('tbody');

        // Header row
        var headerRow = document.createElement('tr');

        // Id column
        if (gridSelf.dataKeyName != '')
        {
            var thId = document.createElement('th');
            thId.id = gridSelf.dataKeyName;
            thId.style.display = 'none';
            headerRow.appendChild(thId);
        }

        // Collapse/expand column for grouping
        if (gridSelf.groupExpandImage)
        {
            var thCE = document.createElement('th');
            thCE.style.width = '17px';
            headerRow.appendChild(thCE);
        }

        for (var key in gridSelf.bindFields)
        {
            var fieldName = gridSelf.bindFields[key].FieldName;
            var header = gridSelf.bindFields[key].Header;

            var th = document.createElement('th');
            th.id = fieldName;
            th.innerHTML = header;

            if (gridSelf.IsSortable && gridSelf.groupedByKeyName == '')
                th.onclick = headerClickHandler;

            if (fieldName == gridSelf.dataKeyName)
                th.style.display = 'none';
            else if (gridSelf.bindFields[key].Width)
                th.style.width = gridSelf.bindFields[key].Width + "px";

            headerRow.appendChild(th);
        }

        tbody.appendChild(headerRow);

        var altClass = '';
        var groupIndentWidth = 0;

        grid.appendChild(tbody);

        // Item rows
        var currenttbody = null;
        var groupRowIndex = 0;
        var inGroup = false;
        for (var index = 0; index < data.length; index++)
        {
            // The following logic is used when grouping rows.
            if (gridSelf.groupedByKeyName && gridSelf.groupedByKeyName != '')
            {
                if (index == 0 || CleanGroupField(data[index - 1][gridSelf.groupedByKeyName].toUpperCase()) != CleanGroupField(data[index][gridSelf.groupedByKeyName].toUpperCase()))
                {
                    groupRowIndex++;
                    altClass = (groupRowIndex % 2 == 1 ? gridSelf.altRowClassName : '');

                    currenttbody = document.createElement('tbody');
                    grid.appendChild(currenttbody);

                    if (index == data.length - 1 || CleanGroupField(data[index][gridSelf.groupedByKeyName].toUpperCase()) != CleanGroupField(data[index + 1][gridSelf.groupedByKeyName].toUpperCase()))
                        inGroup = false;
                    else
                    {
                        inGroup = true;
                        var numColumns = 0;
                        var groupRow = document.createElement('tr');
                        groupRow.onclick = groupRowClick;
                        groupRow.tabIndex = groupRowIndex;
                        var width = 0;

                        if (gridSelf.groupExpandImage)
                        {
                            var tdExp = document.createElement('td');
                            tdExp.style.width = '17px'

                            var expandImage = document.createElement('img');
                            expandImage.setAttribute('src', 'expand.png');
                            var collapseImage = document.createElement('img');
                            collapseImage.setAttribute('src', 'collapse.png');
                            collapseImage.style.display = 'none';

                            tdExp.appendChild(expandImage);
                            tdExp.appendChild(collapseImage);
                            groupRow.appendChild(tdExp);
                        }
                        groupRow.style.cursor = "pointer";
                        groupRow.className = gridSelf.groupRowClassName;
                        groupRow.className += " " + altClass;

                        for (var key in gridSelf.bindFields)
                        {
                            var fieldName = gridSelf.bindFields[key].FieldName;

                            var td = document.createElement('td');
                            if (fieldName == gridSelf.groupedByKeyName)
                            {
                                width = gridSelf.bindFields[key].Width;
                                td.innerHTML = data[index][fieldName];
                                td.style.width = width + "px";
                            }

                            if (fieldName == gridSelf.dataKeyName)
                                td.style.display = 'none';
                            else
                                numColumns++;

                            groupRow.appendChild(td);
                        }
                        currenttbody.appendChild(groupRow);

                        currenttbody = document.createElement('tbody');
                        currenttbody.style.display = "none";
                        grid.appendChild(currenttbody);

                    }
                }
            }
            else if (currenttbody == null)
            {
                currenttbody = document.createElement('tbody');
                grid.appendChild(currenttbody);
            }
            // Item row
            var row = document.createElement('tr');
            row.tabIndex = groupRowIndex;

            var tdCEPlaceholder = document.createElement('td');
            tdCEPlaceholder.style.width = '17px';
            row.appendChild(tdCEPlaceholder);

            // Id column
            if (gridSelf.dataKeyName != '')
            {
                var tdId = document.createElement('td');
                tdId.id = gridSelf.dataKeyName;
                tdId.style.display = 'none';
                tdId.innerHTML = data[index][gridSelf.dataKeyName];
                row.appendChild(tdId);
            }

            for (var key in gridSelf.bindFields)
            {
                var fieldName = gridSelf.bindFields[key].FieldName;

                var td = document.createElement('td');
                td.id = fieldName;
                td.innerHTML = data[index][fieldName] ? data[index][fieldName] : '';

                if (fieldName == gridSelf.dataKeyName)
                    td.style.display = 'none';
                else if (gridSelf.bindFields[key].Width)
                    td.style.width = gridSelf.bindFields[key].Width + "px";

                row.appendChild(td);
            }

            if (gridSelf.IsSelectable)
                row.onclick = rowClickHandler;

            row.className = inGroup ? gridSelf.groupRowHighlight : altClass;

            currenttbody.appendChild(row);
        }

        if (gridSelf.ScrollBars && gridSelf.MaximumHeight > 0)
        {
            var scrollDiv = document.createElement('div');
            scrollDiv.style.overflow = "auto";
            scrollDiv.style.maxHeight = gridSelf.MaximumHeight + "px";

            grid.parentElement.replaceChild(scrollDiv, grid);
            scrollDiv.appendChild(grid);
        }
        grid.style.cursor = gridSelf.IsSelectable ? "pointer" : "";
    }

}  // End of class

function CleanGroupField(fieldValue)
{
    var returnVal = fieldValue.replace('USA', '').replace('UNITED STATES', '').replace('US', '');
    return returnVal;
}

/*=============================================================================
Helper Methods
=============================================================================*/
function sortObjectsByKey(objects, key, sortOrder)
{
    objects.sort(function ()
    {
        return function (a, b)
        {
            var objectIDA = CleanGroupField(a[key].toUpperCase());
            var objectIDB = CleanGroupField(b[key].toUpperCase());
            if (objectIDA === objectIDB)
                return 0;
            if (sortOrder == 'asc')
                return objectIDA > objectIDB ? 1 : -1;
            else
                return objectIDA < objectIDB ? 1 : -1;
        };
    } ()); 
} 