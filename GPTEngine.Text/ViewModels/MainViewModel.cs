using GPTEngine.Roles;
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
using GPTSupervisorEngine.Roles;
using AssessorEngine.Agents;
using Lexicographer.Agents;
using AssessorEngine.Roles;
using System.Text.RegularExpressions;

namespace GPTEngine.Text.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        int MAX_ITERATIONS = 10;

        Conversation _step1, _step2;
        GPT _gpt;

        private ObservableCollection<string> _history;

        public List<RoleBehaviour> Roles { get; private set; }

        public ICommand RoleChangedCommand { get; private set; }

        public ICommand SendToGPT { get; set; }
        string _missionStatement;
        Conversation _supervisor;

        AgentLookup _agents;
        public MainViewModel()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _missionStatement = configuration["MissionStatement"];

            _gpt = new GPT(configuration["OpenApiKey"], configuration["Model"]);

            _history = new ObservableCollection<string>();
            _agents = new AgentLookup(_gpt);

            _agents.RoleAssigned += (s, RoleAssignation) => History.Add(RoleAssignation);
            _agents.SummarisationCompleted += async (s, e) =>
             {
                 History.Add("Summarisation Completed");
                 await SetupSupervisorAsync();

                 ShowInput = true;
                 OnPropertyChanged(nameof(ShowInput));
             };

            History.Add("Loading ... please wait");

            Activate();

            SendToGPT = new AsyncRelayCommand(SendToGPTHandlerAsync);
        }

        public bool ShowInput { get; set; } = false;

        private async Task SendToGPTHandlerAsync(object arg)
        {
            string input = Input;

            _supervisor.AddMessage($"Instruct your agents to define {input}");
            History.Add($"Instruct your agents to define {input}");
            var response = await _gpt.Call(_supervisor);

            int iterations = 0;

            while (!response.Response.Trim().ToUpper().StartsWith("OUT") && iterations++ < MAX_ITERATIONS)
            {
                string nextMessage = "Oops, something went wrong";

                History.Add(response.Response);
                if (response.IsError) return;


                if (response.Response.Trim().ToUpper().StartsWith("CALL"))
                {
                    nextMessage = "Oops, someting went wrong";

                    string text = response.Response.Substring(response.Response.IndexOf(' ') + 1);

                    string agentNameRaw = text.Split(' ')[0];

                    string responseText = text.Substring(agentNameRaw.Length + 1).Trim();

                    string agentName = Regex.Replace(agentNameRaw, "[^a-zA-Z0-9]", "");
                    var agent = _agents.GetAgent(agentName);

                    if (agent != null)
                    {
                        var convo = new Conversation(agent);
                        convo.AddMessage(input);

                        var agentResponse = await _gpt.Call(convo);
                        History.Add(agentResponse.Response);
                        nextMessage = $"Agent {agent.Name} responded: {agentResponse.Response}";
                        input = agentResponse.Response;
                    }
                    else
                    {
                        nextMessage = $"Agent {agentName} not found";
                    }
                }
                else
                {
                    nextMessage = "THE ONLY THINGS you may only output are 1) CALL (Agent's name) (with a Prompt), OR, 2) OUT: (with the output). DO NOT ANSWER THIS BY APOLOGISING OR MAKING ANY OTHER STATEMENT BUT THOSE 2. If the agent has completed the task reply OUT: with its output ";
                }

                _supervisor.AddMessage(nextMessage);
                History.Add($"> {nextMessage}");
                response = await _gpt.Call(_supervisor);
            }
            History.Add(response.Response);
            History.Add("Complete");
        }

        private async Task SetupSupervisorAsync()
        {
            _supervisor = new Conversation(
                new Role(RoleType.System, RoleBehaviour.Create(_missionStatement)),
                new Supervisor(_missionStatement, _agents).As(RoleType.Assistant),
                false);            
        }



        public async Task Activate()
        {
            ShowInput = false;
            OnPropertyChanged(nameof(ShowInput));

            await _agents.AddAgentsAsync(
                new Definer(), new Editor()
                );
        }


        private void OnRoleChanged(object parameter)
        {
            if (
                History.Count > 0 &&
                MessageBox.Show("Clear conversation?", "Are you sure", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;

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
