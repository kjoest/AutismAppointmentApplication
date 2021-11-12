namespace AutismAppointmentApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPatientModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patient", "Address");
        }
    }
}
