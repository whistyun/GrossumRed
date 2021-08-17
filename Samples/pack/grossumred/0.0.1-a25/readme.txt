GrossumRed
================

GrossumRed read for C#. 
this integrates with MSBuild and Visual Studio.


Getting Started
----------------

You will need to reload your project for the 'GrossumVwMdl' build action to be recognized.

Once you marked file as 'GrossumVwMdl' in your project, 
this file will be compiled to their respective `.g.cs` parser classes before every build.
These classes will be automatically included in compilation.


I/O Sample
----------------

Input: 
    public class VmCls
    {
        public string PropertyA { get; set; }
    }

Output:
    public class VmCls : System.ComponentModel.INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public string PropertyA
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
        private string __PropertyA;
    }
