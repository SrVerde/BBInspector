using BBS.BBRZ;
using BBS.Converter;
using BBS.Models;
using Ltds.Wpf.Mvvm.Commands;
using Ltds.Wpf.Mvvm.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BBS
{
    class MainViewModel : BindableObject
    {
        public MainViewModel()
        {
            _replays = new ObservableCollection<BBMatch>();

        }

        private ObservableCollection<BBMatch> _replays;
        public ObservableCollection<BBMatch> Replays
        {
            get { return _replays; }
        }


        private BBMatch _selectedReplay;
        public BBMatch SelectedReplay
        {
            get
            {
                return _selectedReplay;
            }

            set
            {
                RaisePropertyChanged<BBMatch>(ref _selectedReplay, value, () => SelectedReplay);
            }
        }


        private ICommand _openFileCommand;
        public ICommand OpenFileCommand
        {
            get
            {
                if (_openFileCommand == null)
                    _openFileCommand = new ModelCommand(OpenFile);

                return _openFileCommand;
            }
        }

        private void OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "BB2 replay files (*.bbrz)|*.bbrz";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.Title = "Please select replay files to analyze";

            //  Go on only with "OK" button
            //
            if (dialog.ShowDialog() == true)
            {
                //  Process each selected file
                //
                foreach (string file in dialog.FileNames)
                {
                    //  Unzip
                    //
                    DateTime preZip = DateTime.Now;
                    string replayTextfile = Zipper.UnzipReplay(file);

                    //  Deserialize
                    //
                    DateTime preDeserialize = DateTime.Now;

                    //  If file already exists, go to the next one
                    //
                    if (File.Exists(replayTextfile) == false)
                    {
                        continue;
                    }

                    //  Read and interpret the file (and write CSV for interpretations!)
                    //
                    string data = File.ReadAllText(replayTextfile);
                    Replay replay = null;
                    try
                    {
                        replay = Replay.CreateFromString(data);
                        //BBRZ_2_CSV.Transform(replay, file);
                        _replays.Add(new BBMatch(replay));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Error abriendo el fichero {0}\n{1} ", replayTextfile, ex.Message));
                    }
                }
            }
        }

    }
}
