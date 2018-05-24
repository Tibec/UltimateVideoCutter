using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using VideoCutterMax2.Model;

namespace VideoCutterMax2.Model
{
    class CutDatabase
    {
        public Uri VideoPath { get; set; }
        static private CutDatabase instance;
        private ObservableCollection<Cut> _cuts;
       

        private CutDatabase()
        {
            _cuts = new ObservableCollection<Cut>();
            instance = null;
            
        }

        public static CutDatabase GetCutDatabase()
        {
            if(instance == null)
            {
                instance = new CutDatabase();
  
            }
            
            return instance;
        }

        public ObservableCollection<Cut> GetCollection()
        {
            return instance._cuts;
        }

        public void AddCut(Cut c)
        {
            instance._cuts.Add(c);
        }
        public int Count()
        {
            if (instance != null)
            {
                return instance._cuts.Count();
            }
            else
            {
                return 0;
            }
            
        }
        public void RemoveAt(int i)
        {
            instance._cuts.RemoveAt(i);
        }

        public void ResetCutsDatabase()
        {
            instance._cuts.Clear();
        }
        public Cut GetAt(int i)
        {
            return instance._cuts[i];
        }

        public bool IsEmpty()
        {
            if(instance._cuts.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
