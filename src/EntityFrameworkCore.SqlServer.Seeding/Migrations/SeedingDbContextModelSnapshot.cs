using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.SqlServer.Seeding.Migrations;

[DbContext(typeof(SeedingDbContext))]
internal partial class SeedingDbContextModelSnapshot : ModelSnapshot
{
	protected override void BuildModel(ModelBuilder modelBuilder)
	{
#pragma warning disable 612, 618
		modelBuilder
			.HasAnnotation("ProductVersion", "3.1.8")
			.HasAnnotation("Relational:MaxIdentifierLength", 128)
			.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

		modelBuilder.Entity("EntityFrameworkCore.SqlServer.Seeding.Models.SeedingEntry", b =>
		{
			b.Property<string>("Name")
				.HasColumnType("nvarchar(450)");

			b.HasKey("Name");

			b.ToTable("__SeedingHistory");
		});
#pragma warning restore 612, 618
	}
}