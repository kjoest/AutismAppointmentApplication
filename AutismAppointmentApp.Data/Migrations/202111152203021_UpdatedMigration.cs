namespace AutismAppointmentApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Therapist", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Therapist", "ImagePath");
        }
    }
}
