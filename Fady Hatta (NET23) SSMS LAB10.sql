SELECT c.CompanyName, c.Country, c.Region, c.Phone, COUNT(o.OrderID) AS NumberOfOrders
FROM Customers AS c
LEFT JOIN Orders AS o ON c.CustomerID = o.CustomerID
GROUP BY c.CompanyName, c.Country, c.Region, c.Phone
ORDER BY c.CompanyName ASC;


-- H�mta alla kunder och antal ordrar de gjort. Sortera fallande p� antal ordrar.
SELECT cu.ContactName, COUNT(o.OrderID) AS TotalOrderValue
FROM Customers AS cu
LEFT JOIN Orders AS o ON cu.CustomerID = o.CustomerID
GROUP BY cu.ContactName
ORDER BY TotalOrderValue DESC;

-- H�mta alla anst�llda tillsammans med territorie de har hand om (EmployeeTerritories och Territories tabellerna)
SELECT e.FirstName, e.LastName, t.TerritoryDescription
FROM Employees AS e
INNER JOIN EmployeeTerritories AS et ON e.EmployeeID = et.EmployeeID
INNER JOIN Territories AS t ON et.TerritoryID = t.TerritoryID;

-- Extra utmaning: I SSMS ist�llet f�r att skriva antal ordrar, skriv ut summan f�r deras totala orderv�rde
SELECT c.CustomerID, c.ContactName, SUM(od.Quantity * p.UnitPrice) AS TotalOrderValue
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
LEFT JOIN [Order Details] od ON o.OrderID = od.OrderID
LEFT JOIN Products p ON od.ProductID = p.ProductID
GROUP BY c.CustomerID, c.ContactName
ORDER BY TotalOrderValue DESC;
