using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableUpdateColumnsName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username_Value",
                schema: "dbo",
                table: "Employee",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Telephone_Value",
                schema: "dbo",
                table: "Employee",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "StatusId_Value",
                schema: "dbo",
                table: "Employee",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "RoleId_Value",
                schema: "dbo",
                table: "Employee",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "PortalId_Value",
                schema: "dbo",
                table: "Employee",
                newName: "PortalId");

            migrationBuilder.RenameColumn(
                name: "Password_Value",
                schema: "dbo",
                table: "Employee",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Name_Value",
                schema: "dbo",
                table: "Employee",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Fax_Value",
                schema: "dbo",
                table: "Employee",
                newName: "Fax");

            migrationBuilder.RenameColumn(
                name: "Email_Value",
                schema: "dbo",
                table: "Employee",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "CompanyId_Value",
                schema: "dbo",
                table: "Employee",
                newName: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "dbo",
                table: "Employee",
                newName: "Username_Value");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                schema: "dbo",
                table: "Employee",
                newName: "Telephone_Value");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                schema: "dbo",
                table: "Employee",
                newName: "StatusId_Value");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "dbo",
                table: "Employee",
                newName: "RoleId_Value");

            migrationBuilder.RenameColumn(
                name: "PortalId",
                schema: "dbo",
                table: "Employee",
                newName: "PortalId_Value");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "dbo",
                table: "Employee",
                newName: "Password_Value");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "Employee",
                newName: "Name_Value");

            migrationBuilder.RenameColumn(
                name: "Fax",
                schema: "dbo",
                table: "Employee",
                newName: "Fax_Value");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "dbo",
                table: "Employee",
                newName: "Email_Value");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                schema: "dbo",
                table: "Employee",
                newName: "CompanyId_Value");
        }
    }
}
