//Update field in business partner
//Código dinâmico (.Net SDK)
//C#

Recordset oRecordset = (Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);
BusinessPartners oBP = (BusinessPartners)company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
string query = "";

query = @"SELECT T1.CardCode
          FROM OCRD T1 
          WHERE T1.SlpCode = 10";
oRecordset.DoQuery(query);
while (!oRecordset.EoF)
{
    if (oBP.GetByKey(Convert.ToString(oRecordset.Fields.Item("CardCode").Value)))
    {
        oBP.Territory = "BRA";
    }
    oBP.Update();
    
    oRecordset.MoveNext();
}