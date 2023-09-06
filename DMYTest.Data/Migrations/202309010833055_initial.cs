namespace DMYTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        Description = c.String(),
                        UnitPrice = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        Address = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        OrderReceived = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ProductImageID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ImagePath = c.String(),
                        Date = c.Time(nullable: false),
                    })
                .PrimaryKey(t => t.ProductImageID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        EmployeeName = c.String(),
                        Job = c.String(),
                        Salary = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PasswordHash = c.Binary(),
                        PasswordSalt = c.Binary(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CompanyName = c.String(),
                        ContactName = c.String(),
                        ContactTitle = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.SupplierID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserOperationClaims",
                c => new
                    {
                        UserOperationClaimID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        OperationClaimID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserOperationClaimID)
                .ForeignKey("dbo.OperationClaims", t => t.OperationClaimID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.OperationClaimID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.OperationClaims",
                c => new
                    {
                        OperationClaimID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OperationClaimID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserOperationClaims", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserOperationClaims", "OperationClaimID", "dbo.OperationClaims");
            DropForeignKey("dbo.Suppliers", "UserID", "dbo.Users");
            DropForeignKey("dbo.Employees", "UserID", "dbo.Users");
            DropForeignKey("dbo.ProductImages", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.UserOperationClaims", new[] { "UserID" });
            DropIndex("dbo.UserOperationClaims", new[] { "OperationClaimID" });
            DropIndex("dbo.Suppliers", new[] { "UserID" });
            DropIndex("dbo.Employees", new[] { "UserID" });
            DropIndex("dbo.ProductImages", new[] { "ProductID" });
            DropIndex("dbo.Orders", new[] { "ProductID" });
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.OperationClaims");
            DropTable("dbo.UserOperationClaims");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Users");
            DropTable("dbo.Employees");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
