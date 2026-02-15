using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[RequireComponent(typeof(Transform), typeof(MeshRenderer))]
	[NativeClass("TextRenderingPrivate::TextMesh")]
	[NativeHeader("Modules/TextRendering/Public/TextMesh.h")]
	public sealed class TextMesh : Component
	{
		// Token: 0x17000008 RID: 8
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000028D8 File Offset: 0x00000AD8
		public unsafe string text
		{
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextMesh>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					TextMesh.set_text_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_text_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);
	}
}
