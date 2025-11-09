using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class removeLazyProxies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ProfilePhotoId", "Surname" },
                values: new object[] { "Wellington", 6, "Kuphal" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ProfilePhotoId", "Surname" },
                values: new object[] { "Malcolm", 2, "Russel" });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "Name", "ProfilePhotoId", "Surname", "UpdatedAt" },
                values: new object[,]
                {
                    { 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kadin", 5, "VonRueden", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Florence", 2, "Murray", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Melyna", 5, "Gleason", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Et minima facilis ratione praesentium tempora dignissimos placeat et voluptas. Iusto voluptatem sit illum. Et aut dolor voluptatem harum architecto eaque provident veritatis aspernatur. Quia dolorem sapiente dicta eum veritatis illo magnam.", "666-1-6362726-71-3", 2, 45.380000000000003, "Dolores qui nam assumenda labore sed sint." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Fugit dicta ea deserunt. Et nam facere voluptate quis. Voluptas quis voluptates doloribus quia.", "666-1-7929691-67-3", 2, 11.52, 2, "Omnis nemo voluptatum recusandae qui." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Aut veritatis aperiam deserunt. Soluta saepe unde voluptatem cumque rem eos corporis deserunt dolorem. Culpa ullam quia et consequatur ut reprehenderit laudantium voluptates blanditiis. Soluta neque consequatur qui et omnis. Est minima eius earum rerum rem dicta quo.", "666-1-2622497-02-9", 2, 29.260000000000002, "Nisi magnam provident repellendus sequi reiciendis." });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedAt", "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Similique aspernatur libero ut perspiciatis. Rerum velit minus provident exercitationem officiis qui facilis enim. Voluptas molestiae hic cum non eligendi nam et rerum. Est quaerat odio nostrum debitis voluptas dolores.", "666-1-2202765-64-1", 2, 14.43, 1, "Quidem et quia aliquam numquam voluptas.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Officiis iusto labore. Recusandae consequuntur vel aut odio expedita delectus quod et reprehenderit. Saepe illo porro molestias doloribus eum numquam aut. Beatae omnis quisquam pariatur fugiat iusto explicabo. Iste veniam vel aut nihil ipsam est ipsam nemo at.", "666-1-8459889-46-8", 1, 40.229999999999997, 2, "Rerum enim impedit odio nobis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sit eius perferendis. Consectetur quod earum consectetur nobis non quia consectetur. Tenetur velit amet quia quia quidem accusantium. Ut fuga quisquam assumenda doloribus iusto animi et omnis porro. Nisi eos earum.", "666-1-9566378-14-5", 1, 11.26, 2, "Consequuntur ut provident tempore quo et et.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aut numquam mollitia nostrum eos velit corporis. Veniam voluptates vel eos nulla. Aut quis nam magni animi in ut incidunt. Et labore et.", "666-1-1791411-97-2", 4, 21.02, 2, "Occaecati veritatis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iste tempora esse temporibus quidem aut. Aspernatur illo incidunt suscipit. Et mollitia quos fugit laudantium eaque ut veniam facere. Rerum minima illum sed omnis architecto. Saepe suscipit laudantium mollitia. Magni adipisci veniam aut quia.", "666-1-5213774-93-3", 4, 18.25, 2, "Est quam optio tempore consequatur facere qui.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aut architecto voluptates neque eveniet officiis adipisci. Laudantium exercitationem fugiat culpa. Aspernatur nihil voluptatem ut asperiores nostrum pariatur fugiat molestiae. Doloribus aut beatae aperiam sapiente rerum sit. Ratione voluptates et est quas cupiditate aut inventore fugit est. Iure sed commodi ullam.", "666-1-2851824-67-1", 2, 17.469999999999999, 1, "Ipsum tenetur maiores ut.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Autem quaerat itaque quia. Eligendi ut quis. Quo est optio aperiam ut dolorem quidem. Temporibus ut mollitia consequatur id.", "666-1-4918824-43-6", 4, 35.149999999999999, 1, "Ea pariatur veniam nulla quia nobis quia.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ullam odio magni qui. Voluptatem veniam maxime vel. Labore qui consectetur consectetur ea cumque eveniet provident quae animi. Earum vitae molestias est ullam fuga in. Harum maxime reiciendis a debitis.\n\nAut dolores ut eum expedita vitae. Placeat itaque veniam ut nihil. Rerum assumenda sint hic. Qui ex ea.", "666-1-8661076-53-6", 3, 7.0999999999999996, 1, "Perspiciatis facilis itaque.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sed iure est eos tempora doloremque et exercitationem hic. Quos nobis qui. Hic et omnis laborum cum sint tempora ut. Illo aut sit quis.\n\nQuo accusantium ab. Eos iusto quia. Rerum voluptates incidunt nulla sit recusandae recusandae aspernatur beatae.", "666-1-3895716-94-6", 3, 19.989999999999998, 2, "Quidem impedit qui placeat eos voluptatem assumenda.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eum libero cumque ducimus libero quia id voluptatem. Non animi autem explicabo quis dolorum nemo sequi nesciunt id. Dolores sed doloremque perferendis corrupti perferendis nulla optio et. Dignissimos quam animi quis alias nihil sit dignissimos.", "666-1-1588781-20-4", 1, 33.649999999999999, 1, "Fugiat sunt quisquam saepe necessitatibus provident distinctio.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Est a expedita ea et ea exercitationem corrupti. Est est veniam velit. Quo itaque minima porro numquam. Aut porro quia veniam a vitae sit. Sapiente quo et debitis autem.", "666-1-9237867-24-7", 4, 29.140000000000001, 1, "Et facilis.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sed ut quo. Ea dolorem quia molestiae cupiditate. Impedit pariatur et esse tenetur tempora. Ipsam error est non nam.\n\nAd ex distinctio placeat quibusdam quam veniam. Animi nemo et commodi. Consectetur error quia reprehenderit quam repudiandae expedita aut. Optio alias deleniti aliquid. Fugit aliquam et vero.", "666-1-3415064-45-5", 4, 24.300000000000001, 1, "Error delectus voluptatem error.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temporibus placeat rerum est fugit. Distinctio ut dignissimos magnam ipsum maiores ut qui. Asperiores nisi sunt sint nesciunt.\n\nConsequatur voluptatum esse voluptates aspernatur. Necessitatibus est maiores commodi sit quis et consequuntur nihil. Maiores iure quas nesciunt nobis pariatur aut voluptatum. Ratione et animi incidunt explicabo. Beatae nostrum et itaque. Incidunt sed natus fugiat vel tempore.", "666-1-4111425-15-5", 2, 13.23, 2, "Aspernatur quae neque.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vitae similique amet tenetur molestiae repellendus dolorem tempore dolor perferendis. Quia molestias nobis voluptas iste aperiam perspiciatis. Odit nihil deleniti saepe reiciendis consequatur nostrum similique laborum et.\n\nAutem cum aperiam sint. Illo laborum quaerat voluptatem corporis maiores doloremque deserunt autem. Et hic sit rem aliquam.", "666-1-6869496-90-2", 4, 29.469999999999999, 2, "Mollitia eum et similique est aut magni.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Et quam veniam tempore et iusto qui ullam animi placeat. Dolorum molestiae sit fugiat consequuntur rem et sint repudiandae. Nulla nam consequuntur aut.", "666-1-6291228-68-2", 1, 28.780000000000001, 1, "Sint animi.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temporibus sed vero possimus id debitis a. Qui expedita possimus nesciunt similique. Voluptatem fugit deserunt iusto sit eveniet voluptas adipisci. Repellat velit labore et iusto.", "666-1-7298722-01-3", 4, 32.460000000000001, 2, "Nihil dolorem qui a.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omnis aperiam temporibus quia sit vel placeat. Quia eos ut et ducimus occaecati corrupti. Nihil exercitationem enim. Quam id harum. Consequatur neque saepe voluptatem repellendus quae sint vero.\n\nCum minus delectus inventore minima totam omnis nihil voluptate et. Non voluptate atque aperiam nisi saepe dolor. Id dicta accusantium in. Ipsum sit atque et. Consequuntur earum quos et cupiditate nulla possimus aspernatur commodi.", "666-1-2549480-54-3", 1, 8.4800000000000004, 2, "Laudantium nulla excepturi.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Necessitatibus quaerat voluptatibus sit eveniet minima tempore aut consequatur. Rerum ad et non voluptas in nemo quis eligendi eum. Sunt voluptatem architecto. Commodi ut aut debitis.\n\nAdipisci molestiae ad rem possimus eos ut illum. Nisi recusandae architecto. Assumenda consequatur velit et autem non nam laudantium a. Est quibusdam repellat voluptates sit sed nemo accusamus incidunt illo. Ducimus pariatur delectus illo. Sed laboriosam iste.", "666-1-0074241-16-9", 3, 42.659999999999997, 1, "Quo sit sit.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Repellendus veniam maxime voluptatum cumque sit tempore quia nesciunt incidunt. Quis cum voluptas voluptates voluptatem voluptates natus. Id id ipsa consequatur delectus odio error assumenda reiciendis. Quis esse vel et. Omnis enim saepe error laudantium alias quisquam. Voluptatibus recusandae assumenda inventore doloribus a enim sint similique doloribus.\n\nAliquid quod repudiandae dolor. Eos magnam eligendi. Repellendus vel adipisci. Explicabo omnis quidem nihil. Aspernatur omnis rerum fuga veritatis. Adipisci vel sunt vel nam exercitationem.", "666-1-3113462-22-4", 4, 29.789999999999999, 1, "Sit reprehenderit.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modi saepe sed enim est soluta. Officia et error iusto accusantium illum. Iure quo quae eos delectus minima enim illum est accusantium. Facere et ipsam et qui cum harum doloribus et. Sed commodi vel quas magni aut cupiditate.", "666-1-1537081-15-9", 4, 45.359999999999999, 2, "Dicta incidunt consequatur ea.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ut sed necessitatibus aut. Sit ea at. Incidunt ea quo qui repellendus. Perferendis quo et cumque et culpa facere error rerum quam. Et quia quam enim ea provident voluptatem. Iste tempore accusamus excepturi sapiente eaque consequatur aut.\n\nCum vel alias in praesentium aut vel unde. Nesciunt qui numquam quidem dolore. Dignissimos ea tempora iusto architecto eum. Occaecati soluta incidunt enim ratione sed ut nam nulla. Labore repudiandae delectus ipsa deserunt numquam quo ipsam.", "666-1-7880231-41-0", 4, 23.469999999999999, 2, "Repellat illo non.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tenetur quia facilis id. Rerum reiciendis est minima nisi. Amet sint sed sit aut natus ea omnis necessitatibus.", "666-1-0615654-48-8", 1, 43.68, 2, "Nesciunt magni laboriosam.", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { new DateTime(2025, 2, 17, 16, 51, 5, 965, DateTimeKind.Unspecified).AddTicks(8834), 1729, 292.50999999999999 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { new DateTime(2025, 7, 26, 8, 45, 7, 378, DateTimeKind.Unspecified).AddTicks(9460), 1535, 231.18000000000001 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { new DateTime(2024, 5, 25, 10, 39, 3, 278, DateTimeKind.Unspecified).AddTicks(2620), 1631, 204.75999999999999 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { new DateTime(2025, 4, 2, 20, 45, 33, 659, DateTimeKind.Unspecified).AddTicks(5317), 1674, 212.91 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { new DateTime(2024, 3, 12, 4, 38, 58, 477, DateTimeKind.Unspecified).AddTicks(5940), 1898, 30.940000000000001 });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Literary Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Biography");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Children's");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Young Adult");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Adventure");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Fantasy");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Thriller");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Science Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Romance");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Mystery");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Action");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Poetry");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Historical Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Horror");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Self-Help");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Name", "ProfilePhotoId" },
                values: new object[] { "36272 VonRueden Rapid, Murphyton, French Southern Territories", "Kuphal, Rempel and O'Reilly", 4 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Name", "ProfilePhotoId" },
                values: new object[] { "24768 Corkery Lodge, South Emmatown, Solomon Islands", "Kohler Group", 4 });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "CreatedAt", "Name", "ProfilePhotoId", "UpdatedAt" },
                values: new object[] { 3, "0161 Alison Mountain, Windlerton, South Georgia and the South Sandwich Islands", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bradtke - Sauer", 1, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "CartId", "Count" },
                values: new object[] { 25, 3, 5 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "CartId", "Count" },
                values: new object[] { 13, 4, 3 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "Count" },
                values: new object[] { 19, 4 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "CartId", "Count" },
                values: new object[] { 17, 6, 4 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "CartId", "Count" },
                values: new object[] { 20, 3, 4 });

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 25, 3 });

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "Stars", "UserId" },
                values: new object[] { 13, 3, 4 });

            migrationBuilder.InsertData(
                table: "RelBookAuthor",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "RelBookGenre",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 8 },
                    { 1, 11 },
                    { 1, 12 },
                    { 2, 4 },
                    { 2, 9 },
                    { 2, 14 },
                    { 3, 1 },
                    { 3, 6 },
                    { 3, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "Olafberg", "Reunion", "Wellington", null, "VonRueden Rapid", "Kuphal" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "Diegofurt", "Denmark", "Florence", null, "Brakus Grove", "Murray" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "Framiburgh", "Kiribati", "Stacey", null, "Corkery Lodge", "Buckridge" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "Roryberg", "Saint Helena", "Dannie", null, "Emma Bridge", "Kihn" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "Lake Emiliostad", "Nauru", "Rebeca", null, "Alison Mountain", "Waters" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "Name", "ProfilePhotoId", "Street", "Surname", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, "Lake Vincenzoton", "New Caledonia", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Angel", null, "Berge Causeway", "Bradtke", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Orlandoburgh", "Andorra", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dennis", null, "Torrance Row", "King", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 21, 7 });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 13, 4 });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 4, 6 });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 18, 5 });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 11, 6 });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatedAt", "OrderDate", "OrderId", "TotalValue", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 6, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 138.91999999999999, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 31, 21, 33, 41, 734, DateTimeKind.Unspecified).AddTicks(7149), 1644, 73.489999999999995, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseItems",
                columns: new[] { "Id", "BookId", "CartId", "Count", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, 7, 4, 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 24, 1, 5, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 18, 3, 4, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BookId", "CreatedAt", "Stars", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 3, 19, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 17, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 5, 20, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 6, 7, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 7, 14, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 8, 24, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "RelBookAuthor",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 10 },
                    { 1, 14 },
                    { 1, 18 },
                    { 1, 19 },
                    { 1, 20 },
                    { 1, 21 },
                    { 1, 22 },
                    { 2, 9 },
                    { 2, 19 },
                    { 2, 25 },
                    { 3, 1 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 13 },
                    { 3, 23 },
                    { 3, 25 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 4, 6 },
                    { 4, 12 },
                    { 4, 15 },
                    { 4, 16 },
                    { 4, 17 },
                    { 4, 24 },
                    { 5, 3 },
                    { 5, 12 },
                    { 5, 15 },
                    { 5, 20 },
                    { 5, 21 }
                });

            migrationBuilder.InsertData(
                table: "RelBookGenre",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 4, 4 },
                    { 4, 11 },
                    { 4, 12 },
                    { 5, 5 },
                    { 6, 2 },
                    { 6, 8 },
                    { 6, 14 },
                    { 7, 5 },
                    { 8, 3 },
                    { 8, 8 },
                    { 9, 7 },
                    { 10, 6 },
                    { 10, 10 },
                    { 10, 13 },
                    { 11, 2 },
                    { 11, 5 },
                    { 12, 4 },
                    { 12, 10 },
                    { 12, 14 },
                    { 13, 3 },
                    { 13, 5 },
                    { 13, 10 },
                    { 14, 8 },
                    { 15, 2 },
                    { 16, 3 },
                    { 16, 13 },
                    { 17, 2 },
                    { 17, 3 },
                    { 17, 13 },
                    { 18, 4 },
                    { 19, 1 },
                    { 19, 10 },
                    { 20, 5 },
                    { 20, 8 },
                    { 20, 15 },
                    { 21, 6 },
                    { 21, 12 },
                    { 21, 15 },
                    { 22, 5 },
                    { 22, 10 },
                    { 22, 14 },
                    { 23, 4 },
                    { 23, 6 },
                    { 23, 12 },
                    { 24, 3 },
                    { 25, 2 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseItems",
                columns: new[] { "Id", "BookId", "CartId", "Count", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 7, 14, 6, 3, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 10, 6, 2, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 19 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 20 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 21 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 22 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 19 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 25 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 23 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 3, 25 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 15 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 16 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 17 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 4, 24 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 12 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 15 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 20 });

            migrationBuilder.DeleteData(
                table: "RelBookAuthor",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 5, 21 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 2, 14 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 4, 11 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 6, 8 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 6, 14 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 9, 7 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 10, 6 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 10, 10 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 10, 13 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 11, 5 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 12, 10 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 12, 14 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 13, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 13, 5 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 13, 10 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 14, 8 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 15, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 16, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 16, 13 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 17, 2 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 17, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 17, 13 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 18, 4 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 19, 1 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 19, 10 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 20, 5 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 20, 8 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 20, 15 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 21, 6 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 21, 12 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 21, 15 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 22, 5 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 22, 10 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 22, 14 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 23, 4 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 23, 6 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 23, 12 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 24, 3 });

            migrationBuilder.DeleteData(
                table: "RelBookGenre",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 25, 2 });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "ProfilePhotoId", "Surname" },
                values: new object[] { "J.R.R.", 9, "Tolkien" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "ProfilePhotoId", "Surname" },
                values: new object[] { "J.K.", 10, "Rowling" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "The first volume in J.R.R. Tolkien's epic adventure, starting the journey to destroy the One Ring.", "978-0618260243", 6, 12.99, "The Fellowship of the Ring" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "The second volume of the trilogy, where the fellowship is scattered and the war for Middle-earth escalates.", "978-0618260281", 7, 14.5, 1, "The Two Towers" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "The final volume, chronicling the final destruction of the Ring and the ultimate fate of Middle-earth.", "978-0618260304", 8, 15.99, "The Return of the King" });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { null, null, 49.990000000000002 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { null, null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1001, 120.5 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1002, 15.75 });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "OrderDate", "OrderId", "TotalValue" },
                values: new object[] { null, null, 200.0 });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Science Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Fantasy");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Mystery");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Thriller");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Romance");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Historical Fiction");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Horror");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Biography");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Self-Help");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Poetry");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Young Adult");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Children's");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Adventure");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Action");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Literary Fiction");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Name", "ProfilePhotoId" },
                values: new object[] { "195 Broadway, New York, NY 10007, USA", "HarperCollins", 11 });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Address", "Name", "ProfilePhotoId" },
                values: new object[] { "1745 Broadway, New York, NY 10019, USA", "Penguin Random House", 12 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "CartId", "Count" },
                values: new object[] { 1, 1, 2 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "CartId", "Count" },
                values: new object[] { 3, 1, 1 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "Count" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "CartId", "Count" },
                values: new object[] { 5, 4, 3 });

            migrationBuilder.UpdateData(
                table: "PurchaseItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "CartId", "Count" },
                values: new object[] { 4, 5, 1 });

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "Stars", "UserId" },
                values: new object[] { 2, 4, 2 });

            migrationBuilder.InsertData(
                table: "RelBookAuthor",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "RelBookGenre",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 2 },
                    { 3, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "New York", "USA", "John", 1, "5th Avenue 123", "Doe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "London", "UK", "Jane", 2, "Baker Street 221B", "Smith" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "Tokyo", "Japan", "Taro", 3, "Shibuya 1-2-3", "Yamada" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "Warsaw", "Poland", "Anna", 4, "Marszałkowska 45", "Kowalska" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "City", "Country", "Name", "ProfilePhotoId", "Street", "Surname" },
                values: new object[] { "Bratislava", "Slovakia", "Peter", 5, "Hviezdoslavovo námestie 7", "Novák" });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 5, 1 });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 1, 2 });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "WishlistItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BookId", "UserId" },
                values: new object[] { 4, 4 });
        }
    }
}
