using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslationManagement.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Translators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HourlyRate = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditCardNumber = table.Column<string>(type: "TEXT", maxLength: 19, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TranslationJobs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginalContent = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    TranslatedContent = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    TranslatorId = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslationJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslationJobs_Translators_TranslatorId",
                        column: x => x.TranslatorId,
                        principalTable: "Translators",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "TranslationJobs",
                columns: new[] { "Id", "CustomerName", "IsDeleted", "OriginalContent", "Price", "Status", "TranslatedContent", "TranslatorId" },
                values: new object[] { "753abc48-2fbf-4023-83b6-6a27fccf4b32", "Netflix", false, "Text to translate", 0.17000000000000001, 0, null, null });

            migrationBuilder.InsertData(
                table: "TranslationJobs",
                columns: new[] { "Id", "CustomerName", "IsDeleted", "OriginalContent", "Price", "Status", "TranslatedContent", "TranslatorId" },
                values: new object[] { "f787b8d2-bd78-4797-9943-d6464c027c2c", "Apple", true, "Text to translate", 0.17000000000000001, 0, null, null });

            migrationBuilder.InsertData(
                table: "Translators",
                columns: new[] { "Id", "CreditCardNumber", "HourlyRate", "IsDeleted", "Name", "Type" },
                values: new object[] { "0d2c4f16-2dd9-438d-b333-da1c3c8189e6", "4590182781315688", 1000, false, "Evica Johansson", 1 });

            migrationBuilder.InsertData(
                table: "Translators",
                columns: new[] { "Id", "CreditCardNumber", "HourlyRate", "IsDeleted", "Name", "Type" },
                values: new object[] { "99b8b395-17aa-448b-9ee6-68838b5a267b", "4590181640630931", 150, true, "Kerstin Bazhaev", 0 });

            migrationBuilder.InsertData(
                table: "Translators",
                columns: new[] { "Id", "CreditCardNumber", "HourlyRate", "IsDeleted", "Name", "Type" },
                values: new object[] { "a4022e95-cc1d-4fa4-b5fd-fe5690a75521", "4590181712697982", 500, false, "Mylie Ritter", 0 });

            migrationBuilder.InsertData(
                table: "TranslationJobs",
                columns: new[] { "Id", "CustomerName", "IsDeleted", "OriginalContent", "Price", "Status", "TranslatedContent", "TranslatorId" },
                values: new object[] { "24f08dff-2385-45c1-afba-252b650fd666", "Microsoft", false, "Text to translate", 0.17000000000000001, 1, null, "0d2c4f16-2dd9-438d-b333-da1c3c8189e6" });

            migrationBuilder.InsertData(
                table: "TranslationJobs",
                columns: new[] { "Id", "CustomerName", "IsDeleted", "OriginalContent", "Price", "Status", "TranslatedContent", "TranslatorId" },
                values: new object[] { "8f423273-6e07-433d-bd32-495e2d4bb2b3", "Xiaomi", false, "Text to translate", 0.17000000000000001, 2, null, "0d2c4f16-2dd9-438d-b333-da1c3c8189e6" });

            migrationBuilder.CreateIndex(
                name: "IX_TranslationJobs_TranslatorId",
                table: "TranslationJobs",
                column: "TranslatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslationJobs");

            migrationBuilder.DropTable(
                name: "Translators");
        }
    }
}
