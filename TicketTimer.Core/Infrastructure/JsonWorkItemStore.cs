using System.Collections.Generic;
using Newtonsoft.Json;

namespace TicketTimer.Core.Infrastructure
{
    public class JsonWorkItemStore : WorkItemStore
    {
        private readonly FileStore _fileStore;
        private const string FileName = "timer.state";
        private TimerState _state;

        public JsonWorkItemStore(FileStore fileStore)
        {
            _fileStore = fileStore;
            Load();
        }

        public void AddToArchive(WorkItem workItem)
        {
            GetState().WorkItemArchive.Add(workItem);
            Save();
        }

        public TimerState GetState()
        {
            if (_state == null)
            {
                Load();
            }
            return _state;
        }

        public void RemoveFromArchive(string ticketNumber)
        {
            GetState().WorkItemArchive.RemoveAll(wi => wi.TicketNumber == ticketNumber);
            Save();
        }

        public void RemoveRangeFromArchive(IEnumerable<string> tickets)
        {
            foreach (var ticketNumber in tickets)
            {
                RemoveFromArchive(ticketNumber);
            }
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(_state, Formatting.Indented);
            _fileStore.WriteFile(json, FileName);
        }

        private void Load()
        {
            var json = _fileStore.ReadFile(FileName);
            if (!string.IsNullOrEmpty(json))
            {
                _state = JsonConvert.DeserializeObject<TimerState>(json);
            }
            else
            {
                _state = new TimerState();
            }
        }

        public void SetCurrent(WorkItem workItem)
        {
            GetState().CurrentWorkItem = workItem;
            Save();
        }

        public void ClearArchive()
        {
            GetState().WorkItemArchive.Clear();
            Save();
        }
    }
}
