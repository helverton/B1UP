//Force update and reload form
//Macro
//MS SQL

StatusBar(Processo em andamento...|W);
ExecuteSQL(UPDATE OCRD SET U_ACT_UField = "New Value" WHERE OCRD.CardCode = $[$5.0.0]);
StatusBar(Processo finalizado!|S);
Activate(1304);