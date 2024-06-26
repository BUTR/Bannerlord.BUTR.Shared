﻿// <auto-generated>
//   This code file has automatically been added by the "Bannerlord.BUTR.Shared" NuGet package (https://www.nuget.org/packages/Bannerlord.BUTR.Shared).
//   Please see https://github.com/BUTR/Bannerlord.BUTR.Shared for more information.
//
//   IMPORTANT:
//   DO NOT DELETE THIS FILE if you are using a "packages.config" file to manage your NuGet references.
//   Consider migrating to PackageReferences instead:
//   https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference
//   Migrating brings the following benefits:
//   * The "Bannerlord.BUTR.Shared/Helpers" folder and the "ModuleInfoExtendedWithPath.cs" file don't appear in your project.
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
    using global::Bannerlord.ModuleManager;

    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, global::System.Diagnostics.DebuggerNonUserCode]
#endif
    internal record ModuleInfoExtendedHelper : ModuleInfoExtended
    {
        public bool IsExternal { get; }
        public string Path { get; }

        public ModuleInfoExtendedHelper(ModuleInfoExtended module, bool isExternal, string path) : base(module)
        {
            IsExternal = isExternal;
            Path = path;
        }
    }
}

#if !BANNERLORDBUTRSHARED_ENABLE_WARNINGS
#pragma warning restore
#endif
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE