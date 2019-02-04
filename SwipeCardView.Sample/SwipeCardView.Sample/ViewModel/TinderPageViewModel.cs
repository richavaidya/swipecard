﻿using MLToolkit.Forms.SwipeCardView.Core;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SwipeCardView.Sample.Model;
using Xamarin.Forms;

namespace SwipeCardView.Sample.ViewModel
{
    public class TinderPageViewModel : BasePageViewModel
    {
        private ObservableCollection<Profile> _profiles = new ObservableCollection<Profile>();

        private uint _threshold;
        private int _count;

        public TinderPageViewModel()
        {
            InitializeProfiles();

            Threshold = (uint)(App.ScreenWidth / 3);

            SwipedCommand = new Command<SwipedCardEventArgs>(OnSwipedCommand);
            DraggingCommand = new Command<DraggingCardEventArgs>(OnDraggingCommand);

            ClearItemsCommand = new Command(OnClearItemsCommand);
            AddItemsCommand = new Command(OnAddItemsCommand);
        }

        public ObservableCollection<Profile> Profiles
        {
            get => _profiles;
            set
            {
                _profiles = value;
                RaisePropertyChanged();
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                RaisePropertyChanged();
            }
        }

        public uint Threshold
        {
            get => _threshold;
            set
            {
                _threshold = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SwipedCommand { get; }

        public ICommand DraggingCommand { get; }

        public ICommand ClearItemsCommand { get; }

        public ICommand AddItemsCommand { get; }

        private void OnSwipedCommand(SwipedCardEventArgs eventArgs)
        {
        }

        private void OnDraggingCommand(DraggingCardEventArgs eventArgs)
        {
            switch (eventArgs.Position)
            {
                case DraggingCardPosition.Start:
                    return;

                case DraggingCardPosition.UnderThreshold:
                    break;

                case DraggingCardPosition.OverThreshold:
                    break;

                case DraggingCardPosition.FinishedUnderThreshold:
                    return;

                case DraggingCardPosition.FinishedOverThreshold:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnClearItemsCommand()
        {
            Profiles.Clear();
        }

        private void OnAddItemsCommand()
        {
        }

        private void InitializeProfiles()
        {
            // Photos are from https://unsplash.com/. Name and Age values are fictional.

            this.Profiles.Add(new Profile { ProfileId = 1, Name = "Laura", Age = 24, Gender = Gender.Female, Photo = "p705193.jpg", Date = DateTime.Parse("2019-02-04 11:00") });
            this.Profiles.Add(new Profile { ProfileId = 2, Name = "Sophia", Age = 21, Gender = Gender.Female, Photo = "p597956.jpg", Date = DateTime.Parse("2019-02-05 21:00") });
            this.Profiles.Add(new Profile { ProfileId = 3, Name = "Anne", Age = 19, Gender = Gender.Female, Photo = "p497489.jpg", Date = DateTime.Parse("2019-02-04 16:00") });
            this.Profiles.Add(new Profile { ProfileId = 4, Name = "Yvonne ", Age = 27, Gender = Gender.Female, Photo = "p467499.jpg", Date = DateTime.Parse("2019-04-02 13:00") });
            //this.Profiles.Add(new Profile { ProfileId = 5, Name = "Abby", Age = 25, Gender = Gender.Female, Photo = "p589739.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 6, Name = "Andressa", Age = 28, Gender = Gender.Female, Photo = "p453095.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 7, Name = "June", Age = 29, Gender = Gender.Female, Photo = "p503001.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 8, Name = "Kim", Age = 22, Gender = Gender.Female, Photo = "p627958.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 9, Name = "Denesha", Age = 26, Gender = Gender.Female, Photo = "p474893.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 10, Name = "Sasha", Age = 23, Gender = Gender.Female, Photo = "p458914.jpg", Date = DateTime.Now });

            //this.Profiles.Add(new Profile { ProfileId = 11, Name = "Austin", Age = 28, Gender = Gender.Male, Photo = "p378674.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 11, Name = "James", Age = 32, Gender = Gender.Male, Photo = "p398931.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 11, Name = "Chris", Age = 27, Gender = Gender.Male, Photo = "p401107.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 11, Name = "Alexander", Age = 30, Gender = Gender.Male, Photo = "p731150.jpg", Date = DateTime.Now });
            //this.Profiles.Add(new Profile { ProfileId = 11, Name = "Steve", Age = 31, Gender = Gender.Male, Photo = "p327144.jpg", Date = DateTime.Now });
        }
    }
}