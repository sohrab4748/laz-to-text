using com.github.mreutegg.laszip4j;
using com.github.mreutegg.laszip4j.laslib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace LAS_to_TEXT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel _viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            dg.ItemsSource = vars.list;
            DataContext = this;
            DataContext = _viewModel;
        }
        #region ToolBar
        private void quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void restart_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
          System.Windows.Forms.Application.Restart();
        }
        private void aboutus_Click(object sender, RoutedEventArgs e)
        {
            new AboutUs().Show();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.toolB.Width = e.NewSize.Width - 15;
        }
        private void VisitWebsite_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.agrimetsoft.com/");
        }
        private void VisitProducts_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.agrimetsoft.com/products");
        }
        #endregion

        private void sellectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "LAZ files (*.laz)|*.laz|LAS files (*.las)|*.las|All files (*.*)|*.*",
                Title = "Select LAZ/LAS Files",
                Multiselect = false,
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    attTxt.Text = "";
                    saveAll.IsEnabled = false;
                    saveOption.IsEnabled = false;
                    readLazFile(openFileDialog.FileName);
                    saveAll.IsEnabled = true;
                    saveOption.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing files: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void readLazFile(string filePath)
        {
            // Ensure vars.list is initialized as ObservableCollection
            if (vars.list == null)
            {
                vars.list = new ObservableCollection<GeoPoint>();
            }
            else
            {
                vars.list.Clear();
            }

            java.io.File ff = new java.io.File(filePath);
            LASReader reader = new LASReader(ff);
            LASHeader header = reader.getHeader();
            vars.headText = header.ConvertLASHeader().ToFormattedString();
            attTxt.Text = vars.headText;
            var xSc = header.getXScaleFactor();
            var ySc = header.getYScaleFactor();
            var zSc = header.getZScaleFactor();
            var points = reader.getPoints();
            var intr = points.iterator();
            int totalSteps = (int)header.getNumberOfPointRecords();
            int i = 0;
            int batchSize = (int)(totalSteps / 100);
            var batchList = new List<GeoPoint>();

            await Task.Run(() =>
            {
                while (intr.hasNext())
                {
                    var point = (LASPoint)intr.next();
                    double x = point.getX();
                    double y = point.getY();
                    double z = point.getZ();
                    var rn = point.getReturnNumber();
                    var rn1 = point.getNumberOfReturns();
                    int intensity = point.getIntensity();
                    double gpst = point.getGPSTime();
                  //  var firstDateTime = DateTime.SpecifyKind(new DateTime(1980, 1, 6, 0, 0, 0, DateTimeKind.Utc).AddSeconds(gpst), DateTimeKind.Utc).ToString("yyyy-MM-dd HH:mm:ss.fff UTC");
                    var ang = (byte)point.getScanAngleRank();
                    ushort si = (ushort)point.getPointSourceID();
                    var ef = (byte)point.getEdgeOfFlightLine();
                    var dr= (byte)point.getScanDirectionFlag();
                    var ud = point.getUserData();
                    int n = point.getNIR();
                    int clas = point.getClassification();

                    var geoPoint = new GeoPoint()
                    {
                        X = x,
                        Y = y,
                        Z = z,
                        Intensity = intensity,
                        Classification = clas,
                        NIR = n,
                        GPS_Time = gpst,
                        ReturnNumber1 = rn,
                        NumberOfReturns = rn1,
                        ScanAngle = ang,
                        PointSourceID = si,
                        EdgeOfFlightLine = ef,
                        ScanDirectionFlag=dr,
                        UserData=ud,
                    };

                    if (point.hasRGB())
                    {
                        geoPoint.R = point.getRed();
                        geoPoint.G = point.getGreen();
                        geoPoint.B = point.getBlue();
                    }
                    batchList.Add(geoPoint);

                    if (batchList.Count >= batchSize || !intr.hasNext())
                    {
                        Dispatcher.Invoke(() =>
                        {
                            foreach (var item in batchList)
                            {
                                vars.list.Add(item);
                            }
                         _viewModel.ProgressValue = i * (100.0 / totalSteps);
                        });
                        batchList.Clear();
                    }

                    i++;
                }
            });

            vars.minx = vars.list.Min(p => p.X);
            vars.miny = vars.list.Min(p => p.Y);
            vars.minz = vars.list.Min(p => p.Z);
            vars.maxx = vars.list.Max(p => p.X);
            vars.maxy = vars.list.Max(p => p.Y);
            vars.maxz = vars.list.Max(p => p.Z);

            // No need for dg.ItemsSource = vars.list here, as it's already set in XAML.
            // dg.UpdateLayout(); // Not usually needed with ObservableCollection
        }

        private void saveAll_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|CSV Comma Delimiter (*.csv)|*.csv";
            saveFileDialog1.RestoreDirectory = true;
            var res = saveFileDialog1.ShowDialog();
            if (res.Value)
            {
                FileInfo fileInfo = new FileInfo(saveFileDialog1.FileName);
                sellectFile.IsEnabled = false;
                saveAll.IsEnabled = false;
                if (fileInfo.Extension == ".txt")
                {
                    string str = vars.headText;
                    str += Environment.NewLine + "==========Points========" + Environment.NewLine;
                    str += vars.list.ToTextString();
                    System.IO.File.WriteAllText(saveFileDialog1.FileName, str);
                    MessageBox.Show("The file has been saved.");
                }
                else if (fileInfo.Extension == ".csv")
                {
                    string str = vars.headText;
                    str += Environment.NewLine + "==========Points========" + Environment.NewLine;
                    str += vars.list.ToCsvString();
                    System.IO.File.WriteAllText(saveFileDialog1.FileName, str);
                    MessageBox.Show("The file has been saved.");
                }
                else { MessageBox.Show("Please enter the correct file extension: .csv or .txt format."); }
                sellectFile.IsEnabled = true;
                saveAll.IsEnabled = true;
            }
        }

        private void saveOption_Click(object sender, RoutedEventArgs e)
        {
            // Create a dictionary of header-property mappings
            Dictionary<string, string> columns = new Dictionary<string, string>();
            foreach (var col in dg.Columns)
            {
                columns.Add(col.Header.ToString(), col.SortMemberPath); // Use SortMemberPath
            }

            SaveOptionsWindow optionsWindow = new SaveOptionsWindow(columns);
            bool? result = optionsWindow.ShowDialog();

            if (result == true)
            {
                // Access selected options from optionsWindow
                bool saveHeader = optionsWindow.chkSaveHeader.IsChecked ?? false;
                List<string> selectedColumns = optionsWindow.lstColumnOrder.Items.Cast<string>().
                    Select(v => columns[v]).ToList();

                // Implement your save logic here using saveHeader and selectedColumns
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|CSV Comma Delimiter (*.csv)|*.csv";
                saveFileDialog1.RestoreDirectory = true;
                var res = saveFileDialog1.ShowDialog();
                if (res.Value)
                {
                    FileInfo fileInfo = new FileInfo(saveFileDialog1.FileName);
                    sellectFile.IsEnabled = false;
                    saveAll.IsEnabled = false;
                    var fn = saveFileDialog1.FileName;
                    if (fileInfo.Extension == ".txt")
                    {
                        string str = "";
                        if (saveHeader)
                        {
                            fn = saveFileDialog1.FileName.Replace(fileInfo.Extension, "_Points" + fileInfo.Extension);
                            System.IO.File.WriteAllText(saveFileDialog1.FileName.Replace(fileInfo.Extension, "_Header" + fileInfo.Extension), vars.headText);
                        }
                        else
                        {
                            str = vars.headText;
                            str += Environment.NewLine + "==========Points========" + Environment.NewLine;
                        }
                        str += vars.list.ToTextString(selectedColumns);
                        System.IO.File.WriteAllText(fn, str);
                        MessageBox.Show("The file has been saved.");
                    }
                    else if (fileInfo.Extension == ".csv")
                    {
                        string str = "";
                        if (saveHeader)
                        {
                            fn = saveFileDialog1.FileName.Replace(fileInfo.Extension, "_Points" + fileInfo.Extension);
                            System.IO.File.WriteAllText(saveFileDialog1.FileName.Replace(fileInfo.Extension, "_header" + fileInfo.Extension), vars.headText);
                        }
                        else
                        {
                            str = vars.headText;
                            str += Environment.NewLine + "==========Points========" + Environment.NewLine;
                        }
                        str += vars.list.ToCsvString(selectedColumns);
                        System.IO.File.WriteAllText(fn, str);
                        MessageBox.Show("The file has been saved.");
                    }
                    else { MessageBox.Show("Please enter the correct file extension: .csv or .txt format."); }
                    sellectFile.IsEnabled = true;
                    saveAll.IsEnabled = true;
                }
            }
        }

        private void Dg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Customize the header based on the property name (e.PropertyName)
            switch (e.PropertyName)
            {
                case "X":
                    e.Column.Header = "X Coordinate";
                    break;
                case "Y":
                    e.Column.Header = "Y Coordinate";
                    break;
                case "Z":
                    e.Column.Header = "Z Coordinate";
                    break;
                case "NIR":
                    e.Column.Header = "Near-Infrared";
                    break;
                case "Classification":
                    e.Column.Header = "Point Classification";
                    break;
                case "Intensity":
                    e.Column.Header = "Point Intensity";
                    break;
                case "GPS_Time":
                    e.Column.Header = "GPS Time";
                    break;
                case "UserData":
                    e.Column.Header = "User Data";
                    break;
                case "ScanAngle":
                    e.Column.Header = "Scan Angle";
                    break;
                case "PointSourceID":
                    e.Column.Header = "Point Source ID";
                    break;
                case "EdgeOfFlightLine":
                    e.Column.Header = "Flight Line Edge";
                    break;
                case "ScanDirectionFlag":
                    e.Column.Header = "Scan Direction";
                    break;
                case "R":
                    e.Column.Header = "Red";
                    break;
                case "G":
                    e.Column.Header = "Green";
                    break;
                case "B":
                    e.Column.Header = "Blue";
                    break;
                    // Add more cases for other properties as needed
            }
        }

    }
    class ViewModel : INotifyPropertyChanged
    {
        private double _progressValue;

        public double ProgressValue
        {
            get { return _progressValue; }
            set { _UpdatePropertyField(ref _progressValue, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void _UpdatePropertyField<T>(
            ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class TooltipDataGrid : DataGrid
    {
        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.OnAutoGeneratingColumn(e);

            e.Column.CellStyle = new Style(typeof(DataGridCell))
            {
                Setters = { new Setter(ToolTipProperty, new Binding(e.PropertyName)) }
            };
        }
    }
}
