using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;

namespace GhanaWoods.Resources.Helpers
{
    public class SizeChangedMethods
    {
        public void OnSizeChangedHideNav(object sender, EventArgs e)
        {
            DisplayOrientation dOri = new DisplayOrientation();
            double sWid = 0;
            double dWid = 0;
            double dHei = 0;
            double dDen = 0;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                dOri = DeviceDisplay.Current.MainDisplayInfo.Orientation;
                dWid = DeviceDisplay.Current.MainDisplayInfo.Width;
                dHei = DeviceDisplay.Current.MainDisplayInfo.Height;
                dDen = DeviceDisplay.Current.MainDisplayInfo.Density;
            });

            if (dHei < dWid) sWid = (dHei / dDen);
            else sWid = (dWid / dDen);

            if (dOri == DisplayOrientation.Landscape)
            {
                if (sWid < 700)
                {
                    Shell.SetNavBarIsVisible((BindableObject)sender, false);
                }
            }
            else
            {
                Shell.SetNavBarIsVisible((BindableObject)sender, true);
            }
        }

        public void OnSizeChangedHideTab(object sender, EventArgs e)
        {
            DisplayOrientation dOri = new DisplayOrientation();
            double sWid = 0;
            double dWid = 0;
            double dHei = 0;
            double dDen = 0;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                dOri = DeviceDisplay.Current.MainDisplayInfo.Orientation;
                dWid = DeviceDisplay.Current.MainDisplayInfo.Width;
                dHei = DeviceDisplay.Current.MainDisplayInfo.Height;
                dDen = DeviceDisplay.Current.MainDisplayInfo.Density;
            });

            if (dHei < dWid) sWid = (dHei / dDen);
            else sWid = (dWid / dDen);

            if (dOri == DisplayOrientation.Landscape)
            {
                if (sWid < 700)
                {
                    Shell.SetTabBarIsVisible((BindableObject)sender, false);
                }
            }
            else
            {
                Shell.SetTabBarIsVisible((BindableObject)sender, true);
            }
        }

        public void OnSizechangedHideNavAndTab(object sender, EventArgs e)
        {
            DisplayOrientation dOri = new DisplayOrientation();
            double sWid = 0;
            double dWid = 0;
            double dHei = 0;
            double dDen = 0;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                dOri = DeviceDisplay.Current.MainDisplayInfo.Orientation;
                dWid = DeviceDisplay.Current.MainDisplayInfo.Width;
                dHei = DeviceDisplay.Current.MainDisplayInfo.Height;
                dDen = DeviceDisplay.Current.MainDisplayInfo.Density;
            });

            if (dHei < dWid) sWid = (dHei / dDen);
            else sWid = (dWid / dDen);

            if (dOri == DisplayOrientation.Landscape)
            {
                if (sWid < 700)
                {
                    Shell.SetNavBarIsVisible((BindableObject)sender, false);
                    Shell.SetTabBarIsVisible((BindableObject)sender, false);
                }
            }
            else
            {
                Shell.SetNavBarIsVisible((BindableObject)sender, true);
                Shell.SetTabBarIsVisible((BindableObject)sender, true);
            }
        }
    }
}
