﻿using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using AndroidX.Navigation;
using Google.Android.Material.Navigation;

namespace com.companyname.NavigationGraph2.Fragments
{
    public class ToolsFragment : Fragment
    {
        private NavFragmentOnBackPressedCallback onBackPressedCallback;

        public ToolsFragment() { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            View view = inflater.Inflate(Resource.Layout.fragment_tools, container, false);
            TextView textView = view.FindViewById<TextView>(Resource.Id.text_tools);
            textView.Text = "This is Tools fragment";
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            // Don't want a menu
            base.OnCreateOptionsMenu(menu, inflater);
            menu.Clear();
        }

        public override void OnResume()
        {
            base.OnResume();

            onBackPressedCallback = new NavFragmentOnBackPressedCallback(this, true);
            //// Android docs:  Strongly recommended to use the ViewLifecycleOwner.This ensures that the OnBackPressedCallback is only added when the LifecycleOwner is Lifecycle.State.STARTED.
            //// The activity also removes registered callbacks when their associated LifecycleOwner is destroyed, which prevents memory leaks and makes it suitable for use in fragments or other lifecycle owners
            //// that have a shorter lifetime than the activity.
            //// Note: this rule out using OnAttach(Context context) as the view hasn't been created yet.
            RequireActivity().OnBackPressedDispatcher.AddCallback(ViewLifecycleOwner, onBackPressedCallback);
        }

        #region OnDestroy
        public override void OnDestroy()
        {
            onBackPressedCallback?.Remove();
            base.OnDestroy();
        }
        #endregion

        #region HandleBackPressed
        public void HandleBackPressed(NavOptions navOptions)
        {
            onBackPressedCallback.Enabled = false;

            NavController navController = Navigation.FindNavController(Activity, Resource.Id.nav_host);
            
            // Navigate back to the ImportFragment
            navController.PopBackStack(Resource.Id.import_fragment, false);
            navController.Navigate(Resource.Id.import_fragment, null, navOptions);

        }
        #endregion
    }
}