using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LuckyDraw.Views
{
    /// <summary>
    /// Interaction logic for CenterItem.xaml
    /// </summary>
    public partial class CenterItem : Window
    {
        public CenterItem()
        {
            InitializeComponent();
        }

        public void LoadItemData(Result itemData)
        {
            this.centerItem.LoadItemData(itemData);
        }

        public Task RunRandom(string random, int randomTime)
        {
           return this.centerItem.RunRandom(random, randomTime);
        }       
    }
}
