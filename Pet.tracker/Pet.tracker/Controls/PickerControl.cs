using Xamarin.Forms;

namespace Pet.Tracker
{
    public class PickerControl : Picker
    {

      


        public static readonly BindableProperty ImageHeightProperty =
            BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(EntryControl), 40);

        public static readonly BindableProperty ImageWidthProperty =
            BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(EntryControl), 40);

        /// <summary>
        /// Binds the border color property to <see cref="BorderColor"/>
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(EntryControl), Color.Gray);

        /// <summary>
        /// Binds the border width property to <see cref="BorderWidth"/>
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(EntryControl), DeviceRuntime(1, 2, 2));

        /// <summary>
        /// Binds the corner radius property to <see cref="CornerRadius"/>
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(EntryControl), DeviceRuntime<double>(6, 7, 7));

        /// <summary>
        /// Binds the corner radius property to <see cref="IsCurvedCornersEnabled"/>
        /// </summary>
        public static readonly BindableProperty IsCurvedCornersEnabledProperty =
            BindableProperty.Create(nameof(IsCurvedCornersEnabled), typeof(bool), typeof(EntryControl), true);

        public static readonly BindableProperty ImageProperty =

			BindableProperty.Create(nameof(Image), typeof(string), typeof(PickerControl), string.Empty);



        #region Helpers

        /// <summary>
        /// Gets the current device in use and returns a value for the current device
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="IOS"></param>
        /// <param name="Android"></param>
        /// <param name="UWP"></param>
        /// <returns></returns>
        public static object DeviceRuntime<T>(T IOS, T Android, T UWP)
        {

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return IOS;

                case Device.Android:
                    return Android;

                case Device.UWP:
                    return UWP;

                default:
                    return 0;
            }
        }

        #endregion

        /// <summary>
        /// Border color property gets or sets the border color value
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        /// Border width property gets or sets the border width value
        /// </summary>
        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        /// <summary>
        /// Corner radius property gets or sets the corner radius value
        /// </summary>
        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Corner width property gets or sets the corner radius enabled value
        /// </summary>
        public bool IsCurvedCornersEnabled
        {
            get => (bool)GetValue(IsCurvedCornersEnabledProperty);
            set => SetValue(IsCurvedCornersEnabledProperty, value);
        }

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public string Image

		{

			get { return (string)GetValue(ImageProperty); }

			set { SetValue(ImageProperty, value); }

		}
	}
}
