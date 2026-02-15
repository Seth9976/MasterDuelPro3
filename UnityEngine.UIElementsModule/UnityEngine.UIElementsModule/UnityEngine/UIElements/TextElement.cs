using System;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000450 RID: 1104
	public class TextElement : BindableElement, INotifyValueChanged<string>, ITextEdition, IExperimentalFeatures, ITextSelection
	{
		// Token: 0x06001FEE RID: 8174 RVA: 0x000764C8 File Offset: 0x000746C8
		public TextElement()
		{
			base.requireMeasureFunction = true;
			base.tabIndex = -1;
			this.uitkTextHandle = new UITKTextHandle(this);
			base.AddToClassList(TextElement.ussClassName);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			this.edition.GetDefaultValueType = new Func<string>(this.GetDefaultValueType);
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000765FC File Offset: 0x000747FC
		private string GetDefaultValueType()
		{
			return "";
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00076613 File Offset: 0x00074813
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x0007661B File Offset: 0x0007481B
		internal UITKTextHandle uitkTextHandle { get; set; }

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00076624 File Offset: 0x00074824
		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			this.UpdateVisibleText();
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0007662E File Offset: 0x0007482E
		// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x00076636 File Offset: 0x00074836
		[CreateProperty]
		public virtual string text
		{
			get
			{
				return ((INotifyValueChanged<string>)this).value;
			}
			set
			{
				((INotifyValueChanged<string>)this).value = value;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x00076640 File Offset: 0x00074840
		// (set) Token: 0x06001FF6 RID: 8182 RVA: 0x00076648 File Offset: 0x00074848
		[CreateProperty]
		public bool enableRichText
		{
			get
			{
				return this.m_EnableRichText;
			}
			set
			{
				bool flag = this.m_EnableRichText == value;
				if (!flag)
				{
					this.m_EnableRichText = value;
					base.MarkDirtyRepaint();
					base.NotifyPropertyChanged(in TextElement.enableRichTextProperty);
				}
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0007667F File Offset: 0x0007487F
		// (set) Token: 0x06001FF8 RID: 8184 RVA: 0x00076688 File Offset: 0x00074888
		[CreateProperty]
		public bool emojiFallbackSupport
		{
			get
			{
				return this.m_EmojiFallbackSupport;
			}
			set
			{
				bool flag = this.m_EmojiFallbackSupport == value;
				if (!flag)
				{
					this.m_EmojiFallbackSupport = value;
					base.MarkDirtyRepaint();
					base.NotifyPropertyChanged(in TextElement.emojiFallbackSupportProperty);
				}
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x000766BF File Offset: 0x000748BF
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x000766C8 File Offset: 0x000748C8
		[CreateProperty]
		public bool parseEscapeSequences
		{
			get
			{
				return this.m_ParseEscapeSequences;
			}
			set
			{
				bool flag = this.m_ParseEscapeSequences == value;
				if (!flag)
				{
					this.m_ParseEscapeSequences = value;
					base.MarkDirtyRepaint();
					base.NotifyPropertyChanged(in TextElement.parseEscapeSequencesProperty);
				}
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x000766FF File Offset: 0x000748FF
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x00076708 File Offset: 0x00074908
		[CreateProperty]
		public bool displayTooltipWhenElided
		{
			get
			{
				return this.m_DisplayTooltipWhenElided;
			}
			set
			{
				bool flag = this.m_DisplayTooltipWhenElided != value;
				if (flag)
				{
					this.m_DisplayTooltipWhenElided = value;
					this.UpdateVisibleText();
					base.MarkDirtyRepaint();
					base.NotifyPropertyChanged(in TextElement.displayTooltipWhenElidedProperty);
				}
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x00076749 File Offset: 0x00074949
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00076751 File Offset: 0x00074951
		[CreateProperty(ReadOnly = true)]
		public bool isElided { get; private set; }

		// Token: 0x06001FFF RID: 8191 RVA: 0x0007675C File Offset: 0x0007495C
		internal void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this.UpdateVisibleText();
			bool flag = TextUtilities.IsFontAssigned(this);
			if (flag)
			{
				bool flag2 = TextUtilities.IsAdvancedTextEnabledForElement(this);
				if (flag2)
				{
					bool isSuccess = false;
					NativeTextInfo textInfo = this.uitkTextHandle.UpdateNative(ref isSuccess);
					bool flag3 = isSuccess;
					if (flag3)
					{
						mgc.DrawNativeText(textInfo, base.contentRect.min);
						bool flag4 = this.selection.HasSelection() && this.selectingManipulator.HasFocus();
						if (flag4)
						{
							this.DrawNativeHighlighting(mgc);
						}
						else
						{
							bool flag5 = !this.edition.isReadOnly && this.selection.isSelectable && this.selectingManipulator.RevealCursor();
							if (flag5)
							{
								this.DrawCaret(mgc);
							}
						}
					}
				}
				else
				{
					mgc.meshGenerator.textJobSystem.GenerateText(mgc, this);
				}
			}
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x00076838 File Offset: 0x00074A38
		internal void OnGenerateTextOver(MeshGenerationContext mgc)
		{
			bool flag = this.selection.HasSelection() && this.selectingManipulator.HasFocus();
			if (flag)
			{
				this.DrawHighlighting(mgc);
			}
			else
			{
				bool flag2 = !this.edition.isReadOnly && this.selection.isSelectable && this.selectingManipulator.RevealCursor();
				if (flag2)
				{
					this.DrawCaret(mgc);
				}
			}
			bool flag3 = this.ShouldElide() && this.uitkTextHandle.TextLibraryCanElide();
			if (flag3)
			{
				this.isElided = this.uitkTextHandle.IsElided();
			}
			this.UpdateTooltip();
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000768D8 File Offset: 0x00074AD8
		internal string ElideText(string drawText, string ellipsisText, float width, TextOverflowPosition textOverflowPosition)
		{
			float paddingRight = base.resolvedStyle.paddingRight;
			bool flag = float.IsNaN(paddingRight);
			if (flag)
			{
				paddingRight = 0f;
			}
			float extraWidth = Mathf.Clamp(paddingRight, 1f / base.scaledPixelsPerPoint, 1f);
			Vector2 size = this.MeasureTextSize(drawText, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
			bool flag2 = size.x <= width + extraWidth || string.IsNullOrEmpty(ellipsisText);
			string text;
			if (flag2)
			{
				text = drawText;
			}
			else
			{
				string minText = ((drawText.Length > 1) ? ellipsisText : drawText);
				Vector2 minSize = this.MeasureTextSize(minText, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
				bool flag3 = minSize.x >= width;
				if (flag3)
				{
					text = minText;
				}
				else
				{
					int drawTextMax = drawText.Length - 1;
					int prevFitMid = -1;
					string truncatedText = drawText;
					int min = ((textOverflowPosition == TextOverflowPosition.Start) ? 1 : 0);
					int max = ((textOverflowPosition == TextOverflowPosition.Start || textOverflowPosition == TextOverflowPosition.Middle) ? drawTextMax : (drawTextMax - 1));
					int mid = (min + max) / 2;
					while (min <= max)
					{
						bool flag4 = textOverflowPosition == TextOverflowPosition.Start;
						if (flag4)
						{
							truncatedText = ellipsisText + drawText.Substring(mid, drawTextMax - (mid - 1));
						}
						else
						{
							bool flag5 = textOverflowPosition == TextOverflowPosition.End;
							if (flag5)
							{
								truncatedText = drawText.Substring(0, mid) + ellipsisText;
							}
							else
							{
								bool flag6 = textOverflowPosition == TextOverflowPosition.Middle;
								if (flag6)
								{
									truncatedText = ((mid - 1 <= 0) ? "" : drawText.Substring(0, mid - 1)) + ellipsisText + ((drawTextMax - (mid - 1) <= 0) ? "" : drawText.Substring(drawTextMax - (mid - 1)));
								}
							}
						}
						size = this.MeasureTextSize(truncatedText, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
						bool flag7 = Math.Abs(size.x - width) < 1E-30f;
						if (flag7)
						{
							return truncatedText;
						}
						bool flag8 = textOverflowPosition == TextOverflowPosition.Start;
						if (flag8)
						{
							bool flag9 = size.x > width;
							if (flag9)
							{
								bool flag10 = prevFitMid == mid - 1;
								if (flag10)
								{
									return ellipsisText + drawText.Substring(prevFitMid, drawTextMax - (prevFitMid - 1));
								}
								min = mid + 1;
							}
							else
							{
								max = mid - 1;
								prevFitMid = mid;
							}
						}
						else
						{
							bool flag11 = textOverflowPosition == TextOverflowPosition.End || textOverflowPosition == TextOverflowPosition.Middle;
							if (flag11)
							{
								bool flag12 = size.x > width;
								if (flag12)
								{
									bool flag13 = prevFitMid == mid - 1;
									if (flag13)
									{
										bool flag14 = textOverflowPosition == TextOverflowPosition.End;
										if (flag14)
										{
											return drawText.Substring(0, prevFitMid) + ellipsisText;
										}
										return drawText.Substring(0, Mathf.Max(prevFitMid - 1, 0)) + ellipsisText + drawText.Substring(drawTextMax - Mathf.Max(prevFitMid - 1, 0));
									}
									else
									{
										max = mid - 1;
									}
								}
								else
								{
									min = mid + 1;
									prevFitMid = mid;
								}
							}
						}
						mid = (min + max) / 2;
					}
					text = truncatedText;
				}
			}
			return text;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00076BB0 File Offset: 0x00074DB0
		private void UpdateTooltip()
		{
			bool needsTooltip = this.displayTooltipWhenElided && this.isElided;
			bool flag = needsTooltip;
			if (flag)
			{
				base.tooltip = this.text;
				this.m_WasElided = true;
			}
			else
			{
				bool wasElided = this.m_WasElided;
				if (wasElided)
				{
					base.tooltip = null;
					this.m_WasElided = false;
				}
			}
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00076C08 File Offset: 0x00074E08
		private void UpdateVisibleText()
		{
			bool shouldElide = this.ShouldElide();
			bool flag = shouldElide && this.uitkTextHandle.TextLibraryCanElide();
			if (!flag)
			{
				bool flag2 = shouldElide;
				if (flag2)
				{
					this.elidedText = this.ElideText(this.text, TextElement.k_EllipsisText, base.contentRect.width, base.computedStyle.unityTextOverflowPosition);
					this.isElided = shouldElide && !string.Equals(this.elidedText, this.text, StringComparison.Ordinal);
				}
				else
				{
					this.isElided = false;
				}
			}
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00076C9C File Offset: 0x00074E9C
		private bool ShouldElide()
		{
			return base.computedStyle.textOverflow == TextOverflow.Ellipsis && base.computedStyle.overflow == OverflowInternal.Hidden;
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x00076CCD File Offset: 0x00074ECD
		internal bool hasFocus
		{
			get
			{
				bool flag;
				if (base.elementPanel != null)
				{
					FocusController focusController = base.elementPanel.focusController;
					flag = ((focusController != null) ? focusController.GetLeafFocusedElement() : null) == this;
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00076CF4 File Offset: 0x00074EF4
		public Vector2 MeasureTextSize(string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			RenderedText renderedText = new RenderedText(textToMeasure);
			return TextUtilities.MeasureVisualElementTextSize(this, in renderedText, width, widthMode, height, heightMode);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00076D1C File Offset: 0x00074F1C
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			RenderedText renderedText = this.renderedText;
			return TextUtilities.MeasureVisualElementTextSize(this, in renderedText, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00076D42 File Offset: 0x00074F42
		// (set) Token: 0x06002009 RID: 8201 RVA: 0x00076D54 File Offset: 0x00074F54
		string INotifyValueChanged<string>.value
		{
			get
			{
				return this.m_Text ?? string.Empty;
			}
			set
			{
				bool flag = this.m_Text != value;
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<string> evt = ChangeEvent<string>.GetPooled(this.text, value))
						{
							evt.elementTarget = this;
							((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
							this.SendEvent(evt);
							base.NotifyPropertyChanged(in TextElement.valueProperty);
							base.NotifyPropertyChanged(in TextElement.textProperty);
						}
					}
					else
					{
						((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x0007662E File Offset: 0x0007482E
		// (set) Token: 0x0600200B RID: 8203 RVA: 0x00076636 File Offset: 0x00074836
		[CreateProperty]
		private string value
		{
			get
			{
				return ((INotifyValueChanged<string>)this).value;
			}
			set
			{
				((INotifyValueChanged<string>)this).value = value;
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00076DEC File Offset: 0x00074FEC
		void INotifyValueChanged<string>.SetValueWithoutNotify(string newValue)
		{
			newValue = ((ITextEdition)this).CullString(newValue);
			bool flag = this.m_Text != newValue;
			if (flag)
			{
				this.SetRenderedText(newValue);
				this.m_Text = newValue;
				bool flag2 = base.computedStyle.height.IsAuto() || base.computedStyle.height.IsNone() || base.computedStyle.width.IsAuto() || base.computedStyle.width.IsNone();
				if (flag2)
				{
					base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				}
				else
				{
					base.IncrementVersion(VersionChangeType.Repaint);
				}
				bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
				if (flag3)
				{
					base.SaveViewData();
				}
			}
			bool flag4 = this.editingManipulator != null;
			if (flag4)
			{
				this.editingManipulator.editingUtilities.text = newValue;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x00038529 File Offset: 0x00036729
		internal ITextEdition edition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x00076ED7 File Offset: 0x000750D7
		// (set) Token: 0x0600200F RID: 8207 RVA: 0x00076EDF File Offset: 0x000750DF
		internal TextEditingManipulator editingManipulator { get; private set; }

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x00076EE8 File Offset: 0x000750E8
		// (set) Token: 0x06002011 RID: 8209 RVA: 0x00076EF0 File Offset: 0x000750F0
		bool ITextEdition.multiline
		{
			get
			{
				return this.m_Multiline;
			}
			set
			{
				bool flag = value != this.m_Multiline;
				if (flag)
				{
					bool flag2 = !this.edition.isReadOnly;
					if (flag2)
					{
						this.editingManipulator.editingUtilities.multiline = value;
					}
					this.m_Multiline = value;
				}
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x00076F3A File Offset: 0x0007513A
		// (set) Token: 0x06002013 RID: 8211 RVA: 0x00076F44 File Offset: 0x00075144
		TouchScreenKeyboardType ITextEdition.keyboardType
		{
			get
			{
				return this.m_KeyboardType;
			}
			set
			{
				bool flag = this.m_KeyboardType == value;
				if (!flag)
				{
					this.m_KeyboardType = value;
					base.NotifyPropertyChanged(in TextElement.keyboardTypeProperty);
				}
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x00076F74 File Offset: 0x00075174
		// (set) Token: 0x06002015 RID: 8213 RVA: 0x00076F81 File Offset: 0x00075181
		[CreateProperty]
		private TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.edition.keyboardType;
			}
			set
			{
				this.edition.keyboardType = value;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x00076F90 File Offset: 0x00075190
		// (set) Token: 0x06002017 RID: 8215 RVA: 0x00076FD0 File Offset: 0x000751D0
		bool ITextEdition.hideMobileInput
		{
			get
			{
				TouchScreenKeyboard.InputFieldAppearance inputFieldAppearance = TouchScreenKeyboard.inputFieldAppearance;
				if (!true)
				{
				}
				bool flag = inputFieldAppearance != TouchScreenKeyboard.InputFieldAppearance.AlwaysVisible && (inputFieldAppearance == TouchScreenKeyboard.InputFieldAppearance.AlwaysHidden || this.m_HideMobileInput);
				if (!true)
				{
				}
				return flag;
			}
			set
			{
				bool flag = TouchScreenKeyboard.inputFieldAppearance > TouchScreenKeyboard.InputFieldAppearance.Customizable;
				if (!flag)
				{
					bool flag2 = this.m_HideMobileInput == value;
					if (!flag2)
					{
						this.m_HideMobileInput = value;
						base.NotifyPropertyChanged(in TextElement.hideMobileInputProperty);
					}
				}
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0007700E File Offset: 0x0007520E
		// (set) Token: 0x06002019 RID: 8217 RVA: 0x0007701B File Offset: 0x0007521B
		[CreateProperty]
		private bool hideMobileInput
		{
			get
			{
				return this.edition.hideMobileInput;
			}
			set
			{
				this.edition.hideMobileInput = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0007702A File Offset: 0x0007522A
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x00077040 File Offset: 0x00075240
		bool ITextEdition.isReadOnly
		{
			get
			{
				return this.m_IsReadOnly || !base.enabledInHierarchy;
			}
			set
			{
				bool flag = value == this.m_IsReadOnly;
				if (!flag)
				{
					TextEditingManipulator editingManipulator = this.editingManipulator;
					if (editingManipulator != null)
					{
						editingManipulator.Reset();
					}
					this.editingManipulator = (value ? null : new TextEditingManipulator(this));
					this.m_IsReadOnly = value;
					Action<bool> action = this.onIsReadOnlyChanged;
					if (action != null)
					{
						action(value);
					}
					base.NotifyPropertyChanged(in TextElement.isReadOnlyProperty);
				}
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x000770A8 File Offset: 0x000752A8
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x000770B5 File Offset: 0x000752B5
		[CreateProperty]
		private bool isReadOnly
		{
			get
			{
				return this.edition.isReadOnly;
			}
			set
			{
				this.edition.isReadOnly = value;
			}
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x000770C4 File Offset: 0x000752C4
		private void ProcessMenuCommand(string command)
		{
			this.Focus();
			using (ExecuteCommandEvent evt = CommandEventBase<ExecuteCommandEvent>.GetPooled(command))
			{
				evt.elementTarget = this;
				this.SendEvent(evt);
			}
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00077110 File Offset: 0x00075310
		private void Cut(DropdownMenuAction a)
		{
			this.ProcessMenuCommand("Cut");
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0007711F File Offset: 0x0007531F
		private void Copy(DropdownMenuAction a)
		{
			this.ProcessMenuCommand("Copy");
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0007712E File Offset: 0x0007532E
		private void Paste(DropdownMenuAction a)
		{
			this.ProcessMenuCommand("Paste");
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x00077140 File Offset: 0x00075340
		private void BuildContextualMenu(ContextualMenuPopulateEvent evt)
		{
			bool flag = ((evt != null) ? evt.target : null) is TextElement;
			if (flag)
			{
				bool flag2 = !this.edition.isReadOnly;
				if (flag2)
				{
					evt.menu.AppendAction("Cut", new Action<DropdownMenuAction>(this.Cut), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CutActionStatus), null);
					evt.menu.AppendAction("Copy", new Action<DropdownMenuAction>(this.Copy), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CopyActionStatus), null);
					evt.menu.AppendAction("Paste", new Action<DropdownMenuAction>(this.Paste), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.PasteActionStatus), null);
				}
				else
				{
					evt.menu.AppendAction("Copy", new Action<DropdownMenuAction>(this.Copy), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CopyActionStatus), null);
				}
			}
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0007722C File Offset: 0x0007542C
		private DropdownMenuAction.Status CutActionStatus(DropdownMenuAction a)
		{
			return (base.enabledInHierarchy && this.selection.HasSelection() && !this.edition.isPassword) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00077264 File Offset: 0x00075464
		private DropdownMenuAction.Status CopyActionStatus(DropdownMenuAction a)
		{
			return ((!base.enabledInHierarchy || this.selection.HasSelection()) && !this.edition.isPassword) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0007729C File Offset: 0x0007549C
		private DropdownMenuAction.Status PasteActionStatus(DropdownMenuAction a)
		{
			bool canPaste = this.editingManipulator.editingUtilities.CanPaste();
			return base.enabledInHierarchy ? (canPaste ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled) : DropdownMenuAction.Status.Hidden;
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000772D4 File Offset: 0x000754D4
		[EventInterest(new Type[]
		{
			typeof(ContextualMenuPopulateEvent),
			typeof(KeyDownEvent),
			typeof(KeyUpEvent),
			typeof(ValidateCommandEvent),
			typeof(ExecuteCommandEvent),
			typeof(FocusEvent),
			typeof(BlurEvent),
			typeof(FocusInEvent),
			typeof(FocusOutEvent),
			typeof(PointerDownEvent),
			typeof(PointerUpEvent),
			typeof(PointerMoveEvent),
			typeof(NavigationMoveEvent),
			typeof(NavigationSubmitEvent),
			typeof(NavigationCancelEvent)
		})]
		protected override void HandleEventBubbleUp(EventBase evt)
		{
			base.HandleEventBubbleUp(evt);
			bool isSelectable = this.selection.isSelectable;
			if (isSelectable)
			{
				TextEditingManipulator editingManipulator = this.editingManipulator;
				bool useTouchScreenKeyboard = editingManipulator != null && editingManipulator.editingUtilities.TouchScreenKeyboardShouldBeUsed();
				bool flag = !useTouchScreenKeyboard || this.edition.hideMobileInput;
				if (flag)
				{
					TextSelectingManipulator selectingManipulator = this.selectingManipulator;
					if (selectingManipulator != null)
					{
						selectingManipulator.HandleEventBubbleUp(evt);
					}
				}
				bool flag2 = !this.edition.isReadOnly;
				if (flag2)
				{
					TextEditingManipulator editingManipulator2 = this.editingManipulator;
					if (editingManipulator2 != null)
					{
						editingManipulator2.HandleEventBubbleUp(evt);
					}
				}
				BaseVisualElementPanel elementPanel = base.elementPanel;
				if (elementPanel != null)
				{
					ContextualMenuManager contextualMenuManager = elementPanel.contextualMenuManager;
					if (contextualMenuManager != null)
					{
						contextualMenuManager.DisplayMenuIfEventMatches(evt, this);
					}
				}
				long? num = ((evt != null) ? new long?(evt.eventTypeId) : null);
				long num2 = EventBase<ContextualMenuPopulateEvent>.TypeId();
				bool flag3 = (num.GetValueOrDefault() == num2) & (num != null);
				if (flag3)
				{
					ContextualMenuPopulateEvent e = evt as ContextualMenuPopulateEvent;
					int count = e.menu.MenuItems().Count;
					this.BuildContextualMenu(e);
					bool flag4 = count > 0 && e.menu.MenuItems().Count > count;
					if (flag4)
					{
						e.menu.InsertSeparator(null, count);
					}
				}
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x00077419 File Offset: 0x00075619
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x00077424 File Offset: 0x00075624
		int ITextEdition.maxLength
		{
			get
			{
				return this.m_MaxLength;
			}
			set
			{
				bool flag = this.m_MaxLength == value;
				if (!flag)
				{
					this.m_MaxLength = value;
					this.text = this.edition.CullString(this.text);
					base.NotifyPropertyChanged(in TextElement.maxLengthProperty);
				}
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0007746C File Offset: 0x0007566C
		// (set) Token: 0x0600202A RID: 8234 RVA: 0x00077479 File Offset: 0x00075679
		[CreateProperty]
		private int maxLength
		{
			get
			{
				return this.edition.maxLength;
			}
			set
			{
				this.edition.maxLength = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x00077488 File Offset: 0x00075688
		// (set) Token: 0x0600202C RID: 8236 RVA: 0x00077490 File Offset: 0x00075690
		string ITextEdition.placeholder
		{
			get
			{
				return this.m_PlaceholderText;
			}
			set
			{
				bool flag = value == this.m_PlaceholderText;
				if (!flag)
				{
					bool flag2 = !string.IsNullOrEmpty(value) && (this.text == null || this.text.Equals(this.edition.GetDefaultValueType()));
					if (flag2)
					{
						this.text = "";
					}
					this.m_PlaceholderText = value;
					Action onPlaceholderChanged = this.OnPlaceholderChanged;
					if (onPlaceholderChanged != null)
					{
						onPlaceholderChanged();
					}
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x00077511 File Offset: 0x00075711
		// (set) Token: 0x0600202E RID: 8238 RVA: 0x00077519 File Offset: 0x00075719
		bool ITextEdition.isDelayed { get; set; }

		// Token: 0x0600202F RID: 8239 RVA: 0x00077522 File Offset: 0x00075722
		void ITextEdition.SaveValueAndText()
		{
			this.m_OriginalText = this.text;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00077531 File Offset: 0x00075731
		void ITextEdition.RestoreValueAndText()
		{
			this.text = this.m_OriginalText;
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x00077541 File Offset: 0x00075741
		// (set) Token: 0x06002032 RID: 8242 RVA: 0x00077549 File Offset: 0x00075749
		Func<char, bool> ITextEdition.AcceptCharacter { get; set; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x00077552 File Offset: 0x00075752
		// (set) Token: 0x06002034 RID: 8244 RVA: 0x0007755A File Offset: 0x0007575A
		Action<bool> ITextEdition.UpdateScrollOffset { get; set; }

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x00077563 File Offset: 0x00075763
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x0007756B File Offset: 0x0007576B
		Action ITextEdition.UpdateValueFromText { get; set; }

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x00077574 File Offset: 0x00075774
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x0007757C File Offset: 0x0007577C
		Action ITextEdition.UpdateTextFromValue { get; set; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x00077585 File Offset: 0x00075785
		// (set) Token: 0x0600203A RID: 8250 RVA: 0x0007758D File Offset: 0x0007578D
		Action ITextEdition.MoveFocusToCompositeRoot { get; set; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x00077596 File Offset: 0x00075796
		// (set) Token: 0x0600203C RID: 8252 RVA: 0x0007759E File Offset: 0x0007579E
		internal Action OnPlaceholderChanged { get; set; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x000775A7 File Offset: 0x000757A7
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x000775AF File Offset: 0x000757AF
		Func<string> ITextEdition.GetDefaultValueType { get; set; }

		// Token: 0x0600203F RID: 8255 RVA: 0x000775B8 File Offset: 0x000757B8
		void ITextEdition.UpdateText(string value)
		{
			bool flag = this.m_TouchScreenKeyboard != null && this.m_TouchScreenKeyboard.text != value;
			if (flag)
			{
				this.m_TouchScreenKeyboard.text = value;
			}
			bool flag2 = this.text != value;
			if (flag2)
			{
				using (InputEvent evt = InputEvent.GetPooled(this.text, value))
				{
					evt.elementTarget = base.parent;
					((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
					VisualElement parent = base.parent;
					if (parent != null)
					{
						parent.SendEvent(evt);
					}
				}
			}
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0007765C File Offset: 0x0007585C
		string ITextEdition.CullString(string s)
		{
			int mLength = this.edition.maxLength;
			bool flag = mLength >= 0 && s != null && s.Length > mLength;
			string text;
			if (flag)
			{
				text = s.Substring(0, mLength);
			}
			else
			{
				text = s;
			}
			return text;
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x0007769D File Offset: 0x0007589D
		// (set) Token: 0x06002042 RID: 8258 RVA: 0x000776A8 File Offset: 0x000758A8
		char ITextEdition.maskChar
		{
			get
			{
				return this.m_MaskChar;
			}
			set
			{
				bool flag = this.m_MaskChar != value;
				if (flag)
				{
					this.m_MaskChar = value;
					bool isPassword = this.edition.isPassword;
					if (isPassword)
					{
						base.IncrementVersion(VersionChangeType.Repaint);
					}
					base.NotifyPropertyChanged(in TextElement.maskCharProperty);
				}
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x000776F8 File Offset: 0x000758F8
		// (set) Token: 0x06002044 RID: 8260 RVA: 0x00077705 File Offset: 0x00075905
		[CreateProperty]
		private char maskChar
		{
			get
			{
				return this.edition.maskChar;
			}
			set
			{
				this.edition.maskChar = value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00077714 File Offset: 0x00075914
		private char effectiveMaskChar
		{
			get
			{
				return this.edition.isPassword ? this.m_MaskChar : '\0';
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x0007772C File Offset: 0x0007592C
		// (set) Token: 0x06002047 RID: 8263 RVA: 0x00077734 File Offset: 0x00075934
		bool ITextEdition.isPassword
		{
			get
			{
				return this.m_IsPassword;
			}
			set
			{
				bool flag = this.m_IsPassword != value;
				if (flag)
				{
					this.m_IsPassword = value;
					base.IncrementVersion(VersionChangeType.Repaint);
					base.NotifyPropertyChanged(in TextElement.isPasswordProperty);
				}
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x00077773 File Offset: 0x00075973
		// (set) Token: 0x06002049 RID: 8265 RVA: 0x00077780 File Offset: 0x00075980
		[CreateProperty]
		private bool isPassword
		{
			get
			{
				return this.edition.isPassword;
			}
			set
			{
				this.edition.isPassword = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0007778F File Offset: 0x0007598F
		// (set) Token: 0x0600204B RID: 8267 RVA: 0x00077797 File Offset: 0x00075997
		bool ITextEdition.hidePlaceholderOnFocus
		{
			get
			{
				return this.m_HidePlaceholderTextOnFocus;
			}
			set
			{
				this.m_HidePlaceholderTextOnFocus = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x000777A0 File Offset: 0x000759A0
		internal bool showPlaceholderText
		{
			get
			{
				bool isPlaceholderVisible = this.m_PlaceholderText.Length > 0;
				bool shouldHideOnFocus = this.edition.hidePlaceholderOnFocus && this.hasFocus;
				bool isTextEmpty = string.IsNullOrEmpty(this.text);
				bool flag = !isPlaceholderVisible;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					bool flag3 = shouldHideOnFocus;
					flag2 = !flag3 && isTextEmpty;
				}
				return flag2;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x00077800 File Offset: 0x00075A00
		// (set) Token: 0x0600204E RID: 8270 RVA: 0x00077808 File Offset: 0x00075A08
		bool ITextEdition.autoCorrection
		{
			get
			{
				return this.m_AutoCorrection;
			}
			set
			{
				bool flag = this.m_AutoCorrection == value;
				if (!flag)
				{
					this.m_AutoCorrection = value;
					base.NotifyPropertyChanged(in TextElement.autoCorrectionProperty);
				}
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x00077838 File Offset: 0x00075A38
		// (set) Token: 0x06002050 RID: 8272 RVA: 0x00077845 File Offset: 0x00075A45
		[CreateProperty]
		private bool autoCorrection
		{
			get
			{
				return this.edition.autoCorrection;
			}
			set
			{
				this.edition.autoCorrection = value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x00077854 File Offset: 0x00075A54
		internal RenderedText renderedText
		{
			get
			{
				bool showPlaceholderText = this.showPlaceholderText;
				RenderedText renderedText;
				if (showPlaceholderText)
				{
					renderedText = (TextUtilities.IsAdvancedTextEnabledForElement(this) ? new RenderedText(this.m_PlaceholderText) : new RenderedText(this.m_PlaceholderText, "\u200b"));
				}
				else
				{
					bool flag = this.effectiveMaskChar > '\0';
					if (flag)
					{
						RenderedText renderedText3;
						if (!TextUtilities.IsAdvancedTextEnabledForElement(this))
						{
							char effectiveMaskChar = this.effectiveMaskChar;
							string renderedText2 = this.m_RenderedText;
							renderedText3 = new RenderedText(effectiveMaskChar, (renderedText2 != null) ? renderedText2.Length : 0, "\u200b");
						}
						else
						{
							char effectiveMaskChar2 = this.effectiveMaskChar;
							string renderedText4 = this.m_RenderedText;
							renderedText3 = new RenderedText(effectiveMaskChar2, (renderedText4 != null) ? renderedText4.Length : 0, null);
						}
						renderedText = renderedText3;
					}
					else
					{
						bool flag2 = !TextUtilities.IsAdvancedTextEnabledForElement(this) && !this.isReadOnly;
						if (flag2)
						{
							renderedText = new RenderedText(this.m_RenderedText, "\u200b");
						}
						else
						{
							renderedText = new RenderedText(this.m_RenderedText);
						}
					}
				}
				return renderedText;
			}
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0007792F File Offset: 0x00075B2F
		private void SetRenderedText(string value)
		{
			this.m_RenderedText = value;
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00077939 File Offset: 0x00075B39
		internal string originalText
		{
			get
			{
				return this.m_OriginalText;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x00038529 File Offset: 0x00036729
		[CreateProperty(ReadOnly = true)]
		public ITextSelection selection
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00077941 File Offset: 0x00075B41
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x00077954 File Offset: 0x00075B54
		bool ITextSelection.isSelectable
		{
			get
			{
				return this.m_IsSelectable && this.focusable;
			}
			set
			{
				bool flag = value == this.m_IsSelectable;
				if (!flag)
				{
					this.focusable = value;
					this.m_IsSelectable = value;
					base.EnableInClassList(TextElement.selectableUssClassName, value);
					base.NotifyPropertyChanged(in TextElement.isSelectableProperty);
				}
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00077999 File Offset: 0x00075B99
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x000779A6 File Offset: 0x00075BA6
		[CreateProperty]
		internal bool isSelectable
		{
			get
			{
				return this.selection.isSelectable;
			}
			set
			{
				this.selection.isSelectable = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x000779B5 File Offset: 0x00075BB5
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x000779D4 File Offset: 0x00075BD4
		int ITextSelection.cursorIndex
		{
			get
			{
				return this.selection.isSelectable ? this.selectingManipulator.cursorIndex : (-1);
			}
			set
			{
				int current = this.selection.cursorIndex;
				bool isSelectable = this.selection.isSelectable;
				if (isSelectable)
				{
					this.selectingManipulator.cursorIndex = value;
				}
				bool flag = current != this.selection.cursorIndex;
				if (flag)
				{
					base.NotifyPropertyChanged(in TextElement.cursorIndexProperty);
				}
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00077A2B File Offset: 0x00075C2B
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x00077A38 File Offset: 0x00075C38
		[CreateProperty]
		private int cursorIndex
		{
			get
			{
				return this.selection.cursorIndex;
			}
			set
			{
				this.selection.cursorIndex = value;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00077A47 File Offset: 0x00075C47
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x00077A64 File Offset: 0x00075C64
		int ITextSelection.selectIndex
		{
			get
			{
				return this.selection.isSelectable ? this.selectingManipulator.selectIndex : (-1);
			}
			set
			{
				int current = this.selection.selectIndex;
				bool isSelectable = this.selection.isSelectable;
				if (isSelectable)
				{
					this.selectingManipulator.selectIndex = value;
				}
				bool flag = current != this.selection.selectIndex;
				if (flag)
				{
					base.NotifyPropertyChanged(in TextElement.selectIndexProperty);
				}
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00077ABB File Offset: 0x00075CBB
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x00077AC8 File Offset: 0x00075CC8
		[CreateProperty]
		private int selectIndex
		{
			get
			{
				return this.selection.selectIndex;
			}
			set
			{
				this.selection.selectIndex = value;
			}
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00077AD8 File Offset: 0x00075CD8
		void ITextSelection.SelectAll()
		{
			bool isSelectable = this.selection.isSelectable;
			if (isSelectable)
			{
				this.selectingManipulator.m_SelectingUtilities.SelectAll();
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00077B08 File Offset: 0x00075D08
		void ITextSelection.SelectNone()
		{
			bool isSelectable = this.selection.isSelectable;
			if (isSelectable)
			{
				this.selectingManipulator.m_SelectingUtilities.SelectNone();
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00077B38 File Offset: 0x00075D38
		bool ITextSelection.HasSelection()
		{
			return this.selection.isSelectable && this.selectingManipulator.HasSelection();
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002064 RID: 8292 RVA: 0x00077B65 File Offset: 0x00075D65
		// (set) Token: 0x06002065 RID: 8293 RVA: 0x00077B70 File Offset: 0x00075D70
		bool ITextSelection.doubleClickSelectsWord
		{
			get
			{
				return this.m_DoubleClickSelectsWord;
			}
			set
			{
				bool flag = this.m_DoubleClickSelectsWord == value;
				if (!flag)
				{
					this.m_DoubleClickSelectsWord = value;
					base.NotifyPropertyChanged(in TextElement.doubleClickSelectsWordProperty);
				}
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x00077BA0 File Offset: 0x00075DA0
		// (set) Token: 0x06002067 RID: 8295 RVA: 0x00077BAD File Offset: 0x00075DAD
		[CreateProperty]
		internal bool doubleClickSelectsWord
		{
			get
			{
				return this.selection.doubleClickSelectsWord;
			}
			set
			{
				this.selection.doubleClickSelectsWord = value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x00077BBC File Offset: 0x00075DBC
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x00077BC4 File Offset: 0x00075DC4
		bool ITextSelection.tripleClickSelectsLine
		{
			get
			{
				return this.m_TripleClickSelectsLine;
			}
			set
			{
				bool flag = this.m_TripleClickSelectsLine == value;
				if (!flag)
				{
					this.m_TripleClickSelectsLine = value;
					base.NotifyPropertyChanged(in TextElement.tripleClickSelectsLineProperty);
				}
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x00077BF4 File Offset: 0x00075DF4
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x00077C01 File Offset: 0x00075E01
		[CreateProperty]
		internal bool tripleClickSelectsLine
		{
			get
			{
				return this.selection.tripleClickSelectsLine;
			}
			set
			{
				this.selection.tripleClickSelectsLine = value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x00077C10 File Offset: 0x00075E10
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x00077C18 File Offset: 0x00075E18
		bool ITextSelection.selectAllOnFocus
		{
			get
			{
				return this.m_SelectAllOnFocus;
			}
			set
			{
				bool flag = this.m_SelectAllOnFocus == value;
				if (!flag)
				{
					this.m_SelectAllOnFocus = value;
					base.NotifyPropertyChanged(in TextElement.selectAllOnFocusProperty);
				}
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x00077C48 File Offset: 0x00075E48
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x00077C55 File Offset: 0x00075E55
		[CreateProperty]
		private bool selectAllOnFocus
		{
			get
			{
				return this.selection.selectAllOnFocus;
			}
			set
			{
				this.selection.selectAllOnFocus = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x00077C64 File Offset: 0x00075E64
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x00077C6C File Offset: 0x00075E6C
		bool ITextSelection.selectAllOnMouseUp
		{
			get
			{
				return this.m_SelectAllOnMouseUp;
			}
			set
			{
				bool flag = this.m_SelectAllOnMouseUp == value;
				if (!flag)
				{
					this.m_SelectAllOnMouseUp = value;
					base.NotifyPropertyChanged(in TextElement.selectAllOnMouseUpProperty);
				}
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x00077C9C File Offset: 0x00075E9C
		// (set) Token: 0x06002073 RID: 8307 RVA: 0x00077CA9 File Offset: 0x00075EA9
		[CreateProperty]
		private bool selectAllOnMouseUp
		{
			get
			{
				return this.selection.selectAllOnMouseUp;
			}
			set
			{
				this.selection.selectAllOnMouseUp = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x00077CB8 File Offset: 0x00075EB8
		Vector2 ITextSelection.cursorPosition
		{
			get
			{
				this.uitkTextHandle.AddTextInfoToPermanentCache();
				return this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(this.selection.cursorIndex, false, true) + base.contentRect.min;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x00077D01 File Offset: 0x00075F01
		[CreateProperty(ReadOnly = true)]
		private Vector2 cursorPosition
		{
			get
			{
				return this.selection.cursorPosition;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x00077D10 File Offset: 0x00075F10
		float ITextSelection.lineHeightAtCursorPosition
		{
			get
			{
				this.uitkTextHandle.AddTextInfoToPermanentCache();
				return this.uitkTextHandle.GetLineHeightFromCharacterIndex(this.selection.cursorIndex);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x00077D44 File Offset: 0x00075F44
		// (set) Token: 0x06002078 RID: 8312 RVA: 0x00077D4C File Offset: 0x00075F4C
		Color ITextSelection.selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				bool flag = this.m_SelectionColor == value;
				if (!flag)
				{
					this.m_SelectionColor = value;
					base.NotifyPropertyChanged(in TextElement.selectionColorProperty);
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x00077D86 File Offset: 0x00075F86
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x00077D93 File Offset: 0x00075F93
		[CreateProperty]
		private Color selectionColor
		{
			get
			{
				return this.selection.selectionColor;
			}
			set
			{
				this.selection.selectionColor = value;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x00077DA2 File Offset: 0x00075FA2
		// (set) Token: 0x0600207C RID: 8316 RVA: 0x00077DAC File Offset: 0x00075FAC
		Color ITextSelection.cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				bool flag = this.m_CursorColor == value;
				if (!flag)
				{
					this.m_CursorColor = value;
					base.NotifyPropertyChanged(in TextElement.cursorColorProperty);
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x00077DE6 File Offset: 0x00075FE6
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x00077DF3 File Offset: 0x00075FF3
		[CreateProperty]
		private Color cursorColor
		{
			get
			{
				return this.selection.cursorColor;
			}
			set
			{
				this.selection.cursorColor = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x00077E02 File Offset: 0x00076002
		float ITextSelection.cursorWidth
		{
			get
			{
				return this.m_CursorWidth;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x00077E0C File Offset: 0x0007600C
		internal TextSelectingManipulator selectingManipulator
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				TextSelectingManipulator textSelectingManipulator;
				if ((textSelectingManipulator = this.m_SelectingManipulator) == null)
				{
					textSelectingManipulator = (this.m_SelectingManipulator = new TextSelectingManipulator(this));
				}
				return textSelectingManipulator;
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00077E34 File Offset: 0x00076034
		private void DrawHighlighting(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			Color playmodeTintColor = ((visualElement != null) ? visualElement.playModeTintColor : Color.white);
			int startIndex = Math.Min(this.selection.cursorIndex, this.selection.selectIndex);
			int endIndex = Math.Max(this.selection.cursorIndex, this.selection.selectIndex);
			Vector2 startPos = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(startIndex, false, true);
			Vector2 endPos = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(endIndex, false, true);
			int firstLineIndex = this.uitkTextHandle.GetLineNumber(startIndex);
			int lastLineIndex = this.uitkTextHandle.GetLineNumber(endIndex);
			float lineHeight = this.uitkTextHandle.GetLineHeight(firstLineIndex);
			Vector2 layoutOffset = base.contentRect.min;
			bool flag = this.m_TouchScreenKeyboard != null && this.hideMobileInput;
			if (flag)
			{
				TextInfo textInfo = this.uitkTextHandle.textInfo;
				int stringPosition = ((this.selection.selectIndex < this.selection.cursorIndex) ? textInfo.textElementInfo[this.selection.selectIndex].index : textInfo.textElementInfo[this.selection.cursorIndex].index);
				int length = ((this.selection.selectIndex < this.selection.cursorIndex) ? (this.selection.cursorIndex - stringPosition) : (this.selection.selectIndex - stringPosition));
				this.m_TouchScreenKeyboard.selection = new RangeInt(stringPosition, length);
			}
			bool flag2 = firstLineIndex == lastLineIndex;
			if (flag2)
			{
				startPos += layoutOffset;
				endPos += layoutOffset;
				mgc.meshGenerator.DrawRectangle(new MeshGenerator.RectangleParams
				{
					rect = new Rect(startPos.x, startPos.y - lineHeight, endPos.x - startPos.x, lineHeight),
					color = this.selection.selectionColor,
					playmodeTintColor = playmodeTintColor
				});
			}
			else
			{
				for (int lineIndex = firstLineIndex; lineIndex <= lastLineIndex; lineIndex++)
				{
					bool flag3 = lineIndex == firstLineIndex;
					if (flag3)
					{
						int lastCharacterOnLine = this.GetLastCharacterAt(lineIndex);
						endPos = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(lastCharacterOnLine, true, true);
					}
					else
					{
						bool flag4 = lineIndex == lastLineIndex;
						if (flag4)
						{
							int firstCharacterOnLine = this.uitkTextHandle.textInfo.lineInfo[lineIndex].firstCharacterIndex;
							startPos = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(firstCharacterOnLine, false, true);
							endPos = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(endIndex, true, true);
						}
						else
						{
							bool flag5 = lineIndex != firstLineIndex && lineIndex != lastLineIndex;
							if (flag5)
							{
								int firstCharacterOnLine = this.uitkTextHandle.textInfo.lineInfo[lineIndex].firstCharacterIndex;
								startPos = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(firstCharacterOnLine, false, true);
								int lastCharacterOnLine = this.GetLastCharacterAt(lineIndex);
								endPos = this.uitkTextHandle.GetCursorPositionFromStringIndexUsingLineHeight(lastCharacterOnLine, true, true);
							}
						}
					}
					startPos += layoutOffset;
					endPos += layoutOffset;
					mgc.meshGenerator.DrawRectangle(new MeshGenerator.RectangleParams
					{
						rect = new Rect(startPos.x, startPos.y - lineHeight, endPos.x - startPos.x, lineHeight),
						color = this.selection.selectionColor,
						playmodeTintColor = playmodeTintColor
					});
				}
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x000781AC File Offset: 0x000763AC
		private void DrawNativeHighlighting(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			Color playmodeTintColor = ((visualElement != null) ? visualElement.playModeTintColor : Color.white);
			int startIndex = Math.Min(this.selection.cursorIndex, this.selection.selectIndex);
			int endIndex = Math.Max(this.selection.cursorIndex, this.selection.selectIndex);
			Rect[] rectangles = this.uitkTextHandle.GetHighlightRectangles(startIndex, endIndex);
			for (int i = 0; i < rectangles.Length; i++)
			{
				mgc.meshGenerator.DrawRectangle(new MeshGenerator.RectangleParams
				{
					rect = new Rect(rectangles[i].position + base.contentRect.min, rectangles[i].size),
					color = this.selection.selectionColor,
					playmodeTintColor = playmodeTintColor
				});
			}
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0007829C File Offset: 0x0007649C
		internal void DrawCaret(MeshGenerationContext mgc)
		{
			VisualElement visualElement = mgc.visualElement;
			Color playmodeTintColor = ((visualElement != null) ? visualElement.playModeTintColor : Color.white);
			float characterHeight = this.uitkTextHandle.GetCharacterHeightFromIndex(this.selection.cursorIndex);
			float width = AlignmentUtils.CeilToPixelGrid(this.selection.cursorWidth, base.scaledPixelsPerPoint, -0.02f);
			mgc.meshGenerator.DrawRectangle(new MeshGenerator.RectangleParams
			{
				rect = new Rect(this.selection.cursorPosition.x, this.selection.cursorPosition.y - characterHeight, width, characterHeight),
				color = this.selection.cursorColor,
				playmodeTintColor = playmodeTintColor
			});
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00078354 File Offset: 0x00076554
		private int GetLastCharacterAt(int lineIndex)
		{
			int lastCharacterIndex = this.uitkTextHandle.textInfo.lineInfo[lineIndex].lastCharacterIndex;
			int firstCharacterIndex = this.uitkTextHandle.textInfo.lineInfo[lineIndex].firstCharacterIndex;
			TextElementInfo lastCharacter = this.uitkTextHandle.textInfo.textElementInfo[lastCharacterIndex];
			for (;;)
			{
				uint character = lastCharacter.character;
				if ((character != 10U && character != 13U) || lastCharacterIndex <= firstCharacterIndex)
				{
					break;
				}
				lastCharacter = this.uitkTextHandle.textInfo.textElementInfo[--lastCharacterIndex];
			}
			return lastCharacterIndex;
		}

		// Token: 0x04000E3B RID: 3643
		internal static readonly BindingId displayTooltipWhenElidedProperty = "displayTooltipWhenElided";

		// Token: 0x04000E3C RID: 3644
		internal static readonly BindingId emojiFallbackSupportProperty = "emojiFallbackSupport";

		// Token: 0x04000E3D RID: 3645
		internal static readonly BindingId enableRichTextProperty = "enableRichText";

		// Token: 0x04000E3E RID: 3646
		internal static readonly BindingId isElidedProperty = "isElided";

		// Token: 0x04000E3F RID: 3647
		internal static readonly BindingId parseEscapeSequencesProperty = "parseEscapeSequences";

		// Token: 0x04000E40 RID: 3648
		internal static readonly BindingId textProperty = "text";

		// Token: 0x04000E41 RID: 3649
		internal static readonly BindingId valueProperty = "value";

		// Token: 0x04000E42 RID: 3650
		public static readonly string ussClassName = "unity-text-element";

		// Token: 0x04000E43 RID: 3651
		public static readonly string selectableUssClassName = TextElement.ussClassName + "__selectable";

		// Token: 0x04000E45 RID: 3653
		private string m_Text = string.Empty;

		// Token: 0x04000E46 RID: 3654
		private bool m_EnableRichText = true;

		// Token: 0x04000E47 RID: 3655
		private bool m_EmojiFallbackSupport = true;

		// Token: 0x04000E48 RID: 3656
		private bool m_ParseEscapeSequences;

		// Token: 0x04000E49 RID: 3657
		private bool m_DisplayTooltipWhenElided = true;

		// Token: 0x04000E4B RID: 3659
		internal static readonly string k_EllipsisText = "...";

		// Token: 0x04000E4C RID: 3660
		internal string elidedText;

		// Token: 0x04000E4D RID: 3661
		private bool m_WasElided;

		// Token: 0x04000E4E RID: 3662
		internal static readonly BindingId autoCorrectionProperty = "autoCorrection";

		// Token: 0x04000E4F RID: 3663
		internal static readonly BindingId hideMobileInputProperty = "hideMobileInput";

		// Token: 0x04000E50 RID: 3664
		internal static readonly BindingId keyboardTypeProperty = "keyboardType";

		// Token: 0x04000E51 RID: 3665
		internal static readonly BindingId isReadOnlyProperty = "isReadOnly";

		// Token: 0x04000E52 RID: 3666
		internal static readonly BindingId isPasswordProperty = "isPassword";

		// Token: 0x04000E53 RID: 3667
		internal static readonly BindingId maxLengthProperty = "maxLength";

		// Token: 0x04000E54 RID: 3668
		internal static readonly BindingId maskCharProperty = "maskChar";

		// Token: 0x04000E56 RID: 3670
		private bool m_Multiline;

		// Token: 0x04000E57 RID: 3671
		internal TouchScreenKeyboard m_TouchScreenKeyboard;

		// Token: 0x04000E58 RID: 3672
		internal Action<bool> onIsReadOnlyChanged;

		// Token: 0x04000E59 RID: 3673
		internal TouchScreenKeyboardType m_KeyboardType = TouchScreenKeyboardType.Default;

		// Token: 0x04000E5A RID: 3674
		private bool m_HideMobileInput;

		// Token: 0x04000E5B RID: 3675
		private bool m_IsReadOnly = true;

		// Token: 0x04000E5C RID: 3676
		private int m_MaxLength = -1;

		// Token: 0x04000E5D RID: 3677
		private string m_PlaceholderText = "";

		// Token: 0x04000E66 RID: 3686
		private const string ZeroWidthSpace = "\u200b";

		// Token: 0x04000E67 RID: 3687
		private string m_RenderedText;

		// Token: 0x04000E68 RID: 3688
		private string m_OriginalText;

		// Token: 0x04000E69 RID: 3689
		private char m_MaskChar;

		// Token: 0x04000E6A RID: 3690
		private bool m_IsPassword;

		// Token: 0x04000E6B RID: 3691
		private bool m_HidePlaceholderTextOnFocus;

		// Token: 0x04000E6C RID: 3692
		private bool m_AutoCorrection;

		// Token: 0x04000E6D RID: 3693
		internal static readonly BindingId isSelectableProperty = "isSelectable";

		// Token: 0x04000E6E RID: 3694
		internal static readonly BindingId cursorIndexProperty = "cursorIndex";

		// Token: 0x04000E6F RID: 3695
		internal static readonly BindingId selectIndexProperty = "selectIndex";

		// Token: 0x04000E70 RID: 3696
		internal static readonly BindingId doubleClickSelectsWordProperty = "doubleClickSelectsWord";

		// Token: 0x04000E71 RID: 3697
		internal static readonly BindingId tripleClickSelectsLineProperty = "tripleClickSelectsLine";

		// Token: 0x04000E72 RID: 3698
		internal static readonly BindingId cursorPositionProperty = "cursorPosition";

		// Token: 0x04000E73 RID: 3699
		internal static readonly BindingId selectionColorProperty = "selectionColor";

		// Token: 0x04000E74 RID: 3700
		internal static readonly BindingId cursorColorProperty = "cursorColor";

		// Token: 0x04000E75 RID: 3701
		internal static readonly BindingId selectAllOnFocusProperty = "selectAllOnFocus";

		// Token: 0x04000E76 RID: 3702
		internal static readonly BindingId selectAllOnMouseUpProperty = "selectAllOnMouseUp";

		// Token: 0x04000E77 RID: 3703
		internal static readonly BindingId selectionProperty = "selection";

		// Token: 0x04000E78 RID: 3704
		private TextSelectingManipulator m_SelectingManipulator;

		// Token: 0x04000E79 RID: 3705
		private bool m_IsSelectable;

		// Token: 0x04000E7A RID: 3706
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x04000E7B RID: 3707
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x04000E7C RID: 3708
		private bool m_SelectAllOnFocus = false;

		// Token: 0x04000E7D RID: 3709
		private bool m_SelectAllOnMouseUp = false;

		// Token: 0x04000E7E RID: 3710
		private Color m_SelectionColor = new Color(0.239f, 0.502f, 0.875f, 0.65f);

		// Token: 0x04000E7F RID: 3711
		private Color m_CursorColor = new Color(0.706f, 0.706f, 0.706f, 1f);

		// Token: 0x04000E80 RID: 3712
		private float m_CursorWidth = 1f;

		// Token: 0x02000451 RID: 1105
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<TextElement, TextElement.UxmlTraits>
		{
		}

		// Token: 0x02000452 RID: 1106
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06002087 RID: 8327 RVA: 0x000785AC File Offset: 0x000767AC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextElement textElement = (TextElement)ve;
				textElement.text = this.m_Text.GetValueFromBag(bag, cc);
				textElement.enableRichText = this.m_EnableRichText.GetValueFromBag(bag, cc);
				textElement.emojiFallbackSupport = this.m_EmojiFallbackSupport.GetValueFromBag(bag, cc);
				textElement.isSelectable = this.m_Selectable.GetValueFromBag(bag, cc);
				textElement.parseEscapeSequences = this.m_ParseEscapeSequences.GetValueFromBag(bag, cc);
				textElement.selection.doubleClickSelectsWord = this.m_SelectWordByDoubleClick.GetValueFromBag(bag, cc);
				textElement.selection.tripleClickSelectsLine = this.m_SelectLineByTripleClick.GetValueFromBag(bag, cc);
				textElement.displayTooltipWhenElided = this.m_DisplayTooltipWhenElided.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000E81 RID: 3713
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x04000E82 RID: 3714
			private UxmlBoolAttributeDescription m_EnableRichText = new UxmlBoolAttributeDescription
			{
				name = "enable-rich-text",
				defaultValue = true
			};

			// Token: 0x04000E83 RID: 3715
			private UxmlBoolAttributeDescription m_EmojiFallbackSupport = new UxmlBoolAttributeDescription
			{
				name = "emoji-fallback-support",
				defaultValue = true
			};

			// Token: 0x04000E84 RID: 3716
			private UxmlBoolAttributeDescription m_ParseEscapeSequences = new UxmlBoolAttributeDescription
			{
				name = "parse-escape-sequences"
			};

			// Token: 0x04000E85 RID: 3717
			private UxmlBoolAttributeDescription m_Selectable = new UxmlBoolAttributeDescription
			{
				name = "selectable"
			};

			// Token: 0x04000E86 RID: 3718
			private UxmlBoolAttributeDescription m_SelectWordByDoubleClick = new UxmlBoolAttributeDescription
			{
				name = "select-word-by-double-click"
			};

			// Token: 0x04000E87 RID: 3719
			private UxmlBoolAttributeDescription m_SelectLineByTripleClick = new UxmlBoolAttributeDescription
			{
				name = "select-line-by-triple-click"
			};

			// Token: 0x04000E88 RID: 3720
			private UxmlBoolAttributeDescription m_DisplayTooltipWhenElided = new UxmlBoolAttributeDescription
			{
				name = "display-tooltip-when-elided"
			};
		}
	}
}
