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

namespace DataLogger.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LeftHand_UserControl.xaml
    /// </summary>
    public partial class LeftHand_UserControl : UserControl
    {
        #region Little Finger
        public static readonly DependencyProperty LittleOpenProperty =
    DependencyProperty.Register("LittleOpen", typeof(float), typeof(LeftHand_UserControl), new PropertyMetadata(0f));

        public float LittleOpen
        {
            get { return (float)GetValue(LittleOpenProperty); }
            set { SetValue(LittleOpenProperty, value); }
        }

        public static readonly DependencyProperty LittleHalfProperty =
DependencyProperty.Register("LittleHalf", typeof(float), typeof(LeftHand_UserControl), new PropertyMetadata(0f));

        public float LittleHalf
        {
            get { return (float)GetValue(LittleHalfProperty); }
            set { SetValue(LittleHalfProperty, value); }
        }
        #endregion

        #region Ring Finger
        public static readonly DependencyProperty RingOpenProperty =
DependencyProperty.Register("RingOpen", typeof(float), typeof(LeftHand_UserControl), new PropertyMetadata(0f));

        public float RingOpen
        {
            get { return (float)GetValue(RingOpenProperty); }
            set { SetValue(RingOpenProperty, value); }
        }

        public static readonly DependencyProperty RingHalfProperty =
DependencyProperty.Register("RingHalf", typeof(float), typeof(LeftHand_UserControl), new PropertyMetadata(0f));

        public float RingHalf
        {
            get { return (float)GetValue(RingHalfProperty); }
            set { SetValue(RingHalfProperty, value); }
        }
        #endregion
        
        #region Middle Finger
        public static readonly DependencyProperty MiddleOpenProperty =
DependencyProperty.Register("MiddleOpen", typeof(float), typeof(LeftHand_UserControl), new PropertyMetadata(0f));

        public float MiddleOpen
        {
            get { return (float)GetValue(MiddleOpenProperty); }
            set { SetValue(MiddleOpenProperty, value); }
        }

        public static readonly DependencyProperty MiddleHalfProperty =
DependencyProperty.Register("MiddleHalf", typeof(float), typeof(LeftHand_UserControl), new PropertyMetadata(0f));

        public float MiddleHalf
        {
            get { return (float)GetValue(MiddleHalfProperty); }
            set { SetValue(MiddleHalfProperty, value); }
        }
        #endregion

        #region Index Finger
                public static readonly DependencyProperty IndexOpenProperty =
        DependencyProperty.Register("IndexOpen", typeof(float), typeof(LeftHand_UserControl), new PropertyMetadata(0f));

                public float IndexOpen
                {
                    get { return (float)GetValue(IndexOpenProperty); }
                    set { SetValue(IndexOpenProperty, value); }
                }

                public static readonly DependencyProperty IndexHalfProperty =
        DependencyProperty.Register("IndexHalf", typeof(float), typeof(LeftHand_UserControl), new PropertyMetadata(0f));

                public float IndexHalf
                {
                    get { return (float)GetValue(IndexHalfProperty); }
                    set { SetValue(IndexHalfProperty, value); }
                }
        #endregion

        #region Mirroring
        public static readonly DependencyProperty IsMirroredProperty =
            DependencyProperty.Register("IsMirrored", typeof(bool), typeof(LeftHand_UserControl), new PropertyMetadata(false));

        public bool IsMirrored
        {
            get { return (bool)GetValue(IsMirroredProperty); }
            set { SetValue(IsMirroredProperty, value); }
        }
        #endregion

        public LeftHand_UserControl()
        {
            InitializeComponent();
        }
    }
}
