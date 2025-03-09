using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            filter = "";
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
            calcContent = "";
            calcResult = "";
            isFormula = false;
            isBitwise = false;
            isContinuous = false;
            calcHistory = new List<double>();
        }
        private string calcContent;
        private string calcResult;
        private bool isFormula;
        private bool isBitwise;
        private bool isContinuous;
        private ICollection<double> calcHistory;

        /// <summary>
        /// 计算内容
        /// </summary>
        public string CalcContent
        {
            get { return calcContent; }
            set
            {
                calcContent = value;
                OnPropertyChanged(nameof(CalcContent));
            }
        }
        /// <summary>
        /// 计算结果
        /// </summary>
        public string CalcResult
        {
            get { return calcResult; }
            set
            {
                calcResult = value;
                OnPropertyChanged(nameof(CalcResult));
            }
        }
        /// <summary>
        /// 是否是公式
        /// </summary>
        public bool IsFormula
        {
            get { return isFormula; }
            set
            {
                isFormula = value;
                OnPropertyChanged(nameof(IsFormula));
            }
        }
        /// <summary>
        /// 是否是位运算
        /// </summary>
        public bool IsBitwise
        {
            get { return isBitwise; }
            set
            {
                isBitwise = value;
                OnPropertyChanged(nameof(IsBitwise));
            }
        }
        /// <summary>
        /// 结果是否连续
        /// </summary>
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

        public Task CalcAsync(bool asc = true)
        {
            var expression = calcContent.Split('*');
            IsFormula = calcContent.Contains('*') && expression.Length == 2;
            if (isFormula)
            {
                var exp_1 = expression[0];
                var exp_2 = expression[1];
                if (isBitwise)
                {
                    for (int i = 1; i <= exp_2.Length; i++)
                    {
                        if (exp_2[i - 1] == '.')
                        {
                            continue;
                        }
                        calcHistory.Add(double.Parse(exp_1) * double.Parse(exp_2[..i]));
                    }

                }
                else
                {
                    var result = double.Parse(exp_1) * double.Parse(exp_2);
                    calcHistory.Add(result);
                }
            }
            else
            {
                calcHistory.Add(double.Parse(calcContent));
            }

            var link = isContinuous ? " " : "\r\n";
            this.CalcResult = string.Join(link, calcHistory);
            return Task.CompletedTask;
        }
    }
}