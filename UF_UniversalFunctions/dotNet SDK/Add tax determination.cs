//Add tax determination
//Código dinâmico (.Net SDK)
//C#

//SQL VIEW: ACT_VW_ListTaxDetermination
//COLUMNS 
// A	|B	|C	|D	        |E	            |G	|H	|I
// 5	|BA	|	|3305.30.00	|CF S/I.E  REG2	|	|	|20230201


try
{
    #region Global variable
    #endregion

    #region Properties
    Recordset oRecordset = (Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
    string query = "SELECT DISTINCT A FROM StartAct..ACT_VW_ListTaxDetermination";
    oRecordset.DoQuery(query);
    while (!oRecordset.EoF)
    {
        //TELA 1 //TELA 1 //TELA 1 //TELA 1
        form = application.Forms.ActiveForm;

        Matrix mt1 = (Matrix)form.Items.Item("2003").Specific;

        application.StatusBar.SetText("seq" + oRecordset.Fields.Item("A").Value.ToString(), BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Warning);

        Cell oCell1 = mt1.Columns.Item("2000").Cells.Item(int.Parse(oRecordset.Fields.Item("A").Value.ToString()));
        oCell1.Click(BoCellClickType.ct_Double);



        //TELA 2 //TELA 2 //TELA 2 //TELA 2
        form = application.Forms.ActiveForm;
        Matrix mt2 = (Matrix)form.Items.Item("2003").Specific;


        Recordset oRecordset1 = (Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
        string query1 = "SELECT DISTINCT A, B, C, D, E, G, H, I FROM StartAct..ACT_VW_ListTaxDetermination";

        oRecordset1.DoQuery(query1);
        while (!oRecordset1.EoF)
        {
            application.StatusBar.SetText(mt2.RowCount.ToString(), BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Warning);

            ((EditText)mt2.Columns.Item("V_1").Cells.Item(mt2.RowCount).Specific).Value = oRecordset1.Fields.Item("B").Value.ToString();
            ((EditText)mt2.Columns.Item("V_4").Cells.Item(mt2.RowCount).Specific).Value = oRecordset1.Fields.Item("D").Value.ToString();
            ((EditText)mt2.Columns.Item("V_7").Cells.Item(mt2.RowCount).Specific).Value = oRecordset1.Fields.Item("E").Value.ToString();

            Cell oCell2 = mt2.Columns.Item("2000").Cells.Item(mt2.RowCount);
            oCell2.Click(BoCellClickType.ct_Double);

            //TELA 3 //TELA 3 //TELA 3 //TELA 3
            form = application.Forms.ActiveForm;
            Matrix mt3 = (Matrix)form.Items.Item("2003").Specific;

            ((EditText)mt3.Columns.Item("2001").Cells.Item(1).Specific).Value = oRecordset1.Fields.Item("I").Value.ToString();

            Cell oCell3 = mt3.Columns.Item("2000").Cells.Item(1);
            oCell3.Click(BoCellClickType.ct_Double);


            //TELA 4 //TELA 4 //TELA 4 //TELA 4
            Recordset oRecordset2 = (Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string query2 = "SELECT F, J FROM StartAct..ACT_VW_ListTaxDetermination " +
                            "WHERE A = '" + oRecordset.Fields.Item("A").Value.ToString() + "' AND " +
                            "B = '" + oRecordset1.Fields.Item("B").Value.ToString() + "' AND " +
                            "D = '" + oRecordset1.Fields.Item("D").Value.ToString() + "' AND " +
                            "E = '" + oRecordset1.Fields.Item("E").Value.ToString() + "'";

            oRecordset2.DoQuery(query2);
            while (!oRecordset2.EoF)
            {
                form = application.Forms.ActiveForm;
                Matrix mt4 = (Matrix)form.Items.Item("2002").Specific;

                int g = 0;
                if (oRecordset2.Fields.Item("F").Value.Equals("Venda")) g = 5;
                if (oRecordset2.Fields.Item("F").Value.Equals("Bonificação")) g = 20;


                ((EditText)mt4.Columns.Item("256000005").Cells.Item(g).Specific).Value = oRecordset2.Fields.Item("J").Value.ToString();

                ((EditText)mt4.Columns.Item("2002").Cells.Item(g).Specific).Value = oRecordset2.Fields.Item("J").Value.ToString();
                ((EditText)mt4.Columns.Item("140002005").Cells.Item(g).Specific).Value = oRecordset2.Fields.Item("J").Value.ToString();

                oRecordset2.MoveNext();
            }
            
            ((SAPbouiCOM.Button)form.Items.Item("2000").Specific).Item.Click();
            ((SAPbouiCOM.Button)form.Items.Item("2000").Specific).Item.Click();


            //TELA 3 //TELA 3 //TELA 3 //TELA 3
            form = application.Forms.ActiveForm;
            ((SAPbouiCOM.Button)form.Items.Item("2000").Specific).Item.Click();
            ((SAPbouiCOM.Button)form.Items.Item("2000").Specific).Item.Click();


            //TELA 2 //TELA 2 //TELA 2 //TELA 2
            form = application.Forms.ActiveForm;
            ((SAPbouiCOM.Button)form.Items.Item("2000").Specific).Item.Click();

            oRecordset1.MoveNext();
        }

        //TELA 2 //TELA 2 //TELA 2 //TELA 2
        form = application.Forms.ActiveForm;

        oRecordset.MoveNext();
    }
    #endregion


    application.StatusBar.SetText("FIM!", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Warning);
}
catch (Exception e)
{
    application.StatusBar.SetText(e.Message, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
    throw;
}