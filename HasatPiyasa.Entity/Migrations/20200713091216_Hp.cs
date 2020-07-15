using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HasatPiyasa.Entity.Migrations
{
    public partial class Hp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Captions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedTime = table.Column<DateTime>(nullable: false),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<int>(nullable: false),
                    EmteaTypeId = table.Column<int>(nullable: false),
                    MainCaptionCount = table.Column<int>(nullable: false),
                    LeftMainCaptionCount = table.Column<int>(nullable: false),
                    StandartCaptionCount = table.Column<int>(nullable: false),
                    NaturalDataInputCaptionCount = table.Column<int>(nullable: false),
                    NaturalDataInputCaption = table.Column<string>(nullable: true),
                    ToptanPiyasaDataInputCaptionCount = table.Column<int>(nullable: false),
                    ToptanPiyasaDataInputCaption = table.Column<string>(nullable: true),
                    PerakendeDataInputCaptionCount = table.Column<int>(nullable: false),
                    PerakendeDataInputCaption = table.Column<string>(nullable: true),
                    EmteaTypesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Captions_EmteaTypes_EmteaTypesId",
                        column: x => x.EmteaTypesId,
                        principalTable: "EmteaTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

                   

          

            migrationBuilder.CreateIndex(
                name: "IX_Captions_EmteaTypesId",
                table: "Captions",
                column: "EmteaTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_SubeId",
                table: "Cities",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataInputs_AddUserId",
                table: "DataInputs",
                column: "AddUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DataInputs_CityId",
                table: "DataInputs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_DataInputs_EmteaTypeId",
                table: "DataInputs",
                column: "EmteaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataInputs_SubeId",
                table: "DataInputs",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataInputs_UpdateUserId",
                table: "DataInputs",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmteaGroups_EmteaId",
                table: "EmteaGroups",
                column: "EmteaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmteaTypeGroups_EmteaTypeId",
                table: "EmteaTypeGroups",
                column: "EmteaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmteaTypes_EmteaId",
                table: "EmteaTypes",
                column: "EmteaId");

            migrationBuilder.CreateIndex(
                name: "IX_Subes_BolgeId",
                table: "Subes",
                column: "BolgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuiks_AddUserId",
                table: "Tuiks",
                column: "AddUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuiks_CityId",
                table: "Tuiks",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuiks_EmteaTypeId",
                table: "Tuiks",
                column: "EmteaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuiks_SubeId",
                table: "Tuiks",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tuiks_UpdateUserId",
                table: "Tuiks",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_ClaimId",
                table: "UserClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubeId",
                table: "Users",
                column: "SubeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Captions");

        }
    }
}
