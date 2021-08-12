# GrossumRed

GrossumRed generates csharp code which inherits INotifyPropertyChanged from POCO.


## Example

**Input VmCls.vm.cs:**  
```cs
using System;

namespace Hoge
{
    public class VmCls: System.Object
    {
        public int PropertyA { get; set; }
        public string PropertyB { get; set; }
    }
}
```

**Output VmCls.vm.g.cs:**  
For readability, I removed some extra code (such as #line) and indented.
```cs
using System;

namespace Hoge
{
    public class VmCls : System.Object, System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public int PropertyA
        {
            get { return __PropertyA; }

            set
            {
                if (Object.ReferenceEquals(__PropertyA, value)) return;
                if (Object.ReferenceEquals(__PropertyA, null) || !__PropertyA.Equals(value))
                {
                    __PropertyA = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(
                            this,
                            new System.ComponentModel.PropertyChangedEventArgs("PropertyA"));
                    }
                }
            }

        }
        private int __PropertyA;

        public string PropertyB
        {
            get { return __PropertyB; }

            set
            {
                if (Object.ReferenceEquals(__PropertyB, value)) return;
                if (Object.ReferenceEquals(__PropertyB, null) || !__PropertyB.Equals(value))
                {
                    __PropertyB = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(
                            this,
                            new System.ComponentModel.PropertyChangedEventArgs("PropertyB"));
                    }
                }
            }
        }
        private string __PropertyB;

    }
}
```


## How to use

You will need to reload your project for the 'GrossumVwMdl' build action to be recognized.

Once you marked file as 'GrossumVwMdl' in your project, 
this file will be compiled to their respective `.g.cs` parser classes before every build.
These classes will be automatically included in compilation.

If you mark .cs file, you may unregister from compiler target.

**WpfSmple.csproj**:  
```xml
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFrameworks>net45</TargetFrameworks>
    <UseWPF>true</UseWPF>
  </PropertyGroup>

  <ItemGroup>
    <Compile Remove="MainWindowViewModel.vm.cs" />
    <GrossumVwMdl Include="MainWindowViewModel.vm.cs" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="GrossumRed" Version="0.0.1-a15" />
  </ItemGroup>
</Project>
```