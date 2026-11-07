# LAZ to TEXT

A Windows desktop app that reads LiDAR point-cloud files (.laz or .las) and
exports the points to a text (.txt) or CSV (.csv) file. It also shows the LAS
header and lets you choose which columns to export.

Free tool by AgriMetSoft.

## What it does

- Reads compressed `.laz` and uncompressed `.las` LiDAR files (using laszip4j).
- Shows the LAS header: file version, point count, point format, the minimum
  and maximum X, Y, Z, and the creation date.
- Shows every point in a table with its attributes: X, Y, Z, intensity,
  classification, near-infrared (NIR), GPS time, return number, number of
  returns, scan angle, point source ID, edge of flight line, scan direction,
  user data, and the red/green/blue color when the file has it.
- Exports the points to `.txt` or `.csv`.
- Lets you pick which columns to export and their order (Save Option).
- Can save the header and the points in separate files.

## How to use

1. Open the app.
2. Click **Select File** and choose a `.laz` or `.las` file.
3. The header and the points appear in the window.
4. Click **Save All** to export everything, or **Save Option** to choose the
   columns first.

## Build from source

- Open `LAS to TEXT.csproj` in Visual Studio 2022.
- Restore the NuGet packages.
- Build. The project targets .NET Framework 4.8.1 (WPF).

Main libraries: laszip4j (LiDAR reading, through IKVM) and SharpZipLib.

## Notes

- This is a free tool. The source is shared so others can read it and learn
  from it.
- Large LiDAR files hold many points, so reading can take a moment. The app
  shows a progress bar while it reads.
