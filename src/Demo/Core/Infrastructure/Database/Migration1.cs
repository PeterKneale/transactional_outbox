using FluentMigrator;

namespace Demo.Core.Infrastructure.Database;

[Migration(1)]
public class Migration1 : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("first_name").AsString()
            .WithColumn("last_name").AsString();
        
        Create.Table("outbox_messages")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("type").AsString()
            .WithColumn("data").AsString()
            .WithColumn("processed").AsBoolean();
    }

    public override void Down()
    {
        DropTableIfExists("users");
        DropTableIfExists("outbox_messages");
    }
    
    private void DropTableIfExists(string tableName)
    {
        Execute.Sql($"DROP TABLE IF EXISTS {tableName};");
    }
}