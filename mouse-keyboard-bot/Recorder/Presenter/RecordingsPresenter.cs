using mouse_keyboard_bot.Model;
using mouse_keyboard_bot.Model.Factories;
using mouse_keyboard_bot.Recorder.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mouse_keyboard_bot.Recorder.Presenter
{
    public class RecordingsPresenter
    {
        Recordings _view;
        Subscribe _subscribe;
        AppStorage _appStorage;
        UserActionsRecorder recorder;
        Replier replier;
        public RecordingsPresenter(Recordings view, Subscribe subscribe, ModelFactory modelFactory, AppStorage appStorage)
        {
            _view = view;
            _subscribe = subscribe;
            _appStorage = appStorage;

            recorder = new UserActionsRecorder(_subscribe, modelFactory);
            replier = new Replier();

            Init();
        }

        private void Init()
        {
            _view.AttachToSubscribe(_subscribe);

            _view.Start += recorder.StartRecording;
            _view.End += view_End;
            _view.ReplyStart += () => replier.Start(recording);
            _view.ReplyEnd += () => replier.Finish();

            _view.Setialize += _view_Setialize;

            _view.AddRecording += _view_AddRecording;

            //recording = Recording.Deserialize();

            recording = _appStorage.Recordings.FirstOrDefault();
        }

        private void _view_AddRecording(string name)
        {
            throw new NotImplementedException();
        }

        private void _view_Setialize()
        {
            //Recording.Serialize(recording);
        }

        Recording recording;
        private void view_End()
        {
            _appStorage.Recordings = new List<Recording>();
            _appStorage.Recordings.Add(recording);
            recording = recorder.FinishRecording();
        }

        private void AddHotkey()
        {

        }
    }
}
