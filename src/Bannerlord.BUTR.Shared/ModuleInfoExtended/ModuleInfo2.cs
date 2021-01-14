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

namespace Bannerlord.BUTR.Shared.ModuleInfoExtended
{
    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;

    using global::System;
    using global::System.Collections.Generic;
    using global::System.IO;
    using global::System.Linq;
    using global::System.Xml;
    
    using global::Bannerlord.BUTR.Shared.Helpers;

    using global::TaleWorlds.Library;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
    internal sealed class ModuleInfo2 : IEquatable<ModuleInfo2>
    {
        private static string NativeModuleId = "Native";
        private static string[] OfficialModuleIds = { NativeModuleId, "SandBox", "SandBoxCore", "StoryMode", "CustomBattle" };
        public static string PathPrefix => Path.Combine(BasePath.Name, "Modules");

        public string Id { get; internal set; } = string.Empty;
        public string Name { get; internal set; } = string.Empty;
        public bool IsOfficial { get; internal set; }
        public ApplicationVersion Version { get; internal set; }
        public string Alias { get; internal set; } = string.Empty;
        public bool IsSingleplayerModule { get; internal set; }
        public bool IsMultiplayerModule { get; internal set; }
        public bool IsSelected { get; set; }
        public List<SubModuleInfo2> SubModules { get; internal set; } = new();
        public List<DependedModule> DependedModules { get; internal set; } = new();

        public string Url { get; internal set; } = string.Empty;

        public List<DependedModuleMetadata> DependedModuleMetadatas { get; internal set; }  = new();

        public void Load(string alias)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(Path.Combine(PathPrefix, alias, "SubModule.xml"));
            Load(xmlDocument);
        }

        public void LoadWithFullPath(string fullPath)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(Path.Combine(fullPath, "SubModule.xml"));
            Load(xmlDocument);
        }

        public void Load(XmlDocument xmlDocument)
        {
            SubModules.Clear();
            DependedModules.Clear();
            DependedModuleMetadatas.Clear();

            var moduleNode = xmlDocument.SelectSingleNode("Module");

            Alias = moduleNode?.SelectSingleNode("Alias")?.Attributes?["value"]?.InnerText ?? string.Empty;
            Name = moduleNode?.SelectSingleNode("Name")?.Attributes?["value"]?.InnerText ?? string.Empty;
            Id = moduleNode?.SelectSingleNode("Id")?.Attributes?["value"]?.InnerText ?? string.Empty;
            ApplicationVersionUtils.TryParse(moduleNode?.SelectSingleNode("Version")?.Attributes?["value"]?.InnerText, out var parsedVersion);
            Version = parsedVersion;

            IsOfficial = moduleNode?.SelectSingleNode("Official")?.Attributes?["value"]?.InnerText?.Equals("true") == true;
            IsSelected = moduleNode?.SelectSingleNode("DefaultModule")?.Attributes?["value"]?.InnerText?.Equals("true") == true || IsNative();
            IsSingleplayerModule = moduleNode?.SelectSingleNode("SingleplayerModule")?.Attributes?["value"]?.InnerText?.Equals("true") == true;
            IsMultiplayerModule = moduleNode?.SelectSingleNode("MultiplayerModule")?.Attributes?["value"]?.InnerText?.Equals("true") == true;

            var dependedModules = moduleNode?.SelectSingleNode("DependedModules");
            var dependedModulesList = dependedModules?.SelectNodes("DependedModule");
            for (var i = 0; i < dependedModulesList?.Count; i++)
            {
                if (dependedModulesList[i]?.Attributes["Id"] is { } idAttr)
                {
                    ApplicationVersionUtils.TryParse(dependedModulesList[i]?.Attributes?["DependentVersion"]?.InnerText, out var version);
                    DependedModules.Add(new DependedModule
                    {
                        ModuleId = idAttr.InnerText,
                        Version = version
                    });
                }
            }

            var subModules = moduleNode?.SelectSingleNode("SubModules");
            var subModuleList = subModules?.SelectNodes("SubModule");
            for (var i = 0; i < subModuleList?.Count; i++)
            {
                var subModuleInfo = new SubModuleInfo2();
                try
                {
                    subModuleInfo.LoadFrom(subModuleList[i], Path.Combine(PathPrefix, Alias));
                    SubModules.Add(subModuleInfo);
                }
                catch { }
            }

            // Custom data
            Url = moduleNode?.SelectSingleNode("Url")?.Attributes?["value"]?.InnerText ?? string.Empty;

            var dependedModuleMetadatas = moduleNode?.SelectSingleNode("DependedModuleMetadatas");
            var dependedModuleMetadatasList = dependedModuleMetadatas?.SelectNodes("DependedModuleMetadata");
            for (var i = 0; i < dependedModuleMetadatasList?.Count; i++)
            {
                if (dependedModuleMetadatasList[i]?.Attributes["id"] is { } idAttr)
                {
                    var incompatible = dependedModuleMetadatasList[i]?.Attributes["incompatible"]?.InnerText.Equals("true") ?? false;
                    if (incompatible)
                    {
                        DependedModuleMetadatas.Add(new DependedModuleMetadata
                        {
                            Id = idAttr.InnerText,
                            LoadType = LoadType.NONE,
                            IsOptional = false,
                            IsIncompatible = incompatible,
                            Version = ApplicationVersionUtils.GetEmpty()
                        });
                    }
                    else if (dependedModuleMetadatasList[i]?.Attributes["order"] is { } orderAttr && Enum.TryParse<LoadTypeParse>(orderAttr.InnerText, out var order))
                    {
                        var optional = dependedModuleMetadatasList[i]?.Attributes["optional"]?.InnerText.Equals("true") ?? false;
                        var version = ApplicationVersionUtils.TryParse(dependedModuleMetadatasList[i]?.Attributes["version"]?.InnerText, out var v) ? v : ApplicationVersionUtils.GetEmpty();
                        DependedModuleMetadatas.Add(new DependedModuleMetadata
                        {
                            Id = idAttr.InnerText,
                            LoadType = (LoadType) order,
                            IsOptional = optional,
                            IsIncompatible = incompatible,
                            Version = version
                        });
                    }
                }
            }

            // Fixed Launcher supported optional tag
            var loadAfterModules = moduleNode?.SelectSingleNode("LoadAfterModules");
            var loadAfterModuleList = loadAfterModules?.SelectNodes("LoadAfterModule");
            for (var i = 0; i < loadAfterModuleList?.Count; i++)
            {
                if (loadAfterModuleList[i]?.Attributes["Id"] is { } idAttr)
                {
                    DependedModuleMetadatas.Add(new DependedModuleMetadata
                    {
                        Id = idAttr.InnerText,
                        LoadType = LoadType.NONE,
                        IsOptional = true,
                        IsIncompatible = false,
                        Version = ApplicationVersionUtils.GetEmpty()
                    });
                }
            }

            // Bannerlord Launcher supported optional tag
            var optionalDependModules = moduleNode?.SelectSingleNode("OptionalDependModules");
            var optionalDependModuleList =
                (dependedModules?.SelectNodes("OptionalDependModule")?.Cast<XmlNode>() ?? Enumerable.Empty<XmlNode>())
                .Concat(optionalDependModules?.SelectNodes("OptionalDependModule")?.Cast<XmlNode>() ?? Enumerable.Empty<XmlNode>())
                .Concat(optionalDependModules?.SelectNodes("DependModule")?.Cast<XmlNode>() ?? Enumerable.Empty<XmlNode>()).ToList();
            for (var i = 0; i < optionalDependModuleList.Count; i++)
            {
                if (optionalDependModuleList[i]?.Attributes["Id"] is { } idAttr)
                {
                    DependedModuleMetadatas.Add(new DependedModuleMetadata
                    {
                        Id = idAttr.InnerText,
                        LoadType = LoadType.NONE,
                        IsOptional = true,
                        IsIncompatible = false,
                        Version = ApplicationVersionUtils.GetEmpty()
                    });
                }
            }

            var requiredGameVersion = moduleNode?.SelectSingleNode("RequiredGameVersion");
            var requiredGameVersionVal = requiredGameVersion?.Attributes?["value"]?.InnerText ?? string.Empty;
            var requiredGameVersionOptional = requiredGameVersion?.Attributes?["optional"]?.InnerText?.Equals("true") == true;
            if (!string.IsNullOrWhiteSpace(requiredGameVersionVal) && ApplicationVersionUtils.TryParse(requiredGameVersionVal, out var gameVersion))
            {
                foreach (var moduleId in OfficialModuleIds)
                {
                    var isNative = moduleId.Equals(NativeModuleId);

                    // Override any existing metadata
                    if (DependedModuleMetadatas.Find(dmm => dmm.Id.Equals(moduleId, StringComparison.Ordinal)) is { } module)
                        DependedModuleMetadatas.Remove(module);

                    DependedModuleMetadatas.Add(new DependedModuleMetadata
                    {
                        Id = moduleId,
                        LoadType = LoadType.LoadBeforeThis,
                        IsOptional = requiredGameVersionOptional && !isNative,
                        IsIncompatible = false,
                        Version = gameVersion
                    });
                }
            }
        }

        public bool IsNative() => Id.Equals(NativeModuleId, StringComparison.OrdinalIgnoreCase);

        public override string ToString() => $"{Id} - {Version}";

        public bool Equals(ModuleInfo2? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }
        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || (obj is ModuleInfo2 other && Equals(other));

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(ModuleInfo2? left, ModuleInfo2? right) => Equals(left, right);
        public static bool operator !=(ModuleInfo2? left, ModuleInfo2? right) => !Equals(left, right);
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE
