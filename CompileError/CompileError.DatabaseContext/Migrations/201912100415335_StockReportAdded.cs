namespace CompileError.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockReportAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Date = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalesDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(),
                        Quantity = c.Int(nullable: false),
                        MRP = c.Double(nullable: false),
                        TotalMRP = c.Double(nullable: false),
                        SaleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId);
            
            CreateIndex("dbo.PurchasedProducts", "ProductId");
            AddForeignKey("dbo.PurchasedProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.PurchasedProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.SalesDetails", new[] { "SaleId" });
            DropIndex("dbo.PurchasedProducts", new[] { "ProductId" });
            DropTable("dbo.SalesDetails");
            DropTable("dbo.Sales");
        }
    }
}
