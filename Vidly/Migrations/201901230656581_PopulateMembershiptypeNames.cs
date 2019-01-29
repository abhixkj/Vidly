namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershiptypeNames : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MEMBERSHIPTYPES SET NAME = 'Pay as You Go' where Id = 1");
            Sql("UPDATE MEMBERSHIPTYPES SET NAME = 'Monthly' where Id = 2");
            Sql("UPDATE MEMBERSHIPTYPES SET NAME = 'Quarterly' where Id = 3");
            Sql("UPDATE MEMBERSHIPTYPES SET NAME = 'Annually' where Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
