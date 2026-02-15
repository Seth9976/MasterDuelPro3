using System;
using System.Collections.Generic;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000023 RID: 35
	internal class InputEventPartialProvider : IEventProviderImpl
	{
		// Token: 0x06000084 RID: 132 RVA: 0x000032F7 File Offset: 0x000014F7
		public void Initialize()
		{
			this._operatingSystemFamily = SystemInfo.operatingSystemFamily;
			this._keyboardButtonsState.Reset();
			this._eventModifiers.Reset();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000331D File Offset: 0x0000151D
		public void Shutdown()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003320 File Offset: 0x00001520
		public void Update()
		{
			int count = Event.GetEventCount();
			for (int i = 0; i < count; i++)
			{
				Event.GetEventAtIndex(i, this._ev);
				this.UpdateEventModifiers(in this._ev);
				EventType type = this._ev.type;
				EventType eventType = type;
				if (eventType - EventType.KeyDown > 1)
				{
					if (eventType - EventType.ValidateCommand <= 1)
					{
						Event @event = Event.From(this.ToCommandEvent(in this._ev));
						EventProvider.Dispatch(in @event);
					}
				}
				else
				{
					bool flag = this._ev.keyCode > KeyCode.None;
					if (flag)
					{
						Event @event = Event.From(this.ToKeyEvent(in this._ev));
						EventProvider.Dispatch(in @event);
						bool sendNavigationEventOnTabKey = this._sendNavigationEventOnTabKey;
						if (sendNavigationEventOnTabKey)
						{
							this.SendNextOrPreviousNavigationEventOnTabKeyDownEvent(in this._ev);
						}
					}
					else
					{
						bool flag2 = this._ev.character > '\0';
						if (flag2)
						{
							Event @event = Event.From(this.ToTextInputEvent(in this._ev));
							EventProvider.Dispatch(in @event);
						}
					}
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000342C File Offset: 0x0000162C
		public void OnFocusChanged(bool focus)
		{
			bool flag = !focus;
			if (flag)
			{
				this._eventModifiers.Reset();
				this._keyboardButtonsState.Reset();
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000345C File Offset: 0x0000165C
		public bool RequestCurrentState(Event.Type type)
		{
			bool flag;
			if (type != Event.Type.KeyEvent)
			{
				flag = false;
			}
			else
			{
				Event @event = Event.From(new KeyEvent
				{
					type = KeyEvent.Type.State,
					keyCode = KeyCode.None,
					buttonsState = this._keyboardButtonsState,
					timestamp = (DiscreteTime)Time.timeAsRational,
					eventSource = EventSource.Keyboard,
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000034E8 File Offset: 0x000016E8
		private DiscreteTime GetTimestamp(in Event ev)
		{
			return (DiscreteTime)Time.timeAsRational;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003504 File Offset: 0x00001704
		private void UpdateEventModifiers(in Event ev)
		{
			this._eventModifiers.SetPressed(EventModifiers.Modifiers.CapsLock, ev.capsLock);
			this._eventModifiers.SetPressed(EventModifiers.Modifiers.FunctionKey, ev.functionKey);
			this._eventModifiers.SetPressed(EventModifiers.Modifiers.Numeric, ev.numeric);
			bool flag = ev.isKey && ev.keyCode > KeyCode.None;
			if (flag)
			{
				bool pressed = ev.type == EventType.KeyDown;
				switch (ev.keyCode)
				{
				case KeyCode.Numlock:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.Numlock, pressed);
					break;
				case KeyCode.RightShift:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.RightShift, pressed);
					break;
				case KeyCode.LeftShift:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.LeftShift, pressed);
					break;
				case KeyCode.RightControl:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.RightCtrl, pressed);
					break;
				case KeyCode.LeftControl:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.LeftCtrl, pressed);
					break;
				case KeyCode.RightAlt:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.RightAlt, pressed);
					break;
				case KeyCode.LeftAlt:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.LeftAlt, pressed);
					break;
				case KeyCode.RightMeta:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.RightMeta, pressed);
					break;
				case KeyCode.LeftMeta:
					this._eventModifiers.SetPressed(EventModifiers.Modifiers.LeftMeta, pressed);
					break;
				}
			}
			bool flag2 = ev.shift != this._eventModifiers.IsPressed(EventModifiers.Modifiers.Shift);
			if (flag2)
			{
				this._eventModifiers.SetPressed(EventModifiers.Modifiers.Shift, ev.shift);
			}
			bool flag3 = ev.control != this._eventModifiers.IsPressed(EventModifiers.Modifiers.Ctrl);
			if (flag3)
			{
				this._eventModifiers.SetPressed(EventModifiers.Modifiers.Ctrl, ev.control);
			}
			bool flag4 = ev.alt != this._eventModifiers.IsPressed(EventModifiers.Modifiers.Alt);
			if (flag4)
			{
				this._eventModifiers.SetPressed(EventModifiers.Modifiers.Alt, ev.alt);
			}
			bool flag5 = ev.command != this._eventModifiers.IsPressed(EventModifiers.Modifiers.Meta);
			if (flag5)
			{
				this._eventModifiers.SetPressed(EventModifiers.Modifiers.Meta, ev.command);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003740 File Offset: 0x00001940
		private KeyEvent ToKeyEvent(in Event ev)
		{
			bool oldState = this._keyboardButtonsState.IsPressed(ev.keyCode);
			bool newState = ev.type == EventType.KeyDown;
			this._keyboardButtonsState.SetPressed(ev.keyCode, newState);
			return new KeyEvent
			{
				type = (newState ? (oldState ? KeyEvent.Type.KeyRepeated : KeyEvent.Type.KeyPressed) : KeyEvent.Type.KeyReleased),
				keyCode = ev.keyCode,
				buttonsState = this._keyboardButtonsState,
				timestamp = this.GetTimestamp(in ev),
				eventSource = EventSource.Keyboard,
				playerId = 0U,
				eventModifiers = this._eventModifiers
			};
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000037EC File Offset: 0x000019EC
		private TextInputEvent ToTextInputEvent(in Event ev)
		{
			return new TextInputEvent
			{
				character = ev.character,
				timestamp = this.GetTimestamp(in ev),
				eventSource = EventSource.Keyboard,
				playerId = 0U,
				eventModifiers = this._eventModifiers
			};
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003844 File Offset: 0x00001A44
		private void SendNextOrPreviousNavigationEventOnTabKeyDownEvent(in Event ev)
		{
			bool flag = this._ev.type == EventType.KeyDown && this._ev.keyCode == KeyCode.Tab;
			if (flag)
			{
				Event @event = Event.From(new NavigationEvent
				{
					type = NavigationEvent.Type.Move,
					direction = (this._ev.shift ? NavigationEvent.Direction.Previous : NavigationEvent.Direction.Next),
					timestamp = this.GetTimestamp(in this._ev),
					eventSource = EventSource.Keyboard,
					playerId = 0U,
					eventModifiers = this._eventModifiers
				});
				EventProvider.Dispatch(in @event);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000038E4 File Offset: 0x00001AE4
		private CommandEvent ToCommandEvent(in Event ev)
		{
			CommandEvent.Command cmd;
			bool flag = !this._IMGUICommandToInputForUICommandType.TryGetValue(ev.commandName, out cmd);
			if (flag)
			{
				Debug.LogWarning("Unsupported command name '" + ev.commandName + "'");
			}
			return new CommandEvent
			{
				type = ((ev.type == EventType.ValidateCommand) ? CommandEvent.Type.Validate : CommandEvent.Type.Execute),
				command = cmd,
				timestamp = this.GetTimestamp(in ev),
				eventSource = EventSource.Unspecified,
				playerId = 0U,
				eventModifiers = this._eventModifiers
			};
		}

		// Token: 0x040000B0 RID: 176
		private Event _ev = new Event();

		// Token: 0x040000B1 RID: 177
		private OperatingSystemFamily _operatingSystemFamily;

		// Token: 0x040000B2 RID: 178
		private KeyEvent.ButtonsState _keyboardButtonsState;

		// Token: 0x040000B3 RID: 179
		internal EventModifiers _eventModifiers;

		// Token: 0x040000B4 RID: 180
		internal bool _sendNavigationEventOnTabKey;

		// Token: 0x040000B5 RID: 181
		private IDictionary<string, CommandEvent.Command> _IMGUICommandToInputForUICommandType = new Dictionary<string, CommandEvent.Command>
		{
			{
				"Cut",
				CommandEvent.Command.Cut
			},
			{
				"Copy",
				CommandEvent.Command.Copy
			},
			{
				"Paste",
				CommandEvent.Command.Paste
			},
			{
				"SelectAll",
				CommandEvent.Command.SelectAll
			},
			{
				"DeselectAll",
				CommandEvent.Command.DeselectAll
			},
			{
				"InvertSelection",
				CommandEvent.Command.InvertSelection
			},
			{
				"Duplicate",
				CommandEvent.Command.Duplicate
			},
			{
				"Rename",
				CommandEvent.Command.Rename
			},
			{
				"Delete",
				CommandEvent.Command.Delete
			},
			{
				"SoftDelete",
				CommandEvent.Command.SoftDelete
			},
			{
				"Find",
				CommandEvent.Command.Find
			},
			{
				"SelectChildren",
				CommandEvent.Command.SelectChildren
			},
			{
				"SelectPrefabRoot",
				CommandEvent.Command.SelectPrefabRoot
			},
			{
				"UndoRedoPerformed",
				CommandEvent.Command.UndoRedoPerformed
			},
			{
				"OnLostFocus",
				CommandEvent.Command.OnLostFocus
			},
			{
				"NewKeyboardFocus",
				CommandEvent.Command.NewKeyboardFocus
			},
			{
				"ModifierKeysChanged",
				CommandEvent.Command.ModifierKeysChanged
			},
			{
				"EyeDropperUpdate",
				CommandEvent.Command.EyeDropperUpdate
			},
			{
				"EyeDropperClicked",
				CommandEvent.Command.EyeDropperClicked
			},
			{
				"EyeDropperCancelled",
				CommandEvent.Command.EyeDropperCancelled
			},
			{
				"ColorPickerChanged",
				CommandEvent.Command.ColorPickerChanged
			},
			{
				"FrameSelected",
				CommandEvent.Command.FrameSelected
			},
			{
				"FrameSelectedWithLock",
				CommandEvent.Command.FrameSelectedWithLock
			}
		};
	}
}
