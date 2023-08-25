using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jopalesha.Common.Data.EntityFramework.Extensions
{
    /// <summary>
    /// Type builder extensions.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Configure hidden id for entity.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <typeparam name="TKey">Entity key type.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <param name="idPropertyName">Id property name.</param>
        public static void ConfigureShadowId<T, TKey>(
            this EntityTypeBuilder<T> entity, string idPropertyName = "Id") where T : class
        {
            entity.Property<TKey>(idPropertyName).IsRequired().ValueGeneratedOnAdd();
            entity.HasKey(idPropertyName);
        }
    }
}
