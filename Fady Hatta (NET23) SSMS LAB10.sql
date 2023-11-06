SELECT Products.ProductName, Products.UnitPrice, Categories.CategoryName
FROM Products
INNER JOIN Categories ON Products.CategoryID = Categories.CategoryID
ORDER BY Categories.CategoryName, Products.ProductName;


-- Hämta alla kunder och antal ordrar de gjort. Sortera fallande på antal ordrar.
SELECT Customers.ContactName, COUNT(Orders.OrderID) AS NumberOfOrders
FROM Customers
LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY Customers.ContactName
ORDER BY NumberOfOrders DESC;

-- Hämta alla anställda tillsammans med territorie de har hand om (EmployeeTerritories och Territories tabellerna)
SELECT Employees.FirstName, Employees.LastName, Territories.TerritoryDescription
FROM Employees
INNER JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID;

-- Extra utmaning: I SSMS istället för att skriva antal ordrar, skriv ut summan för deras totala ordervärde
SELECT c.CustomerID, c.ContactName, SUM(od.Quantity * p.UnitPrice) AS TotalOrderValue
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
LEFT JOIN [Order Details] od ON o.OrderID = od.OrderID
LEFT JOIN Products p ON od.ProductID = p.ProductID
GROUP BY c.CustomerID, c.ContactName
ORDER BY TotalOrderValue DESC;
