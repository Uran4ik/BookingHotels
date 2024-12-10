using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingHotels.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    hotel_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    rating = table.Column<decimal>(type: "decimal(2,1)", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    main_image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Hotels__45FE7E260CA9EBD4", x => x.hotel_id);
                });

            migrationBuilder.CreateTable(
                name: "Payment_Methods",
                columns: table => new
                {
                    payment_method_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    method_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment___8A3EA9EB394A5327", x => x.payment_method_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370F6915DCE3", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Hotel_Services",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hotel_id = table.Column<int>(type: "int", nullable: false),
                    service_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Hotel_Se__3E0DB8AF32534962", x => x.service_id);
                    table.ForeignKey(
                        name: "FK__Hotel_Ser__hotel__71D1E811",
                        column: x => x.hotel_id,
                        principalTable: "Hotels",
                        principalColumn: "hotel_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    room_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hotel_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    price_per_night = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    max_occupancy = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rooms__19675A8A446226D3", x => x.room_id);
                    table.ForeignKey(
                        name: "FK__Rooms__hotel_id__5CD6CB2B",
                        column: x => x.hotel_id,
                        principalTable: "Hotels",
                        principalColumn: "hotel_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    favorite_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    hotel_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite__46ACF4CB097CE581", x => x.favorite_id);
                    table.ForeignKey(
                        name: "FK__Favorites__hotel__6EF57B66",
                        column: x => x.hotel_id,
                        principalTable: "Hotels",
                        principalColumn: "hotel_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Favorites__user___6E01572D",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hotel_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<decimal>(type: "decimal(2,1)", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__60883D9067811F9C", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Reviews__hotel_i__6A30C649",
                        column: x => x.hotel_id,
                        principalTable: "Hotels",
                        principalColumn: "hotel_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Reviews__user_id__6B24EA82",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    booking_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    room_id = table.Column<int>(type: "int", nullable: false),
                    check_in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    check_out = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__5DE3A5B151CAE1D6", x => x.booking_id);
                    table.ForeignKey(
                        name: "FK__Bookings__room_i__656C112C",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "room_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Bookings__user_i__6477ECF3",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    discount_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hotel_id = table.Column<int>(type: "int", nullable: true),
                    room_id = table.Column<int>(type: "int", nullable: true),
                    discount_percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discount__BDBE9EF952C01398", x => x.discount_id);
                    table.ForeignKey(
                        name: "FK__Discounts__hotel__60A75C0F",
                        column: x => x.hotel_id,
                        principalTable: "Hotels",
                        principalColumn: "hotel_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Discounts__room___619B8048",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "room_id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    transaction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    booking_id = table.Column<int>(type: "int", nullable: false),
                    payment_method_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transact__85C600AFA79C0866", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK__Transacti__booki__778AC167",
                        column: x => x.booking_id,
                        principalTable: "Bookings",
                        principalColumn: "booking_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Transacti__payme__787EE5A0",
                        column: x => x.payment_method_id,
                        principalTable: "Payment_Methods",
                        principalColumn: "payment_method_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_room_id",
                table: "Bookings",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_user_id",
                table: "Bookings",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_hotel_id",
                table: "Discounts",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_room_id",
                table: "Discounts",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_hotel_id",
                table: "Favorites",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_user_id",
                table: "Favorites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_Services_hotel_id",
                table: "Hotel_Services",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_hotel_id",
                table: "Reviews",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_user_id",
                table: "Reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_hotel_id",
                table: "Rooms",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_booking_id",
                table: "Transactions",
                column: "booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_payment_method_id",
                table: "Transactions",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__AB6E61647934012F",
                table: "Users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Hotel_Services");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Payment_Methods");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
