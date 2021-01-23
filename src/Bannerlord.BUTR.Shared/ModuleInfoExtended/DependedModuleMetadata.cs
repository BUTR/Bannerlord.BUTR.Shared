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

using System;

#if !BANNERLORDBUTRSHARED_DISABLE
#nullable enable
#pragma warning disable

#if !BANNERLORDBUTRSHARED_BUTTERLIB
namespace Bannerlord.BUTR.Shared.ModuleInfoExtended
#else
namespace Bannerlord.ButterLib.Common.Helpers
#endif
{
    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;

    using global::Bannerlord.BUTR.Shared.Helpers;

    using global::TaleWorlds.Library;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
#if !BANNERLORDBUTRSHARED_BUTTERLIB
    internal
#else
    public
#endif
        readonly struct ApplicationVersionRange : IEquatable<ApplicationVersionRange>
    {
        public static ApplicationVersionRange Empty => new ApplicationVersionRange(ApplicationVersionHelper.Empty, ApplicationVersionHelper.Empty);

        public ApplicationVersion Min { get; init; }
        public ApplicationVersion Max { get; init; }

        public ApplicationVersionRange(ApplicationVersion min, ApplicationVersion max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString() => $"{Min} - {Max}";

        public static bool TryParse(string versionRangeAsString, out ApplicationVersionRange versionRange)
        {
            versionRange = Empty;

            if (string.IsNullOrWhiteSpace(versionRangeAsString))
                return false;

            versionRangeAsString = versionRangeAsString.Replace(" ", string.Empty);
            var index = versionRangeAsString.IndexOf('-');
            if (index < 0)
                return false;

            var minAsString = versionRangeAsString.Substring(0, index);
            var maxAsString = versionRangeAsString.Substring(index + 1, versionRangeAsString.Length - 1 - index);

            if (ApplicationVersionHelper.TryParse(minAsString, out var min, true) && ApplicationVersionHelper.TryParse(maxAsString, out var max, false))
            {
                versionRange = new ApplicationVersionRange(min, max);
                return true;
            }

            return false;
        }

        public bool Equals(ApplicationVersionRange other) => Min.IsSame(other.Min) && Max.IsSame(other.Max);
        public override bool Equals(object? obj) => obj is ApplicationVersionRange other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(Min, Max);
        public static bool operator ==(ApplicationVersionRange left, ApplicationVersionRange right) => left.Equals(right);
        public static bool operator !=(ApplicationVersionRange left, ApplicationVersionRange right) => !left.Equals(right);
    }

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
#if !BANNERLORDBUTRSHARED_BUTTERLIB
    internal 
#else
    public
#endif
    readonly struct DependedModuleMetadata
    {
        public string Id { get; init; }
        public LoadType LoadType { get; init; }
        public bool IsOptional { get; init; }
        public bool IsIncompatible { get; init; }
        public ApplicationVersion Version { get; init; }
        public ApplicationVersionRange VersionRange { get; init; }

        public DependedModuleMetadata(string id, LoadType loadType, bool isOptional, ApplicationVersion version)
        {
            Id = id;
            LoadType = loadType;
            IsOptional = isOptional;
            IsIncompatible = false;
            Version = version;
            VersionRange = ApplicationVersionRange.Empty;
        }
        public DependedModuleMetadata(string id, LoadType loadType, bool isOptional, bool isIncompatible, ApplicationVersion version)
        {
            Id = id;
            LoadType = loadType;
            IsOptional = isOptional;
            IsIncompatible = isIncompatible;
            Version = version;
            VersionRange = ApplicationVersionRange.Empty;
        }
        public DependedModuleMetadata(string id, LoadType loadType, bool isOptional, bool isIncompatible, ApplicationVersionRange versionRange)
        {
            Id = id;
            LoadType = loadType;
            IsOptional = isOptional;
            IsIncompatible = isIncompatible;
            Version = ApplicationVersionHelper.Empty;
            VersionRange = versionRange;
        }

        internal static string GetLoadType(LoadType loadType) => loadType switch
        {
            LoadType.NONE           => "",
            LoadType.LoadAfterThis  => "Before       ",
            LoadType.LoadBeforeThis => "After        ",
            _                       => "ERROR        "
        };
        private static string GetVersion(ApplicationVersion av) => av == ApplicationVersionHelper.Empty ? "" : $" {av}";
        private static string GetVersionRange(ApplicationVersionRange avr) => avr == ApplicationVersionRange.Empty ? "" : $" {avr}";
        private static string GetOptional(bool isOptional) => isOptional ? " Optional" : "";
        private static string GetIncompatible(bool isOptional) => isOptional ? "Incompatible " : "";
        public override string ToString() => GetLoadType(LoadType) + GetIncompatible(IsIncompatible) + Id + GetVersion(Version) + GetVersionRange(VersionRange) + GetOptional(IsOptional);
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE
