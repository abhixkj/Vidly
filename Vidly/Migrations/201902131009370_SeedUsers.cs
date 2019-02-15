namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO[dbo].[AspNetRoles]([Id], [Name]) VALUES(N'8337c31a-58cc-44a7-b75e-b259c7f8620e', N'CanManageMovies') 
                INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'b4901349-74b9-44d1-ada1-9ecbe42eb5ff', N'guest@vidly.com', 0, N'ADPUqC3LTzKhWftSsMl1gIxNjgOSvceLDD24iffOrAXLDpscINrv3tlasceHmr6nAw==', N'f57a3bf1-b3d4-4e7b-8753-4579a58f1b6a', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')  
                INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'b6c14113-994d-4ee3-9322-627ab5539824', N'admin@vidly.com', 0, N'AHTXMrCS5eeCdGF7MEXLdKcZgApsTuw2JGJ3oM2694ZIb3vDaMCvUuVIhuGTG7wlqQ==', N'08ddcf5b-5cf7-4eeb-84ba-1e0fb452890d', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com') 
                INSERT INTO[dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES(N'b6c14113-994d-4ee3-9322-627ab5539824', N'8337c31a-58cc-44a7-b75e-b259c7f8620e')"
                );
        }
        
        public override void Down()
        {
        }
    }
}
