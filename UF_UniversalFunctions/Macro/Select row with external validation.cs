//Select row with external validation
//Macro
//MS SQL

@STORE1 = 1; //Contador do laço externo
@STORE2 = $[$10.0.0.LAST]; //Última linha válida
@STORE3 = ''; //DocEntry
@STORE4 = ''; //Numero do pedido

StatusBar(Processo em andamento...|W);
WHILE @STORE1 <= @STORE2
BEGIN
    @STORE4 = $[$10.11.0.@STORE1]; //Numero do pedido
    
    IF @STORE3 <> @STORE4
    BEGIN
        @STORE3 = @STORE4;
    END
    
    @STORE5 = SQL('SELECT T0.[BPLId] FROM ORDR T0 WHERE T0.[DocEntry] = @STORE3'); 
    IF @STORE5 = 7
    BEGIN
        Set($[$10.1.0.@STORE1]|Y)
    END
    ELSE 
    BEGIN
        Set($[$10.1.0.@STORE1]|N)
    END


    StatusBar(Percorrendo linha @STORE1...|W);
    @STORE1 = @STORE1 +1;
END
StatusBar(Processo finalizado...|S);