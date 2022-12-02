﻿using feladat8.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace feladat8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            List<Person> Population = new List<Person>();
            List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
            List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            for(int year =2005; year<=2024; year++)
            {
                for (int i=0; i<Population.Count; i++)
                {

                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                  where x.Gender == Gender.Female && x.IsAlive
                                  select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));



            }
        }


        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr= new StreamReader(csvpath,Encoding.Default))
            {
                while(!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear=int.Parse(line[0]),
                        Gender=(Gender)Enum.Parse(typeof(Gender),line[1]),
                        NbrOfChildren=int.Parse(line[2])
                    });
                }
            }

            return population;
        }


        public List<BirthProbability> GetBirthProbabilities(string csvpath2)
        {
            List<BirthProbability> birthprobability = new List<BirthProbability>();

            using(StreamReader sr= new StreamReader(csvpath2,Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthprobability.Add(new BirthProbability()
                    {
                        Age=int.Parse(line[0]),
                        NbrOfChildren=int.Parse(line[1]),
                        Probability=Convert.ToDouble(line[2])
                    });
                }
            }

            return birthprobability;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath3)
        {
            List<DeathProbability> deathprobability = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath3, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathprobability.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        Probability = Convert.ToDouble(line[2])
                    });
                }
            }

            return deathprobability;
        }

        Random rng = new Random(1234);
    }
}
