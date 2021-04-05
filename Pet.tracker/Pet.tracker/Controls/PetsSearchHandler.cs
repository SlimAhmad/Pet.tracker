using Pet.Tracker.Core;
using PropertyChanged;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pet.Tracker
{

    [SuppressPropertyChangedWarnings]
    public class PetsSearchHandler : SearchHandler
    {
      

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = PetsData.Items
                    .Where(key => key.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);

            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            await Shell.Current.GoToAsync($"missing?name={((MissingPetsApiModel)item).Name}");
        }
    }
}
