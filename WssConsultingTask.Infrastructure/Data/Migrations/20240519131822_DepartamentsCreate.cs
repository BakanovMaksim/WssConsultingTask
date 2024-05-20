using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.SqlServer.Types;

#nullable disable

namespace WssConsultingTask.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DepartamentsCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hierarchy = table.Column<SqlHierarchyId>(type: "hierarchyid", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departaments", x => x.Id);
                });

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Departaments ON

                INSERT INTO Departaments (Id, Hierarchy, ParentId)
                    VALUES
                        (1, '/', NULL),
                        (2, '/1/', 1),
                        (3, '/1/1/', 2),
                        (4, '/1/2/', 2),
                        (5, '/1/3/', 2),
                        (6, '/1/1/1/', 3),
                        (7, '/1/1/2/', 3),
                        (8, '/1/2/1/', 4),
                        (9, '/1/2/2/', 4),
                        (10, '/2/', 1)

                SET IDENTITY_INSERT Departaments OFF"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departaments");
        }
    }
}
