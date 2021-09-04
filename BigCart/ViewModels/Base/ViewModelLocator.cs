using BigCart.DependencyInjection;
using System;
using Xamarin.Forms;

namespace BigCart.ViewModels
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), false, propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable) => (bool)bindable.GetValue(AutoWireViewModelProperty);

        public static void SetAutoWireViewModel(BindableObject bindable, bool value) => bindable.SetValue(AutoWireViewModelProperty, value);

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue.Equals(true) && bindable is Element view)
            {
                Type viewType = view.GetType();
                string viewName = viewType.FullName;
                if (viewName.Contains(".Pages."))
                {
                    viewName = viewName.Replace(".Pages.", ".ViewModels.")
                        .Replace("Page", "View")
                        .Replace("Tab", "View");
                }

                string viewAssemblyName = viewType.Assembly.FullName;
                string viewModelName = $"{viewName}Model, {viewAssemblyName}";

                Type viewModelType = Type.GetType(viewModelName);
                if (viewModelType != null)
                    view.BindingContext = DependencyResolver.Container.GetInstance(viewModelType);
            }
        }
    }
}
