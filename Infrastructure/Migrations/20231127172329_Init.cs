using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuit = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Companys_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companys",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hierarchy = table.Column<int>(type: "int", nullable: false),
                    MaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Companys_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companys",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SuperiorId = table.Column<int>(type: "int", nullable: true),
                    HistoryFlag = table.Column<bool>(type: "bit", nullable: false),
                    ApprovalsFlag = table.Column<bool>(type: "bit", nullable: false),
                    IsApprover = table.Column<bool>(type: "bit", nullable: false),
                    DepartamentId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartamentId",
                        column: x => x.DepartamentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_SuperiorId",
                        column: x => x.SuperiorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "CompanyId", "Adress", "Cuit", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Av. Calchaquí 3950", "30-69730872-1", "Easy SRL", "4229-4000" },
                    { 2, "Av. Rivadavia 430", "33-70892523-9", "Remax", "4253-4987" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "IdCompany", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Recursos Humanos" },
                    { 2, 1, "Marketing" },
                    { 3, 1, "Comercial" },
                    { 4, 2, "Control de Gestión" },
                    { 5, 2, "Logística y Operaciones" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Hierarchy", "IdCompany", "MaxAmount", "Name" },
                values: new object[,]
                {
                    { 1, 10, 1, 50000m, "Director" },
                    { 2, 10, 1, 50000m, "Lider" },
                    { 3, 10, 1, 500m, "Empleado" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "ApprovalsFlag", "DepartamentId", "FirstName", "HistoryFlag", "IsApprover", "LastName", "PositionId", "SuperiorId" },
                values: new object[,]
                {
                    { 1, false, 1, "diego", false, false, "rodriguez", 1, null },
                    { 2, false, 1, "jose", false, false, "martinez", 2, 1 },
                    { 3, false, 1, "Miguel Ángel", false, false, "Merentiel", 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_IdCompany",
                table: "Departments",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartamentId",
                table: "Employees",
                column: "DepartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SuperiorId",
                table: "Employees",
                column: "SuperiorId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_IdCompany",
                table: "Positions",
                column: "IdCompany");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Companys");
        }
    }
}
