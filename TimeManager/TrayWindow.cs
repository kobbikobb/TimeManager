using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using TimeManagerLib.Model;
using TimeManagerLib.View;

namespace TimeManager
{
    public class TrayWindow
    {
        private WindowView _window;
        private static TrayWindow _instance;

        private TrayWindow()
        {
            
        }

        public static TrayWindow Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TrayWindow();
                return _instance;
            }
        }

        public bool IsShowing
        {
            get { return _window != null && _window.IsVisible; }
        }

        public bool Show(ViewModelBase viewModel)
        {
            if(_window == null)
                _window = new WindowView();

            var refreashable = viewModel as IRefreashable;
            if (refreashable != null)
                refreashable.Refreash();

            if (_window.DataContext != null && _window.DataContext.Equals(viewModel))
            {
                return false;
            }
            
            if(_window.IsVisible)
            {
                _window.Focus();
                return false;
            }

            _window.Title = viewModel.ToString();
            _window.DataContext = viewModel;

            var result = _window.ShowDialog();        

            _window = null;

            return result == true;
        }


        //private readonly ViewModelBase _viewModel;
        

        //private readonly string _windowName;
        //private readonly int _windowHeight;
        //private readonly int _windowWidth;

        //public TrayWindow(ViewModelBase viewModel, string windowName, int windowHeight = 600, int windowWidth = 860)
        //{
        //    _viewModel = viewModel;

        //    _windowName = windowName;
        //    _windowHeight = windowHeight;
        //    _windowWidth = windowWidth;
        //}

        //public bool IsShowing
        //{
        //    get { return _window != null && _window.IsVisible; }
        //}

        //public bool Show()
        //{
        //    var refreashable = _viewModel as IRefreashable;
        //    if (refreashable != null)
        //        refreashable.Refreash();

        //    if (IsShowing)
        //    {
        //        _window.Focus();

        //        return true;
        //    }

        //    try
        //    {
        //        _window = new WindowView() { Height = _windowHeight, Width = _windowWidth, Title = _windowName };
        //        _window.DataContext = _viewModel;
        //        return _window.ShowDialog() == true;        
        //    }
        //    finally
        //    {
        //        _window = null;
        //    }
        //}
    }
}
