using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/ImageConversion/ScriptBindings/ImageConversion.bindings.h")]
	public static class ImageConversion
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[NativeMethod(Name = "ImageConversionBindings::EncodeToPNG", IsFreeFunction = true, ThrowsException = true)]
		public static byte[] EncodeToPNG(this Texture2D tex)
		{
			byte[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeToPNG_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(tex), out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000208C File Offset: 0x0000028C
		[NativeMethod(Name = "ImageConversionBindings::EncodeToJPG", IsFreeFunction = true, ThrowsException = true)]
		public static byte[] EncodeToJPG(this Texture2D tex, int quality)
		{
			byte[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ImageConversion.EncodeToJPG_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(tex), quality, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				byte[] array;
				blittableArrayWrapper.Unmarshal<byte>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020C8 File Offset: 0x000002C8
		[NativeMethod(Name = "ImageConversionBindings::LoadImage", IsFreeFunction = true)]
		public unsafe static bool LoadImage([NotNull] this Texture2D tex, byte[] data, bool markNonReadable)
		{
			if (tex == null)
			{
				ThrowHelper.ThrowArgumentNullException(tex, "tex");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Texture2D>(tex);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(tex, "tex");
			}
			Span<byte> span = new Span<byte>(data);
			bool flag;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = ImageConversion.LoadImage_Injected(intPtr, ref managedSpanWrapper, markNonReadable);
			}
			return flag;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002128 File Offset: 0x00000328
		public static bool LoadImage(this Texture2D tex, byte[] data)
		{
			return tex.LoadImage(data, false);
		}

		// Token: 0x06000005 RID: 5
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeToPNG_Injected(IntPtr tex, out BlittableArrayWrapper ret);

		// Token: 0x06000006 RID: 6
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EncodeToJPG_Injected(IntPtr tex, int quality, out BlittableArrayWrapper ret);

		// Token: 0x06000007 RID: 7
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadImage_Injected(IntPtr tex, ref ManagedSpanWrapper data, bool markNonReadable);
	}
}
