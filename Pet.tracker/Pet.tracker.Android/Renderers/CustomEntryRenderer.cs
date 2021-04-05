using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Util;
using  Pet.Tracker;
using Pet.Tracker.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(EntryControl), typeof(CustomEntryRenderer))]
namespace  Pet.Tracker.Droid
{
    /// <summary>
    /// Custom entry renderer for a text field
    /// </summary>
    public class CustomEntryRenderer : EntryRenderer
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        #endregion

        /// <summary>
        /// Handles the creation of the custom entry 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                //Cast Xamarin platform element to custom entry 
                var view = (EntryControl)Element;

                var editText = this.Control;
                //Curved corners is enabled 
                if (view.IsCurvedCornersEnabled)
                {

                    if (!string.IsNullOrEmpty(view.Image))
                    {
                        switch (view.ImageAlignment)
                        {
                            case ImageAlignment.Left:
                                editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(view.Image, view), null, null, null);
                                break;
                            case ImageAlignment.Right:
                                editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(view.Image, view), null);
                                break;
                        }
                    }

                    editText.CompoundDrawablePadding = 25;
                  
                    //instance of gradientDrawable for creating the curved background
                    var gradientbackground = new GradientDrawable();
                    //the shape to a rectangle
                    gradientbackground.SetShape(ShapeType.Rectangle);
                    //Radius for the curve
                    gradientbackground.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(view.CornerRadius)));
                    //Thickness of the stroke line
                    gradientbackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());
                    //Color of the Entry
                    gradientbackground.SetColor(view.BackgroundColor.ToAndroid());



                    //Sets the drawable created to visible
                    gradientbackground.SetVisible(true, true);

                    //Set the background to the gradient background created
                    Control.SetBackground(gradientbackground);

                }

           
                //Sets the Padding within the Entry rectangle
                Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(22)), Control.PaddingTop,
                     (int)DpToPixels(this.Context, Convert.ToSingle(22)), Control.PaddingBottom
                    );
            }


        }

        public float DpToPixels(Context context, float valueInDp)
        {
            //     A structure describing general information about a display, such as its size,
            //     density, and font scaling.
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            //convert the complex data to a final floating point
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }

        private BitmapDrawable GetDrawable(string imageEntryImage, EntryControl view)
        {

            var imageId = imageEntryImage.Split('.');
       
            var resID = Resources.GetIdentifier(imageId[0],"drawable", Context.PackageName);
      
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;
            
            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, view.ImageWidth * 2, view.ImageHeight * 2, true));
        }

    }
}