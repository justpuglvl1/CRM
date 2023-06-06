using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Data.Interfaces;
using Data;
using Infrastructure.Commands;
using Models;
using WPF.ViewsModels;

namespace ViewsModels
{
    public class MainWindowViewModel : ViewModel
    {
        //IDiaryData _data = new DiaryDataTest();
        IDiaryData _data = new DiaryDataApi();

        public MainWindowViewModel()
        {
            NotesList = (ObservableCollection<Notes>)_data.AllNotes();
        }

        #region Properties

        private ObservableCollection<Notes> _notesList;
        public ObservableCollection<Notes> NotesList
        {
            get => _notesList;
            set => Set(ref _notesList, value);
        }

        private int _id;
        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        
        private string _description;
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private string _date;
        public string Date
        {
            get => _date;
            set => Set(ref _date, value);
        }

 

        private string _address;
        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        private string _iban;
        public string Iban
        {
            get => _iban;
            set => Set(ref _iban, value);
        }

        private Notes _selectedNote;
        public Notes SelectedNote
        {
            get => _selectedNote;
            set
            {
                Set(ref _selectedNote, value);

                if (SelectedNote != null)
                {
                    var currentNote = _data.GetNoteById(SelectedNote.Id);

                    Id = currentNote.Id;
                    Name = currentNote.Name;
                    Date = currentNote.Date;
                    Description = currentNote.Description;
                    Address = currentNote.Address;
                    Iban = currentNote.Iban;
                }

                else
                {
                    ClearDetails();
                }
            }
        }

        #endregion

        #region Commands

        private readonly ICommand _addNoteCommand;
        public ICommand AddNoteCommand => _addNoteCommand ?? new RelayCommand(() =>
        {
            var note = GetCurrentNote();
            _data.AddNote(note);

            RefreshData();
        });
        
        private readonly ICommand _today;
        public ICommand Today => _today ?? new RelayCommand(() =>
        {
            var note = GetCurrentNote();
            _data.AddNote(note);

            RefreshData();
        });

        private readonly ICommand _updateNoteCommand;
        public ICommand UpdateNoteCommand => _updateNoteCommand ?? new RelayCommand(() =>
        {
            var note = GetCurrentNote();
            _data.UpdateNote(note);

            RefreshData();
        });

        private readonly ICommand _clearDetailsCommand;
        public ICommand ClearDetailsCommand => _clearDetailsCommand ?? new RelayCommand(() =>
        {
            SelectedNote = null;
            ClearDetails();
        });

        private readonly ICommand _deleteNoteCommand;
        public ICommand DeleteNoteCommand => _deleteNoteCommand ?? new RelayCommand(() =>
        {
            if (SelectedNote != null)
                _data.DeleteNote(SelectedNote.Id);

            RefreshData();
        });

        #endregion

        private void ClearDetails()
        {
            Name = string.Empty;
            Date = string.Empty;
            Description = string.Empty;
            Address = string.Empty;
            Iban = string.Empty;
        }

        private Notes GetCurrentNote()
        {
            var note = new Notes();

            note.Id = Id;
            note.Name = Name;
            note.Date = Date;
            note.Description = Description;
            note.Address = Address;
            note.Iban = Iban;

            return note;
        }

        private void RefreshData()      // workaround for api mode
        {
            NotesList = (ObservableCollection<Notes>)_data.AllNotes();
        }
    }
}
