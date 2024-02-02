namespace R54_M8_Class_03_Work_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "JoinDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "JoinDate", c => c.DateTime());
        }
    }
}
