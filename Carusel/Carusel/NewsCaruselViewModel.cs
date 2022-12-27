using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Carusel
{
    public delegate void NewsCarusel();
    internal class NewsCaruselViewModel
    {
        public event NewsCarusel? Carusel;
        private ObservableCollection<ImageCarouselItemView> ImageCarouselItems { get; set; }
        public ObservableCollection<ImageCarouselItemView> ImageCarouselItemsCurrent { get; set; }
        public ObservableCollection<RadioButton> RadioButtons { get; set; }

        private int CurrentItem = 0;

           
        public NewsCaruselViewModel()
        {
            ImageCarouselItemsCurrent = new ObservableCollection<ImageCarouselItemView>();
            ImageCarouselItems = new ObservableCollection<ImageCarouselItemView>();
            RadioButtons = new ObservableCollection<RadioButton>();
            AddItemsInCarousel();

            ImageCarouselItemsCurrent.Add(ImageCarouselItems[0]);
            RadioButtons[0].IsChecked = true;

            Carusel += StartCarusel;
            Carusel?.Invoke();
        }


        public async void StartCarusel()
        {
            while (true)
            {
                await Task.Delay(3000);

                if (CurrentItem < RadioButtons.Count)
                {
                    ImageCarouselItemsCurrent.Clear();
                    ImageCarouselItemsCurrent.Add(ImageCarouselItems[RadioButtons[CurrentItem].TabIndex]);
                    RadioButtons[CurrentItem].IsChecked = true;
                    CurrentItem++;
                }
                else
                {
                    ImageCarouselItemsCurrent.Clear();
                    ImageCarouselItemsCurrent.Add(ImageCarouselItems[RadioButtons[0].TabIndex]);
                    RadioButtons[0].IsChecked = true;
                }
            }
        }

        private void AddItemsInCarousel()
        {
            #region AddItems
            ImageCarouselItems.Add(new ImageCarouselItemView() { ImageSource = "/Images/1.jpg", ButtonText = "BUTTON", Title = "TEXT", Text = "Information" });
            ImageCarouselItems.Add(new ImageCarouselItemView() { ImageSource = "/Images/2.png", ButtonText = "BUTTON", Title = "TEXT", Text = "Information" });
            ImageCarouselItems.Add(new ImageCarouselItemView() { ImageSource = "/Images/3.png", ButtonText = "BUTTON", Title = "TEXT", Text = "Information" });
            ImageCarouselItems.Add(new ImageCarouselItemView() { ImageSource = "/Images/4.png", ButtonText = "BUTTON", Title = "TEXT", Text = "Information" });
            #endregion

            for (int i = 0; i < ImageCarouselItems.Count; i++)
            {
                RadioButton radioButton = new RadioButton() { GroupName = "Items", TabIndex = i, Margin = new Thickness(0, 0, 16, 0), VerticalAlignment = VerticalAlignment.Center };
                radioButton.Click += Click;
                radioButton.Checked += Checked;
                RadioButtons.Add(radioButton);
            }
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            ImageCarouselItemsCurrent.Clear();
            ImageCarouselItemsCurrent.Add(ImageCarouselItems[RadioButtons[CurrentItem].TabIndex]);
            RadioButtons[CurrentItem].IsChecked = true;
        }

        private void Checked(object sender, RoutedEventArgs e)
        {
            var a = sender as RadioButton;
            CurrentItem = a.TabIndex;
        }

    }
}