using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcAndFilter.Model
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ClacModel ClacModel1 { get; set; }
        public ClacModel ClacModel2 { get; set; }
        public ClacModel ClacModel3 { get; set; }
        private string filter;
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                OnPropertyChanged(nameof(Filter));
            }
        }

        public ViewModel()
        {
            ClacModel1 = new ClacModel();
            ClacModel2 = new ClacModel();
            ClacModel3 = new ClacModel();
            Filter = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class ClacModel : INotifyPropertyChanged
    {
        public ClacModel()
        {
            CalcContent = "";
            CalcResult = "";
            IsFormula = false;
            IsBitwise = false;
            IsContinuous = false;
        }
        private string calcContent;
        public string CalcContent
        {
            get { return calcContent; }
            set
            {
                calcContent = value;
                OnPropertyChanged(nameof(CalcContent));
            }
        }
        private string calcResult;
        public string CalcResult
        {
            get { return calcResult; }
            set
            {
                calcResult = value;
                OnPropertyChanged(nameof(CalcResult));
            }
        }
        private bool isFormula;
        public bool IsFormula
        {
            get { return isFormula; }
            set
            {
                isFormula = value;
                OnPropertyChanged(nameof(IsFormula));
            }
        }
        private bool isBitwise;
        public bool IsBitwise
        {
            get { return isBitwise; }
            set
            {
                isBitwise = value;
                OnPropertyChanged(nameof(IsBitwise));
            }
        }
        private bool isContinuous;
        public bool IsContinuous
        {
            get { return isContinuous; }
            set
            {
                isContinuous = value;
                OnPropertyChanged(nameof(IsContinuous));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
