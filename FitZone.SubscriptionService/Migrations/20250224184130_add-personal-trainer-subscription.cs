using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitZone.SubscriptionService.Migrations
{
    /// <inheritdoc />
    public partial class addpersonaltrainersubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PersonalTrainerSubscription_SubscriptionID",
                table: "PersonalTrainerSubscription",
                column: "SubscriptionID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionId",
                table: "Payments",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Subscriptions_SubscriptionId",
                table: "Payments",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalTrainerSubscription_Subscriptions_SubscriptionID",
                table: "PersonalTrainerSubscription",
                column: "SubscriptionID",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Subscriptions_SubscriptionId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalTrainerSubscription_Subscriptions_SubscriptionID",
                table: "PersonalTrainerSubscription");

            migrationBuilder.DropIndex(
                name: "IX_PersonalTrainerSubscription_SubscriptionID",
                table: "PersonalTrainerSubscription");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SubscriptionId",
                table: "Payments");
        }
    }
}
