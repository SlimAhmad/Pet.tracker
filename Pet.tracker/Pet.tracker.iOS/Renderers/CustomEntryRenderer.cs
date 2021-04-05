using CoreGraphics;
using Pet.Tracker;
using Pet.Tracker.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryControl), typeof(CustomEntryRenderer))]
namespace  Pet.Tracker.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(e.OldElement == null)
            {
                //cast the element to a control
                var entry = (EntryControl)Element;

                if (entry.IsCurvedCornersEnabled)
                {
                    //Set the borderColor Color
                    Control.Layer.BorderColor = entry.BorderColor.ToCGColor();
                    //set the Border Width
                    Control.Layer.BorderWidth = entry.BorderWidth;
                    //set the cornerRadius
                    Control.Layer.CornerRadius = (float)entry.CornerRadius;

                    Control.LeftView = new UIKit.UIView(new CGRect(0, 0, 10, 0));
                    Control.LeftViewMode = UIKit.UITextFieldViewMode.Always;
                }
            

            }


        }
    }
}