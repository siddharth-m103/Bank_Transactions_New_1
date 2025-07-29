using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank_transactions__system_1.Migrations
{
    /// <inheritdoc />
    public partial class AddFkTranIdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "BankTransactions");

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "BankTransactions",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BankTransactions_TransactionTypeId",
                table: "BankTransactions",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankTransactions_TransactionTypes_TransactionTypeId",
                table: "BankTransactions",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankTransactions_TransactionTypes_TransactionTypeId",
                table: "BankTransactions");

            migrationBuilder.DropIndex(
                name: "IX_BankTransactions_TransactionTypeId",
                table: "BankTransactions");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "BankTransactions");

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "BankTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
