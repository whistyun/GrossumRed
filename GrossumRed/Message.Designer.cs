﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GrossumRed {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Message {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Message() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GrossumRed.Message", typeof(Message).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GRD0003:getter is duplicated.
        /// </summary>
        internal static string GetterDuplicated {
            get {
                return ResourceManager.GetString("GetterDuplicated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GRD0002:blockend notfound.
        /// </summary>
        internal static string NotFoundBlockEnd {
            get {
                return ResourceManager.GetString("NotFoundBlockEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GRD0004:setter is duplicated.
        /// </summary>
        internal static string SetterDuplicated {
            get {
                return ResourceManager.GetString("SetterDuplicated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GRD0001:unexpected blockend.
        /// </summary>
        internal static string UnexpectedBlockEnd {
            get {
                return ResourceManager.GetString("UnexpectedBlockEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GRD0005:unexpected character &apos;{0}&apos;.
        /// </summary>
        internal static string UnexpectedSyntaxInfo {
            get {
                return ResourceManager.GetString("UnexpectedSyntaxInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The number of output files must match the number of input files.
        /// </summary>
        internal static string WrongNumberOfOutputFiles {
            get {
                return ResourceManager.GetString("WrongNumberOfOutputFiles", resourceCulture);
            }
        }
    }
}