using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using com.github.mreutegg.laszip4j;
using com.github.mreutegg.laszip4j.laslib;
using LAS_to_TEXT;
public static class LASheaderExtensions
{
    public static LASHeaderData ConvertLASHeader(this LASHeader header)
    {
        if (header == null)
        {
            return null; // Or throw an exception
        }

        var data = new LASHeaderData();
        try { data.FileSignature = header.getFileSignature(); } catch { }
        try { data.FileSourceID = header.getFileSourceID(); } catch { }
        try { data.GlobalEncoding = header.getGlobalEncoding(); } catch { }
        try { data.ProjectID_GUIDData1 = header.getProjectID_GUIDData1(); } catch { }
        try { data.ProjectID_GUIDData2 = header.getProjectID_GUIDData2(); } catch { }
        try { data.ProjectID_GUIDData3 = header.getProjectID_GUIDData3(); } catch { }
        try { data.ProjectID_GUIDData4 = header.getProjectID_GUIDData4(); } catch { }
        try { data.VersionMajor = header.getVersionMajor(); } catch { }
        try { data.VersionMinor = header.getVersionMinor(); } catch { }
        try { data.SystemIdentifier = header.getSystemIdentifier(); } catch { }
        try { data.GeneratingSoftware = header.getGeneratingSoftware(); } catch { }
        try { data.FileCreationDayOfYear = header.getFileCreationDayOfYear(); } catch { }
        try { data.FileCreationYear = header.getFileCreationYear(); } catch { }
        try { data.HeaderSize = header.getHeaderSize(); } catch { }
        try { data.OffsetToPointData = header.getOffsetToPointData(); } catch { }
        try { data.NumberOfVariableLengthRecords = header.getNumberOfVariableLengthRecords(); } catch { }
        try { data.PointDataRecordFormat = header.getPointDataRecordFormat(); } catch { }
        try { data.PointDataRecordLength = header.getPointDataRecordLength(); } catch { }
        try { data.LegacyNumberOfPointRecords = header.getLegacyNumberOfPointRecords(); } catch { }
        try { data.LegacyNumberOfPointsByReturn = header.getLegacyNumberOfPointsByReturn(); } catch { }
        try { data.XScaleFactor = header.getXScaleFactor(); } catch { }
        try { data.YScaleFactor = header.getYScaleFactor(); } catch { }
        try { data.ZScaleFactor = header.getZScaleFactor(); } catch { }
        try { data.XOffset = header.getXOffset(); } catch { }
        try { data.YOffset = header.getYOffset(); } catch { }
        try { data.ZOffset = header.getZOffset(); } catch { }
        try { data.MaxX = header.getMaxX(); } catch { }
        try { data.MinX = header.getMinX(); } catch { }
        try { data.MaxY = header.getMaxY(); } catch { }
        try { data.MinY = header.getMinY(); } catch { }
        try { data.MaxZ = header.getMaxZ(); } catch { }
        try { data.MinZ = header.getMinZ(); } catch { }
        try { data.StartOfWaveformDataPacketRecord = header.getStartOfWaveformDataPacketRecord(); } catch { }
        try { data.StartOfFirstExtendedVariableLengthRecord = header.getStartOfFirstExtendedVariableLengthRecord(); } catch { }
        try { data.NumberOfExtendedVariableLengthRecords = header.getNumberOfExtendedVariableLengthRecords(); } catch { }
        try { data.NumberOfPointRecords = header.getNumberOfPointRecords(); } catch { }
        try { data.NumberOfPointsByReturn = header.getNumberOfPointsByReturn(); } catch { }
        try { data.VariableLengthRecords = ConvertIterableToList<LASVariableLengthRecord>(header.getVariableLengthRecords()).AsEnumerable(); } catch { }
        try {
            var variableLengthRecordsIterable = header.getVariableLengthRecords();
            if (variableLengthRecordsIterable != null)
            {
                data.VariableLengthRecords = ConvertIterableToList<LASVariableLengthRecord>(variableLengthRecordsIterable).AsEnumerable();
            }
            else
            {
                data.VariableLengthRecords = Enumerable.Empty<LASVariableLengthRecord>(); // Or handle null as needed
            }
        } catch { }
        try { data.ExtraBytesDescriptions = ConvertIterableToList<LASExtraBytesDescription>(header.getExtraBytesDescriptions()).AsEnumerable(); } catch { }
        return data;
    }
    public static LASheaderData ConvertLASHeader(this LASheader header)
    {
        if (header == null)
        {
            return null; // Or throw an exception
        }

        var data = new LASheaderData
        {
            FileSignature = stringFromByteArray(header.file_signature), // [cite: 9, 10]
            FileSourceID = header.file_source_ID, // [cite: 11, 12]
            GlobalEncoding = header.global_encoding, // [cite: 13, 14]
            ProjectID_GUIDData1 = header.project_ID_GUID_data_1, // [cite: 15, 16, 17, 18, 19, 20, 21, 22, 23]
            ProjectID_GUIDData2 = header.project_ID_GUID_data_2,
            ProjectID_GUIDData3 = header.project_ID_GUID_data_3,
            ProjectID_GUIDData4 = header.project_ID_GUID_data_4,
            VersionMajor = header.version_major, // [cite: 24, 25, 26, 27, 28, 29, 30]
            VersionMinor = header.version_minor,
            SystemIdentifier = stringFromByteArray(header.system_identifier),
            GeneratingSoftware = stringFromByteArray(header.generating_software),
            FileCreationDayOfYear = header.file_creation_day, // [cite: 31, 32, 33, 34, 35, 36, 37, 38, 39, 40]
            FileCreationYear = header.file_creation_year,
            HeaderSize = header.header_size,
            OffsetToPointData = header.offset_to_point_data,
            NumberOfVariableLengthRecords = header.number_of_variable_length_records,
            PointDataRecordFormat = header.point_data_format, // [cite: 41, 42, 43, 44, 45, 46, 47, 48, 49]
            PointDataRecordLength = header.point_data_record_length,
            XScaleFactor = header.x_scale_factor, // [cite: 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72]
            YScaleFactor = header.y_scale_factor,
            ZScaleFactor = header.z_scale_factor,
            XOffset = header.x_offset,
            YOffset = header.y_offset,
            ZOffset = header.z_offset,
            MaxX = header.max_x,
            MinX = header.min_x,
            MaxY = header.max_y,
            MinY = header.min_y,
            MaxZ = header.max_z,
            MinZ = header.min_z,
            StartOfWaveformDataPacketRecord = header.start_of_waveform_data_packet_record, // [cite: 73, 74, 75, 76, 77, 78, 79, 80, 81, 82]
            StartOfFirstExtendedVariableLengthRecord = header.start_of_first_extended_variable_length_record,
            NumberOfExtendedVariableLengthRecords = header.number_of_extended_variable_length_records,
            NumberOfPointRecords = header.number_of_point_records,
            NumberOfPointsByReturn = header.number_of_points_by_return,

        };


        return data;
    }

    public static List<T> ConvertIterableToList<T>(java.lang.Iterable iterable)
    {
        var list = new List<T>();
        var iterator = iterable.iterator();
        while (iterator.hasNext())
        {
            list.Add((T)iterator.next());
        }
        return list;
    }
    public class LASHeaderData
    {
        public string FileSignature { get; set; }
        public char FileSourceID { get; set; }
        public char GlobalEncoding { get; set; }
        public int ProjectID_GUIDData1 { get; set; }
        public char ProjectID_GUIDData2 { get; set; }
        public char ProjectID_GUIDData3 { get; set; }
        public byte[] ProjectID_GUIDData4 { get; set; }
        public byte VersionMajor { get; set; }
        public byte VersionMinor { get; set; }
        public string SystemIdentifier { get; set; }
        public string GeneratingSoftware { get; set; }
        public char FileCreationDayOfYear { get; set; }
        public char FileCreationYear { get; set; }
        public char HeaderSize { get; set; }
        public int OffsetToPointData { get; set; }
        public int NumberOfVariableLengthRecords { get; set; }
        public byte PointDataRecordFormat { get; set; }
        public char PointDataRecordLength { get; set; }
        public int LegacyNumberOfPointRecords { get; set; }
        public int[] LegacyNumberOfPointsByReturn { get; set; }
        public double XScaleFactor { get; set; }
        public double YScaleFactor { get; set; }
        public double ZScaleFactor { get; set; }
        public double XOffset { get; set; }
        public double YOffset { get; set; }
        public double ZOffset { get; set; }
        public double MaxX { get; set; }
        public double MinX { get; set; }
        public double MaxY { get; set; }
        public double MinY { get; set; }
        public double MaxZ { get; set; }
        public double MinZ { get; set; }
        public long StartOfWaveformDataPacketRecord { get; set; }
        public long StartOfFirstExtendedVariableLengthRecord { get; set; }
        public int NumberOfExtendedVariableLengthRecords { get; set; }
        public long NumberOfPointRecords { get; set; }
        public long[] NumberOfPointsByReturn { get; set; }
        public IEnumerable<LASVariableLengthRecord> VariableLengthRecords { get; set; }
        public IEnumerable<LASExtendedVariableLengthRecord> ExtendedVariableLengthRecords { get; set; }
        public IEnumerable<LASExtraBytesDescription> ExtraBytesDescriptions { get; set; }

    }
    public class LASheaderData
    {
        public string FileSignature { get; set; }
        public char FileSourceID { get; set; }
        public char GlobalEncoding { get; set; }
        public int ProjectID_GUIDData1 { get; set; }
        public char ProjectID_GUIDData2 { get; set; }
        public char ProjectID_GUIDData3 { get; set; }
        public byte[] ProjectID_GUIDData4 { get; set; }
        public byte VersionMajor { get; set; }
        public byte VersionMinor { get; set; }
        public string SystemIdentifier { get; set; }
        public string GeneratingSoftware { get; set; }
        public char FileCreationDayOfYear { get; set; }
        public char FileCreationYear { get; set; }
        public char HeaderSize { get; set; }
        public int OffsetToPointData { get; set; }
        public int NumberOfVariableLengthRecords { get; set; }
        public byte PointDataRecordFormat { get; set; }
        public char PointDataRecordLength { get; set; }
        public double XScaleFactor { get; set; }
        public double YScaleFactor { get; set; }
        public double ZScaleFactor { get; set; }
        public double XOffset { get; set; }
        public double YOffset { get; set; }
        public double ZOffset { get; set; }
        public double MaxX { get; set; }
        public double MinX { get; set; }
        public double MaxY { get; set; }
        public double MinY { get; set; }
        public double MaxZ { get; set; }
        public double MinZ { get; set; }
        public long StartOfWaveformDataPacketRecord { get; set; }
        public long StartOfFirstExtendedVariableLengthRecord { get; set; }
        public int NumberOfExtendedVariableLengthRecords { get; set; }
        public long NumberOfPointRecords { get; set; }
        public int[] NumberOfPointsByReturn { get; set; }

    }
    public static string ToFormattedString(this LASHeaderData data)
    {
        if (data == null)
        {
            return "LASHeaderData object is null.";
        }

        var sb = new StringBuilder();

        sb.AppendLine("LASHeaderData:");
        sb.AppendLine("--------------");

        sb.AppendLine($"File Signature: {data.FileSignature}");

        // Handle char fields explicitly (casting to int)
        sb.AppendLine($"File Source ID: {(int)data.FileSourceID}");
        sb.AppendLine($"Global Encoding: {(int)data.GlobalEncoding}");
        sb.AppendLine($"Project ID - GUID Data 2: {(int)data.ProjectID_GUIDData2}");
        sb.AppendLine($"Project ID - GUID Data 3: {(int)data.ProjectID_GUIDData3}");
        sb.AppendLine($"File Creation Day Of Year: {(int)data.FileCreationDayOfYear}");
        sb.AppendLine($"File Creation Year: {(int)data.FileCreationYear}");
        sb.AppendLine($"Header Size: {(int)data.HeaderSize}");
        sb.AppendLine($"Point Data Record Length: {(int)data.PointDataRecordLength}");

        // Handle byte
        sb.AppendLine($"Project ID - GUID Data 4: {BitConverter.ToString(data.ProjectID_GUIDData4).Replace("-", ", ")}");

        sb.AppendLine($"Version Major: {data.VersionMajor}");
        sb.AppendLine($"Version Minor: {data.VersionMinor}");
        sb.AppendLine($"System Identifier: {data.SystemIdentifier}");
        sb.AppendLine($"Generating Software: {data.GeneratingSoftware}");
        sb.AppendLine($"Offset To Point Data: {data.OffsetToPointData}");
        sb.AppendLine($"Number Of Variable Length Records: {data.NumberOfVariableLengthRecords}");
        sb.AppendLine($"Point Data Record Format: {data.PointDataRecordFormat}");
        sb.AppendLine($"Legacy Number Of Point Records: {data.LegacyNumberOfPointRecords}");
        sb.AppendLine($"X Scale Factor: {data.XScaleFactor}");
        sb.AppendLine($"Y Scale Factor: {data.YScaleFactor}");
        sb.AppendLine($"Z Scale Factor: {data.ZScaleFactor}");
        sb.AppendLine($"X Offset: {data.XOffset}");
        sb.AppendLine($"Y Offset: {data.YOffset}");
        sb.AppendLine($"Z Offset: {data.ZOffset}");
        sb.AppendLine($"Max X: {data.MaxX}");
        sb.AppendLine($"Min X: {data.MinX}");
        sb.AppendLine($"Max Y: {data.MaxY}");
        sb.AppendLine($"Min Y: {data.MinY}");
        sb.AppendLine($"Max Z: {data.MaxZ}");
        sb.AppendLine($"Min Z: {data.MinZ}");
        sb.AppendLine($"Start Of Waveform Data Packet Record: {data.StartOfWaveformDataPacketRecord}");
        sb.AppendLine($"Start Of First Extended Variable Length Record: {data.StartOfFirstExtendedVariableLengthRecord}");
        sb.AppendLine($"Number Of Extended Variable Length Records: {data.NumberOfExtendedVariableLengthRecords}");
        sb.AppendLine($"Number Of Point Records: {data.NumberOfPointRecords}");
        sb.AppendLine($"Number Of Points By Return: {string.Join(", ", data.NumberOfPointsByReturn)}");
        sb.AppendLine($"Legacy Number Of Points By Return: {string.Join(", ", data.LegacyNumberOfPointsByReturn)}");

        // Handle IEnumerable<T> properties
        sb.AppendLine("Variable Length Records:");
        if (data.VariableLengthRecords != null && data.VariableLengthRecords.Any())
        {
            foreach (var record in data.VariableLengthRecords)
            {
                sb.AppendLine($"  - {record}"); // Or format the record as needed
            }
        }
        else
        {
            sb.AppendLine("  - None");
        }

        sb.AppendLine("Extended Variable Length Records:");
        if (data.ExtendedVariableLengthRecords != null && data.ExtendedVariableLengthRecords.Any())
        {
            foreach (var record in data.ExtendedVariableLengthRecords)
            {
                sb.AppendLine($"  - {record}"); // Or format the record as needed
            }
        }
        else
        {
            sb.AppendLine("  - None");
        }

        sb.AppendLine("Extra Bytes Descriptions:");
        if (data.ExtraBytesDescriptions != null && data.ExtraBytesDescriptions.Any())
        {
            foreach (var description in data.ExtraBytesDescriptions)
            {
                sb.AppendLine($"  - {description}"); // Or format the description as needed
            }
        }
        else
        {
            sb.AppendLine("  - None");
        }

        return sb.ToString();
    }
    public static string ToFormattedString(this LASheaderData data)
        {
            if (data == null)
            {
                return "LASHeaderData object is null.";
            }

            var sb = new StringBuilder();

            sb.AppendLine("LASHeaderData:");
            sb.AppendLine("--------------");

            sb.AppendLine($"File Signature: {data.FileSignature}");

            // Handle char fields explicitly
            sb.AppendLine($"File Source ID: {(int)data.FileSourceID}"); // Cast to int to see the numeric value
            sb.AppendLine($"Global Encoding: {(int)data.GlobalEncoding}");
            sb.AppendLine($"Project ID - GUID Data 2: {(int)data.ProjectID_GUIDData2}");
            sb.AppendLine($"Project ID - GUID Data 3: {(int)data.ProjectID_GUIDData3}");
            sb.AppendLine($"File Creation Day Of Year: {(int)data.FileCreationDayOfYear}");
            sb.AppendLine($"File Creation Year: {(int)data.FileCreationYear}");
            sb.AppendLine($"Header Size: {(int)data.HeaderSize}");
            sb.AppendLine($"Point Data Record Length: {(int)data.PointDataRecordLength}");

            // Handle byte
            sb.AppendLine($"Project ID - GUID Data 4: {BitConverter.ToString(data.ProjectID_GUIDData4).Replace("-", ", ")}");

            sb.AppendLine($"Version Major: {data.VersionMajor}");
            sb.AppendLine($"Version Minor: {data.VersionMinor}");
            sb.AppendLine($"System Identifier: {data.SystemIdentifier}");
            sb.AppendLine($"Generating Software: {data.GeneratingSoftware}");
            sb.AppendLine($"Offset To Point Data: {data.OffsetToPointData}");
            sb.AppendLine($"Number Of Variable Length Records: {data.NumberOfVariableLengthRecords}");
            sb.AppendLine($"Point Data Record Format: {data.PointDataRecordFormat}");
            sb.AppendLine($"X Scale Factor: {data.XScaleFactor}");
            sb.AppendLine($"Y Scale Factor: {data.YScaleFactor}");
            sb.AppendLine($"Z Scale Factor: {data.ZScaleFactor}");
            sb.AppendLine($"X Offset: {data.XOffset}");
            sb.AppendLine($"Y Offset: {data.YOffset}");
            sb.AppendLine($"Z Offset: {data.ZOffset}");
            sb.AppendLine($"Max X: {data.MaxX}");
            sb.AppendLine($"Min X: {data.MinX}");
            sb.AppendLine($"Max Y: {data.MaxY}");
            sb.AppendLine($"Min Y: {data.MinY}");
            sb.AppendLine($"Max Z: {data.MaxZ}");
            sb.AppendLine($"Min Z: {data.MinZ}");
            sb.AppendLine($"Start Of Waveform Data Packet Record: {data.StartOfWaveformDataPacketRecord}");
            sb.AppendLine($"Start Of First Extended Variable Length Record: {data.StartOfFirstExtendedVariableLengthRecord}");
            sb.AppendLine($"Number Of Extended Variable Length Records: {data.NumberOfExtendedVariableLengthRecords}");
            sb.AppendLine($"Number Of Point Records: {data.NumberOfPointRecords}");
            sb.AppendLine($"Number Of Points By Return: {string.Join(", ", data.NumberOfPointsByReturn)}");

            return sb.ToString();
        }
    private static string stringFromByteArray(byte[] bytes)
    {
        return com.github.mreutegg.laszip4j.laszip.MyDefs.stringFromByteArray(bytes);
    }
    public static string ToCsvString(this ObservableCollection<GeoPoint> geoPoints)
    {
        if (geoPoints == null)
        {
            return ""; // Or throw an exception if you prefer
        }

        var sb = new StringBuilder();

        // Add the header row
        sb.AppendLine("X,Y,Z,NIR,Classification,Intensity,ReturnNumber1,NumberOfReturns,DateTime,ScanAngle,PointSourceID,EdgeOfFlightLine,ScanDirectionFlag,UserData,Red,Green,Blue");

        foreach (var point in geoPoints)
        {
            // Format each row
            sb.AppendLine($"{point.X},{point.Y},{point.Z},{point.NIR},{point.Classification},{point.Intensity},{point.ReturnNumber1},{point.NumberOfReturns},{point.GPS_Time},{point.ScanAngle},{point.PointSourceID},{point.EdgeOfFlightLine},{point.ScanDirectionFlag},{point.UserData},{point.R},{point.G},{point.B}");
        }

        return sb.ToString();
    }
    public static string ToTextString(this ObservableCollection<GeoPoint> geoPoints)
    {
        if (geoPoints == null)
        {
            return ""; // Or throw an exception if you prefer
        }

        var sb = new StringBuilder();

        // Add the header row
        sb.AppendLine("X\tY\tZ\tNIR\tClassification\tIntensity\tReturnNumber1\tNumberOfReturns\tDateTime\tScanAngle\tPointSourceID\tEdgeOfFlightLine\tScanDirectionFlag\tUserData\tRed\tGreen\tBlue");

        foreach (var point in geoPoints)
        {
            // Format each row
            sb.AppendLine($"{point.X}\t{point.Y}\t{point.Z}\t{point.NIR}\t{point.Classification}\t{point.Intensity}\t{point.ReturnNumber1}\t{point.NumberOfReturns}\t{point.GPS_Time}\t{point.ScanAngle}\t{point.PointSourceID}\t{point.EdgeOfFlightLine}\t{point.ScanDirectionFlag}\t{point.UserData}\t{point.R}\t{point.G}\t{point.B}");
        }

        return sb.ToString();
    }
    public static string ToCsvString(this ObservableCollection<GeoPoint> geoPoints, List<string> selectedColumns)
    {
        if (geoPoints == null || selectedColumns == null || selectedColumns.Count == 0)
        {
            return "";
        }

        var sb = new StringBuilder();

        // Add the header row based on selected columns
        sb.AppendLine(string.Join(",", selectedColumns));

        foreach (var point in geoPoints)
        {
            var rowValues = new List<string>();
            foreach (var columnName in selectedColumns)
            {
                string lowerColumnName = columnName.ToLower(); // Convert to lowercase

                switch (lowerColumnName)
                {
                    case "x": rowValues.Add(point.X.ToString()); break;
                    case "y": rowValues.Add(point.Y.ToString()); break;
                    case "z": rowValues.Add(point.Z.ToString()); break;
                    case "nir": rowValues.Add(point.NIR.ToString()); break;
                    case "classification": rowValues.Add(point.Classification.ToString()); break;
                    case "intensity": rowValues.Add(point.Intensity.ToString()); break;
                    case "returnnumber1": rowValues.Add(point.ReturnNumber1.ToString()); break;
                    case "numberofreturns": rowValues.Add(point.NumberOfReturns.ToString()); break;
                    case "gps_time": rowValues.Add(point.GPS_Time.ToString()); break;
                    case "scanangle": rowValues.Add(point.ScanAngle.ToString()); break;
                    case "pointsourceid": rowValues.Add(point.PointSourceID.ToString()); break;
                    case "edgeofflightline": rowValues.Add(point.EdgeOfFlightLine.ToString()); break;
                    case "scandirectionflag": rowValues.Add(point.ScanDirectionFlag.ToString()); break;
                    case "userdata": rowValues.Add(point.UserData.ToString()); break;
                    case "red": rowValues.Add(point.R.ToString()); break;
                    case "green": rowValues.Add(point.G.ToString()); break;
                    case "blue": rowValues.Add(point.B.ToString()); break;
                        // Add more cases for other properties as needed
                }
            }
            sb.AppendLine(string.Join(",", rowValues));
        }

        return sb.ToString();
    }

    public static string ToTextString(this ObservableCollection<GeoPoint> geoPoints, List<string> selectedColumns)
    {
        if (geoPoints == null || selectedColumns == null || selectedColumns.Count == 0)
        {
            return "";
        }

        var sb = new StringBuilder();

        // Add the header row based on selected columns
        sb.AppendLine(string.Join("\t", selectedColumns));

        foreach (var point in geoPoints)
        {
            var rowValues = new List<string>();
            foreach (var columnName in selectedColumns)
            {
                string lowerColumnName = columnName.ToLower(); // Convert to lowercase

                switch (lowerColumnName)
                {
                    case "x": rowValues.Add(point.X.ToString()); break;
                    case "y": rowValues.Add(point.Y.ToString()); break;
                    case "z": rowValues.Add(point.Z.ToString()); break;
                    case "nir": rowValues.Add(point.NIR.ToString()); break;
                    case "classification": rowValues.Add(point.Classification.ToString()); break;
                    case "intensity": rowValues.Add(point.Intensity.ToString()); break;
                    case "returnnumber1": rowValues.Add(point.ReturnNumber1.ToString()); break;
                    case "numberofreturns": rowValues.Add(point.NumberOfReturns.ToString()); break;
                    case "gps_time": rowValues.Add(point.GPS_Time.ToString()); break;
                    case "scanangle": rowValues.Add(point.ScanAngle.ToString()); break;
                    case "pointsourceid": rowValues.Add(point.PointSourceID.ToString()); break;
                    case "edgeofflightline": rowValues.Add(point.EdgeOfFlightLine.ToString()); break;
                    case "scandirectionflag": rowValues.Add(point.ScanDirectionFlag.ToString()); break;
                    case "userdata": rowValues.Add(point.UserData.ToString()); break;
                    case "red": rowValues.Add(point.R.ToString()); break;
                    case "green": rowValues.Add(point.G.ToString()); break;
                    case "blue": rowValues.Add(point.B.ToString()); break;
                        // Add more cases for other properties as needed
                }
            }
            sb.AppendLine(string.Join("\t", rowValues));
        }

        return sb.ToString();
    }

}