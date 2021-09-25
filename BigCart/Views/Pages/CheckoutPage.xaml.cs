using BigCart.Models;
using BigCart.ViewModels;
using Syncfusion.XForms.EffectsView;
using Syncfusion.XForms.ProgressBar;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.Converters;
using Xamarin.Forms;
using AppIcons = BigCart.Icons.Icons;

namespace BigCart.Pages
{
    public partial class CheckoutPage : Page
    {
        CheckoutViewModel _viewModel;

        public CheckoutPage()
        {
            InitializeComponent();

            BindableLayout.SetItemsSource(_shippingMethodLayout, new[]
            {
                new
                {
                    Title = "Standard Delivery",
                    Description = "Order will be delivered between 3 - 4 business days straight to your doorstep.",
                    Price = 3
                },
                new
                {
                    Title = "Next Day Delivery",
                    Description = "Order will be delivered between 1 - 2 business days straight to your doorstep.",
                    Price = 5
                },
                new
                {
                    Title = "Nominated Delivery",
                    Description = "Order will be delivered on your chosen date.",
                    Price = 3
                }
            });

            BindableLayout.SetItemsSource(_paymentMethodLayout, new[]
            {
                new
                {
                    Id = CreditCardType.Paypal,
                    Label = "Paypal",
                    IconGlyph = AppIcons.Paypal
                },
                new
                {
                    Id = CreditCardType.Mastercard,
                    Label = "Credit Card",
                    IconGlyph = AppIcons.CreditCard
                },
                new
                {
                    Id = CreditCardType.ApplePay,
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Status == PageStatus.Pending)
            {
                var equalConverter = (EqualConverter)Application.Current.Resources["EqualConverter"];
                var notEqualConverter = (NotEqualConverter)Application.Current.Resources["NotEqualConverter"];

                foreach (View view in _paymentMethodLayout.Children)
                {
                    var normalVisualState = GetVisualState(view, "Normal");
                    var selectedVisualState = GetVisualState(view, "Selected");

                    StateTrigger stateTrigger = new();
                    stateTrigger.SetBinding(StateTrigger.IsActiveProperty, new Binding
                    {
                        Path = nameof(CheckoutViewModel.CardType),
                        Source = _viewModel,
                        Converter = notEqualConverter,
                        ConverterParameter = view.FindByName<SfEffectsView>("effectsView").TouchUpCommandParameter
                    });
                    normalVisualState.StateTriggers.Add(stateTrigger);

                    stateTrigger = new StateTrigger();
                    stateTrigger.SetBinding(StateTrigger.IsActiveProperty, new Binding
                    {
                        Path = nameof(CheckoutViewModel.CardType),
                        Source = _viewModel,
                        Converter = equalConverter,
                        ConverterParameter = view.FindByName<SfEffectsView>("effectsView").TouchUpCommandParameter
                    });
                    selectedVisualState.StateTriggers.Add(stateTrigger);
                }
            }
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