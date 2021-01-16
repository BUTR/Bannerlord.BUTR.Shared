﻿// <auto-generated>
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
#if !BANNERLORDBUTRSHARED_BUTTERLIB
    using SubModuleInfo_ = global::Bannerlord.BUTR.Shared.ModuleInfoExtended.SubModuleInfo2;
    using ModuleInfo_ = global::Bannerlord.BUTR.Shared.ModuleInfoExtended.ModuleInfo2;
#else
    using SubModuleInfo_ = global::Bannerlord.ButterLib.Common.Helpers.ExtendedSubModuleInfo;
    using ModuleInfo_ = global::Bannerlord.ButterLib.Common.Helpers.ExtendedModuleInfo;
#endif

    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.IO;
    using global::System.Reflection;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
    internal static class ModuleUtils
    {
        private delegate string[] GetModulesNamesDelegate();

        private static readonly GetModulesNamesDelegate? GetModulesNames;

        private static readonly Type? _moduleHelperType = Type.GetType("TaleWorlds.ModuleManager.ModuleHelper, TaleWorlds.ModuleManager", false);
        private static readonly FieldInfo? _platformModuleExtensionField = _moduleHelperType?.GetField("_platformModuleExtension");

        private delegate bool GetSubModuleValiditiyDelegate(object instance, SubModuleInfo_.SubModuleTags tag, string value);
        private delegate object GetCurrentModuleDelegate();
        private static readonly Type? _moduleType = Type.GetType("TaleWorlds.MountAndBlade.Module, TaleWorlds.MountAndBlade", false);
        private static readonly GetSubModuleValiditiyDelegate? GetSubModuleValiditiy;
        private static readonly GetCurrentModuleDelegate? GetCurrentModule;

        static ModuleUtils()
        {
            var engineUtilitiesType =
                Type.GetType("TaleWorlds.Engine.Utilities, TaleWorlds.Engine", false);
            GetModulesNames =
                ReflectionHelper.GetDelegate<GetModulesNamesDelegate>(engineUtilitiesType?.GetMethod("GetModulesNames", BindingFlags.Public | BindingFlags.Static));

            GetCurrentModule =
                ReflectionHelper.GetDelegate<GetCurrentModuleDelegate>(_moduleType?.GetProperty("CurrentModule", BindingFlags.Public | BindingFlags.Static)?.GetMethod);
            GetSubModuleValiditiy =
                ReflectionHelper.GetDelegateObjectInstance<GetSubModuleValiditiyDelegate>(_moduleType.GetMethod("GetSubModuleValiditiy"));
        }

        public static IEnumerable<ModuleInfo_> GetLoadedModules()
        {
            if (GetModulesNames == null) yield break;

            foreach (string modulesName in GetModulesNames())
            {
                var moduleInfo = new ModuleInfo_();
                moduleInfo.Load(modulesName);
                yield return moduleInfo;
            }
        }

        public static IEnumerable<ModuleInfo_> GetModules()
        {
            var list = new List<ModuleInfo_>();
            var physicalModules = GetPhysicalModules();
            var platformModules = GetPlatformModules();
            list.AddRange(physicalModules);
            list.AddRange(platformModules);
            var _allFoundModules = new Dictionary<string, ModuleInfo_>();
            foreach (var moduleInfo in list)
            {
                if (!_allFoundModules.ContainsKey(moduleInfo.Id.ToLower()))
                {
                    _allFoundModules.Add(moduleInfo.Id.ToLower(), moduleInfo);
                }
            }
            return list;
        }

        private static IEnumerable<ModuleInfo_> GetPhysicalModules()
        {
            foreach (string text in GetModulePaths(ModuleInfo_.PathPrefix, 1).ToArray<string>())
            {
                var moduleInfo = new ModuleInfo_();
                try
                {
                    string directoryName = System.IO.Path.GetDirectoryName(text);
                    moduleInfo.LoadWithFullPath(directoryName);
                }
                catch { }
                yield return moduleInfo;
            }
        }

        private static IEnumerable<ModuleInfo_> GetPlatformModules()
        {
            if (_moduleHelperType == null) yield break;
            if (_platformModuleExtensionField == null) yield break;

            var platformModuleExtension = _platformModuleExtensionField.GetValue(null);
            if (platformModuleExtension == null) yield break;

            var getModulePathsInvoker = platformModuleExtension.GetType().GetMethod("GetModulePaths", BindingFlags.Public | BindingFlags.Instance);
            if (getModulePathsInvoker == null) yield break;

            var modulePaths = getModulePathsInvoker.Invoke(platformModuleExtension, Array.Empty<object>()) as string[];
            if (modulePaths == null) yield break;

            foreach (string path in modulePaths)
            {
                var moduleInfo = new ModuleInfo_();
                try
                {
                    moduleInfo.LoadWithFullPath(path);
                }
                catch { }
                yield return moduleInfo;
            }
        }

        internal static IEnumerable<string> GetModulePaths(string directoryPath, int searchDepth)
        {
            if (searchDepth > 0)
            {
                foreach (string directories in Directory.GetDirectories(directoryPath))
                {
                    foreach (string path in GetModulePaths(directories, searchDepth - 1))
                    {
                        yield return path;
                    }
                }
            }
            foreach (string path in Directory.GetFiles(directoryPath, "SubModule.xml"))
            {
                yield return path;
            }
        }

        internal static bool GetSubModuleTagValiditiy(SubModuleInfo_.SubModuleTags tag, string value)
        {
            if (GetSubModuleValiditiy is null || GetCurrentModule is null || GetCurrentModule() is not { } instance)
                return true;

            return GetSubModuleValiditiy(instance, tag, value);
        }
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE
