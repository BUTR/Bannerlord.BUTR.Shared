﻿// <auto-generated>
//   This code file has automatically been added by the "Bannerlord.BUTR.Shared" NuGet package (https://www.nuget.org/packages/Bannerlord.BUTR.Shared).
//   Please see https://github.com/BUTR/Bannerlord.BUTR.Shared for more information.
//
//   IMPORTANT:
//   DO NOT DELETE THIS FILE if you are using a "packages.config" file to manage your NuGet references.
//   Consider migrating to PackageReferences instead:
//   https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference
//   Migrating brings the following benefits:
//   * The "Bannerlord.BUTR.Shared/Helpers" folder and the "ModuleInfoHelper.cs" file don't appear in your project.
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
#if !BANNERLORDBUTRSHARED_ENABLE_WARNINGS
#pragma warning disable
#endif

namespace Bannerlord.BUTR.Shared.Helpers
{
    using global::Bannerlord.BUTR.Shared.Extensions;
    using global::Bannerlord.ModuleManager;

    using global::HarmonyLib;
    using global::HarmonyLib.BUTR.Extensions;

    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System;
    using global::System.Collections.Concurrent;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.IO;
    using global::System.Reflection;
    using global::System.Text;
    using global::System.Threading;
    using global::System.Xml;

    using global::TaleWorlds.Library;
    using global::TaleWorlds.Localization;
    using global::TaleWorlds.ModuleManager;
    using global::TaleWorlds.MountAndBlade;

    using Module = TaleWorlds.MountAndBlade.Module;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
    internal static class ModuleInfoHelper
    {
        public const string ModulesFolder = "Modules";
        public const string SubModuleFile = "SubModule.xml";

        private static readonly AccessTools.FieldRef<IPlatformModuleExtension>? _platformModuleExtensionField;

        static ModuleInfoHelper()
        {
            _platformModuleExtensionField = AccessTools2.StaticFieldRefAccess<IPlatformModuleExtension>("TaleWorlds.ModuleManager.ModuleHelper:_platformModuleExtension");
        }

        public static ModuleInfoExtendedWithMetadata? LoadFromId(string id) => GetModules().FirstOrDefault(x => x.Id == id);

        public static IEnumerable<ModuleInfoExtendedWithMetadata> GetLoadedModules()
        {
            var moduleNames = TaleWorlds.Engine.Utilities.GetModulesNames();
            if (moduleNames.Length == 0) yield break;

            var allModulesAvailable = GetModules().ToDictionary(x => x.Id, x => x);
            foreach (string modulesId in moduleNames)
            {
                if (allModulesAvailable.TryGetValue(modulesId, out var moduleInfo))
                    yield return moduleInfo;
            }
        }

        private static Lazy<List<ModuleInfoExtendedWithMetadata>> _cachedModules = new(() =>
        {
            var list = new List<ModuleInfoExtendedWithMetadata>();
            var foundIds = new HashSet<string>();
            foreach (var moduleInfo in GetPhysicalModules().Concat(GetPlatformModules()))
            {
                if (!foundIds.Contains(moduleInfo.Id.ToLower()))
                {
                    foundIds.Add(moduleInfo.Id.ToLower());
                    list.Add(moduleInfo);
                }
            }
            return list;
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Provides unordered modules
        /// </summary>
        public static IEnumerable<ModuleInfoExtendedWithMetadata> GetModules() => _cachedModules.Value;

        private static ConcurrentDictionary<string, string> _cachedAssemblyLocationToModulePath = new();
        public static string? GetModulePath(Type? type)
        {
            if (type is null)
                return null;

            if (string.IsNullOrWhiteSpace(type.Assembly.Location))
                return null;

            if (_cachedAssemblyLocationToModulePath.TryGetValue(type.Assembly.Location, out var modulePath))
                return modulePath;

            var assemblyFile = new FileInfo(Path.GetFullPath(type.Assembly.Location));

            static DirectoryInfo? GetMainDirectory(DirectoryInfo? directoryInfo)
            {
                while (directoryInfo?.Parent is not null && directoryInfo.Exists)
                {
                    if (directoryInfo.GetFiles(SubModuleFile).Length == 1)
                        return directoryInfo;

                    directoryInfo = directoryInfo.Parent;
                }
                return null;
            }

            return modulePath = GetMainDirectory(assemblyFile.Directory)?.FullName;
        }

        public static string? GetModulePath(ModuleInfoExtended module) => _cachedModules.Value.FirstOrDefault(x => x.Id == module.Id)?.Path;

        public static ModuleInfoExtendedWithMetadata? GetModuleByType(Type? type)
        {
            var modulePath = GetModulePath(type);
            return _cachedModules.Value.FirstOrDefault(x => x.Path == modulePath);
        }

        private static string GetFullPathWithEndingSlashes(string input) =>
            $"{Path.GetFullPath(input).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)}{Path.DirectorySeparatorChar}";

        public static IEnumerable<ModuleInfoExtendedWithMetadata> GetPhysicalModules()
        {
            if (string.IsNullOrEmpty(TaleWorlds.Library.BasePath.Name)) yield break;

            foreach (var modulePath in Directory.GetDirectories(Path.Combine(TaleWorlds.Library.BasePath.Name, ModulesFolder)))
            {
                if (TryReadXml(Path.Combine(modulePath, SubModuleFile), out var xml)&& ModuleInfoExtended.FromXml(xml) is { } moduleInfo)
                    yield return new ModuleInfoExtendedWithMetadata(moduleInfo, false, Path.GetFullPath(modulePath));
            }
        }

        public static IEnumerable<ModuleInfoExtendedWithMetadata> GetPlatformModules()
        {
            if (_platformModuleExtensionField == null) yield break;

            var platformModuleExtension = _platformModuleExtensionField();
            if (platformModuleExtension == null) yield break;

            var modulePaths = platformModuleExtension.GetModulePaths();
            if (modulePaths == null) yield break;

            foreach (string modulePath in modulePaths)
            {
                if (TryReadXml(Path.Combine(modulePath, SubModuleFile), out var xml) && ModuleInfoExtended.FromXml(xml) is { } moduleInfo)
                    yield return new ModuleInfoExtendedWithMetadata(moduleInfo, true, Path.GetFullPath(modulePath));
            }
        }

        public static bool CheckIfSubModuleCanBeLoaded(SubModuleInfoExtended subModuleInfo) => CheckIfSubModuleCanBeLoaded(subModuleInfo,
            ApplicationPlatform.CurrentPlatform, ApplicationPlatform.CurrentRuntimeLibrary, Module.CurrentModule.StartupInfo.DedicatedServerType, Module.CurrentModule.StartupInfo.PlayerHostedDedicatedServer);

        public static bool CheckIfSubModuleCanBeLoaded(SubModuleInfoExtended subModuleInfo, Platform cPlatform, Runtime cRuntime, DedicatedServerType cServerType, bool playerHostedDedicatedServer)
        {
            if (subModuleInfo.Tags.Count <= 0) return true;

            foreach (var (key, values) in subModuleInfo.Tags)
            {
                if (!Enum.TryParse<SubModuleTags>(key, out var tag))
                    continue;

                if (values.Any(value => !GetSubModuleTagValiditiy(tag, value, cPlatform, cRuntime, cServerType, playerHostedDedicatedServer)))
                    return false;
            }
            return true;
        }

        public static bool GetSubModuleTagValiditiy(SubModuleTags tag, string value) => GetSubModuleTagValiditiy(tag, value,
            ApplicationPlatform.CurrentPlatform, ApplicationPlatform.CurrentRuntimeLibrary, Module.CurrentModule.StartupInfo.DedicatedServerType, Module.CurrentModule.StartupInfo.PlayerHostedDedicatedServer);

        public static bool GetSubModuleTagValiditiy(SubModuleTags tag, string value, Platform cPlatform, Runtime cRuntime, DedicatedServerType cServerType, bool playerHostedDedicatedServer) => tag switch
        {
            SubModuleTags.RejectedPlatform => !Enum.TryParse<Platform>(value, out var platform) || cPlatform != platform,
            SubModuleTags.ExclusivePlatform => !Enum.TryParse<Platform>(value, out var platform) || cPlatform == platform,
            SubModuleTags.DependantRuntimeLibrary => !Enum.TryParse<Runtime>(value, out var runtime) || cRuntime == runtime,
            SubModuleTags.IsNoRenderModeElement => value.Equals("false"),
            SubModuleTags.DedicatedServerType => value.ToLower() switch
            {
                "none" => cServerType == DedicatedServerType.None,
                "both" => cServerType == DedicatedServerType.None,
                "custom" => cServerType == DedicatedServerType.Custom,
                "matchmaker" => cServerType == DedicatedServerType.Matchmaker,
                _ => false
            },
            SubModuleTags.PlayerHostedDedicatedServer => playerHostedDedicatedServer && value.Equals("true"),
            _ => true,
        };

        public static bool ValidateLoadOrder(Type subModuleType, out string report)
        {
            return ValidateLoadOrder(GetModuleByType(subModuleType), out report);
        }

        public static bool ValidateLoadOrder(ModuleInfoExtended? moduleInfo, out string report)
        {
            const string SErrorModuleNotFound = @"{=FE6ya1gzZR}{REQUIRED_MODULE} module was not found!";
            const string SErrorIncompatibleModuleFound = @"{=EvI6KPAqTT}Incompatible module {DENIED_MODULE} was found!";
            const string SErrorWrongModuleOrderTooEarly = @"{=5G9zffrgMh}{MODULE} is loaded before the {REQUIRED_MODULE}!{NL}Make sure {MODULE} is loaded after it!";
            const string SErrorWrongModuleOrderTooLate = @"{=UZ8zfvudMs}{MODULE} is loaded after the {REQUIRED_MODULE}!{NL}Make sure {MODULE} is loaded before it!";
            const string SErrorMutuallyExclusiveDirectives = @"{=FcR4BXnhx8}{MODULE} has mutually exclusive mod order directives specified for the {REQUIRED_MODULE}!";

            if (moduleInfo is null)
            {
                report = "CRITICAL ERROR";
                return false;
            }

            var loadedModules = ModuleInfoHelper.GetLoadedModules().ToList();
            var moduleIndex = CollectionsExtensions.IndexOf(loadedModules, moduleInfo);

            var sb = new StringBuilder();

            void ReportMissingModule(string requiredModuleId)
            {
                if (sb.Length != 0) sb.AppendLine();
                sb.AppendLine(new TextObject(SErrorModuleNotFound)
                    ?.SetTextVariable("REQUIRED_MODULE", requiredModuleId)
                    ?.ToString() ?? "ERROR");
            }

            void ReportIncompatibleModule(string deniedModuleId)
            {
                if (sb.Length != 0) sb.AppendLine();
                sb.AppendLine(new TextObject(SErrorIncompatibleModuleFound)
                    ?.SetTextVariable("DENIED_MODULE", deniedModuleId)
                    ?.ToString() ?? "ERROR");
            }

            void ReportLoadingOrderIssue(string reason, string requiredModuleId)
            {
                if (sb.Length != 0) sb.AppendLine();
                sb.AppendLine(new TextObject(reason)
                    ?.SetTextVariable("MODULE", moduleInfo.Id)
                    ?.SetTextVariable("REQUIRED_MODULE", requiredModuleId)
                    ?.SetTextVariable("NL", Environment.NewLine)
                    ?.ToString() ?? "ERROR");
            }

            void ReportMutuallyExclusiveDirectives(string requiredModuleId)
            {
                if (sb.Length != 0) sb.AppendLine();
                sb.AppendLine(new TextObject(SErrorMutuallyExclusiveDirectives)
                    ?.SetTextVariable("MODULE", moduleInfo.Id)
                    ?.SetTextVariable("REQUIRED_MODULE", requiredModuleId)
                    ?.ToString() ?? "ERROR");
            }

            void ValidateDependedModuleCompatibility(int deniedModuleIndex, string deniedModuleId)
            {
                if (deniedModuleIndex != -1)
                {
                    ReportIncompatibleModule(deniedModuleId);
                }
            }

            void ValidateDependedModuleLoadBeforeThis(int requiredModuleIndex, string requiredModuleId, bool isOptional = false)
            {
                if (!isOptional && requiredModuleIndex == -1)
                {
                    ReportMissingModule(requiredModuleId);
                }
                else if (requiredModuleIndex > moduleIndex)
                {
                    ReportLoadingOrderIssue(SErrorWrongModuleOrderTooEarly, requiredModuleId);
                }
            }

            void ValidateDependedModuleLoadAfterThis(int requiredModuleIndex, string requiredModuleId, bool isOptional)
            {
                if (requiredModuleIndex == -1)
                {
                    if (!isOptional) ReportMissingModule(requiredModuleId);
                }
                else if (requiredModuleIndex < moduleIndex)
                {
                    ReportLoadingOrderIssue(SErrorWrongModuleOrderTooLate, requiredModuleId);
                }
            }

            foreach (var dependedModule in moduleInfo.DependentModules)
            {
                var module = loadedModules.SingleOrDefault(x => x.Id == dependedModule.Id);
                var dependedModuleIndex = module is not null ? loadedModules.IndexOf(module) : -1;
                ValidateDependedModuleLoadBeforeThis(dependedModuleIndex, dependedModule.Id);
            }
            foreach (var dependedModule in moduleInfo.DependentModuleMetadatas)
            {
                var module = loadedModules.SingleOrDefault(x => x.Id == dependedModule.Id);
                var dependedModuleIndex = module is not null ? loadedModules.IndexOf(module) : -1;

                if (dependedModule.IsIncompatible)
                {
                    if (moduleInfo.DependentModules.Any(dm => dm.Id == dependedModule.Id))
                    {
                        ReportMutuallyExclusiveDirectives(dependedModule.Id);
                        continue;
                    }
                    ValidateDependedModuleCompatibility(dependedModuleIndex, dependedModule.Id);
                }
                else if (dependedModule.LoadType == LoadType.LoadBeforeThis)
                {
                    if (moduleInfo.DependentModules.Any(dm => dm.Id == dependedModule.Id)) continue;
                    ValidateDependedModuleLoadBeforeThis(dependedModuleIndex, dependedModule.Id, dependedModule.IsOptional);
                }
                else if (dependedModule.LoadType == LoadType.LoadAfterThis)
                {
                    if (moduleInfo.DependentModules.Any(dm => dm.Id == dependedModule.Id) || (moduleInfo.DependentModuleMetadatas.Any(dm => dm.Id == dependedModule.Id && dm.LoadType == LoadType.LoadBeforeThis)))
                    {
                        ReportMutuallyExclusiveDirectives(dependedModule.Id);
                        continue;
                    }
                    ValidateDependedModuleLoadAfterThis(dependedModuleIndex, dependedModule.Id, dependedModule.IsOptional);
                }
            }

            if (sb.Length > 0)
            {
                report = sb.ToString();
                return false;
            }

            report = string.Empty;
            return true;
        }


        public static bool IsModuleAssembly(ModuleInfoExtendedWithMetadata loadedModule, Assembly assembly)
        {
            if (assembly.IsDynamic || string.IsNullOrWhiteSpace(assembly.CodeBase))
                return false;

            var modulePath = new Uri(Path.GetFullPath(loadedModule.Path));
            var moduleDirectory = Path.GetFileName(loadedModule.Path);

            var assemblyPath = new Uri(assembly.CodeBase);
            var relativePath = modulePath.MakeRelativeUri(assemblyPath);
            return relativePath.OriginalString.StartsWith(moduleDirectory);
        }

        private static bool TryReadXml(string path, out XmlDocument? xml)
        {
            try
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(File.ReadAllText(path));
                xml = xmlDocument;
                return true;
            }
            catch (Exception)
            {
                xml = null;
                return false;
            }
        }
    }
}

#if !BANNERLORDBUTRSHARED_ENABLE_WARNINGS
#pragma warning restore
#endif
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE