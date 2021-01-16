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

using Bannerlord.BUTR.Shared.Extensions;

#if !BANNERLORDBUTRSHARED_DISABLE
#nullable enable
#pragma warning disable

namespace Bannerlord.BUTR.Shared.Helpers
{
    using global::System.Diagnostics;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Collections.Generic;
    using global::System.Linq;

    using global::TaleWorlds.Localization;

#if !BANNERLORDBUTRSHARED_INCLUDE_IN_CODE_COVERAGE
    [ExcludeFromCodeCoverage, DebuggerNonUserCode]
#endif
    internal static class TextObjectUtils
    {
        private delegate TextObject ConstructorDelegate();

        private delegate void SetTextVariableTextObjectDelegate(TextObject instance, string tag, TextObject variable);
        private delegate TextObject SetTextVariable2TextObjectDelegate(TextObject instance, string tag, TextObject variable);

        private delegate void SetTextVariableTextObjectStringDelegate(TextObject instance, string tag, string variable);
        private delegate TextObject SetTextVariable2TextObjectStringDelegate(TextObject instance, string tag, string variable);

        private delegate void SetTextVariableTextObjectInt32Delegate(TextObject instance, string tag, int variable);
        private delegate TextObject SetTextVariable2TextObjectInt32Delegate(TextObject instance, string tag, int variable);

        private delegate void SetTextVariableTextObjectSingleDelegate(TextObject instance, string tag, float variable);
        private delegate TextObject SetTextVariable2TextObjectSingleDelegate(TextObject instance, string tag, float variable);

        private delegate TextObject SetTextVariableFromObjectDelegate(TextObject instance, string tag, object variable);

        private static readonly ConstructorDelegate? TextObjectConstructor;
        private static readonly SetTextVariableTextObjectDelegate? SetTextVariableTextObject;
        private static readonly SetTextVariable2TextObjectDelegate? SetTextVariable2TextObject;
        private static readonly SetTextVariableTextObjectStringDelegate? SetTextVariableTextObjectString;
        private static readonly SetTextVariable2TextObjectStringDelegate? SetTextVariable2TextObjectString;
        private static readonly SetTextVariableTextObjectInt32Delegate? SetTextVariableTextObjectInt32;
        private static readonly SetTextVariable2TextObjectInt32Delegate? SetTextVariable2TextObjectInt32;
        private static readonly SetTextVariableTextObjectSingleDelegate? SetTextVariableTextObjectSingle;
        private static readonly SetTextVariable2TextObjectSingleDelegate? SetTextVariable2TextObjectSingle;
        private static readonly SetTextVariableFromObjectDelegate? SetTextVariableFromObject;
        static TextObjectUtils()
        {
            if (typeof(TextObject).GetConstructors().FirstOrDefault() is { } constructorInfo)
                TextObjectConstructor = ReflectionHelper.GetDelegate<ConstructorDelegate>(constructorInfo);

            if (typeof(TextObject).GetMethod("SetTextVariable", new[] {typeof(string), typeof(TextObject)}) is { } setTextVariableTO)
            {
                SetTextVariableTextObject = ReflectionHelper.GetDelegate<SetTextVariableTextObjectDelegate>(setTextVariableTO);
                SetTextVariable2TextObject = ReflectionHelper.GetDelegate<SetTextVariable2TextObjectDelegate>(setTextVariableTO);
            }
            if (typeof(TextObject).GetMethod("SetTextVariable", new[] {typeof(string), typeof(string)}) is { } setTextVariableStr)
            {
                SetTextVariableTextObjectString = ReflectionHelper.GetDelegate<SetTextVariableTextObjectStringDelegate>(setTextVariableStr);
                SetTextVariable2TextObjectString = ReflectionHelper.GetDelegate<SetTextVariable2TextObjectStringDelegate>(setTextVariableStr);
            }
            if (typeof(TextObject).GetMethod("SetTextVariable", new[] {typeof(string), typeof(int)}) is { } setTextVariableInt)
            {
                SetTextVariableTextObjectInt32 = ReflectionHelper.GetDelegate<SetTextVariableTextObjectInt32Delegate>(setTextVariableInt);
                SetTextVariable2TextObjectInt32 = ReflectionHelper.GetDelegate<SetTextVariable2TextObjectInt32Delegate>(setTextVariableInt);
            }
            if (typeof(TextObject).GetMethod("SetTextVariable", new[] {typeof(string), typeof(float)}) is { } setTextVariableFloat)
            {
                SetTextVariableTextObjectSingle = ReflectionHelper.GetDelegate<SetTextVariableTextObjectSingleDelegate>(setTextVariableFloat);
                SetTextVariable2TextObjectSingle = ReflectionHelper.GetDelegate<SetTextVariable2TextObjectSingleDelegate>(setTextVariableFloat);
            }
            if (typeof(TextObject).GetMethod("SetTextVariableFromObject", new[] {typeof(string), typeof(object)}) is { } setTextVariableFromObject)
            {
                SetTextVariableFromObject = ReflectionHelper.GetDelegate<SetTextVariableFromObjectDelegate>(setTextVariableFromObject);
            }
        }

        public static TextObject? Create(string value) => TextObjectConstructor is not null ? TextObjectConstructor() : null;
        public static TextObject? Create(string value, Dictionary<string, TextObject> attributes)
        {
            if (Create(value) is { } textObject)
            {
                foreach (var (key, value2) in attributes)
                    textObject.SetTextVariable2(key, value2);
                return textObject;
            }
            return null;
        }
        public static TextObject? Create(string value, Dictionary<string, object> attributes)
        {
            if (SetTextVariableFromObject is not null && Create(value) is { } textObject)
            {
                foreach (var (key, value2) in attributes)
                    SetTextVariableFromObject(textObject, key, value2);
                return textObject;
            }
            return null;
        }

        public static TextObject SetTextVariable2(this TextObject textObject, string? tag, TextObject? variable)
        {
            if (tag is null || variable is null) return textObject;
            if (SetTextVariableTextObject is not null)
                SetTextVariableTextObject(textObject, tag, variable);
            if (SetTextVariable2TextObject is not null)
                SetTextVariable2TextObject(textObject, tag, variable);
            return textObject;
        }
        public static TextObject SetTextVariable2(this TextObject textObject, string? tag, string? variable)
        {
            if (tag is null || variable is null) return textObject;
            if (SetTextVariableTextObjectString is not null)
                SetTextVariableTextObjectString(textObject, tag, variable);
            if (SetTextVariable2TextObjectString is not null)
                SetTextVariable2TextObjectString(textObject, tag, variable);
            return textObject;
        }
        public static TextObject SetTextVariable2(this TextObject textObject, string? tag, int variable)
        {
            if (tag is null) return textObject;
            if (SetTextVariableTextObjectInt32 is not null)
                SetTextVariableTextObjectInt32(textObject, tag, variable);
            if (SetTextVariable2TextObjectInt32 is not null)
                SetTextVariable2TextObjectInt32(textObject, tag, variable);
            return textObject;
        }
        public static TextObject SetTextVariable2(this TextObject textObject, string? tag, float variable)
        {
            if (tag is null) return textObject;
            if (SetTextVariableTextObjectSingle is not null)
                SetTextVariableTextObjectSingle(textObject, tag, variable);
            if (SetTextVariable2TextObjectSingle is not null)
                SetTextVariable2TextObjectSingle(textObject, tag, variable);
            return textObject;
        }
    }
}

#pragma warning restore
#nullable restore
#endif // BANNERLORDBUTRSHARED_DISABLE
