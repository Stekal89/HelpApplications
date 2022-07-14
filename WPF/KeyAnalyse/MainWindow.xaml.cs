using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyAnalyse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.System)
            { 
                //string pressed = $"{e.Key} + {e.SystemKey}";
                lblPressedKey.Content = $"{e.Key} + {e.SystemKey}";
                return;
            }

            

            //Key key = new Key();
            

            //e = new KeyEventArgs(Keyboard.PrimaryDevice, PresentationSource.CurrentSources, 0, new Key())

            lblPressedKey.Content = e.Key.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tbxTextBox.Focus();

            WindowsInput.InputSimulator inputSimulator = new WindowsInput.InputSimulator();
            inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_A);
            inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_B);
            inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.VK_C);

            inputSimulator.Keyboard.ModifiedKeyStroke(WindowsInput.Native.VirtualKeyCode.LMENU, new[] {
                WindowsInput.Native.VirtualKeyCode.VK_K,
                WindowsInput.Native.VirtualKeyCode.VK_C
            });
            inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.EXECUTE);

            //SendDown(Key.LeftAlt);
            //SendDown(Key.A);
            //SendUp(Key.A);
            //SendDown(Key.B);
            //SendUp(Key.B);
            //SendDown(Key.C);
            //SendUp(Key.C);
        }

        /// <summary>
        ///   Sends the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void SendDown(System.Windows.Input.Key key)
        {
            if (System.Windows.Input.Keyboard.PrimaryDevice != null)
            {
                if (System.Windows.Input.Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new System.Windows.Input.KeyEventArgs(System.Windows.Input.Keyboard.PrimaryDevice, System.Windows.Input.Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = System.Windows.Input.Keyboard.KeyDownEvent
                    };



                    System.Windows.Input.InputManager.Current.ProcessInput(e);

                    // Note: Based on your requirements you may also need to fire events for:
                    // RoutedEvent = Keyboard.PreviewKeyDownEvent
                    // RoutedEvent = Keyboard.KeyUpEvent
                    // RoutedEvent = Keyboard.PreviewKeyUpEvent
                }
            }
        }

        public static void SendUp(System.Windows.Input.Key key)
        {
            if (System.Windows.Input.Keyboard.PrimaryDevice != null)
            {
                if (System.Windows.Input.Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new System.Windows.Input.KeyEventArgs(System.Windows.Input.Keyboard.PrimaryDevice, System.Windows.Input.Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = System.Windows.Input.Keyboard.KeyUpEvent
                    };



                    System.Windows.Input.InputManager.Current.ProcessInput(e);

                    // Note: Based on your requirements you may also need to fire events for:
                    // RoutedEvent = Keyboard.PreviewKeyDownEvent
                    // RoutedEvent = Keyboard.KeyUpEvent
                    // RoutedEvent = Keyboard.PreviewKeyUpEvent
                }
            }
        }
    }
}
