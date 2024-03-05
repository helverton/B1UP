//PrintOrder
//Relatório SQL
//MS SQL
//CTE

WITH ORD AS
(
	SELECT CAST(T0.DocEntry AS CHAR(15)) [DocEntry], 
		   T0.CardName, 
		   T2.Name [City], 
		   CONVERT(CHAR(10),GETDATE(),103) [Date], 
		   T4.CardName [Carrier], 
		   T0.LicTradNum [TaxId]
	FROM ORDR T0
	LEFT JOIN RDR12 T1 ON T0.DocEntry = T1.DocEntry
	LEFT JOIN OCNT T2 ON T1.County = T2.AbsId
	LEFT JOIN OCRD T3 ON T0.CardCode = T3.CardCode
	LEFT JOIN OCRD T4 ON ISNULL(T1.Carrier, T3.U_EASY_Trsp) = T4.CardCode
	WHERE T0.DocEntry = $[$8.0.0]
), RDR AS
(
	SELECT CAST(T1.ItemCode AS CHAR(20)) [ItemCode], 
		   T1.ItemName, 
		   CAST(T1.U_QntPack AS CHAR(20)) [QntPack], 
		   CAST(CAST(T0.Quantity AS INT) AS CHAR(20)) [Quantity], 
		   REPLACE(CAST(CAST(T0.Quantity/ISNULL(T1.U_QntPack, 1) AS NUMERIC(16,5)) AS CHAR(20)),'.',',') [Pack] 
	FROM RDR1 T0
	LEFT JOIN OITM T1 ON T0.ItemCode = T1.ItemCode
	WHERE T0.DocEntry = $[$8.0.0]
), VRF AS
(
	SELECT ItemCode, '#' [Repeat] 
	FROM RDR
	GROUP BY ItemCode
	HAVING COUNT(*)>1
), RPT AS
(
	SELECT 'Cliente' [1], T0.CardName [2], 'Número' [3], T0.DocEntry [4], '' [5], 1 [6], '' [7] FROM ORD T0
	UNION ALL
	SELECT 'Cidade' [1], T0.City [2], 'Data' [3], T0.Date [4], '' [5], 2 [6], '' [7] FROM ORD T0
	UNION ALL
	SELECT 'Transp.' [1], T0.Carrier [2], 'Volumes' [3], '' [4], '' [5], 3 [6], '' [7] FROM ORD T0
	UNION ALL
	SELECT '' [1], '' [2], '' [3], '' [4], '' [5], 4 [6], '' [7] FROM ORD T0
	UNION ALL
	SELECT 'CNPJ' [1], T0.TaxId [2], '' [3], '' [4], '' [5], 5 [6], '' [7] FROM ORD T0
	UNION ALL
	SELECT '' [1], '' [2], '' [3], '' [4], '' [5], 6 [6], '' [7] FROM ORD T0
	UNION ALL
	SELECT 'quant/cx' [1], 'produto' [2], 'código' [3], 'unitário' [4], 'caixas' [5], 7 [6], '' [7] FROM ORD T0
	UNION ALL
	SELECT T0.QntPack [1], T0.ItemName [2], T0.ItemCode [3], T0.Quantity [4], T0.Pack [5], 8 [6], '' [7] FROM RDR T0
)
SELECT [1], [2], [3], [4], [5], [Repeat][7] 
FROM RPT
LEFT JOIN VRF ON RPT.[3] = VRF.ItemCode
ORDER BY [6], [3]