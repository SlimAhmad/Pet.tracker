using CoreGraphics;
using  Pet.Tracker;
using Pet.Tracker.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PickerControl), typeof(CustomPickerRenderer))]
namespace Pet.Tracker.iOS
{
    /// <summary>
    /// Customized picker control
    /// </summary>
    public class CustomPickerRenderer : PickerRenderer
    {
		

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)

		{

			base.OnElementChanged(e);



			var element = (PickerControl)this.Element;



			if (Control != null && Element != null && !string.IsNullOrEmpty(element.Image))
			{

				var downarrow = UIImage.FromBundle(element.Image);
				//Set the borderColor Color
				Control.Layer.BorderColor = element.BorderColor.ToCGColor();
				//set the Border Width
				Control.Layer.BorderWidth = element.BorderWidth;
				//set the cornerRadius
				Control.Layer.CornerRadius = (float)element.CornerRadius;

				Control.LeftView = new UIKit.UIView(new CGRect(0, 0, 10, 0));

				Control.RightViewMode = UITextFieldViewMode.Always;

				Control.RightView = new UIImageView(downarrow);


			}

		}
	}
}