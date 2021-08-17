﻿// <auto-generated>
//   This code file has automatically been added by the "Bannerlord.BUTR.Shared" NuGet package (https://www.nuget.org/packages/Bannerlord.BUTR.Shared).
//   Please see https://github.com/BUTR/Bannerlord.BUTR.Shared for more information.
//
//   IMPORTANT:
//   DO NOT DELETE THIS FILE if you are using a "packages.config" file to manage your NuGet references.
//   Consider migrating to PackageReferences instead:
//   https://docs.microsoft.com/en-us/nuget/consume-packages/migrate-packages-config-to-package-reference
//   Migrating brings the following benefits:
//   * The "Bannerlord.BUTR.Shared\Utils" folder and the "WrappedPropertyInfo.cs" file don't appear in your project.
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

namespace Bannerlord.BUTR.Shared.Utils
{
    using global::System;
    using global::System.Globalization;
    using global::System.Reflection;

    internal sealed class WrappedConstructorInfo : ConstructorInfo
    {
        private readonly object[] _args;
        private readonly ConstructorInfo _constructorInfoImplementation;

        public WrappedConstructorInfo(ConstructorInfo actualConstructorInfo, object[] args)
        {
            _constructorInfoImplementation = actualConstructorInfo;
            _args = args;
        }

        public override object[] GetCustomAttributes(bool inherit) =>
            _constructorInfoImplementation.GetCustomAttributes(inherit);

        public override bool IsDefined(Type attributeType, bool inherit) =>
            _constructorInfoImplementation.IsDefined(attributeType, inherit);

        public override ParameterInfo[] GetParameters() => _constructorInfoImplementation.GetParameters();

        public override MethodImplAttributes GetMethodImplementationFlags() =>
            _constructorInfoImplementation.GetMethodImplementationFlags();

        public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture) =>
            _constructorInfoImplementation.Invoke(obj, invokeAttr, binder, parameters, culture);

        public override string Name => _constructorInfoImplementation.Name;
        public override Type? DeclaringType => _constructorInfoImplementation.DeclaringType;
        public override Type? ReflectedType => _constructorInfoImplementation.ReflectedType;
        public override RuntimeMethodHandle MethodHandle => _constructorInfoImplementation.MethodHandle;
        public override MethodAttributes Attributes => _constructorInfoImplementation.Attributes;

        public override object[] GetCustomAttributes(Type attributeType, bool inherit) =>
            _constructorInfoImplementation.GetCustomAttributes(attributeType, inherit);

        public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture) =>
            _constructorInfoImplementation.Invoke(invokeAttr, binder, _args, culture);
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE