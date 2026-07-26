using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEmprestimoLivroCurso.Migrations
{
    public partial class AdicionarBairroColuna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bairro",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bairro",
                table: "Enderecos");
        }
    }
}
