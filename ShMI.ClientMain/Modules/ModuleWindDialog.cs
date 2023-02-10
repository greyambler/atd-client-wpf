using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace ShMI.ClientMain.Modules
{
    public class ModuleWindDialog : INotifyPropertyChanged
    {
        public ModuleWindDialog()
        {
            //IconTypeWindow = InitIconTypeWindow();
        }

        #region INotifyPropertyChanged Members
        public string MChangeProperty
        {
            set
            {
                try
                {
                    NotifyPropertyChanged(value);
                }
                catch (Exception)
                {
                }
            }
            get => "";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged( string propertyName )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public BitmapFrame InitIconTypeWindow( string PathImage = "Icons/Spell.ico" )
        {
            try
            {
                BitmapFrame img = null;
                if (File.Exists(PathImage))
                {
                    FileStream fileStream = new FileStream(PathImage, FileMode.Open);
                    BitmapDecoder bitmapDecorder = BitmapDecoder.Create(fileStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    img = bitmapDecorder.Frames[0];
                    fileStream.Close();
                }
                return img;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private BitmapFrame _IconTypeWindow;
        public BitmapFrame IconTypeWindow
        {
            get => _IconTypeWindow;
            set
            {
                _IconTypeWindow = value;
                MChangeProperty = "IconTypeWindow";
            }
        }

    }
}
