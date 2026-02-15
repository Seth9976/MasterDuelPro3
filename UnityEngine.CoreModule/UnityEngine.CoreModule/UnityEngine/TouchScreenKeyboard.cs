using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001DE RID: 478
	[NativeConditional("ENABLE_ONSCREEN_KEYBOARD")]
	[NativeHeader("Runtime/Input/KeyboardOnScreen.h")]
	[NativeHeader("Runtime/Export/TouchScreenKeyboard/TouchScreenKeyboard.bindings.h")]
	public class TouchScreenKeyboard
	{
		// Token: 0x06001263 RID: 4707
		[FreeFunction("TouchScreenKeyboard_Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06001264 RID: 4708 RVA: 0x00026DB0 File Offset: 0x00024FB0
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				TouchScreenKeyboard.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00026DF4 File Offset: 0x00024FF4
		~TouchScreenKeyboard()
		{
			this.Destroy();
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00026E24 File Offset: 0x00025024
		public TouchScreenKeyboard(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int characterLimit)
		{
			TouchScreenKeyboard_InternalConstructorHelperArguments arguments = default(TouchScreenKeyboard_InternalConstructorHelperArguments);
			arguments.keyboardType = Convert.ToUInt32(keyboardType);
			arguments.autocorrection = Convert.ToUInt32(autocorrection);
			arguments.multiline = Convert.ToUInt32(multiline);
			arguments.secure = Convert.ToUInt32(secure);
			arguments.alert = Convert.ToUInt32(alert);
			arguments.characterLimit = characterLimit;
			this.m_Ptr = TouchScreenKeyboard.TouchScreenKeyboard_InternalConstructorHelper(ref arguments, text, textPlaceholder);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00026EA4 File Offset: 0x000250A4
		[FreeFunction("TouchScreenKeyboard_InternalConstructorHelper")]
		private unsafe static IntPtr TouchScreenKeyboard_InternalConstructorHelper(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, string text, string textPlaceholder)
		{
			IntPtr intPtr;
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
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(textPlaceholder, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = textPlaceholder.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				intPtr = TouchScreenKeyboard.TouchScreenKeyboard_InternalConstructorHelper_Injected(ref arguments, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return intPtr;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x00026F30 File Offset: 0x00025130
		public static bool isSupported
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				RuntimePlatform runtimePlatform = platform;
				RuntimePlatform runtimePlatform2 = runtimePlatform;
				if (runtimePlatform2 <= RuntimePlatform.MetroPlayerARM)
				{
					if (runtimePlatform2 != RuntimePlatform.IPhonePlayer && runtimePlatform2 != RuntimePlatform.Android && runtimePlatform2 - RuntimePlatform.WebGLPlayer > 3)
					{
						goto IL_004F;
					}
				}
				else if (runtimePlatform2 <= RuntimePlatform.Switch)
				{
					if (runtimePlatform2 != RuntimePlatform.PS4 && runtimePlatform2 - RuntimePlatform.tvOS > 1)
					{
						goto IL_004F;
					}
				}
				else if (runtimePlatform2 - RuntimePlatform.GameCoreXboxSeries > 2 && runtimePlatform2 != RuntimePlatform.VisionOS)
				{
					goto IL_004F;
				}
				return true;
				IL_004F:
				return false;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00026F91 File Offset: 0x00025191
		internal static bool disableInPlaceEditing { get; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x00026F98 File Offset: 0x00025198
		public static bool isInPlaceEditingAllowed
		{
			get
			{
				bool disableInPlaceEditing = TouchScreenKeyboard.disableInPlaceEditing;
				return !disableInPlaceEditing && TouchScreenKeyboard.IsInPlaceEditingAllowed();
			}
		}

		// Token: 0x0600126B RID: 4715
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInPlaceEditingAllowed();

		// Token: 0x0600126C RID: 4716 RVA: 0x00026FBC File Offset: 0x000251BC
		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder, [DefaultValue("0")] int characterLimit)
		{
			return new TouchScreenKeyboard(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00026FE0 File Offset: 0x000251E0
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			int characterLimit = 0;
			string textPlaceholder = "";
			bool alert = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x0002700C File Offset: 0x0002520C
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x0002704C File Offset: 0x0002524C
		public unsafe string text
		{
			[NativeName("GetText")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					TouchScreenKeyboard.get_text_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[NativeName("SetText")]
			set
			{
				try
				{
					IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
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
					TouchScreenKeyboard.set_text_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x170002E8 RID: 744
		// (set) Token: 0x06001270 RID: 4720
		public static extern bool hideInput
		{
			[NativeName("SetInputHidden")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001271 RID: 4721
		public static extern TouchScreenKeyboard.InputFieldAppearance inputFieldAppearance
		{
			[NativeName("GetInputFieldAppearance")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000270B0 File Offset: 0x000252B0
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x000270D4 File Offset: 0x000252D4
		public bool active
		{
			[NativeName("IsActive")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_active_Injected(intPtr);
			}
			[NativeName("SetActive")]
			set
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TouchScreenKeyboard.set_active_Injected(intPtr, value);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000270F8 File Offset: 0x000252F8
		public TouchScreenKeyboard.Status status
		{
			[NativeName("GetKeyboardStatus")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_status_Injected(intPtr);
			}
		}

		// Token: 0x170002EC RID: 748
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x0002711C File Offset: 0x0002531C
		public int characterLimit
		{
			[NativeName("SetCharacterLimit")]
			set
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TouchScreenKeyboard.set_characterLimit_Injected(intPtr, value);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00027140 File Offset: 0x00025340
		public bool canGetSelection
		{
			[NativeName("CanGetSelection")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_canGetSelection_Injected(intPtr);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00027164 File Offset: 0x00025364
		public bool canSetSelection
		{
			[NativeName("CanSetSelection")]
			get
			{
				IntPtr intPtr = TouchScreenKeyboard.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TouchScreenKeyboard.get_canSetSelection_Injected(intPtr);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00027188 File Offset: 0x00025388
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x000271B0 File Offset: 0x000253B0
		public RangeInt selection
		{
			get
			{
				RangeInt range;
				TouchScreenKeyboard.GetSelection(out range.start, out range.length);
				return range;
			}
			set
			{
				bool flag = value.start < 0 || value.length < 0 || value.start + value.length > this.text.Length;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("selection", "Selection is out of range.");
				}
				TouchScreenKeyboard.SetSelection(value.start, value.length);
			}
		}

		// Token: 0x0600127A RID: 4730
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSelection(out int start, out int length);

		// Token: 0x0600127B RID: 4731
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSelection(int start, int length);

		// Token: 0x0600127C RID: 4732
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr TouchScreenKeyboard_InternalConstructorHelper_Injected(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, ref ManagedSpanWrapper text, ref ManagedSpanWrapper textPlaceholder);

		// Token: 0x0600127D RID: 4733
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_text_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600127E RID: 4734
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_text_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x0600127F RID: 4735
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_active_Injected(IntPtr _unity_self);

		// Token: 0x06001280 RID: 4736
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_active_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001281 RID: 4737
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TouchScreenKeyboard.Status get_status_Injected(IntPtr _unity_self);

		// Token: 0x06001282 RID: 4738
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_characterLimit_Injected(IntPtr _unity_self, int value);

		// Token: 0x06001283 RID: 4739
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canGetSelection_Injected(IntPtr _unity_self);

		// Token: 0x06001284 RID: 4740
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetSelection_Injected(IntPtr _unity_self);

		// Token: 0x040006D0 RID: 1744
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x020001DF RID: 479
		public enum Status
		{
			// Token: 0x040006D3 RID: 1747
			Visible,
			// Token: 0x040006D4 RID: 1748
			Done,
			// Token: 0x040006D5 RID: 1749
			Canceled,
			// Token: 0x040006D6 RID: 1750
			LostFocus
		}

		// Token: 0x020001E0 RID: 480
		public enum InputFieldAppearance
		{
			// Token: 0x040006D8 RID: 1752
			Customizable,
			// Token: 0x040006D9 RID: 1753
			AlwaysVisible,
			// Token: 0x040006DA RID: 1754
			AlwaysHidden
		}

		// Token: 0x020001E1 RID: 481
		internal static class BindingsMarshaller
		{
			// Token: 0x06001285 RID: 4741 RVA: 0x00027213 File Offset: 0x00025413
			public static IntPtr ConvertToNative(TouchScreenKeyboard touchScreenKeyboard)
			{
				return touchScreenKeyboard.m_Ptr;
			}
		}
	}
}
