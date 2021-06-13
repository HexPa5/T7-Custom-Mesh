// Decompiled with JetBrains decompiler
// Type: Custom_Mesh.Properties.Settings
// Assembly: T7 Custom Mesh, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C0A6D4B-7DEF-4F49-AF1B-F00673F61BCA
// Assembly location: C:\Users\HYPER\3D Objects\MESH MODS\T7 Custom Mesh\T7 Custom Mesh.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Custom_Mesh.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.8.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool First_Run
    {
      get => (bool) this[nameof (First_Run)];
      set => this[nameof (First_Run)] = (object) value;
    }
  }
}
