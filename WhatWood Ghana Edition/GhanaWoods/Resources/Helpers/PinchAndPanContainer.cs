using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhanaWoods.Resources.Helpers
{
    public class PinchAndPanContainer : ContentView
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        public PinchAndPanContainer()
        {
            PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);
            PanGestureRecognizer panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            //this.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object? sender, EventArgs e)
        {
            //DisplayOrientation dOri = new DisplayOrientation();
            //double sWid = 0;
            //double dWid = 0;
            //double dHei = 0;
            //double dDen = 0;

            //MainThread.BeginInvokeOnMainThread(() =>
            //{
            //    dOri = DeviceDisplay.Current.MainDisplayInfo.Orientation;
            //    dWid = DeviceDisplay.Current.MainDisplayInfo.Width;
            //    dHei = DeviceDisplay.Current.MainDisplayInfo.Height;
            //    dDen = DeviceDisplay.Current.MainDisplayInfo.Density;
            //});

            //if (dHei < dWid) sWid = (dHei / dDen);
            //else sWid = (dWid / dDen);

            //if (dOri == DisplayOrientation.Landscape)
            //{
            //    if (sWid < 700)
            //    {
            //        Shell.SetNavBarIsVisible(this, false);
            //        Shell.SetTabBarIsVisible(this, false);
            //    }
            //}
            //else
            //{
            //    Shell.SetNavBarIsVisible(this, true);
            //    Shell.SetTabBarIsVisible(this, true);
            //}

            currentScale = 1;
            startScale = 1;
            xOffset = 0;
            yOffset = 0;
        }

        void OnPinchUpdated(object? sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - originX * Content.Width * (currentScale - startScale);
                double targetY = yOffset - originY * Content.Height * (currentScale - startScale);

                // Apply translation based on the change in origin.
                Content.TranslationX = Math.Clamp(targetX, -Content.Width * (currentScale - 1), 0);
                Content.TranslationY = Math.Clamp(targetY, -Content.Height * (currentScale - 1), 0);

                // Apply scale factor
                Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }


        private void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            try
            {
                if (Content.Scale == 1)
                {
                    return;
                }
                switch (e.StatusType)
                {
                    case GestureStatus.Running:
                        var newX = e.TotalX * Scale + xOffset;
                        var newY = e.TotalY * Scale + yOffset;
                        var width = Content.Width * Content.Scale;
                        //var height = Content.Height * Content.Scale;
                        var height = (Content.Height * 2) * Content.Scale;
                        //var canMoveX = width > Application.Current.MainPage.Width;
                        //var canMoveY = height > Application.Current.MainPage.Height;
                        //if (canMoveX)
                        //{
                        //    var minX = (width - Application.Current.MainPage.Width / 2) * -1;
                        //    var maxX = Math.Min(Application.Current.MainPage.Width / 2, width / 2);
                        if (Application.Current != null)
                        {
                            var canMoveX = width > Application.Current.Windows[0].Width;
                            var canMoveY = height > Application.Current.Windows[0].Height;
                            if (canMoveX)
                            {
                                var minX = (width - Application.Current.Windows[0].Width / 2) * -1;
                                var maxX = Math.Min(Application.Current.Windows[0].Width / 2, width / 2);
                                if (newX < minX)
                                {
                                    newX = minX;
                                }
                                if (newX > maxX)
                                {
                                    newX = maxX;
                                }
                            }
                            else
                            {
                                newX = 0;
                            }
                            if (canMoveY)
                            {
                                //var minY = (height - Application.Current.MainPage.Height / 2) * -1;
                                ////var maxY = Math.Min(Application.Current.MainPage.Width / 2, height / 2);
                                //var maxY = Math.Min(Application.Current.MainPage.Height / 2, height / 2);
                                var minY = (height - Application.Current.Windows[0].Height / 2) * -1;
                                //var maxY = Math.Min(Application.Current.Windows[0].Width / 2, height / 2);
                                var maxY = Math.Min(Application.Current.Windows[0].Height / 2, height / 2);
                                if (newY < minY)
                                {
                                    newY = minY;
                                }
                                if (newY > maxY)
                                {
                                    newY = maxY;
                                }
                            }
                            else
                            {
                                newY = 0;
                            }
                        }
                        Content.TranslationX = newX;
                        Content.TranslationY = newY;
                        break;
                    case GestureStatus.Completed:
                        xOffset = Content.TranslationX;
                        yOffset = Content.TranslationY;
                        break;
                    case GestureStatus.Started:
                        break;
                    case GestureStatus.Canceled:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                //logger.Error(ex.Message);
                Console.WriteLine(ex.ToString());
                if (ex.InnerException != null) Console.WriteLine(ex.InnerException.ToString());
            }
        }
    }
}
