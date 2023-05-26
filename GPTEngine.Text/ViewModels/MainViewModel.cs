using GPTEngine.Roles;
using GPTEngine.Roles.Types;
using GPTEngine.Text.WPFCommand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace GPTEngine.Text.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        Conversation _step1, _step2;
        GPT _gpt;

        private ObservableCollection<string> _history;

        public List<RoleBehaviour> Roles { get; private set; }        

        public ICommand RoleChangedCommand { get; private set; }

        public ICommand SendToGPT { get; set; }

        public MainViewModel()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _gpt = new GPT(configuration["OpenApiKey"], configuration["Model"]);

            SendToGPT = new AsyncRelayCommand(SendToGPTHandlerAsync);
            RoleChangedCommand = new RelayCommand(OnRoleChanged);

            BuildRoles();

            _history = new ObservableCollection<string>();
            PrintNextStatement();

            ResetConversation();
        }

        private void PrintNextStatement()
        {
            History.Add("Please enter a word to define, and press Send (or simply hit [Enter]):");
        }

        private void ResetConversation()
        {
            _step1 = new Conversation(
                Roles[(int)ContentType.LyingLexicographer].As(RoleType.System),
                Roles[(int)ContentType.LyingLexicographer].As(RoleType.Assistant),
                Roles[(int)ContentType.LyingLexicographer].ResetEachTime
                );

            _step2 = new Conversation(
                Roles[(int)ContentType.LexicographicEditor].As(RoleType.System),
                Roles[(int)ContentType.LexicographicEditor].As(RoleType.Assistant),
                Roles[(int)ContentType.LexicographicEditor].ResetEachTime
                );
        }

        private void BuildRoles()
        {
            Roles = new List<RoleBehaviour>
            {
                
                new ArchitectAssistantRole(),
                new BiasedIdiot(),
                new GiftedSongwriter(),
                new KidsAuthor(),
                new Poet(),
                new Chef(),
                new Superhero(),
                new FakeLexicographer(),
                new LexicographicEditor(),
            };

            SortRoleBehaviours(Roles);

            SelectedIndex = 0;
        }

        public static void SortRoleBehaviours(List<RoleBehaviour> roles)
        {
            roles.Sort((role1, role2) => role1.ContentType.CompareTo(role2.ContentType));
        }

        private async Task SendToGPTHandlerAsync(object parameter)
        {
            _step1.AddMessage(Input);
            History.Add(Input);
            Input=string.Empty;

            History.Add("    > Looking up definition ...");

            GPTResponse response1 = await _gpt.Call(_step1);
            
            if (response1.IsError) History.Add($"Error: {response1.Error}");
            else
            {
                //History.Add(response1.Response);
                _step2.AddMessage(response1.Response);
            }

            History.Add("    > Thinking of a response ...");

            GPTResponse response2 = await _gpt.Call(_step2);

            if (response2.IsError) History.Add($"Error: {response2.Error}");
            else History.Add(response2.Response);

            History.Add("-------------------------------------------------");
            PrintNextStatement();
        }

        private void OnRoleChanged(object parameter)
        {
            if (
                History.Count > 0 &&
                MessageBox.Show("Clear conversation?", "Are you sure", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

            ResetConversation();

            History.Clear();


        }
        private string _input = string.Empty;
        
        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }
        private string _output = string.Empty;
        public string Output
        {
            get { return _output; }
            set
            {
                _output = value;
                OnPropertyChanged(nameof(Output));
            }
        }

        int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        public ObservableCollection<string> History
        {
            get { return _history; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
