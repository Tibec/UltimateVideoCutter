﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoCutterMax2.View
{
    /// <summary>
    /// Logique d'interaction pour VideoViewer.xaml
    /// </summary>
    public partial class VideoViewer : UserControl
    {

        TimeSpan _position;
        DispatcherTimer _timer = new DispatcherTimer();
        private bool _draggingSlider = false;

        public VideoViewer()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(SecondsTimer);
            _timer.Start();
        }

        /*
         * Refresh the slider position
         */
        void SecondsTimer(object sender, EventArgs e)
        {

            if ( _draggingSlider == false)
            {
                videoSlider.Value = mainVideo.Position.TotalSeconds;
                this.ShowTimer();
            }
           
        }

        private void videoPlay_click(object sender, RoutedEventArgs e)
        {

            if ((string)videoPlay.Content == "Play")
            {
                mainVideo.Play();
                videoPlay.Content = "Pause";
            }
            else
            {
                mainVideo.Pause();
                videoPlay.Content = "Play";
            }

        }
        
        private void Element_MediaOpened(object sender, EventArgs e)
        {
  
            _position = TimeSpan.MinValue;
            videoSlider.Minimum = 0;
            videoSlider.Maximum = mainVideo.NaturalDuration.TimeSpan.TotalSeconds;
            
        }


        /*
         * Actualize the video time in the text block
         */
        private void ShowTimer()
        {

            if (mainVideo.NaturalDuration != null)
            {
                videoTextTimer.Text = mainVideo.Position.ToString().Substring(0, 8) + "/" + mainVideo.NaturalDuration.ToString().Substring(0, 8);
            }
            
        }
        private void ShowTimer(TimeSpan e)
        {

            if (mainVideo.NaturalDuration != null)
            {
                videoTextTimer.Text = e.ToString().Substring(0, 8) + "/" + mainVideo.NaturalDuration.ToString().Substring(0, 8);
            }

        }
        /*
         * Slider Gestion
         * 
         */

        /*
         * To display the time when we hover (perhaps needs to be deleted)
         */
        private void HoverSlider(object sender, EventArgs e)
        {

            //System.Diagnostics.Debug.WriteLine(e);
          
            
            videoSlider.ToolTip = new TimeSpan(0, 0, 0, Convert.ToInt32(videoSlider.Value), 0).ToString().Substring(0, 8) + "/" + mainVideo.NaturalDuration.ToString().Substring(0, 8);
        }

        /*
         * Change the video moment
         */
        private void StartDrag(object sender, DragStartedEventArgs e)
        {
            _draggingSlider = true;
        }

        private void EndDrag(object sender, DragCompletedEventArgs e)
        {
            _draggingSlider = false;
            mainVideo.Position = TimeSpan.FromSeconds(videoSlider.Value);
        }

        private void ActualizeTime(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            this.ShowTimer(new TimeSpan(0, 0, 0, Convert.ToInt32(e.NewValue), 0));
            
        }

        /*
         * Begin and End button
         */


    }
}
