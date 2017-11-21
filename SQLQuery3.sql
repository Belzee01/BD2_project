select  * from Production.TransactionHistory ORDER BY (SELECT NULL)
OFFSET 10 ROWS FETCH NEXT 13 ROWS ONLY;
