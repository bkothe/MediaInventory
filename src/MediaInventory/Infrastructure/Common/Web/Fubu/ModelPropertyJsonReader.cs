using System;
using System.Web.Script.Serialization;
using FubuCore;
using FubuCore.Binding;
using FubuCore.Descriptions;
using FubuMVC.Core;
using FubuMVC.Core.Http;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Resources.Conneg;

namespace MediaInventory.Infrastructure.Common.Web.Fubu
{
    public class ModelPropertyPolicy : Policy
    {
        public ModelPropertyPolicy()
        {
            Where.ChainMatches(x => x.InputType() != null && ModelProperty.HasModelProperty(x.InputType()));
            ModifyBy(x => {
                x.Input.ClearAll();
                x.Input.Readers.AddToEnd(new Reader(typeof(ModelPropertyJsonReader<>), x.InputType()));
            }, configurationType: ConfigurationType.Attachment);
        }
    }

    [MimeType(new[] { "application/json", "text/json" })]
    [Title("Model Property Json Reader")]
    public class ModelPropertyJsonReader<T> : ReaderBase<T>
    {
        private static Func<Type, ModelProperty> _getModelProperty;

        public ModelPropertyJsonReader(
            IStreamingData data,
            IObjectResolver objectResolver,
            IRequestData requestData,
            IServiceLocator serviceLocator)
            : base(data, objectResolver, requestData, serviceLocator, "application/json", "text/json")
        {
            if (_getModelProperty == null)
                _getModelProperty = FuncExtensions.Memoize<Type, ModelProperty>(ModelProperty.Create);
        }

        public override T Deserialize(string data, string mimeType)
        {
            var serializer = new JavaScriptSerializer();
            var modelProperty = _getModelProperty(typeof(T));
            if (modelProperty == null) return default(T);
            var model = Activator.CreateInstance<T>();
            if (modelProperty.IsList && !data.Trim().StartsWith("["))
                modelProperty.AddModel(model, serializer.Deserialize(data, modelProperty.ModelType));
            else modelProperty.SetValue(model, serializer.Deserialize(data, modelProperty.PropertyType));
            return model;
        }
    }
}
