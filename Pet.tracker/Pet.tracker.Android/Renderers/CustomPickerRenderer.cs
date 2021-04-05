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

[assembly: ExportRenderer(typeof(PickerControl), typeof(CustomPickerRenderer))]
namespace  Pet.Tracker.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {

		public CustomPickerRenderer(Context context) : base(context)
		{

		}

		PickerControl element;

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{

			base.OnElementChanged(e);

			element = (PickerControl)this.Element;



			if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))

				
				if (element.IsCurvedCornersEnabled)
				{
					if (!string.IsNullOrWhiteSpace(element.Image))
					{
						Control.Background = AddPickerStyles(element.Image, element);
				
					}

					Control.Background = AddPickerStyles(element?.Image, element);
				}
				//Sets the Padding within the Entry rectangle
				Control.SetPadding(
					(int)DpToPixels(this.Context, Convert.ToSingle(22)), Control.PaddingTop,
					 (int)DpToPixels(this.Context, Convert.ToSingle(22)), Control.PaddingBottom
					);






		}



		public LayerDrawable AddPickerStyles(string imagePath, PickerControl element)

		{

		

			//instance of gradientDrawable for creating the curved background
			var gradientbackground = new GradientDrawable();
			//the shape to a rectangle
			gradientbackground.SetShape(ShapeType.Rectangle);
			//Radius for the curve
			gradientbackground.SetCornerRadius(DpToPixels(Context, Convert.ToSingle(element.CornerRadius)));
			//Thickness of the stroke line
			gradientbackground.SetStroke(element.BorderWidth, element.BorderColor.ToAndroid());
			//Color of the Entry
			gradientbackground.SetColor(element.BackgroundColor.ToAndroid());

		
			

			//Sets the drawable created to visible
			gradientbackground.SetVisible(true, true);


			Drawable[] layers = { gradientbackground, GetDrawable(imagePath, element) };

			LayerDrawable layerDrawable = new LayerDrawable(layers);
	
			layerDrawable.SetLayerInset(1, 10, 10, 20, 10);
		
			return layerDrawable;

		}

		public float DpToPixels(Context context, float valueInDp)
		{
			//     A structure describing general information about a display, such as its size,
			//     density, and font scaling.
			DisplayMetrics metrics = context.Resources.DisplayMetrics;
			//convert the complex data to a final floating point
			return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
		}

		private BitmapDrawable GetDrawable(string imagePath, PickerControl element)

		{
			
			var imageId = imagePath.Split(".");
			int resID = Resources.GetIdentifier(imageId[0], "drawable", Context.PackageName);
			var drawable = ContextCompat.GetDrawable(Context, resID);
			var bitmap = ((BitmapDrawable)drawable).Bitmap;
			var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
		
			result.Gravity = Android.Views.GravityFlags.Right;



			return result;

		}



	}
}
