//Set valid values in custom field combo box
//Macro
//MS SQL

SetValidValues($[$BOYX_1.0.0]|SQL:SELECT WhsCode, WhsName FROM OWHS WHERE Nettable = 'Y' AND BPLid = $[$2001.0.NUMBER]);