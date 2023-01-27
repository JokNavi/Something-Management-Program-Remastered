﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Something_Management_Program_Remastered.Model;
using Something_Management_Program_Remastered.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Windows.Controls;

namespace Something_Management_Program_Remastered.ViewModel
{
    public partial class ObjectiveValueViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private ObservableCollection<ObjectiveValue> objectiveValueCollection;

        [ObservableProperty]
        private ObjectiveValue selectedObjectiveValue;

        [ObservableProperty]
        private Modifier currentModifier;

        [RelayCommand]
        private void NewObjectviveValue()
        {
            ObjectiveValueCollection.Add(new ObjectiveValue { Name = "Empty", Amount = 0, CurrentTime = DateTime.Now });
            WriteJsonObjectiveValueCollection(ObjectiveValueCollection);
            Debug.WriteLine("added");
        }

        [RelayCommand]
        private void DeleteObjectviveValue()
        {
            ObjectiveValueCollection.Remove(SelectedObjectiveValue);
            WriteJsonObjectiveValueCollection(ObjectiveValueCollection);
            Debug.WriteLine("removed");
        }

        public ObjectiveValueViewModel()
        {
            ObjectiveValueCollection = ReadJsonObjectiveValueCollection();      
        }

        #region Json Functions
        private ObservableCollection<ObjectiveValue> ReadJsonObjectiveValueCollection()
        {
            string text = File.ReadAllText(@"ObjectiveValueCollection.json");
            return JsonSerializer.Deserialize<ObservableCollection<ObjectiveValue>>(text) ?? new ObservableCollection<ObjectiveValue>();
        }

        public void WriteJsonObjectiveValueCollection(ObservableCollection<ObjectiveValue> obj)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            string jsonObjString = JsonSerializer.Serialize<ObservableCollection<ObjectiveValue>>(obj, options);
            File.WriteAllText(@"ObjectiveValueCollection.json", jsonObjString);
        }
        #endregion

    }
}