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

namespace Bannerlord.BUTR.Shared
{
    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;

    using global::TaleWorlds.Library;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
    internal static class ApplicationVersionUtils
    {
        public static bool TryParse(string? versionAsString, out ApplicationVersion version)
        {
            var major = 0;
            var minor = 0;
            var revision = 0;
            var changeSet = 0;
            bool skipCheck = false;
            version = default;
            if (versionAsString is null)
                return false;

            var array = versionAsString.Split('.');
            if (array.Length != 3 && array.Length != 4 && array[0].Length == 0)
                return false;

            var applicationVersionType = ApplicationVersion.ApplicationVersionTypeFromString(array[0][0].ToString());
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
                minor = 0;
                revision = 0;
                changeSet = 0;
                skipCheck = true;
            }
            if (!skipCheck && !int.TryParse(array[2], out revision))
            {
                if (array[2] != "*") return false;
                revision = 0;
                changeSet = 0;
                skipCheck = true;
            }

            if (!skipCheck && array.Length == 4 && !int.TryParse(array[3], out changeSet))
            {
                if (array[3] != "*") return false;
                changeSet = 0;
                skipCheck = true;
            }

            version = new ApplicationVersion(applicationVersionType, major, minor, revision, changeSet, ApplicationVersionGameType.Singleplayer);
            return true;
        }

        public static string ToString(ApplicationVersion av)
        {
            string prefix = ApplicationVersion.GetPrefix(av.ApplicationVersionType);
            var def = ApplicationVersion.FromParametersFile(ApplicationVersionGameType.Singleplayer);
            return $"{prefix}{av.Major}.{av.Minor}.{av.Revision}{(av.ChangeSet == def.ChangeSet ? "" : $".{av.ChangeSet}")}";
        }
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE
