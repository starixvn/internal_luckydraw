using LuckyDraw;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace LuckyDraw.Model
{
    class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ViewPrize> PrizeList
        {
            get
            {
                using (var db = new LuckyDrawEntities())
                {
                    var collection = new ObservableCollection<ViewPrize>();

                    foreach (var h in db.Prizes.ToList())
                    {
                        var p = new ViewPrize();                        
                        p.Id = h.Id;
                        p.PrizeIndex = h.PrizeIndex;
                        p.PrizeName = h.PrizeName;
                        p.RoundIndex = h.RoundIndex;
                        p.RoundName = h.RoundName;

                        collection.Add(p);
                    }


                    return collection;
                }
            }         
            
          
        }

        public ObservableCollection<Setting> SettingList
        {
            get
            {
                using (var db = new LuckyDrawEntities())
                {
                    var collection = new ObservableCollection<Setting>();

                    foreach (var h in db.Settings.ToList())
                    {
                        collection.Add(h);
                    }


                    return collection;
                }
            }
        }       

        public ObservableCollection<ViewResult> ResultList
        {
            get
            {
                using (var db = new LuckyDrawEntities())
                {
                    var collection = new ObservableCollection<ViewResult>();

                    if (db.Results.Count() > 0)
                    {
                        var flag = db.Results.Max(i => i.Flag);
                        var resultList = db.Results.Where(i => i.Flag == flag);

                        foreach (var h in resultList.ToList())
                        {
                            var r = new ViewResult();
                            r.PrizeIndex = h.PrizeIndex;
                            r.PrizeName = h.PrizeName;
                            r.Random = h.Random;
                            r.RoundName = h.RoundName;

                            collection.Add(r);
                        }
                    }

                    return collection;
                }
            }
        }

        public ObservableCollection<ViewLog> LogList
        {
            get
            {
                using (var db = new LuckyDrawEntities())
                {
                    var collection = new ObservableCollection<ViewLog>();

                    foreach (var h in db.Results.ToList())
                    {
                        var r = new ViewLog();
                        r.Flag = h.Flag;
                        r.PrizeIndex = h.PrizeIndex;
                        r.PrizeName = h.PrizeName;
                        r.Random = h.Random;
                        r.RoundName = h.RoundName;
                        r.UpdatedTime = h.UpdatedTime;

                        collection.Add(r);
                    }

                    return collection;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
