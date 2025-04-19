using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mscourier",
                columns: table => new
                {
                    CourierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourierName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mscourier", x => x.CourierID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mspayment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PaymentName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mspayment", x => x.PaymentID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "msproduct",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight = table.Column<float>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msproduct", x => x.ProductID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mssales",
                columns: table => new
                {
                    SalesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SalesName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mssales", x => x.SalesID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ltcourierfee",
                columns: table => new
                {
                    WeightID = table.Column<int>(type: "int", nullable: false),
                    CourierID = table.Column<int>(type: "int", nullable: false),
                    StartKg = table.Column<int>(type: "int", nullable: false),
                    EndKg = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ltcourierfee", x => new { x.WeightID, x.CourierID });
                    table.ForeignKey(
                        name: "FK_ltcourierfee_mscourier_CourierID",
                        column: x => x.CourierID,
                        principalTable: "mscourier",
                        principalColumn: "CourierID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "trinvoice",
                columns: table => new
                {
                    InvoiceNo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    InvoiceTo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShipTo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SalesID = table.Column<int>(type: "int", nullable: false),
                    CourierID = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    CourierFee = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trinvoice", x => x.InvoiceNo);
                    table.ForeignKey(
                        name: "FK_trinvoice_mscourier_CourierID",
                        column: x => x.CourierID,
                        principalTable: "mscourier",
                        principalColumn: "CourierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trinvoice_mspayment_PaymentType",
                        column: x => x.PaymentType,
                        principalTable: "mspayment",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trinvoice_mssales_SalesID",
                        column: x => x.SalesID,
                        principalTable: "mssales",
                        principalColumn: "SalesID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "trinvoicedetail",
                columns: table => new
                {
                    InvoiceNo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "float", nullable: false),
                    Qty = table.Column<short>(type: "smallint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trinvoicedetail", x => new { x.InvoiceNo, x.ProductID });
                    table.ForeignKey(
                        name: "FK_trinvoicedetail_msproduct_ProductID",
                        column: x => x.ProductID,
                        principalTable: "msproduct",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trinvoicedetail_trinvoice_InvoiceNo",
                        column: x => x.InvoiceNo,
                        principalTable: "trinvoice",
                        principalColumn: "InvoiceNo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ltcourierfee_CourierID",
                table: "ltcourierfee",
                column: "CourierID");

            migrationBuilder.CreateIndex(
                name: "IX_trinvoice_CourierID",
                table: "trinvoice",
                column: "CourierID");

            migrationBuilder.CreateIndex(
                name: "IX_trinvoice_PaymentType",
                table: "trinvoice",
                column: "PaymentType");

            migrationBuilder.CreateIndex(
                name: "IX_trinvoice_SalesID",
                table: "trinvoice",
                column: "SalesID");

            migrationBuilder.CreateIndex(
                name: "IX_trinvoicedetail_ProductID",
                table: "trinvoicedetail",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ltcourierfee");

            migrationBuilder.DropTable(
                name: "trinvoicedetail");

            migrationBuilder.DropTable(
                name: "msproduct");

            migrationBuilder.DropTable(
                name: "trinvoice");

            migrationBuilder.DropTable(
                name: "mscourier");

            migrationBuilder.DropTable(
                name: "mspayment");

            migrationBuilder.DropTable(
                name: "mssales");
        }
    }
}
