using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using SocialNewsApp.View;
using Windows.Foundation;
using WinRT.Interop;
using Microsoft.UI.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SocialNewsApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public static MainWindow Instance;
        public static Microsoft.UI.Dispatching.DispatcherQueue UIDispatcher;
        public static Frame MainFrame;
        public static FrameworkElement AppTitleBar;
        public static ContentControl ContentControl;

        public MainWindow()
        {
            Instance = this;
            SetSystemBackdrop();
            SetTitleBar();

            this.InitializeComponent();

            var hwnd = WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 1000, Height = 900 });

            UIDispatcher = this.DispatcherQueue;
            ContentControl = PersonButton;

            MainFrame = mainFrame;
            MainFrame.Content = new StartPage();

            AppTitleBar = appTitleBar;
            AppTitleBar.SizeChanged += AppTitleBar_SizeChanged;
        }

        private void AppTitleBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ExtendsContentIntoTitleBar == true)
            {
                SetRegionsForCustomTitleBar(this, AppTitleBar, ContentControl);
            }
        }

        private void SetTitleBar()
        {
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(AppTitleBar);
        }

        private static void SetRegionsForCustomTitleBar(MainWindow mainWindow, UIElement appTitleBar, FrameworkElement activityContent)
        {
            double scaleAdjustment = appTitleBar.XamlRoot.RasterizationScale;

            var transform = activityContent.TransformToVisual(null);
            var bounds = transform.TransformBounds(new Rect(0, 0,
                                                        activityContent.ActualWidth,
                                                        activityContent.ActualHeight));
            var PersonPicRect = mainWindow.GetRect(bounds, scaleAdjustment);

            var rectArray = new Windows.Graphics.RectInt32[] { PersonPicRect };

            InputNonClientPointerSource nonClientInputSrc =
                InputNonClientPointerSource.GetForWindowId(mainWindow.AppWindow.Id);
            nonClientInputSrc.SetRegionRects(NonClientRegionKind.Passthrough, rectArray);
        }

        private Windows.Graphics.RectInt32 GetRect(Rect bounds, double scale)
        {
            return new Windows.Graphics.RectInt32(
                _X: (int)Math.Round(bounds.X * scale),
                _Y: (int)Math.Round(bounds.Y * scale),
                _Width: (int)Math.Round(bounds.Width * scale),
                _Height: (int)Math.Round(bounds.Height * scale)
            );
        }

        public static void LoadPersonPicture(UIElement uIElement)
        {
            ContentControl.Content = uIElement;
            SetRegionsForCustomTitleBar(Instance, AppTitleBar, ContentControl);
        }

        private void SetSystemBackdrop()
        {
            if(Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
            {
                Microsoft.UI.Xaml.Media.MicaBackdrop micaBackdrop = new Microsoft.UI.Xaml.Media.MicaBackdrop();
                micaBackdrop.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;
                this.SystemBackdrop = micaBackdrop;
            }
            else
            {
                if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
                {
                    Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop DesktopAcrylicBackdrop = new Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop();
                    this.SystemBackdrop = DesktopAcrylicBackdrop;
                }
            }
        }
    }
}