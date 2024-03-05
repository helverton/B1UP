//Stock check report
//Relatório SQL
//Parâmetros: %0[OWHS|Depósito];
//Destino dos dados selec.: T0.[Warehouse]
//MS SQL

SELECT T0.DocDate 'Data de lançamento', 
       CASE T0.[TransType] WHEN 18 THEN 'NFE' WHEN '13' THEN 'NFS' WHEN '19' THEN 'DVNFE' WHEN '14' THEN 'DVNFS' WHEN '67' THEN 'TRNS' ELSE '???' END 'Documento', 
	   ISNULL(T12.DocEntry, ISNULL(T11.DocEntry, ISNULL(T10.DocEntry, ISNULL(T8.DocEntry, T9.DocEntry)))) 'N° SAP', 
	   ISNULL(T11.Serial, ISNULL(T10.Serial, ISNULL(T8.Serial, T9.Serial))) 'N° NF', 
       CASE ISNULL(T8.U_IBG_CrossDocking, T9.U_IBG_CrossDocking) WHEN 'Y' THEN 'Sim' ELSE 'Não' END  'Cross Docking',
       T0.[Warehouse] 'Depósito', T1.[ItemCode] 'Item Código', T1.[ItemName] 'Item Descrição', T0.[InQty] + T0.[OutQty] 'Quantidade'
FROM [OINM] T0  
INNER JOIN OITM T1 ON T1.[ItemCode] = T0.[ItemCode]    
LEFT JOIN OIVK T2 ON T2.[INMTransSe] = T0.[TransSeq]    
LEFT JOIN OIVL T3 ON T3.[TransSeq] = T2.[TransSeq]     
LEFT JOIN ONCM T6 ON T6.[AbsEntry] = T1.[NCMCode]    
LEFT JOIN OINV T8 ON T0.TransType = T8.ObjType AND T0.CreatedBy = T8.DocEntry
LEFT JOIN OPCH T9 ON T0.TransType = T9.ObjType AND T0.CreatedBy = T9.DocEntry
LEFT JOIN ORIN T10 ON T0.TransType = T10.ObjType AND T0.CreatedBy = T10.DocEntry
LEFT JOIN ORPC T11 ON T0.TransType = T11.ObjType AND T0.CreatedBy = T11.DocEntry
LEFT JOIN OWTR T12 ON T0.TransType = T12.ObjType AND T0.CreatedBy = T12.DocEntry
WHERE T0.[DocDate] >= (REPLACE($[$8.0.DATE], '-','')) AND T0.[DocDate] <= (REPLACE($[$7.0.DATE], '-','')) 
      AND T0.[TransValue] <> 0 AND T0.TransType IN('13', '18', '19', '67') 
      AND T0.[Warehouse] IN('01', '04', '10', '14', '17')  
      AND T0.ItemCode = CASE WHEN $[$11.0.0] = '' THEN T0.ItemCode ELSE $[$11.0.0] END
ORDER BY T0.DocDate, T0.[TransSeq]