// <auto-generated>
//   This code file has automatically been added by the "Bannerlord.BUTR.Shared" NuGet package (https://www.nuget.org/packages/Bannerlord.BUTR.Shared).
//   Please see https://github.com/BUTR/Bannerlord.BUTR.Shared for more information.
//
//   IMPORTANT:
//   DO NOT DELETE THIS FILE if you are using a "packages.config" file to manage your NuGet references.
//   Consider migrating to PackageReferences instead:
//   https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference
//   Migrating brings the following benefits:
//   * The "Bannerlord.BUTR.Shared\ModuleInfoExtended" folder and the "SubModuleInfo2.cs" file don't appear in your project.
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

#if !BANNERLORDBUTRSHARED_BUTTERLIB
namespace Bannerlord.BUTR.Shared.ModuleInfoExtended
#else
namespace Bannerlord.ButterLib.Common.Helpers
#endif
{
#if !BANNERLORDBUTRSHARED_BUTTERLIB
    using SubModuleInfo_ = global::Bannerlord.BUTR.Shared.ModuleInfoExtended.SubModuleInfo2;
    using ModuleInfo_ = global::Bannerlord.BUTR.Shared.ModuleInfoExtended.ModuleInfo2;
    using global::Bannerlord.BUTR.Shared.Extensions;
#else
    using SubModuleInfo_ = global::Bannerlord.ButterLib.Common.Helpers.ExtendedSubModuleInfo;
    using ModuleInfo_ = global::Bannerlord.ButterLib.Common.Helpers.ExtendedModuleInfo;
    using global::Bannerlord.ButterLib.Common.Extensions;
#endif

    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;

    using global::System;
    using global::System.Collections.Generic;
    using global::System.IO;
    using global::System.Linq;
    using global::System.Xml;

    using global::TaleWorlds.MountAndBlade;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
#if !BANNERLORDBUTRSHARED_BUTTERLIB
    internal sealed class SubModuleInfo2 : IEquatable<SubModuleInfo2>
#else
    public sealed class ExtendedSubModuleInfo : IEquatable<ExtendedSubModuleInfo>
#endif
    {
        private static readonly string? ConfigName = new DirectoryInfo(Directory.GetCurrentDirectory())?.Name;

        private static bool CheckIfSubmoduleCanBeLoadable(SubModuleInfo_ subModuleInfo)
        {
            if (subModuleInfo.Tags.Count > 0)
            {
                foreach (var (key, value) in subModuleInfo.Tags)
                {
                    if (!global::Bannerlord.BUTR.Shared.Helpers.ModuleInfoHelper.GetSubModuleTagValiditiy(key, value))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public enum SubModuleTags
        {
            RejectedPlatform,
            ExclusivePlatform,
            DedicatedServerType,
            IsNoRenderModeElement,
            DependantRuntimeLibrary
        }

        public string Name { get; internal set; } = string.Empty;
		public string DLLName { get; internal set; } = string.Empty;
		public bool DLLExists { get; internal set; }
		public List<string> Assemblies { get; internal set; } = new();
		public string SubModuleClassType { get; internal set; } = string.Empty;
        public Dictionary<SubModuleTags, string> Tags { get; internal set; } = new();

        public MBSubModuleBase? SubModuleInstance { get; private set; }
        public bool IsLoadable { get; private set; }

		public void LoadFrom(XmlNode subModuleNode, string modulePath)
		{
            Assemblies.Clear();
			Tags.Clear();
			Name = subModuleNode?.SelectSingleNode("Name")?.Attributes["value"]?.InnerText ?? string.Empty;
			DLLName = subModuleNode?.SelectSingleNode("DLLName")?.Attributes["value"]?.InnerText ?? string.Empty;

			if (!string.IsNullOrEmpty(DLLName))
                DLLExists = File.Exists(Path.Combine(modulePath, "bin", ConfigName ?? "Win64_Shipping_Client", DLLName));

            SubModuleClassType = subModuleNode?.SelectSingleNode("SubModuleClassType")?.Attributes["value"]?.InnerText ?? string.Empty;
			if (subModuleNode?.SelectSingleNode("Assemblies") != null)
			{
				var assembliesList = subModuleNode?.SelectSingleNode("Assemblies")?.SelectNodes("Assembly");
				for (var i = 0; i < assembliesList?.Count; i++)
				{
                    if (assembliesList[i]?.Attributes["value"]?.InnerText is { } value)
					    Assemblies.Add(value);
				}
			}

            var tagsList = subModuleNode?.SelectSingleNode("Tags")?.SelectNodes("Tag");
            for (var i = 0; i < tagsList?.Count; i++)
			{
                if (tagsList[i]?.Attributes["key"]?.InnerText is { } key && tagsList[i]?.Attributes["value"]?.InnerText is { } value && Enum.TryParse<SubModuleTags>(key, out var subModuleTags))
				{
                    Tags.Add(subModuleTags, value);
                }
			}

            SubModuleInstance = Module.CurrentModule?.SubModules?.FirstOrDefault(s => s.GetType().FullName.Equals(SubModuleClassType, StringComparison.OrdinalIgnoreCase));
            IsLoadable = CheckIfSubmoduleCanBeLoadable(this);
		}

        public override string ToString() => $"{Name} - {DLLName}";

        public bool Equals(SubModuleInfo_? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is SubModuleInfo_ other && Equals(other)); 
        public override int GetHashCode() => Name.GetHashCode();
        public static bool operator ==(SubModuleInfo_? left, SubModuleInfo_? right) => Equals(left, right);
        public static bool operator !=(SubModuleInfo_? left, SubModuleInfo_? right) => !Equals(left, right);
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE
