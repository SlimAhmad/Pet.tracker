using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace  Pet.Tracker
{
    /// <summary>
    /// A base attached property to replace the vanilla WPF attached property
    /// </summary>
    /// <typeparam name="Parent">The parent class to be the attached property</typeparam>
    /// <typeparam name="Property">The type of this attached property</typeparam>
    public abstract class BaseBindableProperties<Parent, Property> : Element
          where Parent : new()
    {
            #region Public Events

            /// <summary>
            /// Fired when the value changes
            /// </summary>
            public event Action<BindableObject, object, object> ValueChanged = (sender, e, o) => { };

            /// <summary>
            /// Fired when the value changes, even when the value is the same
            /// </summary>
            public event Action<BindableObject, object> ValueUpdated = (sender, value) => { };

            #endregion

            #region Public Properties

            /// <summary>
            /// A singleton instance of our parent class
            /// </summary>
            public static Parent Instance { get; private set; } = new Parent();

            #endregion

            #region Attached Property Definitions

            /// <summary>
            /// The attached property for this class
            /// </summary>
            public static readonly BindableProperty ValueProperty = BindableProperty.CreateAttached(
                "Value",
                typeof(Property),
                typeof(BaseBindableProperties<Parent, Property>),
                default(Property),propertyChanged: OnValuePropertyChanged, coerceValue: OnValuePropertyUpdated
            );


            /// <summary>
            /// The callback event when the <see cref="ValueProperty"/> is changed
            /// </summary>
            /// <param name="bindable">The UI element that had it's property changed</param>
            /// <param name="value">The arguments for the events value</param>
            private static object OnValuePropertyUpdated(BindableObject bindable, object value)
                {
                    // Call the parent function
                    (Instance as BaseBindableProperties<Parent, Property>)?.OnValueUpdated(bindable, value);

                    // Call event listeners
                    (Instance as BaseBindableProperties<Parent, Property>)?.ValueUpdated(bindable, value);

                  // Return the value
                  return value;
                }


        /// <summary>
        /// The callback event when the <see cref="ValueProperty"/> is changed, even if it is the same value
        /// </summary>
        /// <param name="bindable">The UI element that had it's property changed</param>
        /// <param name="oldValue">The arguments for the event</param>
        /// <param name="newValue">The arguments for the events</param>
        [SuppressPropertyChangedWarnings]
        private static void OnValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
                    {
                        // Call the parent function
                        (Instance as BaseBindableProperties<Parent, Property>)?.OnValueChanged(bindable, oldValue, newValue);

                        // Call event listeners
                        (Instance as BaseBindableProperties<Parent, Property>)?.ValueChanged(bindable, oldValue, newValue);
                    }



    

            /// <summary>
        /// Gets the attached property
        /// </summary>
        /// <param name="d">The element to get the property from</param>
        /// <returns></returns>
            public static Property GetValue(BindableObject d) => (Property)d.GetValue(ValueProperty);


            /// <summary>
            /// Sets the attached property
            /// </summary>
            /// <param name="d">The element to get the property from</param>
            /// <param name="value">The value to set the property to</param>
            public static void SetValue(BindableObject d, Property value) => d.SetValue(ValueProperty, value);

        #endregion

        #region Event Methods

        /// <summary>
        /// The method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="e">The arguments for this event</param>
        [SuppressPropertyChangedWarnings]
        public virtual void OnValueChanged(BindableObject sender, object oldValue, object newValue) { }

            /// <summary>
            /// The method that is called when any attached property of this type is changed, even if the value is the same
            /// </summary>
            /// <param name="sender">The UI element that this property was changed for</param>
            /// <param name="e">The arguments for this event</param>
            public virtual void OnValueUpdated(BindableObject sender, object value) { }

            #endregion
        }

    }

