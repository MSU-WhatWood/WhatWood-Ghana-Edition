using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhanaWoods.Resources.Helpers
{
    public class PanContainer : ContentView
    {
        double startX, startY;
        double panX, panY;

        public PanContainer()
        {
            // Set PanGestureRecognizer.TouchPoints to control the
            // number of touch points needed to pan
            PanGestureRecognizer panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);
        }

        void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    startX = Content.TranslationX;
                    startY = Content.TranslationY;
                    break;

                case GestureStatus.Running:
                    // Translate and pan.
                    double boundsX = Content.Width;
                    double boundsY = Content.Height;
                    //Content.TranslationX = Math.Clamp(panX + e.TotalX, -boundsX, boundsX);
                    //Content.TranslationY = Math.Clamp(panY + e.TotalY, -boundsY, boundsY);
                    Content.TranslationX = Math.Clamp(startX + e.TotalX, -boundsX, boundsX);
                    Content.TranslationY = Math.Clamp(startY + e.TotalY, -boundsY, boundsY);
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    panX = Content.TranslationX;
                    panY = Content.TranslationY;
                    break;
            }
        }
    }
}
