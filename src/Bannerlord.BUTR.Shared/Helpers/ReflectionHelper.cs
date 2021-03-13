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
    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Linq.Expressions;
    using global::System.Reflection;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
    internal static class ReflectionHelper
    {
        public static TDelegate? GetDelegate<TDelegate>(ConstructorInfo? constructorInfo) where TDelegate : Delegate
        {
            if (constructorInfo is null) return null;
            
            if (typeof(TDelegate).GetMethod("Invoke") is not { } delegateInvoke) return null;

            var delegateParameters = delegateInvoke.GetParameters();
            var constructorParameters = constructorInfo.GetParameters();

            if (delegateParameters.Length != constructorParameters.Length) return null;

            try
            {
                var instance = Expression.Parameter(typeof(object), "instance");
                var returnParameters = delegateParameters
                    .Select((pi, i) => Expression.Parameter(pi.ParameterType, $"p{i}"))
                    .ToList();
                var inputParameters = returnParameters
                    .Select((pe, i) => Expression.Convert(pe, constructorParameters[i].ParameterType))
                    .ToList();

                Expression body = Expression.New(constructorInfo, inputParameters);
                if (delegateInvoke.ReturnType != constructorInfo.DeclaringType) body = Expression.Convert(body, constructorInfo.DeclaringType);

                return Expression.Lambda<TDelegate>(body, returnParameters).Compile();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>Get a delegate for a method described by <paramref name="methodInfo"/>.</summary>
        /// <param name="methodInfo">The method's <see cref="MethodInfo"/>.</param>
        /// <returns>A delegate or <see langword="null"/> when <paramref name="methodInfo"/> is <see langword="null"/>.</returns>
        public static TDelegate? GetDelegate<TDelegate>(MethodInfo? methodInfo) where TDelegate : Delegate
        {
            if (methodInfo is null) return null;
            
            if (typeof(TDelegate).GetMethod("Invoke") is not { } delegateInvoke) return null;

            var delegateParameters = delegateInvoke.GetParameters();
            var methodParameters = methodInfo.GetParameters();

            if (delegateParameters.Length != methodParameters.Length) return null;

            try
            {
                var returnParameters = delegateParameters
                    .Select((pi, i) => Expression.Parameter(pi.ParameterType, $"p{i}"))
                    .ToList();
                var inputParameters = returnParameters
                    .Select((pe, i) => Expression.Convert(pe, methodParameters[i].ParameterType))
                    .ToList();

                Expression body = Expression.Call(methodInfo, inputParameters);
                if (delegateInvoke.ReturnType != methodInfo.ReturnType) body = Expression.Convert(body, methodInfo.ReturnType);

                return Expression.Lambda<TDelegate>(body, returnParameters).Compile();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get a delegate for an instance method described by <paramref name="methodInfo"/> and bound to <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">The instance for which the method is defined.</param>
        /// <param name="methodInfo">The method's <see cref="MethodInfo"/>.</param>
        /// <returns>
        /// A delegate or <see langword="null"/> when <paramref name="instance"/> or <paramref name="methodInfo"/>
        /// is <see langword="null"/> or when the method cannot be found.
        /// </returns>
        public static TDelegate? GetDelegate<TDelegate>(object? instance, MethodInfo? methodInfo) where TDelegate : Delegate
        {
            if (methodInfo is null) return null;

            if (typeof(TDelegate).GetMethod("Invoke") is not { } delegateInvoke) return null;

            var delegateParameters = delegateInvoke.GetParameters();
            var methodParameters = methodInfo.GetParameters();

            if (delegateParameters.Length != methodParameters.Length) return null;

            try
            {
                var returnParameters = delegateParameters
                    .Select((pi, i) => Expression.Parameter(pi.ParameterType, $"p{i}"))
                    .ToList();
                var inputParameters = returnParameters
                    .Select((pe, i) => Expression.Convert(pe, methodParameters[i].ParameterType))
                    .ToList();

                Expression body = Expression.Call(Expression.Constant(instance), methodInfo, inputParameters);
                if (delegateInvoke.ReturnType != methodInfo.ReturnType) body = Expression.Convert(body, methodInfo.ReturnType);

                return Expression.Lambda<TDelegate>(body, returnParameters).Compile();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static TDelegate? GetDelegateObjectInstance<TDelegate>(MethodInfo? methodInfo) where TDelegate : Delegate
        {
            if (methodInfo is null) return null;
            
            if (typeof(TDelegate).GetMethod("Invoke") is not { } delegateInvoke) return null;

            var delegateParameters = delegateInvoke.GetParameters();
            if (delegateParameters.Length == 0) return null;
            var methodParameters = methodInfo.GetParameters();

            if (delegateParameters.Length != (methodParameters.Length + 1)) return null;

            try
            {
                var instance = Expression.Parameter(typeof(object), "instance");
                var returnParameters = delegateParameters
                    .Skip(1)
                    .Select((pi, i) => Expression.Parameter(pi.ParameterType, $"p{i}"))
                    .ToList();
                var inputParameters = returnParameters
                    .Select((pe, i) => Expression.Convert(pe, methodParameters[i].ParameterType))
                    .ToList();

                Expression body = Expression.Call(Expression.Convert(instance, methodInfo.DeclaringType), methodInfo, inputParameters);
                if (delegateInvoke.ReturnType != methodInfo.ReturnType) body = Expression.Convert(body, methodInfo.ReturnType);

                return Expression.Lambda<TDelegate>(body, new List<ParameterExpression> { instance }.Concat(returnParameters)).Compile();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE