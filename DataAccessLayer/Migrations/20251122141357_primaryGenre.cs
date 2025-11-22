using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class primaryGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrimaryGenreId",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "PrimaryGenreId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Ea deserunt ut. Nam facere voluptate quis voluptatem voluptas quis voluptates doloribus quia. Voluptas optio nisi magnam provident repellendus.", "666-1-2969167-31-2", 1, 12.699999999999999, 3, "Nemo voluptatum recusandae qui temporibus repellat." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Unde voluptatem cumque rem eos corporis deserunt dolorem doloribus culpa. Quia et consequatur ut reprehenderit. Voluptates blanditiis et soluta neque consequatur. Et omnis harum est minima eius. Rerum rem dicta quo reprehenderit sunt illum quidem et quia.\n\nVoluptas labore non explicabo. Quod tempore sunt soluta. Sit non eum quaerat magnam similique. Libero ut perspiciatis.", "666-1-0295472-00-0", 4, 31.629999999999999, 5, "Enim nostrum dolores sint voluptas." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ISBN", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Nostrum debitis voluptas dolores voluptatem magnam. Rerum enim impedit odio nobis dolorem deleniti. Et et pariatur molestiae voluptate id at. Iusto culpa eaque officiis iusto labore a recusandae consequuntur.\n\nOdio expedita delectus quod et reprehenderit minus saepe illo porro. Doloribus eum numquam aut cupiditate beatae omnis. Pariatur fugiat iusto explicabo saepe iste veniam vel. Nihil ipsam est.", "666-1-8349626-64-9", 32.439999999999998, 9, "Provident exercitationem officiis qui facilis enim." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ISBN", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Quia voluptate provident eos alias error architecto sit eius perferendis. Consectetur quod earum consectetur nobis non quia consectetur. Tenetur velit amet quia quia quidem accusantium. Ut fuga quisquam assumenda doloribus iusto animi et omnis porro. Nisi eos earum.", "666-1-1356780-95-6", 34.619999999999997, 1, "Voluptates itaque." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Eos velit corporis laboriosam veniam. Vel eos nulla est aut quis nam magni animi in. Incidunt et et labore et et excepturi quo est quam. Tempore consequatur facere qui provident tempora sit quia. Repellendus qui doloribus enim ut exercitationem in repudiandae voluptatum.", "666-1-4119723-03-5", 3, 9.9100000000000001, 5, 1, "Veritatis consequuntur id ut consequuntur." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Illum sed omnis architecto amet. Suscipit laudantium mollitia commodi magni adipisci veniam aut quia officiis. Iusto ipsum tenetur maiores ut veniam at. Velit aut aliquam et harum quisquam fugit. Dolores ut occaecati aut architecto. Neque eveniet officiis adipisci eos laudantium exercitationem fugiat culpa et.", "666-1-3755414-09-2", 1, 38.899999999999999, 4, "Quidem aut voluptatem aspernatur illo incidunt." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Est quas cupiditate aut inventore fugit est qui. Sed commodi ullam reprehenderit suscipit non. Pariatur veniam nulla quia nobis quia. Et sequi illum quo incidunt dolores. Vel nobis libero non quia velit. Quaerat itaque quia architecto eligendi ut quis fuga quo.", "666-1-8959000-96-0", 3, 46.280000000000001, 1, "Asperiores nostrum pariatur." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Dolor aut minus laborum deserunt nisi libero quae. Quidem magni ullam odio magni qui velit voluptatem veniam maxime. Aut labore qui consectetur consectetur ea. Eveniet provident quae animi perspiciatis earum vitae molestias. Ullam fuga in nostrum harum maxime reiciendis a.\n\nVelit aut dolores ut eum. Vitae ullam placeat itaque veniam ut nihil consectetur. Assumenda sint hic eaque qui ex ea dolorum. Eveniet quidem impedit qui placeat eos. Assumenda ipsam rerum doloribus officia. Sit distinctio rerum laudantium facilis sit sunt ea omnis.", "666-1-7950682-25-6", 1, 47.210000000000001, 7, "Dolorem quidem corporis." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ISBN", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Sit quis explicabo. Quo accusantium ab. Eos iusto quia.", "666-1-6479656-65-2", 29.23, 10, 2, "Tempora doloremque et exercitationem hic illo quos." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Mollitia at saepe quod. Quia labore accusantium dignissimos quidem enim enim et eum. Cumque ducimus libero quia id voluptatem tenetur non. Autem explicabo quis dolorum nemo sequi nesciunt. Est dolores sed doloremque perferendis corrupti perferendis.\n\nEt expedita dignissimos quam animi quis alias nihil. Dignissimos eius voluptas. Et facilis molestiae. Consequatur minus rerum et quisquam. Deleniti impedit sed quos et. Est a expedita ea et ea exercitationem corrupti.", "666-1-9007998-07-8", 1, 45.039999999999999, 8, 2, "Nulla sit recusandae." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ISBN", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Quo et debitis autem dolor sit corrupti error delectus voluptatem. Ut odio sed deserunt consequatur id quam. In ut molestiae qui minima quasi. Ut quo autem ea dolorem quia molestiae.", "666-1-9272517-12-9", 8.6199999999999992, 8, 1, "Velit quis quo." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ISBN", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Nemo et commodi est consectetur error quia. Quam repudiandae expedita aut nesciunt optio. Deleniti aliquid dolorem.", "666-1-6265523-67-7", 23.940000000000001, 7, "Et esse tenetur tempora quis ipsam error." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ISBN", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Placeat rerum est fugit distinctio distinctio ut dignissimos magnam. Maiores ut qui quis. Nisi sunt sint nesciunt delectus nostrum consequatur voluptatum esse voluptates. Eum necessitatibus est.", "666-1-1114251-55-1", 38.799999999999997, 1, "Officiis quis nemo aspernatur quae neque nihil." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ISBN", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Ut beatae nostrum. Itaque rem incidunt sed natus fugiat. Tempore ut aut ut mollitia eum.\n\nEst aut magni expedita consequatur est sapiente. Earum harum hic doloremque nostrum perspiciatis. Magni itaque vitae similique amet tenetur molestiae repellendus dolorem tempore.", "666-1-9351680-42-1", 41.060000000000002, 6, "Et consequuntur nihil expedita." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Sunt consequuntur autem cum aperiam sint vel illo laborum. Voluptatem corporis maiores doloremque deserunt. Quaerat et hic sit rem aliquam. Ut accusantium sint animi cum non hic dolores numquam numquam. Eligendi rerum non non rem ipsum tenetur et quam.\n\nEt iusto qui ullam animi placeat vero dolorum. Sit fugiat consequuntur rem et sint. Dolores nulla nam consequuntur aut dicta sed laboriosam nihil dolorem. A omnis incidunt ut quo porro incidunt modi.", "666-1-5059144-89-0", 1, 18.09, 4, 1, "Molestias nobis voluptas." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Deserunt iusto sit. Voluptas adipisci commodi repellat velit labore et iusto voluptas facere. Laudantium nulla excepturi exercitationem. Deleniti rerum molestiae at alias cupiditate velit.\n\nIste nam molestias. Aperiam temporibus quia sit vel placeat deserunt quia eos. Et ducimus occaecati corrupti et. Exercitationem enim inventore quam id harum.", "666-1-8778928-67-1", 3, 30.59, 2, 1, "Sed minima deserunt temporibus sed." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Aperiam nisi saepe dolor et id. Accusantium in qui. Sit atque et aut. Earum quos et cupiditate.\n\nAspernatur commodi sit nulla ullam quo sit sit ipsa. Possimus velit veniam. Sequi eos laborum aut quo et. Dolorem necessitatibus quaerat voluptatibus sit. Minima tempore aut consequatur rerum rerum ad et non voluptas. Nemo quis eligendi eum aspernatur sunt.", "666-1-6790247-74-6", 2, 32.039999999999999, 2, "Voluptatem repellendus quae sint vero in molestiae." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "ISBN", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Assumenda consequatur velit et autem non nam laudantium a. Est quibusdam repellat voluptates sit sed nemo accusamus incidunt illo. Ducimus pariatur delectus illo.\n\nLaboriosam iste rerum eaque dolorem sit reprehenderit. Eos dolorem nisi molestiae libero. Modi ut deleniti omnis cum.", "666-1-6142478-98-0", 20.48, 8, "Ut aut debitis hic." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Id id ipsa consequatur delectus odio error assumenda reiciendis. Quis esse vel et. Omnis enim saepe error laudantium alias quisquam. Voluptatibus recusandae assumenda inventore doloribus a enim sint similique doloribus. Nesciunt aliquid quod repudiandae dolor aperiam eos magnam eligendi aut.\n\nAdipisci magni explicabo omnis quidem nihil ut aspernatur omnis. Fuga veritatis iusto adipisci vel sunt vel nam exercitationem et. Atque dicta incidunt consequatur ea fugit. Quia facere accusantium accusamus adipisci quia in. Eveniet dolor cum dolores modi saepe sed enim est soluta. Officia et error iusto accusantium illum.", "666-1-0631253-63-9", 4, 19.289999999999999, 8, 1, "Maxime voluptatum cumque." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Et culpa sed commodi vel quas magni aut cupiditate officiis. Ipsam repellat illo non minus et rerum aperiam numquam. Amet ea sed sunt velit aut. Dolorem ut sed necessitatibus aut eaque sit ea at. Incidunt ea quo qui repellendus.\n\nQuo et cumque. Culpa facere error rerum quam deserunt et quia. Enim ea provident voluptatem porro iste. Accusamus excepturi sapiente eaque consequatur aut sint et. Vel alias in praesentium aut vel unde enim. Qui numquam quidem dolore.", "666-1-2387087-83-6", 2, 25.309999999999999, 5, "Eos delectus." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Ipsa deserunt numquam quo ipsam saepe vero voluptatem nesciunt magni. Aperiam fuga consequuntur sint quidem. Ducimus odio dolorem at eos voluptatem ipsum. Tenetur quia facilis id. Rerum reiciendis est minima nisi. Amet sint sed sit aut natus ea omnis necessitatibus.", "666-1-8562212-96-8", 1, 36.460000000000001, 1, 2, "Iusto architecto eum." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Nisi repellendus nam ducimus et. Quaerat pariatur nostrum et assumenda. Tenetur iusto aut quaerat in. Harum dolor quidem.", "666-1-0112053-72-6", 1, 43.049999999999997, 6, 1, "Doloremque ut magnam quibusdam." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "PublisherId", "Title" },
                values: new object[] { "Labore dolorem soluta quas omnis sunt molestiae. Tenetur vitae quibusdam laborum dolor cum dolor. Dicta corporis architecto sunt quibusdam dicta eum. Praesentium placeat accusantium quaerat consequatur ut asperiores pariatur impedit rerum. Nisi deleniti architecto eum quam eum. Consequatur dolorem porro eligendi aliquid magnam repellat libero harum.", "666-1-4795359-12-7", 2, 31.210000000000001, 4, 1, "Doloribus possimus." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PrimaryGenreId", "Title" },
                values: new object[] { "Hic minima natus enim voluptas in omnis quaerat. Et quo est ex. Dolore et dolor doloremque quis officiis neque eaque qui minus. Autem consequatur aut dolorem cupiditate dolorum ea rem laborum. Officiis debitis molestiae ad laborum assumenda voluptate libero molestias.\n\nUnde facilis aperiam non. Voluptatibus unde dolor in voluptates. Dolorem quas aut deserunt aut explicabo laborum veniam ex magnam. Aspernatur autem doloremque ex facere omnis odit facere.", "666-1-6840995-28-1", 2, 5.5499999999999998, 4, "Voluptatem odio optio ipsa aut perspiciatis." });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PrimaryGenreId",
                table: "Books",
                column: "PrimaryGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_PrimaryGenreId",
                table: "Books",
                column: "PrimaryGenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_PrimaryGenreId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_PrimaryGenreId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PrimaryGenreId",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Fugit dicta ea deserunt. Et nam facere voluptate quis. Voluptas quis voluptates doloribus quia.", "666-1-7929691-67-3", 2, 11.52, "Omnis nemo voluptatum recusandae qui." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Aut veritatis aperiam deserunt. Soluta saepe unde voluptatem cumque rem eos corporis deserunt dolorem. Culpa ullam quia et consequatur ut reprehenderit laudantium voluptates blanditiis. Soluta neque consequatur qui et omnis. Est minima eius earum rerum rem dicta quo.", "666-1-2622497-02-9", 2, 29.260000000000002, "Nisi magnam provident repellendus sequi reiciendis." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ISBN", "Price", "Title" },
                values: new object[] { "Similique aspernatur libero ut perspiciatis. Rerum velit minus provident exercitationem officiis qui facilis enim. Voluptas molestiae hic cum non eligendi nam et rerum. Est quaerat odio nostrum debitis voluptas dolores.", "666-1-2202765-64-1", 14.43, "Quidem et quia aliquam numquam voluptas." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "ISBN", "Price", "Title" },
                values: new object[] { "Officiis iusto labore. Recusandae consequuntur vel aut odio expedita delectus quod et reprehenderit. Saepe illo porro molestias doloribus eum numquam aut. Beatae omnis quisquam pariatur fugiat iusto explicabo. Iste veniam vel aut nihil ipsam est ipsam nemo at.", "666-1-8459889-46-8", 40.229999999999997, "Rerum enim impedit odio nobis." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Sit eius perferendis. Consectetur quod earum consectetur nobis non quia consectetur. Tenetur velit amet quia quia quidem accusantium. Ut fuga quisquam assumenda doloribus iusto animi et omnis porro. Nisi eos earum.", "666-1-9566378-14-5", 1, 11.26, 2, "Consequuntur ut provident tempore quo et et." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Aut numquam mollitia nostrum eos velit corporis. Veniam voluptates vel eos nulla. Aut quis nam magni animi in ut incidunt. Et labore et.", "666-1-1791411-97-2", 4, 21.02, "Occaecati veritatis." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Iste tempora esse temporibus quidem aut. Aspernatur illo incidunt suscipit. Et mollitia quos fugit laudantium eaque ut veniam facere. Rerum minima illum sed omnis architecto. Saepe suscipit laudantium mollitia. Magni adipisci veniam aut quia.", "666-1-5213774-93-3", 4, 18.25, "Est quam optio tempore consequatur facere qui." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Aut architecto voluptates neque eveniet officiis adipisci. Laudantium exercitationem fugiat culpa. Aspernatur nihil voluptatem ut asperiores nostrum pariatur fugiat molestiae. Doloribus aut beatae aperiam sapiente rerum sit. Ratione voluptates et est quas cupiditate aut inventore fugit est. Iure sed commodi ullam.", "666-1-2851824-67-1", 2, 17.469999999999999, "Ipsum tenetur maiores ut." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Description", "ISBN", "Price", "PublisherId", "Title" },
                values: new object[] { "Autem quaerat itaque quia. Eligendi ut quis. Quo est optio aperiam ut dolorem quidem. Temporibus ut mollitia consequatur id.", "666-1-4918824-43-6", 35.149999999999999, 1, "Ea pariatur veniam nulla quia nobis quia." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Ullam odio magni qui. Voluptatem veniam maxime vel. Labore qui consectetur consectetur ea cumque eveniet provident quae animi. Earum vitae molestias est ullam fuga in. Harum maxime reiciendis a debitis.\n\nAut dolores ut eum expedita vitae. Placeat itaque veniam ut nihil. Rerum assumenda sint hic. Qui ex ea.", "666-1-8661076-53-6", 3, 7.0999999999999996, 1, "Perspiciatis facilis itaque." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Description", "ISBN", "Price", "PublisherId", "Title" },
                values: new object[] { "Sed iure est eos tempora doloremque et exercitationem hic. Quos nobis qui. Hic et omnis laborum cum sint tempora ut. Illo aut sit quis.\n\nQuo accusantium ab. Eos iusto quia. Rerum voluptates incidunt nulla sit recusandae recusandae aspernatur beatae.", "666-1-3895716-94-6", 19.989999999999998, 2, "Quidem impedit qui placeat eos voluptatem assumenda." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Description", "ISBN", "Price", "Title" },
                values: new object[] { "Eum libero cumque ducimus libero quia id voluptatem. Non animi autem explicabo quis dolorum nemo sequi nesciunt id. Dolores sed doloremque perferendis corrupti perferendis nulla optio et. Dignissimos quam animi quis alias nihil sit dignissimos.", "666-1-1588781-20-4", 33.649999999999999, "Fugiat sunt quisquam saepe necessitatibus provident distinctio." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Description", "ISBN", "Price", "Title" },
                values: new object[] { "Est a expedita ea et ea exercitationem corrupti. Est est veniam velit. Quo itaque minima porro numquam. Aut porro quia veniam a vitae sit. Sapiente quo et debitis autem.", "666-1-9237867-24-7", 29.140000000000001, "Et facilis." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Description", "ISBN", "Price", "Title" },
                values: new object[] { "Sed ut quo. Ea dolorem quia molestiae cupiditate. Impedit pariatur et esse tenetur tempora. Ipsam error est non nam.\n\nAd ex distinctio placeat quibusdam quam veniam. Animi nemo et commodi. Consectetur error quia reprehenderit quam repudiandae expedita aut. Optio alias deleniti aliquid. Fugit aliquam et vero.", "666-1-3415064-45-5", 24.300000000000001, "Error delectus voluptatem error." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Temporibus placeat rerum est fugit. Distinctio ut dignissimos magnam ipsum maiores ut qui. Asperiores nisi sunt sint nesciunt.\n\nConsequatur voluptatum esse voluptates aspernatur. Necessitatibus est maiores commodi sit quis et consequuntur nihil. Maiores iure quas nesciunt nobis pariatur aut voluptatum. Ratione et animi incidunt explicabo. Beatae nostrum et itaque. Incidunt sed natus fugiat vel tempore.", "666-1-4111425-15-5", 2, 13.23, 2, "Aspernatur quae neque." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Vitae similique amet tenetur molestiae repellendus dolorem tempore dolor perferendis. Quia molestias nobis voluptas iste aperiam perspiciatis. Odit nihil deleniti saepe reiciendis consequatur nostrum similique laborum et.\n\nAutem cum aperiam sint. Illo laborum quaerat voluptatem corporis maiores doloremque deserunt autem. Et hic sit rem aliquam.", "666-1-6869496-90-2", 4, 29.469999999999999, 2, "Mollitia eum et similique est aut magni." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Et quam veniam tempore et iusto qui ullam animi placeat. Dolorum molestiae sit fugiat consequuntur rem et sint repudiandae. Nulla nam consequuntur aut.", "666-1-6291228-68-2", 1, 28.780000000000001, "Sint animi." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Description", "ISBN", "Price", "Title" },
                values: new object[] { "Temporibus sed vero possimus id debitis a. Qui expedita possimus nesciunt similique. Voluptatem fugit deserunt iusto sit eveniet voluptas adipisci. Repellat velit labore et iusto.", "666-1-7298722-01-3", 32.460000000000001, "Nihil dolorem qui a." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Omnis aperiam temporibus quia sit vel placeat. Quia eos ut et ducimus occaecati corrupti. Nihil exercitationem enim. Quam id harum. Consequatur neque saepe voluptatem repellendus quae sint vero.\n\nCum minus delectus inventore minima totam omnis nihil voluptate et. Non voluptate atque aperiam nisi saepe dolor. Id dicta accusantium in. Ipsum sit atque et. Consequuntur earum quos et cupiditate nulla possimus aspernatur commodi.", "666-1-2549480-54-3", 1, 8.4800000000000004, 2, "Laudantium nulla excepturi." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Necessitatibus quaerat voluptatibus sit eveniet minima tempore aut consequatur. Rerum ad et non voluptas in nemo quis eligendi eum. Sunt voluptatem architecto. Commodi ut aut debitis.\n\nAdipisci molestiae ad rem possimus eos ut illum. Nisi recusandae architecto. Assumenda consequatur velit et autem non nam laudantium a. Est quibusdam repellat voluptates sit sed nemo accusamus incidunt illo. Ducimus pariatur delectus illo. Sed laboriosam iste.", "666-1-0074241-16-9", 3, 42.659999999999997, "Quo sit sit." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Repellendus veniam maxime voluptatum cumque sit tempore quia nesciunt incidunt. Quis cum voluptas voluptates voluptatem voluptates natus. Id id ipsa consequatur delectus odio error assumenda reiciendis. Quis esse vel et. Omnis enim saepe error laudantium alias quisquam. Voluptatibus recusandae assumenda inventore doloribus a enim sint similique doloribus.\n\nAliquid quod repudiandae dolor. Eos magnam eligendi. Repellendus vel adipisci. Explicabo omnis quidem nihil. Aspernatur omnis rerum fuga veritatis. Adipisci vel sunt vel nam exercitationem.", "666-1-3113462-22-4", 4, 29.789999999999999, 1, "Sit reprehenderit." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Modi saepe sed enim est soluta. Officia et error iusto accusantium illum. Iure quo quae eos delectus minima enim illum est accusantium. Facere et ipsam et qui cum harum doloribus et. Sed commodi vel quas magni aut cupiditate.", "666-1-1537081-15-9", 4, 45.359999999999999, 2, "Dicta incidunt consequatur ea." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "PublisherId", "Title" },
                values: new object[] { "Ut sed necessitatibus aut. Sit ea at. Incidunt ea quo qui repellendus. Perferendis quo et cumque et culpa facere error rerum quam. Et quia quam enim ea provident voluptatem. Iste tempore accusamus excepturi sapiente eaque consequatur aut.\n\nCum vel alias in praesentium aut vel unde. Nesciunt qui numquam quidem dolore. Dignissimos ea tempora iusto architecto eum. Occaecati soluta incidunt enim ratione sed ut nam nulla. Labore repudiandae delectus ipsa deserunt numquam quo ipsam.", "666-1-7880231-41-0", 4, 23.469999999999999, 2, "Repellat illo non." });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Description", "ISBN", "ImageId", "Price", "Title" },
                values: new object[] { "Tenetur quia facilis id. Rerum reiciendis est minima nisi. Amet sint sed sit aut natus ea omnis necessitatibus.", "666-1-0615654-48-8", 1, 43.68, "Nesciunt magni laboriosam." });
        }
    }
}
