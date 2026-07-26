using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEmprestimoLivroCurso.Migrations
{
    public partial class AdicionarBairroColunaMaiscula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bairro",
                table: "Enderecos",
                newName: "Bairro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Enderecos",
                newName: "bairro");
        }
    }
}
