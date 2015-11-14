﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model
{
    public class Map : INotifyPropertyChanged
    {
        private AreaType[,] areas;
        public ObservableCollection<MapArea> MapArias { get; private set; }
        #region Singleton

        private static readonly Object lockObject = new Object();
        private static Map instance = null;
        private Map()
        {
            MapArias = new ObservableCollection<MapArea>();
            areas = GenMap(10, 10);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    MapArias.Add(new MapArea(i, j, areas[i,j]));
        }
            }
        }
        public static Map Instance
        {
            get
            {
                if (instance != null) return instance;
                Monitor.Enter(lockObject);
                Map temp = new Map();
                Interlocked.Exchange(ref instance, temp);
                Monitor.Exit(lockObject);
                return instance;
            }
        }
        #endregion+		rnd	null	System.Random

        public AreaType[,] Areas
        {
            get { return areas; }
            set
            {
                areas = value;
                OnPropertyChanged("Areas");

            }
        }

        #region Implement INotyfyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public AreaType[,] GenMap(int x, int y)
        {
            
           Random rnd = new Random();
            AreaType[,] map = new AreaType[x, y];
            
            for (int i = 0; i < x; i++)
                for (int o = 0; o < y; o++) 
                {
                    map[i, o] = (AreaType)rnd.Next(0, 4);
                }
                    
            int checkSumX = 0;
            int checkSumY = 0;


            for (int i = 0; i < x; i++)
            {
                for (int o = 0; o < y; o++)
                {
                    checkSumX += (int)map[i, o];
                    checkSumY += (int)map[o, i];
                }
                if (checkSumX == 10)
                {
                    int j = rnd.Next(0, 99);
                    map[i, j] = (AreaType)0;
                }
                if (checkSumY == 10)
                {
                    int j = rnd.Next(0, 99);
                    map[j, i] = (AreaType)0;
                }
                }
            return map;
        }
    }
}

