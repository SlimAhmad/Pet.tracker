using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace  Pet.Tracker
{
    public class ViewCellControl : ViewCell
    {



        public Color SelectedItemBackgroundColor
        {
            get { return (Color)GetValue(SelectedItemBackgroundColorProperty); }
            set { SetValue(SelectedItemBackgroundColorProperty, value); }
        }


        public Color UnSelectedItemBackgroundColor
        {
            get { return (Color)GetValue(UnSelectedItemBackgroundColorProperty); }
            set { SetValue(UnSelectedItemBackgroundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty SelectedItemBackgroundColorProperty =
            BindableProperty.Create("SelectedItemBackgroundColor", typeof(Color), typeof(ViewCellControl), default);

        public static readonly BindableProperty UnSelectedItemBackgroundColorProperty =
               BindableProperty.Create("SelectedItemBackgroundColor", typeof(Color), typeof(ViewCellControl), default);


    }
}
