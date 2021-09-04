using System.Collections.Generic;

namespace BigCart.Pages
{
    public abstract partial class TabbedPage : Page
    {
        private Tab[] _tabs;
        private Tab _prevTab;
        private List<Tab> _visitedTabs;

        protected abstract Tab[] GetTabs();
        protected abstract int GetActiveTabIndex();

        protected override void OnStart()
        {
            base.OnStart();
            _tabs = GetTabs();
            Tab activeTab = GetActiveTab();
            activeTab.OnStart();
            _prevTab = activeTab;
            _visitedTabs = new List<Tab>(_tabs.Length) { activeTab };
        }

        protected override void OnPause()
        {
            base.OnPause();
            GetActiveTab().OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            GetActiveTab().OnResume();
        }

        protected override void OnStop()
        {
            base.OnStop();

            Tab activeTab = GetActiveTab();

            foreach (Tab tab in _visitedTabs)
            {
                if (tab != activeTab)
                    tab.OnStop();
            }
        }

        private Tab GetActiveTab()
        {
            return _tabs[GetActiveTabIndex()];
        }

        protected void OnSelectedTabChanged()
        {
            if (_prevTab == null)
                return;

            _prevTab.OnPause();

            Tab activeTab = GetActiveTab();

            if (_visitedTabs.Contains(activeTab))
                activeTab.OnResume();
            else
            {
                activeTab.OnStart();
                _visitedTabs.Add(activeTab);
            }

            _prevTab = activeTab;
        }
    }
}
