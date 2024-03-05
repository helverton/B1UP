//Copy data from a custom field to a sap field
//Macro
//MS SQL

@STORE1 = SQL('SELECT Name FROM OCST WHERE CODE = $[$BOYX_40.0.0]');

Set($[$178.2.0]|$[$BOYX_36.0.0]); //Rua
Set($[$178.3.0]|$[$BOYX_37.0.0]); //Bairro
Set($[$178.4.0]|$[$BOYX_38.0.0]); //Cidade
Set($[$178.5.0]|$[$BOYX_39.0.0]); //CEP

Set($[$178.8.0]|Brasil); //País
Set($[$178.7.0]|$[$BOYX_40.0.0]); //Estado
Set($[$178.6.0]|$[$BOYX_38.0.0]); //Município


Set($[$178.2002.0]|$[$BOYX_41.0.0]); //Tipo lagr.
Set($[$178.2003.0]|$[$BOYX_42.0.0]); //N° lagr.
Set($[$178.2000.0]|$[$BOYX_43.0.0]); //Complemento