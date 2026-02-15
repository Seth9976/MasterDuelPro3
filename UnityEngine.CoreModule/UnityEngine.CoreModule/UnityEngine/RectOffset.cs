using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000CA RID: 202
	[UsedByNativeCode]
	[NativeHeader("Modules/IMGUI/GUIStyle.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class RectOffset : IFormattable
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0000B901 File Offset: 0x00009B01
		public RectOffset()
		{
			this.m_Ptr = RectOffset.InternalCreate();
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000B916 File Offset: 0x00009B16
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		internal RectOffset(object sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000B930 File Offset: 0x00009B30
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Destroy();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000B970 File Offset: 0x00009B70
		public RectOffset(int left, int right, int top, int bottom)
		{
			this.m_Ptr = RectOffset.InternalCreate();
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000B9A8 File Offset: 0x00009BA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000B9C4 File Offset: 0x00009BC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", new object[]
			{
				this.left.ToString(format, formatProvider),
				this.right.ToString(format, formatProvider),
				this.top.ToString(format, formatProvider),
				this.bottom.ToString(format, formatProvider)
			});
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000BA48 File Offset: 0x00009C48
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				RectOffset.InternalDestroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000531 RID: 1329
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalCreate();

		// Token: 0x06000532 RID: 1330
		[ThreadAndSerializationSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000BA84 File Offset: 0x00009C84
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000BAA8 File Offset: 0x00009CA8
		[NativeProperty("left", false, TargetType.Field)]
		public int left
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_left_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectOffset.set_left_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000BACC File Offset: 0x00009CCC
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		[NativeProperty("right", false, TargetType.Field)]
		public int right
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_right_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectOffset.set_right_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000BB14 File Offset: 0x00009D14
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000BB38 File Offset: 0x00009D38
		[NativeProperty("top", false, TargetType.Field)]
		public int top
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_top_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectOffset.set_top_Injected(intPtr, value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000BB5C File Offset: 0x00009D5C
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000BB80 File Offset: 0x00009D80
		[NativeProperty("bottom", false, TargetType.Field)]
		public int bottom
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_bottom_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				RectOffset.set_bottom_Injected(intPtr, value);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public int horizontal
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_horizontal_Injected(intPtr);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		public int vertical
		{
			get
			{
				IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RectOffset.get_vertical_Injected(intPtr);
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public Rect Remove(Rect rect)
		{
			IntPtr intPtr = RectOffset.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rect rect2;
			RectOffset.Remove_Injected(intPtr, ref rect, out rect2);
			return rect2;
		}

		// Token: 0x0600053E RID: 1342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_left_Injected(IntPtr _unity_self);

		// Token: 0x0600053F RID: 1343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_left_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000540 RID: 1344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_right_Injected(IntPtr _unity_self);

		// Token: 0x06000541 RID: 1345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_right_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000542 RID: 1346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_top_Injected(IntPtr _unity_self);

		// Token: 0x06000543 RID: 1347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_top_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000544 RID: 1348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_bottom_Injected(IntPtr _unity_self);

		// Token: 0x06000545 RID: 1349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bottom_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000546 RID: 1350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_horizontal_Injected(IntPtr _unity_self);

		// Token: 0x06000547 RID: 1351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_vertical_Injected(IntPtr _unity_self);

		// Token: 0x06000548 RID: 1352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Remove_Injected(IntPtr _unity_self, [In] ref Rect rect, out Rect ret);

		// Token: 0x04000274 RID: 628
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000275 RID: 629
		private readonly object m_SourceStyle;

		// Token: 0x020000CB RID: 203
		internal static class BindingsMarshaller
		{
			// Token: 0x06000549 RID: 1353 RVA: 0x0000BC13 File Offset: 0x00009E13
			public static IntPtr ConvertToNative(RectOffset rectOffset)
			{
				return rectOffset.m_Ptr;
			}
		}
	}
}
