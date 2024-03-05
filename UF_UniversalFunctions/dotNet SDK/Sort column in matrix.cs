//Sort column in matrix
//Código dinâmico (.Net SDK)
//C#

var pVal = eventData.ExtendedEventInformation as ItemEvent;
EditText sort = (EditText)form.Items.Item("BOYX_1").Specific;

if (!pVal.ColUID.Equals("2000"))
{
    Matrix oMatrix = (Matrix)form.Items.Item("2003").Specific;
    Column oColumn = oMatrix.Columns.Item(pVal.ColUID);
    if (sort.Value.Equals("Y") || sort.Value.Equals(""))
    {
        oColumn.TitleObject.Sort(SAPbouiCOM.BoGridSortType.gst_Ascending);
        sort.Value = "N";
    }
    else
    {
        oColumn.TitleObject.Sort(SAPbouiCOM.BoGridSortType.gst_Descending);
        sort.Value = "Y";
    }
}