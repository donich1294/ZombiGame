﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EventArgument;
using System.Drawing;
using PathFinder;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Model.Objects 
{
    public class Person : GameObject, INotifyPropertyChanged
    {
        #region Fields
        protected String heroName;
        protected int helthPoint;
        protected int damage;
        protected int armor;
        protected int moveSpead;
        protected int mana;
        protected int range;
        protected AttackStyle attackStyle;
        private bool whatToDo = false;

        #endregion
        #region Properties
        public Side Team { get; set; }
        public Person Goal { get; set; }
        new public int X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");

            }
        }

        new public int Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");

            }
        }
        public int HelthPoint
        {
            get { return helthPoint; }
            set
            {
                helthPoint = value;
                OnPropertyChanged("HelthPoint");

            }
        }

        

        public Person(int helthPoint, int x, int y, Side team) 
        {
            this.HelthPoint = helthPoint;
            this.x = x;
            this.y = y;
            this.Team = team;
        }

        private bool isAlive() {
            if (HelthPoint <= 0)
            {
                return false;
            }
            else
                return true;
           
        }
        #endregion
        /*private void Move(ObservableCollection<Person> Persons)
        {
            List<Point> CurentPoints = new List<Point>();
            int minCount = 1000;
            Point bestStep = new Point(this.X, this.Y);
            foreach(Person person in Persons)
            {
                if ((this.x != person.x) && (person.y != this.y) && this.team != person.team)
                {
                    Map map = Map.Instance;

                    CurentPoints = FindWay.FindPath(map.ways, new Point(this.x, this.y), new Point(person.x, person.y));
                    if (CurentPoints == null) break;
                    if (CurentPoints.Count != 0 && CurentPoints.Count < minCount)
                    {
                        bestStep = CurentPoints[0];
                    }
                }
            }

            //Map.Instance.ways[this.X, this.Y] = 0;
            //Map.Instance.ways[bestStep.X, bestStep.Y] = 1;

            this.X = bestStep.X;
            this.Y = bestStep.Y;
        }*/
        private void Move()
        {
            if(Goal!=null)
            {
                if (X > Goal.X)
                {
                    X--;
        }
                else if (X < Goal.X)
                {
                    X++;
                }
                if (Y > Goal.Y)
                {
                    Y--;
                }
                else if (Y < Goal.Y)
                {
                    Y++;
                }

                if((X + 10  == Goal.X) || (Y + 10 == Goal.Y) || (X - 10 == Goal.X) || (Y - 10 == Goal.Y))
                {
                    whatToDo = true;
                }
            }
        }
        public event EventHandler<EnemyAttack_Event> AttackEnemy;

        public void TakingDamage(object sender, EnemyAttack_Event e)
        {
            int realDamage = e.getEnemyDamage() - armor / 10;
            HelthPoint -= realDamage;
        }



        public void Live()
        {
            while (isAlive())
            {
                Move(World.Instance.Persons);
            }
            return goal;
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

    }
}
