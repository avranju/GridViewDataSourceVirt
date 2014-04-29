using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Certificates;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.Extensions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GridViewDataSourceVirt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Number of pixels offset from the right when incremental load should trigger
        /// as user scrolls the grid view.
        /// </summary>
        private const int INCREMENTAL_LOAD_OFFSET_TRIGGER = 100;
        ObservableCollection<PhotoGroup> photos = new ObservableCollection<PhotoGroup>();

        public MainPage()
        {
            this.InitializeComponent();
            Init();
        }

        async void Init()
        {
            progress.Visibility = Visibility.Visible;
            var pgs = await PhotosDataSource.LoadPhotos(8);
            progress.Visibility = Visibility.Collapsed;

            foreach (var pg in pgs)
            {
                photos.Add(pg);
            }

            photosSource.Source = photos;

            var scrollViewer = gridPhotos.GetFirstDescendantOfType<ScrollViewer>();
            scrollViewer.ViewChanging += scrollViewer_ViewChanging;
        }

        async void scrollViewer_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            ScrollViewer sv = sender as ScrollViewer;

            // if the difference between horizontal offset and scrollable width is less than
            // INCREMENTAL_LOAD_OFFSET_TRIGGER pixels load next group
            if (((sv.ScrollableWidth - e.FinalView.HorizontalOffset) < INCREMENTAL_LOAD_OFFSET_TRIGGER) &&
                (PhotosDataSource.IsBusy == false) &&
                (PhotosDataSource.HasMoreItems))
            {
                progress.Visibility = Visibility.Visible;
                var newPhotos = await PhotosDataSource.LoadPhotos(1);
                progress.Visibility = Visibility.Collapsed;

                foreach (var pg in newPhotos)
                {
                    photos.Add(pg);
                }
            }
        }
    }
}
