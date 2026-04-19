using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArticleHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "journals",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    abbreviation = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_journals", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    last_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    honorific = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_persons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    dio = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    stage = table.Column<string>(type: "text", nullable: false),
                    submitted_by_id = table.Column<int>(type: "integer", nullable: false),
                    submitted_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    accepted_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    published_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    journal_id = table.Column<int>(type: "integer", nullable: false),
                    journal_id1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_articles", x => x.id);
                    table.ForeignKey(
                        name: "fk_articles_journals_journal_id",
                        column: x => x.journal_id,
                        principalTable: "journals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_articles_journals_journal_id1",
                        column: x => x.journal_id1,
                        principalTable: "journals",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_articles_persons_submitted_by_id",
                        column: x => x.submitted_by_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "article_actor",
                columns: table => new
                {
                    article_id = table.Column<int>(type: "integer", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false, defaultValue: "AUT")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_actor", x => new { x.article_id, x.person_id, x.role });
                    table.ForeignKey(
                        name: "fk_article_actor_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_article_actor_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_article_actor_person_id",
                table: "article_actor",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_journal_id",
                table: "articles",
                column: "journal_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_journal_id1",
                table: "articles",
                column: "journal_id1");

            migrationBuilder.CreateIndex(
                name: "ix_articles_submitted_by_id",
                table: "articles",
                column: "submitted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_title",
                table: "articles",
                column: "title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_actor");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "journals");

            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}
