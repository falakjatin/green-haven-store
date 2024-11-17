using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace green_haven_store.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Cat_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cat_Name = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Cat_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cat_IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Cat_IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Cat_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    U_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_Name = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    U_Email = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    U_Address = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    U_Password = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    U_Pincode = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    U_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    U_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    U_IsActive = table.Column<bool>(type: "bit", nullable: false),
                    U_IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.U_Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    P_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    P_Name = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    P_Price = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    P_PictureUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    P_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    P_IsActive = table.Column<bool>(type: "bit", nullable: false),
                    P_IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.P_Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Cat_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Cart_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Cart_Quantity = table.Column<int>(type: "int", nullable: true),
                    isOrderAdded = table.Column<bool>(type: "bit", nullable: true),
                    Cart_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cart_Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Cart_Id);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "P_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Order_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductIds = table.Column<int>(type: "int", nullable: true),
                    CartIds = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Order_Tax = table.Column<double>(type: "float", nullable: false),
                    Order_PayableAmount = table.Column<double>(type: "float", nullable: false),
                    Order_Quantity = table.Column<int>(type: "int", nullable: true),
                    Order_IsPaymentDone = table.Column<bool>(type: "bit", nullable: false),
                    Order_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductP_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductP_Id",
                        column: x => x.ProductP_Id,
                        principalTable: "Products",
                        principalColumn: "P_Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "U_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Cat_Id", "Cat_Date", "Cat_IsActive", "Cat_IsDelete", "Cat_Name" },
                values: new object[] { 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(8834), true, false, "Plant" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "U_Id", "U_Address", "U_Date", "U_Email", "U_IsActive", "U_IsDelete", "U_Name", "U_Password", "U_Pincode", "U_Type" },
                values: new object[,]
                {
                    { 1, "65 Foresthead Street, Kitchener", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test@gmail.com", true, false, "Test Customer", "test", "N2L 3T6", "user" },
                    { 2, "Canada", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", true, false, "Admin", "admin", "N2L 3T6", "admin" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "P_Id", "CategoryId", "P_Date", "P_Description", "P_IsActive", "P_IsDelete", "P_Name", "P_PictureUri", "P_Price" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(8994), "Have you ever wanted to grow your own adorable pineapple indoors? With the Bromeliad Pineapple, you can! This bromeliad sprouts a fun ornamental pineapple from a whorl of long green leaves. Sure to spark a conversation with your next dinner guests, you’ll know your pineapple fruit has reached maturity when it turns fragrant and a brighter shade of yellow. Although this variety is not grown for edible consumption, it’s still a unique gift or addition to any personal collection.", true, false, "Bromeliad Pineapple", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_bromeliad-pineapple_charcoal-e1625250094764.jpg?ver=279190", "79.99" },
                    { 2, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(8998), "Easy and graceful, with lush dark green fronds", true, false, "Parlor Palm", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_parlor-palmloomscape_slate-e1625249746437.jpg?ver=279264", "69.99" },
                    { 3, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9000), "The Monstera deliciosa, also known as the Swiss Cheese Plant, is a striking tropical plant originating from the rainforests of southern Mexico. Lively and wild with large, tropical leaves make it a popular choice for indoor gardens.", true, false, "Monstera Deliciosa", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_monstera_alt_clay.jpg?ver=279414", "84.99" },
                    { 4, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9002), "Impressive and tropical with large, glossy leaves that naturally split over time.", true, false, "Bird of Paradise", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_bird-of-paradise_indigo.jpg?ver=279491", "139.99" },
                    { 5, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9004), "With dense foliage and lush fronds, the Bamboo Palm makes a statement. An air-purifying plant adaptable to low light, this palm can reach heights of up to 8 feet tall in the right conditions.\r\n\r\n", true, false, "Bamboo Palm", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_bamboo-palm_stone.jpg?ver=279484", "160.99" },
                    { 6, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9006), "Popular for its use in Feng Shui, the Money Tree is a pet-friendly and air-purifying plant with large star-shaped leaves and a braided trunk to give your home a tropical feel.\r\n\r\n", true, false, "Money Tree", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_money-tree_slate-e1643402075928.jpg?ver=279409", "135.99" },
                    { 7, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9008), "Architectural and sturdy, this plant is easy to care for and highly adaptable. Also known as a Snake Plant and Mother-in-Law’s Tongue.\r\n\r\n", true, false, "Sansevieria", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_sansevieria_charcoal-e1633460982733.jpg?ver=279439", "119.99" },
                    { 8, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9010), "Easy and graceful, with lush dark green fronds", true, false, "Parlor Palm", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_parlor-palmloomscape_slate-e1625249746437.jpg?ver=279264", "69.99" },
                    { 9, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9012), "Robust and dramatic, the Burgundy Rubber Tree sports large, glossy leaves on durable, upright stems. This striking plant is ready to make a statement in your home with its dark, moody color palette ranging from deep forest green to rich burgundy red. This low-maintenance plant will be happiest in a spot with bright indirect light.\r\n\r\n", true, false, "Burgundy Rubber Tree", "https://bloomscape.com/wp-content/uploads/2022/08/bloomscape_burgundy-rubber-tree_lg_charcoal.jpg?ver=927090", "84.99" },
                    { 10, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9014), "Easy and graceful, with lush dark green fronds", true, false, "Parlor Palm", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_parlor-palmloomscape_slate-e1625249746437.jpg?ver=279264", "69.99" },
                    { 11, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9016), "With graceful layered leaves, the hardy ZZ Plant is a statement plant that can reach up to 2.5 feet tall. This drought-tolerant plant is tough, beautiful, and nearly indestructible.\r\n\r\n", true, false, "ZZ Plant", "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_zz-plant_charcoal-e1625251444532.jpg?ver=279472", "119.99" },
                    { 12, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9018), "A colorful and low-maintenance air plant, the Bromeliad Summer is a striking houseplant that adds warmth to your home. With a bright and cheery magenta flower, this bromeliad is pet-friendly and makes for a great gift for any plant lover or beginner plant parent.", true, false, "Bromeliad Summer", "https://bloomscape.com/wp-content/uploads/2023/04/BloomScape_SM_Bromeliad-Summer_white-scaled.jpg?ver=1022056", "49.99" },
                    { 13, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9020), "Easy-going Monstera with striking, fenestrated leaves. Also known as the Swiss Cheese Vine.\r\n\r\n", true, false, "Monstera Adansonii", "https://bloomscape.com/wp-content/uploads/2019/11/bloomscape_monstera-adansonii_md_clay-scaled-e1721151440768.jpg?ver=440338", "79.99" },
                    { 14, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9022), "The Ficus Tineke (Ficus elastica), the vibrant younger sibling of our popular Burgundy Rubber Tree. Boasting variegated leaves in hues of creamy pink, yellow, and green, this rubber tree is a captivating presence in any interior setting, whether it graces your space solo or thrives amidst your collection.\r\n\r\n", true, false, "Ficus Tineke", "https://bloomscape.com/wp-content/uploads/2023/08/230805_BB_fiscus_elastica_tineke_slate-scaled-e1692911022828.jpg?ver=1057588", "79.99" },
                    { 15, 1, new DateTime(2024, 11, 16, 18, 49, 33, 853, DateTimeKind.Local).AddTicks(9024), "Vibrant and bright with patterned, neon green leaves.", true, false, "Neon Prayer Plant", "https://bloomscape.com/wp-content/uploads/2020/09/bloomscape_neon-prayer-plant_charcoal-e1625250223838.jpg?ver=292318", "69.99" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductP_Id",
                table: "Orders",
                column: "ProductP_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
