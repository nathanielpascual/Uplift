using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Migrations
{
    public partial class addOrderHeaderAndDetailsToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Frequency_FrequencyId",
                table: "Service");

            migrationBuilder.AlterColumn<int>(
                name: "FrequencyId",
                table: "Service",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    ServiceCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderHeaderId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    ServiceName = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeader_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ServiceId",
                table: "OrderDetails",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Frequency_FrequencyId",
                table: "Service",
                column: "FrequencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Frequency_FrequencyId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderHeader");

            migrationBuilder.AlterColumn<int>(
                name: "FrequencyId",
                table: "Service",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Frequency_FrequencyId",
                table: "Service",
                column: "FrequencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
