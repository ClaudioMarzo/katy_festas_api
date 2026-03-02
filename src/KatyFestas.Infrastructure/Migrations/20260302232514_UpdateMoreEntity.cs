using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KatyFestas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoreEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimate_stores_StoreId",
                table: "Estimate");

            migrationBuilder.DropForeignKey(
                name: "FK_EstimateItem_Estimate_EstimateId",
                table: "EstimateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_EstimateItem_items_ItemId",
                table: "EstimateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_Customer_CustomerId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_stores_StoreId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalItem_Rental_RentalId",
                table: "RentalItem");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalItem_items_ItemId",
                table: "RentalItem");

            migrationBuilder.DropForeignKey(
                name: "FK_User_stores_StoreId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalItem",
                table: "RentalItem");

            migrationBuilder.DropIndex(
                name: "IX_RentalItem_RentalId",
                table: "RentalItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rental",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstimateItem",
                table: "EstimateItem");

            migrationBuilder.DropIndex(
                name: "IX_EstimateItem_EstimateId",
                table: "EstimateItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estimate",
                table: "Estimate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "RentalItem",
                newName: "rental_items");

            migrationBuilder.RenameTable(
                name: "Rental",
                newName: "rentals");

            migrationBuilder.RenameTable(
                name: "EstimateItem",
                newName: "estimate_items");

            migrationBuilder.RenameTable(
                name: "Estimate",
                newName: "estimates");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "customers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "users",
                newName: "store_id");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "users",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "users",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_User_StoreId",
                table: "users",
                newName: "ix_users_store_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "rental_items",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "rental_items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "rental_items",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RentalId",
                table: "rental_items",
                newName: "rental_id");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "rental_items",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "rental_items",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "rental_items",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_RentalItem_ItemId",
                table: "rental_items",
                newName: "IX_rental_items_item_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "rentals",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "rentals",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "rentals",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "rentals",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "rentals",
                newName: "store_id");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "rentals",
                newName: "event_date");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "rentals",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "rentals",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "rentals",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_StoreId",
                table: "rentals",
                newName: "ix_rentals_store_id");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_CustomerId",
                table: "rentals",
                newName: "ix_rentals_customer_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "estimate_items",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "estimate_items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "estimate_items",
                newName: "item_id");

            migrationBuilder.RenameColumn(
                name: "EstimateId",
                table: "estimate_items",
                newName: "estimate_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "estimate_items",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_EstimateItem_ItemId",
                table: "estimate_items",
                newName: "IX_estimate_items_item_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "estimates",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "estimates",
                newName: "store_id");

            migrationBuilder.RenameColumn(
                name: "EventForecastDate",
                table: "estimates",
                newName: "event_forecast_date");

            migrationBuilder.RenameColumn(
                name: "EventDescription",
                table: "estimates",
                newName: "event_description");

            migrationBuilder.RenameColumn(
                name: "CustomerPhone",
                table: "estimates",
                newName: "customer_phone");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "estimates",
                newName: "customer_name");

            migrationBuilder.RenameColumn(
                name: "CustomerEmail",
                table: "estimates",
                newName: "customer_email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "estimates",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Estimate_StoreId",
                table: "estimates",
                newName: "ix_estimates_store_id");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "customers",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "customers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "customers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "customers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "customers",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "customers",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "customers",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "customers",
                newName: "created_at");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password_hash",
                table: "users",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "rentals",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "notes",
                table: "rentals",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "event_description",
                table: "estimates",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "customer_phone",
                table: "estimates",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "customer_name",
                table: "estimates",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "customer_email",
                table: "estimates",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phone",
                table: "customers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "customers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "customers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                table: "customers",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rental_items",
                table: "rental_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rentals",
                table: "rentals",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_estimate_items",
                table: "estimate_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_estimates",
                table: "estimates",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true,
                filter: "email IS NOT NULL AND deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_rental_items_rental_id_item_id",
                table: "rental_items",
                columns: new[] { "rental_id", "item_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rentals_event_date",
                table: "rentals",
                column: "event_date");

            migrationBuilder.CreateIndex(
                name: "ix_rentals_status",
                table: "rentals",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_estimate_items_estimate_id_item_id",
                table: "estimate_items",
                columns: new[] { "estimate_id", "item_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_estimates_event_forecast_date",
                table: "estimates",
                column: "event_forecast_date");

            migrationBuilder.CreateIndex(
                name: "ix_customers_email",
                table: "customers",
                column: "email",
                unique: true,
                filter: "email IS NOT NULL AND deleted_at IS NULL");

            migrationBuilder.CreateIndex(
                name: "ix_customers_phone",
                table: "customers",
                column: "phone");

            migrationBuilder.AddForeignKey(
                name: "FK_estimate_items_estimates_estimate_id",
                table: "estimate_items",
                column: "estimate_id",
                principalTable: "estimates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_estimate_items_items_item_id",
                table: "estimate_items",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_estimates_stores_store_id",
                table: "estimates",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rental_items_items_item_id",
                table: "rental_items",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rental_items_rentals_rental_id",
                table: "rental_items",
                column: "rental_id",
                principalTable: "rentals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rentals_customers_customer_id",
                table: "rentals",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rentals_stores_store_id",
                table: "rentals",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_stores_store_id",
                table: "users",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_estimate_items_estimates_estimate_id",
                table: "estimate_items");

            migrationBuilder.DropForeignKey(
                name: "FK_estimate_items_items_item_id",
                table: "estimate_items");

            migrationBuilder.DropForeignKey(
                name: "FK_estimates_stores_store_id",
                table: "estimates");

            migrationBuilder.DropForeignKey(
                name: "FK_rental_items_items_item_id",
                table: "rental_items");

            migrationBuilder.DropForeignKey(
                name: "FK_rental_items_rentals_rental_id",
                table: "rental_items");

            migrationBuilder.DropForeignKey(
                name: "FK_rentals_customers_customer_id",
                table: "rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_rentals_stores_store_id",
                table: "rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_users_stores_store_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_email",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rentals",
                table: "rentals");

            migrationBuilder.DropIndex(
                name: "ix_rentals_event_date",
                table: "rentals");

            migrationBuilder.DropIndex(
                name: "ix_rentals_status",
                table: "rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rental_items",
                table: "rental_items");

            migrationBuilder.DropIndex(
                name: "ix_rental_items_rental_id_item_id",
                table: "rental_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_estimates",
                table: "estimates");

            migrationBuilder.DropIndex(
                name: "ix_estimates_event_forecast_date",
                table: "estimates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_estimate_items",
                table: "estimate_items");

            migrationBuilder.DropIndex(
                name: "ix_estimate_items_estimate_id_item_id",
                table: "estimate_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_customers_email",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "ix_customers_phone",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "rentals",
                newName: "Rental");

            migrationBuilder.RenameTable(
                name: "rental_items",
                newName: "RentalItem");

            migrationBuilder.RenameTable(
                name: "estimates",
                newName: "Estimate");

            migrationBuilder.RenameTable(
                name: "estimate_items",
                newName: "EstimateItem");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "Customer");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "User",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "store_id",
                table: "User",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "User",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "User",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "User",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "User",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_users_store_id",
                table: "User",
                newName: "IX_User_StoreId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Rental",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Rental",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Rental",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Rental",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "store_id",
                table: "Rental",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "event_date",
                table: "Rental",
                newName: "EventDate");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Rental",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Rental",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Rental",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_rentals_store_id",
                table: "Rental",
                newName: "IX_Rental_StoreId");

            migrationBuilder.RenameIndex(
                name: "ix_rentals_customer_id",
                table: "Rental",
                newName: "IX_Rental_CustomerId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "RentalItem",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RentalItem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "RentalItem",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "rental_id",
                table: "RentalItem",
                newName: "RentalId");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "RentalItem",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "RentalItem",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "RentalItem",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_rental_items_item_id",
                table: "RentalItem",
                newName: "IX_RentalItem_ItemId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Estimate",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "store_id",
                table: "Estimate",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "event_forecast_date",
                table: "Estimate",
                newName: "EventForecastDate");

            migrationBuilder.RenameColumn(
                name: "event_description",
                table: "Estimate",
                newName: "EventDescription");

            migrationBuilder.RenameColumn(
                name: "customer_phone",
                table: "Estimate",
                newName: "CustomerPhone");

            migrationBuilder.RenameColumn(
                name: "customer_name",
                table: "Estimate",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "customer_email",
                table: "Estimate",
                newName: "CustomerEmail");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Estimate",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "ix_estimates_store_id",
                table: "Estimate",
                newName: "IX_Estimate_StoreId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "EstimateItem",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "EstimateItem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "item_id",
                table: "EstimateItem",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "estimate_id",
                table: "EstimateItem",
                newName: "EstimateId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "EstimateItem",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_estimate_items_item_id",
                table: "EstimateItem",
                newName: "IX_EstimateItem_ItemId");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Customer",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Customer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Customer",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Customer",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Customer",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Customer",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "Customer",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Customer",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "User",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Rental",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Rental",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventDescription",
                table: "Estimate",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerPhone",
                table: "Estimate",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Estimate",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerEmail",
                table: "Estimate",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Customer",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rental",
                table: "Rental",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalItem",
                table: "RentalItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estimate",
                table: "Estimate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstimateItem",
                table: "EstimateItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RentalItem_RentalId",
                table: "RentalItem",
                column: "RentalId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateItem_EstimateId",
                table: "EstimateItem",
                column: "EstimateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimate_stores_StoreId",
                table: "Estimate",
                column: "StoreId",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstimateItem_Estimate_EstimateId",
                table: "EstimateItem",
                column: "EstimateId",
                principalTable: "Estimate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstimateItem_items_ItemId",
                table: "EstimateItem",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_Customer_CustomerId",
                table: "Rental",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_stores_StoreId",
                table: "Rental",
                column: "StoreId",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalItem_Rental_RentalId",
                table: "RentalItem",
                column: "RentalId",
                principalTable: "Rental",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalItem_items_ItemId",
                table: "RentalItem",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_stores_StoreId",
                table: "User",
                column: "StoreId",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
