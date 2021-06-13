// Decompiled with JetBrains decompiler
// Type: Obj_Creator.App
// Assembly: T7 Custom Mesh, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C0A6D4B-7DEF-4F49-AF1B-F00673F61BCA
// Assembly location: C:\Users\HYPER\3D Objects\MESH MODS\T7 Custom Mesh\T7 Custom Mesh.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;

namespace Obj_Creator
{
  public class App : Application
  {
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent() => this.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);

    [STAThread]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public static void Main()
    {
      App app = new App();
      app.InitializeComponent();
      app.Run();
    }
  }
}
