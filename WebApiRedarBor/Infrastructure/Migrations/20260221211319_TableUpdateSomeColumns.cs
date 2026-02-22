using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableUpdateSomeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PortalId",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "dbo",
                table: "Employee");

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
                newName: "RoleId_Value");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GetDate()");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId_Value",
                schema: "dbo",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PortalId_Value",
                schema: "dbo",
                table: "Employee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId_Value",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PortalId_Value",
                schema: "dbo",
                table: "Employee");

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
                newName: "CompanyId");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                schema: "dbo",
                table: "Employee",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PortalId",
                schema: "dbo",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                schema: "dbo",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
