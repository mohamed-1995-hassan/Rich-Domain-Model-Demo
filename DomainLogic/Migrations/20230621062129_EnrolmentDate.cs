using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLogic.Migrations
{
    public partial class EnrolmentDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PurchaseDate",
                table: "UserCourseEnrolment",
                newName: "EnrolmentDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnrolmentDate",
                table: "UserCourseEnrolment",
                newName: "PurchaseDate");
        }
    }
}
