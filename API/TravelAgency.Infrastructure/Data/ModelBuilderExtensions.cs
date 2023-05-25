using Microsoft.EntityFrameworkCore;

namespace TravelAgency.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyConfiguration<T>(this ModelBuilder modelBuilder, Type configurationType, Type entityType)
        {
            if (typeof(T).IsAssignableFrom(entityType))
            {
                var configurationGenericType = configurationType.MakeGenericType(entityType);
                var configuration = Activator.CreateInstance(configurationGenericType);
                var applyEntityConfigurationMethod = typeof(ModelBuilder)
                    .GetMethods()
                    .Single(e => e.Name == nameof(ModelBuilder.ApplyConfiguration)
                                 && e.ContainsGenericParameters
                                 && e.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
                var target = applyEntityConfigurationMethod.MakeGenericMethod(entityType);
                target.Invoke(modelBuilder, new[] { configuration });
            }

            return modelBuilder;
        }
    }
}
