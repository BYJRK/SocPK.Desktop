using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SocPK.Desktop.Helpers;
using SocPK.Desktop.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocPK.Desktop.ViewModels
{
    public sealed class MainWindowViewModel : ObservableObject
    {
        private readonly WebHelper webHelper;

        public ObservableCollection<Soc> SocList { get; private set; }

        public MainWindowViewModel(WebHelper webHelper)
        {
            LoadedCommand = new AsyncRelayCommand(GetSocListAsync);
            this.webHelper = webHelper;
        }

        #region Relay Commands

        public AsyncRelayCommand LoadedCommand { get; }

        #endregion

        async Task GetSocListAsync()
        {
            var url = @"https://www.socpk.com/overall/soclist.js";
            var res = await webHelper.GetStringAsync(url);
            var socMatches = Regex.Matches(res, @"\['(.*?)','(.*?)',([0-9.]+?)\]");
            var socList = new List<Soc>();
            foreach (Match match in socMatches)
            {
                socList.Add(
                    new Soc(
                        match.Groups[1].Value,
                        match.Groups[2].Value,
                        double.Parse(match.Groups[3].Value)
                    )
                );
            }
            var brands = new[] { "苹果", "联发科", "华为", "高通", "三星" };
            SocList = new ObservableCollection<Soc>(
                socList.Where(s => brands.Contains(s.Brand)).OrderByDescending(s => s.Score)
            );
        }
    }
}
