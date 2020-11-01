using Microsoft.EntityFrameworkCore.Migrations;

namespace DecathlonDynamoErpApp.Migrations
{
    public partial class ParticularLineBalancingModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticularLineBalancings_Lines_LineId",
                table: "ParticularLineBalancings");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticularLineBalancings_StandardOperationTimes_StandardOperationTimeId",
                table: "ParticularLineBalancings");

            migrationBuilder.AlterColumn<int>(
                name: "StandardOperationTimeId",
                table: "ParticularLineBalancings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LineId",
                table: "ParticularLineBalancings",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticularLineBalancings_Lines_LineId",
                table: "ParticularLineBalancings",
                column: "LineId",
                principalTable: "Lines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticularLineBalancings_StandardOperationTimes_StandardOperationTimeId",
                table: "ParticularLineBalancings",
                column: "StandardOperationTimeId",
                principalTable: "StandardOperationTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticularLineBalancings_Lines_LineId",
                table: "ParticularLineBalancings");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticularLineBalancings_StandardOperationTimes_StandardOperationTimeId",
                table: "ParticularLineBalancings");

            migrationBuilder.AlterColumn<int>(
                name: "StandardOperationTimeId",
                table: "ParticularLineBalancings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LineId",
                table: "ParticularLineBalancings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ParticularLineBalancings_Lines_LineId",
                table: "ParticularLineBalancings",
                column: "LineId",
                principalTable: "Lines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticularLineBalancings_StandardOperationTimes_StandardOperationTimeId",
                table: "ParticularLineBalancings",
                column: "StandardOperationTimeId",
                principalTable: "StandardOperationTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
