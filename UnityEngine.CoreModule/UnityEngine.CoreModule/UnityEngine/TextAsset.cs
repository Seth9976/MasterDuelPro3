using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001BA RID: 442
	[NativeHeader("Runtime/Scripting/TextAsset.h")]
	public class TextAsset : Object
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00024A50 File Offset: 0x00022C50
		public byte[] bytes
		{
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextAsset>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TextAsset.get_bytes_Injected(intPtr);
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00024A74 File Offset: 0x00022C74
		[return: Unmarshalled]
		private byte[] GetPreviewBytes(int maxByteCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TextAsset.GetPreviewBytes_Injected(intPtr, maxByteCount);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00024A98 File Offset: 0x00022C98
		private unsafe static void Internal_CreateInstance([Writable] TextAsset self, string text)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(text, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = text.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				TextAsset.Internal_CreateInstance_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00024AF0 File Offset: 0x00022CF0
		private unsafe static void Internal_CreateInstanceFromBytes([Writable] TextAsset self, ReadOnlySpan<byte> bytes)
		{
			ReadOnlySpan<byte> readOnlySpan = bytes;
			fixed (byte* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				TextAsset.Internal_CreateInstanceFromBytes_Injected(self, ref managedSpanWrapper);
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00024B24 File Offset: 0x00022D24
		private IntPtr GetDataPtr()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TextAsset.GetDataPtr_Injected(intPtr);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00024B48 File Offset: 0x00022D48
		private long GetDataSize()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TextAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TextAsset.GetDataSize_Injected(intPtr);
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00024B6C File Offset: 0x00022D6C
		public string text
		{
			get
			{
				byte[] localBytes = this.bytes;
				return (localBytes.Length == 0) ? string.Empty : TextAsset.DecodeString(localBytes);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00024B96 File Offset: 0x00022D96
		public long dataSize
		{
			get
			{
				return this.GetDataSize();
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00024BA0 File Offset: 0x00022DA0
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00024BB8 File Offset: 0x00022DB8
		public TextAsset()
			: this(TextAsset.CreateOptions.CreateNativeObject, null)
		{
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00024BC4 File Offset: 0x00022DC4
		public TextAsset(string text)
			: this(TextAsset.CreateOptions.CreateNativeObject, text)
		{
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00024BD0 File Offset: 0x00022DD0
		public TextAsset(ReadOnlySpan<byte> bytes)
			: this(TextAsset.CreateOptions.CreateNativeObject, bytes)
		{
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00024BDC File Offset: 0x00022DDC
		internal TextAsset(TextAsset.CreateOptions options, string text)
		{
			bool flag = options == TextAsset.CreateOptions.CreateNativeObject;
			if (flag)
			{
				TextAsset.Internal_CreateInstance(this, text);
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00024C04 File Offset: 0x00022E04
		internal TextAsset(TextAsset.CreateOptions options, ReadOnlySpan<byte> bytes)
		{
			bool flag = options == TextAsset.CreateOptions.CreateNativeObject;
			if (flag)
			{
				TextAsset.Internal_CreateInstanceFromBytes(this, bytes);
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00024C2C File Offset: 0x00022E2C
		public unsafe NativeArray<T> GetData<T>() where T : struct
		{
			long size = this.GetDataSize();
			long stride = (long)UnsafeUtility.SizeOf<T>();
			bool flag = size % stride != 0L;
			if (flag)
			{
				throw new ArgumentException(string.Format("Type passed to {0} can't capture the asset data. Data size is {1} which is not a multiple of type size {2}", "GetData", size, stride));
			}
			long arrSize = size / stride;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataPtr(), (int)arrSize, Allocator.None);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00024C98 File Offset: 0x00022E98
		internal string GetPreview(int maxChars)
		{
			return TextAsset.DecodeString(this.GetPreviewBytes(maxChars * 4));
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00024CB8 File Offset: 0x00022EB8
		internal static string DecodeString(byte[] bytes)
		{
			int encodingLookupLength = TextAsset.EncodingUtility.encodingLookup.Length;
			int i = 0;
			int preambleLength;
			while (i < encodingLookupLength)
			{
				byte[] preamble = TextAsset.EncodingUtility.encodingLookup[i].Key;
				preambleLength = preamble.Length;
				bool flag = bytes.Length >= preambleLength;
				if (flag)
				{
					for (int j = 0; j < preambleLength; j++)
					{
						bool flag2 = preamble[j] != bytes[j];
						if (flag2)
						{
							preambleLength = -1;
						}
					}
					bool flag3 = preambleLength < 0;
					if (!flag3)
					{
						try
						{
							Encoding tempEncoding = TextAsset.EncodingUtility.encodingLookup[i].Value;
							return tempEncoding.GetString(bytes, preambleLength, bytes.Length - preambleLength);
						}
						catch
						{
						}
					}
				}
				IL_00A2:
				i++;
				continue;
				goto IL_00A2;
			}
			preambleLength = 0;
			Encoding encoding = TextAsset.EncodingUtility.targetEncoding;
			return encoding.GetString(bytes, preambleLength, bytes.Length - preambleLength);
		}

		// Token: 0x06001133 RID: 4403
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] get_bytes_Injected(IntPtr _unity_self);

		// Token: 0x06001134 RID: 4404
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] GetPreviewBytes_Injected(IntPtr _unity_self, int maxByteCount);

		// Token: 0x06001135 RID: 4405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstance_Injected([Writable] TextAsset self, ref ManagedSpanWrapper text);

		// Token: 0x06001136 RID: 4406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstanceFromBytes_Injected([Writable] TextAsset self, ref ManagedSpanWrapper bytes);

		// Token: 0x06001137 RID: 4407
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDataPtr_Injected(IntPtr _unity_self);

		// Token: 0x06001138 RID: 4408
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetDataSize_Injected(IntPtr _unity_self);

		// Token: 0x020001BB RID: 443
		internal enum CreateOptions
		{
			// Token: 0x04000686 RID: 1670
			None,
			// Token: 0x04000687 RID: 1671
			CreateNativeObject
		}

		// Token: 0x020001BC RID: 444
		private static class EncodingUtility
		{
			// Token: 0x06001139 RID: 4409 RVA: 0x00024DA4 File Offset: 0x00022FA4
			static EncodingUtility()
			{
				Encoding utf32BE = new UTF32Encoding(true, true, true);
				Encoding utf32LE = new UTF32Encoding(false, true, true);
				Encoding utf16BE = new UnicodeEncoding(true, true, true);
				Encoding utf16LE = new UnicodeEncoding(false, true, true);
				Encoding utf8BOM = new UTF8Encoding(true, true);
				TextAsset.EncodingUtility.encodingLookup = new KeyValuePair<byte[], Encoding>[]
				{
					new KeyValuePair<byte[], Encoding>(utf32BE.GetPreamble(), utf32BE),
					new KeyValuePair<byte[], Encoding>(utf32LE.GetPreamble(), utf32LE),
					new KeyValuePair<byte[], Encoding>(utf16BE.GetPreamble(), utf16BE),
					new KeyValuePair<byte[], Encoding>(utf16LE.GetPreamble(), utf16LE),
					new KeyValuePair<byte[], Encoding>(utf8BOM.GetPreamble(), utf8BOM)
				};
			}

			// Token: 0x04000688 RID: 1672
			internal static readonly KeyValuePair<byte[], Encoding>[] encodingLookup;

			// Token: 0x04000689 RID: 1673
			internal static readonly Encoding targetEncoding = Encoding.GetEncoding(Encoding.UTF8.CodePage, new EncoderReplacementFallback("\ufffd"), new DecoderReplacementFallback("\ufffd"));
		}
	}
}
