using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Trading.DAL.Extentions
{
    public static class ModelBuilderExtentions
    {
        public static void SetDecimalPrecision(this ModelBuilder builder, int precision, int scale)
        {
            var decimalProperties = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));
            
            foreach (var property in decimalProperties)
            {
                property.SetPrecision(precision);
                property.SetScale(scale);
            }
        }
    }
}