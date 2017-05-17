using Newtonsoft.Json;

namespace TicketTimer.Core
{
    public class JsonWorkItemStore : WorkItemStore
    {
        private readonly FileStore _fileStore;
        private const string FileName = "timer.state";
        private TimerState _state;

        public JsonWorkItemStore(FileStore fileStore)
        {
            _fileStore = fileStore;
        }


        public void Add(WorkItem workItem)
        {
            if (_state == null)
            {
                Load();
            }
            _state.WorkItems.Add(workItem);
        }

        public TimerState GetState()
        {
            if (_state == null)
            {
                Load();
            }
            return _state;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(_state);
            _fileStore.WriteFile(json, FileName);
        }

        public void Load()
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
    }
}
