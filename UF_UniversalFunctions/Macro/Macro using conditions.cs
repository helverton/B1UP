//Macro using conditions
//Macro
//MS SQL

IF $[$38.ItemCode.0] = 'I001' OR $[$38.ItemCode.0] = 'I002' 
BEGIN
    Set($[$38.2000.0]|DIM01);
END
ELSE IF $[$38.ItemCode.0] = 'I003'
BEGIN
    Set($[$38.2000.0]|DIM02);
END
ELSE
BEGIN
    Set($[$38.2000.0]|DIM02);
END