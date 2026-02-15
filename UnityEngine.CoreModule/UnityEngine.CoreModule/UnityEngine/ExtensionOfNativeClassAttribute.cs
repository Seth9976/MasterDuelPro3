using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A2 RID: 418
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true)]
	[RequiredByNativeCode]
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal sealed class ExtensionOfNativeClassAttribute : Attribute
	{
	}
}
