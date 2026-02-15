using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000057 RID: 87
	[NativeHeader("Modules/TextCoreTextEngine/Native/TextSelectionService.h")]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements.PlayModeTests" })]
	internal class TextSelectionService
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0002C5D4 File Offset: 0x0002A7D4
		[NativeMethod(Name = "TextSelectionService::Substring")]
		internal static string Substring(IntPtr textGenerationInfo, int startIndex, int endIndex)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				TextSelectionService.Substring_Injected(textGenerationInfo, startIndex, endIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000234 RID: 564
		[NativeMethod(Name = "TextSelectionService::SelectCurrentWord")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectCurrentWord(IntPtr textGenerationInfo, int currentIndex, ref int startIndex, ref int endIndex);

		// Token: 0x06000235 RID: 565
		[NativeMethod(Name = "TextSelectionService::PreviousCodePointIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int PreviousCodePointIndex(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x06000236 RID: 566
		[NativeMethod(Name = "TextSelectionService::NextCodePointIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int NextCodePointIndex(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x06000237 RID: 567 RVA: 0x0002C608 File Offset: 0x0002A808
		[NativeMethod(Name = "TextSelectionService::GetCursorLogicalIndexFromPosition")]
		internal static int GetCursorLogicalIndexFromPosition(IntPtr textGenerationInfo, Vector2 position)
		{
			return TextSelectionService.GetCursorLogicalIndexFromPosition_Injected(textGenerationInfo, ref position);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0002C620 File Offset: 0x0002A820
		[NativeMethod(Name = "TextSelectionService::GetCursorPositionFromLogicalIndex")]
		internal static Vector2 GetCursorPositionFromLogicalIndex(IntPtr textGenerationInfo, int logicalIndex)
		{
			Vector2 vector;
			TextSelectionService.GetCursorPositionFromLogicalIndex_Injected(textGenerationInfo, logicalIndex, out vector);
			return vector;
		}

		// Token: 0x06000239 RID: 569
		[NativeMethod(Name = "TextSelectionService::LineUpCharacterPosition")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LineUpCharacterPosition(IntPtr textGenerationInfo, int originalPos);

		// Token: 0x0600023A RID: 570
		[NativeMethod(Name = "TextSelectionService::LineDownCharacterPosition")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LineDownCharacterPosition(IntPtr textGenerationInfo, int originalPos);

		// Token: 0x0600023B RID: 571 RVA: 0x0002C638 File Offset: 0x0002A838
		[NativeMethod(Name = "TextSelectionService::GetHighlightRectangles")]
		internal static Rect[] GetHighlightRectangles(IntPtr textGenerationInfo, int cursorIndex, int selectIndex)
		{
			Rect[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				TextSelectionService.GetHighlightRectangles_Injected(textGenerationInfo, cursorIndex, selectIndex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Rect[] array;
				blittableArrayWrapper.Unmarshal<Rect>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600023C RID: 572
		[NativeMethod(Name = "TextSelectionService::GetCharacterHeightFromIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetCharacterHeightFromIndex(IntPtr textGenerationInfo, int index);

		// Token: 0x0600023D RID: 573
		[NativeMethod(Name = "TextSelectionService::GetStartOfNextWord")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetStartOfNextWord(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x0600023E RID: 574
		[NativeMethod(Name = "TextSelectionService::GetEndOfPreviousWord")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetEndOfPreviousWord(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x0600023F RID: 575
		[NativeMethod(Name = "TextSelectionService::GetFirstCharacterIndexOnLine")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetFirstCharacterIndexOnLine(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x06000240 RID: 576
		[NativeMethod(Name = "TextSelectionService::GetLastCharacterIndexOnLine")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLastCharacterIndexOnLine(IntPtr textGenerationInfo, int currentIndex);

		// Token: 0x06000241 RID: 577
		[NativeMethod(Name = "TextSelectionService::GetLineHeight")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetLineHeight(IntPtr textGenerationInfo, int lineIndex);

		// Token: 0x06000242 RID: 578
		[NativeMethod(Name = "TextSelectionService::GetLineNumberFromLogicalIndex")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLineNumber(IntPtr textGenerationInfo, int logicalIndex);

		// Token: 0x06000243 RID: 579
		[NativeMethod(Name = "TextSelectionService::SelectToPreviousParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectToPreviousParagraph(IntPtr textGenerationInfo, ref int cursorIndex);

		// Token: 0x06000244 RID: 580
		[NativeMethod(Name = "TextSelectionService::SelectToStartOfParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectToStartOfParagraph(IntPtr textGenerationInfo, ref int cursorIndex);

		// Token: 0x06000245 RID: 581
		[NativeMethod(Name = "TextSelectionService::SelectToEndOfParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectToEndOfParagraph(IntPtr textGenerationInfo, ref int cursorIndex);

		// Token: 0x06000246 RID: 582
		[NativeMethod(Name = "TextSelectionService::SelectToNextParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectToNextParagraph(IntPtr textGenerationInfo, ref int cursorIndex);

		// Token: 0x06000247 RID: 583
		[NativeMethod(Name = "TextSelectionService::SelectCurrentParagraph")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SelectCurrentParagraph(IntPtr textGenerationInfo, ref int cursorIndex, ref int selectIndex);

		// Token: 0x06000248 RID: 584
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Substring_Injected(IntPtr textGenerationInfo, int startIndex, int endIndex, out ManagedSpanWrapper ret);

		// Token: 0x06000249 RID: 585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCursorLogicalIndexFromPosition_Injected(IntPtr textGenerationInfo, [In] ref Vector2 position);

		// Token: 0x0600024A RID: 586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCursorPositionFromLogicalIndex_Injected(IntPtr textGenerationInfo, int logicalIndex, out Vector2 ret);

		// Token: 0x0600024B RID: 587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHighlightRectangles_Injected(IntPtr textGenerationInfo, int cursorIndex, int selectIndex, out BlittableArrayWrapper ret);
	}
}
