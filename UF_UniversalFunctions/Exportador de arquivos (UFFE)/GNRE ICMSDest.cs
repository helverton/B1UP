//GNRE ICMSDest
//Exportador de arquivos (UFFE)
/Tipo de exportação: Único


//MS SQL
//BODY SQL SOURCE
SELECT T0.Serial, (SELECT TOP 1 CFOPCode FROM INV1 WHERE DocEntry = T0.DocEntry) 'CFOPCode', 
       (SELECT TOP 1 U_ChaveAcesso FROM [@SKL25NFE] WHERE U_inStatus = 3 AND U_DocEntry = T0.DocEntry) 'Chave',
       CONVERT(VARCHAR,CONVERT(NUMERIC(19,2), SUM(T1.TaxSum))) 'FCP', CONVERT(VARCHAR,CONVERT(NUMERIC(19,2), SUM(T2.TaxSum))) 'ICMSDest', 
	   FORMAT(GETDATE(), 'yyyy-MM-dd') 'ToDayDate', 
	   RIGHT(CONCAT('0', MONTH(GETDATE())),2) 'ToDayM',
	   YEAR(GETDATE()) 'ToDayY',  
       T0.CardName, 
	   
	   CASE T3.StateS 
		    WHEN 'AL' THEN 65
			WHEN 'BA' THEN 86
			WHEN 'CE' THEN 86
			WHEN 'MA' THEN 94
			WHEN 'PB' THEN 99
			WHEN 'PE' THEN 92
			WHEN 'PI' THEN 86
			WHEN 'RN' THEN 97
			WHEN 'SE' THEN 77
            WHEN 'AC' THEN 76
			WHEN 'AM' THEN 12
			WHEN 'AP' THEN 47
            WHEN 'RO' THEN 83
			WHEN 'RR' THEN 36
			WHEN 'TO' THEN 80
			WHEN 'SC' THEN 84
	   END 'Code',

	   CASE WHEN LEN(REPLACE(REPLACE(REPLACE(T5.LicTradNum, '.', ''), '/', ''), '-', '')) <= 11 THEN '2' ELSE '1' END 'DocId',
	   CASE WHEN LEN(REPLACE(REPLACE(REPLACE(T5.LicTradNum, '.', ''), '/', ''), '-', '')) <= 11 THEN 'CPF' ELSE 'CNPJ' END 'DocType',
	   REPLACE(REPLACE(REPLACE(T5.LicTradNum, '.', ''), '/', ''), '-', '') LicTradNum, 
	   (SELECT TOP 1 CASE WHEN ISNULL(TaxId1, '') <> '' AND ISNULL(TaxId1, '') <> 'ISENTO' THEN TaxId1 ELSE '00' END  FROM CRD7 WHERE ISNULL(TaxId1, '') <> '' AND CardCode = T0.CardCode) 'IE',
	   T3.StateS, RIGHT(T7.IbgeCode, 5) 'MunD', T5.U_SKILL_indIEDest 'indIEDest',
       T4.BPLName, '4130868181' 'Phone', REPLACE(REPLACE(REPLACE(T4.TaxIdNum, '.', ''), '/', ''), '-', '') TaxIdNum, 
	   REPLACE(T4.ZipCode, '-', '') ZipCode, RIGHT(T6.IbgeCode, 5) 'MunE', 
	   CASE WHEN T0.BPLId = 5 
		    THEN CONCAT(T4.Street, ', ', T4.Block, ', ', T4.State) 
			ELSE CONCAT(T4.AddrType, ' ', T4.Street, ' - ',  T4.StreetNo, ', ', T4.Block, ', ', T4.State) 
	   END  
	   Address, 
	   T4.State	   
FROM OINV T0 
INNER JOIN (SELECT DocEntry, SUM(TaxSum) TaxSum FROM INV4 WHERE StaType = 34 GROUP BY DocEntry) T1 ON T0.DocEntry = T1.DocEntry
INNER JOIN (SELECT DocEntry, SUM(TaxSum) TaxSum FROM INV4 WHERE StaType = 33 GROUP BY DocEntry) T2 ON T0.DocEntry = T2.DocEntry
INNER JOIN INV12 T3 ON T0.DocEntry = T3.DocEntry
INNER JOIN OBPL T4 ON T0.BPLId = T4.BPLId
INNER JOIN OCRD T5 ON T0.CardCode = T5.CardCode
INNER JOIN OCNT T6 ON T4.County = T6.AbsId 
INNER JOIN OCNT T7 ON T3.CountyS = T7.AbsId
WHERE T0.DocEntry = DocKey@
GROUP BY T0.CardCode, T0.DocEntry, T0.Serial, T0.LicTradNum, T0.CardName, T5.LicTradNum, T3.StateS, T5.U_SKILL_indIEDest, 
         T4.BPLName, T4.TaxIdNum, T4.ZipCode, T6.IbgeCode, 
		 T4.State, T7.IbgeCode,
		 CASE WHEN T0.BPLId = 5 THEN CONCAT(T4.Street, ', ', T4.Block, ', ', T4.State) 
		 ELSE CONCAT(T4.AddrType, ' ', T4.Street, ' - ',  T4.StreetNo, ', ', T4.Block, ', ', T4.State) END


//XML
//BODY FILE
<?xml version="1.0" encoding="UTF-8"?>
<TLote_GNRE xmlns="http://www.gnre.pe.gov.br" versao="1.00">
    <guias>
        <TDadosGNRE>
             <c01_UfFavorecida>@Get('StateS')</c01_UfFavorecida> 
             <c02_receita>100102</c02_receita>  
             <c27_tipoIdentificacaoEmitente>1</c27_tipoIdentificacaoEmitente>  
             <c03_idContribuinteEmitente>  
                 <CNPJ>@Get('TaxIdNum')</CNPJ> 
             </c03_idContribuinteEmitente> 
             <c28_tipoDocOrigem>10</c28_tipoDocOrigem>
             <c04_docOrigem>@Get('Serial')</c04_docOrigem>
             <c06_valorPrincipal>@Get('ICMSDest')</c06_valorPrincipal>  
             <c14_dataVencimento>@Get('ToDayDate')</c14_dataVencimento>  
             <c15_convenio>93/2015</c15_convenio>  
             <c16_razaoSocialEmitente>@Get('BPLName')</c16_razaoSocialEmitente> 
             <c18_enderecoEmitente>@Get('Address')</c18_enderecoEmitente>  
             <c19_municipioEmitente>@Get('MunE')</c19_municipioEmitente>  
             <c20_ufEnderecoEmitente>@Get('State')</c20_ufEnderecoEmitente>  
             <c21_cepEmitente>@Get('ZipCode')</c21_cepEmitente>  
             <c22_telefoneEmitente>@Get('Phone')</c22_telefoneEmitente>  
             <c34_tipoIdentificacaoDestinatario>@Get('DocId')</c34_tipoIdentificacaoDestinatario>
             <c35_idContribuinteDestinatario>
               <@Get('DocType')>@Get('LicTradNum')</@Get('DocType')>
             </c35_idContribuinteDestinatario> 
             <c37_razaoSocialDestinatario>@Get('CardName')</c37_razaoSocialDestinatario> 
             <c38_municipioDestinatario>@Get('MunD')</c38_municipioDestinatario> 
             <c33_dataPagamento>@Get('ToDayDate')</c33_dataPagamento> 
             <c05_referencia>
                <periodo>0</periodo>
                <mes>@Get('ToDayM')</mes>
                <ano>@Get('ToDayY')</ano>
             </c05_referencia>
             <c39_camposExtras> 
             #IF(@Get('StateS') = 'PB' OR @Get('StateS') = 'RN'  OR @Get('StateS') = 'SC')
             #BEGIN
                <campoExtra>
                    <codigo>@Get('Code')</codigo>
                    <tipo>T</tipo>
                    <valor>@Get('Chave')</valor>
                </campoExtra>
             #END
             #ELSE
             #BEGIN
                <campoExtra> 
                    <codigo>50</codigo> 
                    <tipo>D</tipo> 
                    <valor>@Get('ToDayDate')</valor> 
                </campoExtra> 
                <campoExtra>
                    <codigo>@Get('Code')</codigo>
                    <tipo>T</tipo>
                    <valor>@Get('Chave')</valor>
                </campoExtra>
             #END
             </c39_camposExtras>
        </TDadosGNRE>
    </guias>
</TLote_GNRE>