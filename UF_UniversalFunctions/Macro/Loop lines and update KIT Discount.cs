//Loop lines and update KIT Discount
//Macro
//MS SQL

@STORE1 = 1; //Contador do laço externo
@STORE2 = $[$38.0.0.LAST-1]; //Última linha válida

WHILE @STORE1 <= @STORE2
BEGIN
    IF $[$38.39.0.@STORE1] = 'S'
    BEGIN
        @STORE3 = SQL('SELECT Discount FROM EDG1 WHERE ObjKey = $[$38.1.0.@STORE1]'); //Desconto do item pai

        IF @STORE3 <> ''
        BEGIN
            @STORE4 = SQL('SELECT Count (*) FROM ITT1 WHERE Father = $[$38.1.0.@STORE1]'); //Número de componentes do item pai
            @STORE5 = 1; // Contador de Laço Interno
            @STORE1 = @STORE1 + 1; // Inicia na linha do item filho
            WHILE @STORE5 <= @STORE4
            BEGIN
                Set($[$38.15.0.@STORE1]|@STORE3); //SetValue campo de desconto
                @STORE1 = @STORE1 + 1; // Increment Linha Ativa
                @STORE5 = @STORE5 + 1; // Increment Contador do Laço Interno
            END
        END
        ELSE
        BEGIN
            StatusBar(O KIT: $[$38.1.0.@STORE1] não tem grupo de desconto cadastrado!|W);
            @STORE1 = @STORE1 +1; 
        END
    END
    ELSE
    BEGIN
        @STORE1 = @STORE1 +1;   
    END
END
//Click($[$1.0.0]);
MessageBox(Concluído com Sucesso!);