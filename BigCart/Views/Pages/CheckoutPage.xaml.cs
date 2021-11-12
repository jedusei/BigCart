using BigCart.Icons;
using BigCart.Models;
using BigCart.ViewModels;
using Syncfusion.XForms.EffectsView;
using Syncfusion.XForms.ProgressBar;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.Converters;
using Xamarin.Forms;

namespace BigCart.Pages
{
    public partial class CheckoutPage : Page
    {
        private CheckoutViewModel _viewModel;
        private IValueConverter _equalConverter = (IValueConverter)Application.Current.Resources["EqualConverter"];

        public CheckoutPage()
        {
            InitializeComponent();

            BindableLayout.SetItemsSource(_shippingMethodLayout, new[]
            {
                new
                {
                    Id = DeliveryMethod.Standard,
                    Title = "Standard Delivery",
                    Description = "Order will be delivered between 3 - 4 business days straight to your doorstep.",
                    Price = 3
                },
                new
                {
                    Id = DeliveryMethod.NextDay,
                    Title = "Next Day Delivery",
                    Description = "Order will be delivered between 1 - 2 business days straight to your doorstep.",
                    Price = 5
                },
                new
                {
                    Id = DeliveryMethod.Nominated,
                    Title = "Nominated Delivery",
                    Description = "Order will be delivered on your chosen date.",
                    Price = 3
                }
            });

            BindableLayout.SetItemsSource(_paymentMethodLayout, new[]
            {
                new
                {
                    Id = PaymentMethod.Paypal,
                    Label = "Paypal",
                    IconGlyph = AppIcons.Paypal
                },
                new
                {
                    Id = PaymentMethod.CreditCard,
                    Label = "Credit Card",
                    IconGlyph = AppIcons.CreditCard
                },
                new
                {
                    Id = PaymentMethod.ApplePay,
                    Label = "Apple Pay",
                    IconGlyph = AppIcons.AppleLogo
                }
            });
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = BindingContext as CheckoutViewModel;
        }

        private void DeliveryMethodView_Loaded(object sender, EventArgs e)
        {
            var view = (SfEffectsView)sender;

            StateTrigger selectedStateTrigger = new();
            selectedStateTrigger.SetBinding(StateTrigger.IsActiveProperty, new Binding()
            {
                Path = nameof(CheckoutViewModel.DeliveryMethod),
                Source = _viewModel,
                Converter = _equalConverter,
                ConverterParameter = view.TouchUpCommandParameter
            });

            VisualState selectedVisualState = GetVisualState(view, "Selected");
            selectedVisualState.StateTriggers.Add(selectedStateTrigger);

            CompareStateTrigger normalStateTrigger = new() { Value = false };
            normalStateTrigger.SetBinding(CompareStateTrigger.PropertyProperty, new Binding()
            {
                Path = StateTrigger.IsActiveProperty.PropertyName,
                Source = selectedStateTrigger
            });

            VisualState normalVisualState = GetVisualState(view, "Normal");
            normalVisualState.StateTriggers.Add(normalStateTrigger);
        }

        private void PaymentMethodView_Loaded(object sender, EventArgs e)
        {
            var view = (View)sender;

            StateTrigger selectedStateTrigger = new();
            selectedStateTrigger.SetBinding(StateTrigger.IsActiveProperty, new Binding()
            {
                Path = nameof(CheckoutViewModel.PaymentMethod),
                Source = _viewModel,
                Converter = _equalConverter,
                ConverterParameter = view.FindByName<SfEffectsView>("effectsView").TouchUpCommandParameter
            });

            VisualState selectedVisualState = GetVisualState(view, "Selected");
            selectedVisualState.StateTriggers.Add(selectedStateTrigger);

            CompareStateTrigger normalStateTrigger = new() { Value = false };
            normalStateTrigger.SetBinding(CompareStateTrigger.PropertyProperty, new Binding()
            {
                Path = StateTrigger.IsActiveProperty.PropertyName,
                Source = selectedStateTrigger
            });

            VisualState normalVisualState = GetVisualState(view, "Normal");
            normalVisualState.StateTriggers.Add(normalStateTrigger);
        }

        private void SfStepProgressBar_StepTapped(object sender, StepTappedEventArgs e)
        {
            if (e.Item.Status != StepStatus.NotStarted)
                _viewModel.CurrentStep = e.Index;
        }

        private VisualState GetVisualState(VisualElement visualElement, string name)
        {
            IList<VisualStateGroup> visualStateGroups = VisualStateManager.GetVisualStateGroups(visualElement);
            return visualStateGroups.SelectMany(group => group.States.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                .FirstOrDefault();
        }
    }
}