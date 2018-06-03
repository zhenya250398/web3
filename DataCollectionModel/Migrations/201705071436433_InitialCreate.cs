namespace DataCollectionModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Department_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Rewards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Year = c.Int(nullable: false),
                        Employee_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rewards", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Rewards", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            DropTable("dbo.Rewards");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
