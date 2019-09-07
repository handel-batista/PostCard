using Microsoft.EntityFrameworkCore.Migrations;

namespace PostCard.Migrations
{
    public partial class AlterTableRemoveSubjectandEmailbody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailBody",
                table: "UploadedDatas");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "UploadedDatas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailBody",
                table: "UploadedDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "UploadedDatas",
                nullable: true);
        }
    }
}
