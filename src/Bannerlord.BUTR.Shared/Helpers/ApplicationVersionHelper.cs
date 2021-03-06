// <auto-generated>
//   This code file has automatically been added by the "Bannerlord.BUTR.Shared" NuGet package (https://www.nuget.org/packages/Bannerlord.BUTR.Shared).
//   Please see https://github.com/BUTR/Bannerlord.BUTR.Shared for more information.
//
//   IMPORTANT:
//   DO NOT DELETE THIS FILE if you are using a "packages.config" file to manage your NuGet references.
//   Consider migrating to PackageReferences instead:
//   https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference
//   Migrating brings the following benefits:
//   * The "Bannerlord.BUTR.Shared" folder and the "Bannerlord.BUTR.Shared.cs" file don't appear in your project.
//   * The added file is immutable and can therefore not be modified by coincidence.
//   * Updating/Uninstalling the package will work flawlessly.
// </auto-generated>

#region License
// MIT License
// 
// Copyright (c) Bannerlord's Unofficial Tools & Resources
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

#if !BANNERLORDBUTRSHARED_DISABLE
#nullable enable
#pragma warning disable

namespace Bannerlord.BUTR.Shared.Helpers
{
    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Xml;
    using global::System.Reflection;
    using global::System.Runtime.Serialization;
    using global::System.IO;

    using global::TaleWorlds.Library;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
    internal static class ApplicationVersionHelper
    {
        /*
        private delegate void SetApplicationVersionTypeDelegate(object instance, ApplicationVersionType applicationVersionType);
        private static readonly SetApplicationVersionTypeDelegate? SetApplicationVersionType;

        private delegate void SetMajorDelegate(object instance, int major);
        private static readonly SetMajorDelegate? SetMajor;

        private delegate void SetMinorDelegate(object instance, int minor);
        private static readonly SetMinorDelegate? SetMinor;

        private delegate void SetRevisionDelegate(object instance, int revision);
        private static readonly SetRevisionDelegate? SetRevision;

        private delegate void SetChangeSetDelegate(object instance, int changeSet);
        private static readonly SetChangeSetDelegate? SetChangeSet;
        */

        private static readonly PropertyInfo? ApplicationVersionTypeProperty;
        private static readonly PropertyInfo? MajorProperty;
        private static readonly PropertyInfo? MinorProperty;
        private static readonly PropertyInfo? RevisionProperty;
        private static readonly PropertyInfo? ChangeSetProperty;
        private static readonly PropertyInfo? VersionGameTypeProperty;

        private delegate ApplicationVersion GetEmptyDelegate();
        private static readonly GetEmptyDelegate? GetEmpty;

        public static ApplicationVersion Empty => GetEmpty is not null ? GetEmpty() : default;

        static ApplicationVersionHelper()
        {
            var all = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            /*
            SetApplicationVersionType = ReflectionHelper.GetDelegate<SetApplicationVersionTypeDelegate>(typeof(ApplicationVersion).GetProperty("ApplicationVersionType", all)?.SetMethod);
            SetMajor = ReflectionHelper.GetDelegate<SetMajorDelegate>(typeof(ApplicationVersion).GetProperty("Major", all)?.SetMethod);
            SetMinor = ReflectionHelper.GetDelegate<SetMinorDelegate>(typeof(ApplicationVersion).GetProperty("Minor", all)?.SetMethod);
            SetRevision = ReflectionHelper.GetDelegate<SetRevisionDelegate>(typeof(ApplicationVersion).GetProperty("Revision", all)?.SetMethod);
            SetChangeSet = ReflectionHelper.GetDelegate<SetChangeSetDelegate>(typeof(ApplicationVersion).GetProperty("ChangeSet", all)?.SetMethod);
            */

            ApplicationVersionTypeProperty = typeof(ApplicationVersion).GetProperty("ApplicationVersionType", all);
            MajorProperty = typeof(ApplicationVersion).GetProperty("Major", all);
            MinorProperty = typeof(ApplicationVersion).GetProperty( "Minor", all);
            RevisionProperty = typeof(ApplicationVersion).GetProperty( "Revision", all);
            ChangeSetProperty = typeof(ApplicationVersion).GetProperty( "ChangeSet", all);
            VersionGameTypeProperty = typeof(ApplicationVersion).GetProperty( "VersionGameType", all);

            GetEmpty = ReflectionHelper.GetDelegate<GetEmptyDelegate>(typeof(ApplicationVersion).GetProperty("Empty", BindingFlags.Public | BindingFlags.Static)?.GetMethod);

        }

        private static string GetPrefix(ApplicationVersionType applicationVersionType) => (int) applicationVersionType switch
        {
            0 => "a",
            1 => "b",
            2 => "e",
            3 => "v",
            4 => "d",
            5 => "m",
            _ => "i"
        };
        private static ApplicationVersionType ApplicationVersionTypeFromString(string applicationVersionType) =>
            string.IsNullOrEmpty(applicationVersionType) ? default : ApplicationVersionTypeFromString(applicationVersionType[0]);
        private static ApplicationVersionType ApplicationVersionTypeFromString(char applicationVersionType) => applicationVersionType switch
        {
            'a' => (ApplicationVersionType) 0,
            'b' => (ApplicationVersionType) 1,
            'e' => (ApplicationVersionType) 2,
            'v' => (ApplicationVersionType) 3,
            'd' => (ApplicationVersionType) 4,
            'm' => (ApplicationVersionType) 5,
            _ => (ApplicationVersionType) (int) -1,
        };

        public static ApplicationVersion FromParametersFile(string versionGameType)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(VirtualFolders.GetFileContent(Path.Combine(BasePath.Name, "Parameters", "Version.xml")));
            var versionAsString = xmlDocument?.SelectSingleNode("Version")?.SelectSingleNode(versionGameType)?.Attributes["Value"]?.InnerText;
            TryParse(versionAsString, out var version);
            return version;
        }

        public static bool TryParse(string? versionAsString, out ApplicationVersion version) => TryParse(versionAsString, out version, true);

        public static bool TryParse(string? versionAsString, out ApplicationVersion version, bool asMin)
        {
            var major = asMin ? 0 : int.MaxValue;
            var minor = asMin ? 0 : int.MaxValue;
            var revision = asMin ? 0 : int.MaxValue;
            var changeSet = asMin ? 0 : int.MaxValue;
            bool skipCheck = false;
            version = Empty;
            if (versionAsString is null)
                return false;

            var array = versionAsString.Split('.');
            if (array.Length != 3 && array.Length != 4 && array[0].Length == 0)
                return false;

            var applicationVersionType = ApplicationVersionTypeFromString(array[0][0]);
            if (!skipCheck && !int.TryParse(array[0].Substring(1), out major))
            {
                if (array[0].Substring(1) != "*") return false;
                major = int.MinValue;
                minor = int.MinValue;
                revision = int.MinValue;
                changeSet = int.MinValue;
                skipCheck = true;
            }
            if (!skipCheck && !int.TryParse(array[1], out minor))
            {
                if (array[1] != "*") return false;
                minor = asMin ? 0 : int.MaxValue;
                revision = asMin ? 0 : int.MaxValue;
                changeSet = asMin ? 0 : int.MaxValue;
                skipCheck = true;
            }
            if (!skipCheck && !int.TryParse(array[2], out revision))
            {
                if (array[2] != "*") return false;
                revision = asMin ? 0 : int.MaxValue;
                changeSet = asMin ? 0 : int.MaxValue;
                skipCheck = true;
            }

            if (!skipCheck && array.Length == 4 && !int.TryParse(array[3], out changeSet))
            {
                if (array[3] != "*") return false;
                changeSet = asMin ? 0 : int.MaxValue;
                skipCheck = true;
            }

            var boxedVersion = FormatterServices.GetUninitializedObject(typeof(ApplicationVersion)); // https://stackoverflow.com/a/6280540
            ApplicationVersionTypeProperty?.SetValue(boxedVersion, applicationVersionType);
            MajorProperty?.SetValue(boxedVersion, major);
            MinorProperty?.SetValue(boxedVersion, minor);
            RevisionProperty?.SetValue(boxedVersion, revision);
            ChangeSetProperty?.SetValue(boxedVersion, changeSet);
            VersionGameTypeProperty?.SetValue(boxedVersion, 0);
            version = (ApplicationVersion) boxedVersion;

            /*
            if (SetApplicationVersionType is not null) SetApplicationVersionType(version, applicationVersionType);
            if (SetMajor is not null) SetMajor(version, major);
            if (SetMinor is not null) SetMinor(version, minor);
            if (SetRevision is not null) SetRevision(version, revision);
            if (SetChangeSet is not null) SetChangeSet(version, changeSet);
            */

            return true;
        }

        public static string ToString(ApplicationVersion av)
        {
            string prefix = GetPrefix(av.ApplicationVersionType);
            var def = FromParametersFile("Singleplayer");
            return $"{prefix}{av.Major}.{av.Minor}.{av.Revision}{(av.ChangeSet == def.ChangeSet ? "" : $".{av.ChangeSet}")}";
        }

        public static bool IsSame(this ApplicationVersion @this, ApplicationVersion other)
        {
            return @this.ApplicationVersionType == other.ApplicationVersionType &&
                   @this.Major == other.Major &&
                   @this.Minor == other.Minor &&
                   @this.Revision == other.Revision;
        }
        public static bool IsSameWithChangeSet(this ApplicationVersion @this, ApplicationVersion other)
        {
            return @this.ApplicationVersionType == other.ApplicationVersionType &&
                   @this.Major == other.Major &&
                   @this.Minor == other.Minor &&
                   @this.Revision == other.Revision &&
                   @this.ChangeSet == other.ChangeSet;
        }
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE
