# T7-Custom-Mesh
NOTICE! This is source code of Dragon_Paul and CrossS95 decompiled in JetBrains Dotpeek (special thanks for AStronomy!)
YT Link for binary - https://www.youtube.com/watch?v=YWY9DjcJgl0
The source include all resources and code work as well but there's only one issue with .Xaml window file of "SystemShadowDropChrome"
The main goal of distribute is modify it for other Unreal 3/4 games and it's package files (Also you can download it and work alone if you want)
This code needs .NET Framework 4.6/4.6.1 and VS 17/19

Unfortunately I don't provide any guides how modify mesh but you may found from other sources step by step. So to make this tool working for other games simply naviagate to the Obj_Creator/MainWindow.cs Then search for 2 important function:

Find_Mesh_Vertices_faces()
From_Hex_String_To_Float_string(string s)
So I assume you need change .uasset bytes values of mesh vertices/faces and it's math for Upk/Pak of your game etc... Then rename file extension from Uasset to Upk or Pak in

Open_Uasset_File()
Dialog_Save_Uasset()
I hope I help you with this repo if you have any questions pull request!


Happy Modding!


