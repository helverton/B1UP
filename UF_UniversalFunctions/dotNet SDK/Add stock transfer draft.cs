//Add stock transfer draft
//Código dinâmico (.Net SDK)
//C#

EditText sDocEntry = (EditText)form.Items.Item("8").Specific;
int docEntry = int.Parse(sDocEntry.Value.ToString());

ComboBox sWhsCode = (ComboBox)form.Items.Item("BOYX_1").Specific;
string whsCode = sWhsCode.Value.ToString();


try
{
    if (!string.IsNullOrEmpty(whsCode))
    {
        application.StatusBar.SetText("Criação de esboço de transferência em andamento...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

        bool first = true;
        StockTransfer oStockTrnsfrDraft = (StockTransfer)company.GetBusinessObject(BoObjectTypes.oStockTransferDraft);
        oStockTrnsfrDraft.DocObjectCode = BoObjectTypes.oStockTransfer;

        Recordset oRecordset = (Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
        string query = @"SELECT ORIN.DocDate, ORIN.BPLId, RIN1.ItemCode, RIN1.Quantity, RIN1.WhsCode 
                         FROM ORIN
                         INNER JOIN RIN1 ON ORIN.DocEntry = RIN1.DocEntry " +
                        "WHERE ORIN.DocEntry = " + docEntry.ToString() + " ORDER BY RIN1.VisOrder";

        oRecordset.DoQuery(query);
        while (!oRecordset.EoF)
        {
            application.StatusBar.SetText("Criação de esboço de transferência em andamento...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);

            if (first)
            {
                oStockTrnsfrDraft.DocDate = DateTime.Now;//DateTime.Parse(oRecordset.Fields.Item("DocDate").Value.ToString());
                oStockTrnsfrDraft.TaxDate = DateTime.Now;//DateTime.Parse(oRecordset.Fields.Item("DocDate").Value.ToString());
                oStockTrnsfrDraft.FromWarehouse = oRecordset.Fields.Item("WhsCode").Value.ToString();
                oStockTrnsfrDraft.ToWarehouse = whsCode;

                oStockTrnsfrDraft.JournalMemo = "Transferências de estoque";
                oStockTrnsfrDraft.Comments = "Transferência doc base Dev. NFe Saída Primário: " + docEntry.ToString();

                first = false;
            }
            else
            {
                oStockTrnsfrDraft.Lines.Add();
            }

            oStockTrnsfrDraft.Lines.ItemCode = oRecordset.Fields.Item("ItemCode").Value.ToString();
            oStockTrnsfrDraft.Lines.Quantity = int.Parse(oRecordset.Fields.Item("Quantity").Value.ToString());
            

            oRecordset.MoveNext();
        }

        int log = oStockTrnsfrDraft.Add();
        if (log != 0)
        {
            application.StatusBar.SetText(company.GetLastErrorDescription(), SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
        }
        else 
        {
            application.StatusBar.SetText("Esboço de transferência criado com sucesso!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
        }
    }
    else 
    {
        application.StatusBar.SetText("Selecione um depósito no campo 'Para depósito'!", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
    }
}
catch (Exception e)
{
    application.StatusBar.SetText(company.GetLastErrorDescription() + "  " + e.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
}