﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EcommerceTemplate.CustomViews
{
    /// <summary>
    /// Custom view showing image with zoom and pan support.
    /// </summary>
    public class PinchZoomView : ContentView
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        public PinchZoomView()
        {
            PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += PinchUpdated;
            GestureRecognizers.Add(pinchGesture);

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            var tapGesture = new TapGestureRecognizer();
            tapGesture.NumberOfTapsRequired = 2;
            tapGesture.Tapped += DoubleTapped;
            GestureRecognizers.Add(tapGesture);
        }

        // Event handler for the pinch gesture. Zooms in or out the image according to the pinch direction.
        void PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {

                startScale = Content.Scale;
                Content.AnchorX = 0;
                Content.AnchorY = 0;
            }

            if (e.Status == GestureStatus.Running)
            {
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                Content.TranslationX = Math.Min(0, Math.Max(targetX, -Content.Width * (currentScale - 1)));
                Content.TranslationY = Math.Min(0, Math.Max(targetY, -Content.Height * (currentScale - 1)));

                Content.Scale = currentScale;
            }

            if (e.Status == GestureStatus.Completed)
            {
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
            }
        }

        // Event handler for the pan gesture. Scrolls the picture according to the pan direction.
        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (Content.Scale == 1)
            {
                return;
            }

            switch (e.StatusType)
            {
                case GestureStatus.Running:

                    double newX = (e.TotalX * Scale) + xOffset;
                    double newY = (e.TotalY * Scale) + yOffset;

                    double width = (Content.Width * Content.Scale);
                    double height = (Content.Height * Content.Scale);

                    bool canMoveX = width > Application.Current.MainPage.Width;
                    bool canMoveY = height > Application.Current.MainPage.Height;

                    if (canMoveX)
                    {
                        double minX = (width - (Application.Current.MainPage.Width / 2)) * -1;
                        double maxX = Math.Min(Application.Current.MainPage.Width / 2, width / 2);

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
                        double minY = (height - (Application.Current.MainPage.Height / 2)) * -1;
                        double maxY = Math.Min(Application.Current.MainPage.Width / 2, height / 2);

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

                    Content.TranslationX = newX;
                    Content.TranslationY = newY;
                    break;
                case GestureStatus.Completed:
                    xOffset = Content.TranslationX;
                    yOffset = Content.TranslationY;
                    break;
            }
        }


        // Event handler for the double tap gesture. It is the shortcut for zoom.
        async void DoubleTapped(object sender, EventArgs e)
        {
            double multiplicator = Math.Pow(2, 1.0 / 10.0);
            startScale = Content.Scale;
            Content.AnchorX = 0;
            Content.AnchorY = 0;

            for (int i = 0; i < 10; i++)
            {
                currentScale *= multiplicator;
                double renderedX = Content.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (Content.Width * startScale);
                double originX = (0.5 - deltaX) * deltaWidth;

                double renderedY = Content.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (Content.Height * startScale);
                double originY = (0.5 - deltaY) * deltaHeight;

                double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

                Content.TranslationX = Math.Min(0, Math.Max(targetX, -Content.Width * (currentScale - 1)));
                Content.TranslationY = Math.Min(0, Math.Max(targetY, -Content.Height * (currentScale - 1)));

                Content.Scale = currentScale;
                await Task.Delay(10);
            }

            xOffset = Content.TranslationX;
            yOffset = Content.TranslationY;
        }
    }
}

