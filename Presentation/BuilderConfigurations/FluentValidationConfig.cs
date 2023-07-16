using FluentValidation.AspNetCore;
using System.Reflection;

namespace Presentation.BuilderConfigurations
{
    public static class FluentValidationConfig
    {
        [Obsolete]
        public static Action<FluentValidationMvcConfiguration> Configuration = options =>
               {
                   options.ImplicitlyValidateChildProperties = true;
                   options.ImplicitlyValidateRootCollectionElements = true;
                   options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
               };
    }
}
