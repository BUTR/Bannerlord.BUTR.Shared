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
    using global::System.Collections.Generic;
    using global::System.ComponentModel;
    using global::System.Globalization;
    using global::System.Linq;
    using global::System.Reflection;

    internal sealed class WrappedPropertyInfo : PropertyInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private readonly object _instance;
        private readonly PropertyInfo _propertyInfoImplementation;

        public WrappedPropertyInfo(PropertyInfo actualPropertyInfo, object instance)
        {
            _propertyInfoImplementation = actualPropertyInfo;
            _instance = instance;
        }

        public override PropertyAttributes Attributes => _propertyInfoImplementation.Attributes;
        public override bool CanRead => _propertyInfoImplementation.CanRead;
        public override bool CanWrite => _propertyInfoImplementation.CanWrite;
        public override IEnumerable<CustomAttributeData> CustomAttributes => _propertyInfoImplementation.CustomAttributes;
        public override Type? DeclaringType => _propertyInfoImplementation.DeclaringType;
        public override MethodInfo? GetMethod => _propertyInfoImplementation.GetMethod;
        public override MemberTypes MemberType => _propertyInfoImplementation.MemberType;
        public override int MetadataToken => _propertyInfoImplementation.MetadataToken;
        public override Module Module => _propertyInfoImplementation.Module;
        public override string Name => _propertyInfoImplementation.Name;
        public override Type PropertyType => _propertyInfoImplementation.PropertyType;
        public override Type? ReflectedType => _propertyInfoImplementation.ReflectedType;
        public override MethodInfo? SetMethod => _propertyInfoImplementation.SetMethod;

        public override MethodInfo[] GetAccessors(bool nonPublic) => _propertyInfoImplementation.GetAccessors(nonPublic)
            .Select(m => new WrappedMethodInfo(m, _instance))
            .Cast<MethodInfo>()
            .ToArray();
        public override object? GetConstantValue() => _propertyInfoImplementation.GetConstantValue();
        public override object[] GetCustomAttributes(Type attributeType, bool inherit) => _propertyInfoImplementation.GetCustomAttributes(attributeType, inherit);
        public override object[] GetCustomAttributes(bool inherit) => _propertyInfoImplementation.GetCustomAttributes(inherit);
        public override IList<CustomAttributeData> GetCustomAttributesData() => _propertyInfoImplementation.GetCustomAttributesData();
        public override MethodInfo GetGetMethod(bool nonPublic)
        {
            var getMethod = _propertyInfoImplementation.GetGetMethod(nonPublic);
            return getMethod is null ? null! : new WrappedMethodInfo(getMethod, _instance);
        }
        public override ParameterInfo[] GetIndexParameters() => _propertyInfoImplementation.GetIndexParameters();
        public override Type[] GetOptionalCustomModifiers() => _propertyInfoImplementation.GetOptionalCustomModifiers();
        public override object? GetRawConstantValue() => _propertyInfoImplementation.GetRawConstantValue();
        public override Type[] GetRequiredCustomModifiers() => _propertyInfoImplementation.GetRequiredCustomModifiers();
        public override MethodInfo GetSetMethod(bool nonPublic)
        {
            var setMethod = _propertyInfoImplementation.GetSetMethod(nonPublic);
            return setMethod is null ? null! : new WrappedMethodInfo(setMethod, _instance);
        }
        public override object? GetValue(object? obj, object?[]? index) => _propertyInfoImplementation.GetValue(_instance, index);
        public override object? GetValue(object? obj, BindingFlags invokeAttr, Binder? binder, object?[]? index, CultureInfo? culture) =>
            _propertyInfoImplementation.GetValue(_instance, invokeAttr, binder, index, culture);
        public override bool IsDefined(Type attributeType, bool inherit) => _propertyInfoImplementation.IsDefined(attributeType, inherit);
        public override void SetValue(object? obj, object? value, object?[]? index)
        {
            _propertyInfoImplementation.SetValue(_instance, value, index);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyInfoImplementation.Name));
        }
        public override void SetValue(object? obj, object? value, BindingFlags invokeAttr, Binder? binder, object?[]? index, CultureInfo? culture)
        {
            _propertyInfoImplementation.SetValue(_instance, value, invokeAttr, binder, index, culture);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyInfoImplementation.Name));
        }

        public override string? ToString() => _propertyInfoImplementation.ToString();
        public override bool Equals(object? obj) => obj switch
        {
            WrappedPropertyInfo proxy => _propertyInfoImplementation.Equals(proxy._propertyInfoImplementation),
            PropertyInfo propertyInfo => _propertyInfoImplementation.Equals(propertyInfo),
            _ => _propertyInfoImplementation.Equals(obj)
        };
        public override int GetHashCode() => _propertyInfoImplementation.GetHashCode();
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE