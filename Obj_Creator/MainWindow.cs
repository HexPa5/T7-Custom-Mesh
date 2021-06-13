// Decompiled with JetBrains decompiler
// Type: Obj_Creator.MainWindow
// Assembly: T7 Custom Mesh, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C0A6D4B-7DEF-4F49-AF1B-F00673F61BCA
// Assembly location: C:\Users\HYPER\3D Objects\MESH MODS\T7 Custom Mesh\T7 Custom Mesh.exe

using Custom_Mesh.Properties;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;

namespace Obj_Creator
{
  public class MainWindow : Window, IComponentConnector
  {
    private byte[] byte_uasset_values;
    private string FilePath_Open_Uasset;
    private string FilePath_Open_Obj;
    private int offset_start_vertices;
    private int offset_start_faces;
    private int count_vertices;
    private int count_faces;
    private int padding_vertices;
    private int padding_faces;
    private int size_of_v;
    private int size_of_f;
    private string[] converted_vertices_values_to_hex;
    private string fileName = "";
    private bool bl_Open_Custom_Obj;
    internal Grid Grid_Settings;
    internal Label Label_Offset_Start;
    internal TextBox txt_Offset_Start;
    internal Label Label_Padding;
    internal Label Label_Count;
    internal TextBox txt_count;
    internal TextBox txt_padding;
    internal Label Label_Offset_Start_Copy;
    internal TextBox txt_Offset_Start_faces;
    internal Label Label_Padding_Copy;
    internal Label Label_Count_Copy;
    internal TextBox txt_count_faces;
    internal TextBox txt_padding_faces;
    internal Label Label_Count_Copy1;
    internal Label Label_Count_Copy2;
    internal Label Label_Offset_Start_Copy1;
    internal TextBox txt_Offset_Finish_Vertices;
    internal Label Label_Offset_Start_Copy2;
    internal TextBox txt_Offset_Finish_faces;
    internal CheckBox Check_Box_Settings;
    internal TextBlock Text_Block_Open_Uasset_Title_Copy;
    internal TextBlock Text_Block_Open_Uasset_Title_Copy1;
    internal ComboBox Combo_Box_Support_Us;
    internal Button Button_Support_Dragon_Paul;
    internal Button Button_Support_CrossS95;
    internal Button button_open_uasset;
    internal TextBlock Text_Block_Open_Uasset_Title;
    internal Button Button_Open_Custom_Obj;
    internal TextBlock Text_Block_Open_Custom_Obj;
    internal TextBlock Text_Block_Save_Finished_Uasset;
    internal Button Button_Save_Uasset;
    internal TextBlock Text_Block_Export_Obj_Title;
    internal Button Button_Export_obj_2;
    internal Button Button_Export_obj_1;
    internal TextBlock Text_Block_Open_Uasset_Title_Copy2;
    private bool _contentLoaded;

    public MainWindow()
    {
      this.InitializeComponent();
      this.Check_For_Updates();
      if (!(bool) Settings.Default["First_Run"])
        return;
      Settings.Default["First_Run"] = (object) false;
      Settings.Default.Save();
      int num = (int) MessageBox.Show("THANKS FOR DOWNLOADING TEKKEN 7 CUSTOM MESH!", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
    }

    private void Button_open_convert_to_hex(object sender, RoutedEventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.FileName = "File";
      openFileDialog.DefaultExt = ".obj";
      openFileDialog.Filter = "3D File (.obj)|*.obj";
      bool? nullable = openFileDialog.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      Path.GetFileName(openFileDialog.FileName);
    }

    public string Convert_To_HexString_Little_Endian(string string_input)
    {
      if (!float.TryParse(string_input, out float _))
        return "";
      byte[] bytes = BitConverter.GetBytes(BitConverter.ToInt32(BitConverter.GetBytes(float.Parse(string_input, (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat)), 0));
      Array.Reverse((Array) bytes);
      return BitConverter.ToInt32(bytes, 0).ToString("X8");
    }

    public void Convert_to_hex_text_reader(string FilePath)
    {
      using (TextReader textReader = (TextReader) File.OpenText(FilePath))
      {
        string[] strArray = textReader.ReadToEnd().Split('\n', '\r', '/', ' ');
        int num1 = 0;
        int index1 = 0;
        double result1 = 0.0;
        bool flag1 = false;
        int num2 = 0;
        double result2 = 0.0;
        bool flag2 = false;
        int num3 = 0;
        int result3 = 0;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        bool flag6 = false;
        int index2;
        for (index2 = 0; index2 < strArray.Length; ++index2)
        {
          if (strArray[index2] == "v" || strArray[index2] == "V")
          {
            int num4 = double.TryParse(strArray[index2 + 1], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result1) ? 1 : 0;
            bool flag7 = double.TryParse(strArray[index2 + 2], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result1);
            bool flag8 = double.TryParse(strArray[index2 + 3], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result1);
            if (num4 != 0 && flag7 && flag8)
            {
              if (!flag1)
              {
                index1 = index2;
                flag1 = true;
              }
              num1 += 3;
              index2 += 3;
            }
          }
          else if (!(strArray[index2] == "vn") && !(strArray[index2] == "VN") && !(strArray[index2] == "Vn"))
          {
            if (strArray[index2] == "f" || strArray[index2] == "F")
              goto label_36;
          }
          else
            break;
        }
        for (; index2 < strArray.Length; ++index2)
        {
          if (strArray[index2] == "vn" || strArray[index2] == "VN" || strArray[index2] == "Vn")
          {
            int num4 = double.TryParse(strArray[index2 + 1], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result2) ? 1 : 0;
            bool flag7 = double.TryParse(strArray[index2 + 2], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result2);
            bool flag8 = double.TryParse(strArray[index2 + 3], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result2);
            int num5 = flag7 ? 1 : 0;
            if ((num4 & num5 & (flag8 ? 1 : 0)) != 0)
            {
              if (!flag2)
                flag2 = true;
              num2 += 3;
              index2 += 3;
            }
          }
          else if (strArray[index2] == "f" || strArray[index2] == "F")
            break;
        }
label_36:
        for (; index2 < strArray.Length; ++index2)
        {
          if (strArray[index2] == "f" || strArray[index2] == "F")
          {
            if (index2 < strArray.Length - 3)
            {
              flag3 = int.TryParse(strArray[index2 + 1], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
              flag4 = int.TryParse(strArray[index2 + 2], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
              flag5 = int.TryParse(strArray[index2 + 3], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
            }
            if (index2 < strArray.Length - 9)
            {
              bool flag7 = int.TryParse(strArray[index2 + 4], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
              bool flag8 = int.TryParse(strArray[index2 + 5], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
              bool flag9 = int.TryParse(strArray[index2 + 6], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
              bool flag10 = int.TryParse(strArray[index2 + 7], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
              bool flag11 = int.TryParse(strArray[index2 + 8], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
              bool flag12 = int.TryParse(strArray[index2 + 9], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.CurrentInfo, out result3);
              if (flag3 & flag4 & flag5 & flag7 & flag8 & flag9 & flag10 & flag11 & flag12)
              {
                if (!flag6)
                  flag6 = true;
                num3 += 9;
                index2 += 9;
              }
              if (flag3 & !flag4 & flag5 & flag7 & !flag8 & flag9 & flag10 & !flag11 & flag12)
              {
                if (!flag6)
                  flag6 = true;
                num3 += 9;
                index2 += 9;
              }
            }
            if (flag3 & flag4 & flag5)
            {
              if (!flag6)
                flag6 = true;
              num3 += 3;
              index2 += 3;
            }
          }
        }
        this.size_of_v = num1;
        int index3 = 0;
        this.converted_vertices_values_to_hex = new string[this.size_of_v];
        while (index3 < this.size_of_v)
        {
          if (strArray[index1] == "v" || strArray[index1] == "V" || strArray[index1] == "")
          {
            ++index1;
          }
          else
          {
            this.converted_vertices_values_to_hex[index3] = this.Convert_To_HexString_Little_Endian(strArray[index1]);
            ++index1;
            ++index3;
          }
        }
        this.size_of_f = num3;
      }
    }

    private void Button_Click_Open_Uasset(object sender, RoutedEventArgs e) => this.Open_Uasset_File();

    private void Button_Save_Uasset_Click(object sender, RoutedEventArgs e)
    {
      if (this.byte_uasset_values != null && this.bl_Open_Custom_Obj && this.count_vertices * 3 == this.size_of_v)
        this.Dialog_Save_Uasset();
      else if (this.byte_uasset_values == null && !this.bl_Open_Custom_Obj)
      {
        string messageBoxText = "1)Open uasset file" + Environment.NewLine + "2)Import obj" + Environment.NewLine + "3)Save uasset";
        string str = "Message";
        MessageBoxButton messageBoxButton = MessageBoxButton.OK;
        MessageBoxImage messageBoxImage = MessageBoxImage.Exclamation;
        string caption = str;
        int num1 = (int) messageBoxButton;
        int num2 = (int) messageBoxImage;
        int num3 = (int) MessageBox.Show(messageBoxText, caption, (MessageBoxButton) num1, (MessageBoxImage) num2);
      }
      else if (this.byte_uasset_values == null && this.bl_Open_Custom_Obj)
      {
        int num4 = (int) MessageBox.Show("Open uasset file before proceed", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      }
      else
      {
        if (this.byte_uasset_values == null || this.bl_Open_Custom_Obj)
          return;
        int num1 = (int) MessageBox.Show("Import obj file before proceed", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      }
    }

    public void Open_Uasset_File()
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.FileName = "File";
      openFileDialog.DefaultExt = ".uasset";
      openFileDialog.Filter = "Uasset File (.uasset)|*.uasset";
      bool? nullable = openFileDialog.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      this.FilePath_Open_Uasset = openFileDialog.FileName;
      this.fileName = Path.GetFileName(this.FilePath_Open_Uasset);
      this.Text_Block_Open_Uasset_Title.Text = "";
      this.Text_Block_Open_Uasset_Title.Text = this.fileName;
      this.Button_Export_obj_1.IsEnabled = false;
      this.Button_Export_obj_2.IsEnabled = false;
      this.Copy_Uasset_Values_To_Array();
      this.Find_Mesh_Vertices_faces();
      this.Text_Block_Export_Obj_Title.Text = this.fileName;
    }

    public void Dialog_Save_Uasset()
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      string withoutExtension = Path.GetFileNameWithoutExtension(this.FilePath_Open_Uasset);
      saveFileDialog.FileName = withoutExtension + "_New";
      saveFileDialog.DefaultExt = ".uasset";
      saveFileDialog.Filter = "Uasset File (.uasset)|*.uasset";
      bool? nullable = saveFileDialog.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      this.FilePath_Open_Uasset = saveFileDialog.FileName;
      this.fileName = Path.GetFileName(this.FilePath_Open_Uasset);
      this.Text_Block_Save_Finished_Uasset.Text = "";
      this.Text_Block_Save_Finished_Uasset.Text = this.fileName;
      this.Save_Uasset_File(saveFileDialog.FileName);
    }

    public void Save_Uasset_File(string filepath)
    {
      BinaryWriter binaryWriter = new BinaryWriter((Stream) File.Create(filepath));
      int num1 = 0;
      int index = 0;
      int num2 = 3;
      bool? isChecked = this.Check_Box_Settings.IsChecked;
      bool flag = true;
      if (isChecked.GetValueOrDefault() == flag & isChecked.HasValue)
      {
        int.TryParse(this.txt_padding.Text, out this.padding_vertices);
        int.TryParse(this.txt_count.Text, out this.count_vertices);
        this.offset_start_vertices = Convert.ToInt32(this.txt_Offset_Start.Text, 16);
      }
      int countVertices = this.count_vertices;
      int paddingVertices = this.padding_vertices;
      int offsetStartVertices = this.offset_start_vertices;
      for (; num1 < countVertices; ++num1)
      {
        for (; index < num2; ++index)
        {
          foreach (byte num3 in MainWindow.HexStringToByteArray(this.converted_vertices_values_to_hex[index]))
          {
            this.byte_uasset_values[offsetStartVertices] = num3;
            ++offsetStartVertices;
          }
        }
        num2 += 3;
        offsetStartVertices += paddingVertices;
      }
      binaryWriter.Write(this.byte_uasset_values);
      binaryWriter.Close();
      int num4 = (int) MessageBox.Show("Save Completed", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
    }

    public void Copy_Uasset_Values_To_Array()
    {
      this.byte_uasset_values = File.ReadAllBytes(this.FilePath_Open_Uasset);
      this.Grid_Settings.IsEnabled = true;
    }

    private void Button_Open_Custom_Obj_Click(object sender, RoutedEventArgs e) => this.Open_Custom_Obj();

    public void Open_Custom_Obj()
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.FileName = "File";
      openFileDialog.DefaultExt = ".obj";
      openFileDialog.Filter = "Obj File (.Obj)|*.Obj";
      bool? nullable = openFileDialog.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      this.FilePath_Open_Obj = openFileDialog.FileName;
      string fileName = Path.GetFileName(this.FilePath_Open_Obj);
      this.Text_Block_Open_Custom_Obj.Text = "";
      this.Text_Block_Open_Custom_Obj.Text = fileName;
      try
      {
        this.Convert_to_hex_text_reader(this.FilePath_Open_Obj);
        this.bl_Open_Custom_Obj = true;
        if (this.bl_Open_Custom_Obj && this.byte_uasset_values != null && this.size_of_v > this.count_vertices * 3)
        {
          string messageBoxText = "Your imported obj model has more vertices than the uasset model." + Environment.NewLine + Environment.NewLine + "Tip: Do not add or delete any vetrext points during editing your custom obj.";
          string str = "Can't import obj file";
          MessageBoxButton messageBoxButton = MessageBoxButton.OK;
          MessageBoxImage messageBoxImage = MessageBoxImage.Exclamation;
          string caption = str;
          int num1 = (int) messageBoxButton;
          int num2 = (int) messageBoxImage;
          int num3 = (int) MessageBox.Show(messageBoxText, caption, (MessageBoxButton) num1, (MessageBoxImage) num2);
          this.bl_Open_Custom_Obj = false;
          this.Text_Block_Open_Custom_Obj.Text = "";
        }
        else
        {
          if (!this.bl_Open_Custom_Obj || this.byte_uasset_values == null || this.size_of_v >= this.count_vertices * 3)
            return;
          string messageBoxText = "Your imported obj model has less vertices than the uasset model." + Environment.NewLine + Environment.NewLine + "Tip: Do not add or delete any vetrext points during editing your custom obj.";
          string str = "Can't import obj file";
          MessageBoxButton messageBoxButton = MessageBoxButton.OK;
          MessageBoxImage messageBoxImage = MessageBoxImage.Exclamation;
          string caption = str;
          int num1 = (int) messageBoxButton;
          int num2 = (int) messageBoxImage;
          int num3 = (int) MessageBox.Show(messageBoxText, caption, (MessageBoxButton) num1, (MessageBoxImage) num2);
          this.bl_Open_Custom_Obj = false;
          this.Text_Block_Open_Custom_Obj.Text = "";
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Can't load obj file", "Message", MessageBoxButton.OK, MessageBoxImage.Hand);
      }
    }

    private static byte[] HexStringToByteArray(string hexString)
    {
      byte[] numArray = new byte[hexString.Length / 2];
      for (int startIndex = 0; startIndex < hexString.Length; startIndex += 2)
        numArray[startIndex / 2] = Convert.ToByte(hexString.Substring(startIndex, 2), 16);
      return numArray;
    }

    private void Button_Export_obj_Click(object sender, RoutedEventArgs e)
    {
      if (this.byte_uasset_values == null)
      {
        int num = (int) MessageBox.Show("You must first open a uasset file", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      }
      else
        this.Dialog_Export_OBJ();
    }

    public void Dialog_Export_OBJ()
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      string withoutExtension = Path.GetFileNameWithoutExtension(this.FilePath_Open_Uasset);
      saveFileDialog.FileName = withoutExtension;
      saveFileDialog.DefaultExt = ".obj";
      saveFileDialog.Filter = "obj File (.obj)|*.obj";
      bool? nullable = saveFileDialog.ShowDialog();
      bool flag = true;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return;
      this.FilePath_Open_Uasset = saveFileDialog.FileName;
      this.fileName = Path.GetFileName(this.FilePath_Open_Uasset);
      this.Text_Block_Export_Obj_Title.Text = "";
      this.Text_Block_Export_Obj_Title.Text = this.fileName;
      this.Export_Obj(saveFileDialog.FileName);
    }

    public void Export_Obj(string filepath)
    {
      try
      {
        bool flag1 = false;
        using (TextWriter text = (TextWriter) File.CreateText(filepath))
        {
          text.Write("#Created with Custom Mesh");
          text.Write(Environment.NewLine);
          text.Write(Environment.NewLine);
          text.Write("#Vertices");
          text.Write(Environment.NewLine);
          bool? isChecked = this.Check_Box_Settings.IsChecked;
          bool flag2 = true;
          if (isChecked.GetValueOrDefault() == flag2 & isChecked.HasValue)
          {
            int.TryParse(this.txt_padding.Text, out this.padding_vertices);
            int.TryParse(this.txt_count.Text, out this.count_vertices);
            this.offset_start_vertices = Convert.ToInt32(this.txt_Offset_Start.Text, 16);
          }
          int num1 = 0;
          int num2 = 0;
          int index1 = 0;
          int index2 = 0;
          int countVertices = this.count_vertices;
          int offsetStartVertices = this.offset_start_vertices;
          int paddingVertices = this.padding_vertices;
          string[] strArray1 = new string[countVertices * 3];
          for (; num1 < countVertices; ++num1)
          {
            for (; num2 < 3; ++num2)
            {
              strArray1[index1] = string.Join("+", new string[1]
              {
                this.byte_uasset_values[offsetStartVertices].ToString("x2") + this.byte_uasset_values[offsetStartVertices + 1].ToString("x2") + this.byte_uasset_values[offsetStartVertices + 2].ToString("x2") + this.byte_uasset_values[offsetStartVertices + 3].ToString("x2")
              });
              strArray1[index1] = this.From_Hex_String_To_Float_string(strArray1[index1]);
              offsetStartVertices += 4;
              ++index1;
            }
            text.Write("v " + strArray1[index2] + " " + strArray1[index2 + 1] + " " + strArray1[index2 + 2]);
            text.Write(Environment.NewLine);
            offsetStartVertices += paddingVertices;
            index2 += 3;
            num2 = 0;
          }
          text.Write(Environment.NewLine);
          text.Write(Environment.NewLine);
          text.Write("#Faces");
          text.Write(Environment.NewLine);
          int.TryParse(this.txt_padding_faces.Text, out this.padding_faces);
          int.TryParse(this.txt_count_faces.Text, out this.count_faces);
          this.offset_start_faces = Convert.ToInt32(this.txt_Offset_Start_faces.Text, 16);
          int countFaces = this.count_faces;
          int offsetStartFaces = this.offset_start_faces;
          int paddingFaces = this.padding_faces;
          string[] strArray2 = new string[countFaces * 3];
          int[] numArray = new int[countFaces * 3];
          int index3 = 0;
          int num3 = 0;
          int num4 = 0;
          int index4 = 0;
          for (; num4 < countFaces; ++num4)
          {
            for (; num3 < 3; ++num3)
            {
              strArray2[index3] = string.Join("+", new string[1]
              {
                this.byte_uasset_values[offsetStartFaces + 1].ToString("x2") + this.byte_uasset_values[offsetStartFaces].ToString("x2")
              });
              numArray[index3] = int.Parse(strArray2[index3], NumberStyles.HexNumber) + 1;
              if (numArray[index3] > countVertices)
              {
                flag1 = false;
                goto label_19;
              }
              else
              {
                strArray2[index3] = (int.Parse(strArray2[index3], NumberStyles.HexNumber) + 1).ToString();
                offsetStartFaces += 2;
                ++index3;
              }
            }
            text.Write("f " + strArray2[index4] + " " + strArray2[index4 + 1] + " " + strArray2[index4 + 2]);
            text.Write(Environment.NewLine);
            offsetStartFaces += paddingFaces;
            index4 += 3;
            num3 = 0;
          }
          text.Write(Environment.NewLine);
          text.Write(Environment.NewLine);
          text.Write("#Dragon_Paul , CrossS95 ");
          flag1 = true;
        }
label_19:
        if (!flag1)
        {
          File.Delete(filepath);
          this.Text_Block_Export_Obj_Title.Text = "";
          this.Text_Block_Open_Uasset_Title.Text = "";
          this.byte_uasset_values = (byte[]) null;
          int num = (int) MessageBox.Show("Can't export this obj at the moment wait for an update", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        else
        {
          if (!flag1)
            return;
          int num = (int) MessageBox.Show("Export Completed", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
      }
      catch (Exception ex)
      {
        File.Delete(filepath);
        this.Text_Block_Export_Obj_Title.Text = "";
        this.Text_Block_Open_Uasset_Title.Text = "";
        this.byte_uasset_values = (byte[]) null;
        int num = (int) MessageBox.Show("Can't export this mesh", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
      }
    }

    private string From_Hex_String_To_Float_string(string s)
    {
      byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(s, 16));
      Array.Reverse((Array) bytes);
      return BitConverter.ToSingle(bytes, 0).ToString("N6");
    }

    private void Find_Mesh_Vertices_faces()
    {
      try
      {
        this.offset_start_vertices = 0;
        int index1;
        for (index1 = 0; index1 < this.byte_uasset_values.Length - 6; ++index1)
        {
          if (this.byte_uasset_values[index1].ToString("X2") == "01" & this.byte_uasset_values[index1 + 1].ToString("X2") == "00" & this.byte_uasset_values[index1 + 2].ToString("X2") == "00" & this.byte_uasset_values[index1 + 3].ToString("X2") == "00" & this.byte_uasset_values[index1 + 4].ToString("X2") == "01" & this.byte_uasset_values[index1 + 5].ToString("X2") == "00" & this.byte_uasset_values[index1 + 6].ToString("X2") == "01" || this.byte_uasset_values[index1].ToString("X2") == "02" & this.byte_uasset_values[index1 + 1].ToString("X2") == "00" & this.byte_uasset_values[index1 + 2].ToString("X2") == "00" & this.byte_uasset_values[index1 + 3].ToString("X2") == "00" & this.byte_uasset_values[index1 + 4].ToString("X2") == "01" & this.byte_uasset_values[index1 + 5].ToString("X2") == "00" & this.byte_uasset_values[index1 + 6].ToString("X2") == "02" || (this.byte_uasset_values[index1].ToString("X2") == "03" & this.byte_uasset_values[index1 + 1].ToString("X2") == "00" & this.byte_uasset_values[index1 + 2].ToString("X2") == "00" & this.byte_uasset_values[index1 + 3].ToString("X2") == "00" & this.byte_uasset_values[index1 + 4].ToString("X2") == "01" & this.byte_uasset_values[index1 + 5].ToString("X2") == "00" & this.byte_uasset_values[index1 + 6].ToString("X2") == "03" || this.byte_uasset_values[index1].ToString("X2") == "04" & this.byte_uasset_values[index1 + 1].ToString("X2") == "00" & this.byte_uasset_values[index1 + 2].ToString("X2") == "00" & this.byte_uasset_values[index1 + 3].ToString("X2") == "00" & this.byte_uasset_values[index1 + 4].ToString("X2") == "01" & this.byte_uasset_values[index1 + 5].ToString("X2") == "00" & this.byte_uasset_values[index1 + 6].ToString("X2") == "04"))
          {
            int num;
            for (num = 0; this.byte_uasset_values[index1 + 6 + num].ToString("X2") != "80" || this.byte_uasset_values[index1 + 6 + num + 1].ToString("X2") != "3F"; ++num)
            {
              if (num == 50)
              {
                index1 = index1 + 6 + num;
                break;
              }
            }
            if (this.byte_uasset_values[index1 + 6 + num].ToString("X2") == "80" && this.byte_uasset_values[index1 + 6 + num + 1].ToString("X2") == "3F")
              break;
          }
        }
        int num1;
        for (; index1 < this.byte_uasset_values.Length - 6; ++index1)
        {
          if (this.byte_uasset_values[index1].ToString("X2") == "01" & this.byte_uasset_values[index1 + 1].ToString("X2") == "00" & this.byte_uasset_values[index1 + 2].ToString("X2") == "00" & this.byte_uasset_values[index1 + 3].ToString("X2") == "00" & this.byte_uasset_values[index1 + 4].ToString("X2") == "01" & this.byte_uasset_values[index1 + 5].ToString("X2") == "00" & this.byte_uasset_values[index1 + 6].ToString("X2") == "01" || this.byte_uasset_values[index1].ToString("X2") == "02" & this.byte_uasset_values[index1 + 1].ToString("X2") == "00" & this.byte_uasset_values[index1 + 2].ToString("X2") == "00" & this.byte_uasset_values[index1 + 3].ToString("X2") == "00" & this.byte_uasset_values[index1 + 4].ToString("X2") == "01" & this.byte_uasset_values[index1 + 5].ToString("X2") == "00" & this.byte_uasset_values[index1 + 6].ToString("X2") == "02" || (this.byte_uasset_values[index1].ToString("X2") == "03" & this.byte_uasset_values[index1 + 1].ToString("X2") == "00" & this.byte_uasset_values[index1 + 2].ToString("X2") == "00" & this.byte_uasset_values[index1 + 3].ToString("X2") == "00" & this.byte_uasset_values[index1 + 4].ToString("X2") == "01" & this.byte_uasset_values[index1 + 5].ToString("X2") == "00" & this.byte_uasset_values[index1 + 6].ToString("X2") == "03" || this.byte_uasset_values[index1].ToString("X2") == "04" & this.byte_uasset_values[index1 + 1].ToString("X2") == "00" & this.byte_uasset_values[index1 + 2].ToString("X2") == "00" & this.byte_uasset_values[index1 + 3].ToString("X2") == "00" & this.byte_uasset_values[index1 + 4].ToString("X2") == "01" & this.byte_uasset_values[index1 + 5].ToString("X2") == "00" & this.byte_uasset_values[index1 + 6].ToString("X2") == "04"))
          {
            this.count_vertices = Convert.ToInt32(string.Join("+", new string[1]
            {
              this.byte_uasset_values[index1 + 6 + 41].ToString("X2") + this.byte_uasset_values[index1 + 6 + 40].ToString("X2")
            }), 16);
            this.txt_count.Text = this.count_vertices.ToString();
            string s = this.byte_uasset_values[index1 + 6 + 36].ToString("X2");
            if (s == "2C")
            {
              s = "32";
              this.padding_vertices = 32;
              this.txt_padding.Text = "32";
            }
            else
            {
              if (s == "20")
              {
                s = "20";
                this.padding_vertices = 20;
                this.txt_padding.Text = "20";
              }
              else if (s == "30")
              {
                s = "36";
                this.padding_vertices = 36;
                this.txt_padding.Text = "36";
                goto label_35;
              }
              else
              {
                int.TryParse(s, out this.padding_vertices);
                this.txt_padding.Text = s;
                int num2 = 1;
                int num3 = 1;
                int num4 = 1;
                while (!this.byte_uasset_values[index1 + 6 + 41 + num2].ToString("X2").Contains("FF"))
                  ++num2;
                if (this.byte_uasset_values[index1 + 6 + 41 + num2].ToString("X2").Contains("FF"))
                {
                  for (; int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3].ToString("X2"), NumberStyles.HexNumber) <= 0 || int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + 1].ToString("X2"), NumberStyles.HexNumber) <= 0 || (int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + 2].ToString("X2"), NumberStyles.HexNumber) <= 0 || int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + 3].ToString("X2"), NumberStyles.HexNumber) <= 0) || this.byte_uasset_values[index1 + 6 + 41 + num3].ToString("X2").Contains("FF"); ++num3)
                  {
                    if (this.byte_uasset_values[index1 + 6 + 41 + num2 + num3].ToString("X2").Contains("FF"))
                      goto label_26;
                  }
                }
                if (int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3].ToString("X2"), NumberStyles.HexNumber) > 0 && int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + 1].ToString("X2"), NumberStyles.HexNumber) > 0 && (int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + 2].ToString("X2"), NumberStyles.HexNumber) > 0 && int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + 3].ToString("X2"), NumberStyles.HexNumber) > 0) && !this.byte_uasset_values[index1 + 6 + 41 + num3].ToString("X2").Contains("FF"))
                {
                  (index1 + 6 + 41 + num2 + num3).ToString("X2");
                  this.txt_Offset_Start.Text = "0x0" + (index1 + 6 + 41 + num2 + num3).ToString("X2");
                  this.offset_start_vertices = index1 + 6 + 41 + num2 + num3;
                  break;
                }
label_26:
                if (this.byte_uasset_values[index1 + 6 + 41 + num2 + num3].ToString("X2").Contains("FF"))
                {
                  while (int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + num4].ToString("X2"), NumberStyles.HexNumber) <= 0 || int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + num4 + 1].ToString("X2"), NumberStyles.HexNumber) <= 0 || (int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + num4 + 2].ToString("X2"), NumberStyles.HexNumber) <= 0 || this.byte_uasset_values[index1 + 6 + 41 + num3].ToString("X2").Contains("FF")))
                    ++num4;
                }
                if (int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + num4].ToString("X2"), NumberStyles.HexNumber) > 0 && int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + num4 + 1].ToString("X2"), NumberStyles.HexNumber) > 0 && (int.Parse(this.byte_uasset_values[index1 + 6 + 41 + num2 + num3 + num4 + 2].ToString("X2"), NumberStyles.HexNumber) > 0 && !this.byte_uasset_values[index1 + 6 + 41 + num3].ToString("X2").Contains("FF")))
                {
                  (index1 + 6 + 41 + num2 + num3 + num4).ToString("X2");
                  this.txt_Offset_Start.Text = "0x0" + (index1 + 6 + 41 + num2 + num3 + num4).ToString("X2");
                  this.offset_start_vertices = index1 + 6 + 41 + num2 + num3 + num4;
                  break;
                }
              }
              if (s == "20")
              {
                num1 = index1 + 6 + 41 + 19;
                num1.ToString("X2");
                TextBox txtOffsetStart = this.txt_Offset_Start;
                num1 = index1 + 6 + 41 + 19;
                string str = "0x0" + num1.ToString("X2");
                txtOffsetStart.Text = str;
                this.offset_start_vertices = index1 + 6 + 41 + 19;
                break;
              }
            }
            if (s == "32")
            {
              num1 = index1 + 6 + 41 + 27;
              num1.ToString("X2");
              TextBox txtOffsetStart = this.txt_Offset_Start;
              num1 = index1 + 6 + 41 + 27;
              string str = "0x0" + num1.ToString("X2");
              txtOffsetStart.Text = str;
              this.offset_start_vertices = index1 + 6 + 41 + 27;
              break;
            }
label_35:
            if (s == "36")
            {
              num1 = index1 + 6 + 41 + 27;
              num1.ToString("X2");
              TextBox txtOffsetStart = this.txt_Offset_Start;
              num1 = index1 + 6 + 41 + 27;
              string str = "0x0" + num1.ToString("X2");
              txtOffsetStart.Text = str;
              this.offset_start_vertices = index1 + 6 + 41 + 27;
              break;
            }
          }
        }
        int num5 = this.offset_start_vertices + this.count_vertices * (12 + this.padding_vertices) - this.padding_vertices;
        this.txt_Offset_Finish_Vertices.Text = "0x0" + num5.ToString("X2");
        int num6 = 0;
        int num7 = 0;
        for (int index2 = num6 + num5; index2 < this.byte_uasset_values.Length - 5; ++index2)
        {
          if (this.byte_uasset_values[index2].ToString("X2") == "02" & this.byte_uasset_values[index2 + 1].ToString("X2") == "02" & this.byte_uasset_values[index2 + 2].ToString("X2") == "00" & this.byte_uasset_values[index2 + 3].ToString("X2") == "00" & this.byte_uasset_values[index2 + 4].ToString("X2") == "00")
          {
            this.txt_padding_faces.Text = "2";
            this.padding_faces = 2;
            num1 = index2 + 4 + 7;
            num1.ToString("X2");
            TextBox offsetStartFaces = this.txt_Offset_Start_faces;
            num1 = index2 + 4 + 7;
            string str1 = "0x0" + num1.ToString("X2");
            offsetStartFaces.Text = str1;
            string s1 = string.Join("+", new string[1]
            {
              this.byte_uasset_values[index2 + 4 + 7 + num7 + 1].ToString("x2") + this.byte_uasset_values[index2 + 4 + 7 + num7].ToString("x2")
            });
            string s2 = string.Join("+", new string[1]
            {
              this.byte_uasset_values[index2 + 4 + 7 + num7 + 3].ToString("x2") + this.byte_uasset_values[index2 + 4 + 7 + num7 + 2].ToString("x2")
            });
            string s3 = string.Join("+", new string[1]
            {
              this.byte_uasset_values[index2 + 4 + 7 + num7 + 5].ToString("x2") + this.byte_uasset_values[index2 + 4 + 7 + num7 + 4].ToString("x2")
            });
            while (int.Parse(s1, NumberStyles.HexNumber) != 64 || int.Parse(s2, NumberStyles.HexNumber) != 0 || int.Parse(s3, NumberStyles.HexNumber) != this.count_vertices)
            {
              if (index2 + 4 + 7 + num7 + 5 < this.byte_uasset_values.Length - 20)
              {
                s1 = string.Join("+", new string[1]
                {
                  this.byte_uasset_values[index2 + 4 + 7 + num7 + 1].ToString("x2") + this.byte_uasset_values[index2 + 4 + 7 + num7].ToString("x2")
                });
                s2 = string.Join("+", new string[1]
                {
                  this.byte_uasset_values[index2 + 4 + 7 + num7 + 3].ToString("x2") + this.byte_uasset_values[index2 + 4 + 7 + num7 + 2].ToString("x2")
                });
                s3 = string.Join("+", new string[1]
                {
                  this.byte_uasset_values[index2 + 4 + 7 + num7 + 5].ToString("x2") + this.byte_uasset_values[index2 + 4 + 7 + num7 + 4].ToString("x2")
                });
                num7 = num7 + 6 + 2;
              }
              else
                goto label_47;
            }
            if (int.Parse(s1, NumberStyles.HexNumber) == 64 && int.Parse(s2, NumberStyles.HexNumber) == 0 && int.Parse(s3, NumberStyles.HexNumber) == this.count_vertices)
            {
              int num2 = index2 + 4 + 7 + num7 - 8;
              this.txt_Offset_Finish_faces.Text = "0x0" + num2.ToString("X2");
              this.count_faces = (num2 + 1 - (index2 + 4 + 7 - 1)) / (6 + this.padding_faces);
              this.txt_count_faces.Text = this.count_faces.ToString();
              break;
            }
label_47:
            TextBox offsetFinishFaces = this.txt_Offset_Finish_faces;
            num1 = this.byte_uasset_values.Length - 20;
            string str2 = "0x0" + num1.ToString("X2");
            offsetFinishFaces.Text = str2;
            this.count_faces = (this.byte_uasset_values.Length + 1 - 20 - (index2 + 4 + 7 - 1)) / (6 + this.padding_faces);
            this.txt_count_faces.Text = this.count_faces.ToString();
            break;
          }
        }
      }
      catch (Exception ex)
      {
        int num1 = (int) MessageBox.Show(ex.ToString());
        int num2 = (int) MessageBox.Show("Can't load this mesh", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
      }
      this.Button_Export_obj_1.IsEnabled = true;
      this.Button_Export_obj_2.IsEnabled = true;
    }

    public void Check_For_Updates()
    {
      string fileName = "";
      Version version1 = (Version) null;
      string url = "https://drive.google.com/uc?export=download&id=1kwQvp7FJtyHn7kf8lia9NmkK7Hj2_IxG";
      XmlTextReader xmlTextReader = (XmlTextReader) null;
      try
      {
        xmlTextReader = new XmlTextReader(url);
        xmlTextReader.Read();
        int content = (int) xmlTextReader.MoveToContent();
        string str = "";
        if (xmlTextReader.NodeType == XmlNodeType.Element)
        {
          if (xmlTextReader.Name == "Custom_Mesh")
          {
            while (xmlTextReader.Read())
            {
              if (xmlTextReader.NodeType == XmlNodeType.Element)
                str = xmlTextReader.Name;
              else if (xmlTextReader.NodeType == XmlNodeType.Text && xmlTextReader.HasValue)
              {
                if (!(str == "Version"))
                {
                  if (str == "Url")
                    fileName = xmlTextReader.Value;
                }
                else
                  version1 = new Version(xmlTextReader.Value);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Custom Mesh can't check for updates now there is a problem with the server.", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      }
      finally
      {
        xmlTextReader?.Close();
      }
      Version version2 = Assembly.GetExecutingAssembly().GetName().Version;
      if (version2.CompareTo(version1) < 0)
      {
        string messageBoxText = "Custom Mesh version " + (object) version1.Major + "." + (object) version1.Minor + "." + (object) version1.Build + " is now available!" + Environment.NewLine + "Would you like to download it?";
        string str = "Update Available";
        MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;
        MessageBoxImage messageBoxImage = MessageBoxImage.Asterisk;
        string caption = str;
        int num1 = (int) messageBoxButton;
        int num2 = (int) messageBoxImage;
        if (MessageBox.Show(messageBoxText, caption, (MessageBoxButton) num1, (MessageBoxImage) num2) != MessageBoxResult.Yes)
          return;
        Process.Start(fileName);
        Environment.Exit(1);
      }
      else
      {
        if (!(version2 == version1))
          return;
        int num = (int) MessageBox.Show("Custom Mesh is up to date", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
      }
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
    }

    private void button_open_uasset_MouseEnter(object sender, MouseEventArgs e)
    {
    }

    private void Button_Support_Dragon_Paul_Click(object sender, RoutedEventArgs e) => Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=88EFNR2NTJ3RQ");

    private void Button_Support_CrossS95_Click(object sender, RoutedEventArgs e) => Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=U6ABCVGW6GDBS");

    private float From_Hex_String_To_Float(string s)
    {
      byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(s, 16));
      Array.Reverse((Array) bytes);
      return BitConverter.ToSingle(bytes, 0);
    }

    private static void InstallCertificate(string cerFileName)
    {
      string password = "padding38";
      X509Certificate2 certificate = new X509Certificate2(cerFileName, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
      X509Store x509Store = new X509Store(StoreName.TrustedPublisher, StoreLocation.CurrentUser);
      x509Store.Open(OpenFlags.ReadWrite);
      x509Store.Add(certificate);
      x509Store.Close();
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/T7 Custom Mesh;component/mainwindow.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.Grid_Settings = (Grid) target;
          break;
        case 2:
          this.Label_Offset_Start = (Label) target;
          break;
        case 3:
          this.txt_Offset_Start = (TextBox) target;
          break;
        case 4:
          this.Label_Padding = (Label) target;
          break;
        case 5:
          this.Label_Count = (Label) target;
          break;
        case 6:
          this.txt_count = (TextBox) target;
          break;
        case 7:
          this.txt_padding = (TextBox) target;
          break;
        case 8:
          this.Label_Offset_Start_Copy = (Label) target;
          break;
        case 9:
          this.txt_Offset_Start_faces = (TextBox) target;
          break;
        case 10:
          this.Label_Padding_Copy = (Label) target;
          break;
        case 11:
          this.Label_Count_Copy = (Label) target;
          break;
        case 12:
          this.txt_count_faces = (TextBox) target;
          break;
        case 13:
          this.txt_padding_faces = (TextBox) target;
          break;
        case 14:
          this.Label_Count_Copy1 = (Label) target;
          break;
        case 15:
          this.Label_Count_Copy2 = (Label) target;
          break;
        case 16:
          this.Label_Offset_Start_Copy1 = (Label) target;
          break;
        case 17:
          this.txt_Offset_Finish_Vertices = (TextBox) target;
          break;
        case 18:
          this.Label_Offset_Start_Copy2 = (Label) target;
          break;
        case 19:
          this.txt_Offset_Finish_faces = (TextBox) target;
          break;
        case 20:
          this.Check_Box_Settings = (CheckBox) target;
          break;
        case 21:
          this.Text_Block_Open_Uasset_Title_Copy = (TextBlock) target;
          break;
        case 22:
          this.Text_Block_Open_Uasset_Title_Copy1 = (TextBlock) target;
          break;
        case 23:
          this.Combo_Box_Support_Us = (ComboBox) target;
          break;
        case 24:
          this.Button_Support_Dragon_Paul = (Button) target;
          this.Button_Support_Dragon_Paul.Click += new RoutedEventHandler(this.Button_Support_Dragon_Paul_Click);
          break;
        case 25:
          this.Button_Support_CrossS95 = (Button) target;
          this.Button_Support_CrossS95.Click += new RoutedEventHandler(this.Button_Support_CrossS95_Click);
          break;
        case 26:
          this.button_open_uasset = (Button) target;
          this.button_open_uasset.Click += new RoutedEventHandler(this.Button_Click_Open_Uasset);
          this.button_open_uasset.MouseEnter += new MouseEventHandler(this.button_open_uasset_MouseEnter);
          break;
        case 27:
          this.Text_Block_Open_Uasset_Title = (TextBlock) target;
          break;
        case 28:
          this.Button_Open_Custom_Obj = (Button) target;
          this.Button_Open_Custom_Obj.Click += new RoutedEventHandler(this.Button_Open_Custom_Obj_Click);
          break;
        case 29:
          this.Text_Block_Open_Custom_Obj = (TextBlock) target;
          break;
        case 30:
          this.Text_Block_Save_Finished_Uasset = (TextBlock) target;
          break;
        case 31:
          this.Button_Save_Uasset = (Button) target;
          this.Button_Save_Uasset.Click += new RoutedEventHandler(this.Button_Save_Uasset_Click);
          break;
        case 32:
          this.Text_Block_Export_Obj_Title = (TextBlock) target;
          break;
        case 33:
          this.Button_Export_obj_2 = (Button) target;
          this.Button_Export_obj_2.Click += new RoutedEventHandler(this.Button_Export_obj_Click);
          break;
        case 34:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click_Open_Uasset);
          break;
        case 35:
          this.Button_Export_obj_1 = (Button) target;
          this.Button_Export_obj_1.Click += new RoutedEventHandler(this.Button_Export_obj_Click);
          break;
        case 36:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Save_Uasset_Click);
          break;
        case 37:
          ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Open_Custom_Obj_Click);
          break;
        case 38:
          this.Text_Block_Open_Uasset_Title_Copy2 = (TextBlock) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
