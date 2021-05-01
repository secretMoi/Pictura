using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Pictura.ClientAndroid.CustomControls
{
	public class PinchToZoomContainer : ContentView
	{
        private double _currentScale = 1;
        private double _startScale = 1;
        private double _xOffset;
        private double _yOffset;  
  
        public PinchToZoomContainer()
        {  
            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinchGesture);
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {  
            switch (e.Status)
            {
                case GestureStatus.Started:
                    // Store the current scale factor applied to the wrapped user interface element,  
                    // and zero the components for the center point of the translate transform.  
                    _startScale = Content.Scale;  
                    Content.AnchorX = 0;  
                    Content.AnchorY = 0;
                    break;
                case GestureStatus.Running:
                {
                    // Calculate the scale factor to be applied.  
                    _currentScale += (e.Scale - 1) * _startScale;  
                    _currentScale = Math.Max(1, _currentScale);  
  
                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,  
                    // so get the X pixel coordinate.  
                    var renderedX = Content.X + _xOffset;  
                    var deltaX = renderedX / Width;  
                    var deltaWidth = Width / (Content.Width * _startScale);  
                    var originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;  
  
                    // The ScaleOrigin is in relative coordinates to the wrapped user interface element,  
                    // so get the Y pixel coordinate.  
                    var renderedY = Content.Y + _yOffset;  
                    var deltaY = renderedY / Height;  
                    var deltaHeight = Height / (Content.Height * _startScale);  
                    var originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;  
  
                    // Calculate the transformed element pixel coordinates.  
                    var targetX = _xOffset - (originX * Content.Width) * (_currentScale - _startScale);  
                    var targetY = _yOffset - (originY * Content.Height) * (_currentScale - _startScale);  
  
                    // Apply translation based on the change in origin.  
                    Content.TranslationX = targetX.Clamp(-Content.Width * (_currentScale - 1), 0);  
                    Content.TranslationY = targetY.Clamp(-Content.Height * (_currentScale - 1), 0);  
  
                    // Apply scale factor  
                    Content.Scale = _currentScale;
                    break;
                }
                case GestureStatus.Completed:
                    // Store the translation delta's of the wrapped user interface element.  
                    _xOffset = Content.TranslationX;  
                    _yOffset = Content.TranslationY;
                    break;
            }
        }
	}
}