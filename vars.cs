using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LAS_to_TEXT
{
    internal class vars
    {
        public static string headText;
        public static ObservableCollection<GeoPoint> list = new ObservableCollection<GeoPoint>();
        public static ObservableCollection<Attributes> attributes = new ObservableCollection<Attributes>();
        public static double minx;
        public static double miny;
        public static double minz;
        public static double maxx;
        public static double maxy;
        public static double maxz;
    }
    public class GeoPoint // All Lidar points
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double NIR { get; set; }
        public int Classification { get; set; }
        public double Intensity { get; set; }
        public double ReturnNumber1 { get; set; }
        public double NumberOfReturns { get; set; }
        public double GPS_Time { get; set; }
        public byte? ScanAngle { get; set; }
        public ushort? PointSourceID { get; set; }
        public byte? EdgeOfFlightLine { get; set; }
        public byte? ScanDirectionFlag { get; set; }
        public Int16? UserData { get; set; }
        public ushort? R { get; set; }
        public ushort? G { get; set; }
        public ushort? B { get; set; }


    }
    public class Attributes : INotifyPropertyChanged
    {
        private bool _chk;
        public bool chk
        {
            get => _chk;
            set
            {
                if (_chk != value)
                {
                    _chk = value;
                    OnPropertyChanged();
                    Debug.WriteLine($"chk changed to {_chk} for {Name}");
                }
            }
        }

        public string Name { get; set; }
        public string Version { get; set; }
        public long Point_Count { get; set; }
        public int Point_Format { get; set; }
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        public double MinZ { get; set; }
        public double MaxZ { get; set; }
        public int CreationYear { get; set; }
        public int CreationDay { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
