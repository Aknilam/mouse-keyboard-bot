using System;
using System.Collections.Generic;

namespace mouse_keyboard_bot.Model
{
    [Serializable]
    public class HotkeyRecordings
    {
        int HotkeyId { get; set; }
        HashSet<int> RecordingsId { get; set; } = new HashSet<int>();

        public HotkeyRecordings(Hotkey hotkey)
        {
            HotkeyId = hotkey.Id;
        }

        public void AddRecording(Recording rec)
        {
            RecordingsId.Add(rec.Id);
        }

        public void RemoveRecording(Recording rec)
        {
            RecordingsId.Remove(rec.Id);
        }
    }
}