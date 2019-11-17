using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Shared;
using System;

namespace Droid
{
    [Activity(Label = "Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.UserLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        public static readonly string FRAGTAG = "AdvancedImmersiveModeFragment";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            GameMain g = new GameMain();
            SetContentView((View)g.Services.GetService(typeof(View)));
            
            //For Android Fullscreen
            int uiOption = (int)Window.DecorView.SystemUiVisibility;
            uiOption |= (int)SystemUiFlags.Fullscreen;
            uiOption |= (int)SystemUiFlags.HideNavigation;
            uiOption |= (int)SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOption;

            g.Run();
        }
    }
}

