using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jopalesha.Common.Data.EntityFramework.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ConfigureShadowId<T,TKey>(
            this EntityTypeBuilder<T> entity, string idPropertyName = "Id") where T : class
        {
            entity.Property<TKey>(idPropertyName).IsRequired().ValueGeneratedOnAdd();
            entity.HasKey(idPropertyName);
        }
    }
}
